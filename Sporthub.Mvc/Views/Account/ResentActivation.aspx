<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/NoNav.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12" style="margin-top: 20px;">
        <div class="grid_3">
            &nbsp;
        </div>
        <div class="grid_6">
        
            <div class="pod">
        
                <div class="headwrap">
                    <h3>We have re-sent your activation email.</h3>
                </div>
                <br />
                <p>We have sent a confirmation email to this address:</p>
                <p><strong class="highlight"><%=Model.User.Email %></strong></p>
                <p>Check your email and follow the instructions to activate your account. If there seems to be a delay then please check your spam folder.</p>
                <br />
                <br />
                <p><a href="/">&larr; Go to the Snowhub home page</a></p>

                <div class="cb"></div>
            </div>
        </div>
        <div class="grid_3">
            &nbsp;
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Create Account</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" language="javascript">

        sporthub.pageHasNavigation = false;
    </script>
</asp:Content>
