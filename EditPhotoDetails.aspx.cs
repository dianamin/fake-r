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

        String photoId = Uri.UnescapeDataString(Request.Params["photo"]);
        this.fetchUserAlbums();
        this.fetchPhoto(photoId);
    }

    private void fetchUserAlbums()
    {
        AlbumsSource.SelectCommand =
            "SELECT AlbumId, Name " +
            "FROM Albums " +
            "WHERE UserName=@pUserName";
        AlbumsSource.SelectParameters.Clear();
        AlbumsSource.SelectParameters.Add("pUserName", Profile.UserName);
        Page.DataBind();
    }

    private void fetchPhoto(String photoId)
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
            bool canEdit = false;
            while (reader.Read())
            {
                Photo.ImageUrl = "Images/" + reader["PhotoName"].ToString();
                Category.SelectedValue = reader["CategoryId"].ToString();
                Album.SelectedValue = reader["AlbumId"].ToString();
                Description.Text = reader["Description"].ToString();

                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        canEdit = true;
                    if (Roles.GetRolesForUser().Contains("Administrator"))
                        canEdit = true;
                }
            }
            cn.Close();
            if (!canEdit)
                Response.Redirect("~/Photo.aspx?photo=" + Uri.UnescapeDataString(Request.Params["photo"]));
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        String description = Uri.UnescapeDataString(Description.Text);
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
            cmd.Parameters.AddWithValue("pPhotoId", Uri.UnescapeDataString(Request.Params["photo"]));
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