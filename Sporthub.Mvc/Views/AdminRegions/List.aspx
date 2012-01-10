<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AdminRegionsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_12">
        <ul>
<%
    foreach (Sporthub.Model.Region region in Model.Regions)
    {
%>
            <li><a href="/admin/regions/<%= region.ID %>/list"><%= region.Name %></a>. region.ParentRegion.Name=<%= region.ParentRegion.Name %>. regions.Country.CountryName=<%= region.Country.CountryName%></li>
<%
    }
%>
        </ul>
        </div>
        <div class="cb"></div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>