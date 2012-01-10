<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.User.RealName %>'s Profile : Edit Resorts</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">

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
        <h2>Your Profile - Edit Resorts</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
       

    <div class="container_12">
        <form class="editProfile" action="<%= Url.Action("EditUserResorts", "Account") %>" method="post">
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3><%= Model.User.RealName %></h3>
                </div>
                <div class="podIn" style="padding-bottom: 20px; padding-right: 10px;">
                    <div class="profileImg">
                        <fb:profile-pic uid="<%= Model.User.FacebookUid %>" size="normal" linked="false"></fb:profile-pic>
                    </div>
                    
                    <%--<p><%= Model.User.GetUserSummary()%></p>--%>

                    <% var checkedIn =  Model.User.CheckIns.Where(x => x.UserID == Model.User.ID && x.IsActive).SingleOrDefault(); %>
                    <% if (checkedIn!=null) { %>
                        <% if (Model.User.IsAuthUserProfile) { %>
                            <p>You are Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong>. <a href="/resorts/checkin/<%= checkedIn.Resort.PrettyUrl %>">Check-In</a> again or <a href="/resorts/checkout/<%= checkedIn.Resort.PrettyUrl %>"> Check-Out</a></p>
                        
                        <% } else { %>
                            <p>Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong></p>
                        <% } %>
                    <% } %>

                    <p><strong>Last seen</strong> <%= Model.User.GetLastVisitTime() %></p>
                    <p><strong>Member</strong> since <%= Model.User.GetMemberSince() %></p>
                    <% if (Model.User.LinkUserSportTypes.Count == 0) { %>
                        <% if (Model.User.IsAuthUserProfile) { %>
                            <p>Are you a Skier? A Snowboarder? Or even both? <a href="/user/edit" title="Edit your Details">Edit your profile</a> to specify.</p>
                        <% } else { %>
                            <p><%= Model.User.RealName %> hasn't specified their sport yet.</p>
                        <% } %>
                    <% } else { %>
                        <% foreach (Sporthub.Model.LinkUserSportType lust in Model.User.LinkUserSportTypes) { %>
                            <% if (lust.SportTypeID == 3) { %>
                            <p><strong><%= lust.ConfigSportType.Collective %></strong></p>
                            <% } else { %>
                            <p><strong><%= lust.ConfigSportType.Collective %></strong> for <%= lust.Seasons %> Season<%= ((lust.Seasons == 1) ? string.Empty : "s")%> (<%= lust.GetSportLevel() %>)</p>
                            <% } %>
                        <% } %>
                    <% } %>
                    <div class="cb"></div>
                </div>
                <a href="/user/" title="" class="profilebutt">Your Profile</a> 
                <a href="/user/edit" title="" class="profilebutt">Edit Details</a> 
                <a href="/user/invite" title="" class="profilebutt">Invite Friends</a> 
                <a href="/user/resorts" title="" class="profilebutt selected">Manage Resorts<span></span></a> 
                <div class="cb"></div>

                <div class="podbtm" style="height: 30px">
                </div>
            </div>
        </div>
        
        <div class="grid_9">
            <div class="pod2">
            this is where you can manage your resorts
                <div class="cb"></div>
            </div>
        </div>
        <div class="clear"></div>
        </form>
    </div>
    <input id="hidUserID" name="hidUserID" type="hidden" value="<%= Model.User.ID.ToString() %>" />
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA"
      type="text/javascript"></script>

<script type="text/javascript" language="javascript">

    var map = new GMap2(document.getElementById("map"));
    var i = 0;
    var markers = [];
    var m = 0;

    $(document).ready(function() {

        $('#map').block({ message: '<img alt="Loading ..." style="width:90%" src="/static/images/anim/loading-horiz-l.gif" />', css: { backgroundColor: 'transparent', borderWidth: '0'} });
        showMap();

        setTimeout('$("#map").unblock();', 1000);
        //mapSetZoomLevel(markers);
    });

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
            icon.iconSize = new GSize(25, 25);
            icon.iconAnchor = new GPoint(12, 25);
            icon.infoWindowAnchor = new GPoint(12, 10);
            addResortMarker(resorts[i], icon, i);
        }
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
            content: "<img class='flag' src='/static/images/flags/" + resort.Code + ".png' alt='[" + resort.Code + "]' />&nbsp;" + resort.Name,
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

</script>
</asp:Content>