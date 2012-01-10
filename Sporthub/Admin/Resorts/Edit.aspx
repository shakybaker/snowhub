<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Sporthub.Web.Admin.Resorts.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Snowhub Admin / <%= vd.Action %> Resort</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1><%= vd.Action %> Region</h1>
<%
    if (vd.Resort != null)
    {
%>
        <h2><%= vd.Resort.Name %></h2>
<%
    }
%>
        <br />
        <br />
        <label>Name</label>
        <br />
        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
        <br />
        <br />
        <label>Wikipedia URL</label>
        <br />
        <asp:TextBox ID="tbWikipediaURL" runat="server"></asp:TextBox>
        <br />
        <br />
        Lat/Long
        <br />
        <asp:TextBox ID="tbCoord" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
        <input id="btnReset" type="reset" value="Reset" />
    </form>
</body>
</html>
