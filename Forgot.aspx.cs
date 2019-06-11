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
    public partial class Forgot: System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Proceed_Click(object sender, EventArgs e)//hide UName, Dev checkbox and Proceed button and then go to proceed to next function....
        {
            if (CheckIfUserExist())
            {
                Proceed.Visible = false;
                UName.Visible = false;
                Dev.Visible = false;
                EnterUName.Visible = false;
                UDev.Visible = false;
                ProceedToNext();
            }
            else
            {
                Proceed.Text = "Try Again";
                Ques.Visible = true;
                Ques.Text = "User not found<br/> New to WebStore <a href='Register.aspx' style='font-size:100%'>Register</a> now";                
            }
        }

        void ProceedToNext() //Add Security Question, Answer textbox and submit button....
        {
            CheckSecurityQuestion();
            Ans.Visible = true;
            RequiredFieldValidator rsvAns = new RequiredFieldValidator() {
                ControlToValidate = "Ans",
                ErrorMessage = "Enter Answer",
                Text = "Enter Answer"
            };
            Proceednext.Controls.Add(rsvAns);
            Submit.Visible = true;
        }        

        bool CheckIfUserExist() //return false if there is no User in database and true if there is a User....
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
                Status.Text = "Exception Occured </br> Reason: " + ex.Message;
            }
            if (val == 0) 
                return false;
            else
                return true;
        }

        void CheckSecurityQuestion() //show the Securiity Question....
        {
            try
            {
                con.Open();
                char ch = Dev.Checked ? 'D' : 'C';
                string command = "select * from fn_ReturnSecurityQuestion('" + UName.Text + "','" + ch + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                Ques.Visible = true;
                Ques.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Failed. Reason: " + ex.Message;
            }       
        }

        protected void Submit_Click(object sender, EventArgs e) //Checks ans and returns Password....
        {
            try
            {
                con.Open();
                char ch = Dev.Checked ? 'D' : 'C';
                string command = "select Pass from fn_CheckAns('" + UName.Text + "','" + ch + "','" + Ans.Text + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                if (cmd.ExecuteScalar() != null)
                {
                    Ques.Visible = false;
                    Ans.Visible = false;
                    Submit.Visible = false;
                    Pass.Text = "Your Password: " + cmd.ExecuteScalar().ToString() + "<br/><a href='Login.aspx' style='font-size:100%'>Login</a> now";
                }
                else
                {
                    Pass.Text = "Password incorrect try again";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Failed. Reason: " + ex.Message;
            }
        } 
    }
}