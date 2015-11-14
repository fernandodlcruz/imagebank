<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ParametersReg.aspx.cs" Inherits="BancoImagens.ControlPanel.Parameters.ParametersReg" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Parametros</span><br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
    <br />
    <table style="color: #000000;">
        <tr>
            <td align="right" style="width: 120px">
                Código:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtCodigo" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 120px">
                Parametro:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtNome" runat="server" MaxLength="30" Width="343px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 120px">
                Valor:</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtValor" runat="server" MaxLength="80" Width="343px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 120px">
            </td>
            <td style="width: 356px">
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Salvar" />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Excluir" Enabled="False" />
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancelar" />
</asp:Content>
