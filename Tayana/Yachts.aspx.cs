using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Tayana
{
    public partial class Yachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeaterGallery();
                BindLeft();
                BindRouteAndTitle();
                BindPanel();
            }
        }

        protected void BindLeft()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from Yacht";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    RepeaterLeft.DataSource = rd;
                    RepeaterLeft.DataBind();
                }
                connection.Close();
            }
        }

        protected void BindRouteAndTitle()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence;
                if (Request.QueryString["Id"] == null)
                {
                    SQLSentence = "select * from Yacht where Id = 1";
                }
                else
                {
                    SQLSentence = "select * from Yacht where Id = @Id";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
                }
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        LiteralRoute.Text = rd["Name"].ToString();
                        LiteralTitle.Text = rd["Name"].ToString();
                    }
                }
                connection.Close();
            }
        }

        protected void BindPanel()
        {
            if (Request.QueryString["Layout"] == "true")
            {
                PanelOverview.Visible = false;
                PanelLayout.Visible = true;
                PanelSpecification.Visible = false;
                BindRepeaterLayout();
            }
            else if (Request.QueryString["Specification"] == "true")
            {
                PanelOverview.Visible = false;
                PanelLayout.Visible = false;
                PanelSpecification.Visible = true;
                BindRepeaterSpecification();
            }
            else
            {
                PanelOverview.Visible = true;
                PanelLayout.Visible = false;
                PanelSpecification.Visible = false;
                BindOverviewContent();
                BindDimension();
            }
        }

        protected void BindOverviewContent()
        {
            if (Request.QueryString["Id"] != null || Request.QueryString["Overview"] == "True")
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select * from YachtOverview where YachtId = @YachtId";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@YachtId", Request.QueryString["Id"]);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    if (rd != null)
                    {
                        while (rd.Read())
                        {
                            LiteraOverviewContent.Text = HttpUtility.HtmlDecode(rd["OverviewContent"].ToString());
                            ImageDimension.ImageUrl = rd["DimensionPhotoPath"].ToString();
                        }
                    }
                    rd.Close();
                    rd = sqlCommand.ExecuteReader();
                    RepeaterDownload.DataSource = rd;
                    RepeaterDownload.DataBind();
                    connection.Close();
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select * from YachtOverview where YachtId = 1";   // 建立 SQL語句
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    if (rd != null)
                    {
                        while (rd.Read())
                        {
                            LiteraOverviewContent.Text = HttpUtility.HtmlDecode(rd["OverviewContent"].ToString());
                            ImageDimension.ImageUrl = rd["DimensionPhotoPath"].ToString();
                        }
                    }
                    rd.Close();
                    rd = sqlCommand.ExecuteReader();
                    RepeaterDownload.DataSource = rd;
                    RepeaterDownload.DataBind();
                    connection.Close();
                }
            }
        }

        protected void BindDimension()
        {
            if (Request.QueryString["Id"] != null || Request.QueryString["Overview"] == "True")
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select * from Yacht where Id = @Id";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    if (rd != null)
                    {
                        while (rd.Read())
                        {
                            string yacht = rd["Name"].ToString();
                            int indexOfSpace = yacht.IndexOf(" ");
                            LiteraDimensionTitle.Text = yacht.Substring(indexOfSpace + 1);
                        }
                    }
                    rd.Close();
                    SQLSentence = "select *, ROW_NUMBER() over (ORDER BY Id asc) as Rank from YachtDimension where YachtId = @YachtId";
                    sqlCommand.Parameters.AddWithValue("@YachtId", Request.QueryString["Id"]);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    RepeaterDimension.DataSource = rd;
                    RepeaterDimension.DataBind();
                    connection.Close();
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select * from Yacht where Id = 1";   // 建立 SQL語句
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    if (rd != null)
                    {
                        while (rd.Read())
                        {
                            string yacht = rd["Name"].ToString();
                            int indexOfSpace = yacht.IndexOf(" ");
                            LiteraDimensionTitle.Text = yacht.Substring(indexOfSpace + 1);
                        }
                    }
                    rd.Close();
                    SQLSentence = "select *, ROW_NUMBER() over (ORDER BY Id asc) as Rank from YachtDimension where YachtId = 1";
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    RepeaterDimension.DataSource = rd;
                    RepeaterDimension.DataBind();
                    rd.Close();
                    connection.Close();
                }
            }
        }

        protected void BindRepeaterLayout()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtLayout where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", Request.QueryString["Id"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    RepeaterLayout.DataSource = rd;
                    RepeaterLayout.DataBind();
                }
                connection.Close();
            }
        }

        protected void BindRepeaterSpecification()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtSpecificationType";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    RepeaterSpecification.DataSource = rd;
                    RepeaterSpecification.DataBind();
                }
                connection.Close();
            }
        }

        protected void RepeaterSpecification_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select * from YachtSpecificationContent where YachtId = @YachtId order by YachtSpecificationTypeId asc";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@YachtId", Request.QueryString["Id"]);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    Literal LiteralSpecificationTitle = e.Item.FindControl("LiteralSpecificationTitle") as Literal;
                    Label Label = e.Item.FindControl("Label1") as Label;

                    LiteralSpecificationTitle.Text = "<ul>";
                    while (rd.Read())
                    {
                        if (Label.Text == rd["YachtSpecificationTypeId"].ToString())
                        {
                            LiteralSpecificationTitle.Text += $"<li>{rd["Content"]}</li>";
                        }
                    }
                    LiteralSpecificationTitle.Text += "<ul>";

                    connection.Close();
                }
            }
        }

        protected void BindRepeaterGallery()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence;
                if (Request.QueryString["Id"] != null)
                {
                    SQLSentence = "select * from YachtGallery where YachtId = @YachtId";
                    sqlCommand.Parameters.AddWithValue("@YachtId", Request.QueryString["Id"]);
                }
                else
                {
                    SQLSentence = "select * from YachtGallery where YachtId = 1";
                }

                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    RepeaterGallery.DataSource = rd;
                    RepeaterGallery.DataBind();
                }
                connection.Close();
            }
        }
    }
}