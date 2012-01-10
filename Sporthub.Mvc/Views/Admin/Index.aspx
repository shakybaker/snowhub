<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.HomeViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Admin Menu</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<div class="container_12">
    <div class="grid_3">
        
        <div class="pod">
            <p><a href="/admin/resorts/list/A">Resorts admin</a></p>
            <p><a href="/admin/resorts/add">Add a resort</a></p>
        </div>


    </div>
    <div class="grid_3">
        <div class="pod">
            <table class="table1">
            <tr><td>Users</td><td>xxx</td></tr>
            <tr><td>Joined in last 24 Hours</td><td>xxx</td></tr>
            <tr><td>Resorts</td><td>xxx</td></tr>
            <tr><td>Resorts Favourited</td><td>xxx</td></tr>
            <tr><td>Resorts Visited</td><td>xxx</td></tr>
            <tr><td>Resorts Rated</td><td>xxx</td></tr>
            <tr><td>Resorts Reviewed</td><td>xxx</td></tr>
            </table>
        </div>
    
    </div>
    <div class="grid_3">
    </div>
    <div class="grid_3">
    </div>
    <div class="cb"></div>
</div>

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>
