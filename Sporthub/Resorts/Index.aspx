<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" MasterPageFile="~/Shared/Masters/Sporthub.Master" Inherits="Sporthub.Web.Resorts.Index" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<div id="PageHeader" class="container_12">
    <div class="HeaderWrap grid_3">
        <h2 id="PageHeading"><%= vd.Resort.Name %></h2>
    </div>
    <div class="grid_9">
        <ul class="menuTabs tabNav">
        <li class="tab selectedTab"><a title="" href="/Resorts/resort.aspx?rid=<%= vd.Resort.ID %>"><span>Overview</span></a></li>
        <li class="tab"><a title="" href="/Resorts/map.aspx?rid=<%= vd.Resort.ID %>"><span>Map</span></a></li>
        <li class="tab"><a title="/Resorts/reviews.aspx?rid=<%= vd.Resort.ID %>" href="#"><span>Reviews</span></a></li>
        <li class="tab"><a title="/Resorts/accom.aspx?rid=<%= vd.Resort.ID %>" href="#"><span>Accomodation</span></a></li>
<%--                <li class="tab"><a title="/Resorts/services.aspx?rid=153" href="#"><span>Services</span></a></li>
        <li class="tab"><a title="/Resorts/bars.aspx?rid=153" href="#"><span>Bars & Restaurants</span></a></li>
        <li class="tab"><a title="/Resorts/pictures.aspx?rid=153" href="#"><span>Pictures</span></a></li>
        <li class="tab"><a title="/Resorts/videos.aspx?rid=153" href="#"><span>Videos</span></a></li>
        <li class="tab"><a title="/Resorts/stats.aspx?rid=153" href="#"><span>Stats</span></a></li>--%>
        </ul>
    </div>
</div>  

<div style="background-color: Green; position: relative; top: 40px; margin: 0 auto; width: 100px; height: 30px; z-index: 9999; background: Transparent url(/Static/Images/Buttons/heart_g.png) 0 0 repeat;"></div>
<div id="Map" style="width: 100%; height: 400px; clear: both; z-index: 0;">
</div>

<div class="container_12">
    <div id="ResortInfo" class="grid_3">
        <div class="Inner">
            <img alt="[<%= vd.Resort.Name %>]" width="200px" src="/Content/Resorts/153-1.png" /><%= vd.Resort.ISO3166Alpha2 %>
            <p><img alt="[<%= vd.Resort.ISO3166Alpha2 %>]" src="/Static/Images/Flags/<%= vd.Resort.ISO3166Alpha2 %>.png" />&nbsp;<%= ((vd.Resort.Region != null) ? string.Format("{0}, ", vd.Resort.Region.Name) : string.Empty) %><%= vd.Resort.CountryName %></p>
            <table class="Stats">
                <tbody>
                    <tr><th>Vertical Drop</th><td>2000 m</td></tr>
                    <tr><th>Skiable Area</th><td>3000 sq m</td></tr>
                    <tr><th>Season Start</th><td>December</td></tr>
                    <tr><th>Season End</th><td>April</td></tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="Div1" class="grid_7" style="background-color: blue; z-index: 100;">
        <p>this is next to where the info goes</p>
    </div>
    <div class="grid_2">
        <div id="ResortButtons">
            <a href="#" class="btn advertise" title="[advertise on this page]">Advertise</a>
            <a href="/Resorts/review.aspx?rid=153" class="btn review" title="[add a review for Chamonix-Mont-Blanc]">Rate/Review</a>
            <a href="#" class="btn visited" title="[mark Chamonix-Mont-Blanc as 'visited']">Visited</a>
            <a href="#" class="btn favourite" title="[mark Chamonix-Mont-Blanc as a 'favourite]'">Favourite</a>
            <a href="#" class="btn discuss" title="[discuss Chamonix-Mont-Blanc]">Discuss</a>
            <a href="#" class="btn upload" title="[upload photos for Chamonix-Mont-Blanc]">Upload Photos</a>
            <a href="#" class="btn addmarker" title="[add a marker to the Chamonix-Mont-Blanc map]">Add Marker</a>
        </div>
        <div style="background-color: Fuchsia;"><p>la la la blah blah blah la la la blah blah blah</p></div>
    </div>
    <div class="cb"></div>
</div>
<asp:HiddenField ID="hidResortID" runat="server" Value="0" />
<asp:HiddenField ID="hidLat" runat="server" Value="0" />
<asp:HiddenField ID="hidLng" runat="server" Value="0" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/dragzoom.js"></script>
    <script type="text/javascript" src="/Static/Scripts/largemap.js"></script>
</asp:Content>
