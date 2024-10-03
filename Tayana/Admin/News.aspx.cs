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
    public partial class AddNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridViewNews();
            }
        }

        protected void BindGridViewNews()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from News";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewNews.DataSource = rd;
                    GridViewNews.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewNews_DataBound(object sender, EventArgs e)
        {
            if (GridViewNews.Rows.Count > 0)
            {
                // 遍歷每一個資料列進行 DropDownList 內容的綁定
                foreach (GridViewRow row in GridViewNews.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {   // 在這裡可以根據每一行的 Id 進行相應的資料繫結，例如從資料庫中取得相關資料並綁定到 DropDownList
                        string rowId = GridViewNews.DataKeys[row.RowIndex].Value.ToString();
                        DropDownList DropDownListNewsGallery = row.FindControl("DropDownListNewsGallery") as DropDownList;
                        if (DropDownListNewsGallery != null)
                        {
                            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                            {
                                connection.Open();
                                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                                string SQLSentence = "Select * from NewsGallery where NewsId = @NewsId";   // 建立 SQL語句
                                sqlCommand.Parameters.AddWithValue("@NewsId", rowId);
                                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    DropDownListNewsGallery.DataValueField = "Id";
                                    DropDownListNewsGallery.DataTextField = "Name";
                                    DropDownListNewsGallery.DataSource = dt;
                                    DropDownListNewsGallery.DataBind();
                                    DropDownListNewsGallery.Items.Add(new ListItem("選擇相片", "0"));
                                    DropDownListNewsGallery.SelectedValue = "0";
                                    ViewState["selectedPhoto"] = DropDownListNewsGallery.SelectedValue;
                                }
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }

        protected void GridViewNews_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewNews.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewNews();
        }

        protected void GridViewNews_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewNews.EditIndex = -1;  // 將資料行轉換為：編輯模式
            BindGridViewNews();
        }

        protected void GridViewNews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewNews.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNewsTitleTextBox = TargetRow.FindControl("TextBoxNewsTitle") as TextBox;
            TextBox UpdateNewsSubtitleTextBox = TargetRow.FindControl("TextBoxNewsSubtitle") as TextBox;
            TextBox UpdateNewsContentTextBox = TargetRow.FindControl("TextBoxNewsContent") as TextBox;
            CheckBox UpdateNewsIsTopCheckBox = TargetRow.FindControl("CheckBoxNewsIsTopEdit") as CheckBox;
            string isTop;
            if (UpdateNewsIsTopCheckBox.Checked)
            {
                isTop = "1";
            }
            else
            {
                isTop = "0";
            }

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewNews.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update News set Title = @Title, Subtitle = @Subtitle, Content = @Content, Is_Top = @Is_Top where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Title", UpdateNewsTitleTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Subtitle", UpdateNewsSubtitleTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Content", UpdateNewsContentTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Is_Top", isTop);
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewNews.EditIndex = -1;
            BindGridViewNews();
        }

        protected void GridViewNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewNews.DataKeys[IndexRow].Value.ToString();

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
            BindGridViewNews();
        }

        protected void GridViewNews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CoverPathUpdate")
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "update News set CoverPath =(select PhotoPath from NewsGallery where id = @Id) where id = @NewsId";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", ViewState["selectedPhoto"]);
                    sqlCommand.Parameters.AddWithValue("@NewsId", e.CommandArgument.ToString());
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
                    }
                    connection.Close();
                }
                BindGridViewNews();
            }
        }

        protected void GridViewNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlNewsGallery = e.Row.FindControl("DropDownListNewsGallery") as DropDownList;
                if (ddlNewsGallery != null)
                {
                    ddlNewsGallery.SelectedIndexChanged += new EventHandler(DropDownListNewsGallery_SelectedIndexChanged);
                }
            }
        }

        protected void DropDownListNewsGallery_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlNewsGallery = sender as DropDownList;
            if (ddlNewsGallery != null)
            {
                ViewState["selectedPhoto"] = ddlNewsGallery.SelectedValue;
            }
        }
    }
}