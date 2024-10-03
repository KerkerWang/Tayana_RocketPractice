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
    public partial class AddAgent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownListGridViewAgent();
            }
        }

        protected void BindDropDownListGridViewAgent()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Select * from Region";   // 建立 SQL語句
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownListRegion.DataValueField = "Id";
                DropDownListRegion.DataTextField = "Name";
                DropDownListRegion.DataSource = dt;
                DropDownListRegion.DataBind();
                DropDownListRegion.SelectedValue = DropDownListRegion.SelectedItem.Value;
                ViewState["SelectedRegionId"] = DropDownListRegion.SelectedValue;
            }
            connection.Close();
        }

        protected void ButtonAddAgent_Click(object sender, EventArgs e)
        {
            if (TextBoxAgentName.Text == "" ||
                TextBoxAgentContact.Text == "" ||
                TextBoxAgentAddress.Text == "" ||
                TextBoxAgentTel.Text == "" ||
                TextBoxAgentFax.Text == ""
                )
            {
                Response.Write("<script>alert('必填欄位請勿空白')</script>");
            }
            else
            {
                AddAgentToDB();
            }
        }

        protected void AddAgentToDB()
        {
            if (FileUpload1.HasFile)
            {
                // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
                string ServerFolderPath = Server.MapPath("~/AgentImg/");

                HttpPostedFile postFile = FileUpload1.PostedFile;
                // 4. 建立 單一檔案 篩選邏輯
                int FileMemory = postFile.ContentLength;  // 取得 單一檔案 容量變數
                string FileName = Path.GetFileName(postFile.FileName); // 取得 單一檔案 名稱變數
                string FileExtention = Path.GetExtension(postFile.FileName); // 取得 單一檔案 檔名變數
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
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "BEGIN TRANSACTION;"
                            + "INSERT INTO AgentImg (Name, Path) VALUES (@Name, @Path);"
                            + "DECLARE @A_Id INT;"
                            + "SET @A_Id = SCOPE_IDENTITY();"
                            + "INSERT INTO Agent (Name, Contact, Address, Tel, Fax, Email, Url, RegionId, Cell, AgentImgId) VALUES (@AgentName, @Contact, @Address, @Tel, @Fax, @Email, @Url, @RegionId, @Cell, @A_Id);"
                            + "COMMIT TRANSACTION;";
                    sqlCommand.Parameters.AddWithValue("@Name", FileName);
                    sqlCommand.Parameters.AddWithValue("@Path", PathStore);
                    sqlCommand.Parameters.AddWithValue("@AgentName", TextBoxAgentName.Text);
                    sqlCommand.Parameters.AddWithValue("@Contact", TextBoxAgentContact.Text);
                    sqlCommand.Parameters.AddWithValue("@Address", TextBoxAgentAddress.Text);
                    sqlCommand.Parameters.AddWithValue("@Tel", TextBoxAgentTel.Text);
                    sqlCommand.Parameters.AddWithValue("@Fax", TextBoxAgentFax.Text);
                    sqlCommand.Parameters.AddWithValue("@Email", TextBoxAgentEmail.Text);
                    sqlCommand.Parameters.AddWithValue("@Url", TextBoxAgentUrl.Text);
                    sqlCommand.Parameters.AddWithValue("@RegionId", ViewState["SelectedRegionId"]);
                    sqlCommand.Parameters.AddWithValue("@Cell", TextBoxAgentCell.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        FileUpload1.SaveAs(FilePath);  // 成功資料庫寫入後，將檔案存入指定資料夾路徑
                        Response.Write("<script>alert('新增成功')</script>");
                        TextBoxAgentName.Text = "";
                        TextBoxAgentContact.Text = "";
                        TextBoxAgentAddress.Text = "";
                        TextBoxAgentTel.Text = "";
                        TextBoxAgentFax.Text = "";
                        TextBoxAgentEmail.Text = "";
                        TextBoxAgentUrl.Text = "";
                        TextBoxAgentCell.Text = "";
                    }
                    connection.Close();
                }
            }
            else
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "INSERT INTO Agent (Name, Contact, Address, Tel, Fax, Email, Url, RegionId, Cell) VALUES (@AgentName, @Contact, @Address, @Tel, @Fax, @Email, @Url, @RegionId, @Cell);";
                sqlCommand.Parameters.AddWithValue("@AgentName", TextBoxAgentName.Text);
                sqlCommand.Parameters.AddWithValue("@Contact", TextBoxAgentContact.Text);
                sqlCommand.Parameters.AddWithValue("@Address", TextBoxAgentAddress.Text);
                sqlCommand.Parameters.AddWithValue("@Tel", TextBoxAgentTel.Text);
                sqlCommand.Parameters.AddWithValue("@Fax", TextBoxAgentFax.Text);
                sqlCommand.Parameters.AddWithValue("@Email", TextBoxAgentEmail.Text);
                sqlCommand.Parameters.AddWithValue("@Url", TextBoxAgentUrl.Text);
                sqlCommand.Parameters.AddWithValue("@RegionId", ViewState["SelectedRegionId"]);
                sqlCommand.Parameters.AddWithValue("@Cell", TextBoxAgentCell.Text);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                if (result != 0)
                {
                    Response.Write("<script>alert('新增成功')</script>");
                    TextBoxAgentName.Text = "";
                    TextBoxAgentContact.Text = "";
                    TextBoxAgentAddress.Text = "";
                    TextBoxAgentTel.Text = "";
                    TextBoxAgentFax.Text = "";
                    TextBoxAgentEmail.Text = "";
                    TextBoxAgentUrl.Text = "";
                    TextBoxAgentCell.Text = "";
                }
                connection.Close();
            }
        }

        protected void DropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedRegionId"] = DropDownListRegion.SelectedValue;
        }
    }
}