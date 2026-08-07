using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class OsobljeSection : SectionPanel
    {
        private readonly IOsobljeService _osobljeService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj osoblje");
        private readonly Button _btnDodaj = Theme.CreateActionButton("Dodaj test osoblje (trener + personalni trener)", Theme.AccentDark);

        public OsobljeSection(IOsobljeService osobljeService)
            : base("Osoblje", "Pregled i test unos zaposlenih.")
        {
            _osobljeService = osobljeService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(_btnDodaj);
            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajOsobljeAsync();
            _btnDodaj.Click += async (s, e) => await DodajTestOsobljeAsync();
        }

        private async Task UcitajOsobljeAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _osobljeService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} zaposlenih.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajTestOsobljeAsync()
        {
            _btnDodaj.Enabled = false;
            var novoOsoblje = new OsobljeDTO
            {
                Ime = "Test",
                Prezime = $"Trener{DateTime.Now:HHmmss}",
                Email = $"trener{DateTime.Now:HHmmss}@fitcentar.rs",
                DatumZaposlenja = DateTime.Now,
                Status = StatusOsoblja.aktivan,
                JeTrener = true,
                NivoIskustvaTrenera = NivoIskustva.pocetnik,
                JePersonalniTrener = true
            };
            var rezultat = await _osobljeService.AddOsobljeAsync(novoOsoblje);
            SetStatus(rezultat.IsSuccess ? "Test osoblje uspešno dodato." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodaj.Enabled = true;
            if (rezultat.IsSuccess) await UcitajOsobljeAsync();
        }
    }
}
