using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (IsPostBack) return;
        this.fetchUserAlbums();
    }

    protected void Upload_Click(object sender, EventArgs e)
    {
        String imageName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-") + Guid.NewGuid().ToString() + ".jpg";
        Image.PostedFile.SaveAs(Server.MapPath("~/Images/") + imageName);
        String description = Description.Text;
        int categoryId = int.Parse(Category.SelectedValue);
        int albumId = int.Parse(Album.SelectedValue);
        String longitude = Longitude.Value;
        String latitude = Latitude.Value;
        
        string insertPhotoQuery =
            "INSERT INTO Photos (Name, CategoryId, Description, AlbumId, Latitude, Longitude) " +
            "VALUES (@pImageName, @pCategoryId, @pDescription, @pAlbumId, @pLatitude, @pLongitude)";

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(insertPhotoQuery, cn);
            cmd.Parameters.AddWithValue("pImageName", imageName);
            cmd.Parameters.AddWithValue("pUserName", Profile.UserName);
            cmd.Parameters.AddWithValue("pCategoryId", categoryId);
            cmd.Parameters.AddWithValue("pDescription", description);
            cmd.Parameters.AddWithValue("pAlbumId", albumId);
            if (latitude == null)
                cmd.Parameters.AddWithValue("pLatitude", null);
            else
                cmd.Parameters.AddWithValue("pLatitude", Double.Parse(latitude));
            if (longitude == null)
                cmd.Parameters.AddWithValue("pLongitude", null);
            else
                cmd.Parameters.AddWithValue("pLongitude", Double.Parse(longitude));
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
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
}