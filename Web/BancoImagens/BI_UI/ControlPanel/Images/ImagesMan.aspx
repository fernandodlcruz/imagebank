<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ImagesMan.aspx.cs" Inherits="BancoImagens.ControlPanel.Images.ImagesMan" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">Alterar Imagem</span><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
    <br />
    <asp:Image ID="imgBank" runat="server" />
    <br />
    <asp:Button ID="btnLoadImage" runat="server" Enabled="False" Text="Carregar imagem" OnClick="btnLoadImage_Click" /><br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table style="color: #000000">
        <tr>
            <td align="right" style="width: 168px">
                Tema:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlTemas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged" Width="337px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Sub-tema:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlSubTemas" runat="server" Width="337px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
            </td>
            <td style="width: 356px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Gaveta:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlGavetas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGavetas_SelectedIndexChanged" Width="180px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Pasta:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlPastas" runat="server" Width="180px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Tipo de licença:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlTpLicencas" runat="server" Width="337px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
            </td>
            <td style="width: 356px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Código:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="10" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Título:</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtTitulo" runat="server" MaxLength="50" Width="337px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Detalhes:</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtDetalhes" runat="server" MaxLength="400" Width="337px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Orientação:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlOrientacoes" runat="server" Width="116px">
                    <asp:ListItem Value="H">Horizontal</asp:ListItem>
                    <asp:ListItem Value="Q">Quadrada</asp:ListItem>
                    <asp:ListItem Value="V">Vertical</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                AUI:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlAUI" runat="server" Width="116px">
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">N&#227;o</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Formato:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlFormatos" runat="server" Width="116px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
            </td>
            <td style="width: 356px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" style="width: 168px">
                Fornecedor:</td>
            <td style="width: 356px">
                <asp:DropDownList ID="ddlFornecedores" runat="server" Width="337px">
                </asp:DropDownList></td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <span class="SubTitulo">Palavras-chave</span><br />
    <br />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/adicionar.gif" />Para adicionar
    uma palavra-chave à imagem, digite-a e clique no botão (+) para adicionar a nova
    palavra.<br />
    <br />
    <asp:Image ID="Image2" runat="server" ImageUrl="~/img/excluir.gif" />Para excluir
    a associação de uma palavra-chave da imagem, selecione-a na lista e clique no botão
    (X) para excluir a associação da palavra com a imagem.<br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <table style="color: #000000" border="1">
        <tr>
            <td align="left" style="width: 228px">
                <asp:Label ID="Label1" runat="server" Text="Palavra-chave:" Width="94px"></asp:Label><br />
                <asp:TextBox ID="txtKey" runat="server" MaxLength="30"></asp:TextBox>&nbsp;<asp:ImageButton
                    ID="igbInsertKey" runat="server" AlternateText="Adicionar à lista" ImageUrl="~/img/adicionar.gif" OnClick="igbInsertKey_Click" /></td>
            <td style="width: 356px" align="center" valign="top">
                <asp:ListBox ID="ltbKeys" runat="server" Height="133px" SelectionMode="Multiple" Width="311px"></asp:ListBox>
                <asp:ImageButton ID="igbDeleteKey" runat="server" AlternateText="Excluir selecionados da lista"
                    ImageUrl="~/img/excluir.gif" OnClick="igbDeleteKey_Click" /></td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Salvar" />
    <asp:Button ID="btnDelete" runat="server" Enabled="False" OnClick="btnDelete_Click"
        Text="Excluir" />
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancelar" />
</asp:Content>
