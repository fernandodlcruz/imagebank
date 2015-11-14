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

Pelo fato de já existir a autenticação no IIS, que provavelmente no seu caso é anonima, então ele deve "zerar" essas informações antes de passar para o ASP.NET. Mesmo no primeiro estágio de uma requisição ASP.NET, você não tem essas informações:

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

Com a autenticação Windows habilitada, essas informações são repassadas para o ASP.NET e, consequentemente, você deve conseguir recuperá-las. Já tentou analisar o modo "mix": http://msdn.microsoft.com/en-us/library/ms972958.aspx ? Além disso, não pode utilizar o ActiveDirectoryMembershipProvider para efetuar a autenticação no AD via Forms e, consequentemente, o User.Identity.Name refletirá o nome do Windows.

Se estiver utilizando IIS 7.0, eu acredito que através de módulos que são acoplados ao pipeline do IIS talvez seja possível fazer o que pretende
*/
    }
}
