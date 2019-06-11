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
    public partial class Threadlist : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore; Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateTable();
            }
        }

        public int getmessagescount(string topicname)
        {
            string type = Request.QueryString["type"];
            string path = Server.MapPath("~/Forums/" + type + "/" + topicname);
            return Directory.GetFiles(path).Length;
        }

        protected void GDThreadList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoToThread")
            {
                string[] cmdargs = e.CommandArgument.ToString().Split('/');
                Response.Redirect("~/Thread.aspx?type=" + cmdargs[1] + "&thread=" + cmdargs[0]);
            }
        }

        void CreateTable()
        {
            string type = Request.QueryString["type"];
            try
            {
                DataTable dt = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Forum where Type = '" + type + "';", con);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                rd.Close();
                con.Close();

                if (type == "PD")
                    TypeName.Text = "Product Discussions";
                else if (type == "CR")
                    TypeName.Text = "Customer's Requests";
                else if (type == "GC")
                    TypeName.Text = "General Chats";

                if (dt.Rows.Count == 0)
                {
                    emptylist.Visible = true;
                    emptylist.Text = "No Discussions. <a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion";
                }
                else
                {
                    Label lbl = new Label()
                    {
                        Text = "<a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to start a new discussion",
                        CssClass = "col-12 form-control text-right",
                    };
                    FirstList.Controls.Add(lbl);

                    GDThreadList.DataSource = dt;
                    GDThreadList.DataBind();
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured: " + ex.Message;
            }
        }

        protected void gD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GDThreadList.PageIndex = e.NewPageIndex;
            CreateTable();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = GDThreadList.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            GDThreadList.PageIndex = pageList.SelectedIndex;
            CreateTable();
        }

        protected void PreviousPage_Click(object sender, EventArgs e)
        {
            int currentPage = GDThreadList.PageIndex;
            if (currentPage != 0)
            {
                GDThreadList.PageIndex = currentPage - 1;
                CreateTable();
            }
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            int currentPage = GDThreadList.PageIndex;
            if (currentPage != GDThreadList.PageCount)
            {
                GDThreadList.PageIndex = currentPage + 1;
                CreateTable();
            }
        }

        protected void gD_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = GDThreadList.BottomPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");


            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < GDThreadList.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == GDThreadList.PageIndex)
                    {
                        item.Selected = true;
                    }

                    // Add the ListItem object to the Items collection of the 
                    // DropDownList.
                    pageList.Items.Add(item);

                }

            }

            if (pageLabel != null)
            {

                // Calculate the current page number.
                int currentPage = GDThreadList.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() + " of " + GDThreadList.PageCount.ToString();
            }

        }
    }
}