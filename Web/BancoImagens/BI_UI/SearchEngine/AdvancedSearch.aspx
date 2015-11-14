<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="AdvancedSearch.aspx.cs" Inherits="BancoImagens.SearchEngine.AdvancedSearch" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    // Captura o evento da tecla pressionada e associa à função grabKey
	//document.body.setAttribute("onKeyDown",grabKey); // Para IE, Chrome e FF
	//document.body.setAttribute("onKeyPress",grabKey);
	//document.keypress(grabKey); // Para FF, Safari e Opera

    function OnEnter(evt, acao)
    {
        var key_code = evt.keyCode  ? evt.keyCode  :
                       evt.charCode ? evt.charCode :
                       evt.which    ? evt.which    : void 0;


        if (key_code == 13)
        {
            if (acao == "PA")
            {
                //document.getElementById("btnPesquisar").
                //alert("oi");
                //btnPesquisarCodigos_Click;
            }
            //return false;
        }
    }

</script>
    <span class="Titulo">Pesquisa de imagens</span><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    Entre com a(s) palavra(s)-chave:
    <asp:TextBox ID="txtPalavrasChave" runat="server" Width="313px" onkeypress="OnEnter(event,'PA');"></asp:TextBox><br />
    <span style="font-size: 8pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        (Utilize a vírgula para separar mais de uma palavra)</span><br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"
        ForeColor="Red"></asp:Label><br />
    <br />
    <table width="100%">
        <tr valign="top">
            <td align="left" width="33%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <asp:CheckBox ID="chkTpLicenca" runat="server" Font-Bold="True" Font-Size="Small"
                    Text="Tipos de licença" OnCheckedChanged="chkTpLicenca_CheckedChanged" AutoPostBack="True" Checked="True" /><asp:CheckBoxList ID="chlLicencas" runat="server">
                    </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td width="33%">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                <asp:CheckBox ID="chkOrientacoes" runat="server" Text="Orientações" Checked="True" AutoPostBack="True" Font-Bold="True" Font-Size="Small" OnCheckedChanged="chkOrientacoes_CheckedChanged" /><asp:CheckBoxList ID="chlOrientacoes" runat="server">
                    <asp:ListItem Selected="True" Value="H">Horizontal</asp:ListItem>
                    <asp:ListItem Selected="True" Value="V">Vertical</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Q">Quadrado</asp:ListItem>
                </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            <td width="33%">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                <asp:CheckBox ID="chkFormatos" runat="server" Text="Formatos disponíveis" Checked="True" AutoPostBack="True" Font-Bold="True" Font-Size="Small" OnCheckedChanged="chkFormatos_CheckedChanged" /><asp:CheckBoxList ID="chlFormatos" runat="server" RepeatColumns="3">
                </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" Height="20px" ImageUrl="~/img/tumbs_nav/loading.gif"
                Width="20px" />Aguarde...
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table width="100%">
        <tr valign="top">
            <td align="left" width="50%">
                <strong>
                Temas:</strong><br />
                <asp:ListBox ID="lsbTemas" runat="server" Height="109px" Width="306px" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lsbTemas_SelectedIndexChanged">
                </asp:ListBox><br />
                <span style="font-size: 7pt">(mantenha a tecla &lt;CTRL&gt; pressionada para selecionar
                    mais de um tema)</span></td>
            <td width="50%" align="left" valign="bottom">
                <strong>Sub-Temas:<br />
                </strong>
                <asp:ListBox ID="lsbSubTemas" runat="server" Height="109px" Width="306px" SelectionMode="Multiple">
                </asp:ListBox><br />
                <span style="font-size: 7pt">(mantenha a tecla &lt;CTRL&gt; pressionada para selecionar
                    mais de um sub-tema)</span></td>
        </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        <br />
        <br />
        <table width="100%">
            <tr valign="top">
                <td align="left" width="50%">
                </td>
                <td align="right" valign="bottom" width="50%">
    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" /></td>
            </tr>
    </table>
    <br />
    <hr style="border-style: dashed; border-color: Silver; height: 1px" />
    <br />
    <span class="SubTitulo">Pesquisa de imagens por código</span><br />
    <br />
    entre com códigos de imagens:
    <asp:TextBox ID="txtCodigosImgs" runat="server" Width="313px"></asp:TextBox>&nbsp;<asp:Button
        ID="btnPesquisarCodigos" runat="server" Text="Pesquisar" OnClick="btnPesquisarCodigos_Click" /><br />
    <span style="font-size: 8pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (Utilize
        a vírgula para separar mais de um código)<br />
    </span>
    <br />
    <hr style="border-style: dashed; border-color: Silver; height: 1px" />
    <br />
    <!--span class="SubTitulo">Pesquisa de imagens por nome de fotógrafo</span><br />
    <br />
    entre com o nome do fotógrafo:
    <asp:TextBox ID="txtPesquisarFotografo" runat="server" Width="313px"></asp:TextBox>
    <asp:Button
        ID="btnPesqFotografo" runat="server" Text="Pesquisar" /><br />
    <br />
    <hr style="border-style: dashed; border-color: Silver; height: 1px" /-->
    &nbsp;
</asp:Content>