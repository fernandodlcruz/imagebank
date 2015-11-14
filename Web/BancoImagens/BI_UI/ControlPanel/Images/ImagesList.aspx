<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="ImagesList.aspx.cs" Inherits="BancoImagens.ControlPanel.Images.ImagesList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span style="font-size: 18pt; color: #4682b4">Imagens</span><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
    <br />
    <table style="width: 701px">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="SubTitulo" Text="Filtro para listagem"></asp:Label><br />
                <asp:Label ID="Label1" runat="server" Text="Código da imagem:"></asp:Label>
                <asp:TextBox ID="txtCodes" runat="server" MaxLength="10"></asp:TextBox>
                ou<br />
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
                    <tr>
                        <td align="left" width="50%">
                            <strong>Temas:</strong><br />
                            <asp:ListBox ID="lsbTemas" runat="server" AutoPostBack="True" Height="109px" OnSelectedIndexChanged="lsbTemas_SelectedIndexChanged"
                                SelectionMode="Multiple" Width="306px"></asp:ListBox><br />
                            <span style="font-size: 7pt">(mantenha a tecla &lt;CTRL&gt; pressionada para selecionar
                                mais de um tema)</span></td>
                        <td align="left" valign="bottom" width="50%">
                            <strong>Sub-Temas:<br />
                            </strong>
                            <asp:ListBox ID="lsbSubTemas" runat="server" Height="109px" SelectionMode="Multiple"
                                Width="306px"></asp:ListBox><br />
                            <span style="font-size: 7pt">(mantenha a tecla &lt;CTRL&gt; pressionada para selecionar
                                mais de um sub-tema)</span></td>
                    </tr>
                </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filtrar" /></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gdvImages" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gdvImages_PageIndexChanging">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="IMG_ID" />
            <asp:ImageField DataImageUrlField="IMG_CODIGO" DataImageUrlFormatString="~/SearchEngine/ThumbImage.aspx?img=BI_{0}.jpg"
                HeaderText="Imagem">
            </asp:ImageField>
            <asp:BoundField DataField="IMG_CODIGO" HeaderText="C&#243;digo" />
            <asp:BoundField DataField="IMG_TITULO" HeaderText="T&#237;tulo" />
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
