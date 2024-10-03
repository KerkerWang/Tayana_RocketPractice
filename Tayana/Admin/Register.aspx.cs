using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Konscious.Security.Cryptography;

namespace Tayana.Admin
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Argon2 加密
        //產生 Salt 功能
        private byte[] CreateSalt()
        {
            //Salt陣列
            var buffer = new byte[16];

            //產生「夠強」的亂數以產生「強勢金鑰(Strong Key)」的類別
            var rng = new RNGCryptoServiceProvider();

            //在位元組陣列中填入在密碼編譯方面的強式隨機值序列。
            rng.GetBytes(buffer);

            return buffer;
        }

        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 4; // 迭代運算次數
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }

        protected void ButtonCreateAccount_Click(object sender, EventArgs e)
        {
            bool haveSameAccount = false;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            string sqlCheck = "SELECT * FROM [User] WHERE Name = @Name";
            string sqlAdd = "INSERT INTO [User] (Name, Password, Salt) VALUES(@Name, @Password, @Salt)";
            SqlCommand commandCheck = new SqlCommand(sqlCheck, connection);
            SqlCommand commandAdd = new SqlCommand(sqlAdd, connection);

            //檢查有無重複帳號
            commandCheck.Parameters.AddWithValue("@Name", TextBoxName.Text);
            connection.Open();
            SqlDataReader rd = commandCheck.ExecuteReader();
            if (rd.Read())
            {
                haveSameAccount = true;
                Response.Write($"<script>alert('此帳號已有人使用')</script>");
                //LabelAdd.Visible = true; //帳號重複通知
            }
            connection.Close();

            //無重複帳號才執行加入
            if (!haveSameAccount)
            {
                //Hash 加鹽加密
                string password = TextBoxPassword.Text;
                var salt = CreateSalt();
                string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
                var hash = HashPassword(password, salt);
                string hashPassword = Convert.ToBase64String(hash);

                commandAdd.Parameters.AddWithValue("@Name", TextBoxName.Text);
                commandAdd.Parameters.AddWithValue("@password", hashPassword);
                commandAdd.Parameters.AddWithValue("@salt", saltStr);

                connection.Open();
                commandAdd.ExecuteNonQuery();
                connection.Close();
                //畫面渲染
                //GridView1.DataBind();
                Response.Write($"<script>alert('註冊成功，將導向登入頁面進行登入。'); window.location.href = 'Login.aspx';</script>");

                //清空輸入欄位
                //TextBoxName.Text = "";
                //TextBoxPassword.Text = "";
                //LabelAdd.Visible = false;
            }
        }
    }
}