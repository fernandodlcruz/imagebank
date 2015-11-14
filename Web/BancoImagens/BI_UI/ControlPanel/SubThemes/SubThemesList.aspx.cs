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
using System.Drawing;

namespace BancoImagens.ControlPanel.SubThemes
{
    public partial class SubThemesList : System.Web.UI.Page
    {
        //private GridViewHelper helper;

        protected void Page_Load(object sender, EventArgs e)
        {
            BSubCategorias subCat = BSubCategorias.getInstance();

            gdvSubThemes.DataSource = subCat.Listar();
            gdvSubThemes.DataBind();

            //helper = new GridViewHelper(this.gdvSubThemes, false);
            //helper.RegisterGroup("Nome", true, true);
            //helper.ApplyGroupSort();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubThemesReg.aspx");
        }

        protected void gdvSubThemes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string paramsAlt;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                paramsAlt = "?Id=" + e.Row.Cells[0].Text;

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                e.Row.Attributes.Add("onClick", "javascript:parent.location.href('SubThemesReg.aspx" + paramsAlt + "');");
                e.Row.Style["cursor"] = "hand";
            }
        }
    }
}
