using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.Security;

public partial class Photo : System.Web.UI.Page
{
    string photoName;
    protected bool seeEditButtons = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (Request.Params["photo"] == null)
            Response.Redirect("~/");
        String photoId = Request.Params["photo"];
        this.fetchPhoto(photoId);
        this.fetchComments(photoId);
    }

    protected void AddComment_Click(object sender, EventArgs e)
    {
        String photoId = Request.Params["photo"];

        TextBox CommentMessage = (TextBox)LoginView2.FindControl("CommentMessage");
        string commentMessage = CommentMessage.Text; 

        string insertCommentQuery = 
            "INSERT INTO Comments (PhotoId,UserName,Message) " +
            "VALUES (@pPhotoId,@pUserName,@pMessage)";

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(insertCommentQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            cmd.Parameters.AddWithValue("pUserName", Profile.UserName);
            cmd.Parameters.AddWithValue("pMessage", commentMessage);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Photo.aspx?photo=" + photoId);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhoto(String photoId)
    {
        string photoQuery =
            "SELECT p.Name as PhotoName, a.UserName as UserName, c.Name as Category, UploadDate, a.Name as AlbumName, a.AlbumId as AlbumId, p.Description as Description " +
            "FROM Photos p JOIN Categories c ON (c.CategoryId = p.CategoryId) JOIN Albums a ON (a.AlbumId = p.AlbumId) " + 
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
                Image.ImageUrl = "Images/" + reader["PhotoName"].ToString();
                this.photoName = reader["PhotoName"].ToString();
                Category.Text = reader["Category"].ToString();
                Description.Text = reader["Description"].ToString();
                UploadDate.Text = reader["UploadDate"].ToString();
                
                this.seeEditButtons = false;
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        this.seeEditButtons = true;
                    if (Roles.GetRolesForUser().Contains("admin"))
                        this.seeEditButtons = true;
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

    private void fetchComments(String photoId)
    {
        CommentsSource.SelectCommand =
            "SELECT CommentId, UserName, Message, PostDate " + 
            "FROM Comments " + 
            "WHERE PhotoId=@pPhotoId ORDER BY PostDate Desc";
        CommentsSource.SelectParameters.Clear();
        CommentsSource.SelectParameters.Add("pPhotoId", photoId);
        Page.DataBind();
    }

    protected void DeletePhoto_Click(object sender, EventArgs e)
    {
        int photoId = int.Parse(Request.Params["photo"]);
        string deletePhotoQuery =
            "DELETE FROM Photos " +
            "WHERE PhotoId = @pPhotoId";

        var photoPath = Server.MapPath("~/Images/" + photoName);
 
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(deletePhotoQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }
            Response.Redirect("~/");
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void DeleteComment_Click(object sender, EventArgs e)
    {   
        String photoId = Request.Params["photo"];
        String commentId = ((Button)sender).CommandArgument.ToString();
        String deleteCommentsQuery =
            "DELETE FROM Comments " +
            "WHERE CommentId = @pCommentId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(deleteCommentsQuery, cn);
            cmd.Parameters.AddWithValue("pCommentId", commentId);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Photo.aspx?photo=" + photoId);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void CropPhoto_Click(object sender, EventArgs e)
    {
        String photoId = Request.Params["photo"];
        Response.Redirect("~/CropPhoto.aspx?photo=" + photoId);
    }

    protected void EditPhotoDetails_Click(object sender, EventArgs e)
    {
        String photoId = Request.Params["photo"];
        Response.Redirect("~/EditPhotoDetails.aspx?photo=" + photoId);
    }
}