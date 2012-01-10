<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Webcams</asp:Content>

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

<%= Html.ResortNavigation(Model.Resort, "webcams", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
    <div class="grid_12">
        <div class="headwrap large">
            <h3>Webcams from Webcams.travel</h3>
        </div>
        <div class="pod">
            <div class="podIn">
                <div id="targetWebcams" style="padding: 10px 0 0 0;"></div>
                <div class="cb"></div>
            </div>
            <div class="podbtm" style="height: 35px">
                <div class="cb"></div>
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=webcams" title="" class="smlbutt" style="margin: 5px 5px 5px 10px">Show on Map</a> 
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
        maxy = lat + 0.3;
        miny = lat - 0.3;
        maxx = lng + 0.3;
        minx = lng - 0.3;
        $("#targetWebcams").html(sporthub.loadingSmall());
        getWebcamThumbs(45);
        sporthub.resortPages.onLoad();

    });

//    function storeWebcamsThumbs(cams) {
//        var target = document.getElementById("targetWebcams");
//        if (cams.webcams.webcam.length == 0) {
//            target.innerHTML = "<p>No webcams are available</p>";
//        }
//        else {
//            target.innerHTML = "";
//            for (var i = 0; i < cams.webcams.webcam.length; i++) {
//                var cam = cams.webcams.webcam[i];
//                var newA = document.createElement("A");
//                newA.setAttribute("title", cam.title);
//                newA.setAttribute("href", cam.url);
//                if ((i + 1) % 2 == 0) {
//                    newA.setAttribute("class", "tnMediaLink tnWebcams even");
//                } else {
//                    newA.setAttribute("class", "tnMediaLink tnWebcams");
//                }
//                newA.setAttribute("rel", "external");
//                var newIMG = document.createElement("IMG");
//                newIMG.setAttribute("class", "tnMedia");
//                newIMG.setAttribute("src", cam.thumbnail_url); //http://images.webcams.travel/webcam/1185534723.jpg
//                newA.appendChild(newIMG);
//                target.appendChild(newA);
//            }
//            sporthub.utility.qTipInit("div#targetWebcams");
//            $("a.tnWebcams").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true });
//        }
//    }

    function storeWebcamsThumbs(cams) {
        var target = document.getElementById("targetWebcams");
        if (cams.webcams.webcam.length == 0) {
            target.innerHTML = "<p>No webcams are available</p>";
        }
        else {
            target.innerHTML = "";
            for (var i = 0; i < cams.webcams.webcam.length; i++) {
                var cam = cams.webcams.webcam[i];
                var newA;
//                if (!isIE) {
                    newA = document.createElement("A");
                    newA.setAttribute("title", cam.title);
                    newA.setAttribute("href", "http://images.webcams.travel/webcam/" + cam.webcamid + ".jpg");
                    //                newA.setAttribute("href", cam.url);
                    if ((i + 1) % 2 == 0) {
                        newA.setAttribute("class", "tnMediaLink tnWebcams even");
                    } else {
                        newA.setAttribute("class", "tnMediaLink tnWebcams");
                    }
                    newA.setAttribute("rel", "external");
//                } else {
//                    var cls;
//                    if ((i + 1) % 2 == 0) {
//                        cls = "tnMediaLink tnWebcams even";
//                    } else {
//                        cls = "tnMediaLink tnWebcams";
//                    }
//                    newA = document.createElement('<a id="webcams_' + i + '" title="' + cam.title + '" href="http://images.webcams.travel/webcam/' + cam.webcamid + '.jpg" class="' + cls + '" rel="external"></a>');
//                }
                var newIMG;
//                if (!isIE) {
                    newIMG = document.createElement("IMG");
                    newIMG.setAttribute("class", "tnMedia");
                    newIMG.setAttribute("src", cam.thumbnail_url);
//                } else {
//                    newIMG = document.createElement('<img class="tnMedia" src="' + cam.thumbnail_url + '" />');
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
            $('a.tnWebcams').qtip({
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
            $("a.tnWebcams").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true });
        }
    }
    function getWebcamThumbs(maxRows) {
        var scrWebcams = document.createElement('script');
        scrWebcams.src = 'http://api.webcams.travel/rest?method=wct.webcams.list_nearby&devid=8a855d4962b04ffce9986e3b4ef2dc0e&lat=' + lat + '&lng=' + lng + '&per_page=' + maxRows + '&format=json&callback=storeWebcamsThumbs';
        var scr = document.getElementById('targetWebcams');
        scr.parentNode.insertBefore(scrWebcams, scr);
    }

    </script>
</asp:Content>
