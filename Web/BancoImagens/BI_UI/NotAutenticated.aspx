<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotAutenticated.aspx.cs" Inherits="BancoImagens.NotAutenticated" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>.:: Fototeca Internacional - Banco de Imagens ::.</title>
    <script language="javascript" type="text/javascript">
    function CloseWindow()
    {
        if (window.opener)
        {
            window.setTimeout('self.close();', 3000);
        }
    }
    </script>
</head>
<body onload="JavaScript:CloseWindow();">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="#C00000"
            Text="É preciso estar com suas credenciais acionadas. Por favor, efetue o login no sistema."></asp:Label></div>
    </form>
</body>
</html>
