using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Tayana
{
    public partial class Contact : System.Web.UI.Page
    {
        private int countryId = 1;
        private int yachtId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //載入DropDownListCountry選項
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                string SQLSentence = $"SELECT * FROM country";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                SqlDataReader rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    DropDownListCountry.DataSource = rd;
                    DropDownListCountry.DataTextField = "Name"; // 設定顯示的文字欄位
                    DropDownListCountry.DataValueField = "Id"; // 設定值的欄位
                    DropDownListCountry.DataBind();
                }
                rd.Close();
                //載入DropDownListYacht選項
                SQLSentence = "SELECT * FROM Yacht";   // 建立 SQL語句
                sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                rd = sqlCommand.ExecuteReader(); // Command物件的命令賦予SQLSentence字串
                if (rd != null)
                {
                    DropDownListYacht.DataSource = rd;
                    DropDownListYacht.DataTextField = "Name"; // 設定顯示的文字欄位
                    DropDownListYacht.DataValueField = "Id"; // 設定值的欄位
                    DropDownListYacht.DataBind();
                }
                connection.Close();
            }
        }

        protected void ImageButtonSubmit_Click(object sender, ImageClickEventArgs e)
        {
            // 1.檢查必填欄位是否都有值
            // 2.必填欄位格式驗證
            // 3.驗證不是機器人
            // 4.連接資料庫
            // 5.新增至資料庫
            // 6.傳送通知函至該信箱
            if (TextBoxName.Text != "" && TextBoxEmail.Text != "" && TextBoxPhone.Text != "")
            {
                if (String.IsNullOrEmpty(RecaptchaWidget1.Response))
                {
                    Response.Write("<script>alert('請進行機器人驗證')</script>");
                }
                else
                {
                    var result = RecaptchaWidget1.Verify();
                    if (result.Success)
                    {
                        //此處可加入"我不是機器人驗證"成功後要做的事
                        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
                        connection.Open();
                        SqlCommand sqlCommand = new SqlCommand();     // 建立 Command物件
                        sqlCommand.Connection = connection;           // Command物件的連線賦予connection物件
                        string SQLSentence = "Insert into Contact([Name], Email, Phone, CountryId, YachtId, Comments) values(@Name, @Email, @Phone, @CountryId, @YachtId, @Comments)";   // 建立 SQL語句
                        sqlCommand.Parameters.AddWithValue("@Name", TextBoxName.Text);
                        sqlCommand.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                        sqlCommand.Parameters.AddWithValue("@Phone", TextBoxPhone.Text);
                        sqlCommand.Parameters.AddWithValue("@CountryId", countryId);
                        sqlCommand.Parameters.AddWithValue("@YachtId", yachtId);
                        sqlCommand.Parameters.AddWithValue("@Comments", TextBoxComments.Text);
                        sqlCommand.CommandText = SQLSentence;         // Command物件的命令賦予SQLSentence字串
                        int data = sqlCommand.ExecuteNonQuery();
                        if (data == 1)
                        {
                            sendGmail();
                            Response.Write("<script>alert('我們已收到你的需求，將盡快處理。')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('你是機器人')</script>");
                        Response.Write("<script>window.location.href = 'Contact.aspx';</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('請填入所有必填欄位')</script>");
            }
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            countryId = Convert.ToInt32(DropDownListCountry.SelectedValue);
        }

        protected void DropDownListYacht_SelectedIndexChanged(object sender, EventArgs e)
        {
            yachtId = Convert.ToInt32(DropDownListYacht.SelectedValue);
        }

        public void sendGmail()
        {
            //宣告使用 MimeMessage
            var message = new MimeMessage();
            //設定發信地址 ("發信人", "發信 email")
            message.From.Add(new MailboxAddress("TayanaYacht", "tayanarocketteam@gmail.com"));
            //設定收信地址 ("收信人", "收信 email")
            message.To.Add(new MailboxAddress(TextBoxName.Text.Trim(), TextBoxEmail.Text.Trim()));
            //寄件副本email
            message.Cc.Add(new MailboxAddress("Brooke", "brookewang0922@gmail.com"));
            //設定優先權
            //message.Priority = MessagePriority.Normal;
            //信件標題
            message.Subject = "TayanaYacht Auto Email";
            //建立 html 郵件格式
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>Thank you for contacting us!</h1>" +
                $"<h3>Name : {TextBoxName.Text.Trim()}</h3>" +
                $"<h3>Email : {TextBoxEmail.Text.Trim()}</h3>" +
                $"<h3>Phone : {TextBoxPhone.Text.Trim()}</h3>" +
                $"<h3>Country : {DropDownListCountry.SelectedItem}</h3>" +
                $"<h3>Type : {DropDownListYacht.SelectedItem}</h3>" +
                $"<h3>Comments : </h3>" +
                $"<p>{TextBoxComments.Text.Trim()}</p>";
            //設定郵件內容
            message.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定 false 關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密)
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("tayanarocketteam@gmail.com", "fjdq rgmg isqh uded");
                //發信
                client.Send(message);
                //結束連線
                client.Disconnect(true);
            }
        }
    }
}