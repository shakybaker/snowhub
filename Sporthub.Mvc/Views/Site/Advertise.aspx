<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("Advertise", "Site") %>" method="post">
    <div class="container_12">
        <div class="grid_8">
            <div class="pod" style="margin-bottom: 140px">
<h3>Advertising on the Snowhub is on it's way.</h3>

<p>We are currently working on our own system to allow businesses to advertise on the Snowhub. There will be a variety of ad formats which can be tailored to display site wide or targeted to specific resorts. </p>

<p>Basic advertising will be free but for a fee businesses will be guaranteed better positioning, more prominent ads and the facility to include images and more information about their business. </p>

<p>We also plan to give businesses the facility to have their own page on the site.</p>

<p>Enter your email if you wish to be informed when advertising is available.</p>

<h3 style="display: block; margin-top: 30px;">Partnerships and Affiliate deals.</h3>

<p>If you run an affiliate programme and you think that the Snowhub would be a good partner for you then please get in touch.  </p>
                <div class="cb"></div>
                <div class="podbtm"></div>
            </div>
        </div>
        <div class="grid_4">
            <div class="pod">
                <div class="headwrap">
                    <h3>Enquire</h3>
                </div>
                <div class="podIn">
                    <p style="margin-right: 10px;">Enter your details below and we will get back to you when advertising is available.</p>
                    <div class="cb"></div>
                    <label>Email Address</label>
                    <div class="cb"></div>
                    <input maxlength="100" id="email" name="email" type="text" class="tb tb250" value="" />
                    <div class="cb"></div>
                    <label>Message</label>
                    <div class="cb"></div>
                    <textarea cols="28" style="height: 180px; margin: 0 0 30px" id="message" name="message"></textarea>
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Advertising</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Advertising</h2>
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
