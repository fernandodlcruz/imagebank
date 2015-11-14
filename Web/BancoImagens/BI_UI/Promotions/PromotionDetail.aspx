<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="PromotionDetail.aspx.cs" Inherits="BancoImagens.Promotions.PromotionDetail" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span class="Titulo"></span><span style="font-size: 18pt; color: #4682b4; font-family: Tahoma, Verdana, Arial">
    Imagens do CD 
    <asp:Label ID="lblNameCD" runat="server"></asp:Label>
</span>
    <br />
    <br />
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
    <asp:DataList ID="dtlImages" runat="server" CellPadding="5" CellSpacing="5" HorizontalAlign="Center"
        RepeatColumns="3" RepeatDirection="Horizontal" Width="100%">
        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemTemplate>
            <b><small>
                <%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>
            </small></b>
            <br />
            <a id='<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>' href='../../img/imgbnk/BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg'
                rel="lightbox[imageBank]" title='<%#DataBinder.Eval(Container.DataItem, "IMG_CODIGO")%> - <%#DataBinder.Eval(Container.DataItem, "IMG_TITULO")%>'>
                <img border="0" src='../SearchEngine/ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg'>
            </a>
            <br />
            <asp:Label ID="lblImgId" runat="server" Visible="false"><%# DataBinder.Eval(Container.DataItem, "IMG_ID") %></asp:Label>
            <span id='spn_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>' style="visibility: hidden;
                display: none">
                <br />
                <br />
                <%# DataBinder.Eval(Container.DataItem, "IMG_DETALHES") %>
                <br />
                <br />
            </span>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <b><small>
                <%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>
            </small></b>
            <br />
            <a id='<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>' href='../../img/imgbnk/BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg'
                rel="lightbox[imageBank]" title='<%#DataBinder.Eval(Container.DataItem, "IMG_CODIGO")%> - <%#DataBinder.Eval(Container.DataItem, "IMG_TITULO")%>'>
                <img border="0" src='../SearchEngine/ThumbImage.aspx?img=BI_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>.jpg'>
            </a>
            <br />
            <asp:Label ID="lblImgId" runat="server" Visible="false"><%# DataBinder.Eval(Container.DataItem, "IMG_ID") %></asp:Label>
            <span id='spn_<%# DataBinder.Eval(Container.DataItem, "IMG_CODIGO") %>' style="visibility: hidden;
                display: none">
                <br />
                <br />
                <%# DataBinder.Eval(Container.DataItem, "IMG_DETALHES") %>
                <br />
                <br />
            </span>
        </AlternatingItemTemplate>
    </asp:DataList>
    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Voltar" /><br />
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
</asp:Content>
