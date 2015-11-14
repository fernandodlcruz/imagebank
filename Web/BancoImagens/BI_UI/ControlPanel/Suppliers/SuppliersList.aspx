<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="SuppliersList.aspx.cs" Inherits="BancoImagens.ControlPanel.Suppliers.SuppliersList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Fornecedores</span><br />
    <br />
    <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="Novo fornecedor" /><br /><br />
    <asp:GridView ID="gdvFornecedores" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvFornecedores_RowDataBound">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" BorderColor="Red" Font-Bold="True" ForeColor="Red" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
