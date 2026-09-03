using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class KorisniciSection : SectionPanel
    {
        private readonly IKorisnikService _korisnikService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj");
        private readonly Button _btnDodaj = Theme.CreateActionButton("Dodaj test", Theme.AccentDark);

        public KorisniciSection(IKorisnikService korisnikService)
            : base("Korisnici")
        {
            _korisnikService = korisnikService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(_btnDodaj);
            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajKorisnikeAsync();
            _btnDodaj.Click += async (s, e) => await DodajTestKorisnikaAsync();
        }

        private async Task UcitajKorisnikeAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _korisnikService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} korisnika.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajTestKorisnikaAsync()
        {
            _btnDodaj.Enabled = false;
            var noviKorisnik = new KorisnikDTO
            {
                Ime = "Test",
                Prezime = $"Korisnik{DateTime.Now:HHmmss}",
                DatumRodjenja = new DateTime(1995, 1, 1),
                Pol = Pol.M,
                Email = $"test{DateTime.Now:HHmmss}@email.com",
                DatumUclanjenja = DateTime.Now,
                StatusClanstva = StatusClanstva.aktivan,
                JeRekreativac = true,
                Ciljevi = new List<string> { "kondicija" }
            };
            var rezultat = await _korisnikService.AddKorisnikAsync(noviKorisnik);
            SetStatus(rezultat.IsSuccess ? "Test korisnik uspešno dodat u bazu." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodaj.Enabled = true;
            if (rezultat.IsSuccess) await UcitajKorisnikeAsync();
        }
    }
}