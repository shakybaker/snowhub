

//START show/hide results pane
$(window).keydown(function(event) {
    var keyCode = event.keyCode || window.event.keyCode;
    if (keyCode == 27) {//ESC
        $("#results").slideUp("slow");
    }
});

$("#search").keydown(function(event) {
    if ($("#search").val().length > 0) {
        $("#results").slideDown("fast");
    }
});
//END show/hide results pane


//gets .net id
function getDotNetID(inID) {
    return document.getElementById("ctl00_ContentMain_" + inID);
}

function mapSetZoomLevel(markers) {
    var bounds = new GLatLngBounds;
    for (var i = 0; i < markers.length; i++) {
        bounds.extend(markers[i].getLatLng());
    }
    map.setZoom(map.getBoundsZoomLevel(bounds));
    map.panTo(bounds.getCenter());
}
