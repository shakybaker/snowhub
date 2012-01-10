<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Invite Friends</asp:Content>
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
        <h2>Invite your Friends</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3><%= Model.User.RealName %></h3>
                </div>
                <div class="podIn" style="padding-bottom: 20px; padding-right: 10px;">
                    <div class="profileImg">
                        <fb:profile-pic uid="<%= Model.User.FacebookUid %>" size="normal" linked="false"></fb:profile-pic>
                    </div>
                </div>    
                <a href="/user/" title="" class="profilebutt">Your Profile</a> 
                <a href="/user/edit" title="" class="profilebutt">Edit Details</a> 
                <a href="/user/invite" title="" class="profilebutt selected">Invite Friends<span></span></a> 
                <div class="cb"></div>
                <div class="podIn" style="padding-right: 10px;">
                    <% var checkedIn =  Model.User.CheckIns.Where(x => x.UserID == Model.User.ID && x.IsActive).SingleOrDefault(); %>
                    <% if (checkedIn!=null) { %>
                        <% if (Model.User.IsAuthUserProfile) { %>
                            <p>You are Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong>. <a href="/resorts/checkin/<%= checkedIn.Resort.PrettyUrl %>">Check-In</a> again or <a href="/resorts/checkout/<%= checkedIn.Resort.PrettyUrl %>"> Check-Out</a></p>
                        
                        <% } else { %>
                            <p>Checked-In to <strong><a href="/resorts/<%= checkedIn.Resort.PrettyUrl %>"><%= checkedIn.Resort.Name%></a></strong></p>
                        <% } %>
                    <% } %>

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

                <div class="podbtm" style="height: 30px">
                </div>
            </div>
        </div>

        <div class="grid_9">
            <div class="pod2">
<%--                            
                        exclude_ids="<?php echo $excludeIds; ?>"
--%>
<fb:serverfbml style="width: 660px;"> 
    <% if (Page.Request.Url.Host.ToLower().Contains("localhost") || Page.Request.Url.Host.ToLower().Contains("127.0.0.1")) {%>
    <script type="text/fbml"> 
    <fb:fbml> 
        <fb:request-form
                action="http://127.0.0.1:61657/user"
                method="POST"
                invite="true"
                type="Snowhub"
                content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more.
                        <fb:req-choice url='http://127.0.0.1:61657/register'
                        label='Register'
                        /> 
                " > 
                <fb:multi-friend-selector
                        showborder="false"
                        actiontext="Invite your friends to try the Snowhub."
                        rows="3"
                /> 
        </fb:request-form> 
    </fb:fbml>
    </script> 
    <%} else {%>    
    <script type="text/fbml"> 
    <fb:fbml> 
        <fb:request-form
                action="http://www.thesnowhub.com/user"
                method="POST"
                invite="true"
                type="Snowhub"
                content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more.
                        <fb:req-choice url='http://www.thesnowhub.com/register'
                        label='Register'
                        /> 
                " > 
                <fb:multi-friend-selector
                        showborder="false"
                        actiontext="Invite your friends to try the Snowhub."
                        rows="3"
                /> 
        </fb:request-form> 
    </fb:fbml>
    </script> 
    <%}%>    
</fb:serverfbml>
            
                <div class="cb"></div>
                <div class="podbtm">
                </div>
            </div>
        </div>
    </div>
    <input id="hidUserID" name="hidUserID" type="hidden" value="<%= Model.User.ID.ToString() %>" />
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA"
      type="text/javascript"></script>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {

    });

</script>
</asp:Content>