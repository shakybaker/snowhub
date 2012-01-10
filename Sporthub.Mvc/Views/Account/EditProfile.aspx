<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.User.GetName() %>'s Profile : Edit Details</asp:Content>
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
        <h2>Your Profile - Edit Details</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <form class="editProfile" action="<%= Url.Action("Edit", "Account") %>" method="post">
       


    <div class="container_12">
        
        <div class="grid_12">
            <div class="pod">
                <div class="headwrap">
                    <h3>Profile Picture</h3>
                </div>
                <div class="podIn">
                    <div class="profileImg" style="margin: 0 20px 20px 0; float: left;">
                        <img alt="profile pic" src="<%=Model.User.GetLargeProfilePic() %>" />
                    </div>
                    
                    <div class="browseforpic">
                        <a href="/user/addpicture" title="" class="smlbutt"><%= (Model.User.HasProfilePicture) ? "Change" : "Add"%> Profile Picture</a> 
                    </div>
                    
                    <div class="cb">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container_12">
            <div class="pod">
                <div class="headwrap">
                    <h3>Basic Details</h3>
                </div>
                <div class="podIn">
                    <%--<table>
                    <tr>
                        <td>Email</td>
                        <td><%= Html.TextBox("Email", ViewData.Model.User.Email, new { @class = "tb tb200 readonly", @readonly="readonly" })%></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Gender</td>
                        <td></td>
                    </tr>
                    </table>--%>
                    <table class="table2">
                    <tr>
                        <td style="width: 200px;"><label for="RealName">RealName</label></td>
                        <td>
                        <%= Html.TextBox("RealName", ViewData.Model.User.RealName, new { @class = "tb tb200", maxlength = 20 })%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><label for="Gender">Gender</label></td>
                        <td>
                            <table>
                                <tr><td><input value="M" id="gender1" name="Gender" type="radio" <%= ((Model.User.Sex == 'M') ? "checked='checked' " : "") %>/></td><td><label for="gender1">Male</label></td><td><input value="F" id="gender2" name="Gender" type="radio" <%= ((Model.User.Sex == 'F') ? "checked='checked' " : "") %>/></td><td><label for="gender2">Female</label></td><td><input value="U" id="gender3" name="Gender" type="radio" <%= ((Model.User.Sex == 'U') ? "checked='checked' " : "") %>/></td><td><label for="gender3">Unknown</label></td></tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><label for="Email">Email</label></td>
                        <td>
		                    <%=Html.TextBox("Email", ViewData.Model.User.Email, new { @readonly = "readonly", @class = "tb readonly tb200" })%><a href="#" class="fl cb" style="margin-top: 5px" id="changeEmail">Change Email</a>
		                </td>
                        <td>Not made public - only used for notifications, recommendations, etc.</td>
                    </tr>
                    <tr>
                        <td><label for="UsualCity">Town/City</label></td>
                        <td>
                        <%= Html.TextBox("UsualCity", ViewData.Model.User.UsualCity, new { @class = "tb tb200" })%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><label for="Country">Country</label></td>
                        <td>
		                    <select name="Country">
		                    <%  string countryoutput = string.Empty;
                              countryoutput += "<option value=\"0\" " + ((ViewData.Model.User.UsualCountryID == 0) ? "selected" : "") + ">-- Select --</option>";
                              foreach (var country in ViewData.Model.Countries)
                              {
                                 countryoutput += "<option value=\"" + country.ID + "\"" + ((ViewData.Model.User.UsualCountryID == country.ID) ? "selected" : "") + ">" + country.CountryName + "</option>"; 
                                } %>
		                     <%=countryoutput%>
		                    </select>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Date of birth</td>
                        <td>

                            <select name="dob_d">
	                            <option value="">Day</option>
                                <%
                                string dayOptions = string.Empty;
                                for (int i = 1; i <= 31; i++)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobDay == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= dayOptions %>
                            </select>
                            <select name="dob_m">
	                            <option value="">Month</option>
                                <%
                                string monthOptions = string.Empty;
                                for (int i = 1; i <= 12; i++)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobMonth == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= monthOptions%>
                            </select>
                            <select name="dob_y">
	                            <option value="" selected="selected">Year</option>
                                <%
                                string yearOptions = string.Empty;
                                for (int i = (DateTime.Now.Year - 10); i >= 1900; i--)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobYear == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= yearOptions%>
                            </select>
                        </td>
                        <td>Only displayed as age in years, not the full date</td>
                    </tr>
                </table>

                    <div class="cb">
                    </div>
                </div>
            </div>
            <div class="pod">
                <div class="headwrap">
                    <h3>Snow Sports</h3>
                </div>
                <div class="podIn">
                    <label>Which snow sports do you take part in? <span style="font-weight: normal; color: #888;">(Tick all that apply)</span></label>
                    <div class="cb"></div>
                    <% var snowboardTop = Model.Sports.Where(x => x.ID == 1).SingleOrDefault(); %>
                    <% var skiTop = Model.Sports.Where(x => x.ID == 2).SingleOrDefault(); %>
                    <% var neitherTop = Model.Sports.Where(x => x.ID == 3).SingleOrDefault(); %>
                        
    <%--                    <% var skiing = Model.Sports.Where(x => x.ParentID == 2).OrderBy(x => x.Sequence).ToList(); %>
                        <% var snowboarding = Model.Sports.Where(x => x.ParentID == 2).OrderBy(x => x.Sequence).ToList(); %>
    --%>                    
                    <table class="sporttypes">
                    <tbody>
                    <tr>
                    <td>
                    <label><input id="cb-1" name="cb-1" type="checkbox" <%= Model.User.HasSportType(1) %>/><%= snowboardTop.Name%></label>
                    </td>
<%--                    <td>
                        <label style="padding-right: 5px;">Seasons</label>
                        <select id="seasons-1" name="seasons-1" class="narrow">
                            <option value="0"<%= Model.User.IsSeasonSelected(1,0) %>>0</option>
                            <option value="1"<%= Model.User.IsSeasonSelected(1,1) %>>1</option>
                            <option value="2"<%= Model.User.IsSeasonSelected(1,2) %>>2</option>
                            <option value="3"<%= Model.User.IsSeasonSelected(1,3) %>>3</option>
                            <option value="4"<%= Model.User.IsSeasonSelected(1,4) %>>4</option>
                            <option value="5"<%= Model.User.IsSeasonSelected(1,5) %>>5</option>
                            <option value="6"<%= Model.User.IsSeasonSelected(1,6) %>>6</option>
                            <option value="7"<%= Model.User.IsSeasonSelected(1,7) %>>7</option>
                            <option value="8"<%= Model.User.IsSeasonSelected(1,8) %>>8</option>
                            <option value="9"<%= Model.User.IsSeasonSelected(1,9) %>>9</option>
                            <option value="10"<%= Model.User.IsSeasonSelected(1,10) %>>10</option>
                            <option value="11"<%= Model.User.IsSeasonSelected(1,11) %>>11</option>
                            <option value="12"<%= Model.User.IsSeasonSelected(1,12) %>>12</option>
                            <option value="13"<%= Model.User.IsSeasonSelected(1,13) %>>13</option>
                            <option value="14"<%= Model.User.IsSeasonSelected(1,14) %>>14</option>
                            <option value="15"<%= Model.User.IsSeasonSelected(1,15) %>>15</option>
                            <option value="16"<%= Model.User.IsSeasonSelected(1,16) %>>16</option>
                            <option value="17"<%= Model.User.IsSeasonSelected(1,17) %>>17</option>
                            <option value="18"<%= Model.User.IsSeasonSelected(1,18) %>>18</option>
                            <option value="19"<%= Model.User.IsSeasonSelected(1,19) %>>19</option>
                            <option value="20"<%= Model.User.IsSeasonSelected(1,20) %>>20</option>
                        </select>
                    </td>--%>
                    <td>
                        <label style="padding-right: 5px;">Level</label>
                        <select id="experience-1" name="experience-1" class="narrow">
                            <option value="1"<%= Model.User.IsLevelSelected(1,1) %>>Beginner</option>
                            <option value="2"<%= Model.User.IsLevelSelected(1,2) %>>Intermediate</option>
                            <option value="3"<%= Model.User.IsLevelSelected(1,3) %>>Expert</option>
                            <option value="4"<%= Model.User.IsLevelSelected(1,4) %>>Advanced</option>
                        </select>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <label><input id="cb-2" name="cb-2" type="checkbox" <%= Model.User.HasSportType(2) %>/><%= skiTop.Name%></label>
                    </td>
<%--                    <td>
                        <label style="padding-right: 5px;">Seasons</label>
                        <select id="seasons-2" name="seasons-2" class="narrow">
                            <option value="0"<%= Model.User.IsSeasonSelected(2,0) %>>0</option>
                            <option value="1"<%= Model.User.IsSeasonSelected(2,1) %>>1</option>
                            <option value="2"<%= Model.User.IsSeasonSelected(2,2) %>>2</option>
                            <option value="3"<%= Model.User.IsSeasonSelected(2,3) %>>3</option>
                            <option value="4"<%= Model.User.IsSeasonSelected(2,4) %>>4</option>
                            <option value="5"<%= Model.User.IsSeasonSelected(2,5) %>>5</option>
                            <option value="6"<%= Model.User.IsSeasonSelected(2,6) %>>6</option>
                            <option value="7"<%= Model.User.IsSeasonSelected(2,7) %>>7</option>
                            <option value="8"<%= Model.User.IsSeasonSelected(2,8) %>>8</option>
                            <option value="9"<%= Model.User.IsSeasonSelected(2,9) %>>9</option>
                            <option value="10"<%= Model.User.IsSeasonSelected(2,10) %>>10</option>
                            <option value="11"<%= Model.User.IsSeasonSelected(2,11) %>>11</option>
                            <option value="12"<%= Model.User.IsSeasonSelected(2,12) %>>12</option>
                            <option value="13"<%= Model.User.IsSeasonSelected(2,13) %>>13</option>
                            <option value="14"<%= Model.User.IsSeasonSelected(2,14) %>>14</option>
                            <option value="15"<%= Model.User.IsSeasonSelected(2,15) %>>15</option>
                            <option value="16"<%= Model.User.IsSeasonSelected(2,16) %>>16</option>
                            <option value="17"<%= Model.User.IsSeasonSelected(2,17) %>>17</option>
                            <option value="18"<%= Model.User.IsSeasonSelected(2,18) %>>18</option>
                            <option value="19"<%= Model.User.IsSeasonSelected(2,19) %>>19</option>
                            <option value="20"<%= Model.User.IsSeasonSelected(2,20) %>>20</option>
                        </select>
                    </td>--%>
                    <td>
                        <label style="padding-right: 5px;">Level</label>
                        <select id="experience-2" name="experience-2" class="narrow">
                            <option value="1"<%= Model.User.IsLevelSelected(2,1) %>>Beginner</option>
                            <option value="2"<%= Model.User.IsLevelSelected(2,2) %>>Intermediate</option>
                            <option value="3"<%= Model.User.IsLevelSelected(2,3) %>>Expert</option>
                            <option value="4"<%= Model.User.IsLevelSelected(2,4) %>>Advanced</option>
                        </select>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <label><input id="cb-3" name="cb-3" type="checkbox" <%= Model.User.HasSportType(3) %>/><%= neitherTop.Name %></label>
                    </td>
                    </tr>
                    </tbody>
                    </table>
                
                    <div class="cb"></div>
                </div>
            </div>
            <span class="button"><button type="submit" id="submitProfile" name="submitProfile" value="Save Details">Save Details</button></span>
        </div>
        <div class="grid_3">
        &nbsp
        </div>
        <div class="clear"></div>
    </div>
    <input id="hidUserID" name="hidUserID" type="hidden" value="<%= Model.User.ID.ToString() %>" />
    </form>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">


</asp:Content>