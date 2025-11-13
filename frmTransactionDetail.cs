using Couchbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Mini_Mart
{
    public partial class Form4 : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private string _transactionCode;


        public Form4(string transactionCode)
        {
            InitializeComponent();
            _transactionCode = transactionCode;
            this.Load += FrmTransactionDetail_Load;
        }
        private async void FrmTransactionDetail_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                ConfigureDataGridView();
                await LoadTransactionDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        private void ConfigureDataGridView()
        {
            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.RowTemplate.Height = 25;
            dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetails.RowHeadersVisible = false;

            dgvDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDetails.Font, FontStyle.Bold);

            dgvDetails.Columns.Clear();

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã SP",
                DataPropertyName = "ProductCode",
                FillWeight = 12,
                ReadOnly = true
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên Sản Phẩm",
                DataPropertyName = "ProductName",
                FillWeight = 20,
                ReadOnly = true
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity",
                HeaderText = "Số Lượng",
                DataPropertyName = "Quantity",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnit",
                HeaderText = "Đơn Vị",
                DataPropertyName = "Unit",
                FillWeight = 10,
                ReadOnly = true
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                HeaderText = "Đơn Giá",
                DataPropertyName = "UnitPrice",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalPrice",
                HeaderText = "Thành Tiền",
                DataPropertyName = "TotalPrice",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }
        private async Task LoadTransactionDetail()
        {
            try
            {
                string documentId = $"transaction::{_transactionCode}";
                var collection = _bucket.Scope("inventory").Collection("inventory_transactions");
                var result = await collection.GetAsync(documentId);
                var content = result.ContentAs<Dictionary<string, object>>();

                // Load thông tin chung - 6 field
                txtTransactionCode.Text = content["transaction_code"]?.ToString() ?? "";
                txtLoaiGiaoDich.Text = GetTransactionTypeName(content["transaction_type"]?.ToString());  // THÊM DÒNG NÀY
                txtTransactionDate.Text = Convert.ToDateTime(content["transaction_date"]).ToString("dd/MM/yyyy HH:mm");
                txtGhiChu.Text = content["note"]?.ToString() ?? "";
                txtTrangThai.Text = GetSupplierName(content["supplier_id"]?.ToString());
                txtStaff.Text = GetStaffName(content["staff_id"]?.ToString());

                // Load chi tiết sản phẩm
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductCode", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("UnitPrice", typeof(decimal));
                dt.Columns.Add("TotalPrice", typeof(decimal));

                var items = content["items"] as List<object>;
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        var itemDict = item as Dictionary<string, object>;
                        if (itemDict != null)
                        {
                            DataRow row = dt.NewRow();
                            row["ProductCode"] = itemDict["product_code"]?.ToString() ?? "";
                            row["ProductName"] = itemDict["product_name"]?.ToString() ?? "";
                            row["Quantity"] = Convert.ToInt32(itemDict["quantity"] ?? 0);
                            row["Unit"] = itemDict["unit"]?.ToString() ?? "";
                            row["UnitPrice"] = Convert.ToDecimal(itemDict["unit_price"] ?? 0);
                            row["TotalPrice"] = Convert.ToDecimal(itemDict["total_price"] ?? 0);
                            dt.Rows.Add(row);
                        }
                    }
                }

                dgvDetails.DataSource = dt;

                // Tính tổng tiền
                decimal totalAmount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["TotalPrice"]);
                }

                txtTotalAmount.Text = totalAmount.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetTransactionTypeName(string type)
        {
            return type switch
            {
                "RECEIPT" => "Nhập hàng",
                "SALE" => "Bán hàng",
                "ADJUSTMENT" => "Điều chỉnh",
                _ => type ?? ""
            };
        }

        // Thêm hàm này
        private string GetSupplierName(string supplierId)
        {
            return supplierId?.Replace("supplier::", "")?.Replace("user::", "") ?? "";
        }

        // Thêm hàm này
        private string GetStaffName(string staffId)
        {
            return staffId?.Replace("user::", "") ?? "";
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cluster?.Dispose();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
