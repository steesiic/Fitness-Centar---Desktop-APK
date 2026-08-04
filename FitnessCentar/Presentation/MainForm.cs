using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;

namespace FitnessCentar.Presentation
{
    public class MainForm : Form
    {
        private readonly IKorisnikService _korisnikService;
        private readonly IOsobljeService _osobljeService;
        private readonly IProstorService _prostorService;
        private readonly IOpremaService _opremaService;
        private readonly IClanarinaService _clanarinaService;
        private readonly IFitnessProgramService _programService;
        private readonly ITreningService _treningService;
        private readonly IPlanTreningaService _planTreningaService;

        private readonly TabControl _tabs = new() { Dock = DockStyle.Fill };

        // --- Tab: Korisnici ---
        private readonly DataGridView _gridKorisnici = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajKorisnike = new() { Text = "Učitaj korisnike", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajKorisnika = new() { Text = "Dodaj test korisnika", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusKorisnici = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Osoblje ---
        private readonly DataGridView _gridOsoblje = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajOsoblje = new() { Text = "Učitaj osoblje", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajOsoblje = new() { Text = "Dodaj test osoblje (trener + personalni trener)", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusOsoblje = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Prostor ---
        private readonly DataGridView _gridProstor = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajProstor = new() { Text = "Učitaj prostore", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajProstor = new() { Text = "Dodaj test prostor", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusProstor = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Oprema ---
        private readonly DataGridView _gridOprema = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajOpremu = new() { Text = "Učitaj opremu", Dock = DockStyle.Top, Height = 40 };
        private readonly TextBox _txtProstorIdZaOpremu = new() { Dock = DockStyle.Top, Height = 30, PlaceholderText = "ID prostora kom oprema pripada (npr. 1)" };
        private readonly Button _btnDodajOpremu = new() { Text = "Dodaj test opremu (za uneti ID prostora)", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusOprema = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Članarine i programi ---
        private readonly DataGridView _gridClanarine = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, Height = 200 };
        private readonly DataGridView _gridProgrami = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajClanarinePrograme = new() { Text = "Učitaj članarine i programe", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajClanarinu = new() { Text = "Dodaj test članarinu", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajProgram = new() { Text = "Dodaj test program", Dock = DockStyle.Top, Height = 40 };
        private readonly TextBox _txtUpisKorisnikId = new() { Dock = DockStyle.Top, Height = 30, PlaceholderText = "ID korisnika za upis (npr. 1)" };
        private readonly TextBox _txtUpisClanarinaId = new() { Dock = DockStyle.Top, Height = 30, PlaceholderText = "ID članarine za upis (npr. 1)" };
        private readonly Button _btnUpisiKorisnika = new() { Text = "Upiši korisnika u članarinu (test složenog ključa)", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusClanarinePrograme = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Trening ---
        private readonly DataGridView _gridTrening = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajTrening = new() { Text = "Učitaj treninge", Dock = DockStyle.Top, Height = 40 };
        private readonly TextBox _txtKorisnikIdZaTrening = new() { Dock = DockStyle.Top, Height = 30, PlaceholderText = "ID korisnika (za individualni test, npr. 1)" };
        private readonly TextBox _txtProstorIdZaTrening = new() { Dock = DockStyle.Top, Height = 30, PlaceholderText = "ID prostora (za tradicionalni test, npr. 1)" };
        private readonly Button _btnDodajIndividualniTradicionalni = new() { Text = "Dodaj: Individualni + Tradicionalni", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodajGrupniOnline = new() { Text = "Dodaj: Grupni + Online", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatusTrening = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        // --- Tab: Plan treninga ---
        private readonly DataGridView _gridPlanTreninga = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitajPlanove = new() { Text = "Učitaj planove", Dock = DockStyle.Top, Height = 35 };
        private readonly TextBox _txtKorisnikIdZaPlan = new() { Dock = DockStyle.Top, Height = 28, PlaceholderText = "ID korisnika (npr. 1)" };
        private readonly TextBox _txtOsobljeIdZaPlan = new() { Dock = DockStyle.Top, Height = 28, PlaceholderText = "ID osoblja - trener (npr. 1)" };
        private readonly Button _btnDodajPlan = new() { Text = "1. Dodaj plan treninga", Dock = DockStyle.Top, Height = 35 };
        private readonly TextBox _txtPlanIdZaFazu = new() { Dock = DockStyle.Top, Height = 28, PlaceholderText = "ID plana (dobijen iz koraka 1)" };
        private readonly Button _btnDodajFazu = new() { Text = "2. Dodaj fazu (redosled 1)", Dock = DockStyle.Top, Height = 35 };
        private readonly TextBox _txtFazaIdZaNedelju = new() { Dock = DockStyle.Top, Height = 28, PlaceholderText = "ID faze (dobijen iz koraka 2)" };
        private readonly Button _btnDodajNedelju = new() { Text = "3. Dodaj nedelju (broj 1)", Dock = DockStyle.Top, Height = 35 };
        private readonly TextBox _txtNedeljaIdZaVezbu = new() { Dock = DockStyle.Top, Height = 28, PlaceholderText = "ID nedelje (dobijen iz koraka 3)" };
        private readonly Button _btnDodajVezbu = new() { Text = "4. Dodaj vežbu", Dock = DockStyle.Top, Height = 35 };
        private readonly Label _lblStatusPlanTreninga = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        public MainForm(
            IKorisnikService korisnikService,
            IOsobljeService osobljeService,
            IProstorService prostorService,
            IOpremaService opremaService,
            IClanarinaService clanarinaService,
            IFitnessProgramService programService,
            ITreningService treningService,
            IPlanTreningaService planTreningaService)
        {
            _korisnikService = korisnikService;
            _osobljeService = osobljeService;
            _prostorService = prostorService;
            _opremaService = opremaService;
            _clanarinaService = clanarinaService;
            _programService = programService;
            _treningService = treningService;
            _planTreningaService = planTreningaService;

            Text = "Fitness Centar - test NHibernate mapiranja";
            Width = 1050;
            Height = 750;

            var tabKorisnici = new TabPage("Korisnici");
            tabKorisnici.Controls.Add(_gridKorisnici);
            tabKorisnici.Controls.Add(_lblStatusKorisnici);
            tabKorisnici.Controls.Add(_btnDodajKorisnika);
            tabKorisnici.Controls.Add(_btnUcitajKorisnike);

            var tabOsoblje = new TabPage("Osoblje");
            tabOsoblje.Controls.Add(_gridOsoblje);
            tabOsoblje.Controls.Add(_lblStatusOsoblje);
            tabOsoblje.Controls.Add(_btnDodajOsoblje);
            tabOsoblje.Controls.Add(_btnUcitajOsoblje);

            var tabProstor = new TabPage("Prostor");
            tabProstor.Controls.Add(_gridProstor);
            tabProstor.Controls.Add(_lblStatusProstor);
            tabProstor.Controls.Add(_btnDodajProstor);
            tabProstor.Controls.Add(_btnUcitajProstor);

            var tabOprema = new TabPage("Oprema");
            tabOprema.Controls.Add(_gridOprema);
            tabOprema.Controls.Add(_lblStatusOprema);
            tabOprema.Controls.Add(_btnDodajOpremu);
            tabOprema.Controls.Add(_txtProstorIdZaOpremu);
            tabOprema.Controls.Add(_btnUcitajOpremu);

            var tabClanarinePrograme = new TabPage("Članarine i programi");
            tabClanarinePrograme.Controls.Add(_gridProgrami);
            tabClanarinePrograme.Controls.Add(_gridClanarine);
            tabClanarinePrograme.Controls.Add(_lblStatusClanarinePrograme);
            tabClanarinePrograme.Controls.Add(_btnUpisiKorisnika);
            tabClanarinePrograme.Controls.Add(_txtUpisClanarinaId);
            tabClanarinePrograme.Controls.Add(_txtUpisKorisnikId);
            tabClanarinePrograme.Controls.Add(_btnDodajProgram);
            tabClanarinePrograme.Controls.Add(_btnDodajClanarinu);
            tabClanarinePrograme.Controls.Add(_btnUcitajClanarinePrograme);

            var tabTrening = new TabPage("Trening");
            tabTrening.Controls.Add(_gridTrening);
            tabTrening.Controls.Add(_lblStatusTrening);
            tabTrening.Controls.Add(_btnDodajGrupniOnline);
            tabTrening.Controls.Add(_btnDodajIndividualniTradicionalni);
            tabTrening.Controls.Add(_txtProstorIdZaTrening);
            tabTrening.Controls.Add(_txtKorisnikIdZaTrening);
            tabTrening.Controls.Add(_btnUcitajTrening);

            var tabPlanTreninga = new TabPage("Plan treninga");
            tabPlanTreninga.Controls.Add(_gridPlanTreninga);
            tabPlanTreninga.Controls.Add(_lblStatusPlanTreninga);
            tabPlanTreninga.Controls.Add(_btnDodajVezbu);
            tabPlanTreninga.Controls.Add(_txtNedeljaIdZaVezbu);
            tabPlanTreninga.Controls.Add(_btnDodajNedelju);
            tabPlanTreninga.Controls.Add(_txtFazaIdZaNedelju);
            tabPlanTreninga.Controls.Add(_btnDodajFazu);
            tabPlanTreninga.Controls.Add(_txtPlanIdZaFazu);
            tabPlanTreninga.Controls.Add(_btnDodajPlan);
            tabPlanTreninga.Controls.Add(_txtOsobljeIdZaPlan);
            tabPlanTreninga.Controls.Add(_txtKorisnikIdZaPlan);
            tabPlanTreninga.Controls.Add(_btnUcitajPlanove);

            _tabs.TabPages.Add(tabKorisnici);
            _tabs.TabPages.Add(tabOsoblje);
            _tabs.TabPages.Add(tabProstor);
            _tabs.TabPages.Add(tabOprema);
            _tabs.TabPages.Add(tabClanarinePrograme);
            _tabs.TabPages.Add(tabTrening);
            _tabs.TabPages.Add(tabPlanTreninga);
            Controls.Add(_tabs);

            _btnUcitajKorisnike.Click += async (s, e) => await UcitajKorisnikeAsync();
            _btnDodajKorisnika.Click += async (s, e) => await DodajTestKorisnikaAsync();

            _btnUcitajOsoblje.Click += async (s, e) => await UcitajOsobljeAsync();
            _btnDodajOsoblje.Click += async (s, e) => await DodajTestOsobljeAsync();

            _btnUcitajProstor.Click += async (s, e) => await UcitajProstoreAsync();
            _btnDodajProstor.Click += async (s, e) => await DodajTestProstorAsync();

            _btnUcitajOpremu.Click += async (s, e) => await UcitajOpremuAsync();
            _btnDodajOpremu.Click += async (s, e) => await DodajTestOpremuAsync();

            _btnUcitajClanarinePrograme.Click += async (s, e) => await UcitajClanarinePrograme();
            _btnDodajClanarinu.Click += async (s, e) => await DodajTestClanarinuAsync();
            _btnDodajProgram.Click += async (s, e) => await DodajTestProgramAsync();
            _btnUpisiKorisnika.Click += async (s, e) => await UpisiKorisnikaAsync();

            _btnUcitajTrening.Click += async (s, e) => await UcitajTreningeAsync();
            _btnDodajIndividualniTradicionalni.Click += async (s, e) => await DodajIndividualniTradicionalniAsync();
            _btnDodajGrupniOnline.Click += async (s, e) => await DodajGrupniOnlineAsync();

            _btnUcitajPlanove.Click += async (s, e) => await UcitajPlanoveAsync();
            _btnDodajPlan.Click += async (s, e) => await DodajPlanAsync();
            _btnDodajFazu.Click += async (s, e) => await DodajFazuAsync();
            _btnDodajNedelju.Click += async (s, e) => await DodajNedeljuAsync();
            _btnDodajVezbu.Click += async (s, e) => await DodajVezbuAsync();
        }

        private async Task UcitajKorisnikeAsync()
        {
            _btnUcitajKorisnike.Enabled = false;
            _lblStatusKorisnici.Text = "Učitavanje...";
            var rezultat = await _korisnikService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusKorisnici.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridKorisnici.DataSource = rezultat.Data;
                _lblStatusKorisnici.Text = $"Učitano {rezultat.Data?.Count ?? 0} korisnika.";
            }
            _btnUcitajKorisnike.Enabled = true;
        }

        private async Task DodajTestKorisnikaAsync()
        {
            _btnDodajKorisnika.Enabled = false;
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
            _lblStatusKorisnici.Text = rezultat.IsSuccess ? "Test korisnik uspešno dodat u bazu." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajKorisnika.Enabled = true;
            if (rezultat.IsSuccess) await UcitajKorisnikeAsync();
        }

        private async Task UcitajOsobljeAsync()
        {
            _btnUcitajOsoblje.Enabled = false;
            _lblStatusOsoblje.Text = "Učitavanje...";
            var rezultat = await _osobljeService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusOsoblje.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridOsoblje.DataSource = rezultat.Data;
                _lblStatusOsoblje.Text = $"Učitano {rezultat.Data?.Count ?? 0} zaposlenih.";
            }
            _btnUcitajOsoblje.Enabled = true;
        }

        private async Task DodajTestOsobljeAsync()
        {
            _btnDodajOsoblje.Enabled = false;
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
            _lblStatusOsoblje.Text = rezultat.IsSuccess ? "Test osoblje uspešno dodato." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajOsoblje.Enabled = true;
            if (rezultat.IsSuccess) await UcitajOsobljeAsync();
        }

        private async Task UcitajProstoreAsync()
        {
            _btnUcitajProstor.Enabled = false;
            _lblStatusProstor.Text = "Učitavanje...";
            var rezultat = await _prostorService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusProstor.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridProstor.DataSource = rezultat.Data;
                _lblStatusProstor.Text = $"Učitano {rezultat.Data?.Count ?? 0} prostora.";
            }
            _btnUcitajProstor.Enabled = true;
        }

        private async Task DodajTestProstorAsync()
        {
            _btnDodajProstor.Enabled = false;
            var noviProstor = new ProstorDTO
            {
                Naziv = $"Test prostor {DateTime.Now:HHmmss}",
                Tip = "teretana",
                Kapacitet = 20,
                Lokacija = "Test lokacija",
                RadnoVreme = "08-20h"
            };
            var rezultat = await _prostorService.AddProstorAsync(noviProstor);
            _lblStatusProstor.Text = rezultat.IsSuccess ? "Test prostor uspešno dodat u bazu." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajProstor.Enabled = true;
            if (rezultat.IsSuccess) await UcitajProstoreAsync();
        }

        private async Task UcitajOpremuAsync()
        {
            _btnUcitajOpremu.Enabled = false;
            _lblStatusOprema.Text = "Učitavanje...";
            var rezultat = await _opremaService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusOprema.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridOprema.DataSource = rezultat.Data;
                _lblStatusOprema.Text = $"Učitano {rezultat.Data?.Count ?? 0} komada opreme.";
            }
            _btnUcitajOpremu.Enabled = true;
        }

        private async Task DodajTestOpremuAsync()
        {
            if (!int.TryParse(_txtProstorIdZaOpremu.Text, out int prostorId))
            {
                _lblStatusOprema.Text = "Prvo unesi validan ID prostora (npr. 1) u polje iznad.";
                return;
            }
            _btnDodajOpremu.Enabled = false;
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
            _lblStatusOprema.Text = rezultat.IsSuccess ? "Test oprema uspešno dodata u bazu." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajOpremu.Enabled = true;
            if (rezultat.IsSuccess) await UcitajOpremuAsync();
        }

        private async Task UcitajClanarinePrograme()
        {
            _btnUcitajClanarinePrograme.Enabled = false;
            _lblStatusClanarinePrograme.Text = "Učitavanje...";
            var clanarine = await _clanarinaService.GetAllAsync();
            var programi = await _programService.GetAllAsync();
            if (!clanarine.IsSuccess) _lblStatusClanarinePrograme.Text = $"GREŠKA (članarine): {clanarine.ErrorMessage}";
            else if (!programi.IsSuccess) _lblStatusClanarinePrograme.Text = $"GREŠKA (programi): {programi.ErrorMessage}";
            else
            {
                _gridClanarine.DataSource = clanarine.Data;
                _gridProgrami.DataSource = programi.Data;
                _lblStatusClanarinePrograme.Text = $"Učitano {clanarine.Data?.Count ?? 0} članarina i {programi.Data?.Count ?? 0} programa.";
            }
            _btnUcitajClanarinePrograme.Enabled = true;
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
            _lblStatusClanarinePrograme.Text = rezultat.IsSuccess ? "Test članarina uspešno dodata." : $"GREŠKA: {rezultat.ErrorMessage}";
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
            _lblStatusClanarinePrograme.Text = rezultat.IsSuccess ? "Test program uspešno dodat." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajProgram.Enabled = true;
            if (rezultat.IsSuccess) await UcitajClanarinePrograme();
        }

        private async Task UpisiKorisnikaAsync()
        {
            if (!int.TryParse(_txtUpisKorisnikId.Text, out int korisnikId) ||
                !int.TryParse(_txtUpisClanarinaId.Text, out int clanarinaId))
            {
                _lblStatusClanarinePrograme.Text = "Unesi validne ID-jeve korisnika i članarine u polja iznad.";
                return;
            }
            _btnUpisiKorisnika.Enabled = false;
            var rezultat = await _clanarinaService.UpisiKorisnikaAsync(korisnikId, clanarinaId, DateTime.Now, DateTime.Now.AddDays(30));
            _lblStatusClanarinePrograme.Text = rezultat.IsSuccess ? "Korisnik uspešno upisan u članarinu." : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnUpisiKorisnika.Enabled = true;
        }

        private async Task UcitajTreningeAsync()
        {
            _btnUcitajTrening.Enabled = false;
            _lblStatusTrening.Text = "Učitavanje...";
            var rezultat = await _treningService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusTrening.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridTrening.DataSource = rezultat.Data;
                _lblStatusTrening.Text = $"Učitano {rezultat.Data?.Count ?? 0} treninga.";
            }
            _btnUcitajTrening.Enabled = true;
        }

        private async Task DodajIndividualniTradicionalniAsync()
        {
            if (!int.TryParse(_txtKorisnikIdZaTrening.Text, out int korisnikId) ||
                !int.TryParse(_txtProstorIdZaTrening.Text, out int prostorId))
            {
                _lblStatusTrening.Text = "Unesi validan ID korisnika i ID prostora u polja iznad.";
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
            _lblStatusTrening.Text = rezultat.IsSuccess
                ? "Individualni + tradicionalni trening uspešno dodat."
                : $"GREŠKA: {rezultat.ErrorMessage}";
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
            _lblStatusTrening.Text = rezultat.IsSuccess
                ? "Grupni + online trening uspešno dodat."
                : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajGrupniOnline.Enabled = true;
            if (rezultat.IsSuccess) await UcitajTreningeAsync();
        }

        private async Task UcitajPlanoveAsync()
        {
            _btnUcitajPlanove.Enabled = false;
            _lblStatusPlanTreninga.Text = "Učitavanje...";
            var rezultat = await _planTreningaService.GetAllAsync();
            if (!rezultat.IsSuccess) _lblStatusPlanTreninga.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            else
            {
                _gridPlanTreninga.DataSource = rezultat.Data;
                _lblStatusPlanTreninga.Text = $"Učitano {rezultat.Data?.Count ?? 0} planova.";
            }
            _btnUcitajPlanove.Enabled = true;
        }

        private async Task DodajPlanAsync()
        {
            if (!int.TryParse(_txtKorisnikIdZaPlan.Text, out int korisnikId) ||
                !int.TryParse(_txtOsobljeIdZaPlan.Text, out int osobljeId))
            {
                _lblStatusPlanTreninga.Text = "Unesi validan ID korisnika i ID osoblja u polja iznad (korak 1).";
                return;
            }

            _btnDodajPlan.Enabled = false;
            var rezultat = await _planTreningaService.AddPlanAsync(korisnikId, osobljeId, $"Test plan {DateTime.Now:HHmmss}");
            _lblStatusPlanTreninga.Text = rezultat.IsSuccess
                ? "Plan dodat. Klikni 'Učitaj planove' da vidiš njegov PlanId, pa ga unesi u sledeći korak."
                : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajPlan.Enabled = true;
            if (rezultat.IsSuccess) await UcitajPlanoveAsync();
        }

        private async Task DodajFazuAsync()
        {
            if (!int.TryParse(_txtPlanIdZaFazu.Text, out int planId))
            {
                _lblStatusPlanTreninga.Text = "Unesi validan ID plana u polje (korak 2).";
                return;
            }

            _btnDodajFazu.Enabled = false;
            var rezultat = await _planTreningaService.AddFazuAsync(planId, "Faza 1 - Adaptacija", 1, "Test faza");
            _lblStatusPlanTreninga.Text = rezultat.IsSuccess
                ? "Faza dodata. Proveri njen FazaId u bazi (SELECT FAZA_ID FROM FAZA_PLANA WHERE PLAN_ID = ...), pa ga unesi u korak 3."
                : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajFazu.Enabled = true;
        }

        private async Task DodajNedeljuAsync()
        {
            if (!int.TryParse(_txtFazaIdZaNedelju.Text, out int fazaId))
            {
                _lblStatusPlanTreninga.Text = "Unesi validan ID faze u polje (korak 3).";
                return;
            }

            _btnDodajNedelju.Enabled = false;
            var rezultat = await _planTreningaService.AddNedeljuAsync(fazaId, 1, "Test nedelja");
            _lblStatusPlanTreninga.Text = rezultat.IsSuccess
                ? "Nedelja dodata. Proveri njen NedeljaId u bazi, pa ga unesi u korak 4."
                : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajNedelju.Enabled = true;
        }

        private async Task DodajVezbuAsync()
        {
            if (!int.TryParse(_txtNedeljaIdZaVezbu.Text, out int nedeljaId))
            {
                _lblStatusPlanTreninga.Text = "Unesi validan ID nedelje u polje (korak 4).";
                return;
            }

            _btnDodajVezbu.Enabled = false;
            var rezultat = await _planTreningaService.AddVezbuAsync(nedeljaId, "Čučnjevi", "snaga", 3, 12);
            _lblStatusPlanTreninga.Text = rezultat.IsSuccess
                ? "Vežba uspešno dodata - ceo lanac Plan -> Faza -> Nedelja -> Vežba radi!"
                : $"GREŠKA: {rezultat.ErrorMessage}";
            _btnDodajVezbu.Enabled = true;
        }
    }
}
