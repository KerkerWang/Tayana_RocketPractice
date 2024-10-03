using CKFinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.Admin
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildCKEditor();
                BindDropDownListCompanyPage();
                BindCKEditorContent();
            }
        }

        protected void BuildCKEditor()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl1);
        }

        protected void BindDropDownListCompanyPage()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Select * from CompanyPage";   // 建立 SQL語句
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownListCompanyPage.DataValueField = "Id";
                DropDownListCompanyPage.DataTextField = "Name";
                DropDownListCompanyPage.DataSource = dt;
                DropDownListCompanyPage.DataBind();
                DropDownListCompanyPage.SelectedValue = DropDownListCompanyPage.SelectedItem.Value;
                ViewState["SelectedPage"] = DropDownListCompanyPage.SelectedValue;
            }
            connection.Close();
        }

        protected void BindCKEditorContent()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Select * from CompanyPage where Id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", ViewState["SelectedPage"]);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    CKEditorControl1.Text = rd["HtmlContent"].ToString();
                }
            }
            connection.Close();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = $"Update CompanyPage set HtmlContent = @HtmlContent where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@HtmlContent", HttpUtility.HtmlEncode(CKEditorControl1.Text));
            sqlCommand.Parameters.AddWithValue("@Id", ViewState["SelectedPage"]);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
            if (result != 0)
            {
                Response.Write("<script>alert('修改內容成功')</script>");
            }
            connection.Close();
        }

        protected void DropDownListCompanyPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedPage"] = DropDownListCompanyPage.SelectedValue;
            BindCKEditorContent();
        }
    }
}