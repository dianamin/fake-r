using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Photo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
        {
            int photoId = int.Parse(Request.Params["id"]);
            this.fetchPhoto(photoId);
            this.fetchComments(photoId);
        }
    }

    protected void AddComment_Click(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
        {
            int photoId = int.Parse(Request.Params["id"]);
            string commentMessage = CommentMessage.Text; 

            string insertCommentQuery = 
                "INSERT INTO Comments (PhotoId,UserId,Message) " + 
                "VALUES (@pPhotoId,'14bc71cc-2838-4807-b95b-eb6f2d7625a6',@pMessage)";

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(insertCommentQuery, cn);
                cmd.Parameters.AddWithValue("pPhotoId", photoId);
                cmd.Parameters.AddWithValue("pMessage", commentMessage);
                System.Diagnostics.Debug.WriteLine(photoId);
                System.Diagnostics.Debug.WriteLine(commentMessage);
                cmd.ExecuteNonQuery();
                cn.Close();
                CommentMessage.Text = "";
            }
            catch (Exception exCMD)
            {
                Console.WriteLine(exCMD.Message);
            }
            finally
            {
                this.fetchComments(photoId);
            }
        }
    }

    private void fetchPhoto(int photoId)
    {
        string cerereSQL = 
            "SELECT Url,UserId,c.Name as Category,Description,UploadDate " + 
            "FROM Photos p JOIN Categories c ON (c.CategoryId = p.CategoryId) " + 
            "WHERE PhotoId=@pPhotoId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(cerereSQL, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Image.ImageUrl = reader["Url"].ToString();
                Category.Text = reader["Category"].ToString();
                Description.Text = reader["Description"].ToString();
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchComments(int photoId)
    {
        int id = int.Parse(Request.Params["id"]);
        string commentsQuery = 
            "SELECT CommentId,u.UserName as UserName,Message,PostDate " + 
            "FROM Comments c,aspnet_Users u " + 
            "WHERE PhotoId=@pPhotoId AND u.UserId=c.UserId ORDER BY PostDate Desc";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(commentsQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            SqlDataReader reader = cmd.ExecuteReader();
            Comments.DataSource = reader;
            Comments.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}