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
    public partial class CreateThread : System.Web.UI.Page
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
                if (Request.QueryString["type"] == "PD")
                    ggf.Text = "Product Discussions";
                else if (Request.QueryString["type"] == "CR")
                    ggf.Text = "Customer's Requests";
                else if (Request.QueryString["type"] == "GC")
                    ggf.Text = "General Chats";

                ThreadStartDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!CheckForumExist())
                {
                    try
                    {
                        UpdateForumDatabase();
                    }
                    catch (Exception ex)
                    {
                        Status.Text = "Error occured try again, if problem persists try later. Reason: " + ex.Message;
                    }
                }
                else
                {
                    Status.Text = "Thread already Exists. Check again or try with another Name";
                }
            }
        }

        bool CheckForumExist()
        {
            int val = 1;
            try
            {
                con.Open();
                string command = "select * from fn_ForumExist('" + Request.QueryString["type"] + "','" + ThreadName.Text + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Exception Occured </br> Reason: " + ex.Message;
            }
            if (val == 0 && !CheckIfDirectoryExist())
                return false;
            else
                return true;
        }

        bool CheckIfDirectoryExist()//checks if there is directory of Thread in forum.....
        {
            bool val = true;
            string type = Request.QueryString["type"];
            try
            {
                string path = Server.MapPath("~/Forums/" + type + "/" + ThreadName.Text + "/");
                if (Directory.Exists(path))
                    val = true;
                else
                    val = false;
            }
            catch
            {
                Status.Text = "Enter a valid thread name. It must not contain any alphanumeric characters like ?, #, % or etc.";
            }
            return val;
        }

        void UpdateForumDatabase()//Updates database and send control to upload fles to save files.....
        {
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand("sp_AddForum", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TType", Request.QueryString["type"]);
                cmd.Parameters.AddWithValue("TName", ThreadName.Text);
                cmd.Parameters.AddWithValue("StarterDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("StarterType", "D");
                cmd.Parameters.AddWithValue("Starter", Session["Developer"].ToString());
                cmd.ExecuteNonQuery();

                CreateThreadFolder(); //It will create a folder for thread.....

                Status.Text = "Thread Created.";                
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Error occured. Reason: " + ex.Message;
            }
        }

        void CreateThreadFolder()
        {
            string type = Request.QueryString["type"];
            try
            {                
                string forumpath = Server.MapPath("~/Forums/" + type + "/" + ThreadName.Text + "/");
                Directory.CreateDirectory(forumpath);
            }
            catch (Exception ex)
            {
                Status.Text = "Error occured: " + ex.Message;
                //if some error occured then delete every thing with same thread name and type from database(will do it later)....
            }
        }
    }
}