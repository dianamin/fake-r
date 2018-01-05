using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class Album : System.Web.UI.Page
{
    protected bool canDelete;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (Request.Params["album"] == null)
            Response.Redirect("~/");
        int id = int.Parse(Request.Params["album"]);
        this.fetchAlbumDetails(id);
        this.fetchPhotos(id);
    }

    private void fetchAlbumDetails(int AlbumId)
    {
        string albumQuery = 
            "SELECT UserName, Name, Description, CreatedDate " +
            "FROM Albums " +
            "WHERE AlbumId=@pAlbumId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(albumQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlbumName.Text = reader["Name"].ToString();
                UserName.Text = reader["UserName"].ToString();

                this.canDelete = false;
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        this.canDelete = true;
                    if (Roles.GetRolesForUser().Contains("Administrator"))
                        this.canDelete = true;
                }
                DataBind();
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhotos(int AlbumId)
    {
        string photosQuery =
            "SELECT PhotoId, p.Name as PhotoName, p.UserName, UploadDate, c.Name as CategoryName, p.Description as Description " +
            "FROM Photos p JOIN Categories c ON (p.CategoryId = c.CategoryId) " +
            "WHERE p.AlbumId = @pAlbumId " +
            "ORDER BY UploadDate DESC";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(photosQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
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

    protected void DeleteAlbum_Click(object sender, EventArgs e)
    {
        if (Request.Params["album"] == null) return;
        int albumId = int.Parse(Request.Params["album"]);
        string deletePhotoQuery =
            "DELETE FROM Albums " +
            "WHERE AlbumId = @pAlbumId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(deletePhotoQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", albumId);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/User.aspx?username=" + Profile.UserName);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}