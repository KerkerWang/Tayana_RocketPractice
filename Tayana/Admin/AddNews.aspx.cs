using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.Admin
{
    public partial class NewsGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonAddNews_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == "")
            {
                Response.Write("<script>alert('Title欄位不得空白')</script>");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                    sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                    string SQLSentence;
                    SQLSentence = "Insert into News (Title, Subtitle, Content) values(@Title, @Subtitle, @Content)";   // 建立 SQL語句
                    sqlCommand.Parameters.AddWithValue("@Title", TextBoxTitle.Text);
                    sqlCommand.Parameters.AddWithValue("@Subtitle", TextBoxSubtitle.Text);
                    sqlCommand.Parameters.AddWithValue("@Content", TextBoxContent.Text);
                    sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                    int result = sqlCommand.ExecuteNonQuery(); // Command物件的命令賦予SQLSentence字串
                    if (result != 0)
                    {
                        Response.Write("<script>alert('新增成功')</script>;<script>window.location.href = 'News.aspx'</script>;");
                    }
                    connection.Close();
                }
            }
        }
    }
}