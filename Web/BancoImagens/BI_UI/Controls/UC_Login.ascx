<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Login.ascx.cs" Inherits="BancoImagens.Controls.UC_Login" %>
&nbsp;<br />
<asp:LoginView ID="LoginView1" runat="server">
    <LoggedInTemplate>
<asp:Table ID="tblLogado" runat="server" Width="222px" Height="61px" BorderColor="#E0E0E0" BorderWidth="1px">
    <asp:TableRow runat="server" HorizontalAlign="Center">
        <asp:TableCell runat="server" BackColor="#507CD1" ForeColor="White">Dados do usuário</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell runat="server"><asp:LoginName ID="LoginName1" runat="server" FormatString="Bem vindo(a) {0}" /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
        <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell runat="server"><a href="~/Favorites/FavoritesList.aspx" runat="server" id="lnkFavorites">Minhas imagens favoritas</a></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" HorizontalAlign="Right">
        <asp:TableCell runat="server"><asp:LoginStatus ID="lgnStatus" runat="server" LogoutPageUrl="~/Default.aspx"/></asp:TableCell>
    </asp:TableRow>
</asp:Table>
    </LoggedInTemplate>
    <AnonymousTemplate>
<asp:Login ID="lgnGeneral" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderPadding="4"
    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
    ForeColor="#333333" FailureText="Falha no login. Por favor, tente novamente." VisibleWhenLoggedIn="False" PasswordLabelText="Senha:" PasswordRequiredErrorMessage="Senha é obrigatória." RememberMeText="Lembre-me na próxima vez." UserNameLabelText="Usuário:" UserNameRequiredErrorMessage="Usuário é obrigatório." Width="212px" OnLoggingIn="lgnGeneral_LoggingIn">
    <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <TextBoxStyle Font-Size="0.8em" />
    <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
</asp:Login>
        <br />
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" AnswerLabelText="Resposta:"
            AnswerRequiredErrorMessage="Resposta é obrigatória." BackColor="#EFF3FB" BorderColor="#B5C7DE"
            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
            Font-Size="0.8em" GeneralFailureText="A tentativa de recuperar a senha falhou. Por favor, tente novamente."
            QuestionFailureText="Sua resposta não pôde ser verificada. Por favor, tente novamente."
            QuestionInstructionText="Responda à pergunta a seguir para receber sua senha."
            QuestionLabelText="Pergunta:" QuestionTitleText="Pergunta de confirmação" SuccessText="Sua senha foi enviada para seu e-mail."
            UserNameFailureText="Não foi possível acessar suas informações. Por favor, tente novamente."
            UserNameInstructionText="Entre com seu usuário para receber sua senha." UserNameLabelText="Usuário:"
            UserNameRequiredErrorMessage="Usuário é obrigatório." UserNameTitleText="Esqueceu sua senha?" Width="212px">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <SuccessTextStyle Font-Bold="True" ForeColor="#507CD1" />
            <TextBoxStyle Font-Size="0.8em" />
            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
            <SubmitButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
        </asp:PasswordRecovery>
    </AnonymousTemplate>
</asp:LoginView>
