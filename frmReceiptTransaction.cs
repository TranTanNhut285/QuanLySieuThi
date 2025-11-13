using Couchbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mini_Mart.frmSupplier;
using Couchbase.KeyValue;
using Couchbase.Query;
using ZXing;
using ZXing.Common;
using ClosedXML.Excel;

namespace Mini_Mart
{
    public partial class frmReceiptTransaction : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private ICouchbaseCollection _collection;
        private string _currentUser;
        private string _currentTransactionCode = "";

        // Dictionaries để mapping code -> name
        private Dictionary<string, string> _supplierCodeToName = new Dictionary<string, string>();
        private Dictionary<string, string> _productCodeToName = new Dictionary<string, string>();

        public frmReceiptTransaction()
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.Load += FrmReceiptTransaction_Load;

            // Đăng ký các sự kiện
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
            nudQuantity.ValueChanged += CalculateTotalPrice;
            nudUnitPrice.ValueChanged += CalculateTotalPrice;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmReceiptTransaction_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                ConfigureDataGridView();
                await LoadSuppliers();
                await LoadProducts();
                GenerateTransactionCode();

                txtStaff.Text = _currentUser;
                dtpTransactionDate.Value = DateTime.Now;

                // Set numeric controls
                nudQuantity.Minimum = 1;
                nudQuantity.Maximum = 10000;
                nudUnitPrice.Minimum = 0;
                nudUnitPrice.Maximum = 100000000;
                nudUnitPrice.DecimalPlaces = 0;

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

                _bucket = await _cluster.BucketAsync("mini_mart");
                _collection = _bucket.Scope("inventory").Collection("inventory_transactions");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMasp",
                HeaderText = "Mã SP",
                DataPropertyName = "Masp",
                FillWeight = 10,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTensp",
                HeaderText = "Tên SP",
                DataPropertyName = "Tensp",
                FillWeight = 20,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSl",
                HeaderText = "Số Lượng",
                DataPropertyName = "Sl",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDonvi",
                HeaderText = "Đơn Vị",
                DataPropertyName = "Donvi",
                FillWeight = 8,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDongia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "Dongia",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colThanhtien",
                HeaderText = "Thành Tiền",
                DataPropertyName = "Thanhtien",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private async Task LoadSuppliers()
        {
            try
            {
                string query = @"
                    SELECT s.code, s.name, META(s).id as _id
                    FROM `mini_mart`.`masterdata`.`suppliers` AS s
                    WHERE s.type = 'supplier' AND s.is_active = true
                    ORDER BY s.name
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var suppliers = await result.Rows.ToListAsync();

                cboSupplier.Items.Clear();
                _supplierCodeToName.Clear();

                foreach (var sup in suppliers)
                {
                    string supCode = sup.code?.ToString() ?? "";
                    string supId = sup._id?.ToString() ?? "";
                    string supName = sup.name?.ToString() ?? "";

                    _supplierCodeToName[supCode] = supName;
                    _supplierCodeToName[$"supplier::{supCode}"] = supName;
                    if (!string.IsNullOrEmpty(supId))
                    {
                        _supplierCodeToName[supId] = supName;
                    }

                    cboSupplier.Items.Add(supName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProducts()
        {
            try
            {
                string query = @"
                    SELECT p.code, p.name, p.unit, p.cost_price, META(p).id as _id
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product' AND p.is_active = true
                    ORDER BY p.code
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var products = await result.Rows.ToListAsync();

                cboProduct.Items.Clear();
                _productCodeToName.Clear();

                foreach (var prod in products)
                {
                    string prodCode = prod.code?.ToString() ?? "";
                    string prodId = prod._id?.ToString() ?? "";
                    string prodName = prod.name?.ToString() ?? "";

                    _productCodeToName[prodCode] = prodName;
                    _productCodeToName[$"product::{prodCode}"] = prodName;
                    if (!string.IsNullOrEmpty(prodId))
                    {
                        _productCodeToName[prodId] = prodName;
                    }

                    cboProduct.Items.Add($"{prodCode} - {prodName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateTransactionCode()
        {
            try
            {
                // Tạo mã phiếu nhập dạng PN + ngày + số thứ tự
                string today = DateTime.Now.ToString("yyyyMMdd");
                int count = 1;

                // Tính toán số thứ tự trong ngày
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    count++;
                }

                txtTransactionCode.Text = $"PN{today}{count:D2}";
                _currentTransactionCode = txtTransactionCode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sinh mã phiếu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cluster == null) return;

            try
            {
                if (cboProduct.SelectedIndex >= 0)
                {
                    string selectedItem = cboProduct.SelectedItem.ToString();
                    string productCode = selectedItem.Split('-')[0].Trim();

                    // Load thông tin sản phẩm
                    LoadProductDetails(productCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void LoadProductDetails(string productCode)
        {
            try
            {
                string query = $@"
            SELECT p.code, p.name, p.unit, p.cost_price
            FROM `mini_mart`.`masterdata`.`products` AS p
            WHERE p.code = '{productCode}' AND p.type = 'product'
        ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var product = await result.Rows.FirstOrDefaultAsync();

                if (product != null)
                {
                    txtProductCode.Text = product.code;
                    txtProductName.Text = product.name;
                    txtUnit.Text = product.unit;
                    nudUnitPrice.Value = Convert.ToDecimal(product.cost_price ?? 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load chi tiết sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalPrice(object sender, EventArgs e)
        {
            decimal totalPrice = nudQuantity.Value * nudUnitPrice.Value;
            txtTotalPrice.Text = totalPrice.ToString("N0");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCode.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nudQuantity.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if product already exists
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["colMasp"].Value?.ToString() == txtProductCode.Text)
                    {
                        MessageBox.Show("Sản phẩm đã có trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Add to DataGridView
                DataTable dt = (DataTable)dataGridView1.DataSource ?? new DataTable();
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Masp", typeof(string));
                    dt.Columns.Add("Tensp", typeof(string));
                    dt.Columns.Add("Sl", typeof(int));
                    dt.Columns.Add("Donvi", typeof(string));
                    dt.Columns.Add("Dongia", typeof(decimal));
                    dt.Columns.Add("Thanhtien", typeof(decimal));
                }

                DataRow newRow = dt.NewRow();
                newRow["Masp"] = txtProductCode.Text;
                newRow["Tensp"] = txtProductName.Text;
                newRow["Sl"] = (int)nudQuantity.Value;
                newRow["Donvi"] = txtUnit.Text;
                newRow["Dongia"] = nudUnitPrice.Value;
                newRow["Thanhtien"] = nudQuantity.Value * nudUnitPrice.Value;

                dt.Rows.Add(newRow);
                dataGridView1.DataSource = dt;

                CalculateTotalAmount();
                ClearProductFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataTable dt = (DataTable)dataGridView1.DataSource;
                        dt.Rows[dataGridView1.SelectedRows[0].Index].Delete();
                        CalculateTotalAmount();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cluster == null)
                {
                    MessageBox.Show("Chưa kết nối Couchbase!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cboSupplier.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm sản phẩm vào phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;

                string transactionId = $"transaction::{_currentTransactionCode}";
                var items = new List<dynamic>();

                DataTable dt = (DataTable)dataGridView1.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    decimal unitPrice = Convert.ToDecimal(row["Dongia"] ?? 0);
                    decimal quantity = Convert.ToDecimal(row["Sl"] ?? 0);

                    items.Add(new
                    {
                        product_id = $"product::{row["Masp"]}",
                        product_code = row["Masp"].ToString(),
                        product_name = row["Tensp"].ToString(),
                        quantity = Convert.ToInt32(quantity),
                        unit = row["Donvi"].ToString(),
                        unit_price = unitPrice,
                        total_price = quantity * unitPrice
                    });
                }

                decimal totalAmount = 0;
                decimal.TryParse(txtTotalAmount.Text?.Replace(",", ""), out totalAmount);

                string supplierName = cboSupplier.SelectedItem?.ToString() ?? "";
                string supplierId = GetSupplierCodeByName(supplierName);

                var transaction = new Dictionary<string, object>
                {
                    ["type"] = "inventory_transaction",
                    ["transaction_type"] = "RECEIPT",
                    ["transaction_code"] = _currentTransactionCode,
                    ["transaction_date"] = dtpTransactionDate.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    ["supplier_id"] = supplierId,
                    ["staff_id"] = $"user::{_currentUser}",
                    ["total_amount"] = totalAmount,
                    ["note"] = txtNote.Text,
                    ["status"] = "COMPLETED",
                    ["items"] = items,
                    ["created_at"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    ["created_by"] = _currentUser,
                    ["updated_at"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    ["updated_by"] = _currentUser
                };

                await _collection.InsertAsync(transactionId, transaction);

                MessageBox.Show("Lưu phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        private void CalculateTotalAmount()
        {
            decimal total = 0;
            DataTable dt = (DataTable)dataGridView1.DataSource;

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    decimal thanhtien = Convert.ToDecimal(row["Thanhtien"] ?? 0);
                    total += thanhtien;
                }
            }

            txtTotalAmount.Text = total.ToString("N0");
        }
        private void ClearProductFields()
        {
            cboProduct.SelectedIndex = -1;
            txtProductCode.Clear();
            txtProductName.Clear();
            txtUnit.Clear();
            nudQuantity.Value = 1;
            nudUnitPrice.Value = 0;
            txtTotalPrice.Clear();
        }

        private void ClearForm()
        {
            dataGridView1.DataSource = null;
            cboSupplier.SelectedIndex = -1;
            txtNote.Clear();
            txtTotalAmount.Clear();
            ClearProductFields();
            GenerateTransactionCode();
            txtStaff.Text = _currentUser;
            dtpTransactionDate.Value = DateTime.Now;
        }

        private string GetSupplierCodeByName(string supplierName)
        {
            if (string.IsNullOrEmpty(supplierName)) return "";
            var entry = _supplierCodeToName.FirstOrDefault(x => x.Value == supplierName);
            return entry.Key ?? "";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cluster?.Dispose();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
