using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore.User.Cus
{
    public partial class AddMoney : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                TryAdding();                
            }
        }

        void TryAdding()//Checks user availability and add requested amount to ther account....
        {
            if (UserExists() && Session["Customer"].ToString() == UName.Text ) //Checks if user exist add money and is the same user....
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddUserMoney",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UName", UName.Text);
                    cmd.Parameters.AddWithValue("UType",'C');
                    cmd.Parameters.AddWithValue("Amount",Convert.ToDouble(Amount.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string script = "alert('Transaction succesfull. Your current balance: " + FindMoney() + "');window.location ='Download.aspx';";
                    ScriptManager.RegisterStartupScript( this, this.GetType(), "alert", script, true);
                }
                catch(Exception ex)
                {
                    Status.Text += "Some Error occured(TA): " + ex.Message;
                }
            }
            else //If wrong credentials ....
            {
                Status.Text += "Username and Password does not match";
            }
        }

        bool UserExists() //Check if user exists or not....
        {
            int val = 0;
            try
            {
                con.Open();
                string command = "select * from fn_UserExist('" + UName.Text + "','" + Pass.Text + "','C');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text += "Some error occured. Reason: " + ex.Message;
            }
            return (val == 1) ? true : false;
        }

        string FindMoney()
        {
            string balance = "";
            try
            {
                con.Open();
                string command = "select * from fn_FillMoney('" + Session["Customer"].ToString() + "','C');";
                SqlCommand cmd = new SqlCommand(command, con);
                balance = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text += "Some Error occured(FM). Reason:" + ex.Message;
            }
            return balance;
        }
    }
}