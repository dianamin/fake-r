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
        int id = int.Parse(Request.Params["category"]);
        this.fetchCategoryDetails(id);
        this.fetchPhotos(id);
    }

    private void fetchCategoryDetails(int CategoryId)
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

    private void fetchPhotos(int CategoryId)
    {
        string photosQuery =
            "SELECT PhotoId, p.Name as PhotoName, p.UserName as UserName, UploadDate, a.Name as AlbumName, a.AlbumId as AlbumId, p.Description as Description " +
            "FROM Photos p, Albums a  " +
            "WHERE CategoryId = @pCategoryId AND p.AlbumId = a.AlbumId " +
            "ORDER BY UploadDate DESC";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(photosQuery, cn);
            cmd.Parameters.AddWithValue("pCategoryId", CategoryId);
            SqlDataReader reader = cmd.ExecuteReader();
            Photos.DataSource = reader;
            Photos.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}