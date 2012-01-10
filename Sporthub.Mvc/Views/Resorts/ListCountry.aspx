<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.LocationName %></asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="This is a list of ski resorts in <%= (Model.Country != null) ? Model.Country.CountryName : string.Empty %>, <%= (Model.Continent != null) ? Model.Continent.ContinentName : string.Empty %>" />
<meta name="keywords" content="<%= (Model.Country != null) ? Model.Country.CountryName : string.Empty %> <%= (Model.Continent != null) ? Model.Continent.ContinentName : string.Empty %> ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
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
</ul>

</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_6">
        <h2 class="pad" style="background: transparent url(/static/images/flags/lg/24/<%= Model.Country.ISO3166Alpha2 %>.png) 0 40% no-repeat;"><%= Model.LocationName %><span>, <%=Model.Continent.ContinentName %></span></h2>
    </div>
    <div class="grid_6">
        <div class="now-viewing">Viewing Resorts in <a href="#"><%= Model.LocationName %><span></span></a></div>
        <div id="jump-to">
            <div id="jump-to-inner">
                <p>Countries in <%=Model.Continent.ContinentName%>:</p>
                <ul class="list1">
                <% foreach (var country in Model.Countries) { %>
                    <li><a href="/resorts/<%= country.PrettyUrl %>/list"><img class="flag" alt="[<%= country.ISO3166Alpha2 %>]" src="/static/images/flags/<%= country.ISO3166Alpha2 %>.png" />&nbsp;<%= country.CountryName%></a></li>
                <% } %>
                </ul>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <% var ids = string.Empty; %>
    <div class="container_12">
        
        <div class="grid_3"><p>&nbsp;</p>
        </div>
        <div class="grid_9">
            <div class="pod">
                <div class="podIn nograd">
                    <div id="map" class="country"></div>
                </div>
            </div>
            <%--<div style="margin: 20px 0 20px 30px">
                <script type="text/javascript">
                    google_ad_client = "pub-2930781007842752";
                    google_ad_width = 728;
                    google_ad_height = 90;
                    google_ad_format = "728x90_as";
                    google_ad_type = "image";
                    google_color_border = "FFFFFF";
                    google_color_bg = "0000FF";
                    google_color_link = "FFFFFF";
                    google_color_text = "000000";
                    google_color_url = "008000";
                </script><script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
            </div>--%>

        </div>
    </div>

    <div class="container_12">
        <div class="grid_3"><p>&nbsp;</p>
        </div>
        <div class="grid_9" style="margin-bottom: 20px;">
        <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                <img alt="ad" src="../../Static/Images/Ads/468x60Fullhorizontalbanner.gif" />
        <% } else { %>
            <script type="text/javascript">
                google_ad_client = "pub-2930781007842752";
                google_ad_width = 468;
                google_ad_height = 60;
                google_ad_format = "468x60_as";
                google_ad_type = "image";
                google_color_border = "FFFFFF";
                google_color_bg = "0000FF";
                google_color_link = "FFFFFF";
                google_color_text = "000000";
                google_color_url = "008000";
            </script>
            <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
        <% } %>
            <a class="big" href="/advertise" style="margin: 5px 0pt 0pt; padding: 15px 12px; float: right;">Advertise on the Snowhub</a>
        </div>
    </div>

    <div class="container_12">
        <div class="grid_3" style="margin-top: -490px; height: 600px;">
            <div class="pod">
                <div class="headwrap">
                    <h3>Resorts</h3>
                </div>
                <div class="headwrap2">
                    <h4>Most Visited Resorts</h4>
                </div>
                <% if (Model.MostVisitedResorts.Count == 0) { %>
                <div class="podIn">
                    <p style="padding-right: 10px">No users have visited any resorts in <%=Model.Country.CountryName %></p>
                </div>
                <% } else { %>
                <div class="podIn list" style="padding-bottom: 8px;">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.MostVisitedResorts) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>" class="<%= (resort.Name.Length>23) ? " smalltext" : string.Empty %>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><span class="counter"><%= resort.VisitedCount %></span></a></li>

                    <%} %>
                    </ul>
                </div>
                <% } %>
                <div class="headwrap2">
                    <h4>Highest Rated Resorts</h4>
                </div>
                <% if (Model.TopRatedResorts.Count == 0) { %>
                <div class="podIn">
                    <p style="padding-right: 10px">No resorts in <%=Model.Country.CountryName %> have been rated yet</p>
                </div>
                <% } else { %>
                <div class="podIn list">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.TopRatedResorts) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>" class="<%= (resort.Name.Length>23) ? " smalltext" : string.Empty %>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><%= Html.Score(resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null)%></a></li>

                    <%} %>
                    </ul>
                </div>
                <% } %>
                <% if (Model.SkiAreas.Count > 0) { %>
                <div class="headwrap2">
                    <h4>Ski Areas</h4>
                </div>
                <div class="podIn list">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.SkiAreas) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>" class="<%= (resort.Name.Length>23) ? " smalltext" : string.Empty %>"><%= resort.Name%></a></li>
                    <%} %>
                    </ul>
                </div>
                <% } %>
                <div class="podbtm"></div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>Latest Reviews</h3>
                </div>
                    <% if (Model.LatestReviews.Count==0) { %>
                <div class="podIn">
                    <p style="padding-right: 10px">No resorts in <%=Model.Country.CountryName %> have been rated yet</p>
                </div>
                    <%} else {%>
                <div id="l1" class="podIn list fbUsersList">
                    <% ids = string.Empty; %>
                    <ul class="list3 reviews">
                    <% var u = 0;%>
                    <% foreach (Sporthub.Model.LinkResortUser review in Model.LatestReviews) { %>
                    <% ids += string.Format("{0},", review.User.FacebookUid);%>

                        <li>
                            <a class="r-bubble-link" href="/resorts/<%= review.Resort.PrettyUrl %>/reviews/<%= review.ID %>">
                            <div class="r-bubble">
                                <div class="r-top">
                                    <span class="flag <%= review.Resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= review.Resort.Name%>
                                    <%= Html.Score(review.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null)%>
                                </div>
                                <h5 class="r-title">"<%=review.Title %>"</h5>
                            </div>
                            <span class="r-beak">&nbsp;</span>
                            </a>
                        
                            <div class="r-bottom">
                                <a class="profile sml qtu" href="/user/<%=review.User.UserName%>"><img alt="profile pic" src="<%=review.User.GetSmallProfilePic() %>" class="tnMedia"/>
                                    <div class="profileSummary">
                                        <div class="profileSummaryIn">
                                            <%=review.User.GetName() %><br />
                                            <em><%=review.User.GetSportTypes() %></em>
                                        </div>
                                    </div>
                                </a>
                                <span class="r-date"><% = review.GetCreatedTime() %></span>
                                <div class="cb"></div>
                            </div>
                        </li>

                    <% u++;%>
                    <%} %>
                    </ul>
                    <input type="hidden" id="l1-idList" name="l1-idList" class="idList" value="<%= ((ids.Length>0) ? ids.Substring(0, (ids.Length-1)) : "0") %>" />
                </div>
                    <%} %>
                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_9">
            <div class="pod">
                <div class="podIn">
                    <div id="filters">

                        <a class="all selected" href="#">ALL</a>
                        <a href="#">A</a>
                        <a href="#">B</a>
                        <a href="#">C</a>
                        <a href="#">D</a>
                        <a href="#">E</a>
                        <a href="#">F</a>
                        <a href="#">G</a>
                        <a href="#">H</a>
                        <a href="#">I</a>
                        <a href="#">J</a>
                        <a href="#">K</a>
                        <a href="#">L</a>
                        <a href="#">M</a>
                        <a href="#">N</a>
                        <a href="#">O</a>
                        <a href="#">P</a>
                        <a href="#">Q</a>
                        <a href="#">R</a>
                        <a href="#">S</a>
                        <a href="#">T</a>
                        <a href="#">U</a>
                        <a href="#">V</a>
                        <a href="#">W</a>
                        <a href="#">X</a>
                        <a href="#">Y</a>
                        <a class="last" href="#">Z</a>
                        <input id="filter-search" type="text" value="" />
                    </div>
                    <div class="cb"></div>
                    <p id="statusMessage"><%= Model.Resorts.Count %> Resorts</p>
                    <ul id="resortList" class="list1 cols">
                    <% foreach (Sporthub.Model.Resort resort in Model.Resorts) { %>
                    <% var m = 0; %>
                    <li id="<%= resort.PrettyUrl %>" class="<%= resort.Name.Substring(0,1).ToUpper() %> resort"><a href="/resorts/<%= resort.PrettyUrl %>" class="<%= (resort.Name.Length>23) ? " smalltext" : string.Empty %>"><%= resort.Name %></a><input class="name" type="hidden" id="<%= resort.PrettyUrl %>_lng" value="<%= resort.Longitude %>" /><input type="hidden" id="<%= resort.PrettyUrl %>_lat" value="<%= resort.Latitude %>" /><input type="hidden" id="<%= resort.PrettyUrl %>_name" value="<%= resort.Name %>" /></li>
                    <%--<li id="Li1" class="<%= resort.Name.Substring(0,1).ToUpper() %>"><a href="/resorts/<%= resort.PrettyUrl %>" onmouseover="resortOvr('<%= resort.PrettyUrl %>')" onmouseout="resortOut('<%= resort.PrettyUrl %>')" class="<%= (resort.Name.Length>23) ? " smalltext" : string.Empty %>"><%= resort.Name %></a><input type="hidden" id="<%= resort.PrettyUrl %>_lng" value="<%= resort.Longitude %>" /><input type="hidden" id="<%= resort.PrettyUrl %>_lat" value="<%= resort.Latitude %>" /><input type="hidden" id="<%= resort.PrettyUrl %>_name" value="<%= resort.Name %>" /></li>--%>
                    <% m++; %>
                    <% } %>
                    </ul>
                <div class="cb"></div>
                </div>
            </div>

            <%--<div class="pod">
                <% if (Model.LocationLevel == Sporthub.Model.Enumerators.LocationLevel.Continent) { %>
                <table class="table1">
                    <tr><th>Capital</th><td><%= Model.Country.Capital %></td></tr>
                    <tr><th>Currency</th><td><%= Model.Country.Currency %> (TODO:)</td></tr>
                    <tr><th>Time zone</th><td><%= Model.Country.GMT %></td></tr>
                    <tr><th>Country Code</th><td>TODO:</td></tr>
                    <tr><th>Size</th><td><%= Model.Country.Area %></td></tr>
                    <tr><th>Population</th><td><%= Model.Country.Population %></td></tr>
                    <tr><th>Highest Point</th><td><%= Model.Country.HighestPoint %> <%= Model.Country.HighestPointDescription %></td></tr>
                </table>
                <% } %>
                <div class="cb"></div>
            </div>--%>

        </div>
        <input id="LocationLevel" name="LocationLevel" type="hidden" value="<%= ((int)Model.LocationLevel).ToString() %>" /><!-- TODO: check level before populating below-->
        <input id="LocationName" name="LocationName" type="hidden" value="<%= Model.LocationName %>" />
        <input id="LocationUrl" name="LocationUrl" type="hidden" value="<%= Model.LocationUrl %>" />
        <input id="LocationID" name="LocationID" type="hidden" value="<%= Model.Country.ID %>" />
        <input id="Latitude" name="Latitude" type="hidden" value="<%= Model.Country.Latitude %>" />
        <input id="Longitude" name="Longitude" type="hidden" value="<%= Model.Country.Longitude %>" />
    </div>
    <div class="cb"></div>
    <div class="container_12">
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/dragzoom.js"></script>
<script type="text/javascript" language="javascript">

    var resortObjects;
    var markers = [];
    var markerGroups = { "panoramio": [], "flickr": [], "weather": [], "webcams": [], "wikipedia": [], "youtube": [], "resorts": [], "airports": [] };
    var markers_airports = [];
    var m = 0;
    var colCount = 0;
    //    var myimages = new Array()
//    function preloadimages() {
//        for (i = 0; i < preloadimages.arguments.length; i++) {
//            myimages[i] = new Image()
//            myimages[i].src = preloadimages.arguments[i]
//        }
//    }

//    preloadimages(
//        "/assets/images/marker_wikipedia_ovr.png"
//    );

    var map = new GMap2(document.getElementById("map"));
    var geocoder = new GClientGeocoder();
    //drag-zoom set-up
    var boxStyleOpts = { opacity: .2, border: "2px solid yellow" };
    var otherOpts = {
        buttonHTML: "<img src='/static/images/maps/zoom-control-inactive.png' />",
        buttonZoomingHTML: "<img src='/static/images/maps/zoom-control-active.png' />",
        buttonStartingStyle: { width: '17px', height: '17px' },
        overlayRemoveTime: 0
    };
    var callbacks = {};
    //drag-zoom end

    window.onunload = GUnload;

    var htmls = [];
    var i = 0;

    $(document).ready(function() {

        //        $(".resort").hover(function() {
        //            var r = $(this).next("input:hidden").val();
        //            alert(r);
        //            //resortOver();
        //        },
        //        function() {
        //        });

        $("#filter-search").keyup(function(event) {
            GetResorts("Search", $(this).val());
        });

        $("div#filters a").click(function(event) {
            $("#filter-search").val('');
            GetResorts($(this).html(), '');
        });

        getAirportMarkers(); 
        showMap();
    });

    function resortOvr(name) {
        for (var i = 0; i < markers.length; i++) {
            if (markers[i].id == name) {
                markers[i].setImage("/static/images/markers/marker_resort_small_ovr.png");
                return false;
            }
        }
    }

    function resortOut(name) {
        for (var i = 0; i < markers.length; i++) {
            if (markers[i].id == name) {
                markers[i].setImage("/static/images/markers/marker_resort_small.png")
                return false;
            }
        }
    }

    function storeResorts(Objects) {
        resortObjects = Objects;
        GetResorts("ALL", "");
    }

    function GetResorts(filter, search) {
        m = 0;
        for (var i = 0; i < markers.length; i++) {
            var marker = markers[i];
            if (marker.isHidden()) {
                marker.show();
            } else {
                marker.hide();
            }
        }
        markers = [];

        var cnt = 0;
        var mkr = 0;
        $("ul.list1 li").each(function() {
            var id = $(this).attr("id");
            if (id != "") {
                if ((filter == "ALL") || ($(this).hasClass(filter))) {
                    $(this).show();
                    var icon = new GIcon();
                    icon.image = "/static/images/markers/marker_resort_small.png";
                    icon.iconSize = new GSize(34, 54);
                    icon.iconAnchor = new GPoint(17, 54);
                    icon.infoWindowAnchor = new GPoint(17, 0);
                    var name = document.getElementById(id + "_name").value;
                    var lng = document.getElementById(id + "_lng").value;
                    var lat = document.getElementById(id + "_lat").value;
                    addResortMarker(name, id, lng, lat, icon, mkr);
                    cnt++;
                } else {
                    if (filter == "Search") {
                        var name = $(this).find("input#" + id + "_name").val().toLowerCase();
                        var re = new RegExp(search, "i");
                        if (name.match(search)) {
                            $(this).show();
                            var icon = new GIcon();
                            icon.image = "/static/images/markers/marker_resort_small.png";
                            icon.iconSize = new GSize(34, 54);
                            icon.iconAnchor = new GPoint(17, 54);
                            icon.infoWindowAnchor = new GPoint(17, 0);
                            var name = document.getElementById(id + "_name").value;
                            var lng = document.getElementById(id + "_lng").value;
                            var lat = document.getElementById(id + "_lat").value;
                            addResortMarker(name, id, lng, lat, icon, mkr);
                            cnt++;
                        } else {
                            $(this).hide();
                        }
                    } else {
                        $(this).hide();
                    }
                }
                mkr++;
            }
        });
        $("#statusMessage").html(cnt + " resorts");
//        sporthub.utility.initListLinks();
        setTimeout('$("#map").unblock();', 1000);
        if (filter == "ALL") {
            mapSetZoomLevel(markers);
        }
    }

    function addResortMarker(name, prettyUrl, lng, lat, myIcon, i) {
//        var latlng = new GLatLng(lat, lng);
//        var icon = new GIcon();
//        icon.image = 'http://uwmike.com/maps/manhattan/img/red-marker.png';
//        icon.iconSize = new GSize(32, 32);
//        icon.iconAnchor = new GPoint(16, 16);
//        opts = {
//            "icon": icon,
//            "clickable": false,
//            "draggable": false,
//            "labelText": name,
//            "labelOffset": new GSize(-16, -16)
//        };
//        var marker = new LabeledMarker(latlng, opts);

//        map.addOverlay(marker);
        var marker = new GMarker(new GLatLng(lat, lng), { id: "mkr_" + i, icon: myIcon });
        map.addOverlay(marker, myIcon);
        var t;
//        GEvent.addListener(marker, "mouseover", function() {
//            var myHtml = "<div class='centre' style='height: 60px; line-height: 60px; border-top: 10px solid #444343;'><h4>"+name+"</h4><img alt='[Loading resort]' src='/static/images/anim/loading-horiz-l.gif' /></div>";
//            t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_red", myHtml, { beakOffset: 0, width: '300px', ajaxUrl: '/Ajax/GetResortPopup?name=' + prettyUrl, ajaxCallback: 'largeInfoWinLoad' }); }, 500);
//        });
        $("img#mtgt_mkr_" + i).qtip({
            content: name,
            position: {
                corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
            },
            show: { effect: 'fade' },
            style: {
                name: 'dark',
                tip: true,
                border: {
                    width: 2,
                    radius: 5
                }
            }
        });
        GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/static/images/markers/marker_resort_small_ovr.png");
        });
        GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/static/images/markers/marker_resort_small.png");
            //clearTimeout(t);
        });
        GEvent.addListener(marker, "click", function() {
            window.location = "/resorts/" + prettyUrl;
        });

        markers[m] = marker;

        m++;
    }

    function showMap() {
        map.setCenter(new GLatLng(document.getElementById("Latitude").value, document.getElementById("Longitude").value), 6);
        map.addControl(new GScaleControl());
        map.addMapType(G_PHYSICAL_MAP);
        map.setMapType(G_PHYSICAL_MAP);
        map.addControl(new DragZoomControl(boxStyleOpts, otherOpts, {}),
                new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(7, 7)));
        map.addControl(new GLargeMapControl3D(), new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(7, 7)));

        var cid = $('LocationID').val();

        $('#map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
        $.getJSON("/Ajax/GetResorts", { level: $("#LocationLevel").val(), name: $("#LocationUrl").val() }, storeResorts);
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
            content: "<span class='flag-m " + airport.Code + "'>&nbsp;</span>" + airport.Name + " Airport",
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

    function storeAirports(Objects) {
        for (var i = 1; i < Objects.Data.length; i++) {
            var icon = new GIcon();
            icon.image = "/Static/Images/airport_off.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            addAirportMarker(Objects.Data[i], icon, i);
        }
    }

    function getAirportMarkers() {
        $.getJSON("/Ajax/GetAirportsByContinent/3", storeAirports);
    }


//    function largeInfoWinLoad() {
//        $("#example > ul").tabs();
//    }

    function LabeledMarker(latlng, options) {
        this.latlng = latlng;
        this.labelText = options.labelText || "";
        this.labelClass = options.labelClass || "markerLabel";
        this.labelOffset = options.labelOffset || new GSize(0, 0);
        GMarker.apply(this, arguments);
    }


    /* It's a limitation of JavaScript inheritance that we can't conveniently
    extend GMarker without having to run its constructor. In order for the
    constructor to run, it requires some dummy GLatLng. */
    LabeledMarker.prototype = new GMarker(new GLatLng(0, 0));


    // Creates the text div that goes over the marker.
    LabeledMarker.prototype.initialize = function(map) {
        // Do the GMarker constructor first.
        GMarker.prototype.initialize.call(this, map);

        var div = document.createElement("div");
        div.className = this.labelClass;
        div.innerHTML = this.labelText;
        div.style.position = "absolute";
        map.getPane(G_MAP_MARKER_PANE).appendChild(div);

        this.map = map;
        this.div = div;
    }

    // Redraw the rectangle based on the current projection and zoom level
    LabeledMarker.prototype.redraw = function(force) {
        GMarker.prototype.redraw.call(this, map);

        // We only need to do anything if the coordinate system has changed
        if (!force) return;

        // Calculate the DIV coordinates of two opposite corners of our bounds to
        // get the size and position of our rectangle
        var p = this.map.fromLatLngToDivPixel(this.latlng);
        var z = GOverlay.getZIndex(this.latlng.lat());

        // Now position our DIV based on the DIV coordinates of our bounds
        this.div.style.left = (p.x + this.labelOffset.width) + "px";
        this.div.style.top = (p.y + this.labelOffset.height) + "px";
        this.div.style.zIndex = z + 1; // in front of the marker
    }

    // Remove the main DIV from the map pane
    LabeledMarker.prototype.remove = function() {
        this.div.parentNode.removeChild(this.div);
        this.div = null;
        GMarker.prototype.remove.call(this);
    }
    </script>
</asp:Content>