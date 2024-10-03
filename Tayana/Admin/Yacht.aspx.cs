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
    public partial class CreateYacht : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridViewYacht();
            }
        }

        protected void BindGridViewYacht()
        {
            using (var connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = $"SELECT * FROM Yacht";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewYacht.DataSource = rd;
                    GridViewYacht.DataBind();
                }
                connection.Close();
            }
        }

        protected void ButtonAddYacht_Click(object sender, EventArgs e)
        {
            AddNewYachtToDB();
            BindGridViewYacht();
        }

        protected void AddNewYachtToDB()
        {
            if (TextBoxNewYacht.Text != "")
            {
                using (var connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "Insert into yacht (Name) values (@Name)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Name", TextBoxNewYacht.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        Response.Write("<script>alert('新增成功')</script>");
                        TextBoxNewYacht.Text = "";
                    }
                    connection.Close();
                }
            }
        }

        protected void GridViewYacht_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewYacht.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewYacht();
        }

        protected void GridViewYacht_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewYacht.EditIndex = -1;  // 將資料行轉換為：編輯模式
            BindGridViewYacht();
        }

        protected void GridViewYacht_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewYacht.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNameTextBox = TargetRow.FindControl("TextBoxYachtName") as TextBox;
            CheckBox UpdateIsNewDesignedEditCheckBox = TargetRow.FindControl("CheckBoxIsNewDesignedEdit") as CheckBox;
            CheckBox UpdateIsNewBuiltEditCheckBox = TargetRow.FindControl("CheckBoxIsNewBuiltEdit") as CheckBox;
            int bitOfUpdateIsNewDesignedCheckBox;
            if (UpdateIsNewDesignedEditCheckBox.Checked)
            {
                bitOfUpdateIsNewDesignedCheckBox = 1;
            }
            else
            {
                bitOfUpdateIsNewDesignedCheckBox = 0;
            }
            int bitOfUpdateIsNewBuiltCheckBox;
            if (UpdateIsNewBuiltEditCheckBox.Checked)
            {
                bitOfUpdateIsNewBuiltCheckBox = 1;
            }
            else
            {
                bitOfUpdateIsNewBuiltCheckBox = 0;
            }
            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewYacht.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Update yacht set Name = @Name, Is_NewDesigned = @Is_NewDesigned, Is_NewBuilt = @Is_NewBuilt where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.Parameters.AddWithValue("@Name", UpdateNameTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@Is_NewDesigned", bitOfUpdateIsNewDesignedCheckBox);
                sqlCommand.Parameters.AddWithValue("@Is_NewBuilt", bitOfUpdateIsNewBuiltCheckBox);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
                }
                connection.Close();
            }

            GridViewYacht.EditIndex = -1;
            BindGridViewYacht();
        }

        protected void GridViewYacht_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewYacht.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from Yacht where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result == 1)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewYacht();
        }

        protected void GridViewYacht_DataBound(object sender, EventArgs e)
        {
            if (GridViewYacht.Rows.Count > 0)
            {
                // 遍歷每一個資料列進行 DropDownList 內容的綁定
                foreach (GridViewRow row in GridViewYacht.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {   // 在這裡可以根據每一行的 Id 進行相應的資料繫結，例如從資料庫中取得相關資料並綁定到 DropDownList
                        string rowId = GridViewYacht.DataKeys[row.RowIndex].Value.ToString();
                        DropDownList DropDownListYachtGallery = row.FindControl("DropDownListYachtGallery") as DropDownList;
                        if (DropDownListYachtGallery != null)
                        {
                            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                            {
                                connection.Open();
                                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                                string SQLSentence = "Select * from YachtGallery where YachtId = @YachtId";   // 建立 SQL語句
                                sqlCommand.Parameters.AddWithValue("@YachtId", rowId);
                                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    DropDownListYachtGallery.DataValueField = "Id";
                                    DropDownListYachtGallery.DataTextField = "Name";
                                    DropDownListYachtGallery.DataSource = dt;
                                    DropDownListYachtGallery.DataBind();
                                    DropDownListYachtGallery.Items.Add(new ListItem("選擇相片", "0"));
                                    DropDownListYachtGallery.SelectedValue = "0";
                                    ViewState["selectedPhoto"] = DropDownListYachtGallery.SelectedValue;
                                }
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }

        protected void GridViewYacht_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CoverPathUpdate")
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence = "update Yacht set CoverPath =(select PhotoPath from YachtGallery where id = @Id) where id = @YachtId";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", ViewState["selectedPhoto"]);
                    sqlCommand.Parameters.AddWithValue("@YachtId", e.CommandArgument.ToString());
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
                    }
                    connection.Close();
                }
                BindGridViewYacht();
            }
        }

        protected void GridViewYacht_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlYachtGallery = e.Row.FindControl("DropDownListYachtGallery") as DropDownList;
                if (ddlYachtGallery != null)
                {
                    ddlYachtGallery.SelectedIndexChanged += new EventHandler(DropDownListYachtGallery_SelectedIndexChanged);
                }
            }
        }

        protected void DropDownListYachtGallery_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlYachtGallery = sender as DropDownList;
            if (ddlYachtGallery != null)
            {
                ViewState["selectedPhoto"] = ddlYachtGallery.SelectedValue;
            }
        }
    }
}