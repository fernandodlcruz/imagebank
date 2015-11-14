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

namespace BancoImagens.ControlPanel.Drawer
{
    public partial class DrawerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BGavetas gaveta = BGavetas.getInstance();

            gdvGavetas.DataSource = gaveta.Listar();
            gdvGavetas.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("DrawerReg.aspx");
        }

        protected void gdvGavetas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string paramsAlt;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                paramsAlt = "?Id=" + e.Row.Cells[1].Text;

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                e.Row.Attributes.Add("onClick", "javascript:parent.location.href('DrawerReg.aspx" + paramsAlt + "');");
                e.Row.Style["cursor"] = "hand";
            }
        }
    }
}
