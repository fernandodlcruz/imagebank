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

namespace BancoImagens.ControlPanel.Drawer
{
    public partial class DrawerReg : System.Web.UI.Page
    {
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescricao.Focus();

            _id = Convert.ToInt32(Request.QueryString["Id"]);

            if (!Page.IsPostBack)
            {
                LoadForm(_id);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TGavetas dtoGav = new TGavetas();
                
                dtoGav.Descricao = txtDescricao.Text;

                BGavetas gaveta = BGavetas.getInstance();

                if (_id == 0)
                {
                    gaveta.Incluir(dtoGav);
                    lblMsg.Text = "Gaveta inserida com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dtoGav.Id = Convert.ToInt32(txtCodigo.Text);
                    gaveta.Alterar(dtoGav);
                    lblMsg.Text = "Gaveta alterada com sucesso.";
                }                
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir gaveta: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DrawerList.aspx");
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtDescricao.Text = "";
        }

        protected void LoadForm(int id)
        {
            try
            {
                if (id == 0)
                {
                    btnDelete.Enabled = false;                    
                }
                else
                {
                    btnDelete.OnClientClick = "return confirm('Deseja realmente excluir?');";
                    btnDelete.Enabled = true;

                    TGavetas dtoGav;

                    BGavetas gaveta = BGavetas.getInstance();

                    dtoGav = gaveta.Pesquisar(id);

                    txtCodigo.Text = dtoGav.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtDescricao.Text = dtoGav.Descricao.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar gaveta: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BGavetas gaveta = BGavetas.getInstance();

                gaveta.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Gaveta excluída com sucesso.";

                LimparCampos();
                Response.Redirect("DrawerList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir gaveta: " + ex.Message;
            }
        }
    }
}
