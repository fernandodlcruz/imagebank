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
using System.IO;
using FWBancoImagens.BO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;

namespace BancoImagens.ControlPanel.Cds
{
    public partial class CdsReg : System.Web.UI.Page
    {
        private int _id = 0;
        private string _nomeArq;

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
            TImagens dtoImg;
            IList<TImagens> lstImgs = new List<TImagens>();
            BImagens objBOImg;
            DataSet dsImgs;

            try
            {
                if (System.IO.Path.GetFileName(imgCapa.ImageUrl) != "")
                {
                    if (dtlImagesCD.Items.Count > 0)
                    {
                        TCds dto = new TCds();

                        dto.Nome = txtNome.Text;
                        dto.Valor = Convert.ToDecimal(txtValor.Text);
                        dto.Capa = System.IO.Path.GetFileName(imgCapa.ImageUrl);
                        dto.NumeroImagens = Convert.ToInt32(txtNroImagens.Text);
                        dto.Resolucao = txtResolucao.Text;

                        objBOImg = BImagens.getInstance();
                        dsImgs = (DataSet)ViewState["dsImgs"];

                        foreach (DataRow dr in dsImgs.Tables[0].Rows)
                        {
                            if (Convert.ToString(dr["NUMERO_IMG"]) == "")
                            {
                                break;
                            }

                            dtoImg = new TImagens();

                            dtoImg = objBOImg.Pesquisar(Convert.ToString(dr["NUMERO_IMG"]));

                            if (dtoImg == null)
                            {
                                lblMsg.Text = "Imagem de código " + Convert.ToString(dr["NUMERO_IMG"]) + " não está cadastrada.";
                                break;
                            }

                            lstImgs.Add(dtoImg);                            
                        }
                        dto.Imagens = lstImgs;

                        BCds objBO = BCds.getInstance();

                        if (_id == 0)
                        {
                            objBO.Incluir(dto);
                            lblMsg.Text = "CD inserido com sucesso.";
                            LimparCampos();
                        }
                        else
                        {
                            dto.Id = Convert.ToInt32(txtCodigo.Text);
                            objBO.Alterar(dto);
                            lblMsg.Text = "CD alterado com sucesso.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Associe as imagens do CD antes de salvar.";
                    }
                }
                else
                {
                    lblMsg.Text = "Selecione e envie a capa do CD antes de salvar.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao inserir CD: " + ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtValor.Text = "";
            txtNroImagens.Text = "";
            txtResolucao.Text = "";
            imgCapa.ImageUrl = "";
            imgCapa.Visible = false;
            dtlImagesCD.DataSource = null;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CdsList.aspx");
        }

        protected void LoadForm(int id)
        {
            DataSet dsTmp = new DataSet();
            DataColumn dtcNro = new DataColumn("NUMERO_IMG");
            DataColumn dtcNome = new DataColumn("NOME_IMG");
            DataRow dtr;
            DataRow dtrNro;

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

                    TCds dto;

                    BCds objBO = BCds.getInstance();

                    dto = objBO.Pesquisar(id);

                    txtCodigo.Text = dto.Id.ToString();
                    txtCodigo.ReadOnly = true;
                    txtNome.Text = dto.Nome.ToString();
                    txtValor.Text = dto.Valor.ToString();
                    txtNroImagens.Text = dto.NumeroImagens.ToString();
                    txtResolucao.Text = dto.Resolucao.ToString();
                    _nomeArq = dto.Capa.ToString();

                    imgCapa.ImageUrl = Util.GetParameterValue("PATH_CAPA_CD") + _nomeArq;
                    imgCapa.Visible = true;

                    dsTmp.Tables.Add();
                    dtcNro.DataType = System.Type.GetType("System.String");
                    dsTmp.Tables[0].Columns.Add(dtcNro);

                    dsTmp.Tables.Add();
                    dtcNome.DataType = System.Type.GetType("System.String");
                    dsTmp.Tables[1].Columns.Add(dtcNome);

                    foreach (TImagens dtoImg in dto.Imagens)
                    {
                        dtrNro = dsTmp.Tables[0].NewRow();
                        dtr = dsTmp.Tables[1].NewRow();

                        dtrNro["NUMERO_IMG"] = Convert.ToString(dtoImg.Codigo);
                        dtr["NOME_IMG"] = Convert.ToString(dtoImg.Codigo + ".jpg");

                        dsTmp.Tables[0].Rows.Add(dtrNro);
                        dsTmp.Tables[1].Rows.Add(dtr);
                    }

                    dtlImagesCD.DataSource = dsTmp.Tables[1].DefaultView;
                    dtlImagesCD.DataBind();

                    ViewState["dsImgs"] = dsTmp;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar CD: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string _arqAntigo;

            try
            {
                BCds objBO = BCds.getInstance();

                objBO.Excluir(Convert.ToInt32(txtCodigo.Text));

                if (System.IO.Path.GetFileName(imgCapa.ImageUrl) != "")
                {
                    _arqAntigo = Server.MapPath(Util.GetParameterValue("CAMINHO_FISICO_CAPAS") + System.IO.Path.GetFileName(imgCapa.ImageUrl));

                    if (File.Exists(_arqAntigo))
                    {
                        File.Delete(_arqAntigo);
                    }
                }

                lblMsg.Text = "CD excluído com sucesso.";

                LimparCampos();
                Response.Redirect("CdsList.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao excluir CD: " + ex.Message;
            }
        }

        protected void btnSendImg_Click(object sender, EventArgs e)
        {
            string _arquivo;
            string _arqAntigo;

            try
            {
                if (UpLoadArchives(fluCapa, Util.GetParameterValue("CAMINHO_FISICO_CAPAS")))
                {
                    _arquivo = fluCapa.PostedFile.FileName;
                    _nomeArq = System.IO.Path.GetFileName(_arquivo);

                    if (System.IO.Path.GetFileName(imgCapa.ImageUrl) != "")
                    {
                        _arqAntigo = Server.MapPath(Util.GetParameterValue("CAMINHO_FISICO_CAPAS") + System.IO.Path.GetFileName(imgCapa.ImageUrl));

                        if (File.Exists(_arqAntigo))
                        {
                            File.Delete(_arqAntigo);
                        }
                    }

                    imgCapa.ImageUrl = Util.GetParameterValue("PATH_CAPA_CD") + _nomeArq;
                    imgCapa.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Selecione um arquivo antes de enviar.";
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = "Problema ao atualizar capa: " + ex.Message;
            }
        }

        protected void btnLoadImages_Click(object sender, EventArgs e)
        {
            if (UpLoadArchives(fluImagensCD, Util.GetParameterValue("PATH_FISICO_EXCEL_CD")))
            {
                DataSet ds = Util.AbreExcel(Server.MapPath(Util.GetParameterValue("PATH_FISICO_EXCEL_CD")), Util.GetParameterValue("ARQUIVO_EXCEL_CDS"), "SELECT * FROM [ImagensCD$]");
                DataColumn dtc = new DataColumn("NOME_IMG");
                DataRow dtr;

                ds.Tables.Add();

                dtc.DataType = System.Type.GetType("System.String");

                ds.Tables[1].Columns.Add(dtc);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["NUMERO_IMG"]) == "")
                    {
                        break;
                    }

                    dtr = ds.Tables[1].NewRow();

                    dtr["NOME_IMG"] = Convert.ToString(dr["NUMERO_IMG"] + ".jpg");

                    ds.Tables[1].Rows.Add(dtr);
                }

                dtlImagesCD.DataSource = ds.Tables[1].DefaultView;
                dtlImagesCD.DataBind();

                ViewState["dsImgs"] = ds;
            }
        }

        protected bool UpLoadArchives(FileUpload objFlu, string path)
        {
            string _arquivo;
            string _nomeArqUp;
            bool _ret = false;

            try
            {
                if (objFlu.HasFile)
                {
                    _arquivo = objFlu.PostedFile.FileName;
                    _nomeArqUp = System.IO.Path.GetFileName(_arquivo);

                    objFlu.PostedFile.SaveAs(Server.MapPath(path) + _nomeArqUp);

                    _ret = true;
                }
                else
                {
                    lblMsg.Text = "Selecione um arquivo antes de enviar.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao enviar arquivo: " + ex.Message;
            }

            return _ret;
        }
    }
}
