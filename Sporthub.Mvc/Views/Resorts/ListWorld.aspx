<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Resorts</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more." />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
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
    <div class="grid_12">
        <h2 class="tk-brevia">Worldwide Resorts</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <% var ids = string.Empty; %>
    <div class="container_12">
<%--        <div class="grid_3">
        &nbsp;
        </div>--%>
        <div class="grid_12">
            <div class="pod">
                    <%--<div id="Div1" style="position: relative; height: 412px; width: 920px; top: 0; left: 0; background: transparent url(/static/images/map-world.png) 0 0 no-repeat;">
                    <ul>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 75px; left: 140px; width: 45px;" href="/resorts/canada/list">Canada</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 120px; left: 110px; width: 77px;" href="/resorts/united-states/list">United States</a></li>

                        <li><a class="smlbutt" style="float: none; position: absolute; top: 290px; left: 190px; width: 30px;" href="/resorts/chile/list">Chile</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 290px; left: 260px; width: 57px;" href="/resorts/argentina/list">Argentina</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 90px; left: 400px; width: 42px;" href="/resorts/europe/list">Europe</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 110px; left: 740px; width: 35px;" href="/resorts/japan/list">Japan</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 300px; left: 750px; width: 55px;" href="/resorts/australia/list">Australia</a></li>
                        <li><a class="smlbutt" style="float: none; position: absolute; top: 350px; left: 790px; width: 73px;" href="/resorts/new-zealand/list">New Zealand</a></li>
                    </ul>
                    </div>--%>
                <%--<div id="map" style="border:10px solid #CCCCCC; height:500px; width:920px;"></div>--%>
                <div id="map" style="border:10px solid #CCCCCC; height:370px; width:920px;"></div>

            </div>
        </div>
    </div>
<%--    <div class="container_12">
        <div class="grid_3"><p>&nbsp;</p>
        </div>
        <div class="grid_9" style="margin-bottom: 20px;">
            <script type="text/javascript">
                google_ad_client = "pub-2930781007842752";
                google_ad_width = 468;
                google_ad_height = 60;
                google_ad_format = "468x60_as";
                google_ad_type = "image";
                google_color_border = "FFFFFF";
                google_color_bg = "0000FF";
                google_color_link = "FFFFFF";
                google_color_text = "000000";
                google_color_url = "008000";
            </script>
            <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
        </div>
    </div>
--%>
    <div class="container_12">
        <%--<div class="grid_3" style="margin-top: -310px">--%>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Countries</h3>
                </div>
                <% 
                    var countryName = string.Empty;
                    var isFirst = true; 
                %>
                
                <% foreach (Sporthub.Model.Continent continent in Model.Continents) { %>

                    <div class="headwrap2">
                        <h4 id="<%= continent.PrettyUrl %>"><%= continent.ContinentName%></h4>
                    </div>
                    <div class="podIn list">
                        <ul class="list1">
                    <%  
                        var m = 0;
                        var countries = from c in Model.Countries
                        where c.ContinentID == continent.ID
                        select new Sporthub.Model.Country
                        {
                            ID = c.ID,
                            CountryName = c.CountryName,
                            PrettyUrl = c.PrettyUrl,
                            ISO3166Alpha2 = c.ISO3166Alpha2,
                            
                        };
                    %>
                    <% foreach (Sporthub.Model.Country country in countries) { %>
                            <li id="Li1"><a href="/resorts/<%= country.PrettyUrl %>/list"><span class="flag <%= country.ISO3166Alpha2%>">&nbsp;</span><%= country.CountryName%></a></li>
                    <% m++; %>
                    <% } %>
                        </ul>
                    </div>
                <% } %>


                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Latest Reviews</h3>
                </div>
                <div id="l1" class="podIn list fbUsersList">
                    <% ids = string.Empty; %>
                    <ul class="list3 reviews">
                    <% int u = 0;%>
                    <% foreach (Sporthub.Model.LinkResortUser review in Model.LatestReviews) { %>
                    <% ids += string.Format("{0},", review.User.FacebookUid);%>

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
                                <a class="profile sml qtu" href="/user/<%=review.User.UserName%>"><img alt="profile pic" src="<%=review.User.GetSmallProfilePic() %>" class="tnMedia"/>
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
                    <input type="hidden" id="l1-idList" name="l1-idList" class="idList" value="<%= ((ids.Length>0) ? ids.Substring(0, (ids.Length-1)) : "0") %>" />
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Resorts</h3>
                </div>
                <div class="headwrap2">
                    <h4>Most Visited Resorts</h4>
                </div>
                <div class="podIn list" style="padding-bottom: 8px;">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.MostVisitedResorts) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><span class="counter"><%= resort.VisitedCount %></span></a></li>

                    <%} %>
                    </ul>
                </div>
                <div class="headwrap2">
                    <h4>Highest Rated Resorts</h4>
                </div>
                <div class="podIn list">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.TopRatedResorts) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><%= Html.Score(resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null)%></a></li>

                    <%} %>
                    </ul>
                </div>
                <div class="podbtm"></div>
            </div>
<%--        <div class="grid_2" style="margin-top: -290px">
            <div class="podIn nograd">
                <img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />
            </div>
--%>        
            <div class="cb"></div>
        </div>
        <input id="LocationLevel" name="LocationLevel" type="hidden" value="1" />
        <input id="LocationName" name="LocationName" type="hidden" value="<%= Model.LocationName %>" />
        <input id="LocationUrl" name="LocationUrl" type="hidden" value="<%= Model.LocationUrl %>" />
    </div>
    <div class="grid_3">
            <a class="big" href="/advertise" style="margin: 0 0 20px 2px; padding: 12px; float: left;">Advertise on the Snowhub</a>
            <div style="margin-bottom: 20px; width: 220px; float: left; clear: both;">
                <div class="podIn nograd">
                    <a href="http://www.skiline.co.uk" target="_blank"><img alt="skiline" src="../../Static/Images/Ads/skiline_200.png" /></a>
                </div>
                <div class="podIn nograd">
                    <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                        <img alt="ad" width="200px" height="200px" src="/Static/Images/Ads/125x125Squarebutton.gif" />
                    <% } else { %>
                    <script type="text/javascript">
                        google_ad_client = "pub-2930781007842752";
                        google_ad_width = 200;
                        google_ad_height = 200;
                        google_ad_format = "200x200_as";
                        google_ad_type = "image";
                        google_color_border = "FFFFFF";
                        google_color_bg = "0000FF";
                        google_color_link = "FFFFFF";
                        google_color_text = "000000";
                        google_color_url = "008000";
                    </script>
                    <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
                    <% } %>
                </div>
            </div>
    </div>
    <div class="cb"></div>
<%--    <div class="container_12">
        <div class="grid_3">
            <div class="pod">
                <h4><img alt="Featured" src="/Utils/GetHeader.aspx?text=Featured&size=13" /></h4>
                <table class="table1">
                <tbody>
                </tbody>
                </table>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <h4><img alt="Most Visited" src="/Utils/GetHeader.aspx?text=Most Visited&size=13" /></h4>
                <table class="table1">
                <tbody>
                </tbody>
                </table>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <h4><img alt="Highest Rated" src="/Utils/GetHeader.aspx?text=Highest Rated&size=13" /></h4>
                <table class="table1">
                <tbody>
                </tbody>
                </table>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <h4><img alt="Highest Rated" src="/Utils/GetHeader.aspx?text=Highest Rated&size=13" /></h4>
                <table class="table1">
                <tbody>
                </tbody>
                </table>
            </div>
        </div>
    </div>
--%>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/extinfowindow.js"></script>
    <script type="text/javascript" src="/Static/Scripts/labeledmarker.js"></script>
    <script type="text/javascript" src="/Static/Scripts/mapClustered.js"></script>
</asp:Content>
