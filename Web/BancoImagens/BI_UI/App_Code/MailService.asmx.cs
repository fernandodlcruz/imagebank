using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Net.Mail;
using System.Configuration;

namespace BancoImagens.Services
{
    /// <summary>
    /// Summary description for MailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class MailService : System.Web.Services.WebService
    {

        [WebMethod]
        public void SendMail(string emailDestinatario, string assunto, string strmensagem)
        {
            //Cria o objeto que envia o e-mail 
            SmtpClient client = new SmtpClient("smtp.fototecainternacional.com.br", 25);

            //Cria o endereço de email do remetente 
            MailAddress de = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");

            //Cria o endereço de email do destinatário -->
            //MailAddress para = new MailAddress(emailDestinatario);
            MailAddress para = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");

            MailMessage mensagem = new MailMessage(de, para);

            mensagem.IsBodyHtml = true;

            //Assunto do email 
            mensagem.Subject = assunto;

            //Conteúdo do email 
            mensagem.Body = strmensagem;

            try
            {
                //Envia o email 
                client.Send(mensagem);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}