using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana
{
    public partial class Dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeft();
                BindRight();
            }
        }

        protected void BindLeft()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = $"SELECT * FROM Country";   // 建立 SQL語句
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd != null)
            {
                RepeaterLeft.DataSource = rd;
                RepeaterLeft.DataBind();
            }
            connection.Close();
        }

        protected void BindRight()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence;
            if (Request.QueryString["Id"] == null)
            {
                SQLSentence = "select * from country where Id = 1";
            }
            else
            {
                SQLSentence = "select * from country where Id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    labelRoute.Text = rd["Name"].ToString();
                    LiteralTitle.Text = rd["Name"].ToString();
                }
            }
            rd.Close();
            sqlCommand.Parameters.Clear();
            if (Request.QueryString["Id"] == null)
            {
                SQLSentence = "select a.*, r.Name as RegionName from Agent as a left join Region as r on a.RegionId = r.Id left join Country as c on r.CountryId = c.Id where c.Id =1";
            }
            else
            {
                SQLSentence = "select a.*, r.Name as RegionName from Agent as a left join Region as r on a.RegionId = r.Id left join Country as c on r.CountryId = c.Id where c.Id = @Id";
                sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            RepeaterAgentDetail.DataSource = rd;
            RepeaterAgentDetail.DataBind();
            connection.Close();
        }
    }
}