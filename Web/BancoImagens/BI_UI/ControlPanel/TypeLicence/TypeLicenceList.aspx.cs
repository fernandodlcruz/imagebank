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

namespace BancoImagens.ControlPanel.TypeLicence
{
    public partial class TypeLicenceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BTiposLicenca tpLic = BTiposLicenca.getInstance();

            gdvTpLicence.DataSource = tpLic.Listar();
            gdvTpLicence.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("TypeLicenceReg.aspx");
        }

        protected void gdvTpLicence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string paramsAlt;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                paramsAlt = "?Id=" + e.Row.Cells[0].Text;

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                e.Row.Attributes.Add("onClick", "javascript:parent.location.href('TypeLicenceReg.aspx" + paramsAlt + "');");
                e.Row.Style["cursor"] = "hand";
            }
        }
    }
}
