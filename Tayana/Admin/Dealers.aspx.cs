using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.Admin
{
    public partial class Dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["content"] = "Country";
                PanelCountry.Visible = true;
                BindGridViewCountry();
            }
        }

        protected void ButtonCountry_Click(object sender, EventArgs e)
        {
            ViewState["content"] = "Country";
            PanelCountry.Visible = true;
            PanelRegion.Visible = false;
            PanelAgent.Visible = false;
            BindGridViewCountry();
        }

        protected void ButtonRegion_Click(object sender, EventArgs e)
        {
            ViewState["content"] = "Region";
            BindDropDownListGridViewRegion();
            PanelRegion.Visible = true;
            PanelCountry.Visible = false;
            PanelAgent.Visible = false;
            BindGridViewRegion();
        }

        protected void ButtonAgent_Click(object sender, EventArgs e)
        {
            ViewState["content"] = "Agent";
            BindDropDownListGridViewAgent();
            BindGridViewAgent();
            PanelAgent.Visible = true;
            PanelRegion.Visible = false;
            PanelCountry.Visible = false;
        }

        protected void ButtonAddCountry_Click(object sender, EventArgs e)
        {
            if (TextBoxCountry.Text == "")
            {
                Response.Write("<script>alert('欄位請勿空白')</script>");
            }
            else
            {
                AddCountryToDB();
            }
        }

        protected void AddCountryToDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = $"Insert into country (Name) values(@Name)";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Name", TextBoxCountry.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
            if (result == 1)
            {
                Response.Write("<script>alert('新增成功')</script>");
                TextBoxCountry.Text = "";
            }
            connection.Close();
            BindGridViewCountry();
        }

        protected void BindGridViewCountry()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = $"SELECT * FROM Country";   // 建立 SQL語句
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd != null)
            {
                GridViewCountry.DataSource = rd;
                GridViewCountry.DataBind();
            }
            connection.Close();
        }

        protected void ButtonAddRegion_Click(object sender, EventArgs e)
        {
            if (TextBoxRegion.Text == "")
            {
                Response.Write("<script>alert('欄位請勿空白')</script>");
            }
            else
            {
                AddRegionToDB();
            }
        }

        protected void BindDropDownListGridViewRegion()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Select * from Country";   // 建立 SQL語句
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownListCountry.DataValueField = "Id";
                DropDownListCountry.DataTextField = "Name";
                DropDownListCountry.DataSource = dt;
                DropDownListCountry.DataBind();
                DropDownListCountry.SelectedValue = DropDownListCountry.SelectedItem.Value;
                ViewState["SelectedRegion"] = DropDownListCountry.SelectedValue;

                DropDownListGridViewRegion.DataValueField = "Id";
                DropDownListGridViewRegion.DataTextField = "Name";
                DropDownListGridViewRegion.DataSource = dt;
                DropDownListGridViewRegion.DataBind();
                DropDownListGridViewRegion.Items.Add(new ListItem("所有國家", "0"));
                DropDownListGridViewRegion.SelectedValue = "0";
                ViewState["GridViewRegionCountryId"] = "0";
            }
            connection.Close();
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedRegion"] = DropDownListCountry.SelectedValue;
        }

        protected void AddRegionToDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = $"Insert into region (Name, CountryId) values(@Name, @CountryId)";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Name", TextBoxRegion.Text);
            sqlCommand.Parameters.AddWithValue("@CountryId", ViewState["SelectedRegion"]);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
            if (result != 0)
            {
                Response.Write("<script>alert('新增成功')</script>");
                TextBoxRegion.Text = "";
            }
            connection.Close();
            BindGridViewRegion();
        }

        protected void DropDownListGridViewRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["GridViewRegionCountryId"] = DropDownListGridViewRegion.SelectedValue;
            BindGridViewRegion();
        }

        protected void BindGridViewRegion()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence;
            if (ViewState["GridViewRegionCountryId"].ToString() == "0")
            {
                SQLSentence = $"SELECT * FROM Region";
            }
            else
            {
                SQLSentence = $"SELECT * FROM Region where CountryId = @CountryId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@CountryId", ViewState["GridViewRegionCountryId"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd != null)
            {
                GridViewRegion.DataSource = rd;
                GridViewRegion.DataBind();
            }
            connection.Close();
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
                DropDownListGridViewAgent.DataValueField = "Id";
                DropDownListGridViewAgent.DataTextField = "Name";
                DropDownListGridViewAgent.DataSource = dt;
                DropDownListGridViewAgent.DataBind();
                DropDownListGridViewAgent.Items.Add(new ListItem("所有地區", "0"));
                DropDownListGridViewAgent.SelectedValue = "0";
                ViewState["GridViewAgentRegionId"] = "0";
            }
            //adapter.Dispose();
            //SQLSentence = "Select * from AgentImg";
            //sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            //adapter = new SqlDataAdapter(sqlCommand);
            //dt = new DataTable();
            //adapter.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    Panel panelAgent = (Panel)FindControl("PanelAgent");
            //    DropDownList ddlGridViewAgent = (DropDownList)panelAgent.FindControl("DropDownListGridViewAgent");

            //    ddlGridViewAgent.DataValueField = "Id";
            //    ddlGridViewAgent.DataTextField = "Name";
            //    ddlGridViewAgent.DataSource = dt;
            //    ddlGridViewAgent.DataBind();
            //    //ddlGridViewAgent.SelectedValue = DropDownListRegion.SelectedItem.Value;
            //    //ViewState["SelectedRegionId"] = DropDownListRegion.SelectedValue;

            //    //DropDownListGridViewAgent.DataValueField = "Id";
            //    //DropDownListGridViewAgent.DataTextField = "Name";
            //    //DropDownListGridViewAgent.DataSource = dt;
            //    //DropDownListGridViewAgent.DataBind();
            //    //DropDownListGridViewAgent.Items.Add(new ListItem("所有地區", "0"));
            //    //DropDownListGridViewAgent.SelectedValue = "0";
            //    //ViewState["GridViewAgentRegionId"] = "0";
            //}

            connection.Close();
        }

        protected void BindGridViewAgent()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence;
            if (ViewState["GridViewAgentRegionId"].ToString() == "0")
            {
                SQLSentence = $"select * from Agent";
            }
            else
            {
                SQLSentence = $"select * from Agent where RegionId = @RegionId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@RegionId", ViewState["GridViewAgentRegionId"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd.HasRows)
            {
                Repeater1.DataSource = rd;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.DataSource = "";
                Repeater1.Dispose();
            }
            if (rd != null)
            {
                GridViewAgent.DataSource = rd;
                GridViewAgent.DataBind();
            }
            connection.Close();
        }

        protected void GridViewCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewCountry.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Delete from country where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
            }
            connection.Close();
            BindGridViewCountry();
        }

        protected void GridViewCountry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewCountry.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewCountry();
        }

        protected void GridViewCountry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewCountry.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNameTextBox = TargetRow.FindControl("TextBoxCountryName") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewCountry.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update Country set Name = @Name where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@Name", UpdateNameTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewCountry.EditIndex = -1;
            BindGridViewCountry();
        }

        protected void GridViewCountry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewCountry.EditIndex = -1;  // 將資料行取消：編輯模式
            BindGridViewCountry();
        }

        protected void GridViewRegion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRegion.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewRegion();
        }

        protected void GridViewRegion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRegion.EditIndex = -1;
            BindGridViewRegion();
        }

        protected void GridViewRegion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewRegion.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Delete from region where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
            }
            connection.Close();
            BindGridViewRegion();
        }

        protected void GridViewRegion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewRegion.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNameTextBox = TargetRow.FindControl("TextBoxRegionName") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewRegion.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update Region set Name = @Name where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@Name", UpdateNameTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewRegion.EditIndex = -1;
            BindGridViewRegion();
        }

        protected void DropDownListGridViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["GridViewAgentRegionId"] = DropDownListGridViewAgent.SelectedValue;
            BindGridViewAgent();
        }

        protected void GridViewAgent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TextBox EditingNameTextBox = PanelAgent.FindControl("TextBoxAgentName") as TextBox;
            TextBox EditingContactTextBox = PanelAgent.FindControl("TextBoxAgentContact") as TextBox;
            TextBox EditingAddressTextBox = GridViewAgent.FindControl("TextBoxAgentAddress") as TextBox;
            TextBox EditingTelTextBox = GridViewAgent.FindControl("TextBoxAgentTel") as TextBox;
            TextBox EditingCellTextBox = GridViewAgent.FindControl("TextBoxAgentCell") as TextBox;
            TextBox EditingFaxTextBox = GridViewAgent.FindControl("TextBoxAgentFax") as TextBox;
            TextBox EditingEmailTextBox = GridViewAgent.FindControl("TextBoxAgentEmail") as TextBox;
            TextBox EditingUrlTextBox = GridViewAgent.FindControl("TextBoxAgentUrl") as TextBox;
            EditingNameTextBox.Width = Unit.Pixel(150);
            EditingContactTextBox.Width = Unit.Pixel(150);
            GridViewAgent.EditIndex = e.NewEditIndex;  // 將資料行轉換為：編輯模式
            BindGridViewAgent();
        }

        protected void GridViewAgent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAgent.EditIndex = -1;
            BindGridViewAgent();
        }

        protected void GridViewAgent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewAgent.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Delete from Agent where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
            }
            connection.Close();
            BindGridViewAgent();
        }

        protected void GridViewAgent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 1. 找到特定表格行數 (Row) ⇒ ex：第五行
            int IndexRow = e.RowIndex;

            // 2. 取得該行數的表格行數物件 (GridViewRow) ⇒ ex：第五行的物件
            GridViewRow TargetRow = GridViewAgent.Rows[IndexRow];

            // 3. 於該物件內部找到要修改的欄位 (Column) 物件⇒ ex：物件中的 Title 物件
            TextBox UpdateNameTextBox = TargetRow.FindControl("TextBoxAgentName") as TextBox;
            TextBox UpdateContactTextBox = TargetRow.FindControl("TextBoxAgentContact") as TextBox;
            TextBox UpdateAddressTextBox = TargetRow.FindControl("TextBoxAgentAddress") as TextBox;
            TextBox UpdateTelTextBox = TargetRow.FindControl("TextBoxAgentTel") as TextBox;
            TextBox UpdateCellTextBox = TargetRow.FindControl("TextBoxAgentCell") as TextBox;
            TextBox UpdateFaxTextBox = TargetRow.FindControl("TextBoxAgentFax") as TextBox;
            TextBox UpdateEmailTextBox = TargetRow.FindControl("TextBoxAgentEmail") as TextBox;
            TextBox UpdateUrlTextBox = TargetRow.FindControl("TextBoxAgentUrl") as TextBox;

            // 4. 找到該行數的 Key Value (ID) ⇒ ex：第五行中的 ID 欄位值
            string IDkey = GridViewAgent.DataKeys[IndexRow].Value.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件

            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Update Agent set Name = @Name, Contact = @Contact, Address = @Address, Tel = @Tel, Cell = @Cell, Fax = @Fax, Email = @Email, Url = @Url where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", IDkey);
            sqlCommand.Parameters.AddWithValue("@Name", UpdateNameTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Contact", UpdateContactTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Address", UpdateAddressTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Tel", UpdateTelTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Cell", UpdateCellTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Fax", UpdateFaxTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Email", UpdateEmailTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Url", UpdateUrlTextBox.Text);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            GridViewAgent.EditIndex = -1;
            BindGridViewAgent();
        }

        protected void GridViewAgent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewAgent.Columns[0].ItemStyle.Width = 150;
            GridViewAgent.Columns[1].ItemStyle.Width = 150;
            GridViewAgent.Columns[2].ItemStyle.Width = 150;
            GridViewAgent.Columns[3].ItemStyle.Width = 150;
            GridViewAgent.Columns[4].ItemStyle.Width = 150;
            GridViewAgent.Columns[5].ItemStyle.Width = 150;
            GridViewAgent.Columns[6].ItemStyle.Width = 150;

            GridViewAgent.Columns[0].ControlStyle.Width = 100;
            GridViewAgent.Columns[1].ControlStyle.Width = 100;
            GridViewAgent.Columns[2].ControlStyle.Width = 100;
            GridViewAgent.Columns[3].ControlStyle.Width = 100;
            GridViewAgent.Columns[4].ControlStyle.Width = 100;
            GridViewAgent.Columns[5].ControlStyle.Width = 100;
            GridViewAgent.Columns[6].ControlStyle.Width = 100;
        }

        protected void BindRepeaterAgent()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence;
            if (ViewState["GridViewAgentRegionId"].ToString() == "0")
            {
                SQLSentence = $"SELECT * FROM Agent";
            }
            else
            {
                SQLSentence = $"SELECT * FROM Agent where RegionId = @RegionId";   // 建立 SQL語句
                sqlCommand.Parameters.AddWithValue("@RegionId", ViewState["GridViewAgentRegionId"]);
            }
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
            SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
            if (rd.HasRows)
            {
                Repeater1.DataSource = rd;
                Repeater1.DataBind();
            }
            if (rd != null)
            {
                GridViewAgent.DataSource = rd;
                GridViewAgent.DataBind();
            }
            connection.Close();
        }

        //protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
        //    connection.Open();
        //    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
        //    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
        //    string SQLSentence = "Select * from AgentImg";
        //    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
        //    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        DropDownList ddlGridViewAgent = e.Item.FindControl("DropDownListChangeAgentImg") as DropDownList;
        //        ddlGridViewAgent.DataValueField = "Id";
        //        ddlGridViewAgent.DataTextField = "Name";
        //        ddlGridViewAgent.DataSource = dt;
        //        ddlGridViewAgent.DataBind();
        //    }
        //    connection.Close();
        //}

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //編輯按鈕
            if (e.CommandName == "EditAgent")
            {
                ToggleToEdit(e);
            }
            //取消按鈕
            if (e.CommandName == "CancelEditAgent")
            {
                ToggleToRead(e);
            }
            //更新按鈕
            if (e.CommandName == "UpdateAgent")
            {
                UpdateTheItem(e);
            }
            //刪除按鈕
            if (e.CommandName == "DeleteAgent")
            {
                DeleteTheItem(e);
            }
        }

        protected void ToggleToEdit(RepeaterCommandEventArgs e)
        {
            ViewState["EditAgentId"] = Convert.ToInt32(e.CommandArgument);
            DropDownList ddlChangeAgentImg = e.Item.FindControl("DropDownListChangeAgentImg") as DropDownList;
            System.Web.UI.WebControls.Label labelAgentName = e.Item.FindControl("LabelAgentName") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentContact = e.Item.FindControl("LabelAgentContact") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentAddress = e.Item.FindControl("LabelAgentAddress") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentTel = e.Item.FindControl("LabelAgentTel") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentCell = e.Item.FindControl("LabelAgentCell") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentFax = e.Item.FindControl("LabelAgentFax") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentEmail = e.Item.FindControl("LabelAgentEmail") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentUrl = e.Item.FindControl("LabelAgentUrl") as System.Web.UI.WebControls.Label;
            TextBox textBoxAgentName = e.Item.FindControl("TextBoxAgentName") as TextBox;
            TextBox textBoxAgentContact = e.Item.FindControl("TextBoxAgentContact") as TextBox;
            TextBox textBoxAgentAddress = e.Item.FindControl("TextBoxAgentAddress") as TextBox;
            TextBox textBoxAgentTel = e.Item.FindControl("TextBoxAgentTel") as TextBox;
            TextBox textBoxAgentCell = e.Item.FindControl("TextBoxAgentCell") as TextBox;
            TextBox textBoxAgentFax = e.Item.FindControl("TextBoxAgentFax") as TextBox;
            TextBox textBoxAgentEmail = e.Item.FindControl("TextBoxAgentEmail") as TextBox;
            TextBox textBoxAgentUrl = e.Item.FindControl("TextBoxAgentUrl") as TextBox;
            Button buttonAgentEdit = e.Item.FindControl("ButtonAgentEdit") as Button;
            Button buttonAgentUpdate = e.Item.FindControl("ButtonAgentUpdate") as Button;
            Button buttonAgentEditCancel = e.Item.FindControl("ButtonAgentEditCancel") as Button;

            ddlChangeAgentImg.Visible = true;
            labelAgentName.Visible = false;
            labelAgentContact.Visible = false;
            labelAgentAddress.Visible = false;
            labelAgentTel.Visible = false;
            labelAgentCell.Visible = false;
            labelAgentFax.Visible = false;
            labelAgentEmail.Visible = false;
            labelAgentUrl.Visible = false;
            textBoxAgentName.Visible = true;
            textBoxAgentContact.Visible = true;
            textBoxAgentAddress.Visible = true;
            textBoxAgentTel.Visible = true;
            textBoxAgentCell.Visible = true;
            textBoxAgentFax.Visible = true;
            textBoxAgentEmail.Visible = true;
            textBoxAgentUrl.Visible = true;
            buttonAgentEdit.Visible = false;
            buttonAgentUpdate.Visible = true;
            buttonAgentEditCancel.Visible = true;
        }

        protected void ToggleToRead(RepeaterCommandEventArgs e)
        {
            ViewState["EditAgentId"] = "";
            System.Web.UI.WebControls.Label labelAgentName = e.Item.FindControl("LabelAgentName") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentContact = e.Item.FindControl("LabelAgentContact") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentAddress = e.Item.FindControl("LabelAgentAddress") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentTel = e.Item.FindControl("LabelAgentTel") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentCell = e.Item.FindControl("LabelAgentCell") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentFax = e.Item.FindControl("LabelAgentFax") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentEmail = e.Item.FindControl("LabelAgentEmail") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label labelAgentUrl = e.Item.FindControl("LabelAgentUrl") as System.Web.UI.WebControls.Label;
            TextBox textBoxAgentName = e.Item.FindControl("TextBoxAgentName") as TextBox;
            TextBox textBoxAgentContact = e.Item.FindControl("TextBoxAgentContact") as TextBox;
            TextBox textBoxAgentAddress = e.Item.FindControl("TextBoxAgentAddress") as TextBox;
            TextBox textBoxAgentTel = e.Item.FindControl("TextBoxAgentTel") as TextBox;
            TextBox textBoxAgentCell = e.Item.FindControl("TextBoxAgentCell") as TextBox;
            TextBox textBoxAgentFax = e.Item.FindControl("TextBoxAgentFax") as TextBox;
            TextBox textBoxAgentEmail = e.Item.FindControl("TextBoxAgentEmail") as TextBox;
            TextBox textBoxAgentUrl = e.Item.FindControl("TextBoxAgentUrl") as TextBox;
            Button buttonAgentEdit = e.Item.FindControl("ButtonAgentEdit") as Button;
            Button buttonAgentUpdate = e.Item.FindControl("ButtonAgentUpdate") as Button;
            Button buttonAgentEditCancel = e.Item.FindControl("ButtonAgentEditCancel") as Button;

            labelAgentName.Visible = true;
            labelAgentContact.Visible = true;
            labelAgentAddress.Visible = true;
            labelAgentTel.Visible = true;
            labelAgentCell.Visible = true;
            labelAgentFax.Visible = true;
            labelAgentEmail.Visible = true;
            labelAgentUrl.Visible = true;
            textBoxAgentName.Visible = false;
            textBoxAgentContact.Visible = false;
            textBoxAgentAddress.Visible = false;
            textBoxAgentTel.Visible = false;
            textBoxAgentCell.Visible = false;
            textBoxAgentFax.Visible = false;
            textBoxAgentEmail.Visible = false;
            textBoxAgentUrl.Visible = false;
            buttonAgentEdit.Visible = true;
            buttonAgentUpdate.Visible = false;
            buttonAgentEditCancel.Visible = false;
        }

        protected void UpdateTheItem(RepeaterCommandEventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            TextBox textBoxAgentName = e.Item.FindControl("TextBoxAgentName") as TextBox;
            TextBox textBoxAgentContact = e.Item.FindControl("TextBoxAgentContact") as TextBox;
            TextBox textBoxAgentAddress = e.Item.FindControl("TextBoxAgentAddress") as TextBox;
            TextBox textBoxAgentTel = e.Item.FindControl("TextBoxAgentTel") as TextBox;
            TextBox textBoxAgentCell = e.Item.FindControl("TextBoxAgentCell") as TextBox;
            TextBox textBoxAgentFax = e.Item.FindControl("TextBoxAgentFax") as TextBox;
            TextBox textBoxAgentEmail = e.Item.FindControl("TextBoxAgentEmail") as TextBox;
            TextBox textBoxAgentUrl = e.Item.FindControl("TextBoxAgentUrl") as TextBox;
            string SQLSentence = "Update Agent set Name = @Name, Contact = @Contact, Address = @Address, Tel = @Tel, Cell = @Cell, Fax = @Fax, Email = @Email, Url = @Url where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Name", textBoxAgentName.Text);
            sqlCommand.Parameters.AddWithValue("@Contact", textBoxAgentContact.Text);
            sqlCommand.Parameters.AddWithValue("@Address", textBoxAgentAddress.Text);
            sqlCommand.Parameters.AddWithValue("@Tel", textBoxAgentTel.Text);
            sqlCommand.Parameters.AddWithValue("@Cell", textBoxAgentCell.Text);
            sqlCommand.Parameters.AddWithValue("@Fax", textBoxAgentFax.Text);
            sqlCommand.Parameters.AddWithValue("@Email", textBoxAgentEmail.Text);
            sqlCommand.Parameters.AddWithValue("@Url", textBoxAgentUrl.Text);
            sqlCommand.Parameters.AddWithValue("@Id", e.CommandArgument);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Response.Write($"<script>alert('修改{result}筆資料成功')</script>");
            }
            connection.Close();
            BindGridViewAgent();
        }

        protected void DeleteTheItem(RepeaterCommandEventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
            sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
            string SQLSentence = "Delete from Agent where id = @Id";   // 建立 SQL語句
            sqlCommand.Parameters.AddWithValue("@Id", e.CommandArgument);
            sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串}
            int result = sqlCommand.ExecuteNonQuery();
            if (result == 1)
            {
                Response.Write($"<script>alert('刪除{result}筆資料成功')</script>");
            }
            connection.Close();
            BindGridViewAgent();
        }

        protected void CloseOtherEditAgent()
        {
            if (ViewState["EditAgentId"] != null)
            {
                ViewState["EditAgentId"] = null;
            }
        }
    }
}