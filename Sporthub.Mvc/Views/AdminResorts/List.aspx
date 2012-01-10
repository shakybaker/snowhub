<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AdminResortsListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : Admin</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Breadcrumb" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
</asp:Content>
<asp:Content ID="Heading" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_12">
            <table>
                <tr>
                    <td><a href="/admin/resorts/list/A" >A</a></td>
                    <td><a href="/admin/resorts/list/B" >B</a></td>
                    <td><a href="/admin/resorts/list/C" >C</a></td>
                    <td><a href="/admin/resorts/list/D" >D</a></td>
                    <td><a href="/admin/resorts/list/E" >E</a></td>
                    <td><a href="/admin/resorts/list/F" >F</a></td>
                    <td><a href="/admin/resorts/list/G" >G</a></td>
                    <td><a href="/admin/resorts/list/H" >H</a></td>
                    <td><a href="/admin/resorts/list/I" >I</a></td>
                    <td><a href="/admin/resorts/list/J" >J</a></td>
                    <td><a href="/admin/resorts/list/K" >K</a></td>
                    <td><a href="/admin/resorts/list/L" >L</a></td>
                    <td><a href="/admin/resorts/list/M" >M</a></td>
                    <td><a href="/admin/resorts/list/N" >N</a></td>
                    <td><a href="/admin/resorts/list/O" >O</a></td>
                    <td><a href="/admin/resorts/list/P" >P</a></td>
                    <td><a href="/admin/resorts/list/Q" >Q</a></td>
                    <td><a href="/admin/resorts/list/R" >R</a></td>
                    <td><a href="/admin/resorts/list/S" >S</a></td>
                    <td><a href="/admin/resorts/list/T" >T</a></td>
                    <td><a href="/admin/resorts/list/U" >U</a></td>
                    <td><a href="/admin/resorts/list/V" >V</a></td>
                    <td><a href="/admin/resorts/list/W" >W</a></td>
                    <td><a href="/admin/resorts/list/X" >X</a></td>
                    <td><a href="/admin/resorts/list/Y" >Y</a></td>
                    <td><a href="/admin/resorts/list/Z" >Z</a></td>
                </tr>
            </table>
        </div>
    </div>
    <div class="container_12">
        <div class="grid_12">
            <table class="Stats">
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Region</th>
                    <th>CountryName</th>
                    <th>ContinentName</th>
                    <th>PrettyUrl</th>
                    <th>Longitude</th>
                    <th>Latitude</th>
<%--
                    <th>IsFeaturedResort?</th>
                    <th>IsNorthernHemisphere?</th>
                    <th>ISO3166Alpha2</th>
                    <th>Overview?</th>
--%>
                    <th>CanPublish?</th>
                    <th>UpdatedDate</th>
                    <th>CreatedDate</th>
                </tr>
<%
    foreach (Sporthub.Model.Resort resort in Model.Resorts)
    {
%>
                <tr>
                    <td><%= resort.ID %></td>
                    <td><a href="/admin/resorts/<%= resort.PrettyUrl %>/edit"><%= resort.Name %></a></td>
                    <%--<td><%= (resort.Region != null) ? resort.Region.Name : string.Empty %></td>--%>
                    <td></td>
                    <td><%= resort.CountryName %></td>
                    <td><%= resort.ContinentName %></td>
                    <td><%= resort.PrettyUrl %></td>
                    <td><%= resort.Longitude %></td>
                    <td><%= resort.Latitude %></td>
<%--
                    <td><%= resort.IsFeaturedResort %></td>
                    <td><%= resort.IsNorthernHemisphere %></td>
                    <td><%= resort.ISO3166Alpha2 %></td>
                    <td><%= (string.IsNullOrEmpty(resort.Overview)) ? false : true %></td>
--%>
                    <td><%= resort.CanPublish %></td>
                    <td><%= resort.UpdatedDate %></td>
                    <td><%= resort.CreatedDate %></td>
                </tr>
<%
    } 
%>
            </table>
        </div>
        <div class="cb"></div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>