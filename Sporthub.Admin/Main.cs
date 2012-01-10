using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Model;

namespace Sporthub.Admin
{
    public partial class Main : Form
    {
        private readonly ResortRepository _resortRepository = new ResortRepository(true);
        private ResortService _resortService;
        private readonly CountryRepository _countryRepository = new CountryRepository(true);
        private CountryService _countryService;
        private readonly ResortLinkRepository _resortLinkRepository = new ResortLinkRepository(true);
        private ResortLinkService _resortLinkService;
        private Resort _resort;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _resortService = new ResortService(_resortRepository);
            _countryService = new CountryService(_countryRepository);

            var countries = _countryService.GetAllWithResorts();
            foreach (var country in countries)
                coCountries.Items.Add(country.CountryName);
            int index = coCountries.FindString("France");
            coCountries.SelectedIndex = index;

            PopResortsCombo(coCountries.SelectedItem.ToString());

            lblStatus.Text = string.Empty;
        }

        private void PopResortsCombo(string name)
        {
            coResorts.Items.Clear();
            var resorts = _resortService.GetAllByCountryIdBasic(name);
            foreach (var resort in resorts)
                coResorts.Items.Add(resort.Name);

        }

        private void coResorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResort();
        }

        private void GetResort()
        {
            dgvLinks.Rows.Clear();
            NewLinkName.Text = string.Empty;
            NewUrl.Text = string.Empty;
            lblStatus.Text = string.Empty;

            _resortService = new ResortService(_resortRepository);
            _resort = _resortService.GetByName(coResorts.SelectedItem.ToString());
            ResortName.Text = _resort.Name;
            PrettyUrl.Text = _resort.PrettyUrl;
            Longitude.Text = _resort.Longitude.ToString();
            Latitude.Text = _resort.Latitude.ToString();
            if (!_resort.IsSkiArea)
            {
                BaseLevel.Text = _resort.ResortStats.BaseLevel;
                TopLevel.Text = _resort.ResortStats.TopLevel;
                VerticalDrop.Text = _resort.ResortStats.VerticalDrop;
                Height.Text = _resort.ResortStats.Height;
                AverageSnowfall.Text = _resort.ResortStats.AverageSnowfall;
                HasSnowmaking.Checked = (_resort.ResortStats.HasSnowmaking == "True") ? true : false;
                SnowmakingCoverage.Text = _resort.ResortStats.SnowmakingCoverage;
                PreSeasonStartMonth.Text = _resort.ResortStats.PreSeasonStartMonth;
                SeasonStartMonth.Text = _resort.ResortStats.SeasonStartMonth;
                SeasonEndMonth.Text = _resort.ResortStats.SeasonEndMonth;
                Population.Text = _resort.ResortStats.Population;
                MountainRestaurants.Text = _resort.ResortStats.MountainRestaurants;

                HasNightskiing.Checked = (_resort.ResortStats.HasNightskiing == "True") ? true : false;
                NightskiingDescription.Text = _resort.ResortStats.NightskiingDescription;
                HasSummerskiing.Checked = (_resort.ResortStats.HasSummerskiing == "True") ? true : false;
                SummerskiingDescription.Text = _resort.ResortStats.SummerskiingDescription;
                SummerStartMonth.Text = _resort.ResortStats.SummerStartMonth;
                SummerEndMonth.Text = _resort.ResortStats.SummerEndMonth;

                BlackRuns.Text = _resort.ResortStats.BlackRuns;
                RedRuns.Text = _resort.ResortStats.RedRuns;
                BlueRuns.Text = _resort.ResortStats.BlueRuns;
                GreenRuns.Text = _resort.ResortStats.GreenRuns;
                LongestRunDistance.Text = _resort.ResortStats.LongestRunDistance;
                RunTotalDistance.Text = _resort.ResortStats.RunTotalDistance;
                RunTotal.Text = _resort.ResortStats.RunTotal;
                SkiableTerrianSize.Text = _resort.ResortStats.SkiableTerrianSize;
                HasSnowpark.Checked = (_resort.ResortStats.HasSnowpark == "True") ? true : false;
                SnowparkTotal.Text = _resort.ResortStats.SnowparkTotal;
                SnowparkDescription.Text = _resort.ResortStats.SnowparkDescription;
                HasHalfpipe.Checked = (_resort.ResortStats.HasHalfpipe == "True") ? true : false;
                HalfpipeTotal.Text = _resort.ResortStats.HalfpipeTotal;
                HalfpipeDescription.Text = _resort.ResortStats.HalfpipeDescription;
                HasQuarterpipe.Checked = (_resort.ResortStats.HasQuarterpipe == "True") ? true : false;
                QuarterpipeTotal.Text = _resort.ResortStats.QuarterpipeTotal;
                QuarterpipeDescription.Text = _resort.ResortStats.QuarterpipeDescription;

                AverageSnowfall.Text = _resort.ResortStats.AverageSnowfall;
                SnowmakingCoverage.Text = _resort.ResortStats.SnowmakingCoverage;
                Snowfall1Jan.Text = _resort.ResortStats.Snowfall1Jan;
                Snowfall2Feb.Text = _resort.ResortStats.Snowfall2Feb;
                Snowfall3Mar.Text = _resort.ResortStats.Snowfall3Mar;
                Snowfall4Apr.Text = _resort.ResortStats.Snowfall4Apr;
                Snowfall5May.Text = _resort.ResortStats.Snowfall5May;
                Snowfall6Jun.Text = _resort.ResortStats.Snowfall6Jun;
                Snowfall7Jul.Text = _resort.ResortStats.Snowfall7Jul;
                Snowfall8Aug.Text = _resort.ResortStats.Snowfall8Aug;
                Snowfall9Sep.Text = _resort.ResortStats.Snowfall9Sep;
                Snowfall10Oct.Text = _resort.ResortStats.Snowfall10Oct;
                Snowfall11Nov.Text = _resort.ResortStats.Snowfall11Nov;
                Snowfall12Dec.Text = _resort.ResortStats.Snowfall12Dec;

                LiftDescription.Text = _resort.ResortStats.LiftDescription;
                LiftTotal.Text = _resort.ResortStats.LiftTotal;
                LiftCapacityHour.Text = _resort.ResortStats.LiftTotal;
                QuadPlusCount.Text = _resort.ResortStats.QuadPlusCount;
                QuadCount.Text = _resort.ResortStats.QuadCount;
                TripleCount.Text = _resort.ResortStats.TripleCount;
                DoubleCount.Text = _resort.ResortStats.DoubleCount;
                SurfaceCount.Text = _resort.ResortStats.SurfaceCount;
                GondolaCount.Text = _resort.ResortStats.GondolaCount;
                FunicularCount.Text = _resort.ResortStats.FunicularCount;
                SurfaceTrainCount.Text = _resort.ResortStats.SurfaceTrainCount;
            }

            IsSkiArea.Checked = _resort.IsSkiArea;

            var resorts = _resortService.GetAllByContinentID(_resort.ContinentID);
            var i = 0;
            for (var index = 0; index < resorts.Count; index++)
            {
                var resort = resorts[index];
                checkedListBoxResortsForSkiArea.Items.Add(resort.Name);
                var cnt = _resort.SkiAreas.Where(r => resort.ID == r.Resort.ID).Count();
                if (cnt > 0)
                    checkedListBoxResortsForSkiArea.SetItemChecked(index, true);

                i++;
            }

            PopulateLinks();
        }

        private void PopulateLinks()
        {
            dgvLinks.ColumnCount = 3;

            dgvLinks.Columns[0].Name = "ID";
            dgvLinks.Columns[1].Name = "Name";
            dgvLinks.Columns[2].Name = "Url";

            dgvLinks.Columns[0].Width = 25;
            dgvLinks.Columns[1].Width = 380;
            dgvLinks.Columns[2].Width = 380;

            foreach (ResortLink link in _resort.ResortLinks)
            {
                string[] row = { link.ID.ToString(), link.Name, link.URL };
                dgvLinks.Rows.Add(row);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _resortService = new ResortService(_resortRepository);

            _resort.Name = ResortName.Text;
            _resort.PrettyUrl = PrettyUrl.Text;
            _resort.Longitude = double.Parse(Longitude.Text);
            _resort.Latitude = double.Parse(Latitude.Text);
            _resort.IsSkiArea = IsSkiArea.Checked;

            _resort.ResortStats.BaseLevel = BaseLevel.Text;
            _resort.ResortStats.TopLevel = TopLevel.Text;
            _resort.ResortStats.VerticalDrop = VerticalDrop.Text;

            _resort.ResortStats.BlackRuns = BlackRuns.Text;
            _resort.ResortStats.RedRuns = RedRuns.Text;
            _resort.ResortStats.BlueRuns = BlueRuns.Text;
            _resort.ResortStats.GreenRuns = GreenRuns.Text;
            _resort.ResortStats.LongestRunDistance = LongestRunDistance.Text;
            _resort.ResortStats.RunTotalDistance = RunTotalDistance.Text;
            _resort.ResortStats.RunTotal = RunTotal.Text;
            _resort.ResortStats.SkiableTerrianSize = SkiableTerrianSize.Text;
            _resort.ResortStats.HasSnowpark = (HasSnowpark.Checked) ? "True" : "False";
            _resort.ResortStats.SnowparkTotal = SnowparkTotal.Text;
            _resort.ResortStats.SnowparkDescription = SnowparkDescription.Text;
            _resort.ResortStats.HasHalfpipe = (HasHalfpipe.Checked) ? "True" : "False";
            _resort.ResortStats.HalfpipeTotal = HalfpipeTotal.Text;
            _resort.ResortStats.HalfpipeDescription = HalfpipeDescription.Text;
            _resort.ResortStats.HasQuarterpipe = (HasQuarterpipe.Checked) ? "True" : "False";
            _resort.ResortStats.QuarterpipeTotal = QuarterpipeTotal.Text;
            _resort.ResortStats.QuarterpipeDescription = QuarterpipeDescription.Text;

            _resort.ResortStats.LiftDescription = LiftDescription.Text;
            _resort.ResortStats.LiftTotal = LiftTotal.Text;
            _resort.ResortStats.LiftCapacityHour = LiftTotal.Text;
            _resort.ResortStats.QuadPlusCount = QuadPlusCount.Text;
            _resort.ResortStats.QuadCount = QuadCount.Text;
            _resort.ResortStats.TripleCount = TripleCount.Text;
            _resort.ResortStats.DoubleCount = DoubleCount.Text;
            _resort.ResortStats.SurfaceCount = SurfaceCount.Text;
            _resort.ResortStats.GondolaCount = GondolaCount.Text;
            _resort.ResortStats.FunicularCount = FunicularCount.Text;
            _resort.ResortStats.SurfaceTrainCount = SurfaceTrainCount.Text;

            _resort.ResortStats.AverageSnowfall = AverageSnowfall.Text;
            _resort.ResortStats.HasSnowmaking = (HasSnowmaking.Checked) ? "True" : "False";
            _resort.ResortStats.SnowmakingCoverage = SnowmakingCoverage.Text;
            _resort.ResortStats.Snowfall1Jan = Snowfall1Jan.Text;
            _resort.ResortStats.Snowfall2Feb = Snowfall2Feb.Text;
            _resort.ResortStats.Snowfall3Mar = Snowfall3Mar.Text;
            _resort.ResortStats.Snowfall4Apr = Snowfall4Apr.Text;
            _resort.ResortStats.Snowfall5May = Snowfall5May.Text;
            _resort.ResortStats.Snowfall6Jun = Snowfall6Jun.Text;
            _resort.ResortStats.Snowfall7Jul = Snowfall7Jul.Text;
            _resort.ResortStats.Snowfall8Aug = Snowfall8Aug.Text;
            _resort.ResortStats.Snowfall9Sep = Snowfall9Sep.Text;
            _resort.ResortStats.Snowfall10Oct = Snowfall10Oct.Text;
            _resort.ResortStats.Snowfall11Nov = Snowfall11Nov.Text;
            _resort.ResortStats.Snowfall12Dec = Snowfall12Dec.Text;

            _resort.ResortStats.HasNightskiing = (HasNightskiing.Checked) ? "True" : "False";
            _resort.ResortStats.NightskiingDescription = NightskiingDescription.Text;
            _resort.ResortStats.SeasonStartMonth = SeasonStartMonth.Text;
            _resort.ResortStats.SeasonEndMonth = SeasonEndMonth.Text;

            _resort.ResortStats.HasSummerskiing = (HasSummerskiing.Checked) ? "True" : "False";
            _resort.ResortStats.SummerskiingDescription = SummerskiingDescription.Text;
            _resort.ResortStats.SummerStartMonth = SummerStartMonth.Text;
            _resort.ResortStats.SummerEndMonth = SummerEndMonth.Text;

            if (IsSkiArea.Checked)
            {
                _resort.SkiAreas = new List<LinkResortSkiArea>();
                if (checkedListBoxResortsForSkiArea.CheckedItems.Count != 0)
                {
                    string s = "";
                    for (int x = 0; x <= checkedListBoxResortsForSkiArea.CheckedItems.Count - 1; x++)
                    {
                        var tempResort = _resortService.GetByName(checkedListBoxResortsForSkiArea.CheckedItems[x].ToString());
                        var lrsa = new LinkResortSkiArea
                                       {
                                           SkiAreaID = _resort.ID,
                                           ResortID = tempResort.ID,
                                           Resort = tempResort
                                       };
                        _resort.SkiAreas.Add(lrsa);
                    }
                    MessageBox.Show(s);
                }
                
            }

            _resortService.Update(_resort);
            lblStatus.Text = "Resort Saved OK";
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            coResorts.SelectedIndex--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            coResorts.SelectedIndex++;
        }

        private void dgvLinks_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgvLinks.Rows[e.RowIndex];
            LinkId.Text = row.Cells[0].Value.ToString();
            LinkName.Text = row.Cells[1].Value.ToString();
            Url.Text = row.Cells[2].Value.ToString();

        }

        private void btnLinkSave_Click(object sender, EventArgs e)
        {
            _resortLinkService = new ResortLinkService(_resortLinkRepository);
            var link = new ResortLink
            {
                ID = int.Parse(LinkId.Text),
                Name = LinkName.Text,
                URL = Url.Text,
                ResortID = _resort.ID
            };
            _resortLinkService.Update(link);

            GetResort();
        }

        private void btnLinkDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnLinkAdd_Click(object sender, EventArgs e)
        {
            _resortLinkService = new ResortLinkService(_resortLinkRepository);
            var link = new ResortLink
            {
                Name = NewLinkName.Text,
                URL = NewUrl.Text,
                ResortID = _resort.ID
            };
            _resortLinkService.Add(link);

            GetResort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvLinks.Rows.Clear();
        }

        private void coCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopResortsCombo(coCountries.SelectedItem.ToString());
        }
    }
}
