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
using System.IO;
using FWBancoImagens.BO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;

namespace BancoImagens.ControlPanel.Images
{
    public partial class ImagesReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 600;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string _arquivo;
            string _nomeArq;

            try
            {
                if (fluExcel.HasFile)
                {
                    _arquivo = fluExcel.PostedFile.FileName;
                    _nomeArq = System.IO.Path.GetFileName(_arquivo);

                    fluExcel.PostedFile.SaveAs(Server.MapPath(Util.GetParameterValue("CAMINHO_FISICO_EXCEL")) + _nomeArq);

                    btnLoad.Enabled = true;

                    lblMsg.Text = "Arquivo enviado com sucesso. Inicie a carga das imagens.";
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
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string _caminhoExcel;
            string _arquivoExcel;
            string _caminhoBnk;

            try
            {
                _caminhoExcel = Server.MapPath(Util.GetParameterValue("CAMINHO_FISICO_EXCEL"));
                _arquivoExcel = Util.GetParameterValue("NOME_ARQUIVO_EXCEL");
                _caminhoBnk = Server.MapPath(Util.GetParameterValue("PATH_IMG_BNK"));

                if (File.Exists(_caminhoExcel + _arquivoExcel))
                {
                    lblMsg.Text = "Aguarde, carregando imagens...";

                    BImagens objBO = BImagens.getInstance();

                    objBO.LoadImages(_caminhoExcel, _caminhoBnk);
                    //objBO.DeleteImages(_caminhoExcel, _caminhoBnk);

                    File.Delete(_caminhoExcel + _arquivoExcel);

                    lblMsg.Text = "Imagens carregadas com sucesso.";
                }
                else
                {
                    lblMsg.Text = "Envie o arquivo com a lista de imagens antes de carregá-las.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema ao carregar as imagens: " + ex.Message;
            }
        }
    }
}
