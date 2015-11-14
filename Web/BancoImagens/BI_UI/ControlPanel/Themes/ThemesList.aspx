<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ThemesList.aspx.cs" Inherits="BancoImagens.ControlPanel.Themes.ThemesList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Temas</span><br />
    <br />
        <asp:Button ID="btnNew" runat="server" Text="Novo Tema" OnClick="btnNew_Click" /><br />
        <br />
        <asp:GridView ID="gdvCategorias" runat="server" AllowPaging="True" CellPadding="4"
            Font-Size="Small" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvCategorias_RowDataBound" OnPageIndexChanging="gdvCategorias_PageIndexChanging">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:HyperLinkField Visible="False" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
</asp:Content>
