<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" MasterPageFile="~/Shared/Masters/Sporthub.Master" Inherits="Sporthub.Web.Resorts.List" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

    <div id="PageHeader" class="container_12">
        <div class="HeaderWrap grid_3">
            <h2 id="PageHeading">World Resorts</h2>
        </div>
        <div class="cb"></div>
        <div class="grid_3">
            <div class="pod">
                <table class="table1"><tbody id="targetTBODY"></tbody></table>
            </div>
        </div>
        <div class="grid_9">
            <div id="divMapWorld">
            </div>
        </div>
        <div class="cb"></div>
    </div>  
<asp:HiddenField ID="hidID" runat="server" Value="0" />
<asp:HiddenField ID="hidLat" runat="server" Value="0" />
<asp:HiddenField ID="hidLng" runat="server" Value="0" />
<asp:HiddenField ID="hidLevel" runat="server" Value="0" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
<script src="/static/scripts/labeledmarker.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">

    var map = new GMap2(e("divMapWorld"));
    var geocoder = new GClientGeocoder();
    var markers = [];
    var m = 0;

    window.onunload = GUnload;

    $(document).ready(function() {

        var idType;
        switch (sporthub.utility.getDotNetID('hidLevel').value) {
            case 'World': idType = ''; break;
            case 'Continent': idType = 'contid'; break;
            case 'Country': idType = 'cid'; break;
            case 'Region': idType = 'regid'; break;
            default: idType = '';
        }

        showMap();
    });

    function storeLocations(Objects) {
        locations = Objects.locations;
        for (var i = 0; i < locations.length; i++) {
            var c = parseInt(locations[i].cnt);
            var icon = new GIcon();
            if (c < 5) {
                icon.image = "/static/images/markers/size/5.png";
                icon.iconSize = new GSize(24, 24);
                icon.iconAnchor = new GPoint(12, 12);
            }
            else if (c < 20) {
                icon.image = "/static/images/markers/size/20.png";
                icon.iconSize = new GSize(30, 30);
                icon.iconAnchor = new GPoint(15, 15);
            }
            else if (c < 100) {
                icon.image = "/static/images/markers/size/50.png";
                icon.iconSize = new GSize(47, 47);
                icon.iconAnchor = new GPoint(23, 23);
            }
            else {
                icon.image = "/static/images/markers/size/50plus.png";
                icon.iconSize = new GSize(75, 75);
                icon.iconAnchor = new GPoint(37, 37);
            }
            var opts = {
                "icon": icon,
                "clickable": true,
                "title": locations[i].name + " has " + c + " resorts",
                "labelText": c,
                "labelOffset": new GSize(-25, -6)
            };
            addMarker(locations[i], icon, opts);
        }
        //mapSetZoomLevel(markers);
    }

    function addMarker(cont, icon, opts) {
        var marker = new LabeledMarker(new GLatLng(cont.lat, cont.lng), opts);
        //var marker = new GMarker(new GLatLng(cont.lat, cont.lng), icon);
        map.addOverlay(marker, icon);
        GEvent.addListener(marker, "click", function() {
            window.location = "list.aspx?' + idType + '=" + cont.id;
        });
        var targetTBODY = document.getElementById('targetTBODY');
        var newTR = document.createElement('TR');
        var newTD1 = document.createElement('TD');
        var newTD2 = document.createElement('TD');
        newTD1.innerHTML = '<a href="list.aspx?' + idType + '=' + cont.id + '">' + cont.name + '</a>';
        newTD2.innerHTML = cont.cnt;
        newTR.appendChild(newTD1);
        newTR.appendChild(newTD2);
        targetTBODY.appendChild(newTR);
        markers[m] = marker;
        m++;
    }

    function showMap() {
        map.setCenter(new GLatLng(20, 0), 1);
        map.addMapType(G_PHYSICAL_MAP);
        map.setMapType(G_PHYSICAL_MAP);

        var scrLocations = document.createElement('script');
        switch (sporthub.utility.getDotNetID('hidLevel').value) {
            case 'World':
                scrLocations.src = '/shared/ajax/GetResortsClustered.aspx?l=1&callback=storeLocations';
                break;
            case 'Continent':
                scrLocations.src = '/shared/ajax/GetResortsClustered.aspx?l=2&callback=storeLocations';
                break;
            case 'Country':
                scrLocations.src = '/shared/ajax/GetResortsClustered.aspx?l=3&callback=storeLocations';
                break;
            case 'Region':
                scrLocations.src = '/shared/ajax/GetResortsClustered.aspx?l=4&callback=storeLocations';
                break;
            default: scrLocations.src = '';
        }
        var scr = document.getElementById('divMapWorld');
        scr.parentNode.insertBefore(scrLocations, scr);
    }

    function e(a) { return document.getElementById(a) }

</script>
</asp:Content>
