using CKEditor.NET;
using CKFinder;
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
    public partial class YachtContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYachtTypeButton();
            }
        }

        protected void BindYachtTypeButton()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from Yacht";
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd.HasRows)
                {
                    RepeaterYachtTypeButtons.DataSource = rd;
                    RepeaterYachtTypeButtons.DataBind();
                }

                connection.Close();
            }
        }

        protected void RepeaterYachtTypeButtons_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChooseYacht")
            {
                ViewState["ChosenYachtId"] = e.CommandArgument;
                Button ButtonChooseYacht = e.Item.FindControl("ButtonChooseYacht") as Button;
                LiteralTitle.Text = $"<h1>現正編輯型號：{ButtonChooseYacht.Text}</h1>";
                ShowDropDownList();
                BindPanel();
            }
        }

        protected void ShowDropDownList()
        {
            if (Request.QueryString["id"] != null)
            {
                DropDownListYachtContentType.Visible = true;
                DropDownListYachtContentType.SelectedValue = "1";
                ViewState["SelectedType"] = DropDownListYachtContentType.SelectedValue;
                BindCKEditor();
                BindPanel();
            }
        }

        protected void DropDownListYachtContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedType"] = DropDownListYachtContentType.SelectedValue;
            BindPanel();
            BindCKEditor();
        }

        protected void BindPanel()
        {
            switch (Convert.ToInt32(ViewState["SelectedType"]))
            {
                case 1:
                    BuildCKEditor();
                    BindCKEditor();
                    PanelOverview.Visible = true;
                    PanelDimension.Visible = false;
                    PanelSpecification.Visible = false;
                    PanelUpload.Visible = false;
                    break;

                case 2:
                    BindGridViewDimension();
                    PanelOverview.Visible = false;
                    PanelDimension.Visible = true;
                    PanelSpecification.Visible = false;
                    PanelUpload.Visible = false;
                    break;

                case 3:
                    BindGridViewSpecificationType();
                    BindGridViewSpecificationItem();
                    PanelOverview.Visible = false;
                    PanelDimension.Visible = false;
                    PanelSpecification.Visible = true;
                    PanelUpload.Visible = false;
                    break;

                case 4:
                    BindGridViewDimensionPhoto();
                    BindGridViewDownload();
                    BindGridViewLayoutPhoto();
                    BindGridViewGalleryPhoto();
                    PanelOverview.Visible = false;
                    PanelDimension.Visible = false;
                    PanelSpecification.Visible = false;
                    PanelUpload.Visible = true;
                    break;
            }
        }

        #region About_PanelOverview_CKEditor

        protected void BuildCKEditor()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControlOverview);
        }

        protected void BindCKEditor()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Select Id, OverviewContent from YachtOverview where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        CKEditorControlOverview.Text = rd["OverviewContent"].ToString();
                        ViewState["OverviewContentId"] = rd["Id"].ToString();
                    }
                }
                else
                {
                    CKEditorControlOverview.Text = "";
                    ViewState["OverviewContentId"] = "";
                }
                connection.Close();
            }
        }

        protected void ButtonOverviewSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence;
                if (ViewState["OverviewContentId"] == null || ViewState["OverviewContentId"].ToString() == "")
                {
                    SQLSentence = "Insert into YachtOverview (OverviewContent, YachtId) values(@OverviewContent, @YachtId)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                }
                else
                {
                    SQLSentence = "Update YachtOverview set OverviewContent = @OverviewContent where Id = @Id";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Id", ViewState["OverviewContentId"]);
                }
                sqlCommand.Parameters.AddWithValue("@OverviewContent", HttpUtility.HtmlEncode(CKEditorControlOverview.Text));
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                if (result != 0)
                {
                    Response.Write("<script>alert('儲存成功')</script>");
                }
                connection.Close();
            }
            BindCKEditor();
        }

        #endregion About_PanelOverview_CKEditor

        #region About_PanelDimension_AddDimension_GridViewDimension

        protected void ButtonAddDimension_Click(object sender, EventArgs e)
        {
            if (TextBoxDimensionKey.Text == "" || TextBoxDimensionValue.Text == "")
            {
                Response.Write("<script>alert('欄位不得空白')</script>");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence;
                    SQLSentence = "Insert into YachtDimension (YachtId, DimensionKey, DimensionValue) values(@YachtId, @DimensionKey, @DimensionValue)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                    sqlCommand.Parameters.AddWithValue("@DimensionKey", TextBoxDimensionKey.Text);
                    sqlCommand.Parameters.AddWithValue("@DimensionValue", TextBoxDimensionValue.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        Response.Write("<script>alert('新增成功')</script>");
                    }
                    connection.Close();
                    TextBoxDimensionKey.Text = "";
                    TextBoxDimensionValue.Text = "";
                }
                BindGridViewDimension();
            }
        }

        protected void BindGridViewDimension()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtDimension where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewDimension.DataSource = rd;
                    GridViewDimension.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewDimension_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDimension.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewDimension();
        }

        protected void GridViewDimension_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewDimension.EditIndex = -1;
            BindGridViewDimension();
        }

        protected void GridViewDimension_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewDimension.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateDimensionKeyTextBox = TargetRow.FindControl("TextBoxDimensionKey") as TextBox;
            TextBox UpdateDimensionValueTextBox = TargetRow.FindControl("TextBoxDimensionValue") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewDimension.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update YachtDimension set DimensionKey = @DimensionKey, DimensionValue = @DimensionValue where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@DimensionKey", UpdateDimensionKeyTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@DimensionValue", UpdateDimensionValueTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewDimension.EditIndex = -1;
            BindGridViewDimension();
        }

        protected void GridViewDimension_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewDimension.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from YachtDimension where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewDimension();
        }

        #endregion About_PanelDimension_AddDimension_GridViewDimension

        #region About_PanelSpecification_AddSpecificationType_GridViewSpecificationType

        protected void ButtonSpecificationTypeAdd_Click(object sender, EventArgs e)
        {
            if (TextBoxSpecificationType.Text == "")
            {
                Response.Write("<script>alert('欄位不得空白')</script>");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence;
                    SQLSentence = "Insert into YachtSpecificationType (Name) values(@Name)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Name", TextBoxSpecificationType.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        Response.Write("<script>alert('新增成功')</script>");
                    }
                    connection.Close();
                    TextBoxSpecificationType.Text = "";
                }
                BindGridViewSpecificationType();
            }
        }

        protected void BindGridViewSpecificationType()
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
                    GridViewSpecificationType.DataSource = rd;
                    GridViewSpecificationType.DataBind();
                }
                rd.Close();
                BindDropDownListSpecificationItem(sqlCommand);
                connection.Close();
            }
        }

        protected void GridViewSpecificationType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSpecificationType.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewSpecificationType();
        }

        protected void GridViewSpecificationType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSpecificationType.EditIndex = -1;
            BindGridViewSpecificationType();
        }

        protected void GridViewSpecificationType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewSpecificationType.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateSpecificationTypeNameTextBox = TargetRow.FindControl("TextBoxSpecificationTypeName") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewSpecificationType.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update YachtSpecificationType set Name = @Name where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@Name", UpdateSpecificationTypeNameTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewSpecificationType.EditIndex = -1;
            BindGridViewSpecificationType();
        }

        protected void GridViewSpecificationType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewSpecificationType.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from YachtSpecificationType where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewSpecificationType();
        }

        #endregion About_PanelSpecification_AddSpecificationType_GridViewSpecificationType

        #region About_PanelSpecification_DropDownListSpecificationItem_AddSpecificationItem_GridViewSpecificationItem

        protected void BindDropDownListSpecificationItem(SqlCommand sqlCommand)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownListSpecificationItem.DataValueField = "Id";
                DropDownListSpecificationItem.DataTextField = "Name";
                DropDownListSpecificationItem.DataSource = dt;
                DropDownListSpecificationItem.DataBind();
                DropDownListSpecificationItem.SelectedValue = DropDownListSpecificationItem.SelectedItem.Value;
                ViewState["SelectedSpecificationTypeId"] = DropDownListSpecificationItem.SelectedValue;
            }
        }

        protected void DropDownListSpecificationItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedSpecificationTypeId"] = DropDownListSpecificationItem.SelectedValue;
        }

        protected void ButtonSpecificationItemAdd_Click(object sender, EventArgs e)
        {
            if (TextBoxSpecificationItem.Text == "")
            {
                Response.Write("<script>alert('欄位不得空白')</script>");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence;
                    SQLSentence = "Insert into YachtSpecificationContent (YachtId, YachtSpecificationTypeId, Content) values(@YachtId, @YachtSpecificationTypeId, @Content)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                    sqlCommand.Parameters.AddWithValue("@YachtSpecificationTypeId", ViewState["SelectedSpecificationTypeId"]);
                    sqlCommand.Parameters.AddWithValue("@Content", TextBoxSpecificationItem.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        Response.Write("<script>alert('新增成功')</script>");
                    }
                    connection.Close();
                    TextBoxSpecificationItem.Text = "";
                    BindGridViewSpecificationItem();
                }
            }
        }

        protected void BindGridViewSpecificationItem()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select ysc.Id, yst.Name, ysc.Content from YachtSpecificationType as yst left join YachtSpecificationContent as ysc on yst.Id = ysc.YachtSpecificationTypeId where ysc.YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewSpecificationItem.DataSource = rd;
                    GridViewSpecificationItem.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewSpecificationItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSpecificationItem.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewSpecificationItem();
        }

        protected void GridViewSpecificationItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSpecificationItem.EditIndex = -1;  // 將資料行轉換為：編輯模式
            BindGridViewSpecificationItem();
        }

        protected void GridViewSpecificationItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewSpecificationItem.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateSpecificationContentTextBox = TargetRow.FindControl("TextBoxSpecificationContent") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewSpecificationItem.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update YachtSpecificationContent set Content = @Content where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@Content", UpdateSpecificationContentTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewSpecificationItem.EditIndex = -1;
            BindGridViewSpecificationItem();
        }

        protected void GridViewSpecificationItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewSpecificationItem.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from YachtSpecificationContent where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewSpecificationItem();
        }

        #endregion About_PanelSpecification_DropDownListSpecificationItem_AddSpecificationItem_GridViewSpecificationItem

        #region About_PanelUpload_UploadDimensionPhoto_GridViewDimensionPhoto_UploadDownload_GridViewDownload

        protected void ButtonUploadDimensionPhoto_Click(object sender, EventArgs e)
        {
            if (FileUploadDimensionPhoto.HasFile)
            {
                // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
                string ServerFolderPath = Server.MapPath("~/OverviewFile/");

                HttpPostedFile postFile = FileUploadDimensionPhoto.PostedFile;

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
                    string PathStore = "/OverviewFile/" + FileName;   // Note：請參考HTML的圖片相對路徑
                    using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                        sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                        string SQLSentence = "Update YachtOverview set DimensionPhotoPath = @DimensionPhotoPath where Id = @Id";
                        sqlCommand.Parameters.AddWithValue("@Id", ViewState["OverviewContentId"]);
                        sqlCommand.Parameters.AddWithValue("@DimensionPhotoPath", PathStore);
                        sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                        int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                        if (result != 0)
                        {
                            FileUploadDimensionPhoto.SaveAs(FilePath);  // 成功資料庫寫入後，將檔案存入指定資料夾路徑
                            Response.Write("<script>alert('上傳成功')</script>");
                        }
                        connection.Close();
                    }
                    BindGridViewDimensionPhoto();
                }
            }
            else
            {
                Response.Write("<script>alert('請選擇圖片')</script>");
            }
        }

        protected void BindGridViewDimensionPhoto()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtOverview where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewDimensionPhoto.DataSource = rd;
                    GridViewDimensionPhoto.DataBind();
                    while (rd.Read())
                    {
                        ViewState["OverviewContentId"] = rd["Id"].ToString();
                    }
                }
                connection.Close();
            }
        }

        protected void ButtonUploadDownload_Click(object sender, EventArgs e)
        {
            if (FileUploadDownload.HasFile)
            {
                // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
                string ServerFolderPath = Server.MapPath("~/OverviewFile/");

                HttpPostedFile postFile = FileUploadDownload.PostedFile;

                // 4. 建立 單一檔案 篩選邏輯
                int FileMemory = postFile.ContentLength;  // 取得 單一檔案 容量變數
                string FileName = Path.GetFileName(postFile.FileName); // 取得 單一檔案 名稱變數
                string FileExtention = Path.GetExtension(postFile.FileName); // 取得 單一檔案 檔名變數
                string FilePath = Path.Combine(ServerFolderPath, FileName);  // 取得 單一檔案 儲存路徑
                if (FileMemory > 1000000)           // 4-1. 如果 單一檔案 大於 1M，跳過不存
                {
                    Response.Write("<script>alert('檔案容量超過1MB，請重新選擇')</script>");
                }
                else if (FileExtention != ".pdf")   // 4-2. 如果 單一檔案 不是".jpg"檔名 跳過不存
                {
                    Response.Write("<script>alert('請選擇pdf格式的檔案')</script>");
                }
                else
                {
                    string PathStore = "/OverviewFile/" + FileName;   // Note：請參考HTML的圖片相對路徑
                    using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                        sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                        string SQLSentence = "Update YachtOverview set DownloadFileName = @DownloadFileName, DownloadFilePath = @DownloadFilePath where Id = @Id";
                        sqlCommand.Parameters.AddWithValue("@Id", ViewState["OverviewContentId"]);
                        sqlCommand.Parameters.AddWithValue("@DownloadFileName", FileName);
                        sqlCommand.Parameters.AddWithValue("@DownloadFilePath", PathStore);
                        sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                        int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                        if (result != 0)
                        {
                            FileUploadDownload.SaveAs(FilePath);  // 成功資料庫寫入後，將檔案存入指定資料夾路徑
                            Response.Write("<script>alert('上傳成功')</script>");
                        }
                        connection.Close();
                    }
                    BindGridViewDownload();
                }
            }
            else
            {
                Response.Write("<script>alert('請選擇檔案')</script>");
            }
        }

        protected void BindGridViewDownload()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtOverview where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewDownload.DataSource = rd;
                    GridViewDownload.DataBind();
                    while (rd.Read())
                    {
                        ViewState["OverviewContentId"] = rd["Id"].ToString();
                    }
                }
                connection.Close();
            }
        }

        protected void GridViewDownload_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDownload.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewDownload();
        }

        protected void GridViewDownload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewDownload.EditIndex = -1;  // 將資料行轉換為：編輯模式
            BindGridViewDownload();
        }

        protected void GridViewDownload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewDownload.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateDownloadFileNameTextBox = TargetRow.FindControl("TextBoxDownloadFileName") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewDownload.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update YachtOverview set DownloadFileName = @DownloadFileName where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@DownloadFileName", UpdateDownloadFileNameTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewDownload.EditIndex = -1;
            BindGridViewDownload();
        }

        #endregion About_PanelUpload_UploadDimensionPhoto_GridViewDimensionPhoto_UploadDownload_GridViewDownload

        protected void ButtonUploadLayoutPhoto_Click(object sender, EventArgs e)
        {// 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
            string ServerFolderPath = Server.MapPath("~/DimensionImg/");
            if (FileUploadLayoutPhoto.HasFiles)
            {
                // 3. 將 FileUpload 控制項裡面的檔案跑迴圈
                foreach (var postfile in FileUploadLayoutPhoto.PostedFiles)
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
                        string PathStore = "/DimensionImg/" + FileName;   // Note：請參考HTML的圖片相對路徑
                        using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                        {
                            connection.Open();
                            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                            string SQLSentence = "Insert into YachtLayout (YachtId, Name, PhotoPath) values(@YachtId, @Name, @PhotoPath)";
                            sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
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
                BindGridViewLayoutPhoto();
            }
            else
            {
                Response.Write("<script>alert('請選擇圖片')</script>");
            }
        }

        protected void BindGridViewLayoutPhoto()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtLayout where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewLayoutPhoto.DataSource = rd;
                    GridViewLayoutPhoto.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewLayoutPhoto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewLayoutPhoto.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from YachtLayout where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewLayoutPhoto();
        }

        protected void ButtonUploadGalleryPhoto_Click(object sender, EventArgs e)
        {
            // 1. 取的Server資料夾路徑 (記得要先去 建立資料夾)
            string ServerFolderPath = Server.MapPath("~/GalleryImg/");
            if (FileUploadGalleryPhoto.HasFiles)
            {
                // 3. 將 FileUpload 控制項裡面的檔案跑迴圈
                foreach (var postfile in FileUploadGalleryPhoto.PostedFiles)
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
                        string PathStore = "/GalleryImg/" + FileName;   // Note：請參考HTML的圖片相對路徑
                        using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                        {
                            connection.Open();
                            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                            string SQLSentence = "Insert into YachtGallery (YachtId, Name, PhotoPath) values(@YachtId, @Name, @PhotoPath)";
                            sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
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
                BindGridViewLayoutPhoto();
            }
            else
            {
                Response.Write("<script>alert('請選擇圖片')</script>");
            }
            BindGridViewGalleryPhoto();
        }

        protected void BindGridViewGalleryPhoto()
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "select * from YachtGallery where YachtId = @YachtId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@YachtId", ViewState["ChosenYachtId"]);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    GridViewGalleryPhoto.DataSource = rd;
                    GridViewGalleryPhoto.DataBind();
                }
                connection.Close();
            }
        }

        protected void GridViewGalleryPhoto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewGalleryPhoto.DataKeys[IndexRow].Value.ToString();

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = "Delete from YachtGallery where id = @Id";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@Id", IDkey);
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
                }
                connection.Close();
            }
            BindGridViewGalleryPhoto();
        }
    }
}