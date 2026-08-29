using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Presentation.Sections;
using FitnessCentar.Presentation.UI;

namespace FitnessCentar.Presentation
{

    public class MainForm : Form
    {
        private readonly List<(string Title, Func<Control> Factory)> _sections;
        private readonly Dictionary<string, Control> _sectionCache = new();
        private readonly Dictionary<string, Button> _navButtons = new();

        private readonly Panel _contentHost = new() { Dock = DockStyle.Fill, BackColor = Theme.ContentBg };
        private readonly Panel _sidebar = new() { Dock = DockStyle.Fill, BackColor = Theme.SidebarBg, AutoScroll = true };

        private Button? _activeNavButton;

        public MainForm(
            IKorisnikService korisnikService,
            IOsobljeService osobljeService,
            IProstorService prostorService,
            IOpremaService opremaService,
            IClanarinaService clanarinaService,
            IFitnessProgramService programService,
            ITreningService treningService,
            IPlanTreningaService planTreningaService,
            IPlanIshraneService planIshraneService,
            IMerenjeEvaluacijaService merenjeEvaluacijaService)
        {
            _sections = new List<(string, Func<Control>)>
            {
                ("Korisnici", () => new KorisniciSection(korisnikService)),
                ("Osoblje", () => new OsobljeSection(osobljeService)),
                ("Prostor", () => new ProstorSection(prostorService)),
                ("Oprema", () => new OpremaSection(opremaService)),
                ("Članarine i programi", () => new ClanarineIProgramiSection(clanarinaService, programService)),
                ("Trening", () => new TreningSection(treningService)),
                ("Plan treninga", () => new PlanTreningaSection(planTreningaService)),
                ("Plan ishrane", () => new PlanIshraneSection(planIshraneService)),
                ("Merenje i evaluacija", () => new MerenjeEvaluacijaSection(merenjeEvaluacijaService)),
            };

            InitializeLayout();
            ShowSection(_sections[0].Title);
        }

        private void InitializeLayout()
        {
            Text = "Fitness Centar — Sistem za upravljanje bazom podataka";
            Font = Theme.FontBase;
            MinimumSize = new Size(1150, 760);
            Width = 1280;
            Height = 820;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Theme.ContentBg;
            Icon = Theme.BuildAppIcon();

            var header = BuildHeader();
            var body = BuildBody();

            Controls.Add(body);
            Controls.Add(header);
        }

        private static Panel BuildHeader()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 68, BackColor = Theme.SidebarBgDarker };

            var logo = new PictureBox
            {
                Image = Theme.BuildAppIcon().ToBitmap(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(40, 40),
                Location = new Point(20, 14)
            };

            var titleLbl = new Label
            {
                Text = "FITNESS CENTAR",
                Font = Theme.FontAppTitle,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(72, 12)
            };

            var subtitleLbl = new Label
            {
                Text = "Sistem za upravljanje bazom podataka",
                Font = Theme.FontButton,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(73, 38)
            };

            header.Controls.Add(logo);
            header.Controls.Add(titleLbl);
            header.Controls.Add(subtitleLbl);
            return header;
        }

        private TableLayoutPanel BuildBody()
        {
            var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            foreach (var (title, _) in _sections)
            {
                var navBtn = Theme.CreateNavButton(title);
                navBtn.Click += (s, e) => ShowSection(title);
                _navButtons[title] = navBtn;
                _sidebar.Controls.Add(navBtn);
            }

            body.Controls.Add(_sidebar, 0, 0);
            body.Controls.Add(_contentHost, 1, 0);
            return body;
        }

        private void ShowSection(string title)
        {
            if (!_sectionCache.TryGetValue(title, out var control))
            {
                control = _sections.First(s => s.Title == title).Factory();
                _sectionCache[title] = control;
            }

            _contentHost.Controls.Clear();
            _contentHost.Controls.Add(control);

            SetActiveNavButton(_navButtons[title]);
        }

        private void InitializeComponent()
        {

        }

        private void SetActiveNavButton(Button button)
        {
            if (_activeNavButton != null) Theme.SetNavButtonState(_activeNavButton, active: false);
            Theme.SetNavButtonState(button, active: true);
            _activeNavButton = button;
        }
    }
}
