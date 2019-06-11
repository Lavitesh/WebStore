using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SearchVal"] = null;
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["Category"] = lnk.Attributes["CustomParameter"].ToString();
            Response.Redirect("~/Search.aspx");
        }
    }
}