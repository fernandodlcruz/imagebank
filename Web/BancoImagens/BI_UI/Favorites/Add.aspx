<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="BancoImagens.Favorites.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body onload="JavaScript:window.setTimeout('self.close();', 3000);">
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/LogoFototeca(AP).jpg" /><br />
        <br />
        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"
            ForeColor="#0000C0"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>
