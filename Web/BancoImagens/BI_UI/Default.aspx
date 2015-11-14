<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BancoImagens.Default" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Image ID="Image1" ImageUrl="img/tumbs_nav/blank.gif" runat="server" style="width:auto" AlternateText="Home" Width="570px" /><br />
    <cc1:SlideShowExtender ID="SlideShowExtender1" runat="server" 
                           TargetControlID="Image1" 
                           AutoPlay="true" 
                           SlideShowServicePath="SlidesService.asmx" 
                           SlideShowServiceMethod="getSlides" Loop="true" />
</asp:Content>
