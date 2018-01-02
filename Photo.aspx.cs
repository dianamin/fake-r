﻿using System;
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
        if (Request.Params["photo"] != null)
        {
            int photoId = int.Parse(Request.Params["photo"]);
            this.fetchPhoto(photoId);
            this.fetchComments(photoId);
        }
    }

    protected void AddComment_Click(object sender, EventArgs e)
    {
        if (Request.Params["photo"] != null)
        {
            int photoId = int.Parse(Request.Params["photo"]);

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
        string photoQuery =
            "SELECT p.Name as PhotoName, p.UserName as UserName, c.Name as Category, UploadDate, a.Name as AlbumName, a.AlbumId as AlbumId, p.Description as Description " +
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
        string commentsQuery = 
            "SELECT CommentId,UserName,Message,PostDate " + 
            "FROM Comments " + 
            "WHERE PhotoId=@pPhotoId ORDER BY PostDate Desc";
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

    protected void DeletePhoto_Click(object sender, EventArgs e)
    {
        if (Request.Params["photo"] == null) return;
        int photoId = int.Parse(Request.Params["photo"]);
        string deletePhotoQuery =
            "DELETE FROM Photos " +
            "WHERE PhotoId = @pPhotoId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(deletePhotoQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/");
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}