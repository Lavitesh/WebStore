using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore.User.Cus
{
    public partial class DownloadApp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Customer"] != null)
            {
                if (!IsPostBack && Session["AppName"] != null)                
                    fillTable();                
            }
            else
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
        }

        void fillTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("AppName");
                dt.Columns.Add("Icon");
                dt.Columns.Add("FileName");
                dt.Columns.Add("Extension");
                dt.Columns.Add("Size");
                dt.Columns.Add("Description");
                dt.Columns.Add("Price");

                con.Open();
                string command = "SELECT * FROM AppDetailsWithoutCategory WHERE AppName = '" + Session["AppName"].ToString() + "';";
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                con.Close();

                AppName.Text = dt.Rows[0]["AppName"].ToString();

                Iconimage.AlternateText = dt.Rows[0]["Icon"].ToString();
                Iconimage.ImageUrl = "~/Apps/" + dt.Rows[0]["AppName"].ToString() + "/img/" + dt.Rows[0]["Icon"].ToString();

                Extension.Text = dt.Rows[0]["Extension"].ToString();

                Size.Text = dt.Rows[0]["Size"].ToString();

                Description.Text = dt.Rows[0]["Description"].ToString();

                DownloadNow.Visible = true;

                if (Convert.ToDouble(dt.Rows[0]["Price"].ToString()) == 0.00)
                    Price.Text = "FREE";
                else
                {
                    Price.Text = "₹" + dt.Rows[0]["Price"].ToString();
                    DownloadNow.OnClientClick = "Confirm()";
                }

                CreateTable();
            }
            catch (Exception ex)
            {
                Status.Text = "Some Error occured. Reason: " + ex.Message;
            }
        }

        protected void DownloadNow_Click(object sender, EventArgs e)
        {
            if (Price.Text=="FREE")
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("AppName");
                    dt.Columns.Add("FileName");

                    con.Open();
                    string command = "SELECT * FROM AppDetailsWithoutCategory WHERE AppName = '" + Session["AppName"].ToString() + "';";
                    SqlCommand cmd = new SqlCommand(command, con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();

                    updatehistory();

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "filename=" + dt.Rows[0][2].ToString());
                    Response.TransmitFile(Server.MapPath("~/Apps/" + dt.Rows[0][1].ToString() + "/" + dt.Rows[0][2].ToString()));
                    Response.End();
                }
                catch (Exception ex)
                {
                    Status.Text = "Some error occured during transmission. Reason: " + ex.Message;
                } 
            }
            else
            {
                string confirmVal = Request.Form["confirm_value"];                
                if (confirmVal == "Yes" )
                {
                    if (!CheckAlreadydownloaded())
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID");
                            dt.Columns.Add("AppName");
                            dt.Columns.Add("FileName");

                            con.Open();
                            string command = "SELECT * FROM AppDetailsWithoutCategory WHERE AppName = '" + Session["AppName"].ToString() + "';";
                            SqlCommand cmd = new SqlCommand(command, con);
                            SqlDataReader rd = cmd.ExecuteReader();
                            dt.Load(rd);
                            con.Close();

                            if (paymoney())//make the user pay for app....
                            {
                                updatehistory();
                                Response.Clear();
                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", "filename=" + dt.Rows[0][2].ToString());
                                Response.TransmitFile(Server.MapPath("~/Apps/" + dt.Rows[0][1].ToString() + "/" + dt.Rows[0][2].ToString()));
                                Response.End();
                            }

                        }
                        catch (Exception ex)
                        {
                            Status.Text = "Some error occured. Reason: " + ex.Message;
                        }
                    } //if not paid before.....
                    else
                    {
                        try
                        {
                            Status.Visible = true;
                            Status.Text = "You have already paid download starting...";
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID");
                            dt.Columns.Add("AppName");
                            dt.Columns.Add("FileName");

                            con.Open();
                            string command = "SELECT * FROM AppDetailsWithoutCategory WHERE AppName = '" + Session["AppName"].ToString() + "';";
                            SqlCommand cmd = new SqlCommand(command, con);
                            SqlDataReader rd = cmd.ExecuteReader();
                            dt.Load(rd);
                            con.Close();

                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.AppendHeader("Content-Disposition", "filename=" + dt.Rows[0][2].ToString());
                            Response.TransmitFile(Server.MapPath("~/Apps/" + dt.Rows[0][1].ToString() + "/" + dt.Rows[0][2].ToString()));
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            Status.Text = "Some error occured. Reason: " + ex.Message;
                        }
                    }//if paid before.....
                }                  
            }
            Page_Load(sender, e);
        }

        void updatehistory()
        {
            if (!CheckAlreadydownloaded())
            {
                try
                {
                    con.Open();                    
                    SqlCommand cmd = new SqlCommand("sp_DownloadHistoryAdd",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Uname",Session["Customer"].ToString());
                    cmd.Parameters.AddWithValue("AppName", Session["AppName"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Status.Text = "Some error occured when updating history. Reason:" + ex.Message;
                } 
            }
        }

        bool CheckAlreadydownloaded()
        {
            int val = 1;
            try
            {
                con.Open();
                string command = "SELECT * FROM fn_DownloadHistoryExist('" + Session["Customer"].ToString() + "','" + Session["AppName"].ToString() + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured. Reason:" + ex.Message;
            }

            return (val == 1) ? true : false;
        }

        protected void LeaveReview_Click(object sender, EventArgs e)
        {
            EnterText.Visible = true;
            LeaveReview.Visible = false;
            Save.Visible = true;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string txtfilename = Session["Customer"].ToString() + "-" + Session["AppName"].ToString() + "-" + DateTime.Now.Date.ToString("yyyy-mm-dd") + ".txt";           
            string path = Server.MapPath("~/Apps/" + Session["AppName"].ToString() + "/Review/" + txtfilename);
            try
            {
                if (!File.Exists(path))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddReview", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CustomerName", Session["Customer"].ToString());
                    cmd.Parameters.AddWithValue("AppName", Session["AppName"].ToString());
                    cmd.Parameters.AddWithValue("ReviewLoc", txtfilename);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    using (FileStream fs = File.Create(path))
                    {
                        Byte[] revtext = new UTF8Encoding(true).GetBytes(EnterText.Text);
                        fs.Write(revtext, 0, revtext.Length);
                    }
                    EnterText.Visible = false;
                    Save.Visible = false;
                    LeaveReview.Visible = true;
                    Noreview.Visible = true;
                    Noreview.Text = "Thanks for giving us your review";
                }
                else
                {
                    EnterText.Visible = false;
                    Save.Visible = false;
                    LeaveReview.Visible = true;
                    Noreview.Visible = true;
                    Noreview.Text = "You have already posted a review";
                }
                CreateTable();
            }
            catch (Exception ex)
            {
                Status.Text = "Error occured: " + ex.Message;
            }

        }

        public string getReview(string path)
        {
            path = Server.MapPath(path);
            string str = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                    str += s;
            }
            return str;
        }

        void CreateTable()
        {
            if (IsReviewAvailable(Session["AppName"].ToString()))
            {
                RevNotAvail.Visible = false;
                DataTable dt = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from AppReviewDetails where AppName = '"+ Session["AppName"].ToString() +"';", con);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                rd.Close();
                con.Close();

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                RevNotAvail.Visible = true;
                RevNotAvail.Text = "Be the first one to leave a review.";
            }
        }

        bool IsReviewAvailable(string Appname)
        {
            int val = 0;
            try
            {
                con.Open();
                string command = "select * from fn_RevAvail('" + Appname + "');";
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

        bool paymoney()
        {
            bool paid = false;
            try
            {
                if (CheckMoney()) //if true then user has money to pay....
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("Developer");
                    string command = "SELECT * FROM AppDetailsWithoutCategory WHERE AppName = '" + Session["AppName"].ToString() + "';";
                    SqlCommand cmd = new SqlCommand(command, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    double Price = Convert.ToDouble(dt.Rows[0]["Price"].ToString());
                    string Dev = dt.Rows[0]["Developer"].ToString();

                    SqlCommand ccmd = new SqlCommand("sp_PayMoney", con);
                    ccmd.CommandType = CommandType.StoredProcedure;
                    ccmd.Parameters.AddWithValue("Price", Price);
                    ccmd.Parameters.AddWithValue("Developer", Dev);
                    ccmd.Parameters.AddWithValue("Customer", Session["Customer"].ToString());
                    ccmd.ExecuteNonQuery();
                    con.Close();
                    paid = true; 
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured. Reason: " + ex.Message;
            }
            return paid;
        }

        bool CheckMoney()
        {
            int val = 0;
            try
            {
                con.Open();
                string command = "select dbo.fn_CheckMoney('" + Session["Customer"].ToString() + "','C','" + Session["AppName"].ToString() + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                Status.Text = "No money left add money to download";
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured(CM). Reason:" + ex.Message;
            }
            return (val == 1) ? true : false;
        }
    }
}