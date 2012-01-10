<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("ContactUs", "Site") %>" method="post">
    <div class="container_12">
        <div class="grid_3">
            <p>&nbsp;</p>
        </div>
        <div class="grid_6">
            <div class="pod" style="margin-bottom: 140px">
                <p>TODO: contact us copy</p>
                <div class="cb"></div>
                <label>Email Address</label>
                <div class="cb"></div>
                <input maxlength="100" id="email" name="email" type="text" class="tb tb250" value="" />
                <div class="cb"></div>
                <label>Message</label>
                <div class="cb"></div>
                <textarea cols="33" style="height: 180px; margin: 0 0 30px" id="message" name="message"></textarea>
                <div class="cb"></div>
                <span style="margin: 10px 10px 0 0;" class="button"><button type="submit" name="submit" value="Send">Send</button></span> 
                <div class="cb"></div>
                <div class="podbtm" style="padding: 10px 10px 0 10px;">
                </div>
            </div>
        </div>
        <div class="grid_3">
            <p>&nbsp;</p>
        </div>
    </div>
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Contact Us</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Contact Us</h2>
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
