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

namespace BancoImagens.ControlPanel.Folder
{
    public partial class FolderReg : System.Web.UI.Page
    {
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescricao.Focus();

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
                TPastas dtoPas = new TPastas();
                TGavetas dtoGav = new TGavetas();
                
                dtoPas.Descricao = txtDescricao.Text;
                dtoGav.Id = Convert.ToInt32(ddlGavetas.SelectedItem.Value);
                dtoPas.Gaveta = dtoGav;

                BPastas pasta = BPastas.getInstance();

                if (_id == 0)
                {
                    pasta.Incluir(dtoPas);
                    lblMsg.Text = "Pasta inserida com sucesso.";
                    LimparCampos();
                }
                else
                {
                    dtoPas.Id = Convert.ToInt32(txtCodigo.Text);
                    pasta.Alterar(dtoPas);
                    lblMsg.Text = "Pasta alterada com sucesso.";
                }                
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir pasta: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FolderList.aspx");
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtDescricao.Text = "";
            LoadDropDown();
        }

        protected void LoadDropDown()
        {
            BGavetas gaveta = BGavetas.getInstance();

            ddlGavetas.DataValueField = "Id";
            ddlGavetas.DataTextField = "Descricao";

            ddlGavetas.DataSource = gaveta.Listar();
            ddlGavetas.DataBind();

            ddlGavetas.Items.Insert(0, "-- Selecione a gaveta --");
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

                    TPastas dtoPas;

                    BPastas pasta = BPastas.getInstance();

                    dtoPas = pasta.Pesquisar(id);

                    txtCodigo.Text = dtoPas.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtDescricao.Text = dtoPas.Descricao.ToString();
                    ddlGavetas.SelectedValue = dtoPas.Gaveta.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar pasta: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BPastas pasta = BPastas.getInstance();

                pasta.Excluir(Convert.ToInt32(txtCodigo.Text));

                lblMsg.Text = "Pasta excluída com sucesso.";

                LimparCampos();
                Response.Redirect("FolderList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir pasta: " + ex.Message;
            }
        }
    }
}
