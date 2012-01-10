<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.UserViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : <%= Model.User.GetName() %>'s Profile : Add Profile Picture</asp:Content>
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
        <h2>Your Profile - Add Profile Picture</h2>
    </div>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
       

    <% Html.BeginForm("AddPicture", "Account", FormMethod.Post, new { id = "picForm", enctype = "multipart/form-data" }); %>

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
                        <p style="margin-bottom: 8px;">Select an image file on your computer (max 4 MB):</p>

                        <input type="file" id="Picture" name="Picture"/>
                    </div>
                    
                    <div class="cb">
                    </div>
                        <label>Delete picture <input id="DeletePic" name="DeletePic" type="checkbox"  /></label>
                    <div class="cb">
                    </div>
                </div>
            </div>
        </div>
        <div class="grid_3">
            <span class="button"><button type="submit" id="submitPic" name="submitPic" value="Save Changes">Save Changes</button></span>
            <div class="cb" style="margin-top: 20px; float: left;">
                <a href="/user/">Cancel</a>
            </div>
        </div>
    </div>
    <% Html.EndForm(); %>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">

</asp:Content>