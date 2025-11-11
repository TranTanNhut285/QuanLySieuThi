using Couchbase;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text;

namespace Mini_Mart
{
    public partial class frmCategory : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private IScope _scope;
        private ICouchbaseCollection _catCollection;
        private List<Category> _allCategories = new();
        public frmCategory()
        {
            InitializeComponent();
            InitializeGrid();
            Load += frmCategory_Load;
        }
        public class Category
        {
            [JsonPropertyName("type")] public string Type { get; set; } = "category";
            [JsonPropertyName("code")] public string Code { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("description")] public string Description { get; set; }
            [JsonPropertyName("display_order")] public int DisplayOrder { get; set; }
            [JsonPropertyName("is_active")] public bool IsActive { get; set; }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearInputs();
            SetInputsEnabled(true, allowEditCode: true);   // thêm mới: cho sửa mã
            txtCode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Hãy chọn danh mục để sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SetInputsEnabled(true, allowEditCode: false);  // sửa: khóa mã
            txtName.Focus();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var code = (txtCode.Text ?? "").Trim();
                if (string.IsNullOrEmpty(code))
                {
                    MessageBox.Show("Chọn danh mục để xóa.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show($"Xóa danh mục \"{code}\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                await _catCollection.RemoveAsync($"category::{code}");
                await LoadCategories();
                ClearInputs();
                SetInputsEnabled(false);
            }
            catch (DocumentNotFoundException)
            {
                await LoadCategories();
                MessageBox.Show("Danh mục không tồn tại (có thể đã xóa).",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEdit = !txtCode.Enabled; // code bị khóa => sửa

                var code = (txtCode.Text ?? "").Trim();
                var name = (txtName.Text ?? "").Trim();
                var description = (txtDescription.Text ?? "").Trim();
                var displayOrd = (int)nudDisplayOrder.Value;
                var isActive = chkIsActive.Checked;

                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Nhập tối thiểu Mã danh mục và Tên.",
                        "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (string.IsNullOrWhiteSpace(code)) txtCode.Focus(); else txtName.Focus();
                    return;
                }

                var cat = new Category
                {
                    Code = code,
                    Name = name,
                    Description = description,
                    DisplayOrder = displayOrd,
                    IsActive = isActive
                };

                var key = $"category::{code}";
                if (isEdit)
                {
                    await _catCollection.UpsertAsync(key, cat);  // sửa
                }
                else
                {
                    try
                    {
                        await _catCollection.InsertAsync(key, cat); // thêm
                    }
                    catch (DocumentExistsException)
                    {
                        MessageBox.Show("Mã danh mục đã tồn tại. Nhập mã khác.",
                            "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        return;
                    }
                }

                await LoadCategories();
                ClearInputs();
                SetInputsEnabled(false); // lưu xong khóa hết
                MessageBox.Show("Đã lưu danh mục.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmCategory_Load(object sender, EventArgs e)
        {
            await ConnectCouchbase();
            InitializeGrid();
            await LoadCategories();
        }
        private async Task ConnectCouchbase()
        {
            var opts = new ClusterOptions { UserName = "Administrator", Password = "123456" };
            _cluster = await Cluster.ConnectAsync("couchbase://localhost", opts);
            await _cluster.WaitUntilReadyAsync(TimeSpan.FromSeconds(10));
            _bucket = await _cluster.BucketAsync("mini_mart");
            _scope = _bucket.Scope("masterdata");
            _catCollection = _scope.Collection("categories");
        }
        private async Task LoadCategories()
        {
            var q = @"
        SELECT
            c.code           AS Code,
            c.name           AS Name,
            c.description    AS Description,
            c.display_order  AS DisplayOrder,
            c.is_active      AS IsActive
        FROM `mini_mart`.`masterdata`.`categories` AS c
    ";

            var res = await _cluster.QueryAsync<Category>(q);
            _allCategories = await res.Rows.ToListAsync();
            RenderCategories(_allCategories);
        }
        private void InitializeGrid()
        {
            dtgv_category.AutoGenerateColumns = false;
            dtgv_category.Columns.Clear();
            dtgv_category.Columns.Add("Code", "Mã danh mục");
            dtgv_category.Columns.Add("Name", "Tên");
            dtgv_category.Columns.Add("Description", "Mô tả");
            dtgv_category.Columns.Add("DisplayOrder", "Thứ tự hiển thị");
            dtgv_category.Columns.Add("IsActive", "Đang hoạt động");
        }
        private void RenderCategories(IEnumerable<Category> list)
        {
            dtgv_category.SuspendLayout();
            dtgv_category.Rows.Clear();
            foreach (var c in list)
                dtgv_category.Rows.Add(c.Code, c.Name, c.Description, c.DisplayOrder, c.IsActive);
            dtgv_category.ResumeLayout();
        }
        private void SetInputsEnabled(bool enabled, bool allowEditCode = true)
        {
            txtCode.Enabled = enabled && allowEditCode;
            txtName.Enabled = enabled;
            txtDescription.Enabled = enabled;
            nudDisplayOrder.Enabled = enabled;
            chkIsActive.Enabled = enabled;
        }

        private void ClearInputs()
        {
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            nudDisplayOrder.Value = 0;
            chkIsActive.Checked = false;
        }

        private void dtgv_category_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dtgv_category.Rows[e.RowIndex];
            string S(string col) => r.Cells[col]?.Value?.ToString() ?? "";
            int I(string col) => int.TryParse(r.Cells[col]?.Value?.ToString(), out var v) ? v : 0;
            bool B(string col)
            {
                var v = r.Cells[col]?.Value;
                if (v is bool b) return b;
                if (v == null) return false;
                if (bool.TryParse(v.ToString(), out var bb)) return bb;
                if (int.TryParse(v.ToString(), out var ii)) return ii != 0;
                return false;
            }

            txtCode.Text = S("Code");
            txtName.Text = S("Name");
            txtDescription.Text = S("Description");
            nudDisplayOrder.Value = I("DisplayOrder");
            chkIsActive.Checked = B("IsActive");
        }
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var norm = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in norm)
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var term = (textBox1.Text ?? "").Trim();

            if (string.IsNullOrEmpty(term))
            {
                RenderCategories(_allCategories); // hiện tất cả
                return;
            }

            var normTerm = RemoveDiacritics(term).ToLowerInvariant();

            var filtered = _allCategories.Where(c =>
                !string.IsNullOrEmpty(c.Name) &&
                RemoveDiacritics(c.Name).ToLowerInvariant().Contains(normTerm));

            RenderCategories(filtered);
        }
    }
}
