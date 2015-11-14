<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="PromotionList.aspx.cs" Inherits="BancoImagens.Promotions.PromotionList" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span class="Titulo">Promoções</span><br />
    <br />
    Confira as promoções que a Fototeca Internacional preparou para você adquirir suas
    imagens:<br />
    <br />
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
    <br />
    <asp:DataList ID="dtlCDs" runat="server" CellPadding="5" CellSpacing="5" HorizontalAlign="Center"
        RepeatColumns="3" RepeatDirection="Horizontal" Width="100%">
        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemTemplate>
            <a href='PromotionDetail.aspx?cdId=<%# DataBinder.Eval(Container.DataItem, "ID") %>&name=<%# DataBinder.Eval(Container.DataItem, "Nome")%>'>
                <img border="0" src='../SearchEngine/ThumbImage.aspx?img=<%# DataBinder.Eval(Container.DataItem, "CAPA") %>&loc=cd'>
            </a>
            <br />
            <asp:Label ID="lblImgId" runat="server" Visible="true"><%# DataBinder.Eval(Container.DataItem, "Nome") %><br />
            R$ <%# DataBinder.Eval(Container.DataItem, "VALOR") %><br />
            <%# DataBinder.Eval(Container.DataItem, "NumeroImagens")%> imagens no CD<br />Resolução
            <%# DataBinder.Eval(Container.DataItem, "Resolucao")%></asp:Label>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <a href='PromotionDetail.aspx?cdId=<%# DataBinder.Eval(Container.DataItem, "ID") %>&name=<%# DataBinder.Eval(Container.DataItem, "Nome")%>'>
                <img border="0" src='../SearchEngine/ThumbImage.aspx?img=<%# DataBinder.Eval(Container.DataItem, "CAPA") %>&loc=cd'>
            </a>
            <br />
            <asp:Label ID="lblImgId" runat="server" Visible="true"><%# DataBinder.Eval(Container.DataItem, "Nome")%><br />
            R$ <%# DataBinder.Eval(Container.DataItem, "VALOR") %><br />
            <%# DataBinder.Eval(Container.DataItem, "NumeroImagens")%> imagens no CD<br />Resolução
            <%# DataBinder.Eval(Container.DataItem, "Resolucao")%></asp:Label>
        </AlternatingItemTemplate>
    </asp:DataList>
    <asp:Label ID="lblNoPromotions" runat="server" Text="No momento não estamos disponibilizando nenhuma promoção."
        Visible="False"></asp:Label><br />
    <hr style="border-left-color: silver; border-bottom-color: silver; border-top-style: dashed;
        border-top-color: silver; border-right-style: dashed; border-left-style: dashed;
        height: 1px; border-right-color: silver; border-bottom-style: dashed" />
</asp:Content>
