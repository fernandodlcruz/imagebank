<%@ Page Language="C#" MasterPageFile="~/MasterPages/Cpanel.Master" AutoEventWireup="true" CodeBehind="CdsReg.aspx.cs" Inherits="BancoImagens.ControlPanel.Cds.CdsReg" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Titulo">CDs</span><br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <br />
    <table style="color: #000000;">
        <tr>
            <td align="right" style="width: 123px">
                Código:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtCodigo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td rowspan="7" align="center">
                <asp:Image ID="imgCapa" runat="server" Height="77px" Visible="False" Width="89px" /><br />
                <br />
                <asp:Button ID="btnSendImg" runat="server" OnClick="btnSendImg_Click" Text="Enviar capa" /></td>    
        </tr>
        <tr>
            <td align="right" style="width: 123px">
                Nome:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Width="343px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 123px">
                Valor:&nbsp;</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtValor" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revValor" runat="server" ControlToValidate="txtValor"
                    ErrorMessage="Informe somente números separando somente as casas decimais com vírgula."
                    ValidationExpression="\d[0-9]{0,18},[0-9]{0,2}">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 123px">
                Número de imagens:</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtNroImagens" runat="server" MaxLength="6"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 123px">
                Resolução:</td>
            <td style="width: 356px">
                <asp:TextBox ID="txtResolucao" runat="server" MaxLength="80"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 123px">
                Capa:</td>
            <td style="width: 356px">
                <asp:FileUpload ID="fluCapa" runat="server" Width="347px" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 123px">
            </td>
            <td style="width: 356px">
            </td>
        </tr>
    </table>
    <br />
    <span class="SubTitulo">Imagens do CD</span><br />
    <br /><table style="color: #000000;">
        <tr>
            <td align="right" style="width: 137px">
                Arquivo excel com a lista de imagens do CD:&nbsp;</td>
            <td style="width: 356px">
                <asp:FileUpload ID="fluImagensCD" runat="server" Width="347px" /></td>
            <td align="center">
                <asp:Button ID="btnLoadImages" runat="server" Text="Carregar Imagens" OnClick="btnLoadImages_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:DataList ID="dtlImagesCD" runat="server" CellPadding="4"
                    HorizontalAlign="Center" RepeatColumns="3" Width="100%" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal">
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <SelectedItemStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate> 
                        <img src="../../SearchEngine/ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "NOME_IMG") %>"> 
                    </ItemTemplate>  
                    <AlternatingItemTemplate> 
                        <img src="../../SearchEngine/ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "NOME_IMG") %>"> 
                    </AlternatingItemTemplate> 
                </asp:DataList></td>
        </tr>
        <tr>
            <td align="right" style="width: 137px">
            </td>
            <td style="width: 356px">
            </td>
            <td align="center"></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Salvar" />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Excluir" Enabled="False" />
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancelar" />
</asp:Content>
