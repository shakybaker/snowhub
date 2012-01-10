<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.PostsViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Forums : <%=Model.Forum.ForumName %> : <%=Model.Thread.Title %></asp:Content>

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
        <h2><%=Model.Thread.Title %></h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("Posts", "Forums") %>" method="post">
    <div class="container_12">
    <div class="grid_12 fbUsersList" id="l1">
    <% var ids = string.Empty; %>
    <% int u = 0;%>
    <% var message = string.Empty; var month = string.Empty; var year = string.Empty;%>
    <% string[] months = { "--Month--", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }; %>
<%
    foreach (Sporthub.Model.Post post in Model.Posts)
    {
%>

<a name="<%= post.ID %>" style="visibility: hidden;"></a>
        <div style="clear: both; width: 73px; float: left; padding: 10px 0 20px 10px;">
            <a id="l1-<%= u %>" rel="" class="profile qtu" href="/user/<%=post.User.UserName %>"><img alt="profile pic" src="<%=post.User.GetLargeProfilePic() %>" class="tnMedia"/>
                <div class="profileSummary">
                    <div class="profileSummaryIn">
                        <%=post.User.GetName() %><br />
                        <em><%=post.User.GetSportTypes() %></em>
                    </div>
                </div>
            </a>
            <div class="whobybeak"></div>
        </div>
        <div class="pod2 review" style="float: left; width: 830px; margin-left:5px; margin-bottom: 20px; min-height: 60px; border: 1px solid #ddd;">
            <%=(string.IsNullOrEmpty(post.PostText)) ? string.Empty : post.PostText.Replace(Environment.NewLine, "<br />")%>
            <div class="cb"></div>
            <p style='<%= (post.CreatedDate == post.UpdatedDate)? string.Empty : "margin: 0; " %>font-size: 10px; font-style: italic; float: right; color: #666; margin-top: 40px;'>Created <%=post.GetCreatedTimespan() %><br /><%= (post.CreatedDate == post.UpdatedDate)? string.Empty : string.Format("Last updated {0}", post.GetUpdatedTimespan()) %></p>
        </div>
        <div class="podbtm" style="margin-top: 10px;">
        </div>
<%
        u++;
    } 
%>
    </div>
    <%--<p style="background-color: #DAFF7F; color: #5B7F00; margin: 10px; padding: 10px; font-weight:bold; width: 896px; border: 2px solid #5B7F00">Forum Rules. PLEASE READ: <a style="color: #5B7F00; text-decoration: underline;" href="#" title="[view Site-wide posting Rules, Regulations and Guidelines]">Site-wide posting Rules, Regulations and Guidelines</a></p>
--%>
    </div>
    <div class="container_12">
<% if (Sporthub.Model.UserContext.UserIsLoggedIn()) { %>
        <div class="grid_1">&nbsp;</div>
        <div class="grid_8">
            <div class="pod" style="margin-left: 5px">
                <div class="headwrap">
                    <h3>Reply to this topic?</h3>
                </div>
                <div class="podIn">
                    <textarea cols="68" style="height: 200px; margin: 13px 0 0 0;" id="post" name="post"><%= Model.PostText %></textarea>
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
                <div class="podIn" style="padding-right:10px">
                    <p>Hopefully common-sense will rule your judgement when deciding what to post, but please read <a href="/guidelines">our guidelines</a> first.</p>
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
<% } else { %>
        <div class="grid_12">
            <div class="pod" style="text-align: center">
                    <p><a href="/account/login">Login</a> or <a href="/account/create">Create an account</a> to reply to this topic.</p>
                <div class="cb"></div>
                <div class="podbtm" style="margin-top: 10px;">
                </div>
            </div>
        </div>
<% } %>
    </div>
    <input type="hidden" name="hidForumId" id="hidForumId" value="<%= Model.Forum.ID %>" />
    <input type="hidden" name="hidThreadId" id="hidThreadId" value="<%= Model.Thread.ID %>" />
</form>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</asp:Content>
