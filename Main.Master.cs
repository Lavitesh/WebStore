using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Customer"] = null;
            Session["Developer"] = null;
            Session["Admin"] = null;
            Session["SearchVal"] = null;
        }

        protected void headerSearch_TextChanged(object sender, EventArgs e)
        {
            if (headerSearch.Text.ToLower() == "admin")
            {
                Response.Redirect("~/AdminLogin.aspx");
            }
            else
            {
                Session["SearchVal"] = headerSearch.Text;
                Response.Redirect("~/Search.aspx");
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["Category"] = lnk.Attributes["CustomParameter"].ToString();
            Response.Redirect("~/Search.aspx");
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            if (headerSearch.Text != "")
            {
                if (headerSearch.Text.ToLower() == "admin")
                {
                    Response.Redirect("~/AdminLogin.aspx");
                }
                else
                {
                    Session["SearchVal"] = headerSearch.Text;
                    Response.Redirect("~/Search.aspx");
                }
            }
        }
    }
}