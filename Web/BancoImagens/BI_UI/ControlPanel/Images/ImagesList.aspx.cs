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

namespace BancoImagens.ControlPanel.Images
{
    public partial class ImagesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadThemes();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string paramsAlt;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                paramsAlt = "?Id=" + e.Row.Cells[0].Text;

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                e.Row.Attributes.Add("onClick", "javascript:parent.location.href('ImagesMan.aspx" + paramsAlt + "');");
                e.Row.Style["cursor"] = "hand";
            }
        }

        protected void LoadThemes()
        {
            IList<TCategorias> lstThemes;
            ListItem itmChk;
            BCategorias objBOCat = BCategorias.getInstance();

            lstThemes = objBOCat.Listar();
            lsbTemas.Items.Clear();
            lsbSubTemas.Items.Clear();

            if (lstThemes.Count > 0)
            {
                for (int i = 0; i < lstThemes.Count; i++)
                {
                    itmChk = new ListItem((lstThemes[i] as TCategorias).Nome, (lstThemes[i] as TCategorias).Id.ToString());
                    //itmChk.Selected = true;

                    lsbTemas.Items.Add(itmChk);
                }
            }
        }

        protected void lsbTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<TSubCategorias> lstSThemes;
            ListItem itmChk;
            BSubCategorias objBOSCat = BSubCategorias.getInstance();
            string _themesId = "";

            try
            {
                for (int i = 0; i < lsbTemas.Items.Count; i++)
                {
                    if (lsbTemas.Items[i].Selected)
                    {
                        _themesId += "'" + lsbTemas.Items[i].Value + "',";
                    }
                }

                if (_themesId != "")
                {
                    _themesId = _themesId.Substring(0, _themesId.Length - 1);
                }

                lstSThemes = objBOSCat.Listar(_themesId);
                lsbSubTemas.Items.Clear();

                if (lstSThemes.Count > 0)
                {
                    for (int i = 0; i < lstSThemes.Count; i++)
                    {
                        itmChk = new ListItem((lstSThemes[i] as TSubCategorias).Nome, (lstSThemes[i] as TSubCategorias).Id.ToString());

                        lsbSubTemas.Items.Add(itmChk);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar sub-temas: " + ex.Message;
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string _themesId = "";
            string _subThemesId = "";

            try
            {
                for (int i = 0; i < lsbTemas.Items.Count; i++)
                {
                    if (lsbTemas.Items[i].Selected)
                    {
                        _themesId += "'" + lsbTemas.Items[i].Value + "',";
                    }
                }

                if (_themesId != "")
                {
                    _themesId = _themesId.Substring(0, _themesId.Length - 1);
                }

                for (int i = 0; i < lsbSubTemas.Items.Count; i++)
                {
                    if (lsbSubTemas.Items[i].Selected)
                    {
                        _subThemesId += lsbSubTemas.Items[i].Value + ",";
                    }
                }

                if (_subThemesId != "")
                {
                    _subThemesId = _subThemesId.Substring(0, _subThemesId.Length - 1);
                }
                
                BSearch objBO = BSearch.getInstance();

                gdvImages.DataSource = objBO.SearchForEdit(txtCodes.Text, _themesId, _subThemesId);
                gdvImages.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problemas ao consultar: " + ex.Message;
            }
        }

        protected void gdvImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvImages.PageIndex = e.NewPageIndex;
        }
    }
}
