<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortLinkViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin</asp:Content>

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
    <form id="ResortForm" action="<%= Url.Action("EditResortLink", "AdminResorts") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
        <br />
            <strong><a href="/admin/resorts/<%= Model.ResortPrettyUrl %>/edit" title="[go back]">&lArr; Go back to Resort</a></strong>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_6">
            <fieldset>
                <table>
                <tr>
                <td>
                <label for="ResortName">Resort Link Name</label>
                </td>
                <td>
                <%= Html.TextBox("ResortName", ViewData.Model.ResortLink.Name, new { @class = "tb tb250" })%>
                </td>
                </tr>
                <tr>
                <td>
                <label for="PrettyUrl">Resort Link Url</label>
                </td>
                <td>
                <%= Html.TextBox("PrettyUrl", ViewData.Model.ResortLink.URL, new { @class = "tb tb250" })%>
                </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div class="grid_6">
        &nbsp;
        </div>
    </div>

    <div class="container_12">
        <div class="grid_12">
            <span class="button"><button type="reset" name="reset" value="Reset">Reset</button></span> 
            <span class="button"><button type="submit" name="submit" value="Submit">Submit</button></span> 

            <input id="ResortPrettyUrl" name="ResortPrettyUrl" type="hidden" value="<%= Model.ResortPrettyUrl %>" />
            <input id="ResortLinkID" name="ResortLinkID" type="hidden" value="<%= Model.ResortLink.ID %>" />
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