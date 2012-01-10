var myimages=new Array()
function preloadimages() {
    for (i = 0; i < preloadimages.arguments.length; i++) {
        myimages[i] = new Image()
        myimages[i].src = preloadimages.arguments[i]
    }
}
    
preloadimages(
    "/assets/images/loadingAnimation.gif",
    "/assets/images/bgH3_ovr.png",
    "/assets/images/bgH3.png",
    "/assets/images/bgList.png",
    "/assets/images/red_b.gif",
    "/assets/images/red_beak.gif",
    "/assets/images/red_bl.gif",
    "/assets/images/red_br.gif",
    "/assets/images/red_close.gif",
    "/assets/images/red_l.gif",
    "/assets/images/red_r.gif",
    "/assets/images/red_t.gif",
    "/assets/images/red_tl.gif",
    "/assets/images/red_tr.gif",
    "/assets/images/win_b.gif",
    "/assets/images/win_beak.gif",
    "/assets/images/win_bl.gif",
    "/assets/images/win_br.gif",
    "/assets/images/win_close.gif",
    "/assets/images/win_l.gif",
    "/assets/images/win_r.gif",
    "/assets/images/win_t.gif",
    "/assets/images/win_tl.gif",
    "/assets/images/win_tr.gif",
    "/assets/images/marker_panoramio_ovr.png",
    "/assets/images/marker_flickr_ovr.png",
    "/assets/images/marker_webcams_ovr.png",
    "/assets/images/marker_wikipedia_ovr.png",
    "/assets/images/marker_youtube_ovr.png"
);

var testCnt = 0;
var photos; //object to hold panoramio photos
var photos2; //object to hold flickr photos
var weather; //object to hold weather station info
var map;
var geocoder;

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

var num = 0;
var tmpUrl;

var maxx, maxy, minx, miny; //map bounds
var maxRows = 50; //max no of items in json request

window.onresize = showMap;
window.onunload = GUnload;

var markerGroups = { "panoramio": [], "flickr": [], "weather": [], "webcams": [], "wikipedia": [], "youtube": [] };
var markers_panoramio = [];
var markers_flickr = [];
var markers_youtube = [];
var markers_weather = [];
var markers_webcams = [];
var markers_wikipedia = [];
var htmls = [];
var m = 0;

$(document).ready(function() {

    $(document).ready(function() {
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
    });

    ////$('#mapOptions').height($(window).height() - 150);

//    $('#markerNav').accordion({
//        active: '.current',
//        header: '.markerNavHead',
//        navigation: false,
//        event: 'click',
//        fillSpace: true,
//        animated: false
//    });
//    $('#markerNav').show('slow');

    showMap();
    addMainResortMarker();

    $('#cbPanoramio').click(function() {
        if (!$(this).is(':checked')) {//note: at this point the 'checked' state will be the oppposite
            setTimeout(function() {
                //$.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting images from <strong style="color: #003970;">Panoramio</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getPanoramioMarkers();
            }, 50);
            setTimeout('$.unblockUI()', 1000);
        }
        else {
            hideMarkerGroup('panoramio');
            document.getElementById('listPanoramio').innerHTML = "";
        }
    });
    $('#cbFlickr').click(function() {
        if (!$(this).is(':checked')) {
            setTimeout(function() {
                $.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting images from <strong style="color: #003970;">Flickr</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getFlickrMarkers();
            }, 50);
            setTimeout('$.unblockUI()', 1000);
        }
        else {
            hideMarkerGroup('flickr')
            document.getElementById('listFlickr').innerHTML = "";
        }
    });
    $('#cbYoutube').click(function() {
        if (!$(this).is(':checked')) {
            setTimeout(function() {
            $.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting images from <strong style="color: #003970;">YouTube</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getYoutubeMarkers();
            }, 50);
            setTimeout('$.unblockUI()', 1000);
        }
        else {
            hideMarkerGroup('youtube')
            document.getElementById('listYoutube').innerHTML = "";
        }
    });
    $('#cbWikipedia').click(function() {
        if (!$(this).is(':checked')) {
            setTimeout(function() {
                $.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting entries from <strong style="color: #003970;">Wikipedia</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getWikipediaMarkers();
            }, 50);
            setTimeout('$.unblockUI()', 1000);
        }
        else {
            hideMarkerGroup('wikipedia')
        }
    });
    $('#cbWebcams').click(function() {
        if (!$(this).is(':checked')) {
            setTimeout(function() {
                $.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%;">Getting webcams from <strong style="color: #003970;">Webcams.travel</strong></p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });
                getMapBounds();
                getWebcamMarkers();
            }, 50);
            setTimeout('$.unblockUI()', 1000);
        }
        else {
            hideMarkerGroup('webcams')
        }
    });
});

function addMainResortMarker() {
    var icon = new GIcon();
    //icon.image = "/img/Flickr-marker.png";
    icon.image = "/Static/Images/Markers/resort_l_off.png";
    //icon.shadow = "/Static/Images/Markers/shadow_l.png";
    icon.iconSize = new GSize(40, 49);
    icon.shadowSize = new GSize(67, 44);
    icon.iconAnchor = new GPoint(20, 48);
    icon.infoWindowAnchor = new GPoint(20, 10);
    var marker = new GMarker(new GLatLng(getDotNetID("hidLat").value, getDotNetID("hidLng").value), icon);
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/Static/Images/Markers/resort_l_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/Static/Images/Markers/resort_l_off.png");
        //clearTimeout(t);
    });
    //    GEvent.addListener(marker, "mouseover", function() {
//        //var myHtml = "<div class='infoWinPanoramio'><img src='/assets/images/logo_panoramio.png' height='20px' width='95px' /><br /><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><p><strong>Added by:</strong> <a href='#'>username</a></p><strong>Original Image:</strong> <a href='#'>image name</a></p></div>";
//        var myHtml = "<div class='centre' style='height: 60px; line-height: 60px; border-top: 10px solid #444343;'><img alt='[Loading resort]' src='/assets/images/loadingAnimation.gif' /></div>";
//        marker.openExtInfoWindow(map, "custom_info_window_red", myHtml, { beakOffset: 0, width: '300px', ajaxUrl: '/Shared/Ajax/GetResortPopup.aspx?rid=' + getDotNetID("hidResortID").value, ajaxCallback: 'largeInfoWinLoad' });
//    });
}


function storePanoramio(panoramio) {
    m = 0;
    //document.getElementById("ctrLoading").value = "";
    photos = panoramio.photos;
    for (var i = 0; i < photos.length; i++) {
        var icon = new GIcon();
        icon.image = "/assets/images/marker_panoramio.png";
        icon.shadow = "/assets/images/marker_shadow.png";
        icon.iconSize = new GSize(40, 49);
        icon.shadowSize = new GSize(67, 44);
        icon.iconAnchor = new GPoint(20, 49);
        icon.infoWindowAnchor = new GPoint(20, 10);
        addPanoramioMarker(photos[i], icon);
        //document.getElementById("ctrPanoramio").value = (i + 1) + "/" + photos.length;
    }
    //setTimeout('document.getElementById("ctrLoading").value = ""', 1000);
}

function addPanoramioMarker(pic, icon) {
    var marker = new GMarker(new GLatLng(pic.latitude, pic.longitude), icon);
    markerGroups["panoramio"].push(marker);

    map.addOverlay(marker, icon);
    var t;
    GEvent.addListener(marker, "mouseover", function() {
        var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + pic.photo_url + "' alt='click to view original in a new window' target='_blank'>" + setTitle(pic.photo_title, 30) + "</a><br /><em>by " + pic.owner_name + "</em></p></div></div>";
        var opts = {
            beakOffset: 0,
            width: pic.width + 'px'
        };
        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, opts); }, 500);
    });
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/assets/images/marker_panoramio_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/assets/images/marker_panoramio.png");
        clearTimeout(t);
    });

    markers_panoramio[m] = marker;

    var targetList = document.getElementById('listPanoramio');
    var newLI = document.createElement('LI');
    newLI.innerHTML = '<a href="' + pic.photo_url + '" onmouseover="showPanoramioInfowin(' + m + ', \'' + pic.photo_file_url + '\', \'' + pic.width + '\', \'' + pic.height + '\', \'' + pic.photo_url + '\', \'' + replaceQuotes(pic.photo_title) + '\', \'' + pic.owner_name + '\'); markers_panoramio[' + m + '].setImage(\'/assets/images/marker_panoramio_ovr.png\')" onmouseout="markers_panoramio[' + m + '].setImage(\'/assets/images/marker_panoramio.png\')" target="_blank">' + pic.photo_title + '</a>';

    targetList.appendChild(newLI);

    m++;
}

function showPanoramioInfowin(i, photo_file_url, width, height, photo_url, photo_title, owner_name) {
    var marker = markers_panoramio[i];
    var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + photo_file_url + "' width='" + width + "px' height='" + height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + photo_url + "' alt='click to view original in a new window' target='_blank'>" + setTitle(photo_title, 30) + "</a><br /><em>by " + owner_name + "</em></p></div></div>";
    marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: width + 'px' });
}

function storeYoutube(youtube) {
    m = 0;
    var feed = youtube.feed;
    var entries = feed.entry || [];
    for (var i = 0; i < entries.length; i++) {
        var icon = new GIcon();
        icon.image = "/assets/images/marker_youtube.png";
        icon.shadow = "/assets/images/marker_shadow.png";
        icon.iconSize = new GSize(40, 49);
        icon.shadowSize = new GSize(67, 44);
        icon.iconAnchor = new GPoint(20, 49);
        icon.infoWindowAnchor = new GPoint(20, 10);

        addYoutubeMarker(entries[i], icon);
    }
}

function addYoutubeMarker(video, icon) {
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
            markerGroups["youtube"].push(marker);
            map.addOverlay(marker, icon);
            var t;
            GEvent.addListener(marker, "mouseover", function() {
                //var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + pic.photo_url + "' alt='click to view original in a new window' target='_blank'>" + setTitle(pic.photo_title, 30) + "</a><br /><em>by " + pic.owner_name + "</em></p></div></div>";
            var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + thumbnailUrl + "' width='" + video.media$group.media$thumbnail[0].width + "px' height='" + video.media$group.media$thumbnail[0].height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + playerUrl + "' alt='click to view original in a new window' target='_blank'>" + setTitle(title, 30) + "</a><br /><em>by " + author + "</em></p></div></div>";
                var opts = {
                    beakOffset: 0,
                    width: video.media$group.media$thumbnail[0].width + 'px'
                };
                t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, opts); }, 500);
            });
            GEvent.addListener(marker, "mouseover", function() {
                marker.setImage("/assets/images/marker_youtube_ovr.png");
            });
            GEvent.addListener(marker, "mouseout", function() {
                marker.setImage("/assets/images/marker_youtube.png");
                clearTimeout(t);
            });

            markers_youtube[m] = marker;

            m++;
        }
    }
   // var blah2 = video.georss$where.gml$Point;
//    html.push('<li onclick="loadVideo(\'', playerUrl, '\', true)">',
//              '<span class="titlec">', title, '...</span><br /><img src="',
//              thumbnailUrl, '" width="130" height="97"/>', '</span></li>');

//    var marker = new GMarker(new GLatLng(pic.latitude, pic.longitude), icon);
//    markerGroups["youtube"].push(marker);

//    map.addOverlay(marker, icon);
//    var t;
//    GEvent.addListener(marker, "mouseover", function() {
//        var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + pic.photo_url + "' alt='click to view original in a new window' target='_blank'>" + setTitle(pic.photo_title, 30) + "</a><br /><em>by " + pic.owner_name + "</em></p></div></div>";
//        var opts = {
//            beakOffset: 0,
//            width: pic.width + 'px'
//        };
//        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, opts); }, 500);
//    });
//    GEvent.addListener(marker, "mouseover", function() {
//        marker.setImage("/assets/images/marker_youtube_ovr.png");
//    });
//    GEvent.addListener(marker, "mouseout", function() {
//        marker.setImage("/assets/images/marker_youtube.png");
//        clearTimeout(t);
//    });

//    markers_youtube[m] = marker;

//    var targetList = document.getElementById('listYoutube');
//    var newLI = document.createElement('LI');
//    newLI.innerHTML = '<a href="' + pic.photo_url + '" onmouseover="showYoutubeInfowin(' + m + ', \'' + pic.photo_file_url + '\', \'' + pic.width + '\', \'' + pic.height + '\', \'' + pic.photo_url + '\', \'' + replaceQuotes(pic.photo_title) + '\', \'' + pic.owner_name + '\'); markers_youtube[' + m + '].setImage(\'/assets/images/marker_youtube_ovr.png\')" onmouseout="markers_youtube[' + m + '].setImage(\'/assets/images/marker_youtube.png\')" target="_blank">' + pic.photo_title + '</a>';

//    targetList.appendChild(newLI);

}

function isdefined(variable) {
return (typeof (variable) !== 'undefined');
}

function showYoutubeInfowin(i, photo_file_url, width, height, photo_url, photo_title, owner_name) {
    var marker = markers_youtube[i];
    var myHtml = "<div class='infoWinPanoramio'><img class='infoWinPhoto' src='" + photo_file_url + "' width='" + width + "px' height='" + height + "px' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='" + photo_url + "' alt='click to view original in a new window' target='_blank'>" + setTitle(photo_title, 30) + "</a><br /><em>by " + owner_name + "</em></p></div></div>";
    marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: width + 'px' });
}

function storeFlickr(flickr) {
    m = 0;
    if (flickr.stat != "ok") {
        // something broke!
        return;
    }

    for (var i = 0; i < flickr.photos.photo.length; i++) {

        var pic = flickr.photos.photo[i];
        var icon = new GIcon();
        //icon.image = "/img/Flickr-marker.png";
        icon.image = "/assets/images/marker_flickr.png";
        icon.shadow = "/assets/images/marker_shadow.png";
        icon.iconSize = new GSize(40, 49);
        icon.shadowSize = new GSize(67, 44);
        icon.iconAnchor = new GPoint(20, 49);
        icon.infoWindowAnchor = new GPoint(20, 10);
        addFlickrMarker(flickr.photos.photo[i], icon);
    }
}

function addFlickrMarker(pic, icon) {
    var marker = new GMarker(new GLatLng(pic.latitude, pic.longitude), icon);
    markerGroups["flickr"].push(marker);
    var t;
    map.addOverlay(marker, icon);
    // "http://farm"+farm+".static.flickr.com/"+server+"/"+id+"_"+secret+".jpg?v=0"
    GEvent.addListener(marker, "mouseover", function() {
        t = setTimeout(function() { showFlickrInfowin(marker, pic.farm, pic.server, pic.id, pic.secret, pic.owner, pic.title) }, 500);
    });
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/assets/images/marker_flickr_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/assets/images/marker_flickr.png");
        clearTimeout(t);
    });

    markers_flickr[m] = marker;

    var targetList = document.getElementById('listFlickr');
    var newLI = document.createElement('LI');
    newLI.innerHTML = '<a href="http://www.flickr.com/photos/' + pic.owner + '/' + pic.id + '" onmouseover="showFlickrInfowin(' + m + ', \'' + pic.farm + '\', \'' + pic.server + '\', \'' + pic.id + '\', \'' + pic.secret + '\', \'' + pic.owner + '\', \'' + replaceQuotes(pic.title) + '\'); markers_flickr[' + m + '].setImage(\'/assets/images/marker_flickr_ovr.png\')" onmouseout="markers_flickr[' + m + '].setImage(\'/assets/images/marker_flickr.png\')" target="_blank">' + pic.title + '</a>';
    
    targetList.appendChild(newLI);

    m++;
}

function showFlickrInfowin(i, farm, server, id, secret, owner, title) {
    var marker = markers_flickr[i];
    var myHtml = "<div class='infoWinPanoramio'><img class='photo' style='height: 75px; width: 75px;' src='http://farm" + farm + ".static.flickr.com/" + server + "/" + id + "_" + secret + "_t.jpg?v=0' /><div id='infoOverlay' class='infoWinOverlay'><p><a href='http://www.flickr.com/photos/" + owner + "/" + id + "' target='_blank'>" + setTitle(title, 9) + "</a><br />by <a href='#'>username</a></p></div></div>";
    marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: '75px' });
}

function storeWikipedia(entries) {
    wikipedia = entries.geonames;
    if (wikipedia) {
        for (var i = 0; i < wikipedia.length; i++) {
            var icon = new GIcon();
            icon.image = "/assets/images/marker_wikipedia.png";
            icon.shadow = "/assets/images/marker_shadow.png";
            icon.iconSize = new GSize(40, 49);
            icon.shadowSize = new GSize(67, 44);
            icon.iconAnchor = new GPoint(20, 49);
            icon.infoWindowAnchor = new GPoint(20, 10);
            addWikipediaMarker(wikipedia[i], icon);
        }
    }
}

function addWikipediaMarker(entry, icon) {
    var marker = new GMarker(new GLatLng(entry.lat, entry.lng), icon);
    markerGroups["wikipedia"].push(marker);
    var t;
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "mouseover", function() {
        var imgHtml = "";
        if (entry.thumbnailImg) {
            imgHtml = "<img alt='[img]' src='" + entry.thumbnailImg + "' style='float: right; border: 3px solid #333;' />";
        }
        var myHtml = "<div class='infoWinPanoramio'><p><a href='http://" + entry.wikipediaUrl + "' title='View the Wikipedia entry for \'" + entry.title + "\'' target='_blank'>" + entry.title + "</a></p><p class='infoWinText'>" + imgHtml + entry.summary + "</p></div>";
        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: '300px' }); }, 500);
    });
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/assets/images/marker_wikipedia_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/assets/images/marker_wikipedia.png");
        clearTimeout(t);
    });
}

function storeWebcams(cams) {
    for (var i = 0; i < cams.webcams.count; i++) {
        var icon = new GIcon();
        icon.image = "/assets/images/marker_webcams.png";
        icon.shadow = "/assets/images/marker_shadow.png";
        icon.iconSize = new GSize(40, 49);
        icon.shadowSize = new GSize(67, 44);
        icon.iconAnchor = new GPoint(20, 49);
        icon.infoWindowAnchor = new GPoint(20, 10);
        addWebcamsMarker(cams.webcams.webcam[i], icon);
    }
}

function addWebcamsMarker(webcam, icon) {
    var t;
    var marker = new GMarker(new GLatLng(webcam.latitude, webcam.longitude), icon);
    markerGroups["webcams"].push(marker);
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "mouseover", function() {
        var myHtml = "<div class='infoWinWebcams'><img class='infoWinPhoto' src='" + webcam.thumbnail_url + "' height='96px' width='128px' /><div id='infoOverlay'><p><a title='View this webcam at webcams.travel (opens in a new window)' href='" + webcam.url + "' target='_blank'>" + webcam.title + "</a></p></div></div>";
        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_norm", myHtml, { beakOffset: 0, width: '128px' }); }, 500);
    });
    GEvent.addListener(marker, "mouseover", function() {
        marker.setImage("/assets/images/marker_webcams_ovr.png");
    });
    GEvent.addListener(marker, "mouseout", function() {
        marker.setImage("/assets/images/marker_webcams.png");
        clearTimeout(t);
    });
}

function showMap() {

    $('#Map').block({ message: '<img alt="[loading ...]" style="width:80%" src="/Static/Images/loadingAnimation.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });

    //$.blockUI({ message: '<p style="padding: 40px 0 10px 0; text-align: center; color: #003970; font-size: 120%; font-weight: bold;">Loading Map</p><p style="padding: 10px 60px 40px 60px"><img alt="[loading ...]" src="/assets/images/loadingAnimation.gif" /></p>' });

    map = new GMap2(document.getElementById("Map"));
    geocoder = new GClientGeocoder();
    map.setCenter(new GLatLng(getDotNetID("hidLat").value, getDotNetID("hidLng").value), 4);
    //map.addControl(new GOverviewMapControl());
    map.setMapType(G_PHYSICAL_MAP);

    var scrResorts = document.createElement('script');
    getMapBounds();
    scrResorts.src = '/Shared/Ajax/GetResorts.aspx?a=BOUNDS&minx=' + minx + '&miny=' + miny + '&maxx=' + maxx + '&maxy=' + maxy + '&callback=storeResorts';
    var scr = document.getElementById('Map');
    scr.parentNode.insertBefore(scrResorts, scr);


    setTimeout('$.unblockUI()', 1000);
}

function storeResorts(Objects) {
    resorts = Objects.resorts;
    for (var i = 0; i < resorts.length; i++) {
        var icon = new GIcon();
        icon.image = "/Static/Images/Markers/marker_resort_small.png";
        icon.iconSize = new GSize(25, 25);
        icon.iconAnchor = new GPoint(12, 25);
        icon.infoWindowAnchor = new GPoint(12, 10);
        addResortMarker(resorts[i], icon);
    }
    //mapSetZoomLevel(markers);
}

function addResortMarker(resort, icon) {
    var marker = new GMarker(new GLatLng(resort.lat, resort.lng), icon);
    map.addOverlay(marker, icon);
    var t;
//    GEvent.addListener(marker, "mouseover", function() {
//        //var myHtml = "<div class='infoWinPanoramio'><img src='/assets/images/logo_panoramio.png' height='20px' width='95px' /><br /><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><p><strong>Added by:</strong> <a href='#'>username</a></p><strong>Original Image:</strong> <a href='#'>image name</a></p></div>";
//        var myHtml = "<div class='centre' style='height: 60px; line-height: 60px; border-top: 10px solid #444343;'><img alt='[Loading resort]' src='/assets/images/loadingAnimation.gif' /></div>";
//        t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_red", myHtml, { beakOffset: 0, width: '300px', ajaxUrl: '/Feeds/GetResortPopup.aspx?rid=' + resort.id, ajaxCallback: 'largeInfoWinLoad' }); }, 500);
//    });
//    GEvent.addListener(marker, "mouseover", function() {
//        marker.setImage("/assets/images/marker_resort_small_ovr.png");
//    });
//    GEvent.addListener(marker, "mouseout", function() {
//        marker.setImage("/assets/images/marker_resort_small.png");
//        clearTimeout(t);
//    });
//    GEvent.addListener(marker, "click", function() {
//        window.location = "/Resorts/resort.aspx?rid=" + resort.id;
//    });

//    markers[m] = marker;

//    var targetList = document.getElementById('targetList');
//    var newLI = document.createElement('LI');
//    newLI.innerHTML = '<a href="/Resorts/resort.aspx?rid=' + resort.id + '" onmouseover="markers[' + m + '].setImage(\'/assets/images/marker_resort_small_ovr.png\')" onmouseout="markers[' + m + '].setImage(\'/assets/images/marker_resort_small.png\')">' + resort.name + '</a>';
//    targetList.appendChild(newLI);

//    m++;
}

function getMapMarkers(isClear) {
    testCnt++;

////    if (isClear) {
////        map.clearOverlays();
////    }

    getMapBounds();
    

////    if (document.getElementById("cbSnowhub").checked) {
////        //get JSON formatted list of panoramio photos
////        document.getElementById("lblSnowhub").style.backgroundImage = "url('images/keylabel_loading.gif')";
////        var scrSnowhub = document.createElement('script');
////        scrSnowhub.src = 'feeds/GetResorts.aspx?minx=' + minx + '&miny=' + miny + '&maxx=' + maxx + '&maxy=' + maxy + '&callback=storeSnowhub';
////        var scr = document.getElementById('map');
////        scr.parentNode.insertBefore(scrSnowhub, scr);

////        //------------------
////        //document.getElementById("tbTest").value = scrSnowHub.src;
////        //------------------
////    }

    if (document.getElementById("cbPanoramio").checked) {
        getPanoramioMarkers();
    }
    if (document.getElementById("cbFlickr").checked) {
        getFlickrMarkers();
    }
    if (document.getElementById("cbYoutube").checked) {
        getYoutubeMarkers();
    }
    if (document.getElementById("cbWikipedia").checked) {
        getWikipediaMarkers();
    }
    /*

    if (document.getElementById("cbWeather").checked) {
    //get JSON formatted list of weather stations
    //document.getElementById("lblWeather").style.backgroundImage = "url('images/keylabel_loading.gif')";
    var scrWeather = document.createElement('script');
    scrWeather.src = 'http://ws.geonames.org/weatherJSON?north=' + maxy + '&south=' + miny + '&east=' + minx + '&west=' + maxx + '&maxRows=100&callback=storeWeather';
    var scr3 = document.getElementById('map');
    scr3.parentNode.insertBefore(scrWeather, scr3);
    //------------------
    //document.getElementById("tbTest").value = scrWeather.src;
    //------------------
    }

    */
    if (document.getElementById("cbWebcams").checked) {
        getWebcamMarkers();
    }
} //END getMapMarkers

function getPanoramioMarkers() {
    //get JSON formatted list of panoramio photos
    //document.getElementById("lblPanoramio").style.backgroundImage = "url('/assets/images/keylabel_loading.gif')";
    var scrPanoramio = document.createElement('script');
    scrPanoramio.src = 'http://www.panoramio.com/map/get_panoramas.php?order=popularity&set=public&from=0&to=' + maxRows + '&minx=' + minx + '&miny=' + miny + '&maxx=' + maxx + '&maxy=' + maxy + '&callback=storePanoramio&size=small';
    var scr = document.getElementById('map');
    scr.parentNode.insertBefore(scrPanoramio, scr);
    //document.getElementById("tbTest").value = scrPanoramio.src;
}

function getFlickrMarkers() {
    //get JSON formatted list of flickr photos
    //hack the url to choose image size
    //    * _b is the big size (1024)
    //    * _t is the thumbnail
    //    * _s is the 75×75 pixels square
    //    * _m is the medium size
    //    * removing the _? extenstion means you choose the small size.
    var scrFlickr = document.createElement('script');
    scrFlickr.src = 'http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=6db84f88528c58eac21924761c19e7a6&min_taken_date=2005-01-01&bbox=' + minx + '%2C' + miny + '%2C' + maxx + '%2C' + maxy + '&extras=geo&format=json&nojsoncallback=1&sort=interestingness-desc&per_page=' + maxRows + '&page=1&jsoncallback=storeFlickr';
    var scr2 = document.getElementById('map');
    scr2.parentNode.insertBefore(scrFlickr, scr2);
    //document.getElementById("tbTest").value = scrFlickr.src;
}

function getYoutubeMarkers() {
    var scrYoutube = document.createElement('script');
    scrYoutube.src = 'http://gdata.youtube.com/feeds/api/videos?location=' + getDotNetID("hidLat").value + ',' + getDotNetID("hidLng").value + '!&location-radius=10km&alt=json-in-script&callback=storeYoutube';
    var scr6 = document.getElementById('map');
    scr6.parentNode.insertBefore(scrYoutube, scr6);
}

function getWikipediaMarkers() {
    //get JSON formatted list of wikipedia entries
    var scrWikipedia = document.createElement('script');
    scrWikipedia.src = 'http://ws.geonames.org/wikipediaBoundingBoxJSON?north=' + miny + '&south=' + maxy + '&east=' + maxx + '&west=' + minx + '&maxRows=50&callback=storeWikipedia';
    var scr4 = document.getElementById('map');
    scr4.parentNode.insertBefore(scrWikipedia, scr4);
}

function getWebcamMarkers() {
    //get JSON formatted list of weather stations
    var scrWebcams = document.createElement('script');
    scrWebcams.src = 'http://api.webcams.travel/rest?method=wct.webcams.list_nearby&devid=8a855d4962b04ffce9986e3b4ef2dc0e&lat=' + getDotNetID("hidLat").value + '&lng=' + getDotNetID("hidLng").value + '&per_page=50&format=json&callback=storeWebcams';
    //scrWebcams.src = 'http://api.webcams.travel/rest?method=wct.webcams.list_nearby&devid=8a855d4962b04ffce9986e3b4ef2dc0e&lat=51.47625114982365&lng=-0.124969482421875&format=json&callback=storeWebcams'; 
    var scr5 = document.getElementById('map');
    scr5.parentNode.insertBefore(scrWebcams, scr5);
    //document.getElementById("tbTest").value = scrWebcams.src;
}

function getMapBounds() {
    //get visible bounds of map
    var bounds = map.getBounds();
    minx = bounds.getSouthWest().lng();
    maxx = bounds.getNorthEast().lng();
    miny = bounds.getSouthWest().lat();
    maxy = bounds.getNorthEast().lat();
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

function replaceQuotes(inStr) {
    var str = inStr.replace(/\'/g,"\\'");
    return str;
}

function largeInfoWinLoad() {
    $("#example > ul").tabs();
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

//function hideMarkerGroup(type) {
//    for (var i = 0; i < markerGroups[type].length; i++) {
//        var marker = markerGroups[type][i];
//        marker.hide();
//    }
//}