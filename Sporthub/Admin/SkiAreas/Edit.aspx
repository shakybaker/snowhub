<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Sporthub.Web.Admin.SkiAreas.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Ski Area - </h1>
        <label>Countries</label>
        <asp:DropDownList ID="ddlCountries" runat="server">
        </asp:DropDownList>
        <br />
        <label>Name</label>
        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Save" />
    </div>
    </form>
</body>
</html>
