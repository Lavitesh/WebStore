using System;
using System.IO;
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
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void Submit_Click(object sender, EventArgs e) //If all fields are filled then sends the control to AddnewUser method to add new user....
        {
            if (Page.IsValid)
                AddNewUser();
            else
                Status.Text = "Please fill up all fields.";
        }

        void AddNewUser()//Try to add a user....
        {
            if (!CheckIfUserExist())//Check if there is already a user or not....
            {
                try
                {                    
                    con.Open();
                    char ch = Dev.Checked ? 'D' : 'C';
                    SqlCommand cmd = new SqlCommand("sp_AddNewUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", Name.Text);
                    cmd.Parameters.AddWithValue("UName", UName.Text);
                    cmd.Parameters.AddWithValue("Pass", Pass.Text);
                    cmd.Parameters.AddWithValue("Type", ch);
                    cmd.Parameters.AddWithValue("Ques", Ques.Text);
                    cmd.Parameters.AddWithValue("Ans", Ans.Text);
                    cmd.ExecuteNonQuery();
                    Status.Text = "User Created. <a href='Login.aspx'>Login</a> now";
                    con.Close();
                }
                catch (Exception ex)
                {
                    Status.Text += "Failed </br> Reason: " + ex.Message;
                } 
            }
            else
            {
                Status.Text += "There is already a user with Username "+ UName.Text +" </br> Try with another Username or try <a href='Login.aspx' style='font-size:100%;'>Login</a>";
            }
        }

        bool CheckIfUserExist() //return false if there is already a User in database and false if there is a not....
        {
            int val = 1;
            try
            {                
                con.Open();
                char ch = Dev.Checked ? 'D' : 'C';
                string command = "select * from fn_CheckUser('" + UName.Text + "','" + ch + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text += "Exception Occured </br> Reason: " + ex.Message + "</br>";
            }
            if (val == 0)
                return false;
            else
                return true;
        }
    }
}