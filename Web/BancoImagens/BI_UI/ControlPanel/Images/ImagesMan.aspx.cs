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
using System.IO;

namespace BancoImagens.ControlPanel.Images
{
    public partial class ImagesMan : System.Web.UI.Page
    {
        private int _id = 0;
        private string _image = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Convert.ToInt32(Request.QueryString["Id"]);

            if (!Page.IsPostBack)
            {
                LoadDropDown();
                LoadForm(_id);
            }
        }

        protected void LoadForm(int id)
        {
            BImagens objBO;
            TImagens dto;
            ListItem itm;
            string _caminhoBnk;

            try
            {
                objBO = BImagens.getInstance();

                dto = objBO.Pesquisar(_id);

                _image = "BI_" + dto.Codigo + ".jpg";
                _caminhoBnk = Server.MapPath(Util.GetParameterValue("PATH_IMG_BNK"));

                ViewState["_image2Delete"] = _image;
                imgBank.ImageUrl = "~/SearchEngine/ThumbImage.aspx?img=BI_" + dto.Codigo + ".jpg";

                if (File.Exists(_caminhoBnk + _image))
                {
                    btnLoadImage.Enabled = false;
                }
                else
                {
                    btnLoadImage.Enabled = true;
                }

                txtCodigo.Text = dto.Codigo;
                txtTitulo.Text = dto.Titulo;
                txtDetalhes.Text = dto.Detalhes;
                
                ddlTemas.SelectedValue = dto.Subtema.Categoria.Id.ToString();
                LoadSubThemes(dto.Subtema.Categoria.Id.ToString());
                ddlSubTemas.SelectedValue = dto.Subtema.Id.ToString();
                
                ddlGavetas.SelectedValue = dto.Pasta.Gaveta.Id.ToString();
                LoadFolders(dto.Pasta.Gaveta.Id);
                ddlPastas.SelectedValue = dto.Pasta.Id.ToString();

                ddlTpLicencas.SelectedValue = dto.TipoLicenca.Id.ToString();
                ddlOrientacoes.SelectedValue = dto.Orientacao;
                ddlAUI.SelectedValue = dto.AUI;
                ddlFormatos.SelectedValue = dto.Disponibilidade.Id.ToString();
                ddlFornecedores.SelectedValue = dto.Fornecedor.Id.ToString();

                // Carregar as palavras-chave
                for (int i = 0; i < dto.PalavrasChave.Count; i++)
                {
                    itm = new ListItem((dto.PalavrasChave[i] as TPalavrasChave).Palavrachave, (dto.PalavrasChave[i] as TPalavrasChave).Id.ToString());

                    ltbKeys.Items.Add(itm);
                }

                btnDelete.OnClientClick = "return confirm('Deseja realmente excluir?\\n\\nATENÇÃO!\\n\\nAo confirmar a exclusão desta imagem, a mesma será excluída do banco de imagens, assim como suas informações relacionadas.');";
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar dados da imagem: " + ex.Message;
            }
        }

        protected void LoadDropDown()
        {
            BCategorias tema = BCategorias.getInstance();

            ddlTemas.DataValueField = "Id";
            ddlTemas.DataTextField = "Nome";

            ddlTemas.DataSource = tema.Listar();
            ddlTemas.DataBind();

            //ddlTemas.Items.Insert(0, "-- Selecione o tema --");

            BGavetas gaveta = BGavetas.getInstance();

            ddlGavetas.DataValueField = "Id";
            ddlGavetas.DataTextField = "Descricao";

            ddlGavetas.DataSource = gaveta.Listar();
            ddlGavetas.DataBind();

            //ddlGavetas.Items.Insert(0, "-- Selecione a gaveta --");

            BTiposLicenca licenca = BTiposLicenca.getInstance();

            ddlTpLicencas.DataValueField = "Id";
            ddlTpLicencas.DataTextField = "Descricao";

            ddlTpLicencas.DataSource = licenca.Listar();
            ddlTpLicencas.DataBind();

            //ddlTpLicencas.Items.Insert(0, "-- Selecione o tipo de licença --");

            BDisponibilidades formato = BDisponibilidades.getInstance();

            ddlFormatos.DataValueField = "Id";
            ddlFormatos.DataTextField = "Descricao";

            ddlFormatos.DataSource = formato.Listar();
            ddlFormatos.DataBind();

            //ddlFormatos.Items.Insert(0, "-- Selecione o formato --");

            BFornecedores fornecedor = BFornecedores.getInstance();

            ddlFornecedores.DataValueField = "Id";
            ddlFornecedores.DataTextField = "Nome";

            ddlFornecedores.DataSource = fornecedor.Listar();
            ddlFornecedores.DataBind();

            //ddlFornecedores.Items.Insert(0, "-- Selecione o fornecedor --");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaForm())
                {
                    BImagens objBO = BImagens.getInstance();
                    TImagens dto = new TImagens();

                    dto.Id = _id;
                    dto.Codigo = txtCodigo.Text;
                    dto.Titulo = txtTitulo.Text;
                    dto.Detalhes = txtDetalhes.Text;
                    dto.AUI = ddlAUI.SelectedValue;
                    dto.Orientacao = ddlOrientacoes.SelectedValue;
                    
                    //DTOS
                    TDisponibilidades dtoDisp = new TDisponibilidades();
                    dtoDisp.Id = Convert.ToInt32(ddlFormatos.SelectedValue);
                    dto.Disponibilidade = dtoDisp;

                    TFornecedores dtoForn = new TFornecedores();
                    dtoForn.Id = Convert.ToInt32(ddlFornecedores.SelectedValue);
                    dto.Fornecedor = dtoForn;

                    IList<TPalavrasChave> lst = new List<TPalavrasChave>();;

                    for (int i = 0; i < ltbKeys.Items.Count; i++)
                    {
                        TPalavrasChave dtoPc = new TPalavrasChave();
                        dtoPc.Palavrachave = ltbKeys.Items[i].Text.Trim();
                        lst.Add(dtoPc);
                    }

                    dto.PalavrasChave = lst;

                    TPastas dtoPasta = new TPastas();
                    dtoPasta.Id = Convert.ToInt32(ddlPastas.SelectedValue);
                    dto.Pasta = dtoPasta;

                    TSubCategorias dtoSubTema = new TSubCategorias();
                    dtoSubTema.Id = Convert.ToInt32(ddlSubTemas.SelectedValue);
                    dto.Subtema = dtoSubTema;

                    TTiposLicenca dtoTpLicenca = new TTiposLicenca();
                    dtoTpLicenca.Id = Convert.ToInt32(ddlTpLicencas.SelectedValue);
                    dto.TipoLicenca = dtoTpLicenca;

                    objBO.Alterar(dto);

                    objBO = null;

                    Response.Redirect("ImagesList.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao salvar dados da imagem: " + ex.Message;
            }
        }

        protected bool ValidaForm()
        {
            string msgRetorno = "";
            bool ret = true;

            try
            {
                if (ddlSubTemas.SelectedValue == "-- Selecione o sub-tema --")
                {
                    msgRetorno  += "Selecione um sub-tema.<br/>";
                    ret = false;
                }

                if (ddlPastas.SelectedValue == "-- Selecione a pasta --")
                {
                    msgRetorno += "Selecione uma pasta.<br/>";
                    ret = false;
                }

                if (txtTitulo.Text == "")
                {
                    msgRetorno += "Preencha o título da imagem.<br/>";
                    ret = false;
                }

                if (ltbKeys.Items.Count <= 0)
                {
                    msgRetorno += "Insira pelo menos uma palavra-chave.";
                    ret = false;
                }

                if (!ret)
                {
                    lblMsg.Text = msgRetorno;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema na validação dos dados: " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string _arquivo;

            try
            {
                BImagens objBO = BImagens.getInstance();

                objBO.Excluir(_id);

                _arquivo = Server.MapPath(Util.GetParameterValue("PATH_IMG_BNK") + (ViewState["_image2Delete"] as string).ToString());

                if (File.Exists(_arquivo))
                {
                    File.Delete(_arquivo);
                    ViewState["_image2Delete"] = "";
                }

                lblMsg.Text = "Imagem excluída com sucesso.";

                Response.Redirect("ImagesList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir CD: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImagesList.aspx");
        }

        protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubThemes(ddlTemas.SelectedValue);
            ddlSubTemas.Items.Insert(0, "-- Selecione o sub-tema --");
        }

        protected void LoadSubThemes(string themeId)
        {
            IList<TSubCategorias> lstSThemes;
            ListItem itm;
            BSubCategorias objBOSCat = BSubCategorias.getInstance();

            try
            {
                lstSThemes = objBOSCat.Listar("'" + themeId + "'");
                ddlSubTemas.Items.Clear();

                if (lstSThemes.Count > 0)
                {
                    for (int i = 0; i < lstSThemes.Count; i++)
                    {
                        itm = new ListItem((lstSThemes[i] as TSubCategorias).Nome, (lstSThemes[i] as TSubCategorias).Id.ToString());

                        ddlSubTemas.Items.Insert(i, itm);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar sub-temas: " + ex.Message;
            }
        }

        protected void ddlGavetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFolders(Convert.ToInt32(ddlGavetas.SelectedValue));
            ddlPastas.Items.Insert(0, "-- Selecione a pasta --");
        }

        protected void LoadFolders(int gavId)
        {
            IList<TPastas> lst;
            ListItem itm;
            BPastas objBO = BPastas.getInstance();

            try
            {
                lst = objBO.Listar(gavId);
                ddlPastas.Items.Clear();

                if (lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        itm = new ListItem((lst[i] as TPastas).Descricao, (lst[i] as TPastas).Id.ToString());

                        ddlPastas.Items.Insert(i, itm);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar pastas: " + ex.Message;
            }
        }

        protected void igbInsertKey_Click(object sender, ImageClickEventArgs e)
        {
            ListItem itm = new ListItem(txtKey.Text, "0");
            ltbKeys.Items.Add(itm);
        }

        protected void igbDeleteKey_Click(object sender, ImageClickEventArgs e)
        {
            int totalItens = ltbKeys.Items.Count-1;

            for (int i = totalItens; i >= 0; i--)
            {
                if (ltbKeys.Items[i].Selected)
                {
                    ltbKeys.Items.Remove(ltbKeys.Items[i]);
                }
            }
        }

        protected void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                BImagens objBOImg = BImagens.getInstance();
                string _imagem = (ViewState["_image2Delete"] as string).ToString().Substring(3, (ViewState["_image2Delete"] as string).ToString().Length-3);
                string _caminhoBnk = Server.MapPath(Util.GetParameterValue("PATH_IMG_BNK"));

                objBOImg.CriaMarcaDagua(_imagem, _caminhoBnk, _imagem);

                objBOImg = null;

                btnLoadImage.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar imagem: " + ex.Message;
            }            
        }
    }
}
