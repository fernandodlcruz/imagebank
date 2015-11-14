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
using FWBancoImagens.Common;

namespace BancoImagens.SearchEngine
{
    public partial class Teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //' Inicializa as variáveis
            string strFilename;
            short shtWidth;
            short shtHeight;
            string strPath;
            System.Drawing.Image g;
            System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            if (Request.QueryString["loc"] == "cd")
            {                
                strPath = Util.GetParameterValue("PATH_CAPA_CD");
            }
            else
            {
                strPath = Util.GetParameterValue("PATH_IMG_BNK");
            }

            //' Obtem o nome do arquivo, que foi passador por parâmetro
            strFilename = strPath + Request.QueryString["img"];

            //' Gera uma nova imagem      a partir do arquivo
            g = System.Drawing.Image.FromFile(Server.MapPath(strFilename));

            //' Caso tenhamos passado uma Largura na QueryString, a largura será respeitada, caso contrário, foi definido que a largura do Thumbnail sera 100px
            if (g.Width > 800)
            {
                shtWidth = 170; //'Caso não for passado a largura na QueryString então a largura sera de 100px
                //' Fazemos um redimensionamento proporcional da imagem entre largura e altura
                shtHeight = (short)(g.Height / (g.Width / shtWidth));
            }
            else
            {
                shtHeight = 113;
                shtWidth = (short)(g.Width / (g.Height / shtHeight)); //'Caso não for passado a largura na QueryString então a largura sera de 100px
            }

            //' Altera o contentType      : Esta página devolve uma imagem
            Response.ContentType = "image/jpeg";

            //' Insere a imagem no objeto      response
            g.GetThumbnailImage(shtWidth, shtHeight, cb, IntPtr.Zero).Save(Response.OutputStream, g.RawFormat);

            //' destroi o objeto imagem      na memória do servidor
            g.Dispose();
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}
