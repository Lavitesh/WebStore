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
    public partial class MyUpload : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppName"] == null)
            {
                Response.Redirect("~/PrevDown.aspx");
            }
            else if (!IsPostBack)
            {
                fillTable();
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

                Size.Text = dt.Rows[0]["Size"].ToString() + " KB";

                Description.Text = dt.Rows[0]["Description"].ToString();

                DownloadNow.Visible = true;

                CreateTable();
            }
            catch (Exception ex)
            {
                Status.Text = "Some Error occured. Reason: " + ex.Message;
            }
        }

        protected void DownloadNow_Click(object sender, EventArgs e)
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
        }

        void CreateTable()
        {
            if (IsReviewAvailable(Session["AppName"].ToString()))
            {
                RevNotAvail.Visible = false;
                DataTable dt = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from AppReviewDetails where AppName = '" + Session["AppName"].ToString() + "';", con);
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
                RevNotAvail.Text = "No Reviews uploaded";
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
    }
}