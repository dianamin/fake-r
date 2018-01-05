using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

public partial class EditPhotoDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Request.Params["photo"] == null)
            Response.Redirect("~/");

        int photoId = int.Parse(Request.Params["photo"]);
        this.fetchUserAlbums();
        this.fetchPhoto(photoId);
    }

    private void fetchUserAlbums()
    {
        if (Album == null) return;
        string albumsQuery =
            "SELECT AlbumId, Name " +
            "FROM Albums " +
            "WHERE UserName=@pUserName";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(albumsQuery, cn);
            cmd.Parameters.AddWithValue("pUserName", Profile.UserName);
            SqlDataReader reader = cmd.ExecuteReader();
            Album.DataSource = reader;
            Album.DataValueField = "AlbumId";
            Album.DataTextField = "Name";
            Album.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhoto(int photoId)
    {
        string photoQuery =
            "SELECT Name as PhotoName, UserName, CategoryId, AlbumId, Description " +
            "FROM Photos " +
            "WHERE PhotoId=@pPhotoId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(photoQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Photo.ImageUrl = "Images/" + reader["PhotoName"].ToString();
                Category.SelectedValue = reader["CategoryId"].ToString();
                Album.SelectedValue = reader["AlbumId"].ToString();
                Description.Text = reader["Description"].ToString();

                bool canEdit = false;
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        canEdit = true;
                    if (Roles.GetRolesForUser().Contains("Administrator"))
                        canEdit = true;
                }
                if (!canEdit)
                    Response.Redirect("~/Photo.aspx?photo=" + Request.Params["photo"]);
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        String description = Description.Text;
        int categoryId = int.Parse(Category.SelectedValue);
        int albumId = int.Parse(Album.SelectedValue);

        string updatePhotoQuery =
            "Update Photos " +
            "SET CategoryId = @pCategoryId, Description = @pDescription, AlbumId = @pAlbumId " +
            "WHERE PhotoId = @pPhotoId";

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(updatePhotoQuery, cn);
            cmd.Parameters.AddWithValue("pCategoryId", categoryId);
            cmd.Parameters.AddWithValue("pDescription", description);
            cmd.Parameters.AddWithValue("pAlbumId", albumId);
            cmd.Parameters.AddWithValue("pPhotoId", int.Parse(Request.Params["photo"]));
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Photo.aspx?photo=" + Request.Params["photo"]);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}