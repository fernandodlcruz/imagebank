<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ParametersList.aspx.cs" Inherits="BancoImagens.ControlPanel.Parameters.ParametersList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Parametros</span><br />
    <br />
    <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="Novo parametro" /><br /><br />
    <asp:GridView ID="gdvParameters" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvParameters_RowDataBound">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" BorderColor="Red" Font-Bold="True" ForeColor="Red" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
