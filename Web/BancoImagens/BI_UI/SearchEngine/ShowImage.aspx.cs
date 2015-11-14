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

namespace FWBancoImagens.SearchEngine
{
    public partial class ShowImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo("C:\\Projetos\\Fototeca\\Web\\BancoImagens\\BI_UI\\img\\imgbnk");
            FileSystemInfo[] fsa;
            Image img;
            HyperLink lnk;
            ContentPlaceHolder cph = new ContentPlaceHolder();
            Table tbl = new Table();
            TableRow row = new TableRow();
            TableCell cell;
            int cntCells = 0;

            cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            fsa = di.GetFileSystemInfos("*.jpg");

            foreach (FileSystemInfo fsi in fsa)
            {
                lnk = new HyperLink();
                lnk.NavigateUrl = fsi.FullName;
                lnk.Attributes.Add("rel", "lightbox[roadtrip]");

                img = new Image();
                img.ImageUrl = "ThumbImage.aspx?img=" + fsi.FullName;

                lnk.Controls.Add(img);

                cell = new TableCell();                

                cell.Controls.Add(lnk);
                row.Cells.Add(cell);
                cntCells += 1;

                if (cntCells == 3)
                {
                    tbl.Rows.Add(row);
                    cntCells = 0;

                    row = new TableRow();
                }
            }

            tbl.Rows.Add(row);
            cph.Controls.Add(tbl);
        }
    }
}
