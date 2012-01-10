<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin : <%= Model.Resort.Name%> Edit : Parks </asp:Content>

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
    <form id="ResortForm" action="<%= Url.Action("Parks", "AdminResorts") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
        <br />
        <table cellspacing="5px">
        <tr>
        <td>
        <strong><a href="/admin/resorts/list/<%= Model.Resort.Name.Substring(0,1) %>" title="[go back]">&larr; Go back to Resort list</a></strong>
        </td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/parks">Basic Info &amp; Location</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/mountain">Mountain Info & Weather</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/lifts">Lifts</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td><a href="/admin/resorts/<%= Model.Resort.PrettyUrl %>/runs">Runs</a></td>
        <td>&nbsp;|&nbsp;</td>
        <td>Parks</td>
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