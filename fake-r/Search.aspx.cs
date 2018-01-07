using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected double Latitude;
    protected double Longitude;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        this.Latitude = 0;
        this.Longitude = 0;
        DataBind();

        if (Request.Params["q"] != null)
        {
            this.search(Uri.UnescapeDataString(Request.Params["q"].ToLower()));
            DataBind();
            return;
        }
        if (Request.Params["lat"] == null) return;
        if (!Double.TryParse(Request.Params["lat"], out this.Latitude)) return;
        if (Request.Params["long"] == null) return;
        if (!Double.TryParse(Request.Params["long"], out this.Longitude)) return;
        this.search(this.Latitude, this.Longitude);
        DataBind();
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

    protected void search(double latitude, double longitude)
    {
        SqlDataSource1.SelectCommand =
            "SELECT PhotoId, p.Name as PhotoName, a.UserName, UploadDate, a.AlbumId, a.Name as AlbumName, c.Name as CategoryName, p.Description as Description, Latitude, Longitude " +
            "FROM Photos p JOIN Categories c ON (p.CategoryId = c.CategoryId) JOIN Albums a ON (p.AlbumId = a.AlbumId) " +
            "WHERE Latitude - @pLatitude <= 1 AND Longitude - @pLongitude <= 1 " +
            "ORDER BY UploadDate DESC";
        SqlDataSource1.SelectParameters.Clear();

        SqlDataSource1.SelectParameters.Add("pLatitude", latitude.ToString());
        SqlDataSource1.SelectParameters.Add("pLongitude", longitude.ToString());
        Page.DataBind();
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?q=" + SearchBox.Text);
    }

    protected void SearchLocation_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?lat=" + LatitudeField.Value + "&long=" + LongitudeField.Value);
    }
}