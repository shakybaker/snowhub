<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.User.GetName() %>'s Profile</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server"></asp:Content>
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
    <% if (Model.User.IsAuthUserProfile)
       { %>
        <h2>Your Profile</h2>
    <% } else { %>
        <h2><%= Model.User.GetName()%>'s Profile</h2>
    <% } %>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    
    <div class="container_12">
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3><%= Model.User.GetName() %></h3>
                </div>
                <div class="podIn" style="padding-bottom: 20px; padding-right: 10px;">
                    <div class="profileImg" style="margin-bottom: 20px">
                        <img alt="profile pic" src="<%=Model.User.GetLargeProfilePic()%>" />
                    </div>
                </div>    
                <a href="/user/edit" title="" class="smlbutt">Edit Details</a> 
                <% if (Model.User.IsAuthUserProfile) { %>
                <% if (!string.IsNullOrEmpty(Model.User.FacebookUid)) { %>
                <a class="smlbutt" href="">Remove Account from Facebook</a>
                <a class="big" href="#" onclick="return showCheckins();">Show Checkins</a>
                <div style="clear: both; float: left;" id="checkins"></div>
                <% } else {%>
                <a class="big" href="#" onclick="return connectToFacebook();">Connect with Facebook</a>
                <% } %>
                <script type="text/javascript">
                    function connectToFacebook() {
                        FB.login(function(response) {
                          if (response.session) {
                            if (response.perms) {
                              // user is logged in and granted some permissions.
                              // perms is a comma separated list of granted permissions
                              
                                $.getJSON("/Account/ConnectExistingToFacebook?", { facebookUid: response.session.uid, accessToken: response.session.access_token }, function(o) {
                                    if (o.Success == true) {
                                        window.location = "/user";
                                    }
                                });
                            } else {
                              alert("user is logged in, but did not grant any permissions");
                              // user is logged in, but did not grant any permissions
                            }
                          } else {
                            alert("user is not logged in");
                            // user is not logged in
                          }
                        }, {perms:'publish_stream,offline_access,user_checkins'});
                        return false;
                    }
                    function showCheckins() {
                        $("#checkins").load("/Account/ShowCheckins", { }, function(o) {
                            if (o.Success == true) {
                            }
                        });
                        return false;
                    }
                </script>
                <%--<a href="/user/invite" title="" class="profilebutt">Invite Friends</a>--%> 
                <% } %>
                <div class="cb"></div>
                <div class="podIn" style="padding-right: 10px;">
                    <%--<p><%= Model.User.GetUserSummary()%></p>--%>

                    <% var checkedIn =  Model.User.CheckIns.Where(x => x.UserID == Model.User.ID && x.IsActive).SingleOrDefault(); %>
                    <% if (checkedIn!=null) { %>
                        <% if (Model.User.IsAuthUserProfile) { %>
                            <p>You are Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong>. <a href="/resorts/checkin/<%= checkedIn.Resort.PrettyUrl %>">Check-In</a> again or <a href="/resorts/checkout/<%= checkedIn.Resort.PrettyUrl %>"> Check-Out</a></p>
                        
                        <% } else { %>
                            <p>Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong></p>
                        <% } %>
                    <% } %>

                    <% if (!Model.User.IsAuthUserProfile) { %>
                    <p><strong>Last seen</strong> <%= Model.User.GetLastVisitTime() %></p>
                    <% } %>
                    <p><strong>Member</strong> since <%= Model.User.GetMemberSince() %></p>
                    <% if (Model.User.LinkUserSportTypes.Count == 0) { %>
                        <% if (Model.User.IsAuthUserProfile) { %>
                            <p>Are you a Skier? Snowboarder? Or even both? <a href="/user/edit" title="Edit your Details">Edit your profile</a> to specify.</p>
                        <% } else { %>
                            <p><%= Model.User.GetName() %> hasn't specified their sport yet.</p>
                        <% } %>
                    <% } else { %>
                        <% foreach (Sporthub.Model.LinkUserSportType lust in Model.User.LinkUserSportTypes) { %>
                            <% if (lust.SportTypeID == 3) { %>
                            <p><strong><%= lust.ConfigSportType.Collective %></strong></p>
                            <% } else { %>
                            <p><strong><%= lust.ConfigSportType.Collective %></strong> <%--for <%= lust.Seasons %> Season<%= ((lust.Seasons == 1) ? string.Empty : "s")%>--%> (<%= lust.GetSportLevel() %>)</p>
                            <% } %>
                        <% } %>
                    <% } %>
                    <% if (!string.IsNullOrEmpty(Model.User.GetAge())) { %>
                    <p><strong>Age</strong> <%=Model.User.GetAge() %></p>
                    <% } %>
                    <% if (!string.IsNullOrEmpty(Model.User.GetLocation())) { %>
                    <p><strong>Based in </strong> <%=Model.User.GetLocation()%></p>
                    <% } %>
                    <div class="cb"></div>
                </div>

                <div class="podbtm">
                </div>
            </div>
            <%--<p><strong><%= lust.ConfigSportType.Collective %><%= ((string.IsNullOrEmpty(lust.ConfigSportType.Alias)) ? string.Empty : string.Format(" ({0})", lust.ConfigSportType.Alias))%></strong> for <%= lust.Seasons %> Season<%= ((lust.Seasons == 1) ? string.Empty : "s")%>, <%= lust.GetSportLevel() %></p>--%>

<%--            <div class="pod">
                <div class="headwrap">
                    <h3>Friend List test</h3>
                </div>
                <div class="podIn">
                    <p>These are Facebook friends who are also on the Snowhub</p>
                    <div id="friends"></div>
                </div>
                <div class="podbtm">
                    <a href="#">xxx</a>
                </div>
            </div>
--%><%--            
    <% if (Model.User.IsAuthUserProfile) { %>
            <div class="pod">
            <div class="podIn">
                <div class="headwrap">
                    <h3>Contacts</h3>
                </div>
                <table class="table1">
                <tr><td style="width: 30px;"><img alt="[]" style="height: 20px; width: 20px; display: block; border: 3px solid #222; margin: 3px 0;" src="/uploads/6/tn/bigears.png" /></td><td style="vertical-align: middle;"><a href="#">bigears</a></td><td style="vertical-align: middle; width: 50px;" class="btn"><a href="#" title="[send a messgage]">Message</a></td></tr>
                <tr><td style="width: 30px;"><img alt="[]" style="height: 20px; width: 20px; display: block; border: 3px solid #222; margin: 3px 0;" src="/uploads/7/tn/noddy.png" /></td><td style="vertical-align: middle;"><a href="#">noddy</a></td><td style="vertical-align: middle; width: 50px;" class="btn"><a href="#" title="[send a messgage]">Message</a></td></tr>
                </table>
                
                <div class="podbtm">
                    <a href="#" title="[click here view all contacts]">View All</a>
                </div>
                <div class="cb"></div>
            </div>
            </div>
    <% } %>
--%>    
        </div>
        
            <%--<div class="pod">
                <h3><img alt="Recent Pictures" src="/Ajax/getHeader.aspx?text=Recent Pictures&size=11" /></h3>
    <%
        if (vd.Threads.Count == 0)
        {
            if (vd.IsAuthUserProfile)
            {
    %>
                <p>You haven't uploaded any Pictures yet.</p>
    <%
            }
            else
            {
    %>
                <p><%= Model.User.UserName%> hasn't uploaded any Pictures yet.</p>
    <%
            }
        }
        else
        {
    %>
    <%
            int p = 1;
            foreach (Snowhub.Model.Picture picture in Model.User.Pictures)
            {
                if (p < 4)
                {
    %>
                <img style="display: block; float: left; margin: 10px 0 10px 11px; background-color: #282828; padding: 5px;" alt="[<%= picture.Title%>" src="/uploads/<%= picture.CreatedUserID%>/tn/<%= picture.Filename%>" />
    <%
                    p++;
                }
            }
    %>
    <%
        }
    %>
                <div class="podbtm">
                    <a href="#" title="[click here to upload a picture]">Upload a Picture</a><a href="#" title="[click here to manage your pictures]">Manage Your Pictures</a>
                </div>
            </div>
        </div>--%>

        <div class="grid_9">
            <% if (Sporthub.Model.SessionContext.CurrentSession.IsRedirectedsFromActivation) { %>
            <div class="callout info">
                <h3>Welcome to the Snowhub!</h3>
                <p>This is your Profile page. You can add resorts that you have been to and manage them here. Please <a href="/user/edit">edit your Profile information</a>, you can upload a profile picture, let people know where you are from and whether you are a Skier, Snowboarder or both.</p>
                <div class="cb"></div>
            </div>
            <% } %>
           
            <div class="pod">
                <div class="headwrap">
    <% if (Model.User.IsAuthUserProfile) { %>
                    <h3>Your Resorts</h3>
    <% } else { %>
                    <h3><%= Model.User.GetName()%>'s Resorts</h3>
    <% } %>
                </div>
        <% if (Model.User.LinkResortUsers.Count > 0) { %>
                <div class="podIn nograd" style="margin-top: 10px;">
                    <div id="map" style="height: 350px; width: 680px;">
                    </div>
                </div>
        <% } %>
                <div class="podIn list">
        <% if (Model.User.LinkResortUsers.Count == 0) {
                if (Model.User.IsAuthUserProfile) { %>
                    <p>You haven't marked any Resorts as 'visited' yet.</p>
                    <p><a href="/resorts/">Browse to a resort that you have been to</a> - you can then mark it as 'visited', add it as a favourite, or even rate and review it.</p>
        <%
                }
                else
                {
        %>
                    <p><%= Model.User.UserName%> hasn't marked any Resorts as 'visited' yet.</p>
        <%
                }
            }
            else
            {
        %>
                    <table class="table1 profile" style="width: 700px; margin: 10px 0">
                        <thead>
                        <tr>
                        <th>Resort</th>
                        <th>Last Visited</th>
                        <th style="text-align: center;">Review</th>
                        <%if (Model.User.IsAuthUserProfile) { %>
                        <th class="editCol" style="width: 45px">&nbsp;</th>
                        <% } %>
                        </tr>
                        </thead>
                        <tbody>
        <%
                foreach (Sporthub.Model.LinkResortUser lru in Model.User.LinkResortUsers)
                {
        %>
                            <tr>
                                <td><img src="/static/images/flags/<%= lru.Resort.Country.ISO3166Alpha2%>.png" alt="<%= lru.Resort.Country.ISO3166Alpha2%>" /><a href="/resorts/<%= lru.Resort.PrettyUrl %>" title="go to <%= lru.Resort.Name%>">&nbsp;<%= lru.Resort.Name%></a>
                                <% if (lru.IsFavourite) { %>
                                <img class="fv" alt="favourite" src="/static/images/fave_on.png" />
                                <% } %>
                                </td>
                                <td>
                                <% if (!string.IsNullOrEmpty(lru.LastVisitDate)) { %>
                                <%--<img style="display: block; margin: 4px 3px 0 0; float: left;" alt="." src="/static/images/tick.png" />&nbsp;--%>
                                <%=lru.GetLastVisitedDate() %>
                                <% } %>
                                </td>
                                <%--<td class="c"><img alt="[TODO:]" src="/static/images/buttons/star_g.png" /></td>--%>
                                <td class="userResortsScore">
                                <% if (!string.IsNullOrEmpty(lru.Title)) { %>
                                <span style="text-align: right; font-style: oblique;">&quot;<%= lru.GetCroppedTitle() %>&quot;</span>&nbsp;&nbsp;
                                <% } %>
                                <%= Html.Score(lru.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 1, null, null, null) %></td>
                                <%if (Model.User.IsAuthUserProfile) { %>
                                <td class="editCol"><a class="smlbutt" href="/review/<%=lru.Resort.PrettyUrl %>?ReturnUrl=/user/">Edit</a></td>
                                <% } %>
                            </tr>
        <%
                }
        %>
                        </tbody>
                    </table>
        <%
            }
        %>
                        <div class="cb"></div>

                </div>
                <div class="podbtm">
                </div>
            </div>
            <%--<div class="pod">
                <h3><img alt="Recent Posts" src="/Ajax/getHeader.aspx?text=Recent Posts&size=11" /></h3>
    <%
        if (vd.Threads.Count == 0)
        {
            if (vd.IsAuthUserProfile)
            {
    %>
                <p>You have made no forum posts yet.</p>
    <%
            }
            else
            {
    %>
                <p><%= Model.User.UserName%> has made no forum posts yet.</p>
    <%
            }
        }
        else
        {
    %>
                <table class="table1">
                    <thead>
                        <tr><th>Thread</th><th>Post Date</th><th>#Posts since</th></tr>
                    </thead>
                    <tbody>
    <%
        foreach (Snowhub.Model.Thread thread in vd.Threads)
        {
    %>
                    <tr><td><%= thread.Title%></td><td>TODO:</td><td>TODO:</td></tr>
    <%
        }
    %>
                    </tbody>
                </table>
    <%
        }
    %>
                <div class="podbtm">
                    <a href="#" title="[click here to start a thread]">Start a Thread</a><a href="#" title="[click here to view all threads you have contributed to]">View Your Threads</a>
                </div>
            </div>--%>
        <% if (Model.User.IsAuthUserProfile) { %>
<%--        <div class="pod">
            <div class="headwrap">
                <h3>Your friends on the Snowhub</h3>
            </div>
            <div class="podIn">
                <div id="l1" class="fbUsersList">
                    <%=Html.UserList
                    (
                        Model.Friends, 
                        1, 
                        Sporthub.Model.Enumerators.ProfileThumbSize.Large,
                        "<p>None of your Facebook friends are members of the Snowhub. <a href=\"/user/invite\">Invite some now</a>.</p>", 
                        "inner", 
                        string.Empty
                    ) 
                    %>
					</div>
                <div class="cb"></div>
            </div>
            <div class="podbtm"></div>
        </div>
--%>        
<% } %>
        </div>
        <%--<div class="grid_2">
            <div class="pod">
                <div class="podIn nograd">
            <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
                <img alt="ad" src="../../Static/Images/Ads/120x600Skyscraper.gif" />
            <% } else { %>
                <script type="text/javascript"><!--
                    google_ad_client = "pub-2930781007842752";
                    /* 120x600, created 25/01/10 */
                    google_ad_slot = "3661795231";
                    google_ad_width = 120;
                    google_ad_height = 600;
                //-->
                </script>
                <script type="text/javascript"
                src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                </script>
            <% } %>
                </div>
            </div>
        </div>--%>
        <div class="clear"></div>
    </div>
    <input id="hidUserID" name="hidUserID" type="hidden" value="<%= Model.User.ID.ToString() %>" />
    <input id="showMap" name="showMap" type="hidden" value="<%= (Model.User.LinkResortUsers.Count > 0) ? "true" : "false" %>" />
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA"
      type="text/javascript"></script>

<script type="text/javascript" language="javascript">

//    window.onload = function() {
//        FB_RequireFeatures(
//                ["XFBML"],
//                function() {
//                    FB.Facebook.init(sh_facebook.fbApiKey, sh_facebook.fbXdPath);
//                }
//            );
//    }

    var map;
    var isShowMap = document.getElementById("showMap").value;
    if (isShowMap == 'true') {
        map = new GMap2(document.getElementById("map"));
    }
    var i = 0;
    var markers = [];
    var m = 0;

    $(document).ready(function() {

        if (isShowMap == 'true') {
            $('#map').block({ message: '<img alt="Loading ..." style="width:90%" src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
            showMap();

            setTimeout('$("#map").unblock();', 1000);
        }
    });

    function mapSetZoomLevel(markers) {
        var bounds = new GLatLngBounds;
        for (var i = 0; i < markers.length; i++) {
            bounds.extend(markers[i].getLatLng());
        }
        var zoom = map.getBoundsZoomLevel(bounds);
        if (zoom > 2)
            zoom = zoom - 2;
        map.setZoom(zoom);
        map.panTo(bounds.getCenter());
    }

    function showMap() {
        map.addControl(new GScaleControl());
        map.addMapType(G_PHYSICAL_MAP);
        map.setMapType(G_PHYSICAL_MAP);
        map.addControl(new GLargeMapControl3D(), new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(7, 7)));
        var geocoder = new GClientGeocoder();
        map.setCenter(new GLatLng(0, 0), 1);

        var scrResorts = document.createElement('script');

        var uid = document.getElementById("hidUserID").value;

        //$.getJSON("/Ajax/GetResortsByUserId", { id: uid }, storeResorts);
        $.getJSON("/Ajax/GetResortsByUserId/"+uid, storeResorts);
    }

    function storeResorts(Objects) {
        resorts = Objects.Data;
        for (var i = 0; i < resorts.length; i++) {
            var icon = new GIcon();
            icon.image = "/Static/Images/Markers/marker_resort_small.png";
            icon.iconSize = new GSize(34, 54);
            icon.iconAnchor = new GPoint(17, 54);
            icon.infoWindowAnchor = new GPoint(17, 0);
            addResortMarker(resorts[i], icon, i);
        }
        mapSetZoomLevel(markers);
    }

    function addResortMarker(resort, myIcon, i) {
        var marker = new GMarker(new GLatLng(resort.Latitude, resort.Longitude), { id: "mkr_" + i, icon: myIcon });
        map.addOverlay(marker, myIcon);

        GEvent.addListener(marker, "mouseover", function() {
            marker.setImage("/static/images/markers/marker_resort_small_ovr.png");
        });
        GEvent.addListener(marker, "mouseout", function() {
            marker.setImage("/static/images/markers/marker_resort_small.png");
        });
        GEvent.addListener(marker, "click", function() {
            window.location = "/resorts/" + resort.PrettyUrl;
        });

        $("img#mtgt_mkr_" + i).qtip({
            content: "<span class='flag-m " + resort.Code + "'>&nbsp;</span>" + resort.Name,
            position: {
                corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
            },
            show: { effect: 'fade' },
            style: {
                name: 'dark',
                tip: true,
                border: {
                    width: 2,
                    radius: 5
                }
            }
        });
        markers[m] = marker;
        m++;

    }


    function isdefined(variable) {
        return (typeof (variable) !== 'undefined');
    }


</script>
</asp:Content>