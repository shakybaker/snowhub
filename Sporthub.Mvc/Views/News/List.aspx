<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.NewsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : News</asp:Content>

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
        <h2>News</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
<%
    int i = 0;
    foreach (Sporthub.Model.NewsFeed newsFeed in Model.NewsFeeds)
    {
        if ((i == 0) || ((i % 4) == 0))
        {
            if (i > 0)
            {
%>
    </div>
<%
            }
%>
    <div class="container_12">
<%
        }
%>
        <div class="grid_3">
            <div class="pod feed">
                <div class="headwrap">
                    <h3 style="background-image: url(<%= ((newsFeed.UseFavicon) ? newsFeed.FaviconURL : "/static/images/feed.png") %>);"><%= newsFeed.FeedName %></h3>
                </div>
                <div class="podIn list" id="TestFeed_<%=i %>">
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
        <input type="hidden" id="feedUrl_<%=i %>" value="<%=newsFeed.FeedURL%>" />
<%
        i++;
    } 
%>
        <input type="hidden" id="feedCount" value="<%=Model.NewsFeeds.Count%>" />
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
    </div>--%>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function() {

        sporthub.news.feedCount = $("#feedCount").val();
        sporthub.news.onLoad();

    });
</script>
</asp:Content>
