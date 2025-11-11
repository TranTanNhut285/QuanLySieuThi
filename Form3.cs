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
using Couchbase.Core.Exceptions.KeyValue;

namespace Mini_Mart
{
    public partial class Form3 : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private IScope _scope;
        private ICouchbaseCollection _collection;
        private string _selectedUsername;
        private List<User> _allUsers = new();
        public Form3()
        {
            InitializeComponent();
            InitializeDataGridView();
            Load += Form3_Load;
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                LoadCurrentUserInfo();

                // Nếu không phải admin, ẩn danh sách người dùng
                if (!UserSession.IsAdmin())
                {
                    grpDSND.Visible = false;
                }
                else
                {
                    await LoadAllUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RenderUsers(IEnumerable<User> users)
        {
            dataGridViewUsers.Rows.Clear();
            foreach (var u in users)
                dataGridViewUsers.Rows.Add(u.Username, u.FullName, u.Role, u.Email, u.Phone, u.Password);
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
        private void LoadCurrentUserInfo()
        {
            // Hiển thị thông tin người dùng hiện tại
            var currentUser = UserSession.CurrentUser;
            if (currentUser != null)
            {
                txtTenNguoiDung.Text = currentUser.Username;
                txtHoVaTen.Text = currentUser.FullName;
                txtVaiTro.Text = currentUser.Role;
                txtEmail.Text = currentUser.Email;
                txtSoDienThoai.Text = currentUser.Phone;
                txtMatKhau.Text = currentUser.Password;
            }
        }
        private void InitializeDataGridView()
        {
            // Xóa cột cũ nếu có
            dataGridViewUsers.Columns.Clear();

            // Tạo các cột mới
            dataGridViewUsers.Columns.Add("Username", "Tên người dùng");
            dataGridViewUsers.Columns.Add("FullName", "Họ và tên");
            dataGridViewUsers.Columns.Add("Role", "Vai trò");
            dataGridViewUsers.Columns.Add("Email", "Email");
            dataGridViewUsers.Columns.Add("Phone", "Số điện thoại");
            dataGridViewUsers.Columns.Add("Password", "Mật khẩu");
        }
        private async Task LoadAllUsers()
        {
            var query = @"
        SELECT 
            u.username   AS Username,
            u.full_name  AS full_name,
           u.`role`    AS `Role`,
            u.email      AS Email,
            u.phone      AS Phone,
            u.`password`  AS `Password`
        FROM `mini_mart`.`security`.`users` AS u
        WHERE u.type = 'user';
    ";


            var result = await _cluster.QueryAsync<User>(query);
           
            _allUsers = await result.Rows.ToListAsync();

            // Vẽ lưới 1 chỗ
            RenderUsers(_allUsers);

            // Xóa dữ liệu cũ trong DataGridView
            //dataGridViewUsers.Rows.Clear();

            // Thêm người dùng vào DataGridView
    //        foreach (var user in users)
    //        {
    //            dataGridViewUsers.Rows.Add(
    //    user.Username, user.FullName, user.Role, user.Email, user.Phone, user.Password
    //);
    //        }

            // Tùy chọn: Đặt tiêu đề cho các cột
            dataGridViewUsers.Columns[0].HeaderText = "Tên người dùng";
            dataGridViewUsers.Columns[1].HeaderText = "Họ và tên";
            dataGridViewUsers.Columns[2].HeaderText = "Vai trò";
            dataGridViewUsers.Columns[3].HeaderText = "Email";
            dataGridViewUsers.Columns[4].HeaderText = "Số điện thoại";
            dataGridViewUsers.Columns[5].HeaderText = "Password";
            
        }

        private async void btn_luuTTNDHT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox
                var currentUser = UserSession.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Bạn chưa đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo một biến để lưu thông tin cập nhật
                var updatedFields = new List<string>();

                // Kiểm tra và cập nhật họ tên
                string newFullName = txtHoVaTen.Text.Trim();
                if (currentUser.FullName != newFullName)
                {
                    currentUser.FullName = newFullName;
                    updatedFields.Add($"full_name = '{newFullName}'");
                }

                // Kiểm tra và cập nhật email
                string newEmail = txtEmail.Text.Trim();
                if (currentUser.Email != newEmail)
                {
                    currentUser.Email = newEmail;
                    updatedFields.Add($"email = '{newEmail}'");
                }

                // Kiểm tra và cập nhật số điện thoại
                string newPhone = txtSoDienThoai.Text.Trim();
                if (currentUser.Phone != newPhone)
                {
                    currentUser.Phone = newPhone;
                    updatedFields.Add($"phone = '{newPhone}'");
                }

                // Kiểm tra và cập nhật mật khẩu
                string newPassword = txtMatKhau.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    currentUser.Password = newPassword; // Lưu mật khẩu mới
                    updatedFields.Add($"`password` = '{newPassword}'"); // Sử dụng dấu nháy đơn
                }

                // Nếu không có thay đổi nào
                if (updatedFields.Count == 0)
                {
                    MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lưu thông tin người dùng vào Couchbase
                string documentKey = $"user::{currentUser.Username}";

                // Tạo câu lệnh UPDATE
                var updateQuery = $@"
            UPDATE `mini_mart`.`security`.`users` 
            SET {string.Join(", ", updatedFields)} 
            WHERE META().id = '{documentKey}'";

                await _cluster.QueryAsync<dynamic>(updateQuery);

                MessageBox.Show("Thông tin tài khoản đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu thông tin tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox
                string username = txttnd.Text.Trim();
                string password = txtmk.Text.Trim();
                string fullName = txthvt.Text.Trim();
                string role = txtvt.Text.Trim();
                string email = txtem.Text.Trim();
                string phone = txtsdt.Text.Trim();

                // Kiểm tra tính hợp lệ của dữ liệu
                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(fullName) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng người dùng mới
                var newUser = new User
                {
                    Username = username,
                    Password = password, // Lưu ý: Mật khẩu nên được mã hóa trước khi lưu
                    FullName = fullName,
                    Role = role,
                    Email = email,
                    Phone = phone,
                    IsActive = true,
                    Type = "user"
                };

                // Lưu người dùng vào Couchbase
                string documentKey = $"user::{username}";

                // Thực hiện Upsert để thêm hoặc cập nhật người dùng
                await _collection.UpsertAsync(documentKey, newUser);

                MessageBox.Show("Người dùng đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật danh sách người dùng nếu cần
                await LoadAllUsers(); // Gọi lại phương thức để tải danh sách người dùng

                // Xóa nội dung các ô TextBox
                txttnd.Clear();
                txtmk.Clear();
                txthvt.Clear();
                txtvt.Clear();
                txtem.Clear();
                txtsdt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillUserTextBoxesFromRow(int rowIndex)
        {
            var row = dataGridViewUsers.Rows[rowIndex];

            txttnd.Text = row.Cells["Username"].Value?.ToString() ?? "";
            txthvt.Text = row.Cells["FullName"].Value?.ToString() ?? "";
            txtvt.Text = row.Cells["Role"].Value?.ToString() ?? "";
            txtem.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtsdt.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            txtmk.Text = row.Cells["Password"].Value?.ToString() ?? "";
            _selectedUsername = row.Cells["Username"].Value?.ToString();
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // click header
            FillUserTextBoxesFromRow(e.RowIndex);
        }

        private async void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Lấy dữ liệu từ UI
                var username = txttnd.Text.Trim();
                var fullName = txthvt.Text.Trim();
                var role = txtvt.Text.Trim();
                var email = txtem.Text.Trim();
                var phone = txtsdt.Text.Trim();
                var password = txtmk.Text.Trim();

                // 2) Validate cơ bản
                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(fullName) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Username, Họ tên, Vai trò, Email.",
                        "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3) Chuẩn bị document sẽ lưu
                var doc = new User
                {
                    Username = username,
                    FullName = fullName,
                    Role = role,
                    Email = email,
                    Phone = phone,
                    Password = password,   // Khuyến nghị: dùng hash thay vì plain text
                    Type = "user",
                    IsActive = true
                };

                // 4) Key cũ và key mới
                var oldKey = string.IsNullOrWhiteSpace(_selectedUsername)
                                ? $"user::{username}"
                                : $"user::{_selectedUsername}";
                var newKey = $"user::{username}";

                // 5) Nếu không đổi username → upsert thẳng
                if (string.Equals(_selectedUsername, username, StringComparison.OrdinalIgnoreCase) ||
                    string.IsNullOrWhiteSpace(_selectedUsername))
                {
                    await _collection.UpsertAsync(newKey, doc);
                }
                else
                {
                    // Đổi username: chèn doc mới rồi xóa doc cũ (đổi khóa)
                    await _collection.InsertAsync(newKey, doc);
                    await _collection.RemoveAsync(oldKey);
                }

                // 6) Refresh lưới + xoá ô nhập
                await LoadAllUsers();
                ClearInputs();
                _selectedUsername = null;

                MessageBox.Show("Đã lưu thông tin người dùng.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txttnd.Clear();    // Username
            txthvt.Clear();    // FullName
            txtvt.Clear();     // Role
            txtem.Clear();     // Email
            txtsdt.Clear();    // Phone
            txtmk.Clear();     // Password
        }

        private async void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Chỉ admin mới được xóa
                if (!UserSession.IsAdmin())
                {
                    MessageBox.Show("Bạn không có quyền xóa người dùng.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2) Lấy username đang chọn (ưu tiên textbox, fallback _selectedUsername)
                var username = (txttnd.Text ?? "").Trim();
                if (string.IsNullOrEmpty(username))
                    username = _selectedUsername;

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Hãy chọn người dùng cần xóa.", "Thiếu dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3) Không cho xóa chính mình (tránh tự khóa tài khoản)
                var me = UserSession.CurrentUser?.Username;
                if (!string.IsNullOrEmpty(me) &&
                    string.Equals(me, username, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Không thể xóa tài khoản đang đăng nhập.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4) Xác nhận
                var confirm = MessageBox.Show(
                    $"Bạn chắc chắn muốn xóa người dùng \"{username}\"?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                // 5) Xóa trên Couchbase
                var key = $"user::{username}";
                try
                {
                    await _collection.RemoveAsync(key);
                }
                catch (DocumentNotFoundException)
                {
                    // Không có cũng coi như xóa xong, chỉ thông báo nhẹ
                    MessageBox.Show("Không tìm thấy document người dùng (đã bị xóa trước đó).",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 6) Refresh + clear
                await LoadAllUsers();
                ClearInputs();
                _selectedUsername = null;

                MessageBox.Show("Đã xóa người dùng.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            var term = txt_timkiem.Text.Trim();
            if (string.IsNullOrEmpty(term))
            {
                RenderUsers(_allUsers);
                return;
            }

            var filtered = _allUsers.Where(u =>
                !string.IsNullOrEmpty(u.Username) &&
                u.Username.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);

            RenderUsers(filtered);
        }
    }
}
