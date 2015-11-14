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
using FWBancoImagens.BO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;
using System.Net.Mail;

namespace BancoImagens.Favorites
{
    public partial class FavoritesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadFavorites();
            }
        }

        protected void LoadFavorites()
        {
            DataSet _ds = new DataSet();
            BImagens objBO = BImagens.getInstance();

            try
            {
                _ds = objBO.ListarFavoritos(Util.ReturnUserId(Request.ServerVariables["REMOTE_USER"]));

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    lblNoImages.Visible = false;
                    dtlImages.DataSource = _ds.Tables[0].DefaultView;
                    dtlImages.DataBind();
                }
                else
                {
                    dtlImages.DataSource = null;
                    dtlImages.DataBind();
                    lblNoImages.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar imagens favoritas: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CheckBox chkItem;
            DataBoundLiteralControl lblItem;
            string _imgIds = "";
            BFavoritos objBO = BFavoritos.getInstance();

            try
            {
                for (int i = 0; i < dtlImages.Items.Count; i++)
                {
                    chkItem = (CheckBox)dtlImages.Items[i].FindControl("chkImgFavorite");

                    if (chkItem.Checked)
                    {
                        lblItem = (DataBoundLiteralControl)dtlImages.Items[i].FindControl("lblImgId").Controls[0];
                        _imgIds += lblItem.Text + ",";
                    }
                }

                if (_imgIds != "")
                {
                    lblMsg.Text = "";
                    _imgIds = _imgIds.Substring(0, _imgIds.Length - 1);

                    objBO.Excluir(_imgIds);

                    LoadFavorites();
                }
                else
                {
                    lblMsg.Text = "Selecione a(s) imagem(ns) desejada(s) para exclusão.";
                }                
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir as imagens selecionadas: " + ex.Message;
            }
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            CheckBox chkItem;
            DataBoundLiteralControl lblItem;
            string _imgCodes = "";
            BClientes objBOClie;
            TClientes dtoClie;
            string strMsgMail;

            try
            {
                for (int i = 0; i < dtlImages.Items.Count; i++)
                {
                    chkItem = (CheckBox)dtlImages.Items[i].FindControl("chkImgFavorite");

                    if (chkItem.Checked)
                    {
                        lblItem = (DataBoundLiteralControl)dtlImages.Items[i].FindControl("lblImgCode").Controls[0];
                        _imgCodes += lblItem.Text + ",";
                    }
                }

                if (_imgCodes != "")
                {
                    lblMsg.Text = "";
                    _imgCodes = _imgCodes.Substring(0, _imgCodes.Length - 1);

                    objBOClie = BClientes.getInstance();

                    dtoClie = objBOClie.Pesquisar(Request.ServerVariables["REMOTE_USER"]);

                    strMsgMail = "O cliente " + dtoClie.RazaoSocial + " solicitou a compra da(s) seguinte(s) imagens: " + _imgCodes + "<br/>";
                    strMsgMail += "Dados para contato:<br/><br/>";
                    strMsgMail += "E-mail: " + dtoClie.Email + "<br/>";
                    strMsgMail += "Telefone: " + dtoClie.TelefoneContato + "<br/>";
                    strMsgMail += "Falar com: " + dtoClie.Contato + "<br/>";
                    strMsgMail += "FAX: " + dtoClie.Fax + "<br/>";

                    SendMail(Util.ReturnClientEmail(Request.ServerVariables["REMOTE_USER"]), "Solicitação de compra de imagens.", strMsgMail);

                    lblMsg.Text = "Email enviado com sucesso! Aguarde contato da Fototeca Internacional.";
                }
                else
                {
                    lblMsg.Text = "Selecione a(s) imagem(ns) que deseja adquirir.";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao enviar a solicitação de compra: " + ex.Message;
            }
            finally
            {
                objBOClie = null;
                dtoClie = null;
            }
        }

        protected void SendMail(string to, string assunto, string strMensagem)
        {
            //Instancia o Objeto Email como MailMessage 
            //SmtpClient client = new SmtpClient("smtp.fototecainternacional.com.br", 25);
            SmtpClient client = new SmtpClient();
            MailAddress adrTo = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");
            MailAddress adrFrom = new MailAddress(ConfigurationManager.AppSettings["eMail"], "Fototeca Internacional");
            MailMessage Email = new MailMessage(adrFrom, adrTo);

            try
            {
                //Atribui ao método From o valor do Remetente 
                //Email.From = adrFrom;

                //Atribui ao método To o valor do Destinatário 
                //Email.= adrTo;

                //Atribui ao método Subject o assunto da mensagem 
                Email.Subject = assunto;

                //Define o formato da mensagem que pode ser Texto ou Html 
                Email.IsBodyHtml = true; // MailFormat.Html;

                //Atribui ao método Body a texto da mensagem 
                Email.Body = strMensagem;

                //Define qual a url que deve ser usada como caminho para as imagens informadas no código html 
                //Email.UrlContentBase = "http://www.fototeca.com.br";

                //Define qual o host a ser usado para envio de mensagens, na locaweb é smtp2.locaweb.com.br 
                //SmtpMail.SmtpServer = "localhost";

                //Envia a mensagem baseado nos dados do objeto Email 
                //SmtpMail.Send(Email);
                client.Send(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
