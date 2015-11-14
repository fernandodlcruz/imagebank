using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Net.Mail;

namespace BancoImagens.App_Code
{
    public class Mail
    {
        /// <summary>
        /// Método para enviar email
        /// </summary>
        /// <param name="emailDestinatario">Email do destinatário</param>
        /// <param name="assunto">Assunto do email</param>
        /// <param name="mensagem">mensagem do email</param>

        //public static void EnviarEmail(string emailDestinatario, string assunto, string mensagem)
        //{
        //    //Cria o objeto que envia o e-mail 
        //    SmtpClient client = new SmtpClient();

        //    //Cria o endereço de email do remetente 
        //    MailAddress de = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");

        //    //Cria o endereço de email do destinatário -->
        //    MailAddress para = new MailAddress(emailDestinatario);

        //    MailMessage mensagem = new MailMessage(de, para);

        //    mensagem.IsBodyHtml = true;

        //    //Assunto do email 
        //    mensagem.Subject = assunto;

        //    //Conteúdo do email 
        //    mensagem.Body = mensagem;

        //    try
        //    {
        //        //Envia o email 
        //        client.Send(mensagem);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}