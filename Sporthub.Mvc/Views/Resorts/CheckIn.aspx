<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.CheckInViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("CheckIn", "Resorts") %>" method="post">
    <div class="container_12">
        <div class="grid_9">
            <div class="pod">
                <div id="Map" style="height: 400px; width: 660px; margin: 10px; z-index: 0; background-color: #333;">
                </div>
                <p id="distanceText" style="display: none; font-size: 16px; margin-left: 10px;">We estimate that you are <span id="distance" style="color: #000; font-size: 16px; font-weight: bold;"></span> miles away from <span id="Span1" style="color: #000; font-size: 16px; font-weight: bold;"><%= Model.Resort.Name %></span>.</p>
                <p id="tooFarText" style="display: none; font-size: 16px;">Sorry, but you are too far away to Check-In to this resort.</p>
                <div class="cb"></div>
                <div class="podbtm"></div>
            </div>
            <div class="pod" id="submitArea" style="display:none; margin: 20px 0 40px 0;">
<%--                <table style="width: 670px; float: right;">
                <tr>

                <% if (Model.HasFacebookPublishPermission)
                   { %>                
                <td><label>Do you want to publish this Check-In to your Facebook Wall?</label></td>
                <td><label><input type="radio" name="publish" value="1" />Yes</label></td>
                <td style="width: 10px">&nbsp;</td>
                <td><label><input type="radio" name="publish" value="0" checked="checked" />No</label></td>
                <% }
                   else
                   { %>
                <td><label>You have publishing to Facebook switched off. You can change this in your Facebook Application settings</label></td>
                <% } %>
                <td style="width: 120px">&nbsp;</td>
                <td><span class="button"><button type="submit" name="checkin" id="checkin" value="CheckIn">Check-In to <%=Model.Resort.Name %>!</button></span></td>
                </tr>
                </table>--%>
                <p style="margin-left: 10px;"><span class="button"><button type="submit" name="checkin" id="checkin" value="CheckIn">Check-In to <%=Model.Resort.Name %>!</button></span></p>
                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Resort</h3>
                </div>
                <div class="podIn">
                    <p><span class="flag <%= Model.Resort.Country.ISO3166Alpha2%>" style="margin-top: 3px">&nbsp;</span><a href="/resorts/<%= Model.Resort.PrettyUrl %>"><%= Model.Resort.Name.ToString() %></a></p>
                </div>
                <div class="podbtm"></div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>Guide to Checking-In</h3>
                </div>
                <div class="podIn" style="padding-right:10px">
                    <p class="bull">You (<img alt="*" src="/Static/Images/Markers/marker_red_mini.png" />) must be located within 50 miles of a resort (<img alt="*" src="/Static/Images/Markers/marker_green_mini.png" />) to be able to Check-In (shown by the green circle around the resort on the map).</p>
                    <p class="bull">The method we use to estimate your location can be hit-and-miss so we apologise if we get it wrong; however, we are working to improve the accuracy as soon as we can.</p>
                    <p class="bull">While you are in the resort try to check-in every few days to confirm you are still there.</p>
                    <p class="bull">If a week passes since a Check-In then you will be automatically Checked-Out.</p>
                </div>
                <div class="podbtm"></div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>Why Check-In at a Resort?</h3>
                </div>
                <div class="podIn" style="padding-right:10px">
                    <p class="bull">You may be able to meet up with other Snowhub members.</p>
                    <p class="bull">You could help other members by giving on-the-spot reports, posting pictures or answering queries.</p>
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
    </div>

<input id="mode" name="mode" type="hidden" value="<%= Model.IsUpdate.ToString() %>" />
<input id="hidResortID" name="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidPrettyUrl" name="hidPrettyUrl" type="hidden" value="<%= Model.Resort.PrettyUrl %>" />
<input id="hidResortName" name="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" name="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" name="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="myLat" name="myLat" type="hidden" value="0" />
<input id="myLng" name="myLng" type="hidden" value="0" />
<input id="hidCountryCode" name="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />
<input id="hidReturnUrl" name="hidReturnUrl" type="hidden" value="<%= Model.ReturnUrl %>" />
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Check In</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Check-In</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            sporthub.ratereview.onLoad();
        });
        
    </script>
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
    <script type="text/javascript">
        var zoom_lvl = 10;
        var lg_map = false;
        var fetch_all = false;
    </script>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="http://j.maxmind.com/app/geoip.js"></script>
    <script type="text/javascript" language="javascript">

        var map;
        var geocoder;
        var maxx, maxy, minx, miny; //map bounds
        var maxRows = 50; //max no of items in json request
        var markers = [];
        var myLat = 0;
        var myLng = 0;
        var m = 0; //marker count

// This file adds a new circle overlay to GMaps2
// it is really a many-pointed polygon, but look smooth enough to be a circle.
var CircleOverlay = function(latLng, radius, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity) {
    this.latLng = latLng;
    this.radius = radius;
    this.strokeColor = strokeColor;
    this.strokeWidth = strokeWidth;
    this.strokeOpacity = strokeOpacity;
    this.fillColor = fillColor;
    this.fillOpacity = fillOpacity;
}

// Implements GOverlay interface
CircleOverlay.prototype = new GOverlay;

CircleOverlay.prototype.initialize = function(map) {
    this.map = map;
}

CircleOverlay.prototype.clear = function() {
    if(this.polygon != null && this.map != null) {
        this.map.removeOverlay(this.polygon);
    }
}

// Calculate all the points and draw them
CircleOverlay.prototype.redraw = function(force) {
    var d2r = Math.PI / 180;
    circleLatLngs = new Array();
    var circleLat = this.radius * 0.014483;  // Convert statute miles into degrees latitude
    var circleLng = circleLat / Math.cos(this.latLng.lat() * d2r);
    var numPoints = 40;
   
    // 2PI = 360 degrees, +1 so that the end points meet
    for (var i = 0; i < numPoints + 1; i++) {
        var theta = Math.PI * (i / (numPoints / 2));
        var vertexLat = this.latLng.lat() + (circleLat * Math.sin(theta));
        var vertexLng = this.latLng.lng() + (circleLng * Math.cos(theta));
        var vertextLatLng = new GLatLng(vertexLat, vertexLng);
        circleLatLngs.push(vertextLatLng);
    }
   
    this.clear();
    this.polygon = new GPolygon(circleLatLngs, this.strokeColor, this.strokeWidth, this.strokeOpacity, this.fillColor, this.fillOpacity);
    this.map.addOverlay(this.polygon);
}

CircleOverlay.prototype.remove = function() {
    this.clear();
}

CircleOverlay.prototype.containsLatLng = function(latLng) {
    // Polygon Point in poly
    if(this.polygon.containsLatLng) {
        return this.polygon.containsLatLng(latLng);
    }
}

CircleOverlay.prototype.setRadius = function(radius) {
    this.radius = radius;
}

CircleOverlay.prototype.setLatLng = function(latLng) {
    this.latLng = latLng;
}

$(document).ready(function() {
    map = new GMap2(document.getElementById("Map"));

    geocoder = new GClientGeocoder();
    map.setCenter(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), zoom_lvl);
    map.addMapType(G_PHYSICAL_MAP);
    map.setMapType(G_PHYSICAL_MAP);

    addMainResortMarker();
    addYouAreHereMarker();
});

        function toRad(deg) {
            return deg * Math.PI / 180;

        }

        function addMainResortMarker() {
            var icon = new GIcon();
            icon.image = "/Static/Images/Markers/marker_green_mini.png";
            icon.iconSize = new GSize(22,22);
            icon.iconAnchor = new GPoint(11,11);
            //    icon.infoWindowAnchor = new GPoint(20, 10);
            var marker = new GMarker(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), { id: "mkr_0", icon: icon });
            map.addOverlay(marker, icon);
            GEvent.addListener(marker, "mouseover", function() {
                marker.setImage("/static/images/markers/marker_green_med_ovr.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
                marker.setImage("/static/images/markers/marker_green_med.png");
            });

            $("img#mtgt_mkr_0").qtip({
                content: "<span class='flag-m " + document.getElementById("hidCountryCode").value + "'>&nbsp;</span>" + document.getElementById("hidResortName").value,
                position: {
                    corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
                },
                show: { effect: 'fade' },
                style: {
                    name: 'dark',
                    tip: true,
                    border: {
                        width: 1,
                        radius: 3
                    }
                }
            });
            markers[0] = marker;
        }
        
        function addYouAreHereMarker() {
            var circle = null;
            var circleRadius = 50; // Miles

            myLat = geoip_latitude();
            myLng = geoip_longitude();

            $("#myLat").val(myLat);
            $("#myLng").val(myLng);
            
            // Create and add the circle
            circle = new CircleOverlay(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), circleRadius, "#2AB800", 1, 1, '#2AB800', 0.25);
            map.addOverlay(circle);

            var icon = new GIcon();
            icon.image = "/Static/Images/Markers/marker_red_mini.png";
            icon.iconSize = new GSize(22,22);
            icon.iconAnchor = new GPoint(11,11);
            var marker = new GMarker(new GLatLng(myLat, myLng), { id: "mkr_1", icon: icon });
            map.addOverlay(marker, icon);
            GEvent.addListener(marker, "mouseover", function() {
                marker.setImage("/Static/Images/Markers/marker_red_med_ovr.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
                marker.setImage("/Static/Images/Markers/marker_red_med.png");
            });
            markers[1] = marker;

            show: { ready: true }
            $("img#mtgt_mkr_1").qtip({
                content: "You",
                position: {
                    corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
                },
                show: { ready: true },
                style: {
                    name: 'dark',
                    tip: true,
                    border: {
                        width: 1,
                        radius: 3
                    }
                }
            });
            
            
            
            var bounds = new GLatLngBounds;
            for (var i = 0; i < markers.length; i++) {
                bounds.extend(markers[i].getLatLng());
            }
            var zoom = map.getBoundsZoomLevel(bounds);
            zoom = zoom - 2;
            map.setZoom(zoom);
            map.panTo(bounds.getCenter());

            //get distance between two points
            var resortLat = document.getElementById("hidLat").value;
            var resortLng = document.getElementById("hidLng").value;
            var R = 6371; // km
            var dLat = toRad(myLat - resortLat);
            var dLon = toRad(myLng - resortLng);
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                Math.cos(toRad(myLat)) * Math.cos(toRad(resortLat)) *
                Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;
            //convert to miles and round down
            var miles = ((d * 0.621)).toFixed(0);

            $("#distanceText").css("display", "block");
            $("#distance").html(miles);
            if (miles > circleRadius) {
                $("#submitArea").css("display", "none");
                $("#tooFarText").css("display", "block");
            }
            else {
                $("#submitArea").css("display", "block");
            }
            
        }

    </script>

</asp:Content>
