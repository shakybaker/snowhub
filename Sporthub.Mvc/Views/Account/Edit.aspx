<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Manage Profile</asp:Content>
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
<%--    <div class="grid_12">
        <h2>Edit your profile</h2>
    </div>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div id="PageHeader" class="container_12">
        <div class="grid_9">
            <h2 id="PageHeading">Manage Profile</h2>
        </div>
        <div class="grid_3">
            &nbsp;
        </div>
    </div>
    <div class="container_12">
        <div class="grid_3">
        &nbsp;
        </div>
        <div class="grid_9" style="height: 23px;">
            <ul class="menuTabs tabNav">
            <li class="tab selectedTab"><a title="" href="/account/edit"><span>Account</span></a></li>
            <li class="tab"><a title="" href="/account/managecontacts"><span>Contacts</span></a></li>
            <li class="tab"><a title="" href="/account/manageresorts"><span>Resorts</span></a></li>
            <li class="tab"><a title="" href="/account/privacysettings"><span>Privacy</span></a></li>
            </ul>
        </div>
    </div>  
    <div class="container_12">
        <div class="grid_12">
            <div class="headwraplarge">
                <h3>Edit Your Account Settings</h3>
            </div>
            <div class="pod">
                <form class="editProfilePhoto" action="<%= Url.Action("profilephoto", "account") %>" method="post">
                <table class="table2">
                    <tr>
                        <td style="width: 200px;">Your profile photo</td>
                        <td style="width: 250px;">
                        <% if (Model.HasProfileImage) { %>
                            <img alt="<%= Model.User.UserName%>" src="<%= Model.ProfileImageUrl %>" />
                        <% } else { %>
                            <img alt="<%= Model.User.UserName%>" src="<%= Model.ProfileImageUrl %>" />
                        <% } %>    
                        </td>
                        <td>
                        Uploaded images (gif/jpg/png) will be resized. On your profile, your image will be 210 pixels wide, with a maximum height of 210 pixels.
    If you don't have access to image editing software you can crop your profile photo with <a href="http://mypictr.com/?size=210x210" rel="external">Mypictr</a>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <input type="file" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <span class="button"><button type="submit" id="upload" name="upload" value="Upload photo">Upload photo</button></span>
                        </td>
                        <td></td>
                    </tr>
                </table>
                </form>
                <form class="editProfileForm" action="<%= Url.Action("edit", "account") %>" method="post">
                <table class="table2">
                    <tr>
                        <td style="width: 200px;">Delete photo</td>
                        <td style="width: 250px;">
                            <input type="checkbox" id="delPhoto" name="delPhoto" value="1" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><label for="RealName">RealName</label></td>
                        <td>
                        <%= Html.TextBox("RealName", ViewData.Model.User.RealName, new { @class = "tb tb200" })%>
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
		                        foreach (var country in ViewData.Model.Countries) {
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
	                            <option value="">&nbsp;</option>
                                <%
                                string dayOptions = string.Empty;
                                for (int i = 1; i <= 31; i++)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobDay == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= dayOptions %>
                            </select>
                            <select name="dob_m">
	                            <option value="">&nbsp;</option>
                                <%
                                string monthOptions = string.Empty;
                                for (int i = 1; i <= 12; i++)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobMonth == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= monthOptions%>
                            </select>
                            <select name="dob_y">
	                            <option value="" selected="selected">&nbsp;</option>
                                <%
                                string yearOptions = string.Empty;
                                for (int i = (DateTime.Now.Year - 10); i >= 1900; i--)
                                { %>
	                            <option value="<%= i %>"<%= ((Model.User.DobYear == i.ToString()) ? " selected='selected'" : string.Empty) %>><%= i %></option>
	                            <% } %>
	                            <%= yearOptions%>
                            </select>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><span class="button"><button type="submit" id="submitProfile" name="submitProfile" value="Save Details">Save Details</button></span></td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                </table>
                
                </form>
            </div>
        </div>
    </div>
    <input id="hidUserID" type="hidden" value="<%= Model.User.ID.ToString() %>" />
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAIQDpC_WWgFnqDJw1bvsKaxQ1hAvxqg7dx2-dsjGJnmPBCAjAZhSnGBbcagzQEThG6FhXvox6S0v8FA"
      type="text/javascript"></script>

<script type="text/javascript" language="javascript">
</script>
</asp:Content>