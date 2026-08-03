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

        public MainForm(
            IKorisnikService korisnikService,
            IOsobljeService osobljeService,
            IProstorService prostorService,
            IOpremaService opremaService)
        {
            _korisnikService = korisnikService;
            _osobljeService = osobljeService;
            _prostorService = prostorService;
            _opremaService = opremaService;

            Text = "Fitness Centar - test NHibernate mapiranja";
            Width = 1000;
            Height = 650;

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

            _tabs.TabPages.Add(tabKorisnici);
            _tabs.TabPages.Add(tabOsoblje);
            _tabs.TabPages.Add(tabProstor);
            _tabs.TabPages.Add(tabOprema);
            Controls.Add(_tabs);

            _btnUcitajKorisnike.Click += async (s, e) => await UcitajKorisnikeAsync();
            _btnDodajKorisnika.Click += async (s, e) => await DodajTestKorisnikaAsync();

            _btnUcitajOsoblje.Click += async (s, e) => await UcitajOsobljeAsync();
            _btnDodajOsoblje.Click += async (s, e) => await DodajTestOsobljeAsync();

            _btnUcitajProstor.Click += async (s, e) => await UcitajProstoreAsync();
            _btnDodajProstor.Click += async (s, e) => await DodajTestProstorAsync();

            _btnUcitajOpremu.Click += async (s, e) => await UcitajOpremuAsync();
            _btnDodajOpremu.Click += async (s, e) => await DodajTestOpremuAsync();
        }

        private async Task UcitajKorisnikeAsync()
        {
            _btnUcitajKorisnike.Enabled = false;
            _lblStatusKorisnici.Text = "Učitavanje...";

            var rezultat = await _korisnikService.GetAllAsync();

            if (!rezultat.IsSuccess)
            {
                _lblStatusKorisnici.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            }
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

            _lblStatusKorisnici.Text = rezultat.IsSuccess
                ? "Test korisnik uspešno dodat u bazu."
                : $"GREŠKA: {rezultat.ErrorMessage}";

            _btnDodajKorisnika.Enabled = true;

            if (rezultat.IsSuccess)
            {
                await UcitajKorisnikeAsync();
            }
        }

        private async Task UcitajOsobljeAsync()
        {
            _btnUcitajOsoblje.Enabled = false;
            _lblStatusOsoblje.Text = "Učitavanje...";

            var rezultat = await _osobljeService.GetAllAsync();

            if (!rezultat.IsSuccess)
            {
                _lblStatusOsoblje.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            }
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

            _lblStatusOsoblje.Text = rezultat.IsSuccess
                ? "Test osoblje (trener + personalni trener) uspešno dodato."
                : $"GREŠKA: {rezultat.ErrorMessage}";

            _btnDodajOsoblje.Enabled = true;

            if (rezultat.IsSuccess)
            {
                await UcitajOsobljeAsync();
            }
        }

        private async Task UcitajProstoreAsync()
        {
            _btnUcitajProstor.Enabled = false;
            _lblStatusProstor.Text = "Učitavanje...";

            var rezultat = await _prostorService.GetAllAsync();

            if (!rezultat.IsSuccess)
            {
                _lblStatusProstor.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            }
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

            _lblStatusProstor.Text = rezultat.IsSuccess
                ? "Test prostor uspešno dodat u bazu."
                : $"GREŠKA: {rezultat.ErrorMessage}";

            _btnDodajProstor.Enabled = true;

            if (rezultat.IsSuccess)
            {
                await UcitajProstoreAsync();
            }
        }

        private async Task UcitajOpremuAsync()
        {
            _btnUcitajOpremu.Enabled = false;
            _lblStatusOprema.Text = "Učitavanje...";

            var rezultat = await _opremaService.GetAllAsync();

            if (!rezultat.IsSuccess)
            {
                _lblStatusOprema.Text = $"GREŠKA: {rezultat.ErrorMessage}";
            }
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

            _lblStatusOprema.Text = rezultat.IsSuccess
                ? "Test oprema uspešno dodata u bazu."
                : $"GREŠKA: {rezultat.ErrorMessage}";

            _btnDodajOpremu.Enabled = true;

            if (rezultat.IsSuccess)
            {
                await UcitajOpremuAsync();
            }
        }
    }
}
