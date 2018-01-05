using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (Request.Params["q"] == null) return;
        search(Uri.UnescapeDataString(Request.Params["q"].ToLower()));
    }

    protected void search(String text)
    {
        SqlDataSource1.SelectCommand =
            "SELECT PhotoId, p.Name as PhotoName, a.UserName, UploadDate, a.AlbumId, a.Name as AlbumName, c.Name as CategoryName, p.Description as Description " +
            "FROM Photos p JOIN Categories c ON (p.CategoryId = c.CategoryId) JOIN Albums a ON (p.AlbumId = a.AlbumId) " +
            "WHERE LOWER(p.Description) LIKE @p Or LOWER(a.Name) LIKE @p OR LOWER(c.Name) Like @p " +
            "ORDER BY UploadDate DESC";
        SqlDataSource1.SelectParameters.Clear();

        SqlDataSource1.SelectParameters.Add("p", "%" + text + "%");
        Page.DataBind();
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?q=" + SearchBox.Text);
    }
}