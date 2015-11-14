using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using BancoImagens.Services;
using System.Net.Mail;

namespace BancoImagens
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            frvGerais.ChangeMode(FormViewMode.Insert);
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            //string _userID = Membership.GetUser(CreateUserWizard1.UserName).ProviderUserKey.ToString();
            StringBuilder strDados = new StringBuilder("");
            //TextBox txtUserId = (TextBox)frvGerais.FindControl("UserIdTextBox");

            try
            {
                // Recupera os dados do cadastro e envia por e-mail
                strDados.Append("<b>Cliente:</b> " + (frvGerais.FindControl("CLI_NOME_RAZAO_SOCIALTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>CPF/CNPJ:</b> " + (frvGerais.FindControl("CLI_CPF_CNPJTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Telefone principal:</b> " + (frvGerais.FindControl("CLI_TELEFONE_COMTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Pessoa para contato:</b> " + (frvGerais.FindControl("CLI_CONTATOTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Telefone direto do contato:</b> " + (frvGerais.FindControl("CLI_TELEFONE_CONTATOTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>FAX:</b> " + (frvGerais.FindControl("CLI_FAXTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Endereço:</b> " + (frvGerais.FindControl("CLI_ENDERECOTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Número:</b> " + (frvGerais.FindControl("CLI_NUMEROTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Complemento:</b> " + (frvGerais.FindControl("CLI_COMPLEMENTOTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>Estado:</b> " + (frvGerais.FindControl("CLI_ESTADOTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>cidade:</b> " + (frvGerais.FindControl("CLI_CIDADETextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/>");
                strDados.Append("<b>CEP:</b> " + (frvGerais.FindControl("CLI_CEPTextBox") as TextBox).Text.Replace('/', ' ').ToString() + "<br/><br/>");
                strDados.Append("<b>Usuário:</b> " + CreateUserWizard1.UserName.ToString() + "<br/>");
                strDados.Append("<b>E-mail:</b> " + CreateUserWizard1.Email.ToString() + "<br/>");

                sdsSignUp.InsertParameters["UserName"].DefaultValue = CreateUserWizard1.UserName;
                frvGerais.InsertItem(false);

                SendMail("", "Novo cadastro realizado no site.", strDados.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void ctvCheck_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (chkAceite.Checked == true);
        }

        protected void CreateUserWizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (!ctvCheck.IsValid)
            {
                return;
            }
        }

        protected void SendMail(string to, string assunto, string strMensagem)
        {
            //Instancia o Objeto Email como MailMessage 
            SmtpClient client = new SmtpClient();
            MailAddress adrTo = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");
            MailAddress adrFrom = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");
            MailMessage Email = new MailMessage(adrFrom, adrTo);

            try
            {
                //Atribui ao método Subject o assunto da mensagem 
                Email.Subject = assunto;

                //Define o formato da mensagem que pode ser Texto ou Html 
                Email.IsBodyHtml = true; // MailFormat.Html;

                //Atribui ao método Body a texto da mensagem 
                Email.Body = strMensagem;


                //Envia a mensagem baseado nos dados do objeto Email 
                client.Send(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
