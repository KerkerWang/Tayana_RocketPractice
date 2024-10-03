﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana
{
    public partial class Company : System.Web.UI.Page
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
            string SQLSentence = $"SELECT * FROM CompanyPage";   // 建立 SQL語句
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
                SQLSentence = "select * from companypage where Id = 1";
            }
            else
            {
                SQLSentence = "select * from companypage where Id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    LiteralTitle.Text = rd["Name"].ToString();
                    LiteralRoute.Text = rd["Name"].ToString();                   
                    LiteralContent.Text = HttpUtility.HtmlDecode(rd["HtmlContent"].ToString());
                }
            }
            rd.Close();
            connection.Close();
        }
    }
}