<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name%> Ski Area</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="<%= Model.Resort.Name %> is a ski area in <%= Model.Resort.Country.CountryName%>, <%= Model.Resort.ContinentName%>" />
<meta name="keywords" content="<%= Model.Resort.Name %> <%= Model.Resort.Country.CountryName%> <%= Model.Resort.ContinentName%> ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
<ul>
<%
int i = 1;
foreach (Sporthub.Model.Breadcrumb bc in Model.Breadcrumbs)
{
%>
    <li class='bcMenuitem' id='bcMenuitem_<%=(i== Model.Breadcrumbs.Count) ? 0 : i %>'><a href='<%= bc.Url %>'><%if (i < Model.Breadcrumbs.Count)
                                                                                                                 {%><span class='bcArrow'><%}%><%= bc.Name%><%if (i < Model.Breadcrumbs.Count)
                                                                                                                                                                                               {%></span><%}%></a></li>
<%
i++;
}
%>                
</ul>
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
<div class="container_12">
    <div class="grid_12"><h2><%= Model.Resort.Name %> <span>Ski Area</span></h2></div>
</div>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<div class="container_12">
    <div class="grid_3">
        <div class="pod" style="margin-top: 20px">
            <div class="headwrap">
                <h3>Resorts</h3>
            </div>
            <p style="padding: 0 0 10px 0;">The following resorts are part of the <strong><%=Model.Resort.Name %></strong> Ski Area.</p>
            <div class="podIn list" style="padding-bottom: 8px;">
                <ul class="list1">
                <% foreach (Sporthub.Model.LinkResortSkiArea linkResort in Model.Resort.SkiAreas)
                   { %>
                    <li><a href="/resorts/<%= linkResort.Resort.PrettyUrl %>" title="go to <%= linkResort.Resort.Name%>"><span class="flag <%= linkResort.Resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= linkResort.Resort.Name%><%= Html.Score(linkResort.Resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null)%></a></li>

                <%} %>
                </ul>
                <div class="podbtm"></div>
            </div>
        </div>
    </div>
    <div class="grid_9" style="margin-top: 20px;">
        <div id="Map" style="border:10px solid #ddd; height:370px; width:680px;"></div>
    </div>
</div>

<div id="rateReview">
</div>
<input id="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />

</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        var zoom_lvl = 10;
        var lg_map = false;
        var fetch_all = true;
    </script>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/dragzoom.js"></script>
    <script type="text/javascript" src="/Static/Scripts/maps.js"></script>
    <script type="text/javascript" language="javascript">

    var maxx, maxy, minx, miny, lat, lng;

    $(document).ready(function() {
        
        $.each($.browser, function(i) {
            if ($.browser.msie) {
                sporthub.isIE = true; //detect IE, because its a pile of fucking shit
            }
        });

        lat = parseFloat($("#hidLat").val());
        lng = parseFloat($("#hidLng").val());
        maxy = lat + 0.1;
        miny = lat - 0.1;
        maxx = lng + 0.1;
        minx = lng - 0.1;
    });

    </script>
</asp:Content>
