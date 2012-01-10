<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.LocationName %></asp:Content>

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
    <div class="grid_6">
        <h2><%= Model.LocationName %></h2>
    </div>
    <div class="grid_6">
        <div class="now-viewing">Viewing Resorts in <a href="#"><%= Model.LocationName %><span></span></a></div>
        <div id="jump-to">
            <div id="jump-to-inner">
                <p>All continents:</p>
                <ul class="list1">
                    <% foreach (var continent in Model.Continents) { %>
                    <li><a href="/resorts/<%=continent.PrettyUrl %>/list"><%=continent.ContinentName %></a></li>
                    <% } %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Countries</h3>
                </div>
<%--                <h3>
<%
    if (Model.LocationLevel == Sporthub.Model.Enumerators.LocationLevel.Country)
    {
%>
                    <img alt="<%= Model.Country.ISO3166Alpha2 %>" src="/static/images/flags/<%= Model.Country.ISO3166Alpha2 %>.png" />
<%
    }
%>                <img alt="<%= Model.LocationName %>" src="/Utils/GetHeader.aspx?text=<%= Model.LocationName %>&size=13" />
                </h3>
--%>
<%--                <table class="table1">
                <tbody id="targetTBODY">
                    <tr class="nobord"><td id="statusMessage" colspan="2"><img alt="Loading..." src="/Static/Images/Anim/load-s.gif" /></td></tr>
                </tbody>
                </table>
--%>            
                <ul class="list1">
                <% if (Model.LocationLevel == Sporthub.Model.Enumerators.LocationLevel.World)
                   { %>
                <% foreach (Sporthub.Model.Continent continent in Model.Continents)
                   { %>
                <% var m = 0; %>
                <li id="<%= continent.PrettyUrl %>"><a href="/resorts/<%= continent.PrettyUrl %>/list"><%= continent.ContinentName%></a></li>
                <% m++; %>
                <% } %>
                <% } else { %>
                <% foreach (Sporthub.Model.Country country in Model.Countries)
                   { %>
                <% var m = 0; %>
                <li id="Li1"><a href="/resorts/<%= country.PrettyUrl %>/list"><img class="flag" alt="[<%= country.ISO3166Alpha2 %>]" src="/static/images/flags/<%= country.ISO3166Alpha2 %>.png" />&nbsp;<%= country.CountryName%></a></li>
                <% m++; %>
                <% } %>
                <% } %>
                </ul>
                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_9">
            <div class="pod">
                <div id="map" style="border:10px solid #CCCCCC; height:370px; width:680px;"></div>
            </div>
        </div>
        <input id="LocationLevel" name="LocationLevel" type="hidden" value="<%= ((int)Model.LocationLevel).ToString() %>" />
        <input id="LocationName" name="LocationName" type="hidden" value="<%= Model.LocationName %>" />
        <input id="LocationUrl" name="LocationUrl" type="hidden" value="<%= Model.LocationUrl %>" />
    </div>
    <div class="cb"></div>
    
    <div class="container_12">
        <%--<div class="grid_3" style="margin-top: -310px">--%>
        <div class="grid_3">
        &nbsp;
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Latest Reviews</h3>
                </div>
                <div id="l1" class="podIn list fbUsersList">
                    <ul class="list3 reviews">
                    <% int u = 0;%>
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
            <div class="pod">
                <div class="headwrap">
                    <h3>Most Visited Resorts</h3>
                </div>
                <div class="podIn list" style="padding-bottom: 8px;">
                    <ul class="list1">
                    <% foreach (Sporthub.Model.Resort resort in Model.MostVisitedResorts) { %>
                        <li><a href="/resorts/<%= resort.PrettyUrl %>" title="go to <%= resort.Name%>"><span class="flag <%= resort.Country.ISO3166Alpha2%>">&nbsp;</span><%= resort.Name%><span class="counter"><%= resort.VisitedCount %></span></a></li>

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
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Highest Rated Resorts</h3>
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
            <div class="cb"></div>
        </div>

        <input id="Hidden1" name="LocationLevel" type="hidden" value="<%= ((int)Model.LocationLevel).ToString() %>" />
        <input id="Hidden2" name="LocationName" type="hidden" value="<%= Model.LocationName %>" />
        <input id="Hidden3" name="LocationUrl" type="hidden" value="<%= Model.LocationUrl %>" />
    </div>
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
