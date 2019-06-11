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

namespace WebStore
{
    public partial class ForumVisitor : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore; Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateTables();
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ThreadList.aspx?type=" + (sender as LinkButton).Attributes["CustomParameter"].ToString());
        }

        public int getmessagescount(string topicname, string type)
        {
            string path = Server.MapPath("~/Forums/" + type + "/" + topicname);
            return Directory.GetFiles(path).Length;
        }

        void CreateTables()
        {
            tablenewcreate("PD");
            tablenewcreate("CR");
            tablenewcreate("GC");
        }

        protected void GDForumPD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoToThread")
            {
                string[] cmdargs = e.CommandArgument.ToString().Split('/');
                Response.Redirect("~/Thread.aspx?type=" + cmdargs[1] + "&thread=" + cmdargs[0]);
            }
        }

        void tablenewcreate(string type)
        {
            try
            {
                DataTable dt = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP(5) * from Forum where Type = '" + type + "';", con);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                rd.Close();
                con.Close();


                if (dt.Rows.Count == 0)
                {
                    if (type == "PD")
                    {
                        PDempty.Visible = true;
                        PDempty.Text = "No Discussions. <a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion";
                    }
                    else if (type == "CR")
                    {
                        CRempty.Visible = true;
                        CRempty.Text = "No Discussions. <a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion";
                    }
                    else
                    {
                        GCempty.Visible = true;
                        GCempty.Text = "No Discussions. <a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion";
                    }
                }
                else if (type == "PD")
                {
                    GDForumPD.DataSource = dt;
                    GDForumPD.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "<a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion",
                        CssClass = "col-12 form-control text-center",
                    };
                    PDLast.Controls.Add(lbl);
                }
                else if (type == "CR")
                {
                    GDForumCR.DataSource = dt;
                    GDForumCR.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "<a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion",
                        CssClass = "col-12 form-control text-center",
                    };
                    CRLast.Controls.Add(lbl);
                }
                else
                {
                    GDForumGC.DataSource = dt;
                    GDForumGC.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "<a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion",
                        CssClass = "col-12 form-control text-center",
                    };
                    GCLast.Controls.Add(lbl);
                }

            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured: " + ex.Message;
            }
        }
    }
}