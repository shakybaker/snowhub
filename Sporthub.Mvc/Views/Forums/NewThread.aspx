<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ForumViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Forums : <%=Model.Forum.ForumName %> : New Topic</asp:Content>

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
        <h2>New Topic</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("NewThread", "Forums") %>" method="post">
    <div class="container_12">
        <%=Model.ErrorMessage %>
<% if (Sporthub.Model.UserContext.UserIsLoggedIn()) { %>
        <div class="grid_9">
            <div class="pod">
                <div class="headwrap">
                    <h3>Start a New Topic in the <span class="qt brevia" style="font-weight: bold; font-size: 16px; color: #000;" title="<%= Model.Forum.Description%>"><%= Model.Forum.ForumName%></span> Forum</h3>
                </div>
                <div class="podIn">
                    <label for="ThreadTitle">Thread Title</label>
                    <input maxlength="250" id="ThreadTitle" name="ThreadTitle" type="text" class="tb tb690" style="margin-bottom: 10px" value="<%= Model.ThreadTitle %>" />
                    <label for="PostText">Post Text</label>
                    <textarea cols="79" style="height: 200px; margin: 0" id="PostText" name="PostText"><%= Model.PostText %></textarea>
                    <span class="button"><button type="submit" name="submit" value="Post">Post</button></span>
                    <div class="cb"></div>
                </div>
                <div class="podbtm">
                </div>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Posting Guidelines</h3>
                </div>
                <div class="podIn">
                    <p>Hopefully common-sense will rule your judgement when deciding what to post, but please read <a href="/guidelines">our guidelines</a> first.</p>
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
<% } else { %>
        <div class="grid_1">
        &nbsp;
        </div>
        <div class="grid_11">
            <div class="pod2">
                    <p>Connect with Facebook to start a topic.</p>
                    <fb:login-button onlogin="refreshPage();" length="long" size="large"></fb:login-button>
                <div class="cb"></div>
                <div class="podbtm" style="margin-top: 10px;">
                </div>
            </div>
        </div>
<% } %>
    </div>
    <input type="hidden" name="hidForumId" id="hidForumId" value="<%= Model.Forum.ID %>" />
</form>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</asp:Content>
