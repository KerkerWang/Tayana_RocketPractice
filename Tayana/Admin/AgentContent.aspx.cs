using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.Admin
{
    public partial class AgentContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] == null)
            {
                Response.Redirect("Dealers.aspx");
            }
            if (!IsPostBack)
            {
                BindDropDownListAgentImg();
                BindAgentContent();
            }
        }

        protected void BindAgentContent()
        {
            using (var connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = $"SELECT * FROM Agent where Id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        ImageAgent.ImageUrl = rd["CoverPath"].ToString();
                        TextBoxAgentName.Text = rd["Name"].ToString();
                        TextBoxAgentContact.Text = rd["Contact"].ToString();
                        TextBoxAgentAddress.Text = rd["Address"].ToString();
                        TextBoxAgentTel.Text = rd["Tel"].ToString();
                        TextBoxAgentCell.Text = rd["Cell"].ToString();
                        TextBoxAgentFax.Text = rd["Fax"].ToString();
                        TextBoxAgentEmail.Text = rd["Email"].ToString();
                        TextBoxAgentUrl.Text = rd["Url"].ToString();
                    }
                }
                connection.Close();
            }
        }

        protected void BindDropDownListAgentImg()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Select * from AgentImg";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DropDownListAgentImg.DataValueField = "Id";
                    DropDownListAgentImg.DataTextField = "Name";
                    DropDownListAgentImg.DataSource = dt;
                    DropDownListAgentImg.DataBind();
                    DropDownListAgentImg.Items.Add(new ListItem("選擇相片", "0"));
                    DropDownListAgentImg.SelectedValue = "0";
                    ViewState["selectedPhoto"] = DropDownListAgentImg.SelectedValue;
                }
                connection.Close();
            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
            string ServerFolderPath = Server.MapPath("~/AgentImg/");
            if (FileUpload1.HasFile)
            {
                // 4. 建立 單一檔案 篩選邏輯
                int FileMemory = FileUpload1.PostedFile.ContentLength;  // 取得 單一檔案 容量變數
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName); // 取得 單一檔案 名稱變數
                string FileExtention = Path.GetExtension(FileUpload1.PostedFile.FileName); // 取得 單一檔案 檔名變數
                string FilePath = Path.Combine(ServerFolderPath, FileName);  // 取得 單一檔案 儲存路徑
                if (FileMemory > 1000000)           // 4-1. 如果 單一檔案 大於 1M，跳過不存
                {
                    Response.Write("<script>alert('檔案容量超過1MB，請重新選擇')</script>");
                }
                else if (FileExtention != ".jpg")   // 4-2. 如果 單一檔案 不是".jpg"檔名 跳過不存
                {
                    Response.Write("<script>alert('請選擇jpg格式的圖片')</script>");
                }
                else
                {
                    string PathStore = "/AgentImg/" + FileName;   // Note：請參考HTML的圖片相對路徑
                    using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                        sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                        string SQLSentence = "Insert into AgentImg (Name, Path) values(@Name, @Path)";
                        sqlCommand.Parameters.AddWithValue("@Name", FileName);
                        sqlCommand.Parameters.AddWithValue("@Path", PathStore);
                        sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                        int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                        if (result != 0)
                        {
                            FileUpload1.PostedFile.SaveAs(FilePath);  // 成功資料庫寫入後，將檔案存入指定資料夾路徑
                        }
                        connection.Close();
                    }
                }

                Response.Write("<script>alert('上傳成功')</script>");
                BindDropDownListAgentImg();
            }
            else
            {
                Response.Write("<script>alert('請選擇圖片')</script>");
            }
        }

        protected void ButtonChangeAgentImg_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "update Agent set CoverPath =(select Path from AgentImg where id = @Id) where id = @AgentId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", ViewState["selectedPhoto"]);
                sqlCommand.Parameters.AddWithValue("@AgentId", Request.QueryString["Id"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindAgentContent();
        }

        protected void DropDownListAgentImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["selectedPhoto"] = DropDownListAgentImg.SelectedValue;
        }

        protected void ButtonUpdateAgent_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Update Agent set Name = @Name, Contact = @Contact, Address = @Address, Tel = @Tel, Cell = @Cell, Fax = @Fax, Email = @Email, Url = @Url where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
                sqlCommand.Parameters.AddWithValue("@Name", TextBoxAgentName.Text);
                sqlCommand.Parameters.AddWithValue("@Contact", TextBoxAgentContact.Text);
                sqlCommand.Parameters.AddWithValue("@Address", TextBoxAgentAddress.Text);
                sqlCommand.Parameters.AddWithValue("@Tel", TextBoxAgentTel.Text);
                sqlCommand.Parameters.AddWithValue("@Cell", TextBoxAgentCell.Text);
                sqlCommand.Parameters.AddWithValue("@Fax", TextBoxAgentFax.Text);
                sqlCommand.Parameters.AddWithValue("@Email", TextBoxAgentEmail.Text);
                sqlCommand.Parameters.AddWithValue("@Url", TextBoxAgentUrl.Text);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindAgentContent();
        }
    }
}