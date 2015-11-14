<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="BancoImagens.SearchEngine.SearchResult" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
    function AddToFavorite(img)
    {
        var myArguments = new Object();
        
        window.showModalDialog("../Favorites/Add.aspx?img=" + img, myArguments,"dialogHeight: 230px;dialogWidth: 550px;center: yes;edge: sunken;resizable: no;scroll: no;status: no;unadorned: yes");
    }
    </script>
    <br />
    <span class="Titulo">Resultado da pesquisa</span><br />
    <br />
    Imagens por página:
    <asp:DropDownList ID="ddlQtdImages" runat="server" AutoPostBack="True">
        <asp:ListItem>12</asp:ListItem>
        <asp:ListItem>21</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
        <asp:ListItem>42</asp:ListItem>
    </asp:DropDownList><br />
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
    &nbsp;<br />
    <table width="100%">
        <tr align="left" valign="top">
            <td>
                <asp:Label ID="lblQtdImagensTop" runat="server"></asp:Label></td>
            <td align="right">
                </td>
        </tr>
        <tr align="center" valign="top">
            <td colspan="2" style="background-color: Silver">
                <asp:ImageButton ID="imbPrevTop" runat="server" AlternateText="Página anterior" Enabled="False"
                    Height="16px" ImageUrl="~/img/tumbs_nav/prev.gif" OnClick="imbPrevTop_Click"
                    Width="16px" />
                <asp:Label ID="lblPaginacaoTop" runat="server"></asp:Label>
                <asp:ImageButton ID="imbNextTop" runat="server" AlternateText="Próxima página" Enabled="False"
                    Height="16px" ImageUrl="~/img/tumbs_nav/next.gif" OnClick="imbNextTop_Click"
                    Width="16px" /></td>            
        </tr>
    </table>
    <br />
    <asp:DataList ID="dtlImages" runat="server" CellPadding="5" RepeatColumns="3" HorizontalAlign="Center" Width="100%" CellSpacing="5" RepeatDirection="Horizontal">
        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemTemplate>
            <b><small><%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %></small></b><br />
            <a href="../../img/imgbnk/BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg" rel="lightbox[imageBank]" title="<%#DataBinder.Eval(Container.DataItem, "IMG_CODIGO")%> - <%#DataBinder.Eval(Container.DataItem, "IMG_TITULO")%>" id="<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>">
            <img src="ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg" border="0">
            </a><br />
            <a href="JavaScript:AddToFavorite('<%# DataBinder.Eval(Container.DataItem, "IMG_ID") %>');">Adicionar a favoritos</a>
            <span id="spn_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>" style="visibility: hidden; display: none">
            <br /><br /><%# DataBinder.Eval(Container.DataItem, "IMG_DETALHES") %><br /><br />
            <b>Licença </b><%# DataBinder.Eval(Container.DataItem, "LIC_NOME") %><br />
            <b>AUI: </b><%# (DataBinder.Eval(Container.DataItem, "IMG_AUI").ToString() == "S" ? "Sim" : "Não") %><br /><br />
            </span>
        </ItemTemplate>  
        <AlternatingItemTemplate> 
            <b><small><%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %></small></b><br />
            <a href="../../img/imgbnk/BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg" rel="lightbox[imageBank]" title="<%#DataBinder.Eval(Container.DataItem, "IMG_CODIGO")%> - <%#DataBinder.Eval(Container.DataItem, "IMG_TITULO")%>" id="<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>">
            <img src="ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg" border="0">
            </a><br />
            <a href="JavaScript:AddToFavorite('<%# DataBinder.Eval(Container.DataItem, "IMG_ID") %>');">Adicionar a favoritos</a>
            <span id="spn_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>" style="visibility: hidden; display: none">
            <br /><br /><%# DataBinder.Eval(Container.DataItem, "IMG_DETALHES") %><br /><br />
            <b>Licença </b><%# DataBinder.Eval(Container.DataItem, "LIC_NOME") %><br />
            <b>AUI: </b><%# (DataBinder.Eval(Container.DataItem, "IMG_AUI").ToString() == "S" ? "Sim" : "Não")%><br /><br />
            </span>
        </AlternatingItemTemplate>
    </asp:DataList>
    <asp:Label ID="lblNoImages" runat="server" Text='As imagens relacionadas à pesquisa realizada podem estar com as atualizações programadas. Consulte-nos em <a href="mailto:atendimento@fototecainternacional.com.br">atendimento@fototecainternacional.com.br</a>.'
        Visible="False"></asp:Label><br />
    <table width="100%">
        <tr align="center" valign="top">
            <td colspan="2" style="background-color: Silver">
                <asp:ImageButton ID="imbPrevBot" runat="server" AlternateText="Página anterior" Enabled="False"
                    Height="16px" ImageUrl="~/img/tumbs_nav/prev.gif" OnClick="imbPrevBot_Click"
                    Width="16px" />
                <asp:Label ID="lblPaginacaoBot" runat="server"></asp:Label>
                <asp:ImageButton ID="imbNextBot" runat="server" AlternateText="Próxima página" Enabled="False"
                    Height="16px" ImageUrl="~/img/tumbs_nav/next.gif" OnClick="imbNextBot_Click"
                    Width="16px" /></td>            
        </tr>
        <tr align="center" valign="top">
            <td align="left">
                <asp:Label ID="lblQtdImagensBot" runat="server"></asp:Label></td>
            <td align="right">
                </td>
        </tr>
    </table>
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
</asp:Content>
