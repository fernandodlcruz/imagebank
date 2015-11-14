<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ClientUserList.aspx.cs" Inherits="BancoImagens.ControlPanel.ClientUsers.ClientUserList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span style="font-size: 18pt; color: #4682b4">Usuários</span><br />
    <br />
    <asp:GridView ID="gdvUsers" runat="server" CellPadding="4" ForeColor="#333333"
        GridLines="None" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gdvUsers_PageIndexChanging" OnRowDataBound="gdvUsers_RowDataBound">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" BorderColor="Red" Font-Bold="True" ForeColor="Red" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Bloqueado" />
            <asp:BoundField DataField="UserName" HeaderText="Nome do usu&#225;rio" />
            <asp:BoundField DataField="Email" HeaderText="E-mail" />
            <asp:BoundField DataField="LastLoginDate" HeaderText="&#218;ltimo Login Realizado" />
        </Columns>
    </asp:GridView>
</asp:Content>
