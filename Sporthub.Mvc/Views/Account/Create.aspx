<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/NoNav.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Create Account</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_3">
        <p>&nbsp;</p>
        </div>
        <div class="grid_6" style="margin-top: 15px">
        
            <div class="pod">
        
                <div class="headwrap">
                    <h3>Sign-up to the Snowhub</h3>
                </div>
                <fieldset style="padding: 20px 0; background-image: none;">
                    <form id="signupForm" class="signupForm" action="<%= Url.Action("create", "account") %>" method="post">
                        <label for="UserName">Username</label>
                        <%= Html.TextBox("UserName", "", new { @class = "tb tb200" })%><span id="f_UserName" class="f"></span>
                        <div class="tip">From 4 to 15 alphanumeric characters</div>
                        <span id="m_UserName" class="m"></span>
                        <label for="Email">Email</label>
                        <%= Html.TextBox("Email", "", new { @class = "tb tb200" })%><span id="f_Email" class="f"></span>
                        <div class="tip">Will not be displayed on the site</div>
                        <span id="m_Email" class="m"></span>
                        <label for="Password">Password</label>
                        <%= Html.Password("Password", string.Empty, new { @class = "tb tb200" })%><span id="f_Password" class="f"></span>
                        <div class="tip">At last 6 characters</div>
                        <span id="m_Password" class="m"></span>
                        <label for="ConfirmPassword">Confirm Password</label>
                        <%= Html.Password("ConfirmPassword", string.Empty, new { @class = "tb tb200" })%><span id="f_ConfirmPassword" class="f"></span><span id="m_ConfirmPassword"  class="m"></span>
                        <p style="clear: both; float: left; margin-top: 20px">By clicking 'Join' you are agreeing to our <a href="/terms/">Terms &amp; Conditions</a>.</p>
                        <input class="basic-btn" type="submit" id="submit" name="submit" value="Sign-Up >>" />
                        <input type="hidden" id="h_UserName" value="false" />
                        <input type="hidden" id="h_Email" value="false" />
                        <input type="hidden" id="h_Password" value="false" />
                        <input type="hidden" id="h_ConfirmPassword" value="false" />
                    </form>
                </fieldset>

                <p>Already have an account? <a href="/account/login">Login here</a></p>
                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_3">
        <p>&nbsp;</p>
        </div>
        <%--<div class="grid_6" style="margin-top: 15px">
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
    </div>--%>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

        sporthub.pageHasNavigation = false;
        var loadingTimer, loadingFrame = 1, loadStart = 0, loadingId = "", success = true;

        function checkEmailFormat(email) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email)) {
                email.focus
                return false;
            }
            return true;
        }

        function ok(id) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            document.getElementById(hid.toString().replace(/#/, "")).value = "true";
            $(fid).removeClass("anim ok err");
            stopLoading();
            $(fid).css('background-position', '-527px 0px');
            $(fid).addClass("ok");
            success = true;
            $(eid).css("border-color", "#66aa00");
        }

        function err(id, msg) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            $(fid).removeClass("anim ok err");
            document.getElementById(hid.toString().replace(/#/, "")).value = "false";
            stopLoading();
            $(fid).css('background-position', '-527px -16px');
            $(fid).addClass("err");
            $(mid).html(msg);
            success = false;
            $(eid).css("border-color", "#ff5555");
        }

        function stopLoading() {
            clearInterval(loadingTimer);
        }

        function animateLoading() {
            if (!$(loadingId).is(':visible')) {
                stopLoading();
                return;
            }

            if (loadingFrame == 9) {
                loadingFrame = 1;
            }
            var top = (loadStart + (loadingFrame * 16));
            $(loadingId).css('background-position', '-511px -' + top + 'px');


            loadingFrame++;
        };

        function showLoading() {
            stopLoading();

            loadingTimer = setInterval(animateLoading, 66);
        };

        function setupAnim(fid, mid) {
            $(mid).html("");
            $(fid).removeClass("anim ok err");
            $(fid).addClass("anim");
            $(fid).css("display", "block");
            loadingId = fid;
            showLoading();
        }
        
        function checkUserName(id) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            document.getElementById(hid.toString().replace(/#/, "")).value = "false";
            setupAnim(fid, mid);
            if ($(eid).val() != "") {
                $.getJSON("/Ajax/CheckUserName", { userName: $(eid).val() }, function(o) {
                    if (o.Flag) { ok(id); } else { err(id, o.Message); }
                });
            }
            else {
                err(id, "Required");
            }
        }

        function checkEmail(id) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            setupAnim(fid, mid);
            if ($(eid).val() != "") {
                if (checkEmailFormat($(eid).val())) {
                    $.getJSON("/Ajax/CheckEmail", { email: $(eid).val() }, function(o) {
                        if (o.Flag) { ok(id); } else { err(id, o.Message); }
                    });
                }
                else {
                    err(id, "Invalid format");
                }
            }
            else {
                err(id, "Required");
            }
        }

        function checkPassword(id) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            setupAnim(fid, mid);
            if ($(eid).val().length > 5) {
                $.getJSON("/Ajax/CheckEmail", { email: $(eid).val() }, function(o) {
                    if (o.Flag) {
                        ok(id);
                        checkConfirmPassword("ConfirmPassword");
                    }
                    else {
                        err(id, o.Message); 
                    }
                });
            }
            else {
                err(id, "Must be at least 6 characters");
            }
        }

        function checkConfirmPassword(id) {
            var fid = "#f_" + id, mid = "#m_" + id, eid = "#" + id, hid = "#h_" + id;
            setupAnim(fid, mid);
            if ($(eid).val().length > 0) {
                if ($(eid).val() == $("#Password").val()) { ok(id); } else { err(id, "Passwords don't match"); }
            }
            else {
                $(fid).removeClass("anim ok err");
            }
        }

        $(document).ready(function() {

            var form = $('#signupForm');
            $("#UserName").blur(function() {
                checkUserName($(this).attr("id"));
            });

            $("#Email").blur(function() {
                checkEmail($(this).attr("id"));
            });

            $("#Password").blur(function() {
                checkPassword($(this).attr("id"));
            });

            //            $("#ConfirmPassword").blur(function() {
            //                checkConfirmPassword($(this).attr("id"));
            //            });

            $("form.signupForm").submit(function() {
                $("#submit").attr("disabled", true);
                $("#submit").val("Saving ..."); 
                
                if ($("#h_UserName").val() == "false") {
                    checkUserName("UserName");
                }
                if ($("#h_Email").val() == "false") {
                    checkEmail("Email");
                }
                if ($("#h_Password").val() == "false") {
                    checkPassword("Password");
                }
                if ($("#h_ConfirmPassword").val() == "false") {
                    checkPassword("ConfirmPassword");
                }

                if (success) {
                    return true;
                }
                $("#submit").removeAttr("disabled");
                $("#submit").val("Sign-Up >>"); 
                
                return false;
            });

        });

    </script>
</asp:Content>