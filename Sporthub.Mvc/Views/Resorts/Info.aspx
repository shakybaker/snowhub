<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Photos</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="<%= Model.Resort.Name %> is a ski resort in <%= Model.Resort.Country.CountryName%>, <%= Model.Resort.ContinentName%>" />
<meta name="keywords" content="<%= Model.Resort.Name %> <%= Model.Resort.Country.CountryName%> <%= Model.Resort.ContinentName%> ski skiing snowboard snowboarding snow sport mountain holiday travel vacation hotels accomodation" />
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
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
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<%= Html.ResortSuits(Model.Resort, "info") %>

<div class="container_12">
    <div class="grid_12">
        <div class="headwraplarge">
            <h3>Info & Stats</h3>
        </div>
        <div class="pod">
            <div id="targetWebcams"></div>
            <div class="podbtm">
                <a href="#" title="">XXX &rarr;</a> 
            </div>
            <div class="cb"></div>
        </div>
    </div>
    <div class="cb"></div>
</div>

<div id="rateReview">
</div>
<input id="hidResortID" name="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

        var rid;
        
        $(document).ready(function() {
            rid = document.getElementById("hidResortID").value;
        });
        
    </script>

