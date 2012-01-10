<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Photos</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="<%= Model.Resort.Name %> is a ski resort in <%= Model.Resort.Country.CountryName%>, <%= Model.Resort.ContinentName%>" />
<meta name="keywords" content="<%= Model.Resort.Name %> <%= Model.Resort.Country.CountryName%> <%= Model.Resort.ContinentName%> ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
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
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<%= Html.ResortNavigation(Model.Resort, "photos", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
    <div class="grid_12">
        <div class="headwrap large">
            <h3>Photos from Flickr.com</h3>
        </div>
        <div class="pod">
            <div class="podIn">
                <div id="targetFlickr" style="padding: 10px 0 0 0;"></div>
                <div class="cb"></div>
            </div>
            <div class="podbtm" style="height: 35px">
                <div class="cb"></div>
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=flickr" title="" class="smlbutt" style="margin: 5px 5px 5px 10px">Show on Map</a> 
                <div class="cb"></div>
            </div>
        </div>
    </div>
    <div class="cb"></div>
</div>

<input id="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

    var maxx, maxy, minx, miny, lat, lng;
    var isIE = false;
    
    $(document).ready(function() {
        $.each($.browser, function(i) {
            if ($.browser.msie) {
                isIE = true;//detect IE, because its a pile of fucking shit
            }
        }); 
        lat = parseFloat($("#hidLat").val());
        lng = parseFloat($("#hidLng").val());
        maxy = lat + 0.1;
        miny = lat - 0.1;
        maxx = lng + 0.1;
        minx = lng - 0.1;
        $("#targetFlickr").html(sporthub.loadingSmall());
        getFlickrThumbs(45);
        //getPanoramioThumbs(4);
        sporthub.resortPages.onLoad();

    });
    
    function storeFlickrThumbs(flickr) {
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

                var newA;
//                if (!isIE) {
                    newA = document.createElement("A");
                    newA.setAttribute("id", "flickr_" + i);
                    newA.setAttribute("title", pic.title);
                    newA.setAttribute("href", "http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + ".jpg?v=0");
                    if ((i + 1) % 2 == 0) {
                        newA.setAttribute("class", "tnMediaLink tnFlickr even");
                    } else {
                        newA.setAttribute("class", "tnMediaLink tnFlickr");
                    }
                    newA.setAttribute("rel", "external");
//                } else {
//                    newA = document.createElement('<a id="flickr_' + i + '" title="test title" href="http://farm' + pic.farm + '.static.flickr.com/' + pic.server + '/' + pic.id + '_' + pic.secret + '.jpg?v=0" class="tnMediaLink tnFlickr" rel="external"></a>');
//                }
                var newIMG;
//                if (!isIE) {
                    newIMG = document.createElement("IMG");
                    newIMG.setAttribute("class", "tnMedia");
                    newIMG.setAttribute("src", "http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + "_t.jpg?v=0");
//                } else {
//                    newIMG = document.createElement('<img class="tnMedia" src="http://farm' + pic.farm + '.static.flickr.com/' + pic.server + '/' + pic.id + '_' + pic.secret + '_t.jpg?v=0"/>');
//                }
                newA.appendChild(newIMG);
                target.appendChild(newA);
            }
//            $("a.tnMediaLink").hover(
//                function() {
//                    $(this).parent("div").find("a.tnMediaLink").css("opacity", "0.5");
//                    $(this).css("opacity", "1");
//                },
//                function() {
//                    $(this).parent("div").find("a.tnMediaLink").css("opacity", "1");
//                }
//            );
            $('a.tnFlickr').qtip({
                style: {
                    name: 'buttonTip'
                },
                position: {
                    corner: {
                        target: 'topMiddle',
                        tooltip: 'bottomLeft'
                    }
                }
            });
            $("a.tnFlickr").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true }); 
        }
    }

    function storePanoramioThumbs(data) {
        photos = data.photos;
        var target = document.getElementById("targetPanoramio");
        if (photos.length == 0) {
            target.innerHTML = "<p>No pictures are available on Panoramio</p>";
        }
        else {
            target.innerHTML = "";
            for (var i = 0; i < photos.length; i++) {
                var pic = photos[i];
                var newA = document.createElement("A");
                newA.setAttribute("id", "panoramio_" + i);
                newA.setAttribute("title", pic.photo_title);
                newA.setAttribute("href", pic.photo_url);
                if ((i + 1) % 2 == 0) {
                    newA.setAttribute("class", "tnMediaLink tnPanoramio even");
                } else {
                    newA.setAttribute("class", "tnMediaLink tnPanoramio");
                }
                newA.setAttribute("rel", "external");
                var newIMG = document.createElement("IMG");
                newIMG.setAttribute("class", "tnMedia");
                newIMG.setAttribute("class", "tnMedia");
                newIMG.setAttribute("src", pic.photo_file_url);
                newA.appendChild(newIMG);
                target.appendChild(newA);
            }
            sporthub.utility.qTipInit("div#targetPanoramio");
            $("a.tnPanoramio").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true });
        }
    }

    function getFlickrThumbs(maxRows) {
        var scrFlickr = document.createElement('script');
        scrFlickr.src = 'http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=6db84f88528c58eac21924761c19e7a6&min_taken_date=2005-01-01&bbox=' + minx + '%2C' + miny + '%2C' + maxx + '%2C' + maxy + '&extras=geo&content_type=1&format=json&nojsoncallback=1&sort=interestingness-desc&per_page=' + maxRows + '&page=1&jsoncallback=storeFlickrThumbs';
        var scr = document.getElementById('targetFlickr');
        scr.parentNode.insertBefore(scrFlickr, scr);
    }

    function getPanoramioThumbs(maxRows) {
        var scrPanoramio = document.createElement('script');
        scrPanoramio.src = 'http://www.panoramio.com/map/get_panoramas.php?order=popularity&set=public&from=0&to=' + maxRows + '&minx=' + minx + '&miny=' + miny + '&maxx=' + maxx + '&maxy=' + maxy + '&callback=storePanoramioThumbs&size=small';
        var scr = document.getElementById('targetPanoramio');
        scr.parentNode.insertBefore(scrPanoramio, scr);
    }
    </script>
</asp:Content>
