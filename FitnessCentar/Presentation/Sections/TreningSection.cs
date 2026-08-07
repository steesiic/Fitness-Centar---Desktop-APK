using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation.Sections
{
    internal sealed class TreningSection : SectionPanel
    {
        private readonly ITreningService _treningService;

        private readonly DataGridView _grid = new() { AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = Theme.CreateActionButton("Učitaj treninge");
        private readonly TextBox _txtKorisnikId = Theme.CreateInput("ID korisnika (za individualni test, npr. 1)", 240);
        private readonly TextBox _txtProstorId = Theme.CreateInput("ID prostora (za tradicionalni test, npr. 1)", 240);
        private readonly Button _btnDodajIndividualniTradicionalni = Theme.CreateActionButton("Dodaj: Individualni + Tradicionalni", Theme.AccentDark);
        private readonly Button _btnDodajGrupniOnline = Theme.CreateActionButton("Dodaj: Grupni + Online", Theme.AccentDark);

        public TreningSection(ITreningService treningService)
            : base("Trening", "Pregled treninga i test unos individualnog/tradicionalnog i grupnog/online treninga.")
        {
            _treningService = treningService;

            ActionsFlow.Controls.Add(_btnUcitaj);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("ID korisnika:"));
            ActionsFlow.Controls.Add(_txtKorisnikId);
            ActionsFlow.Controls.Add(Theme.CreateFieldLabel("ID prostora:"));
            ActionsFlow.Controls.Add(_txtProstorId);
            ActionsFlow.Controls.Add(_btnDodajIndividualniTradicionalni);
            ActionsFlow.Controls.Add(_btnDodajGrupniOnline);
            PutSingleGrid(_grid);

            _btnUcitaj.Click += async (s, e) => await UcitajTreningeAsync();
            _btnDodajIndividualniTradicionalni.Click += async (s, e) => await DodajIndividualniTradicionalniAsync();
            _btnDodajGrupniOnline.Click += async (s, e) => await DodajGrupniOnlineAsync();
        }

        private async Task UcitajTreningeAsync()
        {
            _btnUcitaj.Enabled = false;
            SetStatus("Učitavanje...");
            var rezultat = await _treningService.GetAllAsync();
            if (!rezultat.IsSuccess) SetStatus($"GREŠKA: {rezultat.ErrorMessage}", false);
            else
            {
                _grid.DataSource = rezultat.Data;
                SetStatus($"Učitano {rezultat.Data?.Count ?? 0} treninga.", true);
            }
            _btnUcitaj.Enabled = true;
        }

        private async Task DodajIndividualniTradicionalniAsync()
        {
            if (!int.TryParse(_txtKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtProstorId.Text, out int prostorId))
            {
                SetStatus("Unesi validan ID korisnika i ID prostora u polja iznad.", false);
                return;
            }

            _btnDodajIndividualniTradicionalni.Enabled = false;

            var noviTrening = new TreningDTO
            {
                Naziv = $"Test individualni trening {DateTime.Now:HHmmss}",
                Tip = "individualni",
                DatumVreme = DateTime.Now.AddDays(1),
                Status = StatusTreninga.zakazan,
                JeIndividualni = true,
                KorisnikId = korisnikId,
                CiljeviTreninga = "Test cilj treninga",
                JeTradicionalni = true,
                ProstorId = prostorId
            };

            var rezultat = await _treningService.AddTreningAsync(noviTrening);
            SetStatus(rezultat.IsSuccess
                ? "Individualni + tradicionalni trening uspešno dodat."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajIndividualniTradicionalni.Enabled = true;
            if (rezultat.IsSuccess) await UcitajTreningeAsync();
        }

        private async Task DodajGrupniOnlineAsync()
        {
            _btnDodajGrupniOnline.Enabled = false;

            var noviTrening = new TreningDTO
            {
                Naziv = $"Test grupni online trening {DateTime.Now:HHmmss}",
                Tip = "grupni",
                DatumVreme = DateTime.Now.AddDays(1),
                Status = StatusTreninga.zakazan,
                JeGrupni = true,
                MaksUcesnika = 15,
                JeOnline = true,
                Platforma = "Zoom"
            };

            var rezultat = await _treningService.AddTreningAsync(noviTrening);
            SetStatus(rezultat.IsSuccess
                ? "Grupni + online trening uspešno dodat."
                : $"GREŠKA: {rezultat.ErrorMessage}", rezultat.IsSuccess);
            _btnDodajGrupniOnline.Enabled = true;
            if (rezultat.IsSuccess) await UcitajTreningeAsync();
        }
    }
}
