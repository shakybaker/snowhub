<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Map</asp:Content>

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
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">

<%= Html.ResortNavigation(Model.Resort, "map", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
    <div class="grid_9" style="height: 1px;">&nbsp;
    </div>
    <div class="grid_3" style="height: 1px;">
        <div id="mapOptions">
            <div class="top toggleMarkers">Map Markers
            </div>                
	        <ul id="markerNav">
	            <li>
		            <label class="marker on"><span class="resorts"></span><input id="cbResorts" type="checkbox" class="checkbox" checked="checked" />Resorts</label>
	            </li>
	            <li>
		            <label class="marker <%= ((Model.Querystring == "flickr") ? "on" : "off") %>"><span class="flickr"></span><input id="cbFlickr" type="checkbox" class="checkbox" <%= ((Model.Querystring == "flickr") ? "checked=\"checked\"" : "") %>/>Images</label>
	            </li>
<%--	            <li>
		            <label class="marker off"><span class="panoramio"></span><input id="cbPanoramio" type="checkbox" class="checkbox" />Images (Panoramio)</label>
	            </li>--%>
	            <li>
		            <label class="marker <%= ((Model.Querystring == "youtube") ? "on" : "off") %>"><span class="youtube"></span><input id="cbYoutube" type="checkbox" class="checkbox" <%= ((Model.Querystring == "youtube") ? "checked=\"checked\"" : "") %>/>Videos</label>
	            </li>
<%--	            <li>
		            <a class="markerNavHead" href="#" title="[toggle markers]"><input id="cbWeather" type="checkbox" class="checkbox" /><img src="/assets/images/keylabel_weather.png" alt="[*]" />Weather</a>
	                <ul class="expandedSection">
	                </ul>
	            </li>--%>
	            <li>
		            <label class="marker <%= ((Model.Querystring == "wikipedia") ? "on" : "off") %>"><span class="wikipedia"></span><input id="cbWikipedia" type="checkbox" class="checkbox" <%= ((Model.Querystring == "wikipedia") ? "checked=\"checked\"" : "") %>/>Wikipedia</label>
	            </li>
	            <li>
		            <label class="marker <%= ((Model.Querystring == "webcams") ? "on" : "off") %>"><span class="webcams"></span><input id="cbWebcams" type="checkbox"  class="checkbox" <%= ((Model.Querystring == "webcams") ? "checked=\"checked\"" : "") %>/>Webcams</label>
	            </li>
            </ul>
            <%--<input id="ctrPanoramio" class="textCtr" type="text" value="" />--%>
            <div class="cb"></div>
            <div class="bottom toggleMarkers">&nbsp;
            </div>                
            
        </div>    
    </div>
</div>
  
<div style="float: left; clear: both; width: 100%; margin-top: -1px;">
    <div id="Map" style="width: 100%; height: 400px; z-index: 0; background-color: #333;">
    </div>
</div>
<div id="mediaWrap" style="bottom: 40px; position: absolute; left : 0; background: transparent url(/static/images/bg_info.png) repeat 0 0; width: 100%;">
    <div class="container_12">
        <div id="mapMediaLink" class="grid_12" style="margin: 5px; height: 37px; overflow: hidden;">
            <div id="targetFlickr"></div>
            <div class="cb"></div>
        </div>
    </div>
</div>
<input id="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />


</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA" type="text/javascript"></script>
    <script type="text/javascript" src="/Static/Scripts/dragzoom.js"></script>
    <script type="text/javascript">
        var zoom_lvl = 12;
        var lg_map = true;
        var fetch_all = true;
    </script>
    <script type="text/javascript" src="/Static/Scripts/maps.js"></script>
    <%--<script type="text/javascript" src="/Static/Scripts/sporthub.maps.js"></script>--%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
//            sporthub.maps.onLoad();
            sporthub.resortPages.onLoad();
        });
        
    </script>
</asp:Content>