<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_12">
            <div class="pod2" style="margin-bottom: 140px">
                <p>We are sorry but only Internet Explorer 8 is currently supported. We are working like crazy to make sure IE 7 also is as soon as possible.</p>
                <p>It is important to us that the Snowhub looks, feels, and works the same in every modern web browser; unfortunately, we are a very small team of developers and currently the overhead required to make this site work correctly in Internet Explorer 7 means that it wouldn't be available for launch until after the end of the 2009/2010 Winter season.</p>
                <p>In the meantime this site works perfectly in <a href="http://www.mozilla-europe.org/en/firefox/">Firefox</a>, <a href="http://www.google.com/chrome">Chrome</a>, <a href="http://www.apple.com/safari/download/">Safari</a> and <a href="http://www.opera.com/download/">Opera</a>.</p>
                <p>Thanks for listening,</p>
                <p>The Snowhub Team</p>
                <div class="podbtm" style="padding: 10px 10px 0 10px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Sorry</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Sorry!</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
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

<asp:Content ID="Content6" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>
