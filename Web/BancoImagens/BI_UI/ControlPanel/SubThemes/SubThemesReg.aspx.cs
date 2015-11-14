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

namespace BancoImagens.ControlPanel.SubThemes
{
    public partial class SubThemesReg : System.Web.UI.Page
    {
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlCategorias.Focus();

            _id = Convert.ToInt32(Request.QueryString["Id"]);

            if (!Page.IsPostBack)
            {
                LoadDropDown();
                LoadForm(_id);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TSubCategorias dtoSubCat = new TSubCategorias();
                TCategorias dtoCat = new TCategorias();

                dtoSubCat.Nome = txtNome.Text;
                dtoCat.Id = ddlCategorias.SelectedItem.Value;
                dtoSubCat.Categoria = dtoCat;

                BSubCategorias subCat = BSubCategorias.getInstance();

                if (_id == 0)
                {
                    subCat.Incluir(dtoSubCat);
                    lblMsg.Text = "Sub-Tema inserido com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dtoSubCat.Id = Convert.ToInt32(txtCodigo.Text);
                    subCat.Alterar(dtoSubCat);
                    lblMsg.Text = "Sub-Tema alterado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir sub-tema: " + ex.Message;
            }
        }

        protected void LoadDropDown()
        {
            BCategorias categoria = BCategorias.getInstance();

            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataTextField = "Nome";

            ddlCategorias.DataSource = categoria.Listar();
            ddlCategorias.DataBind();

            ddlCategorias.Items.Insert(0, "-- Selecione o tema --");
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtDtCriacao.Text = "";
            LoadDropDown();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubThemesList.aspx");
        }

        protected void LoadForm(int id)
        {
            try
            {
                if (id == 0)
                {
                    btnDelete.Enabled = false;
                    ddlCategorias.Enabled = true;
                }
                else
                {
                    btnDelete.OnClientClick = "return confirm('Deseja realmente excluir?');";
                    btnDelete.Enabled = true;
                    ddlCategorias.Enabled = false;

                    TSubCategorias dtoSubCat;

                    BSubCategorias subCat = BSubCategorias.getInstance();

                    dtoSubCat = subCat.Pesquisar(id);

                    txtCodigo.Text = dtoSubCat.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtNome.Text = dtoSubCat.Nome.ToString();
                    txtDtCriacao.Text = dtoSubCat.DataCriacao.ToString();
                    ddlCategorias.SelectedValue = dtoSubCat.Categoria.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar sub-tema: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BSubCategorias subCat = BSubCategorias.getInstance();

                subCat.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Sub-Tema excluído com sucesso.";

                LimparCampos();
                Response.Redirect("SubThemesList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir sub-tema: " + ex.Message;
            }
        }
    }
}
