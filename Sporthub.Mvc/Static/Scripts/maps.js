var map;
var geocoder;
var maxx, maxy, minx, miny; //map bounds
var maxRows = 50; //max no of items in json request
var markers = [];
var markerGroups = { "panoramio": [], "flickr": [], "weather": [], "webcams": [], "wikipedia": [], "youtube": [], "resorts": [], "airports": [] };
var markers_panoramio = [];
var markers_flickr = [];
var markers_youtube = [];
var markers_weather = [];
var markers_webcams = [];
var markers_wikipedia = [];
var markers_resorts = [];
var markers_airports = [];
var htmls = [];
var m = 0; //marker count
var rid = 0;
var qs = sporthub.utility.getQueryString("m");
//drag-zoom set-up
var boxStyleOpts = { opacity: .2, border: "2px solid yellow" };
var otherOpts = {
    buttonHTML: "<img src='/assets/images/zoom-control-inactive.png' />",
    buttonZoomingHTML: "<img src='/assets/images/zoom-control-active.png' />",
    buttonStartingStyle: { width: '17px', height: '17px' },
    overlayRemoveTime: 0
};
var callbacks = {
//    buttonclick: function() { GLog.write("") },
//    dragstart: function() { GLog.write("") },
//    dragging: function() { GLog.write("") },
//    dragend: function() { GLog.write("") }
};
//drag-zoom end

//window.onresize = showMap();
window.onunload = GUnload;

$(document).ready(function() {

    rid = document.getElementById("hidResortID").value;

    if (lg_map) {
        $("#Map").css("height", $(window).height() - 240);
    }

    $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });


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

    showMap();
    addMainResortMarker();
    getMapBounds();

    if (lg_map) {
        if (document.getElementById("cbResorts").checked) {
            getResortMarkers();
            getAirportMarkers();
        }
        //        if (document.getElementById("cbPanoramio").checked) || ()) {
        //            getPanoramioMarkers();
        //        }
        if ((document.getElementById("cbFlickr").checked) || (qs == "flickr")) {
            getFlickrMarkers(100);
        }
        if ((document.getElementById("cbYoutube").checked) || (qs == "youtube")) {
            getYoutubeMarkers();
        }
        if ((document.getElementById("cbWikipedia").checked) || (qs == "wikipedia")) {
            getWikipediaMarkers();
        }
        if ((document.getElementById("cbWebcams").checked) || (qs == "webcams")) {
            getWebcamMarkers();
        }
    }
    else {

        if (fetch_all) {
            getResortMarkers();
            getAirportMarkers();
        }
    }


    //$('#markerNav').slideToggle(400);

    //    $(".toggleMarkers").click(function() {
    //        if ($("#markerNav").css("display") == "none") {
    //            $("#markerNav").slideDown();
    //        }
    //        else {
    //            $("#markerNav").slideUp();
    //        }
    //    });
    $('.toggleMarkers').click(function() {
        if ($('#markerNav').css('display') == 'block') {
            $('#markerNav').hide();
        }
        else {
            $('#markerNav').slideDown();
        }
        return false;
    });
    $('#cbPanoramio').click(function() {
        if ($(this).is(':checked')) {//note: at this point the 'checked' state will be the oppposite
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                getMapBounds();
                getPanoramioMarkers();
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('panoramio');
        }
    });
    $('#cbResorts').click(function() {
        if ($(this).is(':checked')) {
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                getMapBounds();
                getResortMarkers();
                getAirportMarkers();
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('resorts');
        }
    });
    $('#cbFlickr').click(function() {
        if ($(this).is(':checked')) {
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                getMapBounds();
                getFlickrMarkers(50);
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('flickr');
        }
    });
    $('#cbYoutube').click(function() {
        if ($(this).is(':checked')) {
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                getMapBounds();
                getYoutubeMarkers();
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('youtube');
        }
    });
    $('#cbWikipedia').click(function() {
        if ($(this).is(':checked')) {
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                //                $.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting entries from <strong style="color: #003970;">Wikipedia</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getWikipediaMarkers();
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('wikipedia');
        }
    });
    $('#cbWebcams').click(function() {
        if ($(this).is(':checked')) {
            $(this).parent().removeClass('off');
            $(this).parent().addClass('on');
            setTimeout(function() {
                $('#Map').block({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
                getMapBounds();
                getWebcamMarkers();
            }, 50);
            setTimeout('$("#Map").unblock();', 1000);
        }
        else {
            $(this).parent().removeClass('on');
            $(this).parent().addClass('off');
            hideMarkerGroup('webcams');
        }
    });
});

function showMap() {
    map = new GMap2(document.getElementById("Map"));

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
    geocoder = new GClientGeocoder();
    map.setCenter(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), zoom_lvl);
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
    if (lg_map) {
        //map.addControl(new GOverviewMapControl());
        map.addControl(new GScaleControl());
        map.addMapType(G_PHYSICAL_MAP);
        map.setMapType(G_PHYSICAL_MAP);
        map.addControl(new DragZoomControl(boxStyleOpts, otherOpts, {}),
            new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(7, 7)));
        map.addControl(new GLargeMapControl3D(), new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(7, 7)));
    }
    map.setMapType(G_PHYSICAL_MAP);

//    if (lg_map) {
//        setTimeout('$.unblockUI()', 1000);
//        $("#mediaWrap").show();

//        setTimeout(function() {
//            $.blockUI({ message: '<img alt="Loading ..." src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
//            //$.blockUI({ message: '<img alt="[loading ...]" src="/static/images/anim/loading-horiz-l.gif" />' });
//            getFlickrMarkers(20);
//        }, 50);
//        setTimeout('$.unblockUI()', 1000);
//    }
}

function storeYoutube(youtube) {
    m = 0;
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
}

function addYoutubeMarker(video, icon, i) {
    var title = video.title.$t;
    var thumbnailUrl = video.media$group.media$thumbnail[0].url;
    var playerUrl = video.media$group.media$content[0].url;
    var author = video.author[0].name.$t;
    var authorUrl = video.author[0].uri.$t;
    var blah = video.georss$where;
    if (isdefined(video.georss$where)) {
        if (isdefined(video.georss$where.gml$Point)) {
            //            var shit;
            //            var point = video.georss$where.gml$Point;
            //            var pos = video.georss$where.gml$Point.gml$pos;
            var posArr = video.georss$where.gml$Point.gml$pos.$t.split(' ');
            var lat = posArr[0];
            var lng = posArr[1];
            var marker = new GMarker(new GLatLng(lat, lng), icon);
            var marker = new GMarker(new GLatLng(lat, lng), { id: "mkr_youtube_" + i, icon: icon });
            markerGroups["youtube"].push(marker);
            map.addOverlay(marker, icon);
            var t;
            GEvent.addListener(marker, "mouseover", function() {
                marker.setImage("/Static/Images/Markers/marker_youtube_ovr.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
                marker.setImage("/Static/Images/Markers/marker_youtube.png");
                clearTimeout(t);
            });

            //var myHtml = "<div class='infoWinPanoramio'><table class='popupTable'><tr><td><img class='infoWinPhoto' src='" + thumbnailUrl + "' width='" + video.media$group.media$thumbnail[0].width + "px' height='" + video.media$group.media$thumbnail[0].height + "px' /></td><td class='tit'><a id='link-" + i + "' href='" + playerUrl + "' alt='click to view original in a new window' target='_blank'>" + setTitle(title, 30) + "</a><br /><em>by " + author + "</em></td></tr></table></div>";
            var myHtml = "<div class='infoWinPanoramio'><table class='popupTable'><tr><td><img class='infoWinPhoto' src='" + thumbnailUrl + "' width='100px' /></td><td class='tit'><a id='link-" + i + "' href='" + playerUrl + "' alt='click to view original in a new window' target='_blank'>" + setTitle(title, 30) + "</a><br /><em>by " + author + "</em></td></tr></table></div>";

            markers_youtube[m] = marker;

            $("img#mtgt_mkr_youtube_" + i).qtip({
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

            $("a#link-" + i).click(function() {
                alert("!");
                $.fancybox({
                    'padding': 0,
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'overlayShow': true,
                    'title': this.title,
                    'width': 680,
                    'height': 495,
                    'href': this.href.replace(new RegExp("watch\\?v=", "i"), 'v/'),
                    'type': 'swf',
                    'swf': {
                        'wmode': 'transparent',
                        'allowfullscreen': 'true'
                    }
                });

                return false;
            });


            m++;
        }
    }
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

function addMainResortMarker() {
    var icon = new GIcon();
    icon.image = "/Static/Images/Markers/resort_l_off.png";
    icon.iconSize = new GSize(34, 54);
    icon.iconAnchor = new GPoint(17, 54);
    icon.infoWindowAnchor = new GPoint(17, 0);
    var marker = new GMarker(new GLatLng(document.getElementById("hidLat").value, document.getElementById("hidLng").value), { id: "mkr_0", icon: icon });
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/static/images/markers/resort_l_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/static/images/markers/resort_l_off.png");
    });

    $("img#mtgt_mkr_0").qtip({
        content: "<span class='flag-m " + document.getElementById("hidCountryCode").value + "'>&nbsp;</span>" + document.getElementById("hidResortName").value,
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
    markers[0] = marker;
}

function storeAirports(Objects) {
    airports = Objects.Data;
    for (var i = 999; i < airports.length; i++) {
        var icon = new GIcon();
        icon.image = "/Static/Images/airport_off.png";
        icon.iconSize = new GSize(34, 54);
        icon.iconAnchor = new GPoint(17, 54);
        icon.infoWindowAnchor = new GPoint(17, 0);
        addAirportMarker(airports[i], icon, i);
    }
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

function getResortMarkers() {
    $.getJSON("/Ajax/GetResortsByBounds", { minx: minx, miny: miny, maxx: maxx, maxy: maxy, rid: rid }, storeResorts);
}

function getAirportMarkers() {
    $.getJSON("/Ajax/GetAirportsByBounds", { minx: minx, miny: miny, maxx: maxx, maxy: maxy, rid: rid }, storeAirports);
}


function getFlickrMarkers(maxRows) {
//    $("#mediaWrap").show();
    var scrFlickr = document.createElement('script');
    scrFlickr.src = 'http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=6db84f88528c58eac21924761c19e7a6&min_taken_date=2005-01-01&bbox=' + minx + '%2C' + miny + '%2C' + maxx + '%2C' + maxy + '&extras=geo&content_type=1&format=json&nojsoncallback=1&sort=interestingness-desc&per_page=' + maxRows + '&page=1&jsoncallback=storeFlickr';
    var scr = document.getElementById('Map');
    scr.parentNode.insertBefore(scrFlickr, scr);
}

function getWebcamMarkers() {
    var scrWebcams = document.createElement('script');
    scrWebcams.src = 'http://api.webcams.travel/rest?method=wct.webcams.list_nearby&devid=8a855d4962b04ffce9986e3b4ef2dc0e&lat=' + document.getElementById("hidLat").value + '&lng=' + document.getElementById("hidLng").value + '&per_page=50&format=json&callback=storeWebcams';
    var scr5 = document.getElementById('Map');
    scr5.parentNode.insertBefore(scrWebcams, scr5);
}

function getYoutubeMarkers() {
    var scrYoutube = document.createElement('script');
    scrYoutube.src = 'http://gdata.youtube.com/feeds/api/videos?location=' + document.getElementById("hidLat").value + ',' + document.getElementById("hidLng").value + '!&location-radius=20km&alt=json-in-script&callback=storeYoutube';
    var scr6 = document.getElementById('Map');
    scr6.parentNode.insertBefore(scrYoutube, scr6);
}

function getWikipediaMarkers() {
    //get JSON formatted list of wikipedia entries
    var scrWikipedia = document.createElement('script');
    scrWikipedia.src = 'http://ws.geonames.org/wikipediaBoundingBoxJSON?north=' + miny + '&south=' + maxy + '&east=' + maxx + '&west=' + minx + '&maxRows=50&callback=storeWikipedia';
    var scr4 = document.getElementById('Map');
    scr4.parentNode.insertBefore(scrWikipedia, scr4);
}

function storeFlickr(flickr) {
    m = 0;
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
            //icon.image = "/img/Flickr-marker.png";
//            icon.image = "/static/images/markers/marker_flickr.png";
//            icon.iconSize = new GSize(31, 31);
//            icon.iconAnchor = new GPoint(15, 15);
//            icon.infoWindowAnchor = new GPoint(15, 15);
            icon.image = "/static/images/markers/marker_flickr.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            addFlickrMarker(flickr.photos.photo[i], icon, i);

//            var newA = document.createElement("A");
//            newA.setAttribute("title", pic.title);
//            newA.setAttribute("href", "http://www.flickr.com/photos/" + pic.owner + "/" + pic.id);
//            newA.setAttribute("class", "tnMediaLink flickr");
//            newA.setAttribute("rel", "external");
//            var newIMG = document.createElement("IMG");
//            newIMG.setAttribute("id", "img_flickr_" + i);
//            newIMG.setAttribute("class", "tnMedia");
//            newIMG.setAttribute("src", "http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + "_t.jpg?v=0");
//            newA.appendChild(newIMG);
//            target.appendChild(newA);
        }
    }
//    $("#targetFlickr").animate({
//        marginLeft: "0"
//    }, 1500);

}

function storeWikipedia(entries) {
    wikipedia = entries.geonames;
    if (wikipedia) {
        for (var i = 0; i < wikipedia.length; i++) {
            var icon = new GIcon();
            icon.image = "/static/images/markers/marker_wikipedia.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            addWikipediaMarker(wikipedia[i], icon, i);
        }
    }
}

function addWikipediaMarker(entry, icon, i) {
    var marker = new GMarker(new GLatLng(entry.lat, entry.lng), icon);
    var marker = new GMarker(new GLatLng(entry.lat, entry.lng), { id: "mkr_wikipedia_" + i, icon: icon });
    markerGroups["wikipedia"].push(marker);
    var t;
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/static/images/markers/marker_wikipedia_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/static/images/markers/marker_wikipedia.png");
        clearTimeout(t);
    });
    var imgHtml = "";
    if (entry.thumbnailImg) {
        imgHtml = "<img alt='[img]' src='" + entry.thumbnailImg + "' style='float: right; border: 3px solid #333;' />";
    }
    //var myHtml = "<div class='infoWinWebcams'><table class='popupTable'><tr><td>" + imgHtml + "</td><td><a href='http://" + entry.wikipediaUrl + "' title='View the Wikipedia entry for \'" + entry.title + "\'' target='_blank'>" + entry.title + "</a><br />" + entry.summary + "</td></tr></table></div>";
    var myHtml = "<div class='infoWinWebcams'><table class='popupTable'><tr><td><a href='http://" + entry.wikipediaUrl + "' title='View the Wikipedia entry for \'" + entry.title + "\'' target='_blank'>" + entry.title + "</a></td></tr><tr><td>" + imgHtml + entry.summary + "</td></tr></table></div>";
    $("img#mtgt_mkr_wikipedia_" + i).qtip({
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

}

function storeWebcams(cams) {
    for (var i = 0; i < cams.webcams.count; i++) {
        var icon = new GIcon();
        icon.image = "/static/images/markers/marker_webcams.png";
        icon.iconSize = new GSize(34, 54);
        icon.iconAnchor = new GPoint(17, 54);
        icon.infoWindowAnchor = new GPoint(17, 0);
        addWebcamsMarker(cams.webcams.webcam[i], icon, i);
    }
}

function addFlickrMarker(pic, myIcon, i) {
    var marker = new GMarker(new GLatLng(pic.latitude, pic.longitude), { id: "mkr_flickr_" + i, icon: myIcon });

    markerGroups["flickr"].push(marker);
    var t;
    map.addOverlay(marker, myIcon);

    markers_flickr[m] = marker;
    
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
//    $("#img_flickr_" + i).qtip({
//        content: myHtml,
//        position: {
//            corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
//        },
//        show: { effect: 'fade' },
//        style: {
//            name: 'dark',
//            tip: true,
//            border: {
//                width: 2,
//                radius: 5
//            }
//        }
//    });

    m++;
}

function addWebcamsMarker(webcam, myIcon, i) {
    var t;
//    var marker = new GMarker(new GLatLng(webcam.latitude, webcam.longitude), { id: "mkr_webcams_" + i, class: "tnWebcams", icon: myIcon });
    var marker = new GMarker(new GLatLng(webcam.latitude, webcam.longitude), { id: "mkr_webcams_" + i, icon: myIcon });
    markerGroups["webcams"].push(marker);
    map.addOverlay(marker, myIcon);
//    GEvent.addListener(marker, "mouseover", function() {
//        var myHtml = "<div class='infoWinWebcams'><img class='infoWinPhoto' src='" + webcam.thumbnail_url + "' height='96px' width='128px' /><div id='infoOverlay'><p><a title='View this webcam at webcams.travel (opens in a new window)' href='" + webcam.url + "' target='_blank'>" + webcam.title + "</a></p></div></div>";
//        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_red", myHtml, { beakOffset: 0, width: '128px' }); }, 500);
//    });
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/static/images/markers/marker_webcams_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/static/images/markers/marker_webcams.png");
        clearTimeout(t);
    });
    GEvent.addListener(marker, "click", function() {
        window.open(webcam.url);
    });
    var myHtml = "<div><table class='popupTable'><tr><td><img class='infoWinPhoto' src='" + webcam.thumbnail_url + "' height='96px' width='128px' /></td><td class='tit'><a href=\"" + webcam.url + "\" alt=\"click to view original in a new window\" target=\"_blank\">" + webcam.title + "</a></td></tr></table></div>";

    $("img#mtgt_mkr_webcams_" + i).qtip({
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
}


function showFlickrInfowin(marker, farm, server, id, secret, owner, title) {
    //var marker = markers_flickr[i];
    var myHtml = "<div class='infoWinPanoramio'><img class='photo' style='height: 75px; width: 75px;' src='http://farm" + farm + ".static.flickr.com/" + server + "/" + id + "_" + secret + "_t.jpg?v=0' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='http://www.flickr.com/photos/" + owner + "/" + id + "' target='_blank'>" + setTitle(title, 9) + "</a><br />by <a href='#'>username</a></p></div></div>";
    marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: '75px' });
}


function setTitle(title, l) {
    var str = "Untitled";
    if (title != "") {
        if (title.length > l) {
            str = title.substr(0, l) + " ...";
        }
        else {
            str = title;
        }
    }
    return str;
}

function hideMarkerGroup(type) {
    for (var i = 0; i < markerGroups[type].length; i++) {
        var marker = markerGroups[type][i];
        if (marker.isHidden()) {
            marker.show();
        } else {
            marker.hide();
        }
    }
}

function isdefined(variable) {
    return (typeof (variable) !== 'undefined');
}
