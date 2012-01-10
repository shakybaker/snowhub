<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Sporthub.Web.Admin.Regions.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Snowhub Admin / <%= vd.Action %> Region</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1><%= vd.Action %> Region</h1>
<%
    if (vd.Region != null)
    {
%>
        <h2><%= vd.Region.Name %></h2>
<%
    }
%>
        <a href="List.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.ParentRegionID) %>=<%= vd.ParentRegionID %>">&laquo; Go Back to list</a>
        <br />
        <br />
        <input type="button" onclick="window.location='../Resorts/Edit.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Action) %>=<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Add) %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.RegionID) %>=<%= vd.Region.ID %>'" value="Add Resort" />
        <br />
        <br />
        <input type="button" onclick="window.location='Edit.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Action) %>=<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Add) %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.ParentRegionID) %>=<%= vd.Region.ID %>'" value="Add Sub-Region" />
        <label>Name</label>
        <br />
        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
        <input id="btnReset" type="reset" value="Reset" />

        <br />
        <br />
        <h2>Resorts in this Region</h2>
        <br />
        <br />
        
<%
        if (vd.Resorts != null)
        {
%>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Resort Name</th>
                    <th>Wikipedia Url</th>
                    <th>Latitude</th>
                    <th>Longitude</th>
                    <th>&nbsp;</th>
                </tr>
            </thead>
            <tbody>
<%
        foreach (Sporthub.Model.Resort resort in vd.Resorts)
        {
%>
                <tr>
                    <td><%= resort.ID%></td>
                    <td><%= resort.Name%></td>
                    <td><%= resort.WikipediaUrl%></td>
                    <td><%= resort.Latitude%></td>
                    <td><%= resort.Longitude%></td>
                    <td><a href="../Resorts/Edit.aspx?<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Action) %>=<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.Edit) %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.CountryID) %>=<%= vd.CountryID %>&<%= Sporthub.Utils.Enums.GetName(Sporthub.Model.Enumerators.QS.ResortID) %>=<%= resort.ID %>" title="click to Edit">Edit</a></td>
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
        <p>No Resorts at this Region level</p>
<%
        }
%>
        
    </form>
</body>
</html>
