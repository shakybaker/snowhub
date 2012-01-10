window.onunload = GUnload;

sporthub.maps = {
    map: null,
    geocoder: null,
    maxx: 0,
    maxy: 0,
    minx: 0,
    miny: 0,
    longitude: 0,
    latitude: 0,
    resortName: "",
    countryCode: "",
    maxRows: 50, //max no of items in json request
    markers: [],
    markerGroups: { "panoramio": [], "flickr": [], "weather": [], "webcams": [], "wikipedia": [], "youtube": [], "resorts": [], "airports": [] },
    markers_panoramio: [],
    markers_flickr: [],
    markers_youtube: [],
    markers_weather: [],
    markers_webcams: [],
    markers_wikipedia: [],
    markers_resorts: [],
    markers_airports: [],
    htmls: [],
    m: 0, //marker count
    rid: 0, //resort id
    qs: sporthub.utility.getQueryString("m"),
    boxStyleOpts: { opacity: .2, border: "2px solid yellow" },
    otherOpts: {
        buttonHTML: "<img src='/assets/images/zoom-control-inactive.png' />",
        buttonZoomingHTML: "<img src='/assets/images/zoom-control-active.png' />",
        buttonStartingStyle: { width: '17px', height: '17px' },
        overlayRemoveTime: 0
    },

    
    onLoad: function() {
        sporthub.maps.rid = $("#hidResortID").val();
        sporthub.maps.latitude = $("#hidLat").val();
        sporthub.maps.longitude = $("#hidLng").val();
        sporthub.maps.resortName = $("#hidResortName").val();
        sporthub.maps.countryCode = $("#hidCountryCode").val();

        if (lg_map) {
            sporthub.maps.loadLargeMap();
        }

        sporthub.maps.blockMapUI();
        sporthub.maps.showMap();
        sporthub.maps.addMainResortMarker();
        sporthub.maps.getMapBounds();
        
        if (lg_map) {
            if (document.getElementById("cbResorts").checked) {
                sporthub.maps.getResortMarkers();
                sporthub.maps.getAirportMarkers();
            }
            if ((document.getElementById("cbFlickr").checked) || (sporthub.maps.qs == "flickr")) {
                sporthub.maps.getFlickrMarkers(100);
            }
            if ((document.getElementById("cbYoutube").checked) || (sporthub.maps.qs == "youtube")) {
                sporthub.maps.getYoutubeMarkers();
            }
//            if ((document.getElementById("cbWikipedia").checked) || (sporthub.maps.qs == "wikipedia")) {
//                !!!!!!!!!!!!!!getWikipediaMarkers();
//            }
//            if ((document.getElementById("cbWebcams").checked) || (sporthub.maps.qs == "webcams")) {
//                !!!!!!!!!!!!!!getWebcamMarkers();
//            }
        }
        else {
            if (fetch_all) {
                sporthub.maps.getResortMarkers();
                sporthub.maps.getAirportMarkers();
            }
        }

        $('.toggleMarkers').click(function() {
            if ($('#markerNav').css('display') == 'block') {
                $('#markerNav').hide();
            }
            else {
                $('#markerNav').slideDown();
            }
            return false;
        });
        
        $('.checkbox').click(function() {
            var markerGroup = $(this).parent().find("span:first").attr("class");
            if ($(this).is(':checked')) {//note: at this point the 'checked' state will be the oppposite
                $(this).parent().removeClass('off');
                $(this).parent().addClass('on');
                setTimeout(function() {
                    sporthub.maps.blockMapUI();
                    sporthub.maps.getMapBounds();
                    switch(markerGroup)
                    {
                        case "resorts":
                            sporthub.maps.getResortMarkers();
                            sporthub.maps.getAirportMarkers();
                        break;
                        case "flickr":
                            sporthub.maps.getFlickrMarkers(50);
                        break;
                        case "youtube":
                            sporthub.maps.getYoutubeMarkers();
                        break;
                        case "wikipedia":
                            sporthub.maps.getWikipediaMarkers();
                        break;
                        case "webcams":
                            sporthub.maps.getWebcamMarkers();
                        break;
                    }
                }, 50);
                setTimeout('$("#Map").unblock();', 1000);
            }
            else {
                $(this).parent().removeClass('on');
                $(this).parent().addClass('off');
                sporthub.maps.hideMarkerGroup(markerGroup);
            }
        });

    },
    loadLargeMap: function() {
        $("#Map").css("height", $(window).height() - 240);
        $('.markerNavHead')
            .filter(':has(:checkbox:checked)')
            .addClass('checked')
            .end()
            .click(function(event) {
                $(this).toggleClass('checked');
                if (event.target.type !== 'checkbox') {
                  $(':checkbox', this).trigger('click');
                }
            });
    },
    blockMapUI: function() {
        $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
    },
    showMap: function() {
        sporthub.maps.map = new GMap2(document.getElementById("Map"));

        if (lg_map) {
            if ((window.location.href.indexOf("localhost") == -1) && (window.location.href.indexOf("127.0.0.1") == -1)) {
                var publisher_id = "pub-2930781007842752";

                var adsManagerOptions = {
                    maxAdsOnMap: 2,
                    style: 'adunit'
                    // The channel field is optional - replace this field with a channel number 
                    // for Google AdSense tracking
                    //channel: 'your_channel_id'
                };

                adsManager = new GAdsManager(map, publisher_id, adsManagerOptions);
                adsManager.enable();
            }
        }
        sporthub.maps.geocoder = new GClientGeocoder();
        sporthub.maps.map.setCenter(new GLatLng(sporthub.maps.latitude, sporthub.maps.longitude), zoom_lvl);

        if (lg_map) {
            sporthub.maps.map.addControl(new GScaleControl());
            sporthub.maps.map.addMapType(G_PHYSICAL_MAP);
            sporthub.maps.map.setMapType(G_PHYSICAL_MAP);
            sporthub.maps.map.addControl(new DragZoomControl(sporthub.maps.boxStyleOpts, sporthub.maps.otherOpts, {}),
                new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(7, 7)));
            sporthub.maps.map.addControl(new GLargeMapControl3D(), new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(7, 7)));
        }
        else {
            sporthub.maps.map.addControl(new GOverviewMapControl());
        }
        sporthub.maps.map.setMapType(G_PHYSICAL_MAP);
    },
    addMainResortMarker: function() {
        var icon = new GIcon();
        icon.image = "/Static/Images/Markers/resort_l_off.png";
        icon.iconSize = new GSize(34, 54);
        icon.iconAnchor = new GPoint(17, 54);
        icon.infoWindowAnchor = new GPoint(17, 0);
        var marker = new GMarker(new GLatLng(sporthub.maps.latitude, sporthub.maps.longitude), { id: "mkr_0", icon: icon });
        sporthub.maps.map.addOverlay(marker, icon);
        GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/static/images/markers/resort_l_ovr.png");
        });
        GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/static/images/markers/resort_l_off.png");
        });

        $("img#mtgt_mkr_0").qtip({
            content: "<span class='flag-m " + sporthub.maps.countryCode + "'>&nbsp;</span>" + sporthub.maps.resortName,
            position: {
                corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
            },
            hide: {
                fixed: true
            },
            show: { effect: 'fade' },
            style: {
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
        sporthub.maps.markers[0] = marker;
    },
    getMapBounds: function() {
        var bounds = sporthub.maps.map.getBounds();
        sporthub.maps.minx = bounds.getSouthWest().lng();
        sporthub.maps.maxx = bounds.getNorthEast().lng();
        sporthub.maps.miny = bounds.getSouthWest().lat();
        sporthub.maps.maxy = bounds.getNorthEast().lat();
    },
    hideMarkerGroup: function(type) {
        for (var i = 0; i < sporthub.maps.markerGroups[type].length; i++) {
            var marker = sporthub.maps.markerGroups[type][i];
            if (marker.isHidden()) {
                marker.show();
            } else {
                marker.hide();
            }
        }
    },
    getResortMarkers: function() {
        $.getJSON("/Ajax/GetResortsByBounds", { minx: sporthub.maps.minx, miny: sporthub.maps.miny, maxx: sporthub.maps.maxx, maxy: sporthub.maps.maxy, rid: sporthub.maps.rid }, sporthub.maps.storeResorts);
    },
    getAirportMarkers: function () {
        $.getJSON("/Ajax/GetAirportsByBounds", { minx: sporthub.maps.minx, miny: sporthub.maps.miny, maxx: sporthub.maps.maxx, maxy: sporthub.maps.maxy, rid: sporthub.maps.rid }, sporthub.maps.storeAirports);
    },
    getFlickrMarkers: function(maxRows) {
        //$("#mediaWrap").show();
        var feedSrc = 'http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=6db84f88528c58eac21924761c19e7a6&min_taken_date=2005-01-01&bbox=' + sporthub.maps.minx + '%2C' + sporthub.maps.miny + '%2C' + sporthub.maps.maxx + '%2C' + sporthub.maps.maxy + '&extras=geo&content_type=1&format=json&nojsoncallback=1&sort=interestingness-desc&per_page=' + maxRows + '&page=1&jsoncallback=sporthub.maps.storeFlickr';
        sporthub.maps.getMarkers(feedSrc, maxRows);
    },
    getYoutubeMarkers: function() {
        var feedSrc = 'http://gdata.youtube.com/feeds/api/videos?location=' + sporthub.maps.latitude + ',' + sporthub.maps.longitude + '!&location-radius=20km&alt=json-in-script&callback=sporthub.maps.storeYoutube';
        sporthub.maps.getMarkers(feedSrc, 0);
    },
    getMarkers: function(feedSrc, maxRows) {
        var scrYoutube = document.createElement('script');
        scrYoutube.src = feedSrc;
        var scr = document.getElementById('Map');
        scr.parentNode.insertBefore(scrYoutube, scr);
    },
    storeAirports: function(Objects) {
        airports = Objects.Data;
        for (var i = 999; i < airports.length; i++) {
            var icon = new GIcon();
            icon.image = "/Static/Images/airport_off.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            sporthub.maps.addAirportMarker(airports[i], icon, i);
        }
    },
    storeResorts: function(Objects) {
        var resorts = Objects.Data;
        for (var i = 1; i < resorts.length; i++) {
            var icon = new GIcon();
            icon.image = "/Static/Images/Markers/marker_resort_small.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            sporthub.maps.addResortMarker(resorts[i], icon, i);
        }
    },
    storeFlickr: function (flickr) {
        if (flickr.stat != "ok") {
            // something broke!
            return;
        }
        var target = document.getElementById("targetFlickr");
        if (flickr.photos.photo.length == 0) {
            target.innerHTML = "<p>No pictures are available on Flickr</p>";
        }
        else {
            target.innerHTML = "";
            for (var i = 0; i < flickr.photos.photo.length; i++) {
                var pic = flickr.photos.photo[i];

                var icon = new GIcon();
                icon.image = "/static/images/markers/marker_flickr.png";
                icon.iconSize = new GSize(34, 54);
                icon.iconAnchor = new GPoint(17, 54);
                icon.infoWindowAnchor = new GPoint(17, 0);
                sporthub.maps.addFlickrMarker(flickr.photos.photo[i], icon, i);
            }
        }
    },
    storeYoutube: function(youtube) {
        sporthub.maps.m = 0;!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!check this for all methods
        var feed = youtube.feed;
        var entries = feed.entry || [];
        for (var i = 0; i < entries.length; i++) {
            var icon = new GIcon();
            icon.image = "/static/images/markers/marker_youtube.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);

            addYoutubeMarker(entries[i], icon, i);
        }
    },
    addAirportMarker: function(airport, myIcon, i) {
        var marker = new GMarker(new GLatLng(airport.Latitude, airport.Longitude), { id: "a_mkr_" + i, icon: myIcon });
        sporthub.maps.markerGroups["airports"].push(marker);
        sporthub.maps.map.addOverlay(marker, myIcon);

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

        sporthub.maps.markers_airports[m] = marker;
        sporthub.maps.m++;
    },
    addResortMarker: function(resort, myIcon, i) {
        var marker = new GMarker(new GLatLng(resort.Latitude, resort.Longitude), { id: "mkr_" + i, icon: myIcon });
        sporthub.maps.markerGroups["resorts"].push(marker);
        sporthub.maps.map.addOverlay(marker, myIcon);
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
        sporthub.maps.markers_resorts[sporthub.maps.m] = marker;
        sporthub.maps.m++;
    },
    addFlickrMarker: function(pic, myIcon, i) {
        var marker = new GMarker(new GLatLng(pic.latitude, pic.longitude), { id: "mkr_flickr_" + i, icon: myIcon });
        sporthub.maps.markerGroups["flickr"].push(marker);
        var t;
        sporthub.maps.map.addOverlay(marker, myIcon);
        GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/static/images/markers/marker_flickr_ovr.png");
        });
        GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/static/images/markers/marker_flickr.png");
            clearTimeout(t);
        });
        GEvent.addListener(marker, "click", function() {
            window.open("http://www.flickr.com/photos/" + pic.owner + "/" + pic.id);
        });
        var myHtml = "<div class='infoWinWebcams'><table class='popupTable'><tr><td><img class='infoWinPhoto' src='http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + "_t.jpg?v=0' height='75px' width='75px' /></td><td class='tit'><a href=\"http://www.flickr.com/photos/" + pic.owner + "/" + pic.id + "\" alt=\"click to view original in a new window\" target=\"_blank\">&quot;" + pic.title + "&quot;</a></td></tr></table></div>";
        $("img#mtgt_mkr_flickr_" + i).qtip({
            content: myHtml,
            position: {
                corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
            },
            hide: {
                fixed: true
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
        sporthub.maps.markers_flickr[sporthub.maps.m] = marker;
        sporthub.maps.m++;
    }
};