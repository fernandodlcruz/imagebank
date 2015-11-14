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

namespace BancoImagens.ControlPanel.Themes
{
    public partial class ThemesReg : System.Web.UI.Page
    {
        private string _id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtCodigo.Focus();

            _id = Request.QueryString["Id"];

            if (!Page.IsPostBack)
            {
                LoadForm(_id);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TCategorias dtoCat = new TCategorias();

                dtoCat.Id = txtCodigo.Text;
                dtoCat.Nome = txtNome.Text;

                BCategorias categoria = BCategorias.getInstance();

                if (_id == null || _id == "")
                {
                    categoria.Incluir(dtoCat);
                    lblMsg.Text = "Tema inserido com sucesso.";
                    LimparCampos();
                }
                else
                {                    
                    categoria.Alterar(dtoCat);
                    lblMsg.Text = "Tema alterado com sucesso.";
                }                
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir tema: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThemesList.aspx");
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtDtCriacao.Text = "";
        }

        protected void LoadForm(string id)
        {
            try
            {
                if (id == null || _id == "")
                {
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.OnClientClick = "return confirm('Deseja realmente excluir?');";
                    btnDelete.Enabled = true;

                    TCategorias dtoCat;

                    BCategorias categoria = BCategorias.getInstance();

                    dtoCat = categoria.Pesquisar(id);

                    txtCodigo.Text = dtoCat.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtNome.Text = dtoCat.Nome.ToString();
                    txtDtCriacao.Text = dtoCat.DataCriacao.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar tema: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BCategorias categoria = BCategorias.getInstance();

                categoria.Excluir(txtCodigo.Text);

                lblMsg.Text = "Tema excluído com sucesso.";

                LimparCampos();
                Response.Redirect("ThemesList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir tema: " + ex.Message;
            }
        }
    }
}
