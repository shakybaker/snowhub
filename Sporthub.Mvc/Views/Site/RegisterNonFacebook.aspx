<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("RegisterNonFacebook", "Site") %>" method="post">
    <div class="container_12">
        <div class="grid_8">
            <div class="pod">
                <div class="headwrap">
                    <h3>Connect to the Snowhub with Facebook</h3>
                </div>
                <div class="podIn">
                    <p style="margin-top: 0;">You can register to the Snowhub simply by Connecting to your Facebook account. Once connected you can invite your friends, post content to your wall, review resorts, upload pictures and much more.</p>
                    <fb:login-button onlogin="refreshPage();" length="long" size="large"></fb:login-button>
                </div>
               
                <div class="podbtm">
                </div>
            </div>
        </div>
        <div class="grid_4">
            <div class="pod">
                <div class="headwrap">
                    <h3>Registration <em style="font-style: italic; font-weight: normal;">without</em> Facebook Connect</h3>
                </div>
                <div class="podIn">
                    <p style="margin-right: 10px;">Sorry, but at the moment the only means of registering to the Snowhub is via Facebook Connect. But, we are working on our own registration process which will be available soon.</p>

                    <p style="margin-right: 10px;">So, if you don't want to connect your Facebook account to the Snowhub, or are unwilling to sign-up to Facebook, then please enter your email address below and we will inform you when our registration process is available.</p>

                    <p>Thanks</p>
                    <p style="margin-bottom: 30px;">the Snowhub Team</p>
                    <div class="cb"></div>
                    <label>Email Address</label>
                    <div class="cb"></div>
                    <input maxlength="100" id="email" name="email" type="text" class="tb tb250" value="" />
                    <div class="cb"></div>
                    <span style="margin: 10px 10px 0 0;" class="button"><button type="submit" name="submit" value="Send">Send</button></span> 
                    <div class="cb"></div>
                </div>
                <div class="podbtm">
                </div>
            </div>
        </div>
    </div>
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Register</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Register</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more." />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
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
