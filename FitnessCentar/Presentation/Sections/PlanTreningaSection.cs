using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    /// <summary>
    /// Testira ceo lanac Plan -> Faza -> Nedelja -> Vežba, korak po korak
    /// (isti tok kao original, samo grupisan u kartice po koraku).
    /// </summary>
    internal sealed class PlanTreningaSection : SectionPanel
    {
        private readonly IPlanTreningaService _planTreningaService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj planove");

        private readonly TextBox _txtKorisnikId = Theme.CreateInput("ID korisnika (npr. 1)", 150);
        private readonly TextBox _txtOsobljeId = Theme.CreateInput("ID osoblja - trener (npr. 1)", 170);
        private readonly Button _btnDodajPlan = Theme.CreateActionButton("1. Dodaj plan treninga", Theme.AccentDark);

        private readonly TextBox _txtPlanIdZaFazu = Theme.CreateInput("ID plana (iz koraka 1)", 170);
        private readonly Button _btnDodajFazu = Theme.CreateActionButton("2. Dodaj fazu (redosled 1)", Theme.AccentDark);

        private readonly TextBox _txtFazaIdZaNedelju = Theme.CreateInput("ID faze (iz koraka 2)", 170);
        private readonly Button _btnDodajNedelju = Theme.CreateActionButton("3. Dodaj nedelju (broj 1)", Theme.AccentDark);

        private readonly TextBox _txtNedeljaIdZaVezbu = Theme.CreateInput("ID nedelje (iz koraka 3)", 170);
        private readonly Button _btnDodajVezbu = Theme.CreateActionButton("4. Dodaj vežbu", Theme.AccentDark);

        public PlanTreningaSection(IPlanTreningaService planTreningaService)
            : base("Plan treninga", "Test celog lanca: Plan → Faza → Nedelja → Vežba.")
        {
            _planTreningaService = planTreningaService;

            ActionsFlow.Controls.Add(_btnUcitaj);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Korisnik ID:"));
            ActionsFlow.Controls.Add(_txtKorisnikId);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Osoblje ID:"));
            ActionsFlow.Controls.Add(_txtOsobljeId);
            ActionsFlow.Controls.Add(_btnDodajPlan);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Plan ID:"));
            ActionsFlow.Controls.Add(_txtPlanIdZaFazu);
            ActionsFlow.Controls.Add(_btnDodajFazu);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Faza ID:"));
            ActionsFlow.Controls.Add(_txtFazaIdZaNedelju);
            ActionsFlow.Controls.Add(_btnDodajNedelju);

            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("Nedelja ID:"));
            ActionsFlow.Controls.Add(_txtNedeljaIdZaVezbu);
            ActionsFlow.Controls.Add(_btnDodajVezbu);

            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajPlanoveAsync();
            _btnDodajPlan.Click += async (s, e) => await DodajPlanAsync();
            _btnDodajFazu.Click += async (s, e) => await DodajFazuAsync();
            _btnDodajNedelju.Click += async (s, e) => await DodajNedeljuAsync();
            _btnDodajVezbu.Click += async (s, e) => await DodajVezbuAsync();
        }

        private async Task UcitajPlanoveAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _planTreningaService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} planova.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajPlanAsync()
        {
            if (!int.TryParse(_txtKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtOsobljeId.Text, out int osobljeId))
            {
                SetStatus("Unesi validan ID korisnika i ID osoblja u polja iznad (korak 1).", false);
                return;
            }

            _btnDodajPlan.Enabled = false;
            var rezultat = await _planTreningaService.AddPlanAsync(korisnikId, osobljeId, $"Test plan {DateTime.Now:HHmmss}");
            SetStatus(rezultat.IsSuccess
                ? "Plan dodat. Klikni 'Učitaj planove' da vidiš njegov PlanId, pa ga unesi u sledeći korak."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajPlan.Enabled = true;
            if (rezultat.IsSuccess) await UcitajPlanoveAsync();
        }

        private async Task DodajFazuAsync()
        {
            if (!int.TryParse(_txtPlanIdZaFazu.Text, out int planId))
            {
                SetStatus("Unesi validan ID plana u polje (korak 2).", false);
                return;
            }

            _btnDodajFazu.Enabled = false;
            var rezultat = await _planTreningaService.AddFazuAsync(planId, "Faza 1 - Adaptacija", 1, "Test faza");
            SetStatus(rezultat.IsSuccess
                ? "Faza dodata. Proveri njen FazaId u bazi (SELECT FAZA_ID FROM FAZA_PLANA WHERE PLAN_ID = ...), pa ga unesi u korak 3."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajFazu.Enabled = true;
        }

        private async Task DodajNedeljuAsync()
        {
            if (!int.TryParse(_txtFazaIdZaNedelju.Text, out int fazaId))
            {
                SetStatus("Unesi validan ID faze u polje (korak 3).", false);
                return;
            }

            _btnDodajNedelju.Enabled = false;
            var rezultat = await _planTreningaService.AddNedeljuAsync(fazaId, 1, "Test nedelja");
            SetStatus(rezultat.IsSuccess
                ? "Nedelja dodata. Proveri njen NedeljaId u bazi, pa ga unesi u korak 4."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajNedelju.Enabled = true;
        }

        private async Task DodajVezbuAsync()
        {
            if (!int.TryParse(_txtNedeljaIdZaVezbu.Text, out int nedeljaId))
            {
                SetStatus("Unesi validan ID nedelje u polje (korak 4).", false);
                return;
            }

            _btnDodajVezbu.Enabled = false;
            var rezultat = await _planTreningaService.AddVezbuAsync(nedeljaId, "Čučnjevi", "snaga", 3, 12);
            SetStatus(rezultat.IsSuccess
                ? "Vežba uspešno dodata - ceo lanac Plan -> Faza -> Nedelja -> Vežba radi!"
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajVezbu.Enabled = true;
        }
    }
}
