<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin : <%= Model.Resort.Name%> Edit : Lifts </asp:Content>

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
    <form id="ResortForm" action="<%= Url.Action("Lifts", "AdminResorts") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
        <br />
        <table cellspacing="5px">
        <tr>
        <td>
        <strong><a href="/admin/resorts/list/<%= Model.Resort.Name.Substring(0,1) %>" title="[go back]">&larr; Go back to Resort list</a></strong>
        </td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/lifts">Basic Info &amp; Location</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/mountain">Mountain Info & Weather</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td>Lifts</td>
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