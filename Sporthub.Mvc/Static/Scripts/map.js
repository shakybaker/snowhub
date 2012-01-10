-if viewing countries in a continent then populate continent model in viewdata (-update sproc)
left col - locations
right col - map
below - a-z list of resorts


var markers = [];
var m = 0;
var map = new GMap2(document.getElementById("map"));
var geocoder = new GClientGeocoder();
//drag-zoom set-up
var boxStyleOpts = { opacity: .2, border: "2px solid yellow" };
var otherOpts = {
    buttonHTML: "<img src='/Static/Images/Maps/zoom-control-inactive.png' />",
    buttonZoomingHTML: "<img src='/Static/Images/Maps/zoom-control-active.png' />",
    buttonStartingStyle: { width: '17px', height: '17px' },
    overlayRemoveTime: 0
};
var callbacks = {};
//drag-zoom end

window.onunload = GUnload;

var htmls = [];
var i = 0;

    $(document).ready(function() {

        showMap();
    });
    
    function storeResorts(Objects) 
    {
        resorts = Objects.resorts;
        for (var i = 0; i < resorts.length; i++)
	    {
		    var icon = new GIcon();
		    icon.image = "/assets/images/marker_resort_small.png";
		    icon.iconSize = new GSize(25, 25);
		    icon.iconAnchor = new GPoint(12, 25);
		    icon.infoWindowAnchor = new GPoint(12, 10);
		    addResortMarker(resorts[i], icon);
		}
		mapSetZoomLevel(markers);
    }

    function addResortMarker(resort, icon) {
        var marker = new GMarker(new GLatLng(resort.lat, resort.lng), icon);
        map.addOverlay(marker, icon);
        var t;
        GEvent.addListener(marker, "mouseover", function() {
            //var myHtml = "<div class='infoWinPanoramio'><img src='/assets/images/logo_panoramio.png' height='20px' width='95px' /><br /><img class='infoWinPhoto' src='" + pic.photo_file_url + "' width='" + pic.width + "px' height='" + pic.height + "px' /><p><strong>Added by:</strong> <a href='#'>username</a></p><strong>Original Image:</strong> <a href='#'>image name</a></p></div>";
            var myHtml = "<div class='centre' style='height: 60px; line-height: 60px; border-top: 10px solid #444343;'><img alt='[Loading resort]' src='/assets/images/loadingAnimation.gif' /></div>";
            t = setTimeout(function() { marker.openExtInfoWindow(map, "custom_info_window_red", myHtml, { beakOffset: 0, width: '300px', ajaxUrl: '/Feeds/GetResortPopup.aspx?rid=' + resort.id, ajaxCallback: 'largeInfoWinLoad' }); }, 500);
        });
        GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/assets/images/marker_resort_small_ovr.png");
        });
        GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/assets/images/marker_resort_small.png");
            clearTimeout(t);
        });
        GEvent.addListener(marker, "click", function() {
            window.location = "/Resorts/resort.aspx?rid=" + resort.id;
        });

        markers[m] = marker;

        var targetList = document.getElementById('targetList');
        var newLI = document.createElement('LI');
        newLI.innerHTML = '<a href="/Resorts/resort.aspx?rid=' + resort.id + '" onmouseover="markers[' + m + '].setImage(\'/assets/images/marker_resort_small_ovr.png\')" onmouseout="markers[' + m + '].setImage(\'/assets/images/marker_resort_small.png\')">' + resort.name + '</a>';
        targetList.appendChild(newLI);

        m++;
    }

    function showMap()

    {
        map.setCenter(new GLatLng($('hidLat').val(), $('hidLng').val()), 6);
        map.addControl(new GScaleControl());
        map.addMapType(G_PHYSICAL_MAP);
        map.setMapType(G_PHYSICAL_MAP);
        map.addControl(new DragZoomControl(boxStyleOpts, otherOpts, {}),
                new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(27, 7)));
        map.addControl(new GLargeMapControl(), new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(7, 32)));

        var scrContinents = document.createElement('script');

        var cid = $('hidCountryID').val();
        
        scrContinents.src = '/feeds/GetResorts.aspx?a=COUNTRY&id=' + cid + '&callback=storeResorts'; 
        var scr = document.getElementById('map');
        scr.parentNode.insertBefore(scrContinents, scr);
    }

    function largeInfoWinLoad() {
        $("#example > ul").tabs();
    }
    