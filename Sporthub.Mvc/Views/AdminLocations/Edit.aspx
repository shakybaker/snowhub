<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <form id="ResortForm" action="<%= Url.Action("Edit", "Locations") %>" method="post">
    <div class="container_12">
        <div class="grid_12">
            <strong><a href="#" title="[go back]">&laquo; Go back to XYZ list</a></strong>
        </div>
        <div class="grid_6">
<% 
    if (Model.Feedback != null)
    { 
%>
            <p><%= Model.Feedback.Message %></p>
<%
    } 
%>

        </div>
        <div class="grid_6">
        </div>
        <div class="grid_12">
            <input type="reset" class="button" value="Reset" />
            <input type="submit" class="button" value="Submit" />
        </div>
        <div class="cb"></div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    $(document).ready(function() {
    });
</asp:Content>