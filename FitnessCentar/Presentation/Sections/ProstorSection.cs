using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class ProstorSection : SectionPanel
    {
        private readonly IProstorService _prostorService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj");
        private readonly Button _btnDodaj = Theme.CreateActionButton("Dodaj test", Theme.AccentDark);

        public ProstorSection(IProstorService prostorService)
            : base("Prostor")
        {
            _prostorService = prostorService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(_btnDodaj);
            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajProstoreAsync();
            _btnDodaj.Click += async (s, e) => await DodajTestProstorAsync();
        }

        private async Task UcitajProstoreAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _prostorService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} prostora.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajTestProstorAsync()
        {
            _btnDodaj.Enabled = false;
            var noviProstor = new ProstorDTO
            {
                Naziv = $"Test prostor {DateTime.Now:HHmmss}",
                Tip = "teretana",
                Kapacitet = 20,
                Lokacija = "Test lokacija",
                RadnoVreme = "08-20h"
            };
            var rezultat = await _prostorService.AddProstorAsync(noviProstor);
            SetStatus(rezultat.IsSuccess ? "Test prostor uspešno dodat u bazu." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodaj.Enabled = true;
            if (rezultat.IsSuccess) await UcitajProstoreAsync();
        }
    }
}