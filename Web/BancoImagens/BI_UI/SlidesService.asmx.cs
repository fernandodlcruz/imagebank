using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;

namespace BancoImagens
{
    /// <summary>
    /// Summary description for SlidesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService()]
    [ToolboxItem(false)]
    public class SlidesService : System.Web.Services.WebService
    {
        [WebMethod]
        public AjaxControlToolkit.Slide[] GetSlides()
        {
            AjaxControlToolkit.Slide[] biSlides = new AjaxControlToolkit.Slide[0];
            int _totalImgs = Directory.GetFiles(Server.MapPath("img\\home\\")).Length;
            string[] files = Directory.GetFiles(Server.MapPath("img\\home\\"));
            int tamStr = 0;
            int ultPos = 0;

            Array.Resize(ref biSlides, _totalImgs);

            for (int cnt = 0; cnt < _totalImgs; cnt++)
            {
                tamStr = files[cnt].Length;
                ultPos = files[cnt].LastIndexOf('\\') + 1;
                biSlides[cnt] = new AjaxControlToolkit.Slide("img/home/" + files[cnt].Substring(ultPos, tamStr - ultPos), "", "");
            }

            return biSlides;
        }
    }
}
