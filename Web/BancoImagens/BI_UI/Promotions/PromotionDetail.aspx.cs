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

namespace BancoImagens.Promotions
{
    public partial class PromotionDetail : System.Web.UI.Page
    {
        private string _cdId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _cdId = Request.QueryString["cdId"];
            lblNameCD.Text = Request.QueryString["name"];

            if (!Page.IsPostBack)
            {
                LoadCdImages(_cdId);
            }
        }

        protected void LoadCdImages(string cdId)
        {
            DataSet _ds = new DataSet();
            BCds objBO = BCds.getInstance();

            try
            {
                _ds = objBO.Listar(Convert.ToInt32(cdId));

                dtlImages.DataSource = _ds.Tables[0].DefaultView;
                dtlImages.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar imagens do CD: " + ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PromotionList.aspx");
        }
    }
}
