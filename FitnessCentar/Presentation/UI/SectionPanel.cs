namespace FitnessCentar.Presentation.UI
{

    internal abstract class SectionPanel : UserControl
    {
        protected readonly FlowLayoutPanel ActionsFlow;
        protected readonly Panel ContentHost;
        protected readonly Label StatusLabel;

        protected SectionPanel(string title)
        {
            Dock = DockStyle.Fill;
            BackColor = Theme.ContentBg;
            Font = Theme.FontBase;
            Padding = new Padding(20, 16, 20, 8);

            var titleWrap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };
            titleWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            titleWrap.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            titleWrap.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var titleLbl = new Label
            {
                Text = title,
                AutoSize = true,
                Font = Theme.FontSectionTitle,
                ForeColor = Theme.TextDark,
                Margin = new Padding(0)
            };
            titleWrap.Controls.Add(titleLbl, 0, 0);
            var actionsBox = Theme.CreateGroupBox("Akcije");
            ActionsFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(4, 6, 4, 0),
                Location = new Point(0, 0)
            };
            actionsBox.Controls.Add(ActionsFlow);

            void OgraniciSirinu()
            {
                if (actionsBox.ClientSize.Width > 0)
                {
                    ActionsFlow.MaximumSize = new Size(actionsBox.ClientSize.Width, 0);
                }
                actionsBox.PerformLayout();
            }
            actionsBox.SizeChanged += (s, e) => OgraniciSirinu();
            ActionsFlow.SizeChanged += (s, e) => actionsBox.PerformLayout();

            ContentHost = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0) };

            StatusLabel = Theme.CreateStatusLabel();
            StatusLabel.Dock = DockStyle.Fill;

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            root.Controls.Add(titleWrap, 0, 0);
            root.Controls.Add(actionsBox, 0, 1);
            root.Controls.Add(ContentHost, 0, 2);
            root.Controls.Add(StatusLabel, 0, 3);

            Controls.Add(root);

            Load += (s, e) => OgraniciSirinu();
        }

        protected void SetStatus(string text, bool? success = null) => Theme.SetStatus(StatusLabel, text, success);


        protected void PutSingleGrid(DataGridView grid)
        {
            Theme.StyleGrid(grid);
            var card = Theme.CreateCard();
            grid.Dock = DockStyle.Fill;
            card.Controls.Add(grid);
            ContentHost.Controls.Add(card);
        }

        protected void PutTwoGrids(string caption1, DataGridView grid1, string caption2, DataGridView grid2, float firstRowPercent = 38f)
        {
            Theme.StyleGrid(grid1);
            Theme.StyleGrid(grid2);

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, firstRowPercent));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f - firstRowPercent));

            var card1 = BuildCaptionedCard(caption1, grid1);
            card1.Margin = new Padding(0, 0, 0, 10);
            var card2 = BuildCaptionedCard(caption2, grid2);

            layout.Controls.Add(card1, 0, 0);
            layout.Controls.Add(card2, 0, 1);
            ContentHost.Controls.Add(layout);
        }

        private static Panel BuildCaptionedCard(string caption, DataGridView grid)
        {
            var card = Theme.CreateCard();
            var inner = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            inner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            inner.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var captionLbl = Theme.CreateCardCaption(caption);
            captionLbl.Dock = DockStyle.Fill;
            grid.Dock = DockStyle.Fill;

            inner.Controls.Add(captionLbl, 0, 0);
            inner.Controls.Add(grid, 0, 1);
            card.Controls.Add(inner);
            return card;
        }
    }
}