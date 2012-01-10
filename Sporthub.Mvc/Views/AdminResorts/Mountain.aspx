<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin : <%= Model.Resort.Name%> Edit : Mountain Info &amp; Weather </asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
    <style type="text/css" media="all">
        @import "/Static/Styles/thickbox.css";
    </style>
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Edit <%= Model.Resort.Name%></h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <form id="ResortForm" action="<%= Url.Action("Mountain", "AdminResorts") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
        <br />
        <table cellspacing="5px">
        <tr>
        <td>
        <strong><a href="/admin/resorts/list/<%= Model.Resort.Name.Substring(0,1) %>" title="[go back]">&larr; Go back to Resort list</a></strong>
        </td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/mountain">Basic Info &amp; Location</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td>Mountain Info & Weather</td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/lifts">Lifts</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/runs">Runs</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/parks">Parks</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/links">Links</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/media">Media</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/places">Places</a></td>
        </tr>
        </table>
            
            <% if (Model.Feedback != null) { %>
            <p style="padding: 10px; width: 100%; margin: 10px 0; background-color: #ff9; color: #f90; font-weight: bold; border: 2px solid #f90; color: #f90;"><%= Model.Feedback.Message %></p>
            <% } %>
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
        <div class="grid_12">
            <span class="button"><button type="reset" name="reset" value="Reset">Reset</button></span> 
            <span class="button"><button type="submit" name="submit" value="Submit">Submit</button></span> 

            <input id="PrettyUrlCheck" type="hidden" value="false" />
            <input id="ResortID" name="ResortID" type="hidden" value="<%= Model.Resort.ID %>" />
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