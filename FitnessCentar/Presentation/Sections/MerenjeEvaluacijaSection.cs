using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class MerenjeEvaluacijaSection : SectionPanel
    {
        private readonly IMerenjeEvaluacijaService _merenjeEvaluacijaService;

        private readonly DataGridView _gridMerenja = new() { AutoGenerateColumns = true };
        private readonly DataGridView _gridEvaluacije = new() { AutoGenerateColumns = true };

        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj merenja i evaluacije");
        private readonly TextBox _txtKorisnikId = Theme.CreateInput("ID korisnika (npr. 1)", 170);
        private readonly TextBox _txtOsobljeId = Theme.CreateInput("ID osoblja koje meri (npr. 1)", 190);
        private readonly Button _btnDodajMerenje = Theme.CreateActionButton("Dodaj test merenje", Theme.AccentDark);
        private readonly Button _btnDodajEvaluaciju = Theme.CreateActionButton("Dodaj test evaluaciju (isti ID-jevi iznad)", Theme.AccentDark);

        public MerenjeEvaluacijaSection(IMerenjeEvaluacijaService merenjeEvaluacijaService)
            : base("Merenje i evaluacija", "Pregled i test unos merenja i evaluacija napretka korisnika.")
        {
            _merenjeEvaluacijaService = merenjeEvaluacijaService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Korisnik ID:"));
            ActionsFlow.Controls.Add(_txtKorisnikId);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Osoblje ID:"));
            ActionsFlow.Controls.Add(_txtOsobljeId);
            ActionsFlow.Controls.Add(_btnDodajMerenje);
            ActionsFlow.Controls.Add(_btnDodajEvaluaciju);

            PutTwoGrids("Merenja", _gridMerenja, "Evaluacije", _gridEvaluacije, 38f);

            _btnUcitaj.Click += async (s, e) => await UcitajMerenjaEvaluacijeAsync();
            _btnDodajMerenje.Click += async (s, e) => await DodajMerenjeAsync();
            _btnDodajEvaluaciju.Click += async (s, e) => await DodajEvaluacijuAsync();
        }

        private async Task UcitajMerenjaEvaluacijeAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");

            var merenja = await _merenjeEvaluacijaService.GetAllMerenjaAsync();
            var evaluacije = await _merenjeEvaluacijaService.GetAllEvaluacijeAsync();

            if (!merenja.IsSuccess) SetStatus($"GREŠKA (merenja): {merenja.ErrorMessage}", false);
            else if (!evaluacije.IsSuccess) SetStatus($"GREŠKA (evaluacije): {evaluacije.ErrorMessage}", false);
            else
            {
                _gridMerenja.DataSource = merenja.Data;
                _gridEvaluacije.DataSource = evaluacije.Data;
                SetStatus($"Učitano {merenja.Data?.Count ?? 0} merenja i {evaluacije.Data?.Count ?? 0} evaluacija.", true);
            }

            _btnUcitaj.Enabled = true;
        }

        private async Task DodajMerenjeAsync()
        {
            if (!int.TryParse(_txtKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtOsobljeId.Text, out int osobljeId))
            {
                SetStatus("Unesi validan ID korisnika i ID osoblja u polja iznad.", false);
                return;
            }

            _btnDodajMerenje.Enabled = false;
            var rezultat = await _merenjeEvaluacijaService.AddMerenjeAsync(korisnikId, osobljeId, "telesna masa", 80, "kg");
            SetStatus(rezultat.IsSuccess
                ? "Test merenje uspešno dodato."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajMerenje.Enabled = true;
            if (rezultat.IsSuccess) await UcitajMerenjaEvaluacijeAsync();
        }

        private async Task DodajEvaluacijuAsync()
        {
            if (!int.TryParse(_txtKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtOsobljeId.Text, out int osobljeId))
            {
                SetStatus("Unesi validan ID korisnika i ID osoblja u polja iznad.", false);
                return;
            }

            _btnDodajEvaluaciju.Enabled = false;
            var rezultat = await _merenjeEvaluacijaService.AddEvaluacijuAsync(
                korisnikId, osobljeId, "Test opis napretka korisnika.", "Test preporuka za nastavak treninga.");
            SetStatus(rezultat.IsSuccess
                ? "Test evaluacija uspešno dodata - poslednji klaster potvrđen!"
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajEvaluaciju.Enabled = true;
            if (rezultat.IsSuccess) await UcitajMerenjaEvaluacijeAsync();
        }
    }
}
