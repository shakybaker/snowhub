<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin : <%= Model.Resort.Name%> Edit</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
    <style type="text/css" media="all">
        @import "/Static/Styles/thickbox.css";
    </style>
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <form id="ResortForm" action="<%= Url.Action("Edit", "AdminResorts") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
        <br />
            <strong><a href="/admin/resorts/list/<%= Model.Resort.Name.Substring(0,1) %>" title="[go back]">&lArr; Go back to Resort list</a></strong>
            <% if (Model.Feedback != null) { %>
            <p><%= Model.Feedback.Message %></p>
            <% } %>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="ResortName">Resort Name</label>
                </td>
                <td>
                <%= Html.TextBox("ResortName", ViewData.Model.Resort.Name, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PrettyUrl">Pretty Url</label>
                </td>
                <td>
                <%= Html.TextBox("PrettyUrl", ViewData.Model.Resort.PrettyUrl, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Longitude">Longitude</label>
                </td>
                <td>
                <%= Html.TextBox("Longitude", ViewData.Model.Resort.Longitude.ToString(), new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Latitude">Latitude</label>
                </td>
                <td>
                <%= Html.TextBox("Latitude", ViewData.Model.Resort.Latitude.ToString(), new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Continent">Continent</label>
                </td>
                <td>
                <%=Html.DropDownList("Continent", new SelectList(ViewData.Model.Continents, "Value", "Text", ViewData.Model.Resort.ContinentID), new { @class = "ddl250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Country">Country</label>
                </td>
                <td>
                <%=Html.DropDownList("Country", new SelectList(ViewData.Model.Countries, "Value", "Text", ViewData.Model.Resort.CountryID), new { @class = "ddl250" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
            <fieldset>
                <label>Links</label>
                <div class="cb"></div>
                <div id="ResortLinks">
                <% if (Model.Resort.ResortLinks.Count > 0) { %>
                <table>
                <% foreach (var link in Model.Resort.ResortLinks) { %>
                <tr><th>URL</th><td><%= link.URL %></td></tr>
                <tr><th>Name</th><td><%= link.Name %></td></tr><%= link.ID %>
                <tr><td><a href="/admin/resortlinks/<%=Model.Resort.PrettyUrl %>/<%= link.ID %>/edit">edit</a></td><td><a href="/admin/resortlinks/<%=Model.Resort.PrettyUrl %>/delete">delete</a></td></tr>
                <tr><td colspan="2"><hr /></td></tr>
                <% } %>
                </table>
                <% } else {%>
                    <p>No links</p>
                <% } %>
                </div>
                <div class="cb"></div>
                <span class="button"><button type="button" id="AddResortLinkBtn" name="AddResortLinkBtn" value="Submit">Add Resort Link</button></span> 
                <div class="cb"></div>
            </fieldset>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">ID</label>
                </td>
                <td>
                <%= Html.TextBox("ResortID", ViewData.Model.Resort.ID, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Base Level</label>
                </td>
                <td>
                <%= Html.TextBox("BaseLevel", ViewData.Model.Resort.ResortStats.BaseLevel, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Top Level</label>
                </td>
                <td>
                <%= Html.TextBox("TopLevel", ViewData.Model.Resort.ResortStats.TopLevel, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Vertical Drop</label>
                </td>
                <td>
                <%= Html.TextBox("VerticalDrop", ViewData.Model.Resort.ResortStats.VerticalDrop, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Height</label>
                </td>
                <td>
                <%= Html.TextBox("Height", ViewData.Model.Resort.ResortStats.Height, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Average Snowfall</label>
                </td>
                <td>
                <%= Html.TextBox("AverageSnowfall", ViewData.Model.Resort.ResortStats.AverageSnowfall, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Has Snowmaking</label>
                </td>
                <td>
                <%= Html.TextBox("HasSnowmaking", ViewData.Model.Resort.ResortStats.HasSnowmaking, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Snowmaking Coverage</label>
                </td>
                <td>
                <%= Html.TextBox("SnowmakingCoverage", ViewData.Model.Resort.ResortStats.SnowmakingCoverage, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Pre-Season Start Month</label>
                </td>
                <td>
                <%= Html.TextBox("PreSeasonStartMonth", ViewData.Model.Resort.ResortStats.PreSeasonStartMonth, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Season Start Month</label>
                </td>
                <td>
                <%= Html.TextBox("SeasonStartMonth", ViewData.Model.Resort.ResortStats.SeasonStartMonth, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Season End Month</label>
                </td>
                <td>
                <%= Html.TextBox("SeasonEndMonth", ViewData.Model.Resort.ResortStats.SeasonEndMonth, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Population</label>
                </td>
                <td>
                <%= Html.TextBox("Population", ViewData.Model.Resort.ResortStats.Population, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Mountain Restaurants</label>
                </td>
                <td>
                <%= Html.TextBox("MountainRestaurants", ViewData.Model.Resort.ResortStats.MountainRestaurants, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Has Summerskiing</label>
                </td>
                <td>
                <%= Html.TextBox("HasSummerskiing", ViewData.Model.Resort.ResortStats.HasSummerskiing, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Summerskiing Description</label>
                </td>
                <td>
                <%= Html.TextBox("SummerskiingDescription", ViewData.Model.Resort.ResortStats.SummerskiingDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Summer Start Month</label>
                </td>
                <td>
                <%= Html.TextBox("SummerStartMonth", ViewData.Model.Resort.ResortStats.SummerStartMonth, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Summer End Month</label>
                </td>
                <td>
                <%= Html.TextBox("SummerEndMonth", ViewData.Model.Resort.ResortStats.SummerEndMonth, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
            <fieldset>
                <p>Avg Snowfall in cms</p>
                <table>
                <tr>
                <td>
                <label for="Snowfall11Nov">Avg Snowfall Nov</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall11Nov", ViewData.Model.Resort.ResortStats.Snowfall11Nov, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall12Dec">Avg Snowfall Dec</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall12Dec", ViewData.Model.Resort.ResortStats.Snowfall12Dec, new { @class = "tb tb100" })%>                            
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall1Jan">Avg Snowfall Jan</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall1Jan", ViewData.Model.Resort.ResortStats.Snowfall1Jan, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall2Feb">Avg Snowfall Feb</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall2Feb", ViewData.Model.Resort.ResortStats.Snowfall2Feb, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall3Mar">Avg Snowfall Mar</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall3Mar", ViewData.Model.Resort.ResortStats.Snowfall3Mar, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall4Apr">Avg Snowfall Apr</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall4Apr", ViewData.Model.Resort.ResortStats.Snowfall4Apr, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall5May">Avg Snowfall May</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall5May", ViewData.Model.Resort.ResortStats.Snowfall5May, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall6Jun">Avg Snowfall Jun</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall6Jun", ViewData.Model.Resort.ResortStats.Snowfall6Jun, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall7Jul">Avg Snowfall Jul</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall7Jul", ViewData.Model.Resort.ResortStats.Snowfall7Jul, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall8Aug">Avg Snowfall Aug</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall8Aug", ViewData.Model.Resort.ResortStats.Snowfall8Aug, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall9Sep">Avg Snowfall Sep</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall9Sep", ViewData.Model.Resort.ResortStats.Snowfall9Sep, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Snowfall10Oct">Avg Snowfall Oct</label>
                </td>
                <td>
                <%= Html.TextBox("Snowfall10Oct", ViewData.Model.Resort.ResortStats.Snowfall10Oct, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">Lift Description</label>
                </td>
                <td>
                <%= Html.TextBox("LiftDescription", ViewData.Model.Resort.ResortStats.LiftDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Lift Total</label>
                </td>
                <td>
                <%= Html.TextBox("LiftTotal", ViewData.Model.Resort.ResortStats.LiftTotal, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Lift Capacity Hour</label>
                </td>
                <td>
                <%= Html.TextBox("LiftCapacityHour", ViewData.Model.Resort.ResortStats.LiftCapacityHour, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">Quad Plus Count</label>
                </td>
                <td>
                <%= Html.TextBox("QuadPlusCount", ViewData.Model.Resort.ResortStats.QuadPlusCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Quad Count</label>
                </td>
                <td>
                <%= Html.TextBox("QuadCount", ViewData.Model.Resort.ResortStats.QuadCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Triple Count</label>
                </td>
                <td>
                <%= Html.TextBox("TripleCount", ViewData.Model.Resort.ResortStats.TripleCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Double Count</label>
                </td>
                <td>
                <%= Html.TextBox("DoubleCount", ViewData.Model.Resort.ResortStats.DoubleCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Single Count</label>
                </td>
                <td>
                <%= Html.TextBox("SingleCount", ViewData.Model.Resort.ResortStats.SingleCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Surface Count</label>
                </td>
                <td>
                <%= Html.TextBox("SurfaceCount", ViewData.Model.Resort.ResortStats.SurfaceCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Cable Lift Count</label>
                </td>
                <td>
                <%= Html.TextBox("CableLiftCount", ViewData.Model.Resort.ResortStats.CableLiftCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Gondola Count</label>
                </td>
                <td>
                <%= Html.TextBox("GondolaCount", ViewData.Model.Resort.ResortStats.GondolaCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Funicular Count</label>
                </td>
                <td>
                <%= Html.TextBox("FunicularCount", ViewData.Model.Resort.ResortStats.FunicularCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Surface Train Count</label>
                </td>
                <td>
                <%= Html.TextBox("SurfaceTrainCount", ViewData.Model.Resort.ResortStats.SurfaceTrainCount, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">Run Total</label>
                </td>
                <td>
                <%= Html.TextBox("RunTotal", ViewData.Model.Resort.ResortStats.RunTotal, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Black Runs</label>
                </td>
                <td>
                <%= Html.TextBox("BlackRuns", ViewData.Model.Resort.ResortStats.BlackRuns, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Red Runs</label>
                </td>
                <td>
                <%= Html.TextBox("RedRuns", ViewData.Model.Resort.ResortStats.RedRuns, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Blue Runs</label>
                </td>
                <td>
                <%= Html.TextBox("BlueRuns", ViewData.Model.Resort.ResortStats.BlueRuns, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Green Runs</label>
                </td>
                <td>
                <%= Html.TextBox("GreenRuns", ViewData.Model.Resort.ResortStats.GreenRuns, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">Longest Run Distance</label>
                </td>
                <td>
                <%= Html.TextBox("LongestRunDistance", ViewData.Model.Resort.ResortStats.LongestRunDistance, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Skiable Terrian Size</label>
                </td>
                <td>
                <%= Html.TextBox("SkiableTerrianSize", ViewData.Model.Resort.ResortStats.SkiableTerrianSize, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Has Nightskiing</label>
                </td>
                <td>
                <%= Html.TextBox("HasNightskiing", ViewData.Model.Resort.ResortStats.HasNightskiing, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Nightskiing Description</label>
                </td>
                <td>
                <%= Html.TextBox("NightskiingDescription", ViewData.Model.Resort.ResortStats.NightskiingDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="Name">Snowpark Total</label>
                </td>
                <td>
                <%= Html.TextBox("SnowparkTotal", ViewData.Model.Resort.ResortStats.SnowparkTotal, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Snowpark Description</label>
                </td>
                <td>
                <%= Html.TextBox("SnowparkDescription", ViewData.Model.Resort.ResortStats.SnowparkDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Halfpipe Total</label>
                </td>
                <td>
                <%= Html.TextBox("HalfpipeTotal", ViewData.Model.Resort.ResortStats.HalfpipeTotal, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Halfpipe Description</label>
                </td>
                <td>
                <%= Html.TextBox("HalfpipeDescription", ViewData.Model.Resort.ResortStats.HalfpipeDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Quarterpipe Total</label>
                </td>
                <td>
                <%= Html.TextBox("QuarterpipeTotal", ViewData.Model.Resort.ResortStats.QuarterpipeTotal, new { @class = "tb tb100" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="Name">Quarterpipe Description</label>
                </td>
                <td>
                <%= Html.TextBox("QuarterpipeDescription", ViewData.Model.Resort.ResortStats.QuarterpipeDescription, new { @class = "tb tb100" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="PictureName">Picture Name</label>
                </td>
                <td>
                <%= Html.TextBox("PictureName", ViewData.Model.Resort.PictureName, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PictureUrl">Picture Url</label>
                </td>
                <td>
                <%= Html.TextBox("PictureUrl", ViewData.Model.Resort.PictureUrl, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PictureLicenceType">Picture Licence</label>
                </td>
                <td>
                <%= Html.TextBox("PictureLicenceType", ViewData.Model.Resort.PictureLicenceType, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PictureAuthor">Picture Author</label>
                </td>
                <td>
                <%= Html.TextBox("PictureAuthor", ViewData.Model.Resort.PictureAuthor, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PictureAuthorUrl">Picture Author Url</label>
                </td>
                <td>
                <%= Html.TextBox("PictureAuthorUrl", ViewData.Model.Resort.PictureAuthorUrl, new { @class = "tb tb250" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_12">
            <span class="button"><button type="reset" name="reset" value="Reset">Reset</button></span> 
            <span class="button"><button type="submit" name="submit" value="Submit">Submit</button></span> 

            <input id="PrettyUrlCheck" type="hidden" value="false" />
            <input id="ResortID" type="hidden" value="<%= Model.Resort.ID %>" />
        </div>
        <div class="cb"></div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script type="text/javascript" src="/Static/Scripts/thickbox.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
    
        //sporthub.adminResortEdit.onLoad();

    });
</script>
</asp:Content>