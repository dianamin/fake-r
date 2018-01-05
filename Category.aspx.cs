using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["category"] == null)
            Response.Redirect("~/Categories.aspx");
        if (Page.IsPostBack) return;
        String id = Uri.UnescapeDataString(Request.Params["category"]);
        this.fetchCategoryDetails(id);
        this.fetchPhotos(id);
    }

    private void fetchCategoryDetails(String CategoryId)
    {
        string categoryQuery =
            "SELECT Name FROM Categories " + 
            "WHERE CategoryId=@pCategoryId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(categoryQuery, cn);
            cmd.Parameters.AddWithValue("pCategoryId", CategoryId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CategoryName.Text = reader["Name"].ToString();
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhotos(String CategoryId)
    {
        string photosQuery =
        PhotosSource.SelectCommand =
            "SELECT PhotoId, p.Name as PhotoName, a.UserName as UserName, UploadDate, a.Name as AlbumName, a.AlbumId as AlbumId, p.Description as Description " +
            "FROM Photos p, Albums a " +
            "WHERE CategoryId = @pCategoryId AND p.AlbumId = a.AlbumId " +
            "ORDER BY UploadDate DESC";
        PhotosSource.SelectParameters.Clear();
        PhotosSource.SelectParameters.Add("pCategoryId", CategoryId);
        Page.DataBind();
    }
}