using Couchbase;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text;

namespace Mini_Mart
{
    public partial class frmSupplier : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private IScope _scope;
        private ICouchbaseCollection _collection;
        private List<Supplier> _allSuppliers = new();
        public frmSupplier()
        {
            InitializeComponent();
            InitializeDataGridView();
            Load += frmSupplier_Load;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearSupplierInputs();
            // Thêm mới → bật toàn bộ, cho sửa Code
            SetSupplierInputsEnabled(true, allowEditCode: true);
            txtCode.Focus();
        }
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var ch in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark) sb.Append(ch);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        private void RenderSuppliers(IEnumerable<Supplier> list)
        {
            dtgv_supplier.SuspendLayout();
            dtgv_supplier.Rows.Clear();

            foreach (var s in list)
            {
                dtgv_supplier.Rows.Add(
                    s.Code, s.Name, s.ContactPerson, s.Phone, s.Email,
                    s.Address, s.TaxCode, s.BankAccount, s.BankName, s.Note, s.IsActive
                );
            }
            dtgv_supplier.ResumeLayout();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            SetSupplierInputsEnabled(true, allowEditCode: false);
            txtName.Focus();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Lấy mã nhà cung cấp từ textbox hoặc dòng được chọn
                var code = (txtCode.Text ?? "").Trim();
                if (string.IsNullOrEmpty(code))
                {
                    if (dtgv_supplier.SelectedRows.Count > 0)
                    {
                        var row = dtgv_supplier.SelectedRows[0];
                        code = row.Cells["Code"]?.Value?.ToString();
                    }
                }

                if (string.IsNullOrWhiteSpace(code))
                {
                    MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Thiếu dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2) Xác nhận
                var confirm = MessageBox.Show(
                    $"Bạn chắc chắn muốn xóa nhà cung cấp \"{code}\"?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                // 3) Xóa trong Couchbase
                var key = $"supplier::{code}";
                try
                {
                    await _collection.RemoveAsync(key);
                }
                catch (DocumentNotFoundException)
                {
                    // Có thể đã bị xóa trước đó
                    MessageBox.Show("Không tìm thấy document nhà cung cấp (có thể đã bị xóa).",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 4) Refresh lưới + xóa & khóa form
                await LoadSuppliers();
                ClearSupplierInputs();
                SetSupplierInputsEnabled(false);  // khóa hết
                txtCode.Enabled = false;

                MessageBox.Show("Đã xóa nhà cung cấp.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Xác định chế độ: Add hay Edit
                bool isEdit = !txtCode.Enabled; // Edit khi Code đang bị khóa

                // 2) Lấy dữ liệu từ form
                var code = (txtCode.Text ?? "").Trim();
                var name = (txtName.Text ?? "").Trim();
                var contactPerson = (txtContactPerson.Text ?? "").Trim();
                var phone = (txtPhone.Text ?? "").Trim();
                var email = (txtEmail.Text ?? "").Trim();
                var address = (txtAddress.Text ?? "").Trim();
                var taxCode = (txtTaxCode.Text ?? "").Trim();
                var bankAccount = (txtBankAccount.Text ?? "").Trim();
                var bankName = (txtBankName.Text ?? "").Trim();
                var note = (txtNote.Text ?? "").Trim();
                var isActive = chkIsActive.Checked;

                // 3) Validate cơ bản
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Vui lòng nhập tối thiểu Mã (Code) và Tên (Name).",
                        "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (string.IsNullOrWhiteSpace(code)) txtCode.Focus(); else txtName.Focus();
                    return;
                }

                // 4) Chuẩn bị document
                var supplier = new Supplier
                {
                    Code = code,
                    Name = name,
                    ContactPerson = contactPerson,
                    Phone = phone,
                    Email = email,
                    Address = address,
                    TaxCode = taxCode,
                    BankAccount = bankAccount,
                    BankName = bankName,
                    Note = note,
                    IsActive = isActive,
                    Type = "supplier"
                };

                var key = $"supplier::{code}";

                // 5) Ghi vào Couchbase
                if (isEdit)
                {
                    // Sửa: Upsert theo key hiện tại
                    await _collection.UpsertAsync(key, supplier);
                }
                else
                {
                    // Thêm: Insert để bắt trùng key
                    try
                    {
                        await _collection.InsertAsync(key, supplier);
                    }
                    catch (DocumentExistsException)
                    {
                        MessageBox.Show("Mã nhà cung cấp đã tồn tại. Vui lòng nhập mã khác.",
                            "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        return;
                    }
                }

                // 6) Refresh lưới + clear form + đưa về trạng thái mặc định
                await LoadSuppliers();
                ClearSupplierInputs();
                SetSupplierInputsEnabled(false);
                txtCode.Enabled = false;

                MessageBox.Show("Đã lưu nhà cung cấp.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupplierInputsEnabled(bool enabled, bool allowEditCode = true)
        {

            txtCode.Enabled = allowEditCode && enabled;
            txtName.Enabled = enabled;
            txtContactPerson.Enabled = enabled;
            txtPhone.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtTaxCode.Enabled = enabled;
            txtBankAccount.Enabled = enabled;
            txtBankName.Enabled = enabled;
            txtNote.Enabled = enabled;
            chkIsActive.Enabled = enabled;
        }
        private void ClearSupplierInputs()
        {
            txtCode.Enabled = true;
            txtCode.Clear();
            txtName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtTaxCode.Clear();
            txtBankAccount.Clear();
            txtBankName.Clear();
            txtNote.Clear();
            chkIsActive.Checked = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            // Xóa cột cũ nếu có
            dtgv_supplier.Columns.Clear();

            // Tạo các cột mới
            dtgv_supplier.Columns.Add("Code", "Mã nhà cung cấp");
            dtgv_supplier.Columns.Add("Name", "Tên nhà cung cấp");
            dtgv_supplier.Columns.Add("ContactPerson", "Người liên hệ");
            dtgv_supplier.Columns.Add("Phone", "Số điện thoại");
            dtgv_supplier.Columns.Add("Email", "Email");
            dtgv_supplier.Columns.Add("Address", "Địa chỉ");
            dtgv_supplier.Columns.Add("TaxCode", "Mã số thuế");
            dtgv_supplier.Columns.Add("BankAccount", "Số tài khoản");
            dtgv_supplier.Columns.Add("BankName", "Ngân hàng");
            dtgv_supplier.Columns.Add("Note", "Ghi chú");
            dtgv_supplier.Columns.Add("IsActive", "Trạng thái");
        }
        private async void frmSupplier_Load(object sender, EventArgs e)
        {
            await ConnectCouchbase();
            await LoadSuppliers();
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
                _scope = _bucket.Scope("masterdata");
                _collection = _scope.Collection("suppliers");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSuppliers()
        {
            try
            {
                var query = @"SELECT s.* FROM `mini_mart`.`masterdata`.`suppliers` AS s";
                var result = await _cluster.QueryAsync<Supplier>(query);
                _allSuppliers = await result.Rows.ToListAsync();   // giữ nguồn cho tìm kiếm

                RenderSuppliers(_allSuppliers);                    // vẽ 1 chỗ
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu nhà cung cấp: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class Supplier
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("contact_person")]
            public string ContactPerson { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("tax_code")]
            public string TaxCode { get; set; }

            [JsonProperty("bank_account")]
            public string BankAccount { get; set; }

            [JsonProperty("bank_name")]
            public string BankName { get; set; }

            [JsonProperty("note")]
            public string Note { get; set; }

            [JsonProperty("is_active")]
            public bool IsActive { get; set; }
        }

        private void dtgv_supplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu hàng được nhấp là hàng hợp lệ
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ hàng đã nhấp
                var selectedRow = dtgv_supplier.Rows[e.RowIndex];

                // Điền thông tin vào các TextBox
                txtCode.Text = selectedRow.Cells["Code"].Value.ToString(); // Mã nhà cung cấp
                txtName.Text = selectedRow.Cells["Name"].Value.ToString(); // Tên nhà cung cấp
                txtContactPerson.Text = selectedRow.Cells["ContactPerson"].Value.ToString(); // Người liên hệ
                txtPhone.Text = selectedRow.Cells["Phone"].Value.ToString(); // Số điện thoại
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString(); // Email
                txtAddress.Text = selectedRow.Cells["Address"].Value.ToString(); // Địa chỉ
                txtTaxCode.Text = selectedRow.Cells["TaxCode"].Value.ToString(); // Mã số thuế
                txtBankAccount.Text = selectedRow.Cells["BankAccount"].Value.ToString(); // Số tài khoản
                txtBankName.Text = selectedRow.Cells["BankName"].Value.ToString(); // Ngân hàng
                txtNote.Text = selectedRow.Cells["Note"].Value.ToString(); // Ghi chú
                chkIsActive.Checked = Convert.ToBoolean(selectedRow.Cells["IsActive"].Value);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var term = (txtSearch.Text ?? "").Trim();
            if (string.IsNullOrEmpty(term))
            {
                RenderSuppliers(_allSuppliers);
                return;
            }

            var normTerm = RemoveDiacritics(term).ToLowerInvariant();
            var filtered = _allSuppliers.Where(s =>
                !string.IsNullOrEmpty(s.Name) &&
                RemoveDiacritics(s.Name).ToLowerInvariant().Contains(normTerm));

            RenderSuppliers(filtered);
        }
    }

}
