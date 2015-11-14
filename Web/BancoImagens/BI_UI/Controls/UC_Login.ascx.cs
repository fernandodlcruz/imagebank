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
using FWBancoImagens.Common;

namespace BancoImagens.Controls
{
    public partial class UC_Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblQtdImages.Text = "(" + Util.ReturnQuantityImages(Util.ReturnUserId(Request.ServerVariables["REMOTE_USER"])).ToString() + ")";
        }

        protected void lgnGeneral_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (Request.ServerVariables["REMOTE_USER"] != null)
            {
                Session["UserId"] = Util.ReturnUserId(Request.ServerVariables["REMOTE_USER"]);
            }
        }
    }
}