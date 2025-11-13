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
using ZXing;
using ZXing.Common;
using ClosedXML.Excel;

namespace Mini_Mart
{
    public partial class frmTransactionHistory : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        public frmTransactionHistory()
        {
            InitializeComponent();
            this.Load += FrmTransactionHistory_Load;

            btnView.Click += BtnViewDetail_Click;
            btnExport.Click += BtnExport_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvTransactions.CellDoubleClick += DgvTransactions_CellDoubleClick;
        }

        private void FrmTransactionHistory_Load(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void frmTransactionHistory_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                ConfigureDataGridView();

                // Set date range defaults
                dtpFromDate.Value = DateTime.Now.AddMonths(-1);
                dtpToDate.Value = DateTime.Now;

                // Load loại giao dịch
                cboTransactionType.Items.Add("Tất cả");
                cboTransactionType.Items.Add("RECEIPT");
                cboTransactionType.Items.Add("SALE");
                cboTransactionType.Items.Add("ADJUSTMENT");
                cboTransactionType.SelectedIndex = 0;

                // Load trạng thái
                cboStatus.Items.Add("Tất cả");
                cboStatus.Items.Add("COMPLETED");
                cboStatus.Items.Add("PENDING");
                cboStatus.Items.Add("CANCELLED");
                cboStatus.SelectedIndex = 0;

                await LoadTransactionHistory();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ConfigureDataGridView()
        {
            dgvTransactions.AutoGenerateColumns = false;
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.RowTemplate.Height = 25;
            dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransactions.RowHeadersVisible = false;

            dgvTransactions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTransactions.Font, FontStyle.Bold);

            dgvTransactions.Columns.Clear();

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã Phiếu",
                DataPropertyName = "Code",
                FillWeight = 12,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colType",
                HeaderText = "Loại",
                DataPropertyName = "Type",
                FillWeight = 10,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDate",
                HeaderText = "Ngày",
                DataPropertyName = "Date",
                FillWeight = 12,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSupplier",
                HeaderText = "Nhà cung cấp",
                DataPropertyName = "Supplier",
                FillWeight = 15,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStaff",
                HeaderText = "Nhân viên",
                DataPropertyName = "Staff",
                FillWeight = 12,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "Tổng tiền",
                DataPropertyName = "Amount",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                FillWeight = 10,
                ReadOnly = true
            });
        }

        private async Task LoadTransactionHistory()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string fromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                string toDate = dtpToDate.Value.ToString("yyyy-MM-dd");
                string transactionType = cboTransactionType.SelectedItem?.ToString() ?? "Tất cả";
                string status = cboStatus.SelectedItem?.ToString() ?? "Tất cả";
                string searchText = txtSearch.Text.Trim().ToLower();

                List<string> whereClauses = new List<string>
                {
                    "t.type = 'inventory_transaction'",
                    $"t.transaction_date >= '{fromDate}T00:00:00Z'",
                    $"t.transaction_date <= '{toDate}T23:59:59Z'"
                };

                if (transactionType != "Tất cả")
                {
                    whereClauses.Add($"t.transaction_type = '{transactionType}'");
                }

                if (status != "Tất cả")
                {
                    whereClauses.Add($"t.status = '{status}'");
                }

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClauses.Add($"(LOWER(t.transaction_code) LIKE '%{searchText}%' OR LOWER(t.note) LIKE '%{searchText}%')");
                }

                string whereClause = "WHERE " + string.Join(" AND ", whereClauses);

                string query = $@"
                    SELECT
                        t.transaction_code,
                        t.transaction_type,
                        t.transaction_date,
                        t.supplier_id,
                        t.staff_id,
                        t.total_amount,
                        t.status,
                        t.note
                    FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
                    {whereClause}
                    ORDER BY t.transaction_date DESC
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Supplier", typeof(string));
                dt.Columns.Add("Staff", typeof(string));
                dt.Columns.Add("Amount", typeof(decimal));
                dt.Columns.Add("Status", typeof(string));

                foreach (var item in rows)
                {
                    DataRow row = dt.NewRow();
                    row["Code"] = item.transaction_code?.ToString() ?? "";
                    row["Type"] = GetTransactionTypeName(item.transaction_type?.ToString());
                    row["Date"] = Convert.ToDateTime(item.transaction_date).ToString("dd/MM/yyyy HH:mm");
                    row["Supplier"] = GetSupplierName(item.supplier_id?.ToString());
                    row["Staff"] = GetStaffName(item.staff_id?.ToString());
                    row["Amount"] = Convert.ToDecimal(item.total_amount ?? 0);
                    row["Status"] = item.status?.ToString() ?? "";
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load lịch sử: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private string GetTransactionTypeName(string type)
        {
            return type switch
            {
                "RECEIPT" => "Nhập hàng",
                "SALE" => "Bán hàng",
                "ADJUSTMENT" => "Điều chỉnh",
                _ => type
            };
        }

        private string GetSupplierName(string supplierId)
        {
            // Có thể load từ cache hoặc database
            return supplierId?.Replace("supplier::", "")?.Replace("user::", "") ?? "";
        }

        private string GetStaffName(string staffId)
        {
            return staffId?.Replace("user::", "") ?? "";
        }

        private void DgvTransactions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnViewDetail_Click(null, null);
            }
        }

        private void BtnViewDetail_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                string transactionCode = dgvTransactions.SelectedRows[0].Cells["colCode"].Value?.ToString();
                if (!string.IsNullOrEmpty(transactionCode))
                {
                    Form4 frm = new Form4(transactionCode);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            await LoadTransactionHistory();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTransactions.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = $"LichSuGiaoDich_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(string filePath)
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Lịch sử giao dịch");

                worksheet.Cell(1, 1).Value = "Mã Phiếu";
                worksheet.Cell(1, 2).Value = "Loại";
                worksheet.Cell(1, 3).Value = "Ngày";
                worksheet.Cell(1, 4).Value = "Nhà cung cấp";
                worksheet.Cell(1, 5).Value = "Nhân viên";
                worksheet.Cell(1, 6).Value = "Tổng tiền";
                worksheet.Cell(1, 7).Value = "Trạng thái";

                var headerRange = worksheet.Range(1, 1, 1, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightBlue;

                DataTable dt = (DataTable)dgvTransactions.DataSource;
                int row = 2;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = (XLCellValue)dr["Code"];
                    worksheet.Cell(row, 2).Value = (XLCellValue)dr["Type"];
                    worksheet.Cell(row, 3).Value = (XLCellValue)dr["Date"];
                    worksheet.Cell(row, 4).Value = (XLCellValue)dr["Supplier"];
                    worksheet.Cell(row, 5).Value = (XLCellValue)dr["Staff"];
                    worksheet.Cell(row, 6).Value = Convert.ToDecimal(dr["Amount"]);
                    worksheet.Cell(row, 7).Value = (XLCellValue)dr["Status"];
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cluster?.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
