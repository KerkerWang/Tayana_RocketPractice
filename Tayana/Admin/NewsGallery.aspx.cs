using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.Admin
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTitle();
                if (Literalh2.Text == "")
                {
                    Response.Redirect("News.aspx");
                }
                BindGridViewNewsGallery();
            }
        }

        protected void BindTitle()
        {
            if (Request.QueryString["Id"] == null)
            {
                Response.Redirect("News.aspx");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "select Title from News where Id = @Id";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", Request.QueryString["Id"]);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                    if (rd != null)
                    {
                        while (rd.Read())
                        {
                            Literalh2.Text = $"<h2>News標題：{rd["Title"]}</h2>";
                        }
                    }
                    else
                    {
                        Response.Redirect("News.aspx");
                    }
                    connection.Close();
                }
            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
            string ServerFolderPath = Server.MapPath("~/NewsGalleryImg/");
            if (FileUploadGallery.HasFiles)
            {
                // 3. 將 FileUpload 控制項裡面的檔案跑迴圈
                foreach (var postfile in FileUploadGallery.PostedFiles)
                {
                    // 4. 建立 單一檔案 篩選邏輯
                    int FileMemory = postfile.ContentLength;  // 取得 單一檔案 容量變數
                    string FileName = Path.GetFileName(postfile.FileName); // 取得 單一檔案 名稱變數
                    string FileExtention = Path.GetExtension(postfile.FileName); // 取得 單一檔案 檔名變數
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
                        string PathStore = "/NewsGalleryImg/" + FileName;   // Note：請參考HTML的圖片相對路徑
                        using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                        {
                            connection.Open();
                            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                            string SQLSentence = "Insert into NewsGallery (NewsId, Name, PhotoPath) values(@NewsId, @Name, @PhotoPath)";
                            sqlCommand.Parameters.AddWithValue("@NewsId", Request.QueryString["Id"]);
                            sqlCommand.Parameters.AddWithValue("@Name", FileName);
                            sqlCommand.Parameters.AddWithValue("@PhotoPath", PathStore);
                            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                            int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                            if (result != 0)
                            {
                                postfile.SaveAs(FilePath);  // 成功資料庫寫入後，將檔案存入指定資料夾路徑
                            }
                            connection.Close();
                        }
                    }
                }
                Response.Write("<script>alert('上傳成功')</script>");
                BindGridViewNewsGallery();
            }
            else
            {
                Response.Write("<script>alert('請選擇圖片')</script>");
            }
            BindGridViewNewsGallery();
        }

        protected void BindGridViewNewsGallery()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from NewsGallery where NewsId = @NewsId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@NewsId", Request.QueryString["Id"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewNewsGallery.DataSource = rd;
                    GridViewNewsGallery.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewNewsGallery_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewNewsGallery.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewNewsGallery();
        }

        protected void GridViewNewsGallery_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewNewsGallery.EditIndex = -1;  // 將資料行轉換為：編輯模式
            BindGridViewNewsGallery();
        }

        protected void GridViewNewsGallery_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewNewsGallery.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNewsGalleryNameTextBox = TargetRow.FindControl("TextBoxNewsGalleryName") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewNewsGallery.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update NewsGallery set Name = @Name where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Name", UpdateNewsGalleryNameTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewNewsGallery.EditIndex = -1;
            BindGridViewNewsGallery();
        }

        protected void GridViewNewsGallery_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewNewsGallery.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from News where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewNewsGallery();
        }
    }
}