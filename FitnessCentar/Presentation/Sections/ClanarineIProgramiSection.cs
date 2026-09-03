using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class ClanarineIProgramiSection : SectionPanel
    {
        private readonly IClanarinaService _clanarinaService;
        private readonly IFitnessProgramService _programService;

        private readonly DataGridView _gridClanarine = new() { AutoGenerateColumns = true };
        private readonly DataGridView _gridProgrami = new() { AutoGenerateColumns = true };

        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj");
        private readonly Button _btnDodajClanarinu = Theme.CreateActionButton("Dodaj članarinu", Theme.AccentDark);
        private readonly Button _btnDodajProgram = Theme.CreateActionButton("Dodaj program", Theme.AccentDark);
        private readonly TextBox _txtUpisKorisnikId = Theme.CreateInput("ID za upis", 80);
        private readonly TextBox _txtUpisClanarinaId = Theme.CreateInput("ID za upis", 80);
        private readonly Button _btnUpisi = Theme.CreateActionButton("Upiši korisnika u članarinu", Color.FromArgb(0x6B, 0x21, 0xA8));

        public ClanarineIProgramiSection(IClanarinaService clanarinaService, IFitnessProgramService programService)
            : base("Članarine i programi")
        {
            _clanarinaService = clanarinaService;
            _programService = programService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(_btnDodajClanarinu);
            ActionsFlow.Controls.Add(_btnDodajProgram);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("ID korisnika:"));
            ActionsFlow.Controls.Add(_txtUpisKorisnikId);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("ID članarine:"));
            ActionsFlow.Controls.Add(_txtUpisClanarinaId);
            ActionsFlow.Controls.Add(_btnUpisi);

            PutTwoGrids("Članarine", _gridClanarine, "Programi", _gridProgrami, 38f);

            _btnUcitaj.Click += async (s, e) => await UcitajClanarinePrograme();
            _btnDodajClanarinu.Click += async (s, e) => await DodajTestClanarinuAsync();
            _btnDodajProgram.Click += async (s, e) => await DodajTestProgramAsync();
            _btnUpisi.Click += async (s, e) => await UpisiKorisnikaAsync();
        }

        private async Task UcitajClanarinePrograme()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var clanarine = await _clanarinaService.GetAllAsync();
            var programi = await _programService.GetAllAsync();
            if (!clanarine.IsSuccess) SetStatus($"GREŠKA (članarine): {clanarine.ErrorMessage}", false);
            else if (!programi.IsSuccess) SetStatus($"GREŠKA (programi): {programi.ErrorMessage}", false);
            else
            {
                _gridClanarine.DataSource = clanarine.Data;
                _gridProgrami.DataSource = programi.Data;
                SetStatus($"Učitano {clanarine.Data?.Count ?? 0} članarina i {programi.Data?.Count ?? 0} programa.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajTestClanarinuAsync()
        {
            _btnDodajClanarinu.Enabled = false;
            var novaClanarina = new ClanarinaDTO
            {
                Naziv = $"Test članarina {DateTime.Now:HHmmss}",
                Tip = TipClanarine.mesecna,
                Cena = 3000,
                PeriodVazenja = 30
            };
            var rezultat = await _clanarinaService.AddClanarinaAsync(novaClanarina);
            SetStatus(rezultat.IsSuccess ? "Test članarina uspešno dodata." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajClanarinu.Enabled = true;
            if (rezultat.IsSuccess) await UcitajClanarinePrograme();
        }

        private async Task DodajTestProgramAsync()
        {
            _btnDodajProgram.Enabled = false;
            var noviProgram = new FitnessProgramDTO
            {
                Naziv = $"Test program {DateTime.Now:HHmmss}",
                Tip = TipPrograma.individualni,
                Cilj = "Test cilj",
                Trajanje = "3 meseca"
            };
            var rezultat = await _programService.AddProgramAsync(noviProgram);
            SetStatus(rezultat.IsSuccess ? "Test program uspešno dodat." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajProgram.Enabled = true;
            if (rezultat.IsSuccess) await UcitajClanarinePrograme();
        }

        private async Task UpisiKorisnikaAsync()
        {
            if (!int.TryParse(_txtUpisKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtUpisClanarinaId.Text, out int clanarinaId))
            {
                SetStatus("Unesi validne ID-jeve korisnika i članarine u polja iznad.", false);
                return;
            }
            _btnUpisi.Enabled = false;
            var rezultat = await _clanarinaService.UpisiKorisnikaAsync(korisnikId, clanarinaId, DateTime.Now, DateTime.Now.AddDays(30));
            SetStatus(rezultat.IsSuccess ? "Korisnik uspešno upisan u članarinu." : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnUpisi.Enabled = true;
        }
    }
}