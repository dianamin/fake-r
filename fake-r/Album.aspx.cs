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
    protected bool seeEditButtons;
    protected String Locations;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (Request.Params["album"] == null)
            Response.Redirect("~/");
        String id = Uri.UnescapeDataString(Request.Params["album"]);
        this.fetchAlbumDetails(id);
        this.fetchPhotos(id);
        this.fetchLocations(id);
        DataBind();
    }

    private void fetchAlbumDetails(String AlbumId)
    {
        string albumQuery = 
            "SELECT UserName, Name, Description, CreatedDate " +
            "FROM Albums " +
            "WHERE AlbumId = @pAlbumId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(albumQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            this.seeEditButtons = false;
            bool albumFound = false;
            while (reader.Read())
            {
                albumFound = true;
                AlbumName.Text = reader["Name"].ToString();
                UserName.Text = reader["UserName"].ToString();
                Description.Text = reader["Description"].ToString();

                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        this.seeEditButtons = true;
                    if (Roles.GetRolesForUser().Contains("Admin"))
                        this.seeEditButtons = true;
                }
                DataBind();
            }
            cn.Close();
            if (!albumFound)
                Response.Redirect("~/");
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhotos(String AlbumId)
    {
        PhotosSource.SelectCommand =
            "SELECT PhotoId, p.Name as PhotoName, a.UserName, UploadDate, c.Name as CategoryName, p.Description as Description " +
            "FROM Photos p JOIN Categories c ON (p.CategoryId = c.CategoryId) JOIN Albums a ON (a.AlbumId = p.AlbumId) " +
            "WHERE p.AlbumId = @pAlbumId " +
            "ORDER BY UploadDate DESC";
        PhotosSource.SelectParameters.Clear();
        PhotosSource.SelectParameters.Add("pAlbumId", AlbumId);
        Page.DataBind();
    }

    private void fetchLocations(String AlbumId)
    {
        this.Locations = "[]";
        string locationsQuery =
            "SELECT Latitude, Longitude " +
            "FROM Photos " +
            "WHERE AlbumId = @pAlbumId AND Latitude IS NOT NULL AND Longitude IS NOT NULL";
        string locations = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(locationsQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                locations += "{Longitude: " + reader["Longitude"].ToString() + ", Latitude: " + reader["Latitude"].ToString() + "}, ";
            }
            cn.Close();
            this.Locations = "[" + locations + "]";
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void DeleteAlbum_Click(object sender, EventArgs e)
    {
        int albumId = int.Parse(Uri.UnescapeDataString(Request.Params["album"]));
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

    protected void EditAlbum_Click(object sender, EventArgs e)
    {
        String albumId = Request.Params["album"];
        Response.Redirect("~/EditAlbum.aspx?album=" + albumId);
    }
}