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

namespace BancoImagens.SearchEngine
{
    public partial class AdvancedSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadLicencas();
                LoadFormatos();
                LoadThemes();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string paramsUrl;
            string idsLincence = "";
            string orientacao = "";
            string _themesId = "";
            string _subThemesId = "";
            string _formato = "";

            try
            {
                //if (txtPalavrasChave.Text.Trim() == "")
                //{
                //    lblMsg.Text = "Informe um texto de pesquisa.";
                //    return;
                //}

                for (int i = 0; i < chlLicencas.Items.Count; i++)
                {
                    if (chlLicencas.Items[i].Selected)
                    {
                        idsLincence += chlLicencas.Items[i].Value + ",";
                    }
                }

                if (idsLincence != "")
                {
                    idsLincence = idsLincence.Substring(0, idsLincence.Length - 1);
                }

                for (int i = 0; i < chlOrientacoes.Items.Count; i++)
                {
                    if (chlOrientacoes.Items[i].Selected)
                    {
                        orientacao += chlOrientacoes.Items[i].Value + ",";
                    }
                }

                if (orientacao != "")
                {
                    orientacao = orientacao.Substring(0, orientacao.Length - 1);
                }

                for (int i = 0; i < chlFormatos.Items.Count; i++)
                {
                    if (chlFormatos.Items[i].Selected)
                    {
                        _formato += chlFormatos.Items[i].Value + ",";
                    }
                }

                if (_formato != "")
                {
                    _formato = _formato.Substring(0, _formato.Length - 1);
                }

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

                paramsUrl = "?type=A&key=" + txtPalavrasChave.Text + "&Licence=" + idsLincence + "&Orientation=" + orientacao +
                    "&Themes=" + _themesId + "&SubThemes=" + _subThemesId + "&Format=" + _formato;

                Response.Redirect("SearchResult.aspx" + paramsUrl);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problemas ao consultar: " + ex.Message;
            }
        }

        protected void LoadLicencas()
        {
            IList<TTiposLicenca> lstTpLic;
            ListItem itmChk;
            BTiposLicenca objBOTpLic = BTiposLicenca.getInstance();

            lstTpLic = objBOTpLic.Listar();

            if (lstTpLic.Count > 0)
            {
                for (int i = 0; i < lstTpLic.Count; i++)
                {
                    itmChk = new ListItem((lstTpLic[i] as TTiposLicenca).Nome, (lstTpLic[i] as TTiposLicenca).Id.ToString());
                    itmChk.Selected = true;
                    chlLicencas.Items.Add(itmChk);
                }
            }
        }

        protected void LoadFormatos()
        {
            IList<TDisponibilidades> lst;
            ListItem itmChk;
            BDisponibilidades objBO = BDisponibilidades.getInstance();

            lst = objBO.Listar();

            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    itmChk = new ListItem((lst[i] as TDisponibilidades).Descricao, (lst[i] as TDisponibilidades).Id.ToString());
                    itmChk.Selected = true;
                    chlFormatos.Items.Add(itmChk);
                }
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

        protected void chkTpLicenca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTpLicenca.Checked)
            {
                for (int i = 0;i < chlLicencas.Items.Count; i++)
                {
                    chlLicencas.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chlLicencas.Items.Count; i++)
                {
                    chlLicencas.Items[i].Selected = false;
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

        protected void btnPesquisarCodigos_Click(object sender, EventArgs e)
        {
            string paramsUrl;

            try
            {
                if (txtCodigosImgs.Text.Trim() == "")
                {
                    lblMsg.Text = "Informe pelo menos um código para pesquisa.";
                    return;
                }

                paramsUrl = "?type=C&codes=" + txtCodigosImgs.Text;

                Response.Redirect("SearchResult.aspx" + paramsUrl);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao pesquisar por códigos: " + ex.Message;
            }
        }

        protected void chkFormatos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFormatos.Checked)
            {
                for (int i = 0; i < chlFormatos.Items.Count; i++)
                {
                    chlFormatos.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chlFormatos.Items.Count; i++)
                {
                    chlFormatos.Items[i].Selected = false;
                }
            }
        }

        protected void chkOrientacoes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOrientacoes.Checked)
            {
                for (int i = 0; i < chlOrientacoes.Items.Count; i++)
                {
                    chlOrientacoes.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chlOrientacoes.Items.Count; i++)
                {
                    chlOrientacoes.Items[i].Selected = false;
                }
            }
        }
    }
}
