<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AirportsLandingViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Airports in Europe</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="European Airports close to ski resorts. Flying to the Alps, skiing and snowboarding in Europe" />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation airports" />
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
<ul>
<%
    int i = 1;
    foreach (Sporthub.Model.Breadcrumb bc in Model.Breadcrumbs) {
%>
    <li class='bcMenuitem' id='bcMenuitem_<%=(i== Model.Breadcrumbs.Count) ? 0 : i %>'><a href='<%= bc.Url %>'><%if (i < Model.Breadcrumbs.Count){%><span class='bcArrow'><%}%><%= bc.Name %><%if (i< Model.Breadcrumbs.Count){%></span><%}%></a></li>
<%      i++;
    } %>                
</ul>
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
<div class="container_12">
    <div class="grid_12"><h2>Ski Airports<span>, Europe</span></h2></div>
</div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Ski Airports</h3>
                </div>
                <ul class="list1">
                <% foreach (Sporthub.Model.Airport airport in Model.Airports) { %>
                <% var m = 0; %>
                <%--<li><a href="/airports/<%= airport.PrettyUrl %>/list"><img class="flag" alt="[<%= airport.Country.ISO3166Alpha2 %>]" src="/static/images/flags/<%= airport.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= airport.Name %></a></li>--%>
                <li><a href="/airports/<%= airport.PrettyUrl %>"><img class="flag" alt="[<%= airport.Country.ISO3166Alpha2 %>]" src="/static/images/flags/<%= airport.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= airport.Name %></a></li>
                <% m++; %>
                <% } %>
                </ul>
                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_9">
            <div class="pod">
                <div id="map" style="border:10px solid #CCCCCC; height:600px; width:680px;"></div>
            </div>
        </div>
    </div>
    <div class="cb"></div>

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript">
        var map = new GMap2(document.getElementById("map"));
        var geocoder = new GClientGeocoder();
        var markerGroups = { "resorts": [], "airports": [] };
        var markers = [];
        var markers_airports = [];
        var m = 0;
        var rid = 0;
        var maxx, maxy, minx, miny; //map bounds

        window.onunload = GUnload;

        $(document).ready(function() {
            showMap();
            getMapBounds();
            getAirportMarkers();
        });
        
        function showMap() {
            map.setCenter(new GLatLng(48.465151, 8.4375), 4);

            $('#map').block({ message: '<img alt="Loading ..." style="width:90%" src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
        }

        function storeAirports(Objects) {
            airports = Objects.Data;
            for (var i = 0; i < airports.length; i++) {
                var icon = new GIcon();
                icon.image = "/Static/Images/airport_off.png";
                icon.iconSize = new GSize(34, 54);
                icon.iconAnchor = new GPoint(17, 54);
                icon.infoWindowAnchor = new GPoint(17, 0);
                addAirportMarker(airports[i], icon, i);
            }
            setTimeout('$("#map").unblock();', 1000);
        }

        function addAirportMarker(airport, myIcon, i) {
            var marker = new GMarker(new GLatLng(airport.Latitude, airport.Longitude), { id: "a_mkr_" + i, icon: myIcon });
            markerGroups["airports"].push(marker);
            map.addOverlay(marker, myIcon);

            GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/static/images/airport_on.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/static/images/airport_off.png");
            });
            GEvent.addListener(marker, "click", function() {
                window.location = "/airports/" + airport.PrettyUrl;
            });

            $("img#mtgt_a_mkr_" + i).qtip({
                content: "<span class='flag-m " + airport.Code + "'>&nbsp;</span>" + airport.Name,
                position: {
                    corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
                },
                hide: {
                    fixed: true
                },
                show: { effect: 'fade' },
                style: {
                    //            name: 'blue',
                    tip: true,
                    border: {
                        width: 2,
                        radius: 5,
                        color: '#094989'
                    },
                    background: '#1E76D0',
                    color: '#ffffff'

                }
            });

            markers_airports[m] = marker;
            m++;
        }
        function getMapBounds() {
            //get visible bounds of map
            var bounds = map.getBounds();
            minx = bounds.getSouthWest().lng();
            maxx = bounds.getNorthEast().lng();
            miny = bounds.getSouthWest().lat();
            maxy = bounds.getNorthEast().lat();
        }

        function getAirportMarkers() {
            $.getJSON("/Ajax/GetAirportsByBounds", { minx: minx, miny: miny, maxx: maxx, maxy: maxy, rid: rid }, storeAirports);
        }
    </script>
</asp:Content>
