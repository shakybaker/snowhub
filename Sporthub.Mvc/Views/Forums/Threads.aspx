<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ThreadsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Forums</asp:Content>

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
        <h2><%= Model.Forum.ForumName%> Forum</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
    <div class="grid_12 fbUsersList" id="l1">
        <div class='pod'>
            <div class="headwrap">
                <h3><%= Model.Forum.ForumName%><span class="sml brevia"><%= Model.Forum.Description%></span></h3>
            </div>
            <div class="podIn list">
<% if (Model.Forum.Threads.Count == 0) { %>
            <p style="margin: 0; padding: 20px 0 0 20px;">There are currently no topics in this Forum.</p>
<% } else { %>
                <table class="table1" style="margin-top: 0;">
                    <tr>
                    <th style="width:380px">&nbsp;Topic</th><th class="c" style="width:40px">Replies</th><th style="width:170px">Started by</th><th style="width:170px">Last Post</th>
                    </tr>
    <% var ids = string.Empty; %>
    <% int u = 0;%>
    <% var message = string.Empty; var month = string.Empty; var year = string.Empty;%>
    <% string[] months = { "--Month--", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }; %>
<%
    foreach (Sporthub.Model.Thread thread in Model.Forum.Threads)
    {
%>
                    <tr>
                    <td><span><a href="/forums/<%= Model.Forum.ID %>/thread/<%= thread.ID %>" title=""><%= thread.Title %></a></span>
                    </td>
                    <td class="c" style="width:40px"><%= thread.Posts.Count %></td>
                    <td>
                        <div style="margin-top: 10px;">
                            <div class="inner">
                                <a class="profile sml qtu" href="/user/<%=thread.StartedBy.UserName%>"><img alt="profile pic" src="<%=thread.StartedBy.GetSmallProfilePic() %>" class="tnMedia"/>
                                    <div class="profileSummary">
                                        <div class="profileSummaryIn">
                                            <%=thread.StartedBy.GetName()%><br />
                                            <em><%=thread.StartedBy.GetSportTypes()%></em>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <span><%= thread.GetCreatedTimespan() %></span>
                    </td>
                    <td>
                        <% u++; %>
                        <% var lastPost = thread.Posts.OrderByDescending(x => x.CreatedDate).Take(1).ToArray(); %>
                        <div style="margin-top: 10px;">
                            <div class="inner">
                                <a class="profile sml qtu" href="/user/<%=lastPost[0].User.UserName%>"><img alt="profile pic" src="<%=lastPost[0].User.GetSmallProfilePic() %>" class="tnMedia"/>
                                    <div class="profileSummary">
                                        <div class="profileSummaryIn">
                                            <%=lastPost[0].User.GetName()%><br />
                                            <em><%=lastPost[0].User.GetSportTypes()%></em>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <span><%= lastPost[0].GetUpdatedTimespan()%></span>
                    </td>
                    </tr>
<%
    u++;
    } 
%>
                </table>
                <input type="hidden" id="l1-idList" name="l1-idList" class="idList" value="<%= ((ids.Length>0) ? ids.Substring(0, (ids.Length-1)) : "0") %>" />
<% } %>
                <div class="cb"></div>
            </div>
            <div class="podbtm">
            </div>
        </div>
    </div>
    <%--<p style="background-color: #DAFF7F; color: #5B7F00; margin: 10px; padding: 10px; font-weight:bold; width: 896px; border: 2px solid #5B7F00">Forum Rules. PLEASE READ: <a style="color: #5B7F00; text-decoration: underline;" href="#" title="[view Site-wide posting Rules, Regulations and Guidelines]">Site-wide posting Rules, Regulations and Guidelines</a></p>
--%>
    </div>
    <div class="container_12">
        <div class="grid_12">
            <div class="pod" style="text-align: center">
<% if (Sporthub.Model.UserContext.UserIsLoggedIn()) { %>
                <p><a href="/newthread/<%= Model.Forum.ID %>" title="" class="big" style="margin:  10px 0 0 10px;">New Topic</a></p>
<% } else { %>
                <p><a href="/account/login">Login</a> or <a href="/account/create">Create an account</a> to start a new topic.</p>
<% } %>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</asp:Content>
