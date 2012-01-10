<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AdminLocationsListViewData>" %>

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
    foreach (Sporthub.Model.Location location in Model.Locations)
    {
%>
            <li><%= location.Name %> - 
            <a href="/admin/locations/<%= location.LocationType %>/<%= location.PrettyUrl %>/list">List</a>
            <a href="/admin/locations/<%= location.LocationType %>/<%= location.PrettyUrl %>/edit">Edit</a>
<%
        if (location.LocationType == "countries")
        { 
%>
            <a href="/admin/regions/country/<%= location.ID %>/list">Regions</a>
            </li>
<%
        }
    }
%>
        </ul>
        </div>
        <div class="cb"></div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>