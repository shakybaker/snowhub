var map = new GMap2(document.getElementById("map"));
var geocoder = new GClientGeocoder();
var markers = [];
var m = 0;
var myimages = new Array()
function preloadimages() {
    for (i = 0; i < preloadimages.arguments.length; i++) {
        myimages[i] = new Image()
        myimages[i].src = preloadimages.arguments[i]
    }
}
preloadimages(
    "/static/images/anim/loading-horiz-l.gif"
);

window.onunload = GUnload;

$(document).ready(function() {

    showMap();
});

function storeContinents(Objects) {
    continents = Objects.Data;

    $("#statusMessage").html(Objects.TotalCount + " resorts");

    for (var i = 0; i < continents.length; i++) {
        var c = parseInt(continents[i].Count);
        var icon = new GIcon();
        if (c < 5) {
            icon.image = "/static/images/maps/5.png";
            icon.iconSize = new GSize(24, 24);
            icon.iconAnchor = new GPoint(12, 12);
        }
        else if (c < 20) {
            icon.image = "/static/images/maps/20.png";
            icon.iconSize = new GSize(30, 30);
            icon.iconAnchor = new GPoint(15, 15);
        }
        else if (c < 100) {
            icon.image = "/static/images/maps/50.png";
            icon.iconSize = new GSize(47, 47);
            icon.iconAnchor = new GPoint(23, 23);
        }
        else {
            icon.image = "/static/images/maps/50plus.png";
            icon.iconSize = new GSize(75, 75);
            icon.iconAnchor = new GPoint(37, 37);
        }
        var opts = {
            "icon": icon,
            "clickable": true,
            "title": "" + continents[i].Name + " has " + c + " resorts",
            "labelText": c,
            "labelOffset": new GSize(-17, -8)
        };
        addContinentMarker(continents[i], icon, opts);
    }
    //sporthub.utility.initRowLinks();

    setTimeout('$("#map").unblock();', 1000);

    if ($("#LocationLevel").val() != "1") {
        mapSetZoomLevel(markers);
    }
}

function addContinentMarker(cont, icon, opts) {
    var marker = new LabeledMarker(new GLatLng(cont.Latitude, cont.Longitude), opts);
    //var marker = new GMarker(new GLatLng(cont.lat, cont.lng), icon);
    map.addOverlay(marker, icon);
    GEvent.addListener(marker, "click", function() {
        window.location = "/resorts/" + cont.PrettyUrl + "/list";
    });
//    var targetTBODY = document.getElementById('targetTBODY');
//    var newTR = document.createElement('TR');
//    var newTD1 = document.createElement('TD');
//    var newTD2 = document.createElement('TD');
//    newTD1.innerHTML = '<a class="row" href="/resorts/' + cont.PrettyUrl + '/list">' + cont.Name + '</a>';
//    newTD2.innerHTML = cont.Count;
//    newTR.appendChild(newTD1);
//    newTR.appendChild(newTD2);
//    targetTBODY.appendChild(newTR);
    markers[m] = marker;
    m++;
}

function showMap() {
    map.setCenter(new GLatLng(10, 0), 2);
//    map.addMapType(G_PHYSICAL_MAP);
//    map.setMapType(G_PHYSICAL_MAP);

    $('#map').block({ message: '<img alt="Loading ..." style="width:90%" src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
    $.getJSON("/Ajax/GetResortsClustered", { level: $("#LocationLevel").val(), name: $("#LocationUrl").val() }, storeContinents);
}

function e(a) { return document.getElementById(a) }

