<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" ValidateRequest="false" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortViewData>" %>
<%@ Import Namespace="Sporthub.Model" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.Resort.Name %> Reviews</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="<%= Model.Resort.Name %> is a ski resort in <%= Model.Resort.Country.CountryName%>, <%= Model.Resort.ContinentName%>" />
<meta name="keywords" content="<%= Model.Resort.Name %> <%= Model.Resort.Country.CountryName%> <%= Model.Resort.ContinentName%> ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
<ul>
<%
    int i = 1;
    foreach (var bc in Model.Breadcrumbs) {
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

<%= Html.ResortNavigation(Model.Resort, "reviews", Server.HtmlEncode(Request.Url.AbsolutePath))%>

<div class="container_12">
    <div class="grid_12">
        <div class="headwrap large" style="margin-bottom: 20px">
            <h3>Snowhub Member Reviews</h3>
        </div>
    </div>
</div>

<div class="container_12">
    <div class="grid_3">
        <div class="pod">
            <div class="scoreWrap">
                <%= Html.Score(Model.Resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Large, Model.Resort.ScoreCount, Model.ReviewCount, Model.FavouriteCount, Model.VisitedCount )%>
                <div class="cb"></div>
            </div>
        </div>
        <div class="pod">
            <div class="headwrap">
                <h3>Resort Suits ...</h3>
            </div>
            <div class="podIn">
            <%= Html.ResortSuits(Model.Resort) %>
                <div class="cb"></div>
            </div>
        </div>
            
            
        <div class="pod">
            <div class="headwrap">
                <h3>Ratings</h3>
            </div>
            <div class="podIn">
                <table class="table1 nobord">
                <% foreach (var item in Model.Ratings) { %>
                    <tr><th><%=item.Name %></th><td style="width: 30px;"><%= Html.Score(item.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, item.ScoreCount, null, null, null)%></td></tr>
                <%} %>
                </table>
                <div class="cb"></div>
            </div>
        </div>
    </div>
    <div class="grid_9 fbUsersList" id="l1" style="padding-bottom: 20px;" >
        <% var ids = string.Empty; %>
        
        <% int u = 0;%>
        <% var message = string.Empty; var month = string.Empty; var year = string.Empty;%>
        <% string[] months = { "--Month--", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }; %>
        <% if (Model.Reviews.Count > 0) { %>
        <% foreach (var review in Model.Reviews) {%>
        <% if (!string.IsNullOrEmpty(review.LastVisitDate)) {
        var tmp = review.LastVisitDate.Split('-');
        month = tmp[1];
        year = tmp[0];
        }%>
        <div class="whoby">
            <a id="l1-<%=u%>" rel="" class="profile qtu" href="/user/<%=review.User.UserName%>"><img alt="profile pic" src="<%=review.User.GetLargeProfilePic()%>" class="tnMedia"/>
                <div class="profileSummary">
                    <div class="profileSummaryIn">
                        <%=review.User.GetName()%><br />
                        <em><%=review.User.GetSportTypes()%></em>
                    </div>
                </div>
            </a>

            <div class="whobybeak"></div>
        </div>
        <div class="pod2 review" style="float: left; width: 595px; margin-bottom: 20px; border: 1px solid #ddd;">
            <h4 style="margin-bottom: 12px; display: block; float: left; width: 400px;">"<%=review.Title%>"</h4>
            <div class="cb"></div>
            <div class="" style="padding:0;">
            <table class="table1 nobord" style="width:590px; margin: 5px;">
            
                <tbody>
                <tr><th style="width: 90px;">Overall Rating</th><td class="float-score-left" style="width: 50px; text-align: left;"><%=Html.Score(review.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null, null,
                                 null)%></td><td style="width: 10px;">&nbsp;</td><th style="width: 90px;">Lift System</th><td style="width: 30px;"><%=Html.Score(review.LiftRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null, null,
                                 null)%></td><td style="width: 10px;">&nbsp;</td><th style="width: 90px;">Snow</th><td style="width: 30px;"><%=Html.Score(review.SnowRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null, null,
                                 null)%></td></tr>
                <tr><th>A Favourite?</th><td style="width: 20px;"><%=((review.IsFavourite) ? "Yes" : "No")%></td><td style="width: 10px;">&nbsp;</td><th>Queueing</th><td><%=Html.Score(review.QueueRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null,
                                 null, null)%></td><td>&nbsp;</td><th>Scenery</th><td><%=Html.Score(review.SceneryRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null,
                                 null, null)%></td></tr>
                <tr><th>Last Visit</th><td><%=((review.HasVisited)
                           ? months[int.Parse(month)] + " '" + year.Substring((year.Length - 2), 2)
                           : "Not Visited")%></td><td>&nbsp;</td><th>Convenience</th><td><%=Html.Score(review.ConvenienceRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0,
                                 null, null, null)%></td><td>&nbsp;</td><th>Accomodation</th><td><%=Html.Score(review.AccomodationRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0,
                                 null, null, null)%></td></tr>
                <tr><th></th><td></td><td>&nbsp;</td><th>Food</th><td><%=Html.Score(review.FoodRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null, null,
                                 null)%></td><td>&nbsp;</td><th>Facilities</th><td><%=Html.Score(review.FacilitiesRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null,
                                 null, null)%></td></tr>
                <tr><th></th><td></td><td>&nbsp;</td><th>Nightlife</th><td><%=Html.Score(review.NightlifeRating.ToString(), Sporthub.Model.Enumerators.ScoreSize.Small, 0, null,
                                 null, null)%></td><td>&nbsp;</td><th></th><td></td></tr>
                </tbody>
            </table>
            </div>
            <div class="cb"></div>

            <p style="border-top: 2px solid #ddd; border-bottom: 1px dotted #ddd;  padding: 10px 0 20px 0; margin-bottom: 10px;">
            <%=(string.IsNullOrEmpty(review.ReviewText))
                          ? string.Empty
                          : review.ReviewText.Replace(Environment.NewLine, "<br />")%>
            </p>
            
        
        
        
            <div class="cb"></div>
            <div class="podbtm" style="margin-top: 10px;">
                <% if (review.ReviewUsefulnessFeedback.Count() > 0) { %>
                <p style="padding-top: 5px; width: 100%; text-align: right;"><%=review.ReviewUsefulnessFeedback.Where(x => x.IsUseful).Count() %> out of <%=review.ReviewUsefulnessFeedback.Count%> people found this review helpful&nbsp;&nbsp;</p>
                <% } %>
                <p style='<%=(review.CreatedDate == review.UpdatedDate) ? string.Empty : "margin: 0; "%>width: 200px; font-size: 10px; font-style: italic; float: left; color: #666;'>Created <%=review.CreatedDate%><br /><%=(review.CreatedDate == review.UpdatedDate)
                          ? string.Empty
                          : string.Format("Last updated {0}", review.UpdatedDate)%>
                </p>
                <div id="feedback-btns">
                    <a id="flag-<%=review.ID %>" title="Flag this review as inappropriate" class="smlbutt<%= (UserContext.UserIsLoggedIn()) ? " feedbck" : " not-logged-in" %>" href="#<%= (UserContext.UserIsLoggedIn()) ? string.Empty : "loginPopup" %>" style="float: right; margin-bottom: 0;">Flag</a>
                    <a id="no-<%=review.ID %>" class="smlbutt<%= (UserContext.UserIsLoggedIn()) ? " feedbck" : " not-logged-in" %>" href="#<%= (UserContext.UserIsLoggedIn()) ? string.Empty : "loginPopup" %>" style="float: right; margin-bottom: 0;">No</a>
                    <a id="yes-<%=review.ID %>" class="smlbutt<%= (UserContext.UserIsLoggedIn()) ? " feedbck" : " not-logged-in" %>" href="#<%= (UserContext.UserIsLoggedIn()) ? string.Empty : "loginPopup" %>" style="float: right; margin-bottom: 0;">Yes</a>
                    <span>Was this review helpful to you?</span>
                </div>
            </div>
        </div>
        <div class="cb"></div>
            <% u++; %>
        <%} %>
        
        <% if (Model.IsSingleReview) { %>
            <a class="smlbutt" href="/resorts/<%= Model.Resort.PrettyUrl %>/reviews/" style="margin: 0 10px 20px 75px;">Show all reviews for <%=Model.Resort.Name %></a>
            <div class="cb"></div>
        <%} %>
        <div class="grid_12">
            <div class="pod">
                <p style="margin-left: 75px"><strong>Have you been to <%=Model.Resort.Name %>? Let everyone know your opinion.</strong></p>
                <% if (!Sporthub.Model.UserContext.UserIsLoggedIn()) { %>
                <p style="margin-left: 75px"><a href="/account/login">Login</a> or <a href="/account/create">Create an account</a> to add a Review.</p>
                <%} else {%>
                <a class="big" href="/review/<%= Model.Resort.PrettyUrl  %>?ReturnUrl=<%=Server.HtmlEncode(Request.Url.AbsolutePath) %>" style="margin: 10px 0 10px 75px;">Add a Review &raquo;</a>
                <%} %>
                <div class="cb"></div>
                <div class="podbtm" style="margin-top: 10px;">
                </div>
            </div>
        </div>

        <% } else {%>
        <div class="grid_12">
            <div class="pod">
                <p style="margin-left: 75px"><strong>No reviews have been added yet for <%=Model.Resort.Name %>. Be the first!</strong></p>
                <% if (!Sporthub.Model.UserContext.UserIsLoggedIn()) { %>
                <p style="margin-left: 75px"><a href="/account/login">Login</a> or <a href="/account/create">Create an account</a> to add a review and share your opinion.</p>
                <%} else {%>
                <a class="big" href="/review/<%= Model.Resort.PrettyUrl  %>?ReturnUrl=<%=Server.HtmlEncode(Request.Url.AbsolutePath) %>" style="margin: 10px 0 10px 75px;">Add a Review &raquo;</a>
                <%} %>
                <div class="cb"></div>
                <div class="podbtm" style="margin-top: 10px;">
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <div class="cb"></div>
</div>

<input id="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />

</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            sporthub.resortPages.onLoad();
            
            $(".feedbck").click(function() {
                $("#feedback-btns").html("<span><img alt='...' src='/Static/Images/Anim/load-s.gif' /> Saving ...</span>");
                $("#feedback-btns").addClass("loading");
                $.getJSON("/Ajax/GiveReviewFeedback", { feedback: $(this).attr('id') }, function(data) {
                    $("#feedback-btns").removeClass("loading");
                    if (data.Result == true) {
                        $("#feedback-btns").html("<span>Thankyou for your feedback!</span>");
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                });
            });
        });
    </script>
</asp:Content>
