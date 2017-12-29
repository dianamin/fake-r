using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["username"] == null) return;
        this.UserName.Text = Request.Params["username"];
        this.fetchAlbums(Request.Params["username"]);
    }

    private void fetchAlbums(string userName)
    {
        string albumsQuery =
            "SELECT a.AlbumId,a.Name,a.Description,COUNT(PhotoId) as PhotosCount " +
            "FROM Albums a LEFT JOIN Photos p ON (a.AlbumId = p.AlbumId) " +
            "GROUP BY a.AlbumId,a.Name,a.Description";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(albumsQuery, cn);
            cmd.Parameters.AddWithValue("pUserName", userName);
            SqlDataReader reader = cmd.ExecuteReader();
            Albums.DataSource = reader;
            Albums.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}