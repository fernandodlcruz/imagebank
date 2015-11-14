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

namespace BancoImagens.ControlPanel.ClientUsers
{
    public partial class ClientUserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipUserCollection mbsUserColl = Membership.GetAllUsers(); //0, 2, out totUsers);

            gdvUsers.DataSource = mbsUserColl;
            gdvUsers.DataBind();
        }

        protected void gdvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUsers.PageIndex = e.NewPageIndex;
        }

        protected void gdvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string paramsAlt;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                paramsAlt = "?Id=" + e.Row.Cells[1].Text;

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                e.Row.Attributes.Add("onClick", "javascript:parent.location.href('ClientUserReg.aspx" + paramsAlt + "');");
                e.Row.Style["cursor"] = "hand";

                //if (Convert.ToBoolean(e.Row.Cells[0].Text))
                //{
                //    e.Row.Attributes.Add("style", "this.style.foregroundColor='#0000FF'");
                //}
            }
        }
    }
}
