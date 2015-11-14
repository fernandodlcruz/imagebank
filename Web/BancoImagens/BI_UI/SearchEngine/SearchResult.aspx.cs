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
using FWBancoImagens.BO;

namespace BancoImagens.SearchEngine
{
    public partial class SearchResult : System.Web.UI.Page
    {
        private string _keys;
        private string _licences;
        private string _type;
        private string _themesId;
        private string _subThemesId;
        private string _orientation;
        private string _format;
        private string _codes;
        private DataSet _ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 600;

            BSearch objBO = BSearch.getInstance();

            _type = Request.QueryString["type"];
            _keys = Request.QueryString["key"];
            _licences = Request.QueryString["Licence"];

            if (!Page.IsPostBack)
            {
                if (_type == "S")
                {
                    _ds = objBO.SimpleSearch(_keys, _licences);
                }
                else if (_type == "A")
                {
                    _orientation = Request.QueryString["Orientation"];
                    _themesId = Request.QueryString["Themes"];
                    _subThemesId = Request.QueryString["SubThemes"];
                    _format = Request.QueryString["Format"];

                    _ds = objBO.AdvancedSearch(_keys, _licences, _orientation, _themesId, _subThemesId, _format);
                }
                else if (_type == "C")
                {
                    _codes = Request.QueryString["Codes"];

                    _ds = objBO.CodeSearch(_codes);
                }

                ViewState["_dsResult"] = _ds;
            }

            if ((ViewState["_dsResult"] as DataSet).Tables[0].Rows.Count > 0)
            {
                ItemsDataList((DataSet)ViewState["_dsResult"]);
                lblNoImages.Visible = false;
            }
            else
            {
                lblNoImages.Visible = true;
            }
        }

        protected void ItemsDataList(DataSet ds)
        {
            PagedDataSource objPds = new PagedDataSource();
            DataView objDtv = new DataView(ds.Tables[0]);

            objPds.DataSource = objDtv;
            objPds.AllowPaging = true;
            objPds.PageSize = Convert.ToInt32(ddlQtdImages.SelectedValue);

            if (objPds.PageCount < this.CurrentPage)
            {
                this.CurrentPage = objPds.PageCount;
            }

            objPds.CurrentPageIndex = this.CurrentPage;

            lblQtdImagensTop.Text = ds.Tables[0].Rows.Count + " imagens selecionadas";
            lblQtdImagensBot.Text = ds.Tables[0].Rows.Count + " imagens selecionadas";
            lblPaginacaoTop.Text = "Página " + (this.CurrentPage + 1).ToString() + " de " + objPds.PageCount.ToString();
            lblPaginacaoBot.Text = "Página " + (this.CurrentPage + 1).ToString() + " de " + objPds.PageCount.ToString();

            // Habilitar ou não os Botões Avançar e Voltar
            if (objPds.IsFirstPage)
            {
                imbPrevTop.Enabled = false;
                imbPrevBot.Enabled = false;
            } 
            else
            {
                imbPrevTop.Enabled = true;
                imbPrevBot.Enabled = true;
            }

            if (objPds.IsLastPage)
            {
                imbNextTop.Enabled = false;
                imbNextBot.Enabled = false;
            } 
            else
            {
                imbNextTop.Enabled = true;
                imbNextBot.Enabled = true;
            }

            dtlImages.DataSource = objPds;
            dtlImages.DataBind();
        }

	    public int CurrentPage
	    {
		    get
            {
                Object o = ViewState["_CurrentPage"];

                if (o == null)
                {
                    return 0;
                }

                return Convert.ToInt32(o);
            }
		    set { ViewState["_CurrentPage"] = value;}
	    }

        protected void imbPrevTop_Click(object sender, ImageClickEventArgs e)
        {
            this.CurrentPage -= 1;
            ItemsDataList((DataSet)ViewState["_dsResult"]);
        }

        protected void imbNextTop_Click(object sender, ImageClickEventArgs e)
        {
            this.CurrentPage += 1;
            ItemsDataList((DataSet)ViewState["_dsResult"]);
        }

        protected void imbPrevBot_Click(object sender, ImageClickEventArgs e)
        {
            this.CurrentPage -= 1;
            ItemsDataList((DataSet)ViewState["_dsResult"]);
        }

        protected void imbNextBot_Click(object sender, ImageClickEventArgs e)
        {
            this.CurrentPage += 1;
            ItemsDataList((DataSet)ViewState["_dsResult"]);
        }
    }
}
