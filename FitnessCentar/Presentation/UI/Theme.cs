using System.Drawing.Drawing2D;

namespace FitnessCentar.Presentation.UI

    internal static class Theme
    {
        public static readonly Color SidebarBg = ColorTranslator.FromHtml("#D7E3EF");
        public static readonly Color SidebarBgDarker = ColorTranslator.FromHtml("#35507D");
        public static readonly Color SidebarText = ColorTranslator.FromHtml("#2C3E50");
        public static readonly Color Accent = ColorTranslator.FromHtml("#6495ED");
        public static readonly Color AccentDark = ColorTranslator.FromHtml("#4A75C4");
        public static readonly Color ContentBg = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color CardBg = Color.White;
        public static readonly Color TextDark = ColorTranslator.FromHtml("#1F2937");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#6B7280");
        public static readonly Color Success = ColorTranslator.FromHtml("#15803D");
        public static readonly Color Error = ColorTranslator.FromHtml("#DC2626");
        public static readonly Color Border = ColorTranslator.FromHtml("#D7E3EF");
        public static readonly Color GridAltRow = ColorTranslator.FromHtml("#EAF1FB");
        public static readonly Color SidebarHover = ColorTranslator.FromHtml("#C3D4E8");


        public static readonly Font FontBase = new("Segoe UI", 9.5f);
        public static readonly Font FontAppTitle = new("Segoe UI", 15f, FontStyle.Bold);
        public static readonly Font FontAppSubtitle = new("Segoe UI", 8.5f);
        public static readonly Font FontSectionTitle = new("Segoe UI", 14f, FontStyle.Bold);
        public static readonly Font FontSectionSubtitle = new("Segoe UI", 9f);
        public static readonly Font FontNav = new("Segoe UI", 10.5f);
        public static readonly Font FontNavActive = new("Segoe UI", 10.5f, FontStyle.Bold);
        public static readonly Font FontButton = new("Segoe UI", 9.5f, FontStyle.Bold);
        public static readonly Font FontLabel = new("Segoe UI", 8.75f);
        public static readonly Font FontGridHeader = new("Segoe UI", 9.5f, FontStyle.Bold);
        public static readonly Font FontCardCaption = new("Segoe UI", 10f, FontStyle.Bold);

        public static Button CreateActionButton(string text, Color? backColor = null)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(0, 34),
                Padding = new Padding(14, 4, 14, 4),
                Margin = new Padding(0, 0, 8, 8),
                Font = FontButton,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor ?? Accent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(btn.BackColor, 0.08f);
            return btn;
        }

        public static Button CreateNavButton(string text)
        {
            var btn = new Button
            {
                Text = "   " + text,
                Dock = DockStyle.Top,
                Height = 46,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = FontNav,
                FlatStyle = FlatStyle.Flat,
                BackColor = SidebarBg,
                ForeColor = SidebarText,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false,
                Margin = new Padding(0),
                Tag = false 
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => { if (btn.Tag is false) btn.BackColor = SidebarHover; };
            btn.MouseLeave += (s, e) => { if (btn.Tag is false) btn.BackColor = SidebarBg; };
            return btn;
        }

        public static void SetNavButtonState(Button btn, bool active)
        {
            btn.Tag = active;
            btn.BackColor = active ? Accent : SidebarBg;
            btn.ForeColor = active ? Color.White : SidebarText;
            btn.Font = active ? FontNavActive : FontNav;
        }

        public static TextBox CreateInput(string placeholder, int width = 220)
        {
            return new TextBox
            {
                PlaceholderText = placeholder,
                Width = width,
                Height = 26,
                Font = FontBase,
                Margin = new Padding(0, 0, 8, 8)
            };
        }

        public static Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = FontLabel,
                ForeColor = TextMuted,
                Margin = new Padding(0, 6, 8, 2)
            };
        }

        public static Label CreateStatusLabel()
        {
            return new Label
            {
                Text = string.Empty,
                Dock = DockStyle.Bottom,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = FontBase,
                ForeColor = TextMuted,
                Padding = new Padding(4, 0, 0, 0)
            };
        }

        public static void SetStatus(Label label, string text, bool? success = null)
        {
            label.Text = text;
            label.ForeColor = success switch
            {
                true => Success,
                false => Error,
                null => TextMuted
            };
        }

        public static GroupBox CreateGroupBox(string text)
        {
            return new GroupBox
            {
                Text = text,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Font = FontCardCaption,
                ForeColor = AccentDark,
                Padding = new Padding(10, 8, 10, 4),
                Margin = new Padding(0, 0, 0, 12)
            };
        }

        public static Label CreateCardCaption(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 26,
                Font = FontCardCaption,
                ForeColor = TextDark,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
        }

        public static Panel CreateCard()
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = CardBg,
                Padding = new Padding(1)
            };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Border);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };
            return card;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = CardBg;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 34;
            grid.RowTemplate.Height = 26;
            grid.Font = FontBase;

            grid.ColumnHeadersDefaultCellStyle.BackColor = AccentDark;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = FontGridHeader;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            grid.DefaultCellStyle.BackColor = CardBg;
            grid.DefaultCellStyle.SelectionBackColor = Accent;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            grid.AlternatingRowsDefaultCellStyle.BackColor = GridAltRow;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Accent;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }


        public static Icon BuildAppIcon()
        {
            using var bmp = new Bitmap(64, 64);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var bg = new LinearGradientBrush(new Rectangle(0, 0, 64, 64), Accent, AccentDark, 45f);
                g.FillEllipse(bg, 1, 1, 61, 61);
                using var pen = new Pen(SidebarBgDarker, 1.5f);
                g.DrawEllipse(pen, 1, 1, 61, 61);
                using var font = new Font("Segoe UI", 20f, FontStyle.Bold);
                using var textBrush = new SolidBrush(Color.White);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("FC", font, textBrush, new RectangleF(0, 1, 64, 64), format);
            }
            return Icon.FromHandle(bmp.GetHicon());
        }
    }
}
