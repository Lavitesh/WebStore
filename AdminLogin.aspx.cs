using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)//control passed to TryLogin method....
        {
            TryLogin();
        }

        void TryLogin()//Checks user availability and redirect to their panel page....
        {
            if (UserExists()) //Checks if user exist....
            {
                Session["Admin"] = UName.Text;
                Response.Redirect("~/Admin/AdminPanel.aspx");
            }
            else //If there is no user....
            {
                Status.Text = "Username and Password does not match";
            }
        }

        bool UserExists() //Check if user exists or not....
        {
            int val=0;
            try
            {
                con.Open();
                string command = "select * from fn_UserExist('" + UName.Text + "','" + Pass.Text + "','A');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured. Reason: " + ex.Message;
            }
            return (val == 1) ? true : false;
        }
    }    
}