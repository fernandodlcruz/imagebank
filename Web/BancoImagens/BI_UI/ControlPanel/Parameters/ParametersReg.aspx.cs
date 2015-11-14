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

namespace BancoImagens.ControlPanel.Parameters
{
    public partial class ParametersReg : System.Web.UI.Page
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
                TParametros dto = new TParametros();

                dto.Parametro = txtNome.Text;
                dto.Valor = txtValor.Text;

                BParametros objBO = BParametros.getInstance();

                if (_id == 0)
                {
                    objBO.Incluir(dto);
                    lblMsg.Text = "Parametro inserido com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dto.Id = Convert.ToInt32(txtCodigo.Text);
                    objBO.Alterar(dto);
                    lblMsg.Text = "Parametro alterado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir parametro: " + ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtValor.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParametersList.aspx");
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

                    TParametros dto;

                    BParametros objBO = BParametros.getInstance();

                    dto = objBO.Pesquisar(id);

                    txtCodigo.Text = dto.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtNome.Text = dto.Parametro.ToString();
                    txtValor.Text = dto.Valor.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar parametro: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BParametros objBO = BParametros.getInstance();

                objBO.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Parametro excluído com sucesso.";

                LimparCampos();
                Response.Redirect("ParametersList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir parametro: " + ex.Message;
            }
        }
    }
}
