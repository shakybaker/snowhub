<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AirportViewViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="Snowhub : <%= Model.Airport.Name %> (<%= Model.Airport.Code %>) is an airport in Europe which serves many popular ski resorts. European Airports close to ski resorts. Flying to the Alps, skiing and snowboarding in Europe" />
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
    <div class="grid_12"><h2 class="pad" style="background: transparent url(/static/images/flags/lg/24/<%= Model.Airport.Country.ISO3166Alpha2 %>.png) 0 37% no-repeat"><%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)<span>, <%= Model.Airport.Country.CountryName %></span></h2></div>
</div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_2">
            <div class="pod">
                <div class="headwrap">
                    <h3>Info</h3>
                </div>
                <div class="podIn">
                    <table class="table1 nobord">
                    <tbody><tr><th style="width: 80%;">IATA Code</th><td><%=Model.Airport.Code %></td></tr>
                    <tr><th>Website</th><td><a href="<%=Model.Airport.Website%>" target="_blank">Link</a></td></tr>
                    </tbody></table>
                    <div class="cb"></div>
                </div>
            </div>

<%--        <% if (Model.Airport.NearbyResorts.Count > 0) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Nearby Resorts</h3>
            </div>
            <div class="podIn">
                <ul class="list1">
                <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.NearbyResorts) { %>
                <li><a href="/resorts/<%=nearbyResort.PrettyUrl %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%=nearbyResort.Name %> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
                <% } %>
                </ul> 
                <p style="padding: 0 0 0 5px;"><em>Distances are "As the crow flies"</em></p>
            </div>
        </div>
        <% } %>--%>

        </div>
        <div class="grid_10">
            <div class="pod">
                <div id="map" style="border:10px solid #ddd; height:520px; width:760px;"></div>
            </div>
        </div>
    </div>
    <div class="cb"></div>
    <p style="text-align: center; margin: 0;"><em>Distances are "As the crow flies"</em></p>
    <div class="cb"></div>
    <% if (Model.Airport.ResortsWithinOneHour.Count>0) { %>
    <div class="container_12">
        <div class="grid_12">
            <h4 style="width: 100%; float: left; clear: both; margin-top: 10px;">Resorts within 1 hour of <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</h4>
            <ul id="resortList" class="list1 cols">
            <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.ResortsWithinOneHour) { %>
            <li id="<%= nearbyResort.PrettyUrl %>" class="<%= nearbyResort.Name.Substring(0,1).ToUpper() %> resort" style="width: 232px"><a href="/resorts/<%= nearbyResort.PrettyUrl %>" class="<%= (nearbyResort.Name.Length>21) ? " smalltext" : string.Empty %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= nearbyResort.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
            <% } %>
            </ul>
        </div>
    </div>
    <% } %>
    <% if (Model.Airport.ResortsWithinOneAndAHalfHours.Count > 0) { %>
    <div class="cb"></div>
    <div class="container_12">
        <div class="grid_12">
            <h4 style="width: 100%; float: left; clear: both; margin-top: 10px;">Resorts within 1.5 hours of <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</h4>
            <ul id="Ul1" class="list1 cols">
            <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.ResortsWithinOneAndAHalfHours) { %>
            <li id="Li1" class="<%= nearbyResort.Name.Substring(0,1).ToUpper() %> resort" style="width: 232px"><a href="/resorts/<%= nearbyResort.PrettyUrl %>" class="<%= (nearbyResort.Name.Length>21) ? " smalltext" : string.Empty %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= nearbyResort.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
            <% } %>
            </ul>
        </div>
    </div>
    <% } %>
    <% if (Model.Airport.ResortsWithinTwoHour.Count > 0)
       { %>
    <div class="cb"></div>
    <div class="container_12">
        <div class="grid_12">
            <h4 style="width: 100%; float: left; clear: both; margin-top: 10px;">Resorts within 2 hours of <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</h4>
            <ul id="Ul2" class="list1 cols">
            <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.ResortsWithinTwoHour) { %>
            <li id="Li2" class="<%= nearbyResort.Name.Substring(0,1).ToUpper() %> resort" style="width: 232px"><a href="/resorts/<%= nearbyResort.PrettyUrl %>" class="<%= (nearbyResort.Name.Length>21) ? " smalltext" : string.Empty %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= nearbyResort.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
            <% } %>
            </ul>
        </div>
    </div>
    <% } %>
    <% if (Model.Airport.ResortsWithinTwoAndAHalfHours.Count > 0)
       { %>
    <div class="cb"></div>
    <div class="container_12">
        <div class="grid_12">
            <h4 style="width: 100%; float: left; clear: both; margin-top: 10px;">Resorts within 2.5 hours of <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</h4>
            <ul id="Ul3" class="list1 cols">
            <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.ResortsWithinTwoAndAHalfHours) { %>
            <li id="Li3" class="<%= nearbyResort.Name.Substring(0,1).ToUpper() %> resort" style="width: 232px"><a href="/resorts/<%= nearbyResort.PrettyUrl %>" class="<%= (nearbyResort.Name.Length>21) ? " smalltext" : string.Empty %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= nearbyResort.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
            <% } %>
            </ul>
        </div>
    </div>
    <% } %>
    <% if (Model.Airport.ResortsFurtherThanTwoAndAHalfHours.Count > 0)
       { %>
    <div class="cb"></div>
    <div class="container_12">
        <div class="grid_12">
            <h4 style="width: 100%; float: left; clear: both; margin-top: 10px;">Resorts further than 2.5 hours from <%= Model.Airport.Name %> Airport (<%= Model.Airport.Code %>)</h4>
            <ul id="Ul4" class="list1 cols">
            <% foreach (Sporthub.Model.Resort nearbyResort in Model.Airport.ResortsFurtherThanTwoAndAHalfHours)
               { %>
            <li id="Li4" class="<%= nearbyResort.Name.Substring(0,1).ToUpper() %> resort" style="width: 232px"><a href="/resorts/<%= nearbyResort.PrettyUrl %>" class="<%= (nearbyResort.Name.Length>21) ? " smalltext" : string.Empty %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= nearbyResort.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
            <% } %>
            </ul>
        </div>
    </div>
    <% } %>
<input id="hidAirportID" type="hidden" value="<%= Model.Airport.ID.ToString() %>" />
<input id="hidAirportName" type="hidden" value="<%= Model.Airport.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Airport.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Airport.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Airport.Country.ISO3166Alpha2.ToString() %>" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        var zoom_lvl = 7;
        var lg_map = false;
        var fetch_all = true;
    </script>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript">
        var map = new GMap2(document.getElementById("map"));
        var geocoder = new GClientGeocoder();
        var markerGroups = { "resorts": [], "airports": [] };
        var markers = [];
        var markers_airports = [];
        var markers_resorts = [];
        var m = 0;
        var rid = 0;
        var maxx, maxy, minx, miny; //map bounds

        window.onunload = GUnload;

        $(document).ready(function() {
            showMap();
            getMapBounds();
            getAirportMarkers();
            getResortMarkers();
        });
        
        function showMap() {
            map.setCenter(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), zoom_lvl);

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
    map.setCenter(new GLatLng(), zoom_lvl);

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

        function storeResorts(Objects) {
            resorts = Objects.Data;
            for (var i = 1; i < resorts.length; i++) {
                var icon = new GIcon();
                icon.image = "/Static/Images/Markers/marker_resort_small.png";
                icon.iconSize = new GSize(34, 54);
                icon.iconAnchor = new GPoint(17, 54);
                icon.infoWindowAnchor = new GPoint(17, 0);
                addResortMarker(resorts[i], icon, i);
            }
        }

        function addResortMarker(resort, myIcon, i) {
            var marker = new GMarker(new GLatLng(resort.Latitude, resort.Longitude), { id: "mkr_" + i, icon: myIcon });
            markerGroups["resorts"].push(marker);
            map.addOverlay(marker, myIcon);

            GEvent.addListener(marker, "mouseover", function() {
                marker.setImage("/static/images/markers/marker_resort_small_ovr.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
                marker.setImage("/static/images/markers/marker_resort_small.png");
            });
            GEvent.addListener(marker, "click", function() {
                window.location = "/resorts/" + resort.PrettyUrl;
            });

            $("img#mtgt_mkr_" + i).qtip({
                content: "<span class='flag-m " + resort.Code + "'>&nbsp;</span>" + resort.Name,
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

            markers_resorts[m] = marker;
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

        function getResortMarkers() {
            $.getJSON("/Ajax/GetResortsByBounds", { minx: minx, miny: miny, maxx: maxx, maxy: maxy, rid: rid }, storeResorts);
        }

        function getAirportMarkers() {
            $.getJSON("/Ajax/GetAirportsByBounds", { minx: minx, miny: miny, maxx: maxx, maxy: maxy, rid: rid }, storeAirports);
        }
    </script>
</asp:Content>
