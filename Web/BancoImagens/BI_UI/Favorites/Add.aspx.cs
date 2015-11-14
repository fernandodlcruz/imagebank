using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FWBancoImagens.BO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;

namespace BancoImagens.Favorites
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idImg;
            TFavoritos dto;
            TClientes dtoClie;
            TImagens dtoImg;
            IList<TImagens> lstImgs;

            try
            {
                idImg = Request.QueryString["img"];

                if (idImg != "")
                {
                    dto = new TFavoritos();
                    dtoClie = new TClientes();
                    dtoImg = new TImagens();
                    lstImgs = new List<TImagens>();

                    dtoClie.Id = Util.ReturnUserId(Request.ServerVariables["REMOTE_USER"]);
                    dtoImg.Id = Convert.ToInt64(idImg);

                    dto.Cliente = dtoClie;
                    lstImgs.Add(dtoImg);
                    dto.Imagem = lstImgs;

                    BFavoritos objBO = BFavoritos.getInstance();

                    objBO.Incluir(dto);

                    lblMsg.Text = "Imagem adicionada aos favoritos!";                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir a imagem ao favorito: " + ex.Message;
            }
        }

        /*Ola Daniel,

Pelo fato de j� existir a autentica��o no IIS, que provavelmente no seu caso � anonima, ent�o ele deve "zerar" essas informa��es antes de passar para o ASP.NET. Mesmo no primeiro est�gio de uma requisi��o ASP.NET, voc� n�o tem essas informa��es:

public class Modulo : IHttpModule
{
    public void Dispose() { }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += (sender, args) =>
        {
            HttpContext ctx = ((HttpApplication)sender).Context;
            ctx.Response.Write("LOGON_USER: " + ctx.Request.ServerVariables["LOGON_USER"] + "<br>");
            ctx.Response.Write("REMOTE_USER: " + ctx.Request.ServerVariables["REMOTE_USER"] + "<br>");
            ctx.Response.Write("AUTH_TYPE: " + ctx.Request.ServerVariables["AUTH_TYPE"] + "<br>");
        };
    }
}

Com a autentica��o Windows habilitada, essas informa��es s�o repassadas para o ASP.NET e, consequentemente, voc� deve conseguir recuper�-las. J� tentou analisar o modo "mix": http://msdn.microsoft.com/en-us/library/ms972958.aspx ? Al�m disso, n�o pode utilizar o ActiveDirectoryMembershipProvider para efetuar a autentica��o no AD via Forms e, consequentemente, o User.Identity.Name refletir� o nome do Windows.

Se estiver utilizando IIS 7.0, eu acredito que atrav�s de m�dulos que s�o acoplados ao pipeline do IIS talvez seja poss�vel fazer o que pretende
*/
    }
}
