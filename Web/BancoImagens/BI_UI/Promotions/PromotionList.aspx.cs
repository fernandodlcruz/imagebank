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

namespace BancoImagens.Promotions
{
    public partial class PromotionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCds();
            }
        }

        protected void LoadCds()
        {
            BCds objBO = BCds.getInstance();
            //DataSet _ds = new DataSet();

            dtlCDs.DataSource = objBO.Listar();
            dtlCDs.DataBind();

            if (dtlCDs.Items.Count <= 0)
            {
                lblNoPromotions.Visible = true;
            }
            else
            {
                lblNoPromotions.Visible = false;
            }
        }
    }
}
