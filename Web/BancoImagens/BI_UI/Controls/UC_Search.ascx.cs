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

namespace BancoImagens.Controls
{
    public partial class UC_Search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadLicencas();
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
                for (int i = 0;i < lstTpLic.Count;i++)
                {                    
                    itmChk = new ListItem((lstTpLic[i] as TTiposLicenca).Nome, (lstTpLic[i] as TTiposLicenca).Id.ToString());
                    itmChk.Selected = true;
                    chlLicencas.Items.Add(itmChk);
                }
            }
        }

        protected void btnPesquisaRap_Click(object sender, EventArgs e)
        {
            string paramsUrl;
            string idsLincence = "";

            if (txtPesquisaRapida.Text.Trim() == "")
            {
                lblMsg.Text = "Informe um texto de pesquisa.";
                return;
            }

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

            paramsUrl = txtPesquisaRapida.Text + "&Licence=" + idsLincence;

            Response.Redirect("~/SearchEngine/SearchResult.aspx?type=S&key=" + paramsUrl);
        }
    }
}