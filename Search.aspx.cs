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
    public partial class Search : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getTable();
            }
        }

        protected void getTable()
        {
            if (Session["SearchVal"] != null)
            {
                Status.Text = "<h2>You Searched for " + Session["SearchVal"].ToString() + "</h2>";
                try
                {
                    DataTable dt = new DataTable();

                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from AppDetailsWithoutCategory where AppName like '%" + Session["SearchVal"].ToString() + "%';", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (!rd.HasRows)
                    {
                        Status.Text = "<h2>" + Session["SearchVal"].ToString() + " Not Found</h2>";
                    }
                    else
                    {
                        dt.Load(rd);
                        rd.Close();
                        con.Close();
                        SearchView.DataSource = dt;
                        SearchView.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = ex.Message;
                }
            }
            else if (Session["Category"] != null)
            {
                Status.Text = "<h2>" + Session["Category"].ToString() + " Applications</h2>";
                try
                {
                    DataTable dt = new DataTable();

                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from AppDetailsWithCategory where Category like '%" + Session["Category"].ToString() + "%';", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (!rd.HasRows)
                    {
                        Status.Text = "<H2>Sorry, There is no applications under " + Session["Category"].ToString() + " Category</H2>";
                    }
                    else
                    {
                        dt.Load(rd);
                        rd.Close();
                        con.Close();
                        SearchView.DataSource = dt;
                        SearchView.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }

        }

        protected void SearchView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoToApp")
            {
                Session["AppName"] = e.CommandArgument;
                Response.Redirect("~/DownloadPage.aspx");
            }
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = SearchView.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            SearchView.PageIndex = pageList.SelectedIndex;
            getTable();
        }

        protected void PreviousPage_Click(object sender, EventArgs e)
        {
            int currentPage = SearchView.PageIndex;
            GridViewRow pagerRow = SearchView.BottomPagerRow;
            LinkButton pageList = (LinkButton)pagerRow.Cells[0].FindControl("NextPage");
            if (currentPage != 0)
            {
                Session["Category"] = (pageList.CommandArgument == "") ? null : pageList.CommandArgument;
                Session["SearchVal"] = (pageList.CommandName == "") ? null : pageList.CommandName;
                SearchView.PageIndex = currentPage - 1;
                getTable();
            }
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            int currentPage = SearchView.PageIndex;
            GridViewRow pagerRow = SearchView.BottomPagerRow;
            LinkButton pageList = (LinkButton)pagerRow.Cells[0].FindControl("NextPage");
            if (currentPage != SearchView.PageCount)
            {
                Session["Category"] = (pageList.CommandArgument == "") ? null : pageList.CommandArgument;
                Session["SearchVal"] = (pageList.CommandName == "") ? null : pageList.CommandName;
                SearchView.PageIndex = currentPage + 1;
                getTable();
            }
        }

        protected void SearchView_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = SearchView.BottomPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");


            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < SearchView.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == SearchView.PageIndex)
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
                int currentPage = SearchView.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() + " of " + SearchView.PageCount.ToString();
            }

        }
    }
}