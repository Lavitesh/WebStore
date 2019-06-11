using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore.User.Dev
{
    public partial class Developer : System.Web.UI.MasterPage
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Developer"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                UserName.Text = Session["Developer"].ToString();
                FindMoney();
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Home.aspx");
        }

        void FindMoney()
        {
            try
            {
                con.Open();
                string command = "select * from fn_FillMoney('" + Session["Developer"].ToString() + "','D');";
                SqlCommand cmd = new SqlCommand(command, con);
                totalmoney.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Some Error occured(FM). Reason:" + ex.Message;
            }
        }

        protected void totalmoney_Click(object sender, EventArgs e)
        {
            Response.Redirect("WithdrawMoney.aspx");
        }
    }
}