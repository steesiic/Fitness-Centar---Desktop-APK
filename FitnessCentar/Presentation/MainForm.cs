using FitnessCentar.App.DTOs;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Entities;

namespace FitnessCentar.Presentation
{
    // Minimalna forma čiji je jedini cilj da dokaže da mapiranja rade end-to-end
    // (INSERT test korisnika + SELECT svih korisnika iz Oracle baze preko NHibernate-a).
    public class MainForm : Form
    {
        private readonly IKorisnikService _korisnikService;
        private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true };
        private readonly Button _btnUcitaj = new() { Text = "Učitaj korisnike", Dock = DockStyle.Top, Height = 40 };
        private readonly Button _btnDodaj = new() { Text = "Dodaj test korisnika", Dock = DockStyle.Top, Height = 40 };
        private readonly Label _lblStatus = new() { Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleLeft };

        public MainForm(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;

            Text = "Fitness Centar - test NHibernate mapiranja (Klaster 1: Korisnik)";
            Width = 900;
            Height = 600;

            Controls.Add(_grid);
            Controls.Add(_lblStatus);
            Controls.Add(_btnDodaj);
            Controls.Add(_btnUcitaj);

            _btnUcitaj.Click += (s, e) => UcitajKorisnike();
            _btnDodaj.Click += (s, e) => DodajTestKorisnika();
        }

        private void UcitajKorisnike()
        {
            var rezultat = _korisnikService.GetAll();

            if (!rezultat.IsSuccess)
            {
                _lblStatus.Text = $"GREŠKA: {rezultat.ErrorMessage}";
                return;
            }

            _grid.DataSource = rezultat.Data;
            _lblStatus.Text = $"Učitano {rezultat.Data?.Count ?? 0} korisnika.";
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MainForm
            // 
            ClientSize = new Size(873, 458);
            Name = "MainForm";
            ResumeLayout(false);
        }

        private void DodajTestKorisnika()
        {
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

            var rezultat = _korisnikService.AddKorisnik(noviKorisnik);

            _lblStatus.Text = rezultat.IsSuccess
                ? "Test korisnik uspešno dodat u bazu."
                : $"GREŠKA: {rezultat.ErrorMessage}";

            if (rezultat.IsSuccess)
            {
                UcitajKorisnike();
            }
        }
    }
}
