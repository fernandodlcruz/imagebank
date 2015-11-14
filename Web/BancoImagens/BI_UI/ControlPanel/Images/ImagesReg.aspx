<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ImagesReg.aspx.cs" Inherits="BancoImagens.ControlPanel.Images.ImagesReg" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Imagens</span><br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
    <br />
    <table style="color: #000000">
        <tr>
            <td align="right" style="width: 168px">
                Selecione o arquivo excel do cadastro de imagens:&nbsp;</td>
            <td style="width: 356px">
                <asp:FileUpload ID="fluExcel" runat="server" Width="351px" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
            </td>
            <td style="width: 356px">
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
            </td>
            <td style="width: 356px">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ControlPanel/Images/ImagesList.aspx">Clique aqui para manutenção das imagens.</asp:HyperLink></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Enviar arquivo" />&nbsp;<asp:Button
        ID="btnLoad" runat="server" OnClick="btnLoad_Click" Text="Carregar imagens" />
</asp:Content>
