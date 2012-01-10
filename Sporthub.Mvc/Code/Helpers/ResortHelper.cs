using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sporthub.Model.Enumerators;
using Sporthub.Model;
using System.Configuration;
using Sporthub.Utilities;

namespace Sporthub.Mvc.Code.Helpers
{
    public static class ResortHelper
    {
        public static string AverageSnowfall(this HtmlHelper helper, Resort resort)
        {
            var showPod = false;
            var ret = string.Empty;
            if (resort.ResortStats != null)
            {

                ret += "<div class='pod'>";
                ret += "<div class='headwrap'>";
                ret += "<h3>Average Snowfall</h3>";
                ret += "</div>";
                ret += "<div class='podIn'>";
                ret += "<table class='avgSnow'>";
                //TODO: swap for southern hemisphere
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall7Jul))
                {
                    showPod = true;
                    ret += "<tr><th>JUL</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall7Jul)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall7Jul + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall8Aug))
                {
                    showPod = true;
                    ret += "<tr><th>AUG</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall8Aug)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall8Aug + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall9Sep))
                {
                    showPod = true;
                    ret += "<tr><th>SEP</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall9Sep)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall9Sep + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall10Oct))
                {
                    showPod = true;
                    ret += "<tr><th>OCT</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall10Oct)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall10Oct + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall11Nov))
                {
                    showPod = true;
                    ret += "<tr><th>NOV</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall11Nov)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall11Nov + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall12Dec))
                {
                    showPod = true;
                    ret += "<tr><th>DEC</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall12Dec)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall12Dec + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall1Jan))
                {
                    showPod = true;
                    ret += "<tr><th>JAN</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall1Jan)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall1Jan + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall2Feb))
                {
                    showPod = true;
                    ret += "<tr><th>FEB</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall2Feb)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall2Feb + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall3Mar))
                {
                    showPod = true;
                    ret += "<tr><th>MAR</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall3Mar)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall3Mar + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall4Apr))
                {
                    showPod = true;
                    ret += "<tr><th>APR</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall4Apr)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall4Apr + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall5May))
                {
                    showPod = true;
                    ret += "<tr><th>MAY</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall5May)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall5May + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                if (!string.IsNullOrEmpty(resort.ResortStats.Snowfall6Jun))
                {
                    showPod = true;
                    ret += "<tr><th>JUN</th><td><span style='width: " +
                           ((int.Parse(resort.ResortStats.Snowfall6Jun)/3) + 10) + "px;'>" +
                           resort.ResortStats.Snowfall6Jun + "cm&nbsp;&nbsp;</span></td></tr>";
                }
                ret += "</table>";
                ret += "<div class='cb'></div>";
                ret += "</div>";
                ret += "</div>";
            }

            if (!showPod)
                return string.Empty;

            return ret;
        }

        public static string ResortRuns(this HtmlHelper helper, Resort resort)
        {
            bool showPod = false;
            string ret = "<table class='runInfo'>";
            var x = 0;
            int w = 8;
            if (!string.IsNullOrEmpty(resort.ResortStats.GreenRuns)) 
            {
                if (int.Parse(resort.ResortStats.GreenRuns)>0) 
                {
                    showPod = true;
                    x++;
                    ret += "<tr class='qt' title='Beginner. These are usually not marked trails, but tend to be large, open, gently sloping areas at the base of the ski area or traverse paths between the main trails.'><th class='green'>Green Runs</th><td class='green'><span style='width: " + GetRunWidth(int.Parse(resort.ResortStats.GreenRuns)) + "px;'>" + resort.ResortStats.GreenRuns + "%&nbsp;&nbsp;&nbsp;</span></td></tr>";
                }
            }
            if (!string.IsNullOrEmpty(resort.ResortStats.BlueRuns)) 
            {
                if (int.Parse(resort.ResortStats.BlueRuns)>0) 
                {
                    showPod = true;
                    x++;
                    ret += "<tr class='qt' title='Easy (similar to the North American Green Circle). Almost always groomed, or on so shallow a slope as not to need it. The slope gradient shall not exceed 25% except for short wide sections with a higher gradient.'><th class='blue'>Blue Runs</th><td class='blue'><span style='width: " + GetRunWidth(int.Parse(resort.ResortStats.BlueRuns)) + "px;'>" + resort.ResortStats.BlueRuns + "%&nbsp;&nbsp;&nbsp;</span></td></tr>";
                }
            }
            if (!string.IsNullOrEmpty(resort.ResortStats.RedRuns)) 
            {
                if (int.Parse(resort.ResortStats.RedRuns)>0) 
                {
                    showPod = true;
                    x++;
                    ret += "<tr class='qt' title='Intermediate. Steeper, or narrower than a blue slope, these are usually groomed, unless the narrowness of the trail prohibits it. The slope gradient shall not exceed 40% except for short wide sections with a higher gradient.'><th class='red'>Red Runs</th><td class='red'><span style='width: " + GetRunWidth(int.Parse(resort.ResortStats.RedRuns)) + "px;'>" + resort.ResortStats.RedRuns + "%&nbsp;&nbsp;&nbsp;</span></td></tr>";
                }
            }
            if (!string.IsNullOrEmpty(resort.ResortStats.BlackRuns)) 
            {
                if (int.Parse(resort.ResortStats.BlackRuns)>0) 
                {
                    showPod = true;
                    x++;
                    ret += "<tr class='qt' title='Expert. Steep, may or may not be groomed, or may be groomed for moguls. Black can be a very wide classification, ranging from a slope marginally more difficult than a Red to very steep avalanche chutes. France tends to have a higher limit between red and black. Black ski trails should be approached with caution and sufficient prior research undertaken to ensure the level of skill required when tackling the run.'><th class='black'>Black Runs</th><td class='black'><span style='width: " + GetRunWidth(int.Parse(resort.ResortStats.BlackRuns)) + "px;'>" + resort.ResortStats.BlackRuns + "%&nbsp;&nbsp;&nbsp;</span></td></tr>";
                }
            }
            ret += "</table>";

            if (!showPod)
                return string.Empty;

            return ret;
        }

        public static int GetRunWidth(int runPercent)
        {
            //int maxVal = 155;
            double multiplier = 1.2;
            int retVal = (int)Math.Round(((decimal)((runPercent*multiplier)+35)), 0);

            return retVal;
        }

        public static string ResortSuits(this HtmlHelper helper, Resort resort)
        {
            if (!ResortSuitsGroup1Check(resort) && !ResortSuitsGroup2Check(resort) && !ResortSuitsGroup3Check(resort) && !ResortSuitsGroup4Check(resort))
            {
                return "<div style=\"margin: 0;line-height: 1.3em;\">No \"Resort Suits\" ratings are available yet. Do you have an opinion? Then be the first to <a class=\"rate\"  href=\"/review/" + resort.PrettyUrl + "?ReturnUrl=/resorts/" + resort.PrettyUrl + "\" id=\"rateReviewResort\">add your ratings</a></div>";
            }
            int[,] group1 = GetGroup1Values(resort);
            string ret = "<table class='table1 nobord'>";
            ret += "<tr><th class='ratinghead' colspan='2'>Ability most suits ...</th></tr>";
            ret += "<tr><td style='width: 60%'>Expert</td><td><span title='" + group1[1, 0] + "% (" + group1[1, 1] + " votes)' style='background-position: -" + (100 - group1[1, 0]) + "px 0;' class='rating-bar green'>" + group1[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Advanced</td><td><span title='" + group1[2, 0] + "% (" + group1[2, 1] + " votes)' style='background-position: -" + (100 - group1[2, 0]) + "px 0;' class='rating-bar green'>" + group1[2, 0] + "%</span></td></tr>";
            ret += "<tr><td>Intermediate</td><td><span title='" + group1[3, 0] + "% (" + group1[3, 1] + " votes)' style='background-position: -" + (100 - group1[3, 0]) + "px 0;' class='rating-bar green'>" + group1[3, 0] + "%</span></td></tr>";
            ret += "<tr><td>Beginner</td><td><span title='" + group1[4, 0] + "% (" + group1[4, 1] + " votes)' style='background-position: -" + (100 - group1[4, 0]) + "px 0;' class='rating-bar green'>" + group1[4, 0] + "%</span></td></tr>";
            int[,] group2 = GetGroup2Values(resort);
            ret += "<tr><th class='ratinghead' colspan='2'>Nightlife is ...</th></tr>";
            ret += "<tr><td style='width: 60%'>Lively</td><td><span title='" + group2[1, 0] + "% (" + group2[1, 1] + " votes)' style='background-position: -" + (100 - group2[1, 0]) + "px 0;' class='rating-bar green'>" + group2[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Average</td><td><span title='" + group2[2, 0] + "% (" + group2[2, 1] + " votes)' style='background-position: -" + (100 - group2[2, 0]) + "px 0;' class='rating-bar green'>" + group2[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Quiet</td><td><span title='" + group2[3, 0] + "% (" + group2[3, 1] + " votes)' style='background-position: -" + (100 - group2[3, 0]) + "px 0;' class='rating-bar green'>" + group2[1, 0] + "%</span></td></tr>";
            int[,] group3 = GetGroup3Values(resort);
            ret += "<tr><th class='ratinghead' colspan='2'>Terrian suits ...</th></tr>";
            ret += "<tr><td style='width: 60%'>Skiers</td><td><span title='" + group3[1, 0] + "% (" + group3[1, 1] + " votes)' style='background-position: -" + (100 - group3[1, 0]) + "px 0;' class='rating-bar green'>" + group3[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Snowboarders</td><td><span title='" + group1[2, 0] + "% (" + group3[2, 1] + " votes)' style='background-position: -" + (100 - group3[2, 0]) + "px 0;' class='rating-bar green'>" + group3[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Both</td><td><span title='" + group3[3, 0] + "% (" + group3[3, 1] + " votes)' style='background-position: -" + (100 - group3[3, 0]) + "px 0;' class='rating-bar green'>" + group3[1, 0] + "%</span></td></tr>";
            int[,] group4 = GetGroup4Values(resort);
            ret += "<tr><th class='ratinghead' colspan='2'>Expense level ...</th></tr>";
            ret += "<tr><td style='width: 60%'>Expensive</td><td><span title='" + group4[1, 0] + "% (" + group4[1, 1] + " votes)' style='background-position: -" + (100 - group4[1, 0]) + "px 0;' class='rating-bar green'>" + group4[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Affordable</td><td><span title='" + group4[2, 0] + "% (" + group4[2, 1] + " votes)' style='background-position: -" + (100 - group4[2, 0]) + "px 0;' class='rating-bar green'>" + group4[1, 0] + "%</span></td></tr>";
            ret += "<tr><td>Cheap</td><td><span title='" + group4[3, 0] + "% (" + group4[3, 1] + " votes)' style='background-position: -" + (100 - group4[3, 0]) + "px 0;' class='rating-bar green'>" + group4[1, 0] + "%</span></td></tr>";
            ret += "</table>";

            return ret;
        }

        public static bool ResortSuitsGroup1Check(Resort resort)
        {
            if (resort.ResortSuitsExpert == 0 && resort.ResortSuitsAdvanced == 0 && resort.ResortSuitsIntermediate == 0 && resort.ResortSuitsBeginner == 0)
            {
                return false;
            }
            return true;
        }

        public static bool ResortSuitsGroup2Check(Resort resort)
        {
            if (resort.ResortSuitsLively == 0 && resort.ResortSuitsAverage == 0 && resort.ResortSuitsQuiet == 0)
            {
                return false;
            }
            return true;
        }

        public static bool ResortSuitsGroup3Check(Resort resort)
        {
            if (resort.ResortSuitsSkiers == 0 && resort.ResortSuitsSnowboarders == 0 && resort.ResortSuitsBoth == 0)
            {
                return false;
            }
            return true;
        }

        public static bool ResortSuitsGroup4Check(Resort resort)
        {
            if (resort.ResortSuitsExpensive == 0 && resort.ResortSuitsAffordable == 0 && resort.ResortSuitsCheap == 0)
            {
                return false;
            }
            return true;
        }

        public static int[,] GetGroup1Values(Resort resort)
        {
            int[,] values = new int[10, 2];
            values[0, 0] = 100;
            values[0, 1] = resort.ResortSuitsExpert + resort.ResortSuitsAdvanced + resort.ResortSuitsIntermediate + resort.ResortSuitsBeginner;
            decimal onePercent = (decimal)values[0, 1] / 100;
            values[1, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsExpert / onePercent), 0);
            values[2, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsAdvanced / onePercent), 0);
            values[3, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsIntermediate / onePercent), 0);
            values[4, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsBeginner / onePercent), 0);
            values[1, 1] = resort.ResortSuitsExpert;
            values[2, 1] = resort.ResortSuitsAdvanced;
            values[3, 1] = resort.ResortSuitsIntermediate;
            values[4, 1] = resort.ResortSuitsBeginner;

            return values;
        }

        public static int[,] GetGroup2Values(Resort resort)
        {
            int[,] values = new int[10, 2];
            values[0, 0] = 100;
            values[0, 1] = resort.ResortSuitsLively + resort.ResortSuitsAverage + resort.ResortSuitsQuiet;
            decimal onePercent = (decimal)values[0, 1] / 100;
            values[1, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsLively / onePercent), 0);
            values[2, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsAverage / onePercent), 0);
            values[3, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsQuiet / onePercent), 0);
            values[1, 1] = resort.ResortSuitsLively;
            values[2, 1] = resort.ResortSuitsAverage;
            values[3, 1] = resort.ResortSuitsQuiet;

            return values;
        }

        public static int[,] GetGroup3Values(Resort resort)
        {
            int[,] values = new int[10, 2];
            values[0, 0] = 100;
            values[0, 1] = resort.ResortSuitsSkiers + resort.ResortSuitsSnowboarders + resort.ResortSuitsBoth;
            decimal onePercent = (decimal)values[0, 1] / 100;
            values[1, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsSkiers / onePercent), 0);
            values[2, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsSnowboarders / onePercent), 0);
            values[3, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsBoth / onePercent), 0);
            values[1, 1] = resort.ResortSuitsSkiers;
            values[2, 1] = resort.ResortSuitsSnowboarders;
            values[3, 1] = resort.ResortSuitsBoth;

            return values;
        }

        public static int[,] GetGroup4Values(Resort resort)
        {
            int[,] values = new int[10, 2];
            values[0, 0] = 100;
            values[0, 1] = resort.ResortSuitsExpensive + resort.ResortSuitsAffordable + resort.ResortSuitsCheap;
            decimal onePercent = (decimal)values[0, 1] / 100;
            values[1, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsExpensive / onePercent), 0);
            values[2, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsAffordable / onePercent), 0);
            values[3, 0] = (values[0, 1] == 0) ? 0 : (int)Math.Round(((decimal)resort.ResortSuitsCheap / onePercent), 0);
            values[1, 1] = resort.ResortSuitsExpensive;
            values[2, 1] = resort.ResortSuitsAffordable;
            values[3, 1] = resort.ResortSuitsCheap;

            return values;
        }

        public static string ResortNavigation(this HtmlHelper helper, Resort resort, string selectedTab, string returnUrl)
        {
            bool isFave = false;
            bool hasVisited = false;
            bool hasReviewed = false;
            string month = string.Empty;
            string year = string.Empty;
            string loggedIn = " not-logged-in";

            if (UserContext.UserIsLoggedIn())
            {
                loggedIn = string.Empty;
                var db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
                var linkResortUserToUpdate = (from lru in db.LinkResortUsers
                                              where lru.ResortID == resort.ID && lru.UserID == UserContext.CurrentUser.ID
                                              select lru).SingleOrDefault();

                if (linkResortUserToUpdate != null)
                {
                    if (linkResortUserToUpdate.IsFavourite)
                    {
                        isFave = true;
                    }
                    if (linkResortUserToUpdate.HasVisited)
                    {
                        hasVisited = true;
                    }
                    if (linkResortUserToUpdate.Score > 0)
                    {
                        hasReviewed = true;
                    }
                    if (!string.IsNullOrEmpty(linkResortUserToUpdate.LastVisitDate))
                    {
                        string[] arr = linkResortUserToUpdate.LastVisitDate.Split('-');
                        year = arr[0];
                        month = arr[1];
                    }
                }
            }

            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string selectBoxes = "<select name=\"dob_m\" id=\"dob_m\" class=\"narrow\" style=\"float: left\">";
            selectBoxes += "<option value=\"\" selected=\"selected\">Month</option>";
            for (int i = 1; i <= 12; i++) 
            {
                selectBoxes += string.Format("<option value=\"{0}\">{1}</option>", i, months[i-1]);
            }
            selectBoxes += "</select>";
            selectBoxes += "<select name=\"dob_y\" id=\"dob_y\" class=\"narrow\" style=\"float: left; margin-left: 8px;\">";
            selectBoxes += "<option value=\"\" selected=\"selected\">Year</option>";
            for (int i = DateTime.Now.Year; i >= 1960; i--)
            {
                selectBoxes += string.Format("<option value=\"{0}\">{1}</option>", i, i);
            }
            selectBoxes += "</select>";

            string ret = "<div id=\"PageHeader\" class=\"container_12\">";
            ret += "<div class=\"grid_12\">";
            //ret += "<h2 id=\"PageHeading\" class=\"pad\" style=\"background: transparent url(/static/images/flags/lg/24/" + resort.Country.ISO3166Alpha2 + ".png) 10px 9px no-repeat\">" + resort.Name + "<span>, " + resort.Country.CountryName + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ((Sporthub.Utils.Helpers.IsAdmin()) ? "<a href=\"/admin/resorts/" + resort.PrettyUrl + "/edit\">edit</a>" : "") + "</h2>";
            ret += "<h2 class=\"pad\" style=\"background: transparent url(/static/images/flags/lg/24/" + resort.Country.ISO3166Alpha2 + ".png) 0 37% no-repeat\">" + resort.Name + "<span>, " + resort.Country.CountryName + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ((Sporthub.Utils.Helpers.IsAdmin()) ? "<a href=\"/admin/resorts/" + resort.PrettyUrl + "/default\">edit</a>" : "") + "</h2>";
            ret += "</div>";
            ret += "</div>";
            ret += "<div class=\"container_12\">";
            ret += "<div class=\"grid_6\">";
            ret += "&nbsp;";
            ret += "</div>";
            ret += "<div class=\"grid_6\">";
            ret += "<div class=\"ratingButtons\">";

            ret += "<table>";
            ret += "<tr>";

            if (hasReviewed)
            {
                ret += "<td><a href=\"/review/" + resort.PrettyUrl + "?ReturnUrl=/resorts/" + resort.PrettyUrl + "\" class=\"actbutt left\" title=\"Edit your review/ratings for this resort\"><span><img src=\"/static/images/review_on.png\" alt=\"\" /> Rate</span</a></td>";
            }
            else
            {
                if (UserContext.UserIsLoggedIn())
                {
                    ret += "<td><a href=\"/review/" + resort.PrettyUrl + "?ReturnUrl=/resorts/" + resort.PrettyUrl + "\" class=\"actbutt left" + loggedIn + "\" title=\"Add a review for this resort and rate facilities\"><span><img src=\"/static/images/review_off.png\" alt=\"\" /> Rate</span</a></td>";
                }
                else
                {
                    ret += "<td><a href=\"#loginPopup\" class=\"actbutt left" + loggedIn + "\" title=\"Add a review for this resort and rate facilities\"><span><img src=\"/static/images/review_off.png\" alt=\"\" /> Rate</span</a></td>";
                }
            }
            if (isFave)
            {
                ret += "<td><a id=\"addAsFave\" href=\"#\" class=\"actbutt middle\" title=\"This resort is a favourite. Click again to clear\"><span><img src=\"/static/images/fave_on.png\" alt=\"\" /> Favourite</span</a></td>";
            }
            else
            {
                if (UserContext.UserIsLoggedIn())
                {
                    ret += "<td><a id=\"addAsFave\" href=\"#\" class=\"actbutt middle" + loggedIn + "\" title=\"Add this resort as a favourite\"><span><img src=\"/static/images/fave_off.png\" alt=\"\" />Favourite</span</a></td>";
                }
                else
                {
                    ret += "<td><a href=\"#loginPopup\" class=\"actbutt middle" + loggedIn + "\" title=\"Add this resort as a favourite\"><span><img src=\"/static/images/fave_off.png\" alt=\"\" />Favourite</span</a></td>";
                }
            }
            if (hasVisited)
            {
                ret += "<td><a id=\"markAsVisited\" href=\"#visitEntryPopup\" class=\"actbutt middle\" title=\"You have visited this resort. Click again to clear\"><span><img src=\"/static/images/been_on.png\" alt=\"\" /> You've Been</span</a></td>";
            }
            else
            {
                if (UserContext.UserIsLoggedIn())
                {
                    ret += "<td><a id=\"markAsVisited\" href=\"#visitEntryPopup\" class=\"actbutt middle" + loggedIn + "\" title=\"Mark resort as 'visited'\"><span><img src=\"/static/images/been_off.png\" alt=\"\" /> I've Been</span</a></td>";
                }
                else
                {
                    ret += "<td><a href=\"#loginPopup\" class=\"actbutt middle" + loggedIn + "\" title=\"Mark resort as 'visited'\"><span><img src=\"/static/images/been_off.png\" alt=\"\" /> I've Been</span</a></td>";
                }
            }
            if (UserContext.UserIsLoggedIn())
            {
                ret += "<td><a href=\"/resorts/checkin/" + resort.PrettyUrl + "?ReturnUrl=" + returnUrl + "\" class=\"actbutt right" + loggedIn + "\" title=\"If you are currently at this resort then 'Check In' here\"><span>Check In</span</a></td>";
            }
            else
            {
                ret += "<td><a href=\"#loginPopup\" class=\"actbutt right" + loggedIn + "\" title=\"If you are currently at this resort then 'Check In' here\"><span>Check In</span</a></td>";
            }

            //ret += "<td>&nbsp;</td>";
            //ret += "<td><a href=\"/places/add/" + resort.PrettyUrl + "?ReturnUrl=" + returnUrl + "\" class=\"actbutt single\" title=\"Add a bar, shop, business etc to this resort\"><span>Add a Place</span</a></td>";

            ret += "</tr>";
            ret += "</table>";
            ret += "<div style=\"display:none\"><div class=\"login\" id=\"loginPopup\"><div class=\"loginPopupIn\"><p>You must be logged-in to do that</p><p><a class=\"smlbutt\" href=\"/account/login\">Login</a> <a class=\"smlbutt\" href=\"/account/create\">Create an Account</a></p><p style=\"float: left; clear: both;\"><a href=\"#\" id=\"cnclButt\">No Thanks</a></p></div></div></div>";
            ret += string.Format("<div style=\"display:none\"><div class=\"visitEntry\" id=\"visitEntryPopup\"><div class=\"visitEntryIn\"><label>Please enter the date of your Last Visit</label>{0}<a id=\"cnclButt\" class=\"smlbutt cncl\" href=\"#\">Cancel</a><a id=\"svButt\" class=\"smlbutt\" href=\"#\">Save</a></div></div></div>", selectBoxes);
            
            //ret += "<a class=\"rb checkin\" href=\"/Resorts/CheckIn/" + resort.PrettyUrl + "?ReturnUrl=" + returnUrl + "\" id=\"checkInHere\"><span></span></a>&nbsp;&nbsp;";
            //if (hasVisited)
            //{
            //    ret += "<a class=\"rb visit isVisited\" href=\"#\" id=\"markAsVisited\"><span></span>Visited</a>&nbsp;&nbsp;";
            //}
            //else
            //{
            //    ret += "<a class=\"rb visit\" href=\"#\" id=\"markAsVisited\"><span></span></a>&nbsp;&nbsp;";
            //}
            //ret += string.Format("<div class=\"visitEntry\" id=\"visitEntryPopup\"><div class=\"visitEntryIn\"><label>Last Visit Date</label>{0}<a id=\"cnclButt\" class=\"smlbutt cncl\" href=\"#\">Cancel</a><a id=\"svButt\" class=\"smlbutt\" href=\"#\">Save</a></div></div>", selectBoxes);
            //if (isFave)
            //{
            //    ret += "<a class=\"rb fave isFave\" title\"This resort is a Favourite. Click to remove\" href=\"#\" id=\"addAsFave\"><span></span>A Favourite</a>&nbsp;&nbsp;";
            //}
            //else
            //{
            //    ret += "<a class=\"rb fave\"  href=\"#\" id=\"addAsFave\"><span></span></a>&nbsp;&nbsp;";
            //}
            //if (UserContext.UserIsLoggedIn())
            //{
            //    ret += "<a class=\"rb rate\" title=\"Rate and review this resort\" href=\"/review/" + resort.PrettyUrl + "?ReturnUrl=" + returnUrl + "\" id=\"rateReviewResort\"><span></span>Rate/Review</a>";
            //}
            //else
            //{
            //    ret += "<a class=\"rb ratewarn\" title=\"Rate and review this resort\" onclick=\"alert('You need to be logged in to do that')\" href=\"#\" id=\"rateReviewResort\"><span></span></a>";
            //}
            ret += "</div>";
            ret += "</div>";
            ret += "</div>";
            ret += "<div class=\"container_12\">";
            ret += "<div class=\"grid_12\" style=\"height: 23px;\">";
            ret += "<ul class=\"menuTabs tabNav\">";
            ret += "<li class=\"tab" + ((selectedTab == "overview") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "\"><span style=\"position: relative;\">Overview</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "map") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/map\"><span>Map</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "reviews") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/reviews\"><span>Reviews</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "places") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/places\"><span>Places</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "photos") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/photos\"><span>Photos</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "videos") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/videos\"><span>Videos</span></a></li>";
            ret += "<li class=\"tab" + ((selectedTab == "webcams") ? " selectedTab" : "") + "\"><a title=\"\" href=\"/resorts/" + resort.PrettyUrl + "/webcams\"><span>Webcams</span></a></li>";
            ret += "</ul>";
            ret += "</div>";
            ret += "</div>";

            return ret;
        }

        public static string Score(this HtmlHelper helper, string score, ScoreSize scoreSize, int? scoreCount, int? reviewedCount, int? favedCount, int? visitedCount)
        {
            // Create tag builder
            var builder = new TagBuilder("span");
            int reviewedCnt = reviewedCount ?? 0;
            int favedCnt = favedCount ?? 0;
            int visitedCnt = visitedCount ?? 0;

            string qt = (reviewedCnt > 0) ? "qt " : string.Empty;

            string className = qt + "rating-score-sm ";
            string className2 = qt + "rating-word ";
            string className3 = string.Empty;
            string ratingWord = string.Empty;
            string scoreWord = string.Empty;
            string ratingHtml = string.Empty;
            string ratingMessage = string.Empty;

            if (scoreSize == ScoreSize.Large)
            {
                className = "qt rating-score ";
            }

            if (string.IsNullOrEmpty(score) || score == "0")
            {
                ratingMessage = "<span class=\"rating-msg\">Login to rate</span>";
                className = string.Concat(className, "unrated");
                className2 = string.Concat(className2, "unrated");
                ratingWord = "Unrated";
                scoreWord = "?";
            }
            else
            {
                if (scoreCount != null)
                {
                    ratingMessage = string.Format("<span class=\"rating-msg\">Based on {0} rating{1}</span>", scoreCount, ((scoreCount == 1) ? string.Empty : "s"));
                }
                else
                {
                    ratingMessage = string.Empty;
                }
                scoreWord = score;
                int s = Convert.ToInt32(score);
                if (s < 30)
                {
                    className = string.Concat(className, "bottom");
                }
                else if (s > 69)
                {
                    className = string.Concat(className, "top");
                }
                else
                {
                    className = string.Concat(className, "middle");
                }

                if (s == 100)
                {
                    className = string.Concat(className, " max");
                    className3 = string.Concat(className2, "top");
                    ratingWord = "Perfect";
                }
                if (s < 100)
                {
                    className3 = string.Concat(className2, "top");
                    ratingWord = "Superb";
                }
                if (s < 90)
                {
                    className3 = string.Concat(className2, "top");
                    ratingWord = "Fantastic";
                }
                if (s < 80)
                {
                    className3 = string.Concat(className2, "top");
                    ratingWord = "Great";
                }
                if (s < 70)
                {
                    className3 = string.Concat(className2, "middle");
                    ratingWord = "Really Good";
                }
                if (s < 60)
                {
                    className3 = string.Concat(className2, "middle");
                    ratingWord = "Good";
                }
                if (s < 50)
                {
                    className3 = string.Concat(className2, "middle");
                    ratingWord = "OK";
                }
                if (s < 40)
                {
                    className3 = string.Concat(className2, "middle");
                    ratingWord = "Below Average";
                }
                if (s < 30)
                {
                    className3 = string.Concat(className2, "bottom");
                    ratingWord = "Really Bad";
                }
                if (s < 20)
                {
                    className3 = string.Concat(className2, "bottom");
                    ratingWord = "Awful";
                }
                if (s < 10)
                {
                    className3 = string.Concat(className2, "bottom");
                    ratingWord = "Terrible";
                }
            }

            if (scoreSize == ScoreSize.Large)
            {
//                ratingHtml = "<p class=\"rating-word-wrap\"><span class=\"" + className2 + "\">" + ratingWord + "</span>" + ratingMessage + "</p>";

                ratingHtml = "<table class=\"community-stats\">";
                ratingHtml += "<tr class=\"qt\" title=\"Reviewed\">";
                ratingHtml += "<td class=\"" + ((reviewedCnt == 0) ? "g" : "b") + " w24\"><img src=\"/Static/Images/white-star_48.png\" /></td>";
                ratingHtml += "<td class=\"" + ((reviewedCnt == 0) ? "g" : "b") + "\"><span class=\"counter\">" + reviewedCnt + "</span></td>";
                ratingHtml += "</tr>";
                ratingHtml += "</table>";
                ratingHtml += "<table class=\"community-stats\">";
                ratingHtml += "<tr class=\"qt\" title=\"Visited\">";
                ratingHtml += "<td class=\"" + ((visitedCnt == 0) ? "g" : "b") + " w24\"><img src=\"/Static/Images/white-tick_48.png\" /></td>";
                ratingHtml += "<td class=\"" + ((visitedCnt == 0) ? "g" : "b") + "\"><span class=\"counter\">" + visitedCnt + "</span></td>";
                ratingHtml += "</tr>";
                ratingHtml += "</table>";
                ratingHtml += "<table class=\"community-stats\">";
                ratingHtml += "<tr class=\"qt\" title=\"Faved\">";
                ratingHtml += "<td class=\"" + ((favedCnt == 0) ? "g" : "b") + " w24\"><img src=\"/Static/Images/white-heart_48.png\" /></td>";
                ratingHtml += "<td class=\"" + ((favedCnt == 0) ? "g" : "b") + "\"><span class=\"counter\">" + favedCnt + "</span></td>";
                ratingHtml += "</tr>";
                ratingHtml += "</table>";
            }

            // Add attributes
            //if (scoreSize == ScoreSize.Large)
            //{
            //    builder.InnerHtml = ratingWord;
            //}
            //else
            //{
            //}

            if (scoreSize == ScoreSize.Large)
            {
                scoreWord += "<span class=\"rating-score-total\">out of 100</span>";
            }
            builder.InnerHtml = scoreWord;
            if (reviewedCnt > 0)
                builder.Attributes.Add("title", string.Format("Based on {0} review{1}", reviewedCnt, (reviewedCnt == 1) ? string.Empty : "s"));
            builder.MergeAttribute("class", className);

            // Render tag
            //return builder.ToString(TagRenderMode.SelfClosing);
            return builder.ToString() + ratingHtml;
        }
    }
}
