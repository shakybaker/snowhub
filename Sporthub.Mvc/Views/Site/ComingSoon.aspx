<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_12">
            <div class="pod2">
                <p>We have many updates in the pipeline for the Snowhub. Below is a list of planned updates to the site with an estimate of when they will be available.</p>
                <div class="podbtm" style="padding: 10px 10px 0 10px;">
                </div>
            </div>
        </div>
        <div class="grid_12">
            <div class="pod" style="margin: 20px 0 140px 0">
                <div class="headwrap">
                    <h3>Planned Features and Upgrades</h3>
                </div>
                <div class="podIn list">
                    <table class="table1" style="margin-top: 0;">
                    <tr><th>Upgrade</th><th>Description</th><th>Status</th><th>Planned Completion</th></tr>
                    <tr><td>Forums</td><td>Members will be able to take part in discussions</td><td><span style="color: #000; background-color: #0c5aa8; display: block; padding: 0 5px; margin: 3px;-moz-border-radius: 5px; -webkit-border-radius: 5px;">In Progress</span></td><td>March 2010</td></tr>
                    <tr><td>Pictures</td><td>Members will be able to upload pictures and manage galleries</td><td><span style="color: #000; background-color: #0c5aa8; display: block; padding: 0 5px; margin: 3px;-moz-border-radius: 5px; -webkit-border-radius: 5px;">In Progress</span></td><td>March 2010</td></tr>
                    <tr><td>Advertising</td><td>Business Owners will be given the facility to advertise on the Snowhub</td><td><span style="color: #000; background-color: #0c5aa8; display: block; padding: 0 5px; margin: 3px;-moz-border-radius: 5px; -webkit-border-radius: 5px;">In Progress</span></td><td>March 2010</td></tr>
                    <tr><td>Build a Trip</td><td>Members will be able to 'build a trip' so they can invite friends, add travel plans, photos, blog posts, discussions etc.</td><td><span style="color: #000; background-color: red; display: block; padding: 0 5px; margin: 3px;-moz-border-radius: 5px; -webkit-border-radius: 5px;">Planned</span></td><td>March 2010</td></tr>
                    </table>
                </div>
                <div class="podbtm" style="padding: 10px 10px 0 10px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Coming Soon</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Coming Soon</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more." />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
<ul>
<%
    int i = 1;
    foreach (Sporthub.Model.Breadcrumb bc in Model.Breadcrumbs)
    {
%>
    <li class='bcMenuitem' id='bcMenuitem_<%=(i== Model.Breadcrumbs.Count) ? 0 : i %>'><a href='<%= bc.Url %>'><%if (i < Model.Breadcrumbs.Count){%><span class='bcArrow'><%}%><%= bc.Name %><%if (i< Model.Breadcrumbs.Count){%></span><%}%></a></li>
<%
        i++;
    }
%>                
</ul>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>
