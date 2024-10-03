using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana
{
    public partial class News : System.Web.UI.Page
    {
        private int pageNum;
        private int renderNum = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeaterMain();
                BindLiteralItemCount();
                BindPage();
            }
        }

        protected void BindRepeaterMain()
        {
            if (Request.QueryString["page"] == null)
            {
                pageNum = 1;
            }
            else
            {
                pageNum = Convert.ToInt32(Request.QueryString["page"]);
            }
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from News order by CreateTime desc OFFSET @OffsetRow ROWS FETCH NEXT @NextRow ROWS ONLY";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@OffsetRow", (pageNum - 1) * renderNum);
                sqlCommand.Parameters.AddWithValue("@NextRow", renderNum);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    RepeaterMain.DataSource = rd;
                    RepeaterMain.DataBind();
                }
                connection.Close();
            }
        }

        protected void BindPage()
        {
            LiteralPageNow.Text = $"{pageNum}/{Convert.ToInt32(LiteralItemCount.Text) / renderNum + 1}";
            for (int i = 1; i <= Convert.ToInt32(LiteralItemCount.Text) / renderNum + 3; i++)
            {
                if (i == pageNum)
                {
                    LiteralPages.Text += $"| {i} ";
                }
                else if (i == Convert.ToInt32(LiteralItemCount.Text) / renderNum + 2)
                {
                    if (pageNum == Convert.ToInt32(LiteralItemCount.Text) / renderNum + 1)
                    {
                        LiteralPages.Text += $"|  <a href='News.aspx?page={pageNum}'>Next</a> ";
                    }
                    else
                    {
                        LiteralPages.Text += $"|  <a href='News.aspx?page={pageNum + 1}'>Next</a> ";
                    }
                }
                else if (i == Convert.ToInt32(LiteralItemCount.Text) / renderNum + 3)
                {
                    LiteralPages.Text += $" <a href='News.aspx?page={Convert.ToInt32(LiteralItemCount.Text) / renderNum + 1}'>LastPage</a>";
                }
                else
                {
                    LiteralPages.Text += $"| <a href='News.aspx?page={i}'>{i}</a> ";
                }
            }
        }

        protected void BindLiteralItemCount()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select COUNT(*) as count from News";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        LiteralItemCount.Text = rd["count"].ToString();
                    }
                }
                connection.Close();
            }
        }
    }
}