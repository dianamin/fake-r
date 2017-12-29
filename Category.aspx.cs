using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] == null) return;
        int id = int.Parse(Request.Params["id"]);
        string cerereSQL = "SELECT Name FROM Categories WHERE CategoryId=@pid";
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-T2GBBU6T\SQLEXPRESS;Initial Catalog=Database;Integrated Security=True");
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(cerereSQL, cn);
            cmd.Parameters.AddWithValue("pId", id);
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
}