using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{

    internal sealed class PlanIshraneSection : SectionPanel
    {
        private readonly IPlanIshraneService _planIshraneService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj");

        private readonly TextBox _txtKorisnikId = Theme.CreateInput("ID korisnika", 80);
        private readonly TextBox _txtNutricionistaId = Theme.CreateInput("ID osoblja", 70);
        private readonly Button _btnDodajPlan = Theme.CreateActionButton("1.Dodaj plan", Theme.AccentDark);

        private readonly TextBox _txtPlanIdZaObrok = Theme.CreateInput("ID plana", 65);
        private readonly Button _btnDodajObrok = Theme.CreateActionButton("2.Dodaj obrok", Theme.AccentDark);

        private readonly TextBox _txtPlanIdZaOgranicenje = Theme.CreateInput("ID plana", 65);
        private readonly Button _btnDodajOgranicenje = Theme.CreateActionButton("3.Dodaj ogr.", Theme.AccentDark);

        public PlanIshraneSection(IPlanIshraneService planIshraneService)
            : base("Plan ishrane")
        {
            _planIshraneService = planIshraneService;

            ActionsFlow.Controls.Add(_btnUcitaj);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Korisnik ID:"));
            ActionsFlow.Controls.Add(_txtKorisnikId);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Nutricionista ID:"));
            ActionsFlow.Controls.Add(_txtNutricionistaId);
            ActionsFlow.Controls.Add(_btnDodajPlan);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Plan ishrane ID:"));
            ActionsFlow.Controls.Add(_txtPlanIdZaObrok);
            ActionsFlow.Controls.Add(_btnDodajObrok);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Plan ishrane ID:"));
            ActionsFlow.Controls.Add(_txtPlanIdZaOgranicenje);
            ActionsFlow.Controls.Add(_btnDodajOgranicenje);

            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajPlanoveIshraneAsync();
            _btnDodajPlan.Click += async (s, e) => await DodajPlanIshraneAsync();
            _btnDodajObrok.Click += async (s, e) => await DodajObrokAsync();
            _btnDodajOgranicenje.Click += async (s, e) => await DodajOgranicenjeAsync();
        }

        private async Task UcitajPlanoveIshraneAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _planIshraneService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} planova ishrane.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajPlanIshraneAsync()
        {
            if (!int.TryParse(_txtKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtNutricionistaId.Text, out int nutricionistaId))
            {
                SetStatus("Unesi validan ID korisnika i nutricioniste u polja iznad.", false);
                return;
            }

            _btnDodajPlan.Enabled = false;
            var rezultat = await _planIshraneService.AddPlanAsync(korisnikId, nutricionistaId, $"Test plan ishrane {DateTime.Now:HHmmss}");
            SetStatus(rezultat.IsSuccess
                ? "Plan ishrane dodat. Klikni 'Učitaj planove ishrane' da vidiš njegov ID."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajPlan.Enabled = true;
            if (rezultat.IsSuccess) await UcitajPlanoveIshraneAsync();
        }

        private async Task DodajObrokAsync()
        {
            if (!int.TryParse(_txtPlanIdZaObrok.Text, out int planIshraneId))
            {
                SetStatus("Unesi validan ID plana ishrane u polje.", false);
                return;
            }

            _btnDodajObrok.Enabled = false;
            var rezultat = await _planIshraneService.AddObrokAsync(planIshraneId, "Doručak", 350, 20);
            SetStatus(rezultat.IsSuccess
                ? "Obrok uspešno dodat."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajObrok.Enabled = true;
            if (rezultat.IsSuccess) await UcitajPlanoveIshraneAsync();
        }

        private async Task DodajOgranicenjeAsync()
        {
            if (!int.TryParse(_txtPlanIdZaOgranicenje.Text, out int planIshraneId))
            {
                SetStatus("Unesi validan ID plana ishrane u polje.", false);
                return;
            }

            _btnDodajOgranicenje.Enabled = false;
            var rezultat = await _planIshraneService.AddOgranicenjeAsync(planIshraneId, "bez glutena");
            SetStatus(rezultat.IsSuccess
                ? "Ograničenje uspešno dodato - ceo lanac Plan ishrane -> Obrok/Ograničenja radi!"
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajOgranicenje.Enabled = true;
            if (rezultat.IsSuccess) await UcitajPlanoveIshraneAsync();
        }
    }
}