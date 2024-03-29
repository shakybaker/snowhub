<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/NoNav.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.RegisterSucessViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12" style="margin-top: 20px;">
        <div class="grid_3">
            &nbsp;
        </div>
        <div class="grid_6">
        
            <div class="pod">
        
                <div class="headwrap">
                    <h3>First step complete.</h3>
                </div>
                <br />
                <h4 style="margin: 0; padding: 10px 0;">Just one more step ...</h4>
                <p>We have sent a confirmation email to this address:</p>
                <p><strong class="highlight"><%=Model.EmailAddress %></strong></p>
                <p>Check your email and follow the instructions to activate your account. If there seems to be a delay then please check your spam folder.</p>
                <br />
                <br />
                <h4 style="margin: 0; padding: 10px 0;">You didn't receive your email?</h4>
                    <form class="resendEmailForm" action="<%= Url.Action("ResendEmail", "Account") %>" method="post">
                    <p>
                        <%= Html.TextBox("Email", ViewData.Model.EmailAddress, new { @class = "tb tb200" })%><span id="f_Email" class="f"></span><span id="m_Email" class="m"></span>

                        <input type="hidden" id="h_Email" value="false" />

                        <span class="button"><button type="submit" id="submit" name="submit" value="Send me another">Send me another</button></span> 
                    </p>
                    </form>

                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_3">
            &nbsp;
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Create Account - First Step Complete</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentScripts" runat="server">
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
                if (!checkEmailFormat($(eid).val())) {
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

            $("form.resendEmailForm").submit(function() {
                if ($("#h_Email").val() == "false") {
                    checkEmail("Email");
                }

                if (success) {
                    var action = form.attr('action');
                    var serialized = form.serialize();
                    $.post(action,
			            serialized,
			            null
		            );
                }
                return false;
            });

        });

        </script>
</asp:Content>

