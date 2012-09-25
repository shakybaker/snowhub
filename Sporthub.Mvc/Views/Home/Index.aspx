<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Home.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.HomeViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Snow Sports Community</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more." />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
<% var ids = string.Empty; %>
<% int u = 0;%>

<% if (Sporthub.Model.UserContext.CurrentUser == null) { %>
<div id="splash" style="width: 100%; margin: 0 auto; height: 250px; clear: both; z-index: 0; background: #666 url(/static/images/splash.png) 10% 0 no-repeat;">
</div>

<div class="container_12">
    <div id="SplashInfo" class="grid_3">
        <div class="Inner">
            <p class="brevia strap">Join for free and you can Rate &amp; Review ski resorts, upload pictures and interact with other snow sports enthusiasts</p>
            <a class="big" style="margin-top: 0;" href="/account/create">Create an Account  &raquo;</a>
            <%--<a class="fb-connect qt" title="Sign-up by Connecting your Facebook account to the Snowhub. You can post reviews to your Facebook wall & Check-In to resorts" href="#" onclick="return connectToFacebook();">Connect with Facebook</a>--%>
           <%-- <form method="post" action="/user" id="fb_form">
            </form>
                <script type="text/javascript">
                    function connectToFacebook() {
                        FB.login(function(response) {
                          if (response.session) {
                            if (response.perms) {
                              // user is logged in and granted some permissions.
                              // perms is a comma separated list of granted permissions
                              
                                $.getJSON("/Account/ConnectNewToFacebook?", { facebookUid: response.session.uid, accessToken: response.session.access_token }, function(o) {
                                    if (o.Success == true) {
                                        $("#fb_form").submit();
                                        //window.location = "/user";
                                    }
                                });
                            } else {
                              alert("You need to grant permissions to the Snowhub if you want to register using your Facebook account.");
                              // user is logged in, but did not grant any permissions
                            }
                          } else {
                              alert("You need to grant permissions to the Snowhub if you want to register using your Facebook account.");
                            // user is not logged in
                          }
                        }, {perms:'email,publish_stream,offline_access'});
                        return false;
                    }
                </script>--%>
<%--            
            <a class="big" href="#" onclick="return shareWithFacebook();">Connect with Facebook</a>
            <script type="text/javascript">
                window.fbAsyncInit = function () {
                    FB.init({ appId: '154838327354', status: true, cookie: true,
                        xfbml: true
                    });
                };
                (function () {
                    var e = document.createElement('script');
                    e.async = true;
                    e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
                    document.getElementById('fb-root').appendChild(e);
                } ());

                function shareWithFacebook() {
                    FB.ui({ method: 'stream.publish',
                        message: 'I reviewed Morzine ski resort on the Snowhub',
                        user_message_prompt: 'This is a Prompt',
                        attachment: {
                            name: 'Morzine, France',
                            caption: 'thesnowhub.com',
                            description: ('88/100 - "Great place for restaurants - rubbish for snowboarding"'),
                            href: 'www.thesnowhub.com/resorts/morzine/reviews/4'
                            //media: [{ 'type': 'image', 'src': 'This is an image', 'href': 'This is a href'}]
                        },
                        action_links: [
                        { text: 'Add a Review', href: 'http://www.thesnowhub.com/resorts/morzine' }
                    ]
                    },
                    function (response) {
                        if (response && response.post_id) {
                            // Do some custom action after the user successfully posts this to their wall
                            alert('Thanks for sharing!');
                        }
                    }
                );
                    return false;
                }
            </script>
--%>
        </div>
    </div>
    <div class="grid_9" style="margin-top:20px;">
    </div>
    <div class="cb"></div>
</div>
<% } %>
<% if (Sporthub.Model.UserContext.CurrentUser == null) { %>
<div class="container_12">
<% } else {%>
<div class="container_12" style="padding-top: 20px;">
<% } %>
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
                <h3>Resorts</h3>
            </div>
            <div class="headwrap2">
                <h4>Highest Rated</h4>
            </div>
            <div class="podIn list" style="padding:0 0 8px;">
                <ul class="list1">
                <% foreach (Sporthub.Model.Resort resort in Model.TopRatedResorts) { %>
                    <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><%= Html.Score(resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null) %></a></li>

                <%} %>
                </ul>
            </div>
            <div class="headwrap2">
                <h4>Most Visited</h4>
            </div>
            <div class="podIn list">
                <ul class="list1">
                <% foreach (Sporthub.Model.Resort resort in Model.MostVisitedResorts) { %>
                    <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><span class="counter"><%= resort.VisitedCount %></span></a></li>

                <%} %>
                </ul>
            </div>
            <div class="podbtm"></div>
        </div>
    </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Latest Reviews</h3>
                </div>
                <div id="l3" class="podIn list fbUsersList">
                    <ul class="list3 reviews">
                    <% u = 0;%>
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
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_3">
            <div class="callout business">
                <h3>Business Owners!</h3>
                <p>Promote your business for free on the Snowhub</p>
                <a href="/advertise" class="big">Find out more &raquo;</a>
                <div class="cb"></div>
            </div>
            
            
            
            
            
            
            <div class="podIn nograd">
                <a href="http://www.skiline.co.uk" target="_blank"><img alt="skiline" src="../../Static/Images/Ads/skiline_200.png" /></a>
            </div>
            <div class="pod" style="margin-top: 20px">
                <div class="headwrap">
                    <h3>Twitter/snowhub</h3>
                </div>
                <div class="podIn">
                    <div id="tweet">
                        
                    </div>
                    <a href="http://twitter.com/snowhub" title="" class="smlbutt" style="margin: 10px 0 0 10px;">Follow us on Twitter &raquo;</a> 
                    <div class="cb"></div>
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
<%--        <div class="pod">
            <div class="headwrap">
                <h3>Facebook Fans</h3>
            </div>
            <div class="podIn">
--%>
                <%--<fb:fan profile_id="154838327354" stream="0" connections="10" width="200" css="http://www.thesnowhub.com/static/styles/facebook.css?151109_4"></fb:fan>
                <iframe frameborder="0" scrolling="no" class="FB_SERVER_IFRAME" src="http://www.connect.facebook.com/connect/connect.php?api_key=e2411c2c547d4f2ec4bde40ba8c42102&amp;channel_url=http%3A%2F%2Fwww.thesnowhub.com%2F%3Ffbc_channel%3D1&amp;id=154838327354&amp;name=&amp;width=345&amp;connections=12&amp;stream=&amp;css=http://www.thesnowhub.com/static/styles/facebook.css?151109_4" allowtransparency="true" name="fbfanIFrame_0" style="border: medium none ; width: 200px; height: 220px;"/>
            </div>
            <div class="podbtm"></div>
        </div>
        --%>
    </div>
    <div class="cb" style="height: 100px;"></div>
</div>
</div>
</div>

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            sporthub.utility.twitter();

            $.getJSON("/Ajax/SnowReport", { resortID: 'x' }, function (data) {
                if (data.Result == true) {
                    alert(data.Data);
                }
                else {
                    alert(data.ErrorMessage);
                }
            });
        });
      
    </script>
</asp:Content>
