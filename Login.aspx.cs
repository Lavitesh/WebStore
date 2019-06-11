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
    public partial class Login : System.Web.UI.Page
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
                con.Open();
                char ch = (dev.Checked) ? 'D' : 'C'; //if checked then ch = C else D....
                if (ch == 'D') //Redirect to DPanel page....
                {
                    Session["Developer"] = UName.Text;
                    Response.Redirect("~/User/Dev/DPanel.aspx");
                }
                else if (ch == 'C') //Redirect to Cpanel Page....
                {
                    Session["Customer"] = UName.Text;
                    Response.Redirect("~/User/Cus/CPanel.aspx");

                }
                else
                    Session.RemoveAll();                
            }
            else //If there is no user....
            {
                Status.Text = "Username and Password does not match";
            }
        }

        bool UserExists() //Check if user exists or not....
        {
            con.Open();
            char ch = (dev.Checked) ? 'D' : 'C';
            string command="select * from fn_UserExist('" + UName.Text + "','" + Pass.Text + "','" + ch + "');";
            SqlCommand cmd = new SqlCommand(command, con);
            int val = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return (val==1) ? true : false;
        }
    }
}