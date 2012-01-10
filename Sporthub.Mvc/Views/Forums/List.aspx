<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ForumsListViewData>" %>

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
        <h2>Forums</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">

    <div class="grid_12">
<%
    bool isFirst = true;
    int x = 1;
    foreach (Sporthub.Model.Forum forum in Model.Forums)
    {
        if (forum.ParentID == 0)
        {
%>
<%
            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
%>
            </table>
        </div>
        <div class="podbtm" style="margin-bottom: 10px;">
        </div>
<%
            }
%>
        <div class='pod<%= ((forum.IsAdmin) ? " admin" : string.Empty ) %>'>
            <div class="headwrap">
                <h3><%= forum.ForumName%></h3>
            </div>
            <div class="podIn list">
                <table class="table1 top" style="margin-top: 0;">
                    <tr>
                    <th style="width:480px">&nbsp;Forum</th><th class="c" style="width:80px">Topics</th><th style="width:170px">Last Post</th>
                    </tr>
<%
        }
        else
        {
%>
                <tr>
                <td><span><a href="/forums/<%= forum.ID %>" title="[go to forum]"><%= forum.ForumName%></a></span><br /><span style="color: #666;"><%= forum.Description %></span></td><td class="c"><%= forum.Threads.Count %></td>
                <td>
                    <% if (forum.LastPost != null)
                       { %>
                    <a href="/forums/<%= forum.ID %>/thread/<%= forum.LastPost.ThreadID %>"><%=forum.LastPost.Thread.GetCroppedTitle()%></a>
                    <div id="l<%=x %>" class="fbUsersList" style="margin-top: 5px;">
                        <div class="inner">
                            <a class="profile sml qtu" href="/user/<%=forum.LastPost.User.UserName%>"><img alt="profile pic" src="<%=forum.LastPost.User.GetSmallProfilePic() %>" class="tnMedia"/>
                                <div class="profileSummary">
                                    <div class="profileSummaryIn">
                                        <%=forum.LastPost.User.GetName()%><br />
                                        <em><%=forum.LastPost.User.GetSportTypes()%></em>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <span><a title="Jump to this post" href="/forums/<%= forum.ID %>/thread/<%= forum.LastPost.ThreadID %>#<%= forum.LastPost.ID %>"><%= forum.LastPost.GetCreatedTimespan()%></a></span>
                    <% } else { %>
                    &nbsp;
                    <% } %>
                </td>
                </tr>
<%
            
        }
    x++;
    } 
%>
                </table>
            </div>
            <div class="podbtm" style="margin-bottom: 10px;">
            </div>
        </div>
    </div>
    </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>
