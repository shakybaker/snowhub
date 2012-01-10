<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %></asp:Content>

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
</ul>
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<%= Html.ResortNavigation(Model.Resort, "overview", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
<%--    <div id="Map" class="grid_6" style="height: 250px; z-index: 0; background-color: #ddd; border: 8px solid #ddd; margin-top: 20px; width: 444px;"></div>
    <div class="grid_6">&nbsp;</div>--%>
    <div class="grid_3">&nbsp;</div>
    <div class="grid_9" style="margin-top: 20px;">
        <div id="Map" style="border:10px solid #ddd; height:370px; width:680px;"></div>
    </div>
</div>

<div class="container_12">
    <% var ids = string.Empty; %>
    <% var u = 0;%>
    <div class="grid_3 resortOverview">
        <% Html.RenderPartial("ResortInfo"); %>
        <div class="pod">
            <div class="headwrap">
                <h3>Resort Suits ...</h3>
            </div>
            <div class="podIn">
            <%= Html.ResortSuits(Model.Resort) %>
                <div class="cb"></div>
            </div>
        </div>
            
            
        <div class="pod">
            <div class="headwrap">
                <h3>Ratings</h3>
            </div>
            <div class="podIn">
                <table class="table1 nobord">
                <% foreach (Sporthub.Model.RatingItem item in Model.Ratings) { %>
                    <tr><th><%=item.Name %></th><td style="width: 30px;"><%= Html.Score(item.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, item.ScoreCount, null, null, null)%></td></tr>
                    <%--<span class="rating-score-sm unrated"><%= (item.Score == 0) ? "?" : item.Score.ToString() %></span></td></tr>--%>
                <%} %>
                <%--            
                <tr><th>Snow</th><td><span class="rating-score-sm bottom">23</span></td></tr>
                <tr><th>Queues</th><td><span class="rating-score-sm middle">34</span></td></tr>
                <tr><th>Scenery</th><td><span class="rating-score-sm middle">65</span></td></tr>
                <tr><th>Convenience</th><td><span class="rating-score-sm bottom">12</span></td></tr>
                <tr><th>Accomodation</th><td><span class="rating-score-sm top">100</span></td></tr>
                <tr><th>Food</th><td><span class="rating-score-sm middle">46</span></td></tr>
                <tr><th>Facilities</th><td><span class="rating-score-sm bottom">2</span></td></tr>
                <tr><th>Nightlife</th><td><span class="rating-score-sm top">92</span></td></tr>
                --%>
                </table>
                <div class="cb"></div>
            </div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Latest Reviews</h3>
            </div>
            <% if (Model.LatestReviews.Count>0) { %>
            <div id="l3" class="podIn list fbUsersList">
                    <% ids = string.Empty; %>
                    <ul class="list3 reviews">
                    <% foreach (Sporthub.Model.LinkResortUser review in Model.LatestReviews) { %>

                        <li>
                            <a class="r-bubble-link" href="/resorts/<%= review.Resort.PrettyUrl %>/reviews/<%= review.ID %>">
                            <div class="r-bubble">
                                <div class="r-top">
                                    <span class="flag <%= review.Resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= review.Resort.Name%>
                                    <%= Html.Score(review.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null)%>
                                </div>
                                <h5 class="r-title">"<%=review.Title %>"</h5>
                            </div>
                            <span class="r-beak">&nbsp;</span>
                            </a>
                        
                            <div class="r-bottom">
                                <a id="bumhole-<%=u %>" class="profile sml qtu" href="/user/<%=review.User.UserName%>"><img alt="profile pic" src="<%=review.User.GetSmallProfilePic() %>" class="tnMedia"/>
                                    <div class="profileSummary">
                                        <div class="profileSummaryIn">
                                            <%=review.User.GetName() %><br />
                                            <em><%=review.User.GetSportTypes() %></em>
                                        </div>
                                    </div>
                                </a>
                                <span class="r-date"><% = review.GetCreatedTime() %></span>
                                <div class="cb"></div>
                            </div>
                        </li>

                    <% u++;%>
                    <%} %>
                    </ul>
                </div>
            <%} else {%>
            <div class="podIn">
                <div style="margin: 0 20px 20px 0;line-height: 1.3em;">
                    No Reviews are available yet. Do you have an opinion? 
                    Then be the first to <a class="rate"  href="/review/<%= Model.Resort.PrettyUrl %>?ReturnUrl=/resorts/<%= Model.Resort.PrettyUrl %>" id="rateReviewResort">
                    add your review</a>
                </div>
            </div>
            <%} %>
            <% if (Model.LatestReviews.Count>0) { %>
            <div class="podbtm" style="height: 35px">
                <div class="cb"></div>
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/reviews" title="" class="smlbutt" style="margin: 5px 5px 5px 10px">Show all</a> 
                <div class="cb"></div>
            </div>
            <%} %>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Visitors</h3>
            </div>
            <div class="podIn">
                <div id="l1" class="fbUsersList">
                    <% if (Model.VisitedBy.Count > 0) {%>
                        <% foreach (var user in Model.VisitedBy) { %>
                        <a class="profile sml qtu" href="/user/<%=user.UserName%>"><img alt="profile pic" src="<%=user.GetSmallProfilePic() %>" class="tnMedia"/>
                            <div class="profileSummary">
                                <div class="profileSummaryIn">
                                    <%=user.GetName() %><br />
                                    <em><%=user.GetSportTypes() %></em>
                                </div>
                            </div>
                        </a>
                        <% }%>
                    <% } else {%>
                        <p>No members have Visited <%= Model.Resort.Name %>.</p><p><a href="#">Have you?</a></p>
                    <% }%>

                    <% if (Model.FavedBy.Count > 6) {%>
                    <div class="fl"><%= Model.VisitedBy.Count %> of <a href="#" title=""><%= Model.VisitedCount %> Visitors</a></div>
                    <% }%>
                </div>
                <div class="cb"></div>
            </div>
        </div>

        <div class="pod">
            <div class="headwrap">
                <h3>Faved</h3>
            </div>
            <div class="podIn">
                <div id="l2" class="fbUsersList">
                    <% if (Model.VisitedBy.Count > 0) {%>
                        <% foreach (var user in Model.FavedBy) { %>
                        <a class="profile sml qtu" href="/user/<%=user.UserName%>"><img alt="profile pic" src="<%=user.GetSmallProfilePic() %>" class="tnMedia"/>
                            <div class="profileSummary">
                                <div class="profileSummaryIn">
                                    <%=user.GetName() %><br />
                                    <em><%=user.GetSportTypes() %></em>
                                </div>
                            </div>
                        </a>
                        <% }%>
                    <% } else {%>
                        <p>No members have added <%= Model.Resort.Name %> as a Favourite.</p><p><a href="#">Is it one of yours?</a></p>
                    <% }%>

                    <% if (Model.FavedBy.Count > 6) {%>
                    <div class="fl"><a href="#" title="">See all <%= Model.FavouriteCount %> Faves</a></div>
                    <% }%>
                </div>
                <div class="cb"></div>
            </div>
        </div>
    </div>
    <%--<div class="grid_3" style="margin-top: -266px;">--%>
    <div class="grid_3" style="margin-top: 20px;">
        <div class="pod">
            <div class="headwrap">
                <h3>Weather</h3>
            </div>
            <div class="podIn">
                <div id="weatherTarget"></div>
                <p style="padding: 0 0 0 5px;"><em style="font-size: 0.8em">Provided by <a href="http://www.wunderground.com/" target="_blank" style="font-size: 1em">Weather Underground</a></em></p>
            </div>
        </div>

        <% if (Model.Resort.NearbyResorts.Count > 0) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Nearby Resorts</h3>
            </div>
            <div class="podIn">
                <ul class="list1">
                <% foreach (Sporthub.Model.Resort nearbyResort in Model.Resort.NearbyResorts) { %>
                <li><a href="/resorts/<%=nearbyResort.PrettyUrl %>"><img class="flag <%= nearbyResort.Country.ISO3166Alpha2%>" alt="[<%= nearbyResort.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyResort.Country.ISO3166Alpha2 %>.png" />&nbsp;<%=nearbyResort.Name %> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyResort.Latitude) %>km)</span></a></li>
                <% } %>
                </ul> 
                <p style="padding: 0 0 0 5px;"><em style="font-size: 0.8em">Distances are "As the crow flies"</em></p>
            </div>
        </div>
        <% } %>
        <% if (Model.Resort.NearbyAirports.Count > 0) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Nearby Airports</h3>
            </div>
            <div class="podIn">
                <ul class="list1">
                <% foreach (Sporthub.Model.Airport nearbyAirport in Model.Resort.NearbyAirports) { %>
                <li><a href="/airports/<%=nearbyAirport.PrettyUrl %>"><img class="flag <%= nearbyAirport.Country.ISO3166Alpha2%>" alt="[<%= nearbyAirport.Country.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= nearbyAirport.Country.ISO3166Alpha2 %>.png" />&nbsp;<%=nearbyAirport.Name%> <span style="font-size: 9px;">(<%= String.Format("{0:0.0}", nearbyAirport.Latitude)%>km)</span></a></li>
                <% } %>
                </ul> 
                <p style="padding: 0 0 0 5px;"><em style="font-size: 0.8em">NOTE: Distances are "As the crow flies". The nearest airports listed may not be most appropriate once transfer time has been taken into account.</em></p>
            </div>
        </div>
        <% } %>
<%--        <div style="margin-bottom: 20px">
        <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
            <img alt="ad" src="../../Static/Images/Ads/300x250MediumRectangle.gif" />
        <% } else { %>
            <script type="text/javascript"><!--
            google_ad_client = "pub-2930781007842752";
            /* 300x250, created 24/01/10 */
            google_ad_slot = "7343998310";
            google_ad_width = 300;
            google_ad_height = 250;
            //-->
            </script>
            <script type="text/javascript"
            src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
            </script>
        <% } %>
        </div>--%>
        <% if (Model.Resort.ResortLinks.Count > 0) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Links</h3>
            </div>
            <div class="podIn">
                <ul class="list1">
                <% foreach (Sporthub.Model.ResortLink link in Model.Resort.ResortLinks) { %>
                <li><a href="<%=link.URL %>" target="_blank"><%=link.Name %></a></li>
                <%--<p><%=link.Name %>, <%=(Sporthub.Model.Enumerators.ResortLinkType)link.ResortLinkTypeID %></p>--%>
                <% } %>
                </ul> 
                <a href="mailto:info@thesnowhub.com&subject=Suggested link for <%= Model.Resort.Name %>" title="" class="smlbutt" style="margin:  10px 0 0 10px;">Suggest a Link</a> 
                <div class="cb"></div>
            </div>
        </div>
        <% } %>
        <div class="pod">
            <div class="podIn nograd">
                <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                    <img alt="ad" width="200px" height="200px" src="/Static/Images/Ads/125x125Squarebutton.gif" />
                <% } else { %>
                <script type="text/javascript"><!--
                    google_ad_client = "pub-2930781007842752";
                    /* 200x200, created 25/01/10 */
                    google_ad_slot = "0202147660";
                    google_ad_width = 200;
                    google_ad_height = 200;
                //-->
                </script>
                <script type="text/javascript"
                src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                </script>
                <% } %>
            </div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Places around <%=Model.Resort.Name %></h3>
            </div>
            <div class="podIn">
                <div id="places-target">
                    <ul class="list1">
                    </ul>
                </div>
                <div class="cb"></div>
            </div>
            <div class="podbtm"></div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Local Interest</h3>
            </div>
            <div class="podIn">
                <div id="targetWikipedia"></div>
                <div class="cb"></div>
                <div id="targetWikipedia-btns" class="ajax-content-btns">
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=wikipedia" title="" class="smlbutt" style="margin: 5px">Show on Map</a> 
                </div>
                <div class="cb"></div>
            </div>
        </div>
    </div>
<%--        
<div class="pod">
            <div class="podIn nograd">
                <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                <img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />
                <% } else { %>
<script type="text/javascript"><!--
    google_ad_client = "pub-2930781007842752";
    /* 120x600, created 25/01/10 */
    google_ad_slot = "5652443482";
    google_ad_width = 120;
    google_ad_height = 600;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
                <% } %>
                <div class="cb"></div>
            </div>
        </div>
        --%>
    <%--<div class="grid_3" style="margin-top: -266px;">--%>
    <div class="grid_4" style="margin-top: 20px;">
        <div class="pod">
            <div class="headwrap">
                <h3>Photos</h3>
            </div>
            <div class="podIn">
                <div id="targetFlickr" class="mini"></div>
                <div class="cb"></div>
                <div id="targetFlickr-btns" class="ajax-content-btns">
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/photos" title="" class="smlbutt">Show all</a> 
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=flickr" title="" class="smlbutt">Show on Map</a> 
                </div>
                <div class="cb"></div>
            </div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Videos</h3>
            </div>
            <div class="podIn">
                <div id="targetVideos" class="mini"></div>
                <div class="cb"></div>
                <div id="targetVideos-btns" class="ajax-content-btns">
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/videos" title="" class="smlbutt">Show all</a> 
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=youtube" title="" class="smlbutt">Show on Map</a> 
                </div>
                <div class="cb"></div>
            </div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Webcams</h3>
            </div>
            <div class="podIn">
                <div id="targetWebcams" class="mini"></div>
                <div class="cb"></div>
                <div id="targetWebcams-btns" class="ajax-content-btns">
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/webcams" title="" class="smlbutt">Show all</a> 
                <a href="/resorts/<%= Model.Resort.PrettyUrl %>/map?m=webcams" title="" class="smlbutt">Show on Map</a> 
                </div>
                <div class="cb"></div>
            </div>
        </div>
        
        <div class="pod">
            <div class="podIn">
<script type="text/javascript"><!--
    amazon_ad_tag = "marathonblog-21"; amazon_ad_width = "300"; amazon_ad_height = "250"; amazon_ad_logo = "hide"; amazon_ad_link_target = "new"; amazon_color_border = "CCCCCC"; amazon_color_logo = "464646";//--></script>
<script type="text/javascript" src="http://www.assoc-amazon.co.uk/s/ads.js"></script>
            </div>
        </div>


    </div>
    <div class="grid_2" style="margin-top: 20px;">
        <div class="pod">
            <div class="podIn nograd">
                <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                <img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />
                <img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />
                <% } else { %>
<script type="text/javascript"><!--
    google_ad_client = "pub-2930781007842752";
    /* 120x600, created 25/01/10 */
    google_ad_slot = "5652443482";
    google_ad_width = 120;
    google_ad_height = 600;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
<script type="text/javascript">
    var uri = 'http://impgb.tradedoubler.com/imp?type(js)g(19196192)a(1906817)' + new String(Math.random()).substring(2, 11);
    document.write('<sc' + 'ript type="text/javascript" src="' + uri + '" charset="ISO-8859-1"></sc' + 'ript>');
</script>
                <% } %>
                <div class="cb"></div>
            </div>
        </div>
    </div>
</div>
<div id="rateReview">
</div>
<input id="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />

</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        var zoom_lvl = 10;
        var lg_map = false;
        var fetch_all = true;
    </script>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/dragzoom.js"></script>
    <script type="text/javascript" src="/Static/Scripts/maps.js"></script>

    <script type="text/javascript" language="javascript">

    var maxx, maxy, minx, miny, lat, lng;

    $(document).ready(function() {
        
//        var scrYoutube = document.createElement('script');
//        scrYoutube.src = "https://maps.googleapis.com/maps/api/place/search/json?location=-33.8670522,151.1957362&radius=500&types=food&name=harbour&sensor=false&key=AIzaSyB1BdZ2CW1PJSpbsA4hBGmMLAhEO0tsVFc";
//        var scr = document.getElementById('Map');
//        scr.parentNode.insertBefore(scrYoutube, scr);
        
      
        
        $.each($.browser, function(i) {
            if ($.browser.msie) {
                sporthub.isIE = true; //detect IE, because its a pile of fucking shit
            }
        });

        sporthub.resortPages.onLoad();
        
        $("#weatherTarget").html(sporthub.loadingSmall()).load("/Ajax/GetResortWeather?", { resortID: document.getElementById("hidResortID").value }, function(o) {
        });

        $("#places-target ul").html(sporthub.loadingSmall());
        $.getJSON("/Ajax/GoogleSearchAPI", { lng: $("#hidLng").val(), lat: $("#hidLat").val() }, function(data) {
            $("#places-target ul").html("");
            if (data.status == "OK") {
                var placesFound = 0;
                $.each(data.results, function(i,result){
                    if($.inArray("locality", result.types) < 0)
                    {
                        var types = "";
                        $.each(result.types, function(index, value) { 
                            types += value + ", "; 
                        });
                        $("#places-target ul").append("<li><a id=\"" + result.reference + "\" href=\"#\" title=\"" + types + "\"><img src=\"" + result.icon + "\" />" + result.name + "</a></li>");
                        placesFound++;
                    }
                });
                if (placesFound == 0)
                    $("#places-target ul").html("<li>No places found</li>");
            }
            else {
                if (data.status == "ZERO_RESULTS") {
                    $("#places-target ul").html("<li>No places found</li>");
                }
                else {
                    $("#places-target ul").html(data.ErrorMessage);
                }
            }
        });



        lat = parseFloat($("#hidLat").val());
        lng = parseFloat($("#hidLng").val());
        maxy = lat + 0.1;
        miny = lat - 0.1;
        maxx = lng + 0.1;
        minx = lng - 0.1;
        $("#targetFlickr").html(sporthub.loadingSmall());
        //$("#targetPanoramio").html(sporthub.loadingSmall());
        $("#targetWebcams").html(sporthub.loadingSmall());
        $("#targetVideos").html(sporthub.loadingSmall());
        $("#targetWikipedia").html(sporthub.loadingSmall());
        getWikipediaList(4);
        getFlickrThumbs(6);
        //getPanoramioThumbs(4);
        getWebcamThumbs(6);
        getYouTubeThumbs(6);

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
            $('#targetFlickr-btns').show();
            target.innerHTML = "";
            for (var i = 0; i < flickr.photos.photo.length; i++) {
                var pic = flickr.photos.photo[i];

                var newA;
//                if (!sporthub.isIE) {
                    newA = document.createElement("A");
                    newA.setAttribute("id", "flickr_" + i);
                    newA.setAttribute("title", pic.title);
                    newA.setAttribute("href", "http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + ".jpg?v=0");
                    if ((i + 1) % 3 == 0) {
                        newA.setAttribute("class", "qt tnMediaLink tnFlickr even");
                    } else {
                        newA.setAttribute("class", "qt tnMediaLink tnFlickr");
                    }
                    newA.setAttribute("rel", "external");
//                } else {
//                var cls;
//                if ((i + 1) % 2 == 0) {
//                    cls = "tnMediaLink tnFlickr even";
//                } else {
//                    cls = "tnMediaLink tnFlickr";
//                }
//                newA = document.createElement('<a id="flickr_' + i + '" title="test title" href="http://farm' + pic.farm + '.static.flickr.com/' + pic.server + '/' + pic.id + '_' + pic.secret + '.jpg?v=0" class="' + cls + '" rel="external"></a>');
//                }
                var newIMG;
//                if (!sporthub.isIE) {
                    newIMG = document.createElement("IMG");
                    newIMG.setAttribute("class", "tnMedia");
                    newIMG.setAttribute("src", "http://farm" + pic.farm + ".static.flickr.com/" + pic.server + "/" + pic.id + "_" + pic.secret + "_t.jpg?v=0");
//                } else {
//                    newIMG = document.createElement('<img class="tnMedia" src="http://farm' + pic.farm + '.static.flickr.com/' + pic.server + '/' + pic.id + '_' + pic.secret + '_t.jpg?v=0"/>');
//                }
                newA.appendChild(newIMG);
                target.appendChild(newA);
            }
            //            sporthub.utility.qTipInit("div#targetFlickr");
//            $("a.tnMediaLink").hover(
//                function() {
//                    $(this).parent("div").find("a.tnMediaLink").css("opacity", "0.5");
//                    $(this).css("opacity", "1");
//                },
//                function() {
//                    $(this).parent("div").find("a.tnMediaLink").css("opacity", "1");
//                }
//            );
            $('.tnFlickr').qtip({
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

            $("a.tnFlickr").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true, 'titlePosition' : 'inside' }); 
        }
    }

    function storeWebcamsThumbs(cams) {
        var target = document.getElementById("targetWebcams");
        if (cams.webcams.webcam.length == 0) {
            target.innerHTML = "<p>No webcams are available</p>";
        }
        else {
            $('#targetWebcams-btns').show();
            target.innerHTML = "";
            for (var i = 0; i < cams.webcams.webcam.length; i++) {
                var cam = cams.webcams.webcam[i];
                var newA;
//                if (!sporthub.isIE) {
                    newA = document.createElement("A");
                    newA.setAttribute("title", cam.title);
                    newA.setAttribute("href", "http://images.webcams.travel/webcam/" + cam.webcamid + ".jpg");
                    //                newA.setAttribute("href", cam.url);
                    if ((i + 1) % 3 == 0) {
                        newA.setAttribute("class", "qt tnMediaLink tnWebcams even");
                    } else {
                        newA.setAttribute("class", "qt tnMediaLink tnWebcams");
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
//                if (!sporthub.isIE) {
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
            //sporthub.utility.qTipInit("a.tnWebcams");
            $('.tnWebcams').qtip({
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

            $("a.tnWebcams").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true, 'titlePosition': 'inside' });
        }
    }


    function storeWikipediaList(entries) {
        var target = document.getElementById("targetWikipedia");
        if (entries.geonames) {
            if (entries.geonames.length == 0) {
                target.innerHTML = "<p>No wikipedia entries are available</p>";
            }
            else {
                $('#targetWikipedia-btns').show();
                target.innerHTML = "";
                var newUL = document.createElement("UL");
                newUL.setAttribute("class", "list2");
                for (var i = 0; i < entries.geonames.length; i++) {
                    var entry = entries.geonames[i];
                    var newLI = document.createElement("LI");

                    //!!!!!!!!!!!!!!!!!!!
                    //                if (!sporthub.isIE) {
                    //                    newA = document.createElement("A");
                    //                    newA.setAttribute("title", cam.title);
                    //                    newA.setAttribute("href", "http://images.webcams.travel/webcam/" + cam.webcamid + ".jpg");
                    //                    //                newA.setAttribute("href", cam.url);
                    //                    if ((i + 1) % 2 == 0) {
                    //                        newA.setAttribute("class", "tnMediaLink tnWebcams even");
                    //                    } else {
                    //                        newA.setAttribute("class", "tnMediaLink tnWebcams");
                    //                    }
                    //                    newA.setAttribute("rel", "external");
                    //                } else {
                    //                    var cls;
                    //                    if ((i + 1) % 2 == 0) {
                    //                        cls = "tnMediaLink tnWebcams even";
                    //                    } else {
                    //                        cls = "tnMediaLink tnWebcams";
                    //                    }
                    //                    newA = document.createElement('<a id="webcams_' + i + '" title="' + cam.title + '" href="http://images.webcams.travel/webcam/' + cam.webcamid + '.jpg" class="' + cls + '" rel="external"></a>');
                    //                }
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!

                    var newA, newP;
                    //                if (!sporthub.isIE) {
                    //because IE is a piece of fucking shit and css isn't applied for injected images - dont display them for IE
                    if (entry.thumbnailImg) {
                        var newIMG = document.createElement("IMG");
                        newIMG.setAttribute("src", entry.thumbnailImg);
                        newIMG.setAttribute("class", "list2thumb");
                    }
                    var newA = document.createElement("A");
                    newA.setAttribute("title", entry.title);
                    newA.innerHTML = entry.title;
                    newA.setAttribute("href", "http://" + entry.wikipediaUrl);
                    newA.setAttribute("rel", "external");
                    var newP = document.createElement("P");
                    if (entry.thumbnailImg) {
                        newP.appendChild(newIMG);
                    }
                    newP.innerHTML += entry.summary;
                    newLI.appendChild(newA);
                    newLI.appendChild(newP);
                    //                }
                    //                else{
                    //                    newA = document.createElement('<a title="' + entry.title + '" href="http://' + entry.wikipediaUrl + '" rel="external">' + entry.title + '</a>');
                    //                    newLI.appendChild(newA);
                    //                }

                    newUL.appendChild(newLI);
                }
                target.appendChild(newUL);
                //            //sporthub.utility.qTipInit("div#targetWikipedia");
            }
        }
        else {
            target.innerHTML = "<p>No wikipedia entries are available</p>";
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
                if ((i + 1) % 3 == 0) {
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

    function storeYouTubeThumbs(youtube) {
        var feed = youtube.feed;
        var entries = feed.entry || [];
        var target = document.getElementById("targetVideos");
        if (entries.length == 0) {
            target.innerHTML = "<p>No videos are available on YouTube</p>";
        }
        else {
            $('#targetVideos-btns').show();
            target.innerHTML = "";
            var l = (entries.length > 6) ? 6 : entries.length;
            for (var i = 0; (i < l); i++) {
                var video = entries[i];
                var title = video.title.$t;
                var thumbnailUrl = video.media$group.media$thumbnail[0].url;
                var playerUrl = video.media$group.media$content[0].url;
                var author = video.author[0].name.$t;
                var authorUrl = video.author[0].uri.$t;
                var newA, newIMG, newDiv, newObject, newParam1, newParam2, newEmbed;
//                if (!sporthub.isIE) {
                    newA = document.createElement("A");
                    newA.setAttribute("id", "youtube_" + i);
                    newA.setAttribute("title", title + " by " + author);
                    //                    newA.setAttribute("href", "#div_youtube_" + i);
                    newA.setAttribute("href", playerUrl);
                    if ((i + 1) % 3 == 0) {
                        newA.setAttribute("class", "qt tnMediaLink tnYoutube even");
                    } else {
                        newA.setAttribute("class", "qt tnMediaLink tnYoutube");
                    }
                    newA.setAttribute("rel", "external");
                    newIMG = document.createElement("IMG");
                    newIMG.setAttribute("class", "tnMedia");
                    newIMG.setAttribute("src", thumbnailUrl);
                    newA.appendChild(newIMG);
                    target.appendChild(newA);
//                    newDiv = document.createElement("DIV");
//                    newDiv.setAttribute("style", "display:none");
//                    newDiv.setAttribute("id", "div_youtube_" + i);
//                    newObject = document.createElement("OBJECT");
//                    newObject.setAttribute("width", "425");
//                    newObject.setAttribute("height", "355");
//                    newParam1 = document.createElement("PARAM");
//                    newParam1.setAttribute("movie", playerUrl);
//                    newObject.appendChild(newParam1);
//                    newParam2 = document.createElement("PARAM");
//                    newParam2.setAttribute("wmode", "transparent");
//                    newObject.appendChild(newParam2);
//                    newEmbed = document.createElement("EMBED");
//                    newEmbed.setAttribute("src", playerUrl);
//                    newEmbed.setAttribute("type", "application/x-shockwave-flash");
//                    newEmbed.setAttribute("wmode", "transparent");
//                    newEmbed.setAttribute("width", "425");
//                    newEmbed.setAttribute("height", "355");
//                    newObject.appendChild(newEmbed);
//                    newDiv.appendChild(newObject);
//                    target.appendChild(newDiv);
//                } else {
//                    var cls = "tnMediaLink tnYoutube";
//                    if ((i + 1) % 2 == 0) cls += " even";
//                    //newA = document.createElement('<a id="youtube_' + i + '" title="' + title + " by " + author + '" href="#div_youtube_' + i + '" class="' + cls + '" rel="external"></a>');
//                    newA = document.createElement('<a href="' + playerUrl + '" title="' + title + " by " + author + '" href="#div_youtube_' + i + '" class="' + cls + '" target="_blank"></a>');
//                    newIMG = document.createElement('<img class="tnMedia" src="' + thumbnailUrl + '" />');
//                    newA.appendChild(newIMG);
//                    target.appendChild(newA);
////                    newDiv = document.createElement('<div style="display: none;" id="div_youtube_' + i + '"></div>');
////                    newObject = document.createElement('<object width="425" height="355"><object>');
////                    newParam1 = document.createElement('<param name="movie" value="' + playerUrl + '">');
////                    newParam2 = document.createElement('<param name="wmode" value="transparent">');
////                    newObject.appendChild(newParam1);
////                    newObject.appendChild(newParam2);
////                    newEmbed = document.createElement('<embed height="355" width="425" src="' + playerUrl + '" type="application/x-shockwave-flash" wmode="transparent"/>');
////                    newObject.appendChild(newEmbed);
//                }

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
            //$("a.tnYoutube").fancybox({ 'zoomSpeedIn': 300, 'zoomSpeedOut': 300, 'overlayShow': true, 'hideOnContentClick': false, 'titlePosition': 'inside' });


            if (!sporthub.isIE) {
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
            }
                        
            $('.tnYoutube').qtip({
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

    function getWebcamThumbs(maxRows) {
        var scrWebcams = document.createElement('script');
        scrWebcams.src = 'http://api.webcams.travel/rest?method=wct.webcams.list_nearby&devid=8a855d4962b04ffce9986e3b4ef2dc0e&lat=' + lat + '&lng=' + lng + '&per_page=' + maxRows + '&format=json&callback=storeWebcamsThumbs';
        var scr = document.getElementById('targetWebcams');
        scr.parentNode.insertBefore(scrWebcams, scr);
    }

    function getYouTubeThumbs(maxRows) {
        var scrYouTube = document.createElement('script');
        scrYouTube.src = 'http://gdata.youtube.com/feeds/api/videos?location=' + lat + ',' + lng + '!&location-radius=10km&alt=json-in-script&callback=storeYouTubeThumbs';
        var scr = document.getElementById('targetVideos');
        scr.parentNode.insertBefore(scrYouTube, scr);
    }

    function getWikipediaList(maxRows) {
        //get JSON formatted list of wikipedia entries
        var scrWikipedia = document.createElement('script');
        scrWikipedia.src = 'http://ws.geonames.org/wikipediaBoundingBoxJSON?north=' + (miny - 0.1) + '&south=' + (maxy + 0.1) + '&east=' + (maxx + 0.1) + '&west=' + (minx - 0.1) + '&maxRows=' + maxRows + '&callback=storeWikipediaList';
        var scr4 = document.getElementById('targetWikipedia');
        scr4.parentNode.insertBefore(scrWikipedia, scr4);
    }
    </script>
</asp:Content>
