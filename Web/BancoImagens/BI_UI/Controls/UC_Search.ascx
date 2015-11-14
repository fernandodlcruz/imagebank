<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Search.ascx.cs" Inherits="BancoImagens.Controls.UC_Search" %>
<table cellspacing="2">
    <tr>
        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid;
            width: 211px; border-bottom: gainsboro 1px solid" align="left">
            <span style="font-size: 9pt"><strong>Pesquisa rápida de imagens</strong></span></td>
    </tr>
    <tr>
        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid;
            width: 211px; border-bottom: gainsboro 1px solid; height: 205px" align="left">
            <asp:TextBox ID="txtPesquisaRapida" runat="server" Width="163px"></asp:TextBox><br />
            <span style="font-size: 8pt">(Pesquise por palavras-chave, utilize a vírgula para separar mais
                de uma palavra)</span><br />
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"></asp:Label><br />
            <asp:CheckBoxList ID="chlLicencas" runat="server">
            </asp:CheckBoxList><br />
            <asp:Button ID="btnPesquisaRap" runat="server" Text="Pesquisar" OnClick="btnPesquisaRap_Click" /></td>
    </tr>
</table>
