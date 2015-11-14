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

namespace BancoImagens.ControlPanel.Suppliers
{
    public partial class SuppliersReg : System.Web.UI.Page
    {
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtNome.Focus();

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
                TFornecedores dto = new TFornecedores();

                dto.Nome = txtNome.Text;

                BFornecedores objBO = BFornecedores.getInstance();

                if (_id == 0)
                {
                    objBO.Incluir(dto);
                    lblMsg.Text = "Fornecedor inserido com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dto.Id = Convert.ToInt32(txtCodigo.Text);
                    objBO.Alterar(dto);
                    lblMsg.Text = "Fornecedor alterado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir fornecedor: " + ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtDtCriacao.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SuppliersList.aspx");
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

                    TFornecedores dto;

                    BFornecedores objBO = BFornecedores.getInstance();

                    dto = objBO.Pesquisar(id);

                    txtCodigo.Text = dto.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtNome.Text = dto.Nome.ToString();
                    txtDtCriacao.Text = dto.DataCriacao.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar fornecedor: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BFornecedores objBO = BFornecedores.getInstance();

                objBO.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Fornecedor excluído com sucesso.";

                LimparCampos();
                Response.Redirect("SuppliersList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir fornecedor: " + ex.Message;
            }
        }
    }
}
