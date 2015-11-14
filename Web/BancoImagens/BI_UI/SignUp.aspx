<%@ Page Language="C#" MasterPageFile="~/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="BancoImagens.SignUp" Title=".:: Fototeca Internacional - Banco de Imagens ::." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
   function campo_numerico (){
         if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;
   }

   /*function cnpj_cpf verifica qual das funcoes tem que chamar cpf ou cnpj*/
   function cnpj_cpf(campo,documento,f,formi){
      form = formi;

      for (Count = 0; Count < 2; Count++){
         if (form.rad[Count].checked)
            break;
      }

      if (Count == 0){
         mascara_cpf (campo,documento,f);
      }
      else{
         mascara_cnpj (campo,documento,f);
      }
   }

   function mascara_cnpj (campo,documento,f){
      var mydata = '';
      mydata = mydata + documento;

      if (mydata.length == 2){
         mydata   = mydata + '.';

         ct_campo = eval("document."+f+"."+campo+".value = mydata");
         ct_campo;
      }

      if (mydata.length == 6){
               mydata   = mydata + '.';

               ct_campo = eval("document."+f+"."+campo+".value = mydata");
               ct_campo;
      }

      if (mydata.length == 10){
         mydata      = mydata + '/';

         ct_campo1 = eval("document."+f+"."+campo+".value = mydata");
         ct_campo1;
      }

      if (mydata.length == 15){
               mydata      = mydata + '-';

               ct_campo1 = eval("document."+f+"."+campo+".value = mydata");
               ct_campo1;
      }

      if (mydata.length == 18){
         valida_cnpj(f,campo);
      }
   }

   function mascara_cpf (campo,documento,f){
         var mydata = '';
         mydata = mydata + documento;

         if (mydata.length == 3){
            mydata   = mydata + '.';

            ct_campo = eval("document."+f+"."+campo+".value = mydata");
            ct_campo;
         }

         if (mydata.length == 7){
                  mydata   = mydata + '.';

                  ct_campo = eval("document."+f+"."+campo+".value = mydata");
                  ct_campo;
         }

         if (mydata.length == 11){
            mydata      = mydata + '-';

            ct_campo1 = eval("document."+f+"."+campo+".value = mydata");
            ct_campo1;
         }

         if (mydata.length == 14){
            valida_cpf(f,campo);
         }
   }

   function valida_cnpj(f,campo){
         pri = eval("document."+f+"."+campo+".value.substring(0,2)");
         seg = eval("document."+f+"."+campo+".value.substring(3,6)");
         ter = eval("document."+f+"."+campo+".value.substring(7,10)");
         qua = eval("document."+f+"."+campo+".value.substring(11,15)");
         qui = eval("document."+f+"."+campo+".value.substring(16,18)");

         var i;
         var numero;
         var situacao = '';

         numero = (pri+seg+ter+qua+qui);

         s = numero;

         c = s.substr(0,12);
         var dv = s.substr(12,2);
         var d1 = 0;

         for (i = 0; i < 12; i++){
            d1 += c.charAt(11-i)*(2+(i % 8));
         }

         if (d1 == 0){
            var result = "falso";
         }
            d1 = 11 - (d1 % 11);

         if (d1 > 9) d1 = 0;

            if (dv.charAt(0) != d1){
               var result = "falso";
            }

         d1 *= 2;
         for (i = 0; i < 12; i++){
            d1 += c.charAt(11-i)*(2+((i+1) % 8));
         }

         d1 = 11 - (d1 % 11);
         if (d1 > 9) d1 = 0;

            if (dv.charAt(1) != d1){
               var result = "falso";
            }

         if (result == "falso") {
            alert("CNPJ inválido!");
            aux1 = eval("document."+f+"."+campo+".focus");
            aux2 = eval("document."+f+"."+campo+".value = ''");
         }
   }

   function valida_cpf(f,campo){
         pri = eval("document."+f+"."+campo+".value.substring(0,3)");
         seg = eval("document."+f+"."+campo+".value.substring(4,7)");
         ter = eval("document."+f+"."+campo+".value.substring(8,11)");
         qua = eval("document."+f+"."+campo+".value.substring(12,14)");

         var i;
         var numero;

         numero = (pri+seg+ter+qua);

         s = numero;
         c = s.substr(0,9);
         var dv = s.substr(9,2);
         var d1 = 0;

         for (i = 0; i < 9; i++){
            d1 += c.charAt(i)*(10-i);
         }

         if (d1 == 0){
            var result = "falso";
         }

         d1 = 11 - (d1 % 11);
         if (d1 > 9) d1 = 0;

         if (dv.charAt(0) != d1){
            var result = "falso";
         }

         d1 *= 2;
         for (i = 0; i < 9; i++){
            d1 += c.charAt(i)*(11-i);
         }

         d1 = 11 - (d1 % 11);
         if (d1 > 9) d1 = 0;

         if (dv.charAt(1) != d1){
            var result = "falso";
         }

         if (result == "falso") {
            alert("CPF inválido!");
            aux1 = eval("document."+f+"."+campo+".focus");
            aux2 = eval("document."+f+"."+campo+".value = ''");
         }
   }
   
   function ValidaCheck(){
      var obj = document.getElementById("ctl00_ContentPlaceHolder1_chkAceite");
      
      if (obj.checked) {
         return true;
      }
      
      return false;
   }
   
   function openTerms(anchor){
        var w = 600; // largura da janela
        var h = 400; // altura da janela
        var lado = (screen.width - w) / 2;
        var topo = (screen.height - h) / 2;
        
      window.open("TermsConditions.aspx" + anchor, "Terms","height="+ h + ",width=" + w + ",left=" + lado + ",top=" + topo + 
                ",toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,menubar=no");
   }
</script>
    <span class="Titulo">Cadastre-se</span><br />
    <br />
    O cadastro é GRATÍS e permite acesso as seguintes funcionalidades:<br />
    <br />
    <ul>
        <li>Adicionar as suas imagens selecionadas ao ligthbox;</li>
        <li>Solicitar orçamento de compra de imagens à Fototeca Internacional.<br />
            &nbsp;<br />
    <span class="SubTitulo">Termos e condições</span><br />
    <br />
    Leia cuidadosamente os <a href="javascript:openTerms('');">Termos e condições para uso deste site</a> e nossa <a href="javascript:openTerms('#politica');">Política de
    Privacidade.</a><br />
    <br />
    <asp:CheckBox ID="chkAceite" runat="server" Text="Li e aceito os Termos e condições para uso deste site e nossa Política de Privacidade." />
            <asp:CustomValidator ID="ctvCheck" runat="server" ErrorMessage="É necessário ler os termos e condições para uso deste site e assinalar a confirmação da leitura."
                OnServerValidate="ctvCheck_ServerValidate" ClientValidationFunction="ValidaCheck();">*</asp:CustomValidator></li>
    </ul>
    <p style="text-align: center">
        <asp:ValidationSummary ID="vlsSignUp" runat="server" Width="427px" />
        &nbsp;
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
            <asp:ListItem Value="AC">Acre</asp:ListItem>
            <asp:ListItem Value="AL">Alagoas</asp:ListItem>
            <asp:ListItem Value="AP">Amap&#225;</asp:ListItem>
            <asp:ListItem Value="AM">Amazonas</asp:ListItem>
            <asp:ListItem Value="BA">Bahia</asp:ListItem>
            <asp:ListItem Value="CE">Ceara</asp:ListItem>
            <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
            <asp:ListItem Value="ES">Espirito Santo</asp:ListItem>
            <asp:ListItem Value="GO">Goi&#225;s</asp:ListItem>
            <asp:ListItem Value="MA">Maranh&#227;o</asp:ListItem>
            <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
            <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
            <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
            <asp:ListItem Value="PA">Para</asp:ListItem>
            <asp:ListItem Value="PB">Paraiba</asp:ListItem>
            <asp:ListItem Value="PR">Parana</asp:ListItem>
            <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
            <asp:ListItem Value="PI">Piaui</asp:ListItem>
            <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
            <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
            <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
            <asp:ListItem Value="RO">Rond&#244;nia</asp:ListItem>
            <asp:ListItem Value="RR">Roraima</asp:ListItem>
            <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
            <asp:ListItem Value="SE">Sergipe</asp:ListItem>
            <asp:ListItem Value="SP">S&#227;o Paulo</asp:ListItem>
            <asp:ListItem Value="TO">Tocantins</asp:ListItem>
        </asp:DropDownList>
        </p>
    <p style="text-align: center">
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
            Height="56px" OnCreatedUser="CreateUserWizard1_CreatedUser" Width="421px" ContinueDestinationPageUrl="~/Default.aspx" FinishDestinationPageUrl="~/Default.aspx" CancelButtonText="Cancelar" CreateUserButtonText="Criar usuário" DuplicateEmailErrorMessage="O e-mail digitado já está em uso. Por favor, entre com um e-mail diferente." DuplicateUserNameErrorMessage="Entre com um usuário diferente." FinishCompleteButtonText="Finalizar" FinishPreviousButtonText="Anterior" InvalidAnswerErrorMessage="Entre com uma resposta de segurança diferente." InvalidEmailErrorMessage="Entre com um e-mail válido." InvalidPasswordErrorMessage="Tamanho mínimo da senha: {0}. Caracteres não alfa-numéricos: {1}." InvalidQuestionErrorMessage="Entre com uma pergunta de segurança diferente." OnNextButtonClick="CreateUserWizard1_NextButtonClick" StartNextButtonText="Próximo" StepNextButtonText="Próximo" StepPreviousButtonText="Anterior" UnknownErrorMessage="Sua conta não foi criada. Por favor, tente novamente.">
            <WizardSteps>
                <asp:WizardStep runat="server" StepType="Start" Title="Dados Gerais">
                    <asp:FormView ID="frvGerais" runat="server" DataKeyNames="CLI_ID" DataSourceID="sdsSignUp"
                        DefaultMode="Insert" Width="100%">
                        <InsertItemTemplate>
                            <table border="0" style="font-size: 100%; width: 100%; font-family: Verdana; height: 177px">
                                <tr>
                                    <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #507cd1">
                                        Dados gerais</td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 50%">
                                        Razão Social / Nome:</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_NOME_RAZAO_SOCIALTextBox" runat="server" Text='<%# Bind("CLI_NOME_RAZAO_SOCIAL") %>' MaxLength="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Razão Social / Nome é obrigatório." ControlToValidate="CLI_NOME_RAZAO_SOCIALTextBox">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        CNPJ / CPF:</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_CPF_CNPJTextBox" runat="server" Text='<%# Bind("CLI_CPF_CNPJ") %>' MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCnpjCpf" runat="server" ErrorMessage="CNPJ / CPF é obrigatório." ControlToValidate="CLI_CPF_CNPJTextBox">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCnpjCpf" runat="server" ErrorMessage="Digite somente números sem formatação." ControlToValidate="CLI_CPF_CNPJTextBox" ValidationExpression="\d[0-9]{0,20}">*</asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Telefone Comercial:<br /><small><i>(DDD - Telefone)</i></small></td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_TELEFONE_COMTextBox" runat="server" Text='<%# Bind("CLI_TELEFONE_COM") %>' MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefone" runat="server" ErrorMessage="Telefone Comercial é obrigatório." ControlToValidate="CLI_TELEFONE_COMTextBox">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtelefone" runat="server" ErrorMessage="Digite somente números. Ex.: 11 12345678" ControlToValidate="CLI_TELEFONE_COMTextBox" ValidationExpression="\d[0-9]{0,3} [0-9]{0,15}">*</asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Contato:</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_CONTATOTextBox" runat="server" Text='<%# Bind("CLI_CONTATO") %>' MaxLength="30"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Telefone Contato:<br /><small><i>(DDD - Telefone)</i></small></td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_TELEFONE_CONTATOTextBox" runat="server" Text='<%# Bind("CLI_TELEFONE_CONTATO") %>' MaxLength="15"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTelContato" runat="server" ErrorMessage="Digite somente números. Ex.: 11 12345678" ControlToValidate="CLI_TELEFONE_CONTATOTextBox" ValidationExpression="\d[0-9]{0,3} [0-9]{0,15}">*</asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Fax:<br /><small><i>(DDD - Telefone)</i></small></td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_FAXTextBox" runat="server" Text='<%# Bind("CLI_FAX") %>' MaxLength="15"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revFax" runat="server" ErrorMessage="Digite somente números. Ex.: 11 12345678" ControlToValidate="CLI_FAXTextBox" ValidationExpression="\d[0-9]{0,3} [0-9]{0,15}">*</asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #507cd1">
                                        Dados de correspondência</td>
                                </tr>
                                <tr>
                                </tr>
                                    <td align="right">
                                        Endereço:</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="CLI_ENDERECOTextBox" runat="server" Text='<%# Bind("CLI_ENDERECO") %>' MaxLength="100"></asp:TextBox></td>
                                    <tr>
                                    </tr>
                                        <td align="right">
                                            Número:</td>
                                        <td style="width: 50%">
                                            <asp:TextBox ID="CLI_NUMEROTextBox" runat="server" Text='<%# Bind("CLI_NUMERO") %>' MaxLength="50"></asp:TextBox></td>
                                        <tr>
                                        </tr>
                                            <td align="right">
                                                Complemento:</td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="CLI_COMPLEMENTOTextBox" runat="server" Text='<%# Bind("CLI_COMPLEMENTO") %>' MaxLength="50"></asp:TextBox></td>
                                            <tr>
                                            </tr>
                                                <td align="right">
                                                    Estado:</td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="CLI_ESTADOTextBox" MaxLength="2" runat="server" Text='<%# Bind("CLI_ESTADO") %>'></asp:TextBox></td>
                                                <tr>
                                                </tr>
                                                    <td align="right">
                                                        Cidade:</td>
                                                    <td style="width: 50%">
                                                        <asp:TextBox ID="CLI_CIDADETextBox" runat="server" Text='<%# Bind("CLI_CIDADE") %>' MaxLength="50"></asp:TextBox></td>
                                                    <tr>
                                                    </tr>
                                                        <td align="right">
                                                            CEP:</td>
                                                        <td style="width: 50%">
                                                            <asp:TextBox ID="CLI_CEPTextBox" runat="server" Text='<%# Bind("CLI_CEP") %>' MaxLength="9"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revCEP" runat="server" ErrorMessage="Digite somente números sem formatação." ControlToValidate="CLI_CEPTextBox" ValidationExpression="\d[0-9]{0,9}">*</asp:RegularExpressionValidator></td>
                                                        <tr>
                                                            <td align="center" colspan="2" style="color: red; height: 14px;">
                                                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                                            </td>
                                                        </tr>
                            </table>
                            <asp:TextBox ID="UserIdTextBox" runat="server" Text='<%# Bind("UserId") %>' Visible="false"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:FormView>
                </asp:WizardStep>
                <asp:CreateUserWizardStep runat="server" Title="Dados de acesso">
                    <ContentTemplate>
                        <table border="0" style="font-size: 100%; width: 393px; font-family: Verdana; height: 56px">
                            <tr>
                                <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #507cd1">
                                    Dados de acesso</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuário:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirme a senha:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Pergunta de segurança:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                        ErrorMessage="Security question is required." ToolTip="Security question is required."
                                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Resposta de segurança:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                        ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="A senha e a confirmação da senha devem ser iguais."
                                        ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: red">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server">
                    <ContentTemplate>
                        <table border="0" style="font-size: 100%; width: 393px; font-family: Verdana; height: 56px">
                            <tr>
                                <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #507cd1">
                                    Completo</td>
                            </tr>
                            <tr>
                                <td>
                                    Sua conta foi criada com sucesso.</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinueButton" runat="server" BackColor="White" BorderColor="#507CD1"
                                        BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="Continue"
                                        Font-Names="Verdana" ForeColor="#284E98" Text="Continue" ValidationGroup="CreateUserWizard1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>
            <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" ForeColor="White" />
            <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" ForeColor="#284E98" />
            <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
                Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
            <CreateUserButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" ForeColor="#284E98" />
            <ContinueButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" ForeColor="#284E98" />
            <StepStyle Font-Size="0.8em" />
        </asp:CreateUserWizard>
        <asp:SqlDataSource ID="sdsSignUp" runat="server" ConnectionString="<%$ ConnectionStrings:BD_IMAGENSConnectionString %>"
            DeleteCommand="DELETE FROM [BI_CLIENTE] WHERE [CLI_ID] = @CLI_ID" InsertCommand="INSERT INTO [BI_CLIENTE] ([USU_ID], [CLI_NOME_RAZAO_SOCIAL], [CLI_EMAIL], [CLI_CPF_CNPJ], [CLI_TELEFONE_COM], [CLI_FAX], [CLI_CONTATO], [CLI_TELEFONE_CONTATO], [CLI_ENDERECO], [CLI_NUMERO], [CLI_COMPLEMENTO], [CLI_ESTADO], [CLI_CIDADE], [CLI_CEP], [CLI_DT_ULT_ATLZ], [UserName]) VALUES (@USU_ID, @CLI_NOME_RAZAO_SOCIAL, @CLI_EMAIL, @CLI_CPF_CNPJ, @CLI_TELEFONE_COM, @CLI_FAX, @CLI_CONTATO, @CLI_TELEFONE_CONTATO, @CLI_ENDERECO, @CLI_NUMERO, @CLI_COMPLEMENTO, @CLI_ESTADO, @CLI_CIDADE, @CLI_CEP, @CLI_DT_ULT_ATLZ, @UserName)"
            SelectCommand="SELECT * FROM [BI_CLIENTE]" UpdateCommand="UPDATE [BI_CLIENTE] SET [USU_ID] = @USU_ID, [CLI_NOME_RAZAO_SOCIAL] = @CLI_NOME_RAZAO_SOCIAL, [CLI_EMAIL] = @CLI_EMAIL, [CLI_CPF_CNPJ] = @CLI_CPF_CNPJ, [CLI_TELEFONE_COM] = @CLI_TELEFONE_COM, [CLI_FAX] = @CLI_FAX, [CLI_CONTATO] = @CLI_CONTATO, [CLI_TELEFONE_CONTATO] = @CLI_TELEFONE_CONTATO, [CLI_ENDERECO] = @CLI_ENDERECO, [CLI_NUMERO] = @CLI_NUMERO, [CLI_COMPLEMENTO] = @CLI_COMPLEMENTO, [CLI_ESTADO] = @CLI_ESTADO, [CLI_CIDADE] = @CLI_CIDADE, [CLI_CEP] = @CLI_CEP, [CLI_DT_ULT_ATLZ] = @CLI_DT_ULT_ATLZ, [UserName] = @UserName WHERE [CLI_ID] = @CLI_ID">
            <DeleteParameters>
                <asp:Parameter Name="CLI_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="USU_ID" Type="Int64" />
                <asp:Parameter Name="CLI_NOME_RAZAO_SOCIAL" Type="String" />
                <asp:Parameter Name="CLI_EMAIL" Type="String" />
                <asp:Parameter Name="CLI_CPF_CNPJ" Type="String" />
                <asp:Parameter Name="CLI_TELEFONE_COM" Type="String" />
                <asp:Parameter Name="CLI_FAX" Type="String" />
                <asp:Parameter Name="CLI_CONTATO" Type="String" />
                <asp:Parameter Name="CLI_TELEFONE_CONTATO" Type="String" />
                <asp:Parameter Name="CLI_ENDERECO" Type="String" />
                <asp:Parameter Name="CLI_NUMERO" Type="String" />
                <asp:Parameter Name="CLI_COMPLEMENTO" Type="String" />
                <asp:Parameter Name="CLI_ESTADO" Type="String" />
                <asp:Parameter Name="CLI_CIDADE" Type="String" />
                <asp:Parameter Name="CLI_CEP" Type="Int32" />
                <asp:Parameter Name="CLI_DT_ULT_ATLZ" Type="DateTime" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="CLI_ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="USU_ID" Type="Int64" />
                <asp:Parameter Name="CLI_NOME_RAZAO_SOCIAL" Type="String" />
                <asp:Parameter Name="CLI_EMAIL" Type="String" />
                <asp:Parameter Name="CLI_CPF_CNPJ" Type="String" />
                <asp:Parameter Name="CLI_TELEFONE_COM" Type="String" />
                <asp:Parameter Name="CLI_FAX" Type="String" />
                <asp:Parameter Name="CLI_CONTATO" Type="String" />
                <asp:Parameter Name="CLI_TELEFONE_CONTATO" Type="String" />
                <asp:Parameter Name="CLI_ENDERECO" Type="String" />
                <asp:Parameter Name="CLI_NUMERO" Type="String" />
                <asp:Parameter Name="CLI_COMPLEMENTO" Type="String" />
                <asp:Parameter Name="CLI_ESTADO" Type="String" />
                <asp:Parameter Name="CLI_CIDADE" Type="String" />
                <asp:Parameter Name="CLI_CEP" Type="String" />
                <asp:Parameter Name="CLI_DT_ULT_ATLZ" Type="DateTime" />
                <asp:Parameter Name="UserName" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        &nbsp;</p>
</asp:Content>
