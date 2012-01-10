<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Videos</asp:Content>

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

<%= Html.ResortNavigation(Model.Resort, "videos", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
    <div class="grid_12">
        <div class="headwrap large">
            <h3>Videos from Youtube.com</h3>
        </div>
        <div class="pod">
            <div class="podIn">
                <div id="targetVideos" style="padding: 10px 0 0 0;"></div>
                <div class="cb"></div>
            </div>
            <div class="podbtm" style="height: 35px">
                <div class="cb"></div>
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=youtube" title="" class="smlbutt" style="margin: 5px 5px 5px 10px">Show on Map</a> 
                <div class="cb"></div>
            </div>
        </div>
    </div>
<%--    <div class="grid_2">
        <div class="pod">
            <div class="podIn nograd">--%>
        <%--<% if (Page.Request.Url.Host.Contains("localhost")) {%>--%>
                <%--<img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />--%>
        <%--<% } else { %>
            <script type="text/javascript"><!--
                google_ad_client = "pub-2930781007842752";
                /* 120x600, created 16/07/09 */
                google_ad_slot = "6878622167";
                google_ad_width = 120;
                google_ad_height = 600;
            //-->
            </script>
            <script type="text/javascript"
            src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
            </script>

            <script type="text/javascript">
                google_ad_client = "pub-2930781007842752";
                google_ad_width = 120;
                google_ad_height = 600;
                google_ad_format = "120x600_as";
                google_ad_type = "image";
                google_color_border = "FFFFFF";
                google_color_bg = "0000FF";
                google_color_link = "FFFFFF";
                google_color_text = "000000";
                google_color_url = "008000";
            </script><script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
        <% } %>--%>
                <%--<div class="cb"></div>
            </div>
        </div>--%>
<%--    </div>--%>
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
        $("#targetVideos").html(sporthub.loadingSmall());
        getYouTubeThumbs(45);
        sporthub.resortPages.onLoad();

    });

    function storeYouTubeThumbs(youtube) {
        var feed = youtube.feed;
        var entries = feed.entry || [];
        var target = document.getElementById("targetVideos");
        if (entries.length == 0) {
            target.innerHTML = "<p>No videos are available on YouTube</p>";
        }
        else {
            target.innerHTML = "";
            var l = (entries.length > 50) ? 50 : entries.length;
            for (var i = 0; (i < l); i++) {
                var video = entries[i];
                var title = video.title.$t;
                var thumbnailUrl = video.media$group.media$thumbnail[0].url;
                var playerUrl = ""
                try
                {
                    playerUrl = video.media$group.media$content[0].url;
                }
                catch(err){}
                var author = video.author[0].name.$t;
                var authorUrl = video.author[0].uri.$t;
                var newA = document.createElement("A");
                newA.setAttribute("id", "youtube_" + i);
                newA.setAttribute("title", title + " by " + author);
                //                    newA.setAttribute("href", "#div_youtube_" + i);
                newA.setAttribute("href", playerUrl);
                if ((i + 1) % 2 == 0) {
                    newA.setAttribute("class", "tnMediaLink tnYoutube even");
                } else {
                    newA.setAttribute("class", "tnMediaLink tnYoutube");
                }
                newA.setAttribute("rel", "external");
                var newIMG = document.createElement("IMG");
                newIMG.setAttribute("class", "tnMedia");
                newIMG.setAttribute("src", thumbnailUrl);
                newA.appendChild(newIMG);
                target.appendChild(newA);
//                var newDiv = document.createElement("DIV");
//                newDiv.setAttribute("style", "display:none");
//                newDiv.setAttribute("id", "div_youtube_" + i);
//                var newObject = document.createElement("OBJECT");
//                newObject.setAttribute("width", "425");
//                newObject.setAttribute("height", "355");
//                var newParam1 = document.createElement("PARAM");
//                newParam1.setAttribute("movie", playerUrl);
//                newObject.appendChild(newParam1);
//                var newParam2 = document.createElement("PARAM");
//                newParam2.setAttribute("wmode", "transparent");
//                newObject.appendChild(newParam2);
//                var newEmbed = document.createElement("EMBED");
//                newEmbed.setAttribute("src", playerUrl);
//                newEmbed.setAttribute("type", "application/x-shockwave-flash");
//                newEmbed.setAttribute("wmode", "transparent");
//                newEmbed.setAttribute("width", "425");
//                newEmbed.setAttribute("height", "355");
//                newObject.appendChild(newEmbed);

//                newDiv.appendChild(newObject);
//                target.appendChild(newDiv);

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
//            $("a.tnYoutube").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true, 'hideOnContentClick': false });
            $("a.tnYoutube").click(function() {
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
            $('a.tnYoutube').qtip({
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

        }
    }

    function getYouTubeThumbs(maxRows) {
        var scrYouTube = document.createElement('script');
        scrYouTube.src = 'http://gdata.youtube.com/feeds/api/videos?location=' + lat + ',' + lng + '!&location-radius=10km&alt=json-in-script&callback=storeYouTubeThumbs';
        var scr = document.getElementById('targetVideos');
        scr.parentNode.insertBefore(scrYouTube, scr);
    }

    </script>
</asp:Content>
