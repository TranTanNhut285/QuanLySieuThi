using Couchbase;
using Couchbase.KeyValue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Mini_Mart
{
    public partial class frmLogin : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private IScope _scope;
        private ICouchbaseCollection _collection;
        public frmLogin()
        {
            InitializeComponent();
        }
        private async Task ConnectCouchbase()
        {
            try
            {
                var options = new ClusterOptions
                {
                    UserName = "Administrator",
                    Password = "123456"
                };

                _cluster = await Cluster.ConnectAsync("couchbase://localhost", options);
                await _cluster.WaitUntilReadyAsync(TimeSpan.FromSeconds(10));

                // Mở bucket và collection
                _bucket = await _cluster.BucketAsync("mini_mart");
                _scope = _bucket.Scope("security");
                _collection = _scope.Collection("users");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        private async void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();

                // Đặt focus vào textbox username
                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                // Disable button để tránh click nhiều lần
                btnLogin.Enabled = false;
                btnLogin.Text = "Đang đăng nhập...";

                // Xác thực người dùng
                var loginResult = await AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text);

                if (loginResult.Success)
                {
                    // Cập nhật last_login
                    await UpdateLastLogin(loginResult.User);

                    // Lưu thông tin user vào session
                    UserSession.CurrentUser = loginResult.User;

                    MessageBox.Show($"Đăng nhập thành công!\nXin chào {loginResult.User.FullName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form chính
                    this.Hide();
                    Form1 mainForm = new Form1();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(loginResult.Message,
                        "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng nhập: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Đăng nhập";
            }
        }
        private async Task<LoginResult> AuthenticateUser(string username, string password)
        {
            try
            {
                // Tìm user bằng username
                var query = $@"
                    SELECT u.* 
                    FROM `mini_mart`.`security`.`users` u 
                    WHERE u.username = '{username}' 
                    AND u.type = 'user'
                    LIMIT 1";

                var result = await _cluster.QueryAsync<User>(query);
                var users = await result.Rows.ToListAsync();

                if (users.Count == 0)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tên đăng nhập không tồn tại!"
                    };
                }

                var user = users[0];

                // Kiểm tra mật khẩu
                if (user.Password != password)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Mật khẩu không chính xác!"
                    };
                }

                // Kiểm tra tài khoản có active không
                if (!user.IsActive)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!"
                    };
                }

                return new LoginResult
                {
                    Success = true,
                    User = user,
                    Message = "Đăng nhập thành công!"
                };
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = $"Lỗi xác thực: {ex.Message}"
                };
            }
        }

        private async Task UpdateLastLogin(User user)
        {
            try
            {
                // Lấy document key
                string documentKey = user.Username == "admin" ? "user::admin" : $"user::{user.Username}";

                // Cập nhật last_login
                var updateQuery = $@"
                    UPDATE `mini_mart`.`security`.`users` 
                    SET last_login = '{DateTime.Now:yyyy-MM-ddTHH:mm:sszzz}'
                    WHERE META().id = '{documentKey}'";

                await _cluster.QueryAsync<dynamic>(updateQuery);
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không ảnh hưởng đến quá trình đăng nhập
                Console.WriteLine($"Lỗi cập nhật last_login: {ex.Message}");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

    }
    public class User
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("last_login")]
        public string LastLogin { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; }
    }

    // Class LoginResult
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }

    // Class UserSession để lưu thông tin user hiện tại
    public static class UserSession
    {
        public static User CurrentUser { get; set; }

        public static bool HasPermission(string permission)
        {
            return CurrentUser?.Permissions?.Contains(permission) ?? false;
        }

        public static bool IsAdmin()
        {
            return CurrentUser?.Role == "ADMIN";
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
