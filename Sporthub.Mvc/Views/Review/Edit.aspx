<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortRatingsViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("Edit", "Review") %>" method="post">
    <div class="container_12">
        <div class="grid_9">
            <div class="pod">
                <% var message = string.Empty; var month = string.Empty; var year = string.Empty;%>
                <% string[] months = { "--Month--", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; %>
                <% if (!string.IsNullOrEmpty(Model.CurrentUserRatings.LastVisitDate)) {
                       var tmp = Model.CurrentUserRatings.LastVisitDate.Split('-');
                       month = tmp[1];
                       year = tmp[0];
                   } %>
                <label class="review" for="fave"><span class="num">1</span>Is this resort a favourite?</label>
                <div class="cb"></div>
                <table style="float: left; margin-left: 32px;">
                <tr>
                <td><label><input type="radio" name="fave" value="1" <%=((Model.CurrentUserRatings.IsFavourite) ? "checked='checked' " : string.Empty) %> />Yes</label></td>
                <td style="width: 10px">&nbsp;</td>
                <td><label><input type="radio" name="fave" value="0" <%=((!Model.CurrentUserRatings.IsFavourite) ? "checked='checked' " : string.Empty) %>/>No</label></td>
                </tr>
                </table>

                <div class="cb"></div>
                <label class="review"><span class="num">2</span>What was the date of your last visit?<span class="red">&#42;</span></label>
                <div class="cb"></div>
                <select name="dob_m" style="float: left; margin-left: 32px;">
                    <option value=""<%= ((string.IsNullOrEmpty(month)) ? " selected='selected'" : string.Empty) %>><%= months[0] %></option>
                    <%
                    for (int i = 1; i <= 12; i++) { %>
                    <option value="<%= i %>"<%= ((month == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= months[i] %></option>
                    <% } %>
                </select>
                <select name="dob_y" style="float: left">
                    <option value=""<%= ((string.IsNullOrEmpty(year)) ? " selected='selected'" : string.Empty) %>>--Year--</option>
                    <%
                    for (int i = DateTime.Now.Year; i >= 1960; i--)
                    { %>
                    <%--<option value="<%= i %>"<%= ((Model.User.DobYear == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>--%>
                    <option value="<%= i %>"<%= ((year == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
                    <% } %>
                </select>
                <% 
                    message = string.Empty;
                    foreach (Sporthub.Model.ErrorItem err in Model.ErrorList.Errors)
                    {
                        if (err.FormField == "dob_y")
                            message = err.Message;
                    }
                %>
                <% if (!string.IsNullOrEmpty(message))
                   { %>
                <div style="float: left; clear: both; color: red; font-weight: bold; margin: 0 0 8px 32px;"><%=message %></div>
                <% } %>
                <div class="cb"></div>
                <label class="review"><span class="num">3</span>How do you rate this resort overall? <span style="color: #888; font-weight: normal;">(out of 100, slide to rate)</span><span class="red">&#42;</span></label>
                <% 
                    message = string.Empty;
                    foreach (Sporthub.Model.ErrorItem err in Model.ErrorList.Errors)
                    {
                        if (err.FormField == "hid_0")
                            message = err.Message;
                    }
                %>
                <% if (!string.IsNullOrEmpty(message))
                   { %>
                <div style="float: left; clear: both; color: red; font-weight: bold; margin: 0 0 8px 32px;"><%=message %></div>
                <% } %>
                <table style="margin-left : 32px; float: left; clear: both;">
                    <tr>
                        <td class="l">Resort Rating</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_0"></div></td>
                        <td style="vertical-align: middle; width: 40px;"><span id="val_0" class="rating-score-sm unrated">??</span><input id="hid_0" name="hid_0" type="hidden" value="<%= (Model.CurrentUserRatings.Score == 0) ? string.Empty : Model.CurrentUserRatings.Score.ToString()%>" /></td>
                    </tr>
                </table>
                <label class="review"><span class="num">4</span>How do you rate the resort's features and facilities?</label>
                <table style="margin-left : 32px; float: left; clear: both;">
                    <tr>
                        <td class="l">Lifts</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_1"></div></td>
                        <%--<td style="vertical-align: middle;"><span id="val_1" class="rating-score-sm unrated">??</span><input id="hid_1" name="hid_1" type="hidden" value="" /></td>--%>
                        <td style="vertical-align: middle; width: 40px;"><span id="val_1" class="rating-score-sm unrated">??</span><input id="hid_1" name="hid_1" type="hidden" value="<%= (Model.CurrentUserRatings.LiftRating==0) ? string.Empty : Model.CurrentUserRatings.LiftRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Queues</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_2"></div></td>
                        <td style="vertical-align: middle;"><span id="val_2" class="rating-score-sm unrated">??</span><input id="hid_2" name="hid_2" type="hidden" value="<%= (Model.CurrentUserRatings.QueueRating == 0) ? string.Empty : Model.CurrentUserRatings.QueueRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Convenience</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_3"></div></td>
                        <td style="vertical-align: middle;"><span id="val_3" class="rating-score-sm unrated">??</span><input id="hid_3" name="hid_3" type="hidden" value="<%= (Model.CurrentUserRatings.ConvenienceRating == 0) ? string.Empty : Model.CurrentUserRatings.ConvenienceRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Accomodation</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_4"></div></td>
                        <td style="vertical-align: middle;"><span id="val_4" class="rating-score-sm unrated">??</span><input id="hid_4" name="hid_4" type="hidden" value="<%= (Model.CurrentUserRatings.AccomodationRating == 0) ? string.Empty : Model.CurrentUserRatings.AccomodationRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Food</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_5"></div></td>
                        <td style="vertical-align: middle;"><span id="val_5" class="rating-score-sm unrated">??</span><input id="hid_5" name="hid_5" type="hidden" value="<%= (Model.CurrentUserRatings.FoodRating == 0) ? string.Empty : Model.CurrentUserRatings.FoodRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Snow</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_6"></div></td>
                        <td style="vertical-align: middle;"><span id="val_6" class="rating-score-sm unrated">??</span><input id="hid_6" name="hid_6" type="hidden" value="<%= (Model.CurrentUserRatings.SnowRating == 0) ? string.Empty : Model.CurrentUserRatings.SnowRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Scenery</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_7"></div></td>
                        <td style="vertical-align: middle;"><span id="val_7" class="rating-score-sm unrated">??</span><input id="hid_7" name="hid_7" type="hidden" value="<%= (Model.CurrentUserRatings.SceneryRating == 0) ? string.Empty : Model.CurrentUserRatings.SceneryRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Facilities</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_8"></div></td>
                        <td style="vertical-align: middle;"><span id="val_8" class="rating-score-sm unrated">??</span><input id="hid_8" name="hid_8" type="hidden" value="<%= (Model.CurrentUserRatings.FacilitiesRating == 0) ? string.Empty : Model.CurrentUserRatings.FacilitiesRating.ToString()%>" /></td>
                    </tr>
                    <tr>
                        <td class="l">Nightlife</td>
                        <td style="vertical-align: middle; padding: 5px 10px;"><div class="slider" id="slide_9"></div></td>
                        <td style="vertical-align: middle;"><span id="val_9" class="rating-score-sm unrated">??</span><input id="hid_9" name="hid_9" type="hidden" value="<%= (Model.CurrentUserRatings.NightlifeRating == 0) ? string.Empty : Model.CurrentUserRatings.NightlifeRating.ToString()%>" /></td>
                    </tr>
                </table>
                <div class="cb"></div>
                <label class="review"><span class="num">5</span>Select the word that suits the resort most in each category</label>
                <div class="cb"></div>
                <div class="selResortSuits" style="margin-left: 32px">
                    <h4>Ability level most suits ...</h4>
                    <label><input type="radio" name="suits_ability" value="1"<%= (Model.CurrentUserRatings.ResortSuitsExpert > 0) ?  "checked='checked'" : string.Empty %> />Expert</label>
                    <label><input type="radio" name="suits_ability" value="2"<%= (Model.CurrentUserRatings.ResortSuitsAdvanced > 0) ?  "checked='checked'" : string.Empty %> />Advanced</label>
                    <label><input type="radio" name="suits_ability" value="3"<%= (Model.CurrentUserRatings.ResortSuitsIntermediate > 0) ?  "checked='checked'" : string.Empty %> />Intermediate</label>
                    <label><input type="radio" name="suits_ability" value="4"<%= (Model.CurrentUserRatings.ResortSuitsBeginner > 0) ?  "checked='checked'" : string.Empty %> />Beginner</label>
                </div>
                <div class="selResortSuits">
                    <h4>Nightlife is ...</h4>
                    <label><input type="radio" name="suits_nightlife"<%= (Model.CurrentUserRatings.ResortSuitsLively > 0) ?  "checked='checked'" : string.Empty %> value="5" />Lively</label>
                    <label><input type="radio" name="suits_nightlife"<%= (Model.CurrentUserRatings.ResortSuitsAverage > 0) ?  "checked='checked'" : string.Empty %> value="6" />Average</label>
                    <label><input type="radio" name="suits_nightlife"<%= (Model.CurrentUserRatings.ResortSuitsQuiet > 0) ?  "checked='checked'" : string.Empty %> value="7" />Quiet</label>
                </div>
                <div class="selResortSuits">
                    <h4>Terrian most suits ...</h4>
                    <label><input type="radio" name="suits_terrian" value="8"<%= (Model.CurrentUserRatings.ResortSuitsSkiers > 0) ?  "checked='checked'" : string.Empty %> />Skiers</label>
                    <label><input type="radio" name="suits_terrian" value="9"<%= (Model.CurrentUserRatings.ResortSuitsSnowboarders > 0) ?  "checked='checked'" : string.Empty %> />Snowboarders</label>
                    <label><input type="radio" name="suits_terrian" value="10"<%= (Model.CurrentUserRatings.ResortSuitsBoth > 0) ?  "checked='checked'" : string.Empty %> />Both</label>
                </div>
                <div class="selResortSuits">
                    <h4>Expense level is ...</h4>
                    <label><input type="radio" name="suits_expense" value="11"<%= (Model.CurrentUserRatings.ResortSuitsExpensive > 0) ?  "checked='checked'" : string.Empty %> />Expensive</label>
                    <label><input type="radio" name="suits_expense" value="12"<%= (Model.CurrentUserRatings.ResortSuitsAffordable > 0) ?  "checked='checked'" : string.Empty %> />Affordable</label>
                    <label><input type="radio" name="suits_expense" value="13"<%= (Model.CurrentUserRatings.ResortSuitsCheap > 0) ?  "checked='checked'" : string.Empty %> />Cheap</label>
                </div>
                <div class="cb"></div>
                <label class="review"><span class="num">6</span>Type your review in the space below</label>
                <label style="margin-left: 32px">Review Title or Short Summary<span class="red">&#42;</span></label>
                <% 
                    message = string.Empty;
                    foreach (Sporthub.Model.ErrorItem err in Model.ErrorList.Errors)
                    {
                        if (err.FormField == "title")
                            message = err.Message;
                    }
                %>
                <% if (!string.IsNullOrEmpty(message))
                   { %>
                <div style="float: left; clear: both; color: red; font-weight: bold; margin: 0 0 8px 32px;"><%=message %></div>
                <% } %>
                <input maxlength="250" id="title" name="title" type="text" class="tb tb400" style="margin-left: 30px" value="<%= Model.CurrentUserRatings.Title %>" />
                <label style="margin-left: 32px">Review Text</label>
                <%--<label style="margin-left: 32px">Review Text<span class="red">&#42;</span></label>--%>
                <% 
                    //message = string.Empty;
                    //foreach (Sporthub.Model.ErrorItem err in Model.ErrorList.Errors)
                    //{
                    //    if (err.FormField == "review")
                    //        message = err.Message;
                    //}
                %>
                <% 
                   //if (!string.IsNullOrEmpty(message))
                   //{ %>
                <%--<div style="float: left; clear: both; color: red; font-weight: bold; margin: 0 0 8px 32px;"><%=message %></div>--%>
                <% 
                //} %>
                <textarea cols="74" style="height: 300px; margin: 0 0 20px 30px" id="review" name="review"><%= Model.CurrentUserRatings.ReviewText %></textarea>
                <div class="cb"></div>
                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_3">
            <div class="pod">
                <div class="headwrap">
                    <h3>Resort being reviewed</h3>
                </div>
                <div class="podIn">
                    <p><span class="flag <%= Model.Resort.Country.ISO3166Alpha2%>" style="margin-top: 3px">&nbsp;</span><a href="/resorts/<%= Model.Resort.PrettyUrl %>"><%= Model.Resort.Name.ToString() %></a></p>
                </div>
                <div class="podbtm"></div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>Guide to ratings</h3>
                </div>
                <div class="podIn" style="padding-right:10px">
                    <p>Please add as many ratings as you can - they all help!</p>
                    <p>You must complete the sections marked with a red star (<span style="color: Red">*</span>), otherwise if you don't have an opinion about something then just leave it</p>
                    <p>If you would rather not enter a full review then please enter something in the "Review Title or Short Summary" box (e.g. "<em>Overrated and Expensive</em>" or "<em>Off-piste paradise with no crowds</em>").</p>
                </div>
                <div class="podbtm"></div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>What makes a good review?</h3>
                </div>
                <div class="podIn" style="padding-right:10px">
                    <p>Be detailed and specific. What would you have wanted to know before you visited this resort?</p>
                    <p>Not too short and not too long. Aim for between 75 and 300 words.</p>
                    <p>Be constructive - if you hated it then explain why (e.g. "<em>This place sucks!</em>" isn't very helpful, whereas "<em>This place sucks because the facilities are out-dated</em>" is better. Likewise, "<em>Awesome!</em>" doesn't really give much insight.</p>
                </div>
                <div class="podbtm"></div>
            </div>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_9">
            <div class="pod2" style="margin: 20px 0 40px 0;">
                <table style="width: 670px; float: right;">
                <tr>

               <%-- <% if (Model.HasFacebookPublishPermission)
                   { %>                
                <td><label>Do you want to publish this review to your Facebook Wall?</label></td>
                <td><label><input type="radio" name="publish" value="1" checked="checked" />Yes</label></td>
                <td style="width: 10px">&nbsp;</td>
                <td><label><input type="radio" name="publish" value="0" />No</label></td>
                <% }
                   else
                   { %>
                <td><label>You have publishing to Facebook switched off. You can change this in your Facebook Application settings</label></td>
                <% } %>--%>
                <td style="width: 120px">&nbsp;</td>
                <td><span class="button"><button type="submit" name="submit" value="Save">Save</button></span></td>
                <%--<td><a href="<%= Model.ReturnUrl %>">Cancel</a></td>--%>
                </tr>
                </table>
                
                
                
                
                
                <div class="cb"></div>
            </div>
        </div>
    </div>

<input id="mode" name="mode" type="hidden" value="<%= Model.IsUpdate.ToString() %>" />
<input id="hidResortID" name="hidResortID" type="hidden" value="<%= Model.Resort.ID.ToString() %>" />
<input id="hidPrettyUrl" name="hidPrettyUrl" type="hidden" value="<%= Model.Resort.PrettyUrl %>" />
<input id="hidResortName" name="hidResortName" type="hidden" value="<%= Model.Resort.Name.ToString() %>" />
<input id="hidLng" name="hidLng" type="hidden" value="<%= Model.Resort.Longitude.ToString() %>" />
<input id="hidLat" name="hidLat" type="hidden" value="<%= Model.Resort.Latitude.ToString() %>" />
<input id="hidCountryCode" name="hidCountryCode" type="hidden" value="<%= Model.Resort.Country.ISO3166Alpha2.ToString() %>" />
<input id="hidReturnUrl" name="hidReturnUrl" type="hidden" value="<%= Model.ReturnUrl %>" />
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= ((Model.IsUpdate) ? "Edit" : "Create") %> Review</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2><%= ((Model.IsUpdate) ? "Edit" : "Create") %> your Review</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            sporthub.ratereview.onLoad(); 
        });
        
    </script>
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
