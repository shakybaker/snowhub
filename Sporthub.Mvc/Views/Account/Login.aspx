<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/NoNav.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.LoginViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Login</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12" style="margin-top: 20px;">
        <div class="grid_3">
        <p>&nbsp;</p>
        </div>
        <div class="grid_6">

<% 
using (Html.BeginForm()) 
{ 
%>
            <div class="pod">
                <div class="headwrap">
                    <h3>Login to the Snowhub</h3>
                </div>
                <% if (!string.IsNullOrEmpty(Model.Message)) { %>
                <div>
                <p><%=Model.Message %></p>
                </div>
                <% } %>
                <label for="Email">Email</label>
                <%= Html.TextBox("Email", ViewData.Model.Email, new { @class = "tb tb200" })%>
                <label for="Password">Password</label>
                <%= Html.Password("Password", ViewData.Model.Email, new { @class = "tb tb200" })%><span style="float: left; display: block; margin: 7px 0 0 7px;">(<a href="/account/forgotpassword">Forgot your password?</a>)</span>
                <label for="Persist"><%= Html.CheckBox("Persist") %>  Remember me for 2 weeks</label>
                <span class="button"><button type="submit" name="login" value="Come In >>">Come In >></button></span> 
<%
}
%>
                <p class="cb" style="padding-top: 20px;">Don't have an account? <a href="/account/create">Create one here</a></p>
                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_3">
        <p>&nbsp;</p>
        </div>
<%--        <div class="grid_6">
            <div class="pod">
                <div class="headwrap">
                    <h3>Connect with Facebook</h3>
                </div>
                <p>If you have a Facebook account you can use it to Login to the Snowhub.</p>
            <a class="fb-connect qt" title="Sign-up by Connecting your Facebook account to the Snowhub. You can post reviews to your Facebook wall & Check-In to resorts" href="#" onclick="return connectToFacebook();">Connect with Facebook</a>
            <form method="post" action="/user" id="fb_form">
            </form>
                <script type="text/javascript">
                    function connectToFacebook() {
                        FB.login(function(response) {
                          if (response.session) {
                            if (response.perms) {
                              // user is logged in and granted some permissions.
                              // perms is a comma separated list of granted permissions
                              
                                $.getJSON("/Account/ConnectNewToFacebook?", { facebookUid: response.session.uid, accessToken: response.session.access_token }, function(o) {
                                    if (o.Success == true) {
                                        $("#fb_form").submit();
                                        //window.location = "/user";
                                    }
                                });
                            } else {
                              alert("You need to grant permissions to the Snowhub if you want to register using your Facebook account.");
                              // user is logged in, but did not grant any permissions
                            }
                          } else {
                              alert("You need to grant permissions to the Snowhub if you want to register using your Facebook account.");
                            // user is not logged in
                          }
                        }, {perms:'email,publish_stream,offline_access'});
                        return false;
                    }
                </script>
                <div class="cb"></div>
            </div>
        </div>
--%>    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

        sporthub.pageHasNavigation = false;
    </script>
</asp:Content>