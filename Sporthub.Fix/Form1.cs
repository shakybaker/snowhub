using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Services;
using Sporthub.Repository;
using System.Net;
using System.Globalization;

namespace Sporthub.Fix
{
    public partial class Form1 : Form
    {
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;
        static Regex regex = new Regex(@"&#(1?\d\d);");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resortService = new ResortService(resortRepository);

            IList<Resort> resorts = resortService.GetEmptyPrettyUrls();

            foreach (Resort resort in resorts)
            {
                string encodedName = EncodeURL(CheckEncodedName(resort.Name));
                var resortCheck = resortService.Get(encodedName);

                if (resortCheck != null)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = resort.Name + " NOT ADDED!!!!!!!!!!";
                    listBox1.Items.Add(item);
                }
                else
                {
                    resort.PrettyUrl = encodedName;
                    resortService.Update(resort);
                    ListViewItem item = new ListViewItem();
                    item.Text = resort.Name + " => " + encodedName;
                    listBox1.Items.Add(item);
                }
            }
        }

        public string EncodeURL(string url)
        {
            Regex rgex = new Regex("&");
            url = rgex.Replace(url, "and");

            rgex = new Regex("[\\W\\.]");
            url = rgex.Replace(url, "-");

            rgex = new Regex("-+");
            url = rgex.Replace(url, "-");

            rgex = new Regex(":");
            url = rgex.Replace(url, "");

            return url.ToLower();
        }

        private static string CheckEncodedName(string name)
        {
            foreach (Match m in regex.Matches(name))
            {
                string i = m.Value.Substring(2, m.Value.Length - 3);

                System.Text.Encoding cp1252 = System.Text.Encoding.GetEncoding(1252);

                string s = cp1252.GetString(new byte[] { Convert.ToByte(i, 10) });

                name = name.Replace(m.Value, s);

            }

            // Perform a final conversion to remove HTML encoded values
            return HttpUtility.HtmlDecode(name);

        }

        private void btnGeoscrape_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbCountryID.Text)) ||
             (!string.IsNullOrEmpty(tbContinentID.Text)) ||
             (!string.IsNullOrEmpty(tbCountryName.Text)) ||
             (!string.IsNullOrEmpty(tbCountryName.Text)))
            {
                resortService = new ResortService(resortRepository);
                List<string> list = new List<string>();
                using (StreamReader reader = new StreamReader("scrape_in.txt"))
                {
                    char tab = '\u0009';
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Replace(" = ", "=");
                        line = line.Replace(tab, '¬');
                        line = line.Replace("¬", "");
                        //line = line.Replace("&#232;", "è");
                        //line = line.Replace("&#233;", "é");
                        //line = line.Replace("&#235;", "ë");
                        list.Add(line);
                    }
                }

                string store_id = string.Empty;
                string lat = string.Empty;
                string lng = string.Empty;
                string name = string.Empty;
                string encoded_name = string.Empty;

                foreach (string line in list)
                {

                    string id = getId(line);
                    if ((store_id != id) && (!string.IsNullOrEmpty(store_id)))
                    {
                        //write to DB
                        Resort resort = new Resort();
                        try
                        {
                            resort.Name = name;
                            resort.CountryID = int.Parse(tbCountryID.Text);
                            resort.CountryName = tbCountryName.Text;
                            resort.ContinentID = int.Parse(tbContinentID.Text);
                            resort.ContinentName = tbContinentName.Text;
                            resort.Latitude = double.Parse(lat);
                            resort.Longitude = double.Parse(lng);
                            resort.PrettyUrl = encoded_name;
                            resort.CanPublish = true;
                            resort.CreatedDate = DateTime.Now;
                            resort.UpdatedDate = DateTime.Now;

                            int resortID = resortService.Add(resort);

                            if (!string.IsNullOrEmpty(tbMountainRange.Text))
                            {
                                int lrmrID = LocationDataManager.AddResortToMountainRangeLink(resortID, int.Parse(tbMountainRange.Text));
                            }
                        }
                        catch (Exception ex)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = string.Format("{0} = {1}", name, ex.Message);
                            listBox1.Items.Add(item);
                        }
                    }
                    store_id = id;

                    string type = line.Substring(0, 4);

                    switch (type)
                    {
                        case "lats":
                            lat = getLatLng(line);
                            break;
                        case "long":
                            lng = getLatLng(line);
                            break;
                        case "type":
                            //skip
                            break;
                        case "info":
                            name = getName(line);
                            name = name.Replace(" - ", "-");
                            name = name.Replace(" / ", "/");
                            encoded_name = EncodeURL(CheckEncodedName(name));
                            name = CheckEncodedName(name);
                            break;
                        default:
                            break;
                    }
                }

                ListViewItem item2 = new ListViewItem();
                item2.Text = "========== END ==========";
                listBox1.Items.Add(item2);
            }
            else
            {
                ListViewItem item2 = new ListViewItem();
                item2.Text = "Textboxes empty";
                listBox1.Items.Add(item2);
            }

        }

        private string getId(string line)
        {
            string id = string.Empty;
            int l = line.LastIndexOf("]") - (line.IndexOf("[") + 1);
            id = line.Substring((line.IndexOf("[") + 1), l);
            return id;
        }

        private string getLatLng(string line)
        {
            string val = string.Empty;
            int l = line.LastIndexOf(";") - (line.IndexOf("=") + 1);
            val = line.Substring((line.IndexOf("=") + 1), l);
            return val;
        }

        private string getName(string line)
        {
            string val = string.Empty;
            int l = line.LastIndexOf("</b>") - (line.IndexOf("<b>") + 3);
            val = line.Substring((line.IndexOf("<b>") + 3), l);
            return val;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //europe
            List<string> urls = new List<string>();
            urls.Add("http://www.onthesnow.co.uk/AD-AD/skireport.html,Andorra");
            urls.Add("http://www.onthesnow.co.uk/AT-AT/skireport.html,Austria");
            urls.Add("http://www.onthesnow.co.uk/BE/skireport.html,Belgium");
            urls.Add("http://www.onthesnow.co.uk/BA/skireport.html,Bosnia");
            urls.Add("http://www.onthesnow.co.uk/BG/skireport.html,Bulgaria");
            urls.Add("http://www.onthesnow.co.uk/CY/skireport.html,Cyprus");
            urls.Add("http://www.onthesnow.co.uk/CZ/skireport.html,Czech Rep.");
            urls.Add("http://www.onthesnow.co.uk/FI/skireport.html,Finland");
            urls.Add("http://www.onthesnow.co.uk/FR-FR/skireport.html,France");
            urls.Add("http://www.onthesnow.co.uk/DE/skireport.html,Germany");
            urls.Add("http://www.onthesnow.co.uk/GR/skireport.html,Greece");
            urls.Add("http://www.onthesnow.co.uk/IS/skireport.html,Iceland");
            urls.Add("http://www.onthesnow.co.uk/IT/skireport.html,Italy");
            urls.Add("http://www.onthesnow.co.uk/LI/skireport.html,Liechtenstein");
            urls.Add("http://www.onthesnow.co.uk/MK/skireport.html,Macedonia");
            urls.Add("http://www.onthesnow.co.uk/NO/skireport.html,Norway");
            urls.Add("http://www.onthesnow.co.uk/PL/skireport.html,Poland");
            urls.Add("http://www.onthesnow.co.uk/PT/skireport.html,Portugal");
            urls.Add("http://www.onthesnow.co.uk/RO/skireport.html,Romania");
            urls.Add("http://www.onthesnow.co.uk/RU/skireport.html,Russia");
            urls.Add("http://www.onthesnow.co.uk/SCT/skireport.html,Scotland");
            urls.Add("http://www.onthesnow.co.uk/SRB/skireport.html,Serbia");
            urls.Add("http://www.onthesnow.co.uk/SK/skireport.html,Slovakia");
            urls.Add("http://www.onthesnow.co.uk/SI/skireport.html,Slovenia");
            urls.Add("http://www.onthesnow.co.uk/ES/skireport.html,Spain");
            urls.Add("http://www.onthesnow.co.uk/SE/skireport.html,Sweden");
            urls.Add("http://www.onthesnow.co.uk/CH-CH/skireport.html,Switzerland");

            urls.Add("http://www.onthesnow.co.uk/california/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/maine/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/colorado/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/idaho/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/montana/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/new-hampshire/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/vermont/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/utah/skireport.html,United States");
            urls.Add("http://www.onthesnow.co.uk/wyoming/skireport.html,United States");

            urls.Add("http://www.onthesnow.co.uk/alberta/skireport.html,Canada");
            urls.Add("http://www.onthesnow.co.uk/british-columbia/skireport.html,Canada");
            urls.Add("http://www.onthesnow.co.uk/ontario/skireport.html,Canada");
            urls.Add("http://www.onthesnow.co.uk/quebec/skireport.html,Canada");

            urls.Add("http://www.onthesnow.co.uk/argentina/skireport.html,Argentina");
            urls.Add("http://www.onthesnow.co.uk/australia/skireport.html,Australia");
            urls.Add("http://www.onthesnow.co.uk/chile/skireport.html,Chile");
            urls.Add("http://www.onthesnow.co.uk/new-zealand/skireport.New Zealand");

            string html = string.Empty;

            foreach (string countryUrl in urls)
            {
                string[] arrUrl = countryUrl.Split(',');
                html = GetPage(arrUrl[0]);

                if (!string.IsNullOrEmpty(html))
                {
                    List<string> resortUrls = new List<string>();
                    bool keepScraping = true;
                    while (keepScraping)
                    {
                        int s = html.IndexOf("<div class=\"name\">");
                        if (s < 0)
                        {
                            keepScraping = false;
                        }
                        else
                        {
                            html = html.Substring(s);
                            s = 5;

                            string resortUrl = GetValue(html, "http://www.onthesnow.co.uk/(?<VALUE>.+?)/skireport.html");
                            string[] arrTmp = resortUrl.Split('/');
                            if (string.IsNullOrEmpty(arrTmp[1]))
                            {
                                keepScraping = false;
                            }
                            else
                            {
                                resortUrl = "http://www.onthesnow.co.uk/" + arrTmp[0] + "/" + arrTmp[1] + "/profile.html";
                                resortUrls.Add(resortUrl + "," + GetValue(html, "skireport.html\" title=\"(?<VALUE>.+?) Snow Report"));
                            }
                            html = html.Substring(s);
                        }

                    }

                    foreach (string url in resortUrls)
                    {
                        GetResortPage(arrUrl[1], url);
                    }
                }
            }


        }

        public void GetResortPage(string countryName, string resortUrl)
        {
            string[] arrUrl = resortUrl.Split(',');
            string html = GetPage(arrUrl[0]);
            string tmp;
            Resort resort = new Resort();
            resort.Name = arrUrl[1].Replace(" - ", "-");
            Country country = new Country();
            country.CountryName = countryName;
            resort.Country = country;
            ResortStats stats = new ResortStats();
            stats.GreenRuns = GetValue(html, "Beginner Runs\" border=\"0\" />(?<VALUE>.+?)%</strong>");
            stats.BlueRuns = GetValue(html, "Intermediate Runs\" border=\"0\" />(?<VALUE>.+?)%</strong>");
            stats.RedRuns = GetValue(html, "Advanced Runs\" border=\"0\" />(?<VALUE>.+?)%</strong>");
            stats.BlackRuns = GetValue(html, "Expert Runs\" border=\"0\" />(?<VALUE>.+?)%</strong>");

            stats.LiftTotal = GetValue(html, "Total # Of Lifts\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            stats.GondolaCount = GetValue(html, "Gondolas & Trams\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            stats.QuadPlusCount = GetValue(html, "High Speed Sixes\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            int quads = 0;
            tmp = GetValue(html, "High Speed Quads\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            quads = (string.IsNullOrEmpty(tmp)) ? 0 : int.Parse(tmp);
            tmp = GetValue(html, "Quad Chairs\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            quads += (string.IsNullOrEmpty(tmp)) ? 0 : int.Parse(tmp);
            stats.QuadCount = (quads == 0) ? string.Empty : quads.ToString();
            stats.TripleCount = GetValue(html, "Triple Chairs\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            stats.DoubleCount = GetValue(html, "Double Chairs\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            stats.SurfaceCount = GetValue(html, "Surface Lifts\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)</strong>");
            stats.AverageSnowfall = GetValue(html, "Average Snowfall\" border=\"0\" align=\"absmiddle\" />(?<VALUE>.+?)  cm</strong>");


            stats.TopLevel = GetValue(html, "Top: <strong>(?<VALUE>.+?) m</strong>");
            stats.VerticalDrop = GetValue(html, "Vertical Drop: <strong>(?<VALUE>.+?) m</strong>");
            stats.BaseLevel = GetValue(html, "Bottom: <strong>(?<VALUE>.+?) m</strong>");
            stats.LongestRunDistance = GetValue(html, "Longest Run: <strong>(?<VALUE>.+?) km</strong>");
            decimal hectares = 0;
            tmp = GetValue(html, "Longest Run: <strong>(?<VALUE>.+?) km</strong>");
            if (!tmp.Contains("N/A"))
            {
                hectares = (string.IsNullOrEmpty(tmp)) ? 0 : decimal.Parse(tmp);
                decimal metres = hectares * 10000;
                stats.SkiableTerrianSize = metres.ToString();
            }
            stats.SnowmakingCoverage = GetValue(html, "Snow Making: <strong>(?<VALUE>.+?) km</strong>");

            //insert 
            int retid = ResortDataManager.AddScrapeResortStats(resort, stats);

        }

        public string GetPage(string url)
        {
            WebRequest req = WebRequest.Create(url);

            // Get the stream from the returned web response

            string retval = string.Empty;
            try
            {
                StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string strLine;
                // Read the stream a line at a time and place each one

                // into the stringbuilder

                while ((strLine = stream.ReadLine()) != null)
                {
                    // Ignore blank lines

                    if (strLine.Length > 0)
                        sb.Append(strLine);
                }
                stream.Close();
                retval = sb.ToString();
            }
            catch(Exception ex)
            {
            }
            // Get the stream from the returned web response

            return retval;
        }

        public string GetValue(string html, string matchString)
        {
            Regex regex = new Regex(matchString, RegexOptions.Singleline);
            return regex.Match(html).Groups["VALUE"].Value.Trim().Replace("N/A",string.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //copy scrape
            resortService = new ResortService(resortRepository);

            IList<Resort> resorts = resortService.GetAll();

            ScrapeResort scrape = new ScrapeResort();
            foreach (Resort resort in resorts)
            {
                scrape = LocationDataManager.GetScrapeResort(resort.Name, resort.CountryName);
                if (!string.IsNullOrEmpty(scrape.Name))
                {
                    UpdateResort(resort, scrape, resortService);
                }
                else
                {
                    //try without accents
                    scrape = LocationDataManager.GetScrapeResort(RemoveAccents(resort.Name), resort.CountryName);
                    if (!string.IsNullOrEmpty(scrape.Name))
                    {
                        UpdateResort(resort, scrape, resortService);
                    }
                }

            }

            label10.Text = "Finished";
        }

        static string RemoveAccents(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string normalized = input.Normalize(NormalizationForm.FormKD);
                StringBuilder builder = new StringBuilder();
                foreach (char c in normalized)
                {
                    if (char.GetUnicodeCategory(c) !=
                    UnicodeCategory.NonSpacingMark)
                    {
                        builder.Append(c);
                    }
                }
                return builder.ToString();
            }
            return string.Empty;
        }
        
        private static void UpdateResort(Resort resort, ScrapeResort scrape, ResortService resortService)
        {
            resort.ResortStats.BaseLevel = scrape.BaseLevel;
            resort.ResortStats.TopLevel = scrape.TopLevel;
            resort.ResortStats.VerticalDrop = scrape.VerticalDrop;

            resort.ResortStats.LiftTotal = scrape.LiftTotal;
            resort.ResortStats.QuadPlusCount = scrape.QuadPlusCount;
            resort.ResortStats.QuadCount = scrape.QuadCount;
            resort.ResortStats.TripleCount = scrape.TripleCount;
            resort.ResortStats.DoubleCount = scrape.DoubleCount;
            resort.ResortStats.SingleCount = scrape.SingleCount;
            resort.ResortStats.SurfaceCount = scrape.SurfaceCount;
            resort.ResortStats.GondolaCount = scrape.GondolaCount;
            resort.ResortStats.FunicularCount = scrape.FunicularCount;
            resort.ResortStats.SurfaceTrainCount = scrape.SurfaceTrainCount;
            resort.ResortStats.RunTotal = scrape.RunTotal;
            resort.ResortStats.RedRuns = scrape.RedRuns;
            resort.ResortStats.BlueRuns = scrape.BlueRuns;
            resort.ResortStats.GreenRuns = scrape.GreenRuns;
            resort.ResortStats.BlackRuns = scrape.BlackRuns;
            resort.ResortStats.LongestRunDistance = scrape.LongestRunDistance;
            resort.ResortStats.AverageSnowfall = scrape.AverageSnowfall;
            resort.ResortStats.SnowmakingCoverage = scrape.SnowmakingCoverage;
            resort.ResortStats.SkiableTerrianSize = scrape.SkiableTerrianSize;

            resort.Display = true;

            resortService.Update(resort);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resortService = new ResortService(resortRepository);

            IList<Resort> resorts = resortService.GetAll();

            foreach (Resort resort in resorts)
            {
                resort.NameFriendlyFormat = RemoveAccents(resort.Name);
                resort.PrettyUrlFriendlyFormat = RemoveAccents(resort.PrettyUrl);

                resortService.Update(resort);
            }

            label11.Text = "Finished";
        }
    }
}
