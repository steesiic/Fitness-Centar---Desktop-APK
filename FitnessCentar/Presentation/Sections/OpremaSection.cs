using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class OpremaSection : SectionPanel
    {
        private readonly IOpremaService _opremaService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj opremu");
        private readonly TextBox _txtProstorId = Theme.CreateInput("ID prostora kom oprema pripada (npr. 1)");
        private readonly Button _btnDodaj = Theme.CreateActionButton("Dodaj test opremu za uneti ID prostora", Theme.AccentDark);

        public OpremaSection(IOpremaService opremaService)
            : base("Oprema", "Pregled i test unos opreme, vezane za konkretan prostor.")
        {
            _opremaService = opremaService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("ID prostora:"));
            ActionsFlow.Controls.Add(_txtProstorId);
            ActionsFlow.Controls.Add(_btnDodaj);
            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajOpremuAsync();
            _btnDodaj.Click += async (s, e) => await DodajTestOpremuAsync();
        }

        private async Task UcitajOpremuAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _opremaService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} komada opreme.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajTestOpremuAsync()
        {
            if (!int.TryParse(_txtProstorId.Text, out int prostorId))
            {
                SetStatus("Prvo unesi validan ID prostora (npr. 1) u polje iznad.", false);
                return;
            }
            _btnDodaj.Enabled = false;
            var novaOprema = new OpremaDTO
            {
                Naziv = $"Test sprava {DateTime.Now:HHmmss}",
                Tip = "sprava za snagu",
                Proizvodjac = "Test proizvođač",
                DatumNabavke = DateTime.Now,
                Status = "ispravna",
                Lokacija = "Test lokacija",
                RasporedOdrzavanja = "mesečno",
                ProstorId = prostorId
            };
            var rezultat = await _opremaService.AddOpremaAsync(novaOprema);
            SetStatus(rezultat.IsSuccess ? "Test oprema uspešno dodata u bazu." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodaj.Enabled = true;
            if (rezultat.IsSuccess) await UcitajOpremuAsync();
        }
    }
}
