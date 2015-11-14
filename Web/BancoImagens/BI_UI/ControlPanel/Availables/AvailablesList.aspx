<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="AvailablesList.aspx.cs" Inherits="BancoImagens.ControlPanel.Availables.AvailablesList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Disponibilidades</span><br />
    <br />
        <asp:Button ID="btnNew" runat="server" Text="Nova disponibilidade" OnClick="btnNew_Click" /><br />
        <br />
        <asp:GridView ID="gdvDisponibilidades" runat="server" AllowPaging="True" CellPadding="4"
            Font-Size="Small" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvDisponibilidades_RowDataBound">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
</asp:Content>
