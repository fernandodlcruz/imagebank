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

namespace BancoImagens.ControlPanel.Availables
{
    public partial class AvailablesReg : System.Web.UI.Page
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
                TDisponibilidades dto = new TDisponibilidades();
                
                dto.Descricao = txtDescricao.Text;

                BDisponibilidades objBO = BDisponibilidades.getInstance();

                if (_id == 0)
                {
                    objBO.Incluir(dto);
                    lblMsg.Text = "Disponibilidade inserida com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dto.Id = Convert.ToInt32(txtCodigo.Text);
                    objBO.Alterar(dto);
                    lblMsg.Text = "Disponibilidade alterada com sucesso.";
                }                
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir Disponibilidade: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AvailablesList.aspx");
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

                    TDisponibilidades dto;

                    BDisponibilidades objBO = BDisponibilidades.getInstance();

                    dto = objBO.Pesquisar(id);

                    txtCodigo.Text = dto.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtDescricao.Text = dto.Descricao.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar Disponibilidade: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BDisponibilidades objBO = BDisponibilidades.getInstance();

                objBO.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Disponibilidade excluída com sucesso.";

                LimparCampos();
                Response.Redirect("AvailablesList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir Disponibilidade: " + ex.Message;
            }
        }
    }
}
