<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Sporthub.Web.Admin.Regions.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Snowhub Admin / Regions in <%= vd.CountryName %></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1><a href="List.aspx">Regions</a></h1>
        
        <label>Country</label>
        <br />
        <asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlCountries_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
<%
    if (!vd.NoCountrySpecified)
    {
%>
        <input type="button" onclick="window.location='Edit.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Action) %>=<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Add) %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.ParentRegionID) %>=<%= vd.ParentRegionID %>'" value="Add Region" />
<%
        if (vd.Regions != null)
        {
%>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Region Name</th>
                    <th>&nbsp;</th>
                </tr>
            </thead>
            <tbody>
<%
        foreach (Sporthub.Model.Region region in vd.Regions)
        {
%>
                <tr>
                    <td><%= region.ID%></td>
                    <td><%= region.Name%></td>
                    <td><a href="Edit.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Action) %>=<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Edit) %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.RegionID) %>=<%= region.ID %>" title="click to Edit">Edit</a></td>
                </tr>
<%
        }
%>
            </tbody>
        </table>
<%
        }
        else
        {
%>
        <p>No Regions defined</p>
<%
        }
    }
%>
    </form>
</body>
</html>
