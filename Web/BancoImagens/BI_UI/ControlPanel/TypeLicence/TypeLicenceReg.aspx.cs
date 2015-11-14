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

namespace BancoImagens.ControlPanel.TypeLicence
{
    public partial class TypeLicenceReg : System.Web.UI.Page
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
                TTiposLicenca dtoTpLic = new TTiposLicenca();

                dtoTpLic.Nome = txtNome.Text;
                dtoTpLic.Descricao = txtDescricao.Text;

                BTiposLicenca tpLic = BTiposLicenca.getInstance();

                if (_id == 0)
                {
                    tpLic.Incluir(dtoTpLic);
                    lblMsg.Text = "Tipo de licença inserido com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dtoTpLic.Id = Convert.ToInt32(txtCodigo.Text);
                    tpLic.Alterar(dtoTpLic);
                    lblMsg.Text = "Tipo de licença alterado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir tipo de licença: " + ex.Message;
            }
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

                    TTiposLicenca dtoTpLic;

                    BTiposLicenca tpLic = BTiposLicenca.getInstance();

                    dtoTpLic = tpLic.Pesquisar(id);

                    txtCodigo.Text = dtoTpLic.Id.ToString();
                    txtNome.Text = dtoTpLic.Nome.ToString();
                    txtDescricao.Text = dtoTpLic.Descricao.ToString();
                    txtDtCriacao.Text = dtoTpLic.DataCriacao.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar tipo de licença: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BTiposLicenca tpLic = BTiposLicenca.getInstance();

                tpLic.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Tipo de licença excluído com sucesso.";

                LimparCampos();
                Response.Redirect("TypeLicenceList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir tipo de licença: " + ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtDtCriacao.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TypeLicenceList.aspx");
        }
    }
}
