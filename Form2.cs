using ClosedXML.Excel;
using Couchbase;
using Couchbase.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace Mini_Mart
{
    public partial class Form2 : Form
    {
        private ICluster _cluster;
        private Dictionary<string, string> _categoryCodeToName = new Dictionary<string, string>();
        private string _currentSortOrder = "ASC";
        private bool _isUpdatingRevenueFilter = false;

        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;

            // Đăng ký sự kiện tab Tồn kho
            txtName.TextChanged += TxtName_TextChanged;
            cboCategory.SelectedIndexChanged += CboCategory_SelectedIndexChanged;
            btnIncrease.Click += BtnIncrease_Click;
            btnDecrease.Click += BtnDecrease_Click;
            btnExport.Click += BtnExport_Click;
            // Đăng ký sự kiện tab Nhập/Xuất
            cboLCT.SelectedIndexChanged += CboLCT_SelectedIndexChanged;
            dtpNX_Start.ValueChanged += DtpNX_ValueChanged;
            dtpNX_End.ValueChanged += DtpNX_ValueChanged;
            btnReset.Click += BtnReset_Click;
            btnExport_NX.Click += BtnExport_NX_Click;
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();

                // ========== TAB TỒN KHO ==========
                ConfigureDataGridView();
                await LoadCategories(); // Load categories cho cả 2 tab

                cboCategory.Items.Insert(0, "Tất cả");
                cboCategory.SelectedIndex = 0;

                await LoadInventoryData();
                await LoadInventoryCharts();

                // ========== TAB DOANH THU ==========
                await InitializeRevenueTab();
                await LoadRevenueByDate();
                await LoadRevenueCharts();
                // ========== TAB NHẬP/XUẤT ==========
                await InitializeImportExportTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private async Task LoadCategories()
        {
            try
            {
                string query = @"
                    SELECT c.code, c.name, META(c).id as _id
                    FROM `mini_mart`.`masterdata`.`categories` AS c
                    WHERE c.type = 'category'
                    ORDER BY c.name
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var categories = await result.Rows.ToListAsync();

                cboCategory.Items.Clear();
                _categoryCodeToName.Clear();

                foreach (var cat in categories)
                {
                    string catCode = cat.code?.ToString() ?? "";
                    string catId = cat._id?.ToString() ?? "";
                    string catName = cat.name?.ToString() ?? "";

                    _categoryCodeToName[catCode] = catName;
                    _categoryCodeToName[$"category::{catCode}"] = catName;
                    if (!string.IsNullOrEmpty(catId))
                    {
                        _categoryCodeToName[catId] = catName;
                    }

                    cboCategory.Items.Add(catName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dtgvTonKho.AutoGenerateColumns = false;
            dtgvTonKho.AllowUserToAddRows = false;
            dtgvTonKho.RowTemplate.Height = 35;
            dtgvTonKho.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvTonKho.RowHeadersVisible = false;

            dtgvTonKho.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvTonKho.ColumnHeadersDefaultCellStyle.Font = new Font(dtgvTonKho.Font, FontStyle.Bold);

            dtgvTonKho.Columns.Clear();

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã SP",
                DataPropertyName = "Code",
                FillWeight = 10,
                ReadOnly = true
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "Name",
                FillWeight = 25,
                ReadOnly = true
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCategory",
                HeaderText = "Loại SP",
                DataPropertyName = "CategoryName",
                FillWeight = 15,
                ReadOnly = true
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnit",
                HeaderText = "Đơn vị",
                DataPropertyName = "Unit",
                FillWeight = 10,
                ReadOnly = true
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMinStock",
                HeaderText = "Min",
                DataPropertyName = "MinStock",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCurrentStock",
                HeaderText = "Tồn kho",
                DataPropertyName = "CurrentStock",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font(dtgvTonKho.Font, FontStyle.Bold)
                }
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaxStock",
                HeaderText = "Max",
                DataPropertyName = "MaxStock",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        private async Task LoadInventoryData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<string> whereClauses = new List<string> { "p.type = 'product'" };

                // Lọc theo tên sản phẩm
                string searchName = txtName.Text.Trim();
                if (!string.IsNullOrEmpty(searchName))
                {
                    whereClauses.Add($"LOWER(p.name) LIKE '%{searchName.ToLower()}%'");
                }

                // Lọc theo category
                string selectedCategory = cboCategory.SelectedItem?.ToString() ?? "";
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "Tất cả")
                {
                    var matchingCodes = _categoryCodeToName
                        .Where(x => x.Value == selectedCategory)
                        .Select(x => x.Key)
                        .ToList();

                    if (matchingCodes.Any())
                    {
                        string categoryFilter = string.Join(" OR ", matchingCodes.Select(code => $"p.category_id = '{code}'"));
                        whereClauses.Add($"({categoryFilter})");
                    }
                }

                string whereClause = "WHERE " + string.Join(" AND ", whereClauses);

                // Sắp xếp theo current_stock
                string orderBy = $"ORDER BY p.current_stock {_currentSortOrder}";

                string query = $@"
                    SELECT
                        p.code,
                        p.name,
                        p.category_id,
                        p.unit,
                        p.current_stock,
                        p.min_stock,
                        p.max_stock
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    {whereClause}
                    {orderBy}
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("CategoryId", typeof(string));
                dt.Columns.Add("CategoryName", typeof(string));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("MinStock", typeof(int));
                dt.Columns.Add("CurrentStock", typeof(int));
                dt.Columns.Add("MaxStock", typeof(int));
                dt.Columns.Add("Status", typeof(string));

                foreach (var item in rows)
                {
                    var row = dt.NewRow();
                    row["Code"] = item.code?.ToString() ?? "";
                    row["Name"] = item.name?.ToString() ?? "";

                    string categoryId = item.category_id?.ToString() ?? "";
                    row["CategoryId"] = categoryId;
                    row["CategoryName"] = _categoryCodeToName.ContainsKey(categoryId) ? _categoryCodeToName[categoryId] : "";

                    row["Unit"] = item.unit?.ToString() ?? "";

                    int minStock = Convert.ToInt32(item.min_stock ?? 0);
                    int currentStock = Convert.ToInt32(item.current_stock ?? 0);
                    int maxStock = Convert.ToInt32(item.max_stock ?? 0);

                    row["MinStock"] = minStock;
                    row["CurrentStock"] = currentStock;
                    row["MaxStock"] = maxStock;

                    string status;
                    if (currentStock <= minStock)
                    {
                        status = "Thiếu hàng";
                    }
                    else if (currentStock >= maxStock)
                    {
                        status = "Tồn cao";
                    }
                    else
                    {
                        status = "Bình thường";
                    }
                    row["Status"] = status;

                    dt.Rows.Add(row);
                }

                dtgvTonKho.DataSource = dt;

                // Tô màu các hàng
                foreach (DataGridViewRow dgvRow in dtgvTonKho.Rows)
                {
                    string status = dgvRow.Cells["colStatus"].Value?.ToString() ?? "";
                    if (status == "Thiếu hàng")
                    {
                        dgvRow.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else if (status == "Tồn cao")
                    {
                        dgvRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadInventoryCharts()
        {
            try
            {
                // Query top 5 ít nhất và nhiều nhất
                string queryLowest = @"
                    SELECT p.name, p.current_stock
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product' AND p.current_stock > 0
                    ORDER BY p.current_stock ASC
                    LIMIT 5
                ";

                string queryHighest = @"
                    SELECT p.name, p.current_stock
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product'
                    ORDER BY p.current_stock DESC
                    LIMIT 5
                ";

                var resultLowest = await _cluster.QueryAsync<dynamic>(queryLowest);
                var rowsLowest = await resultLowest.Rows.ToListAsync();

                var resultHighest = await _cluster.QueryAsync<dynamic>(queryHighest);
                var rowsHighest = await resultHighest.Rows.ToListAsync();

                // Tạo dữ liệu cho biểu đồ
                var dataLowest = new List<Tuple<string, int>>();
                foreach (var item in rowsLowest)
                {
                    string name = item.name?.ToString() ?? "";
                    int stock = Convert.ToInt32(item.current_stock ?? 0);

                    if (name.Length > 12)
                        name = name.Substring(0, 10) + "..";

                    dataLowest.Add(new Tuple<string, int>(name, stock));
                }

                var dataHighest = new List<Tuple<string, int>>();
                foreach (var item in rowsHighest)
                {
                    string name = item.name?.ToString() ?? "";
                    int stock = Convert.ToInt32(item.current_stock ?? 0);

                    if (name.Length > 12)
                        name = name.Substring(0, 10) + "..";

                    dataHighest.Add(new Tuple<string, int>(name, stock));
                }

                // Vẽ 2 biểu đồ
                DrawDualCharts(dataLowest, dataHighest);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DrawDualCharts(List<Tuple<string, int>> dataLowest, List<Tuple<string, int>> dataHighest)
        {
            panelTonKho.Controls.Clear();

            if (dataLowest.Count == 0 && dataHighest.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = "Không có dữ liệu",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 12)
                };
                panelTonKho.Controls.Add(lblNoData);
                return;
            }

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Tăng kích thước để đủ chỗ
            int chartWidth = Math.Max(panelTonKho.Width, 1000);
            int chartHeight = Math.Max(panelTonKho.Height, 450);

            Bitmap bitmap = new Bitmap(chartWidth, chartHeight);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Chia đôi màn hình với khoảng cách giữa 2 biểu đồ
                int spacing = 40;
                int halfWidth = (bitmap.Width - spacing) / 2;

                // Vẽ biểu đồ bên trái (Tồn kho ít nhất)
                DrawSingleChart(g, dataLowest, new Rectangle(10, 0, halfWidth - 10, bitmap.Height),
                    "Top 5 tồn kho ít nhất", Color.Coral);

                // Vẽ biểu đồ bên phải (Tồn kho nhiều nhất)
                DrawSingleChart(g, dataHighest, new Rectangle(halfWidth + spacing, 0, halfWidth - 10, bitmap.Height),
                    "Top 5 tồn kho nhiều nhất", Color.SteelBlue);
            }

            pictureBox.Image = bitmap;
            panelTonKho.Controls.Add(pictureBox);
        }

        private void DrawSingleChart(Graphics g, List<Tuple<string, int>> data, Rectangle bounds, string title, Color barColor)
        {
            if (data.Count == 0) return;

            // Tiêu đề
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
            SizeF titleSize = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, Brushes.Black,
                bounds.X + (bounds.Width - titleSize.Width) / 2, bounds.Y + 10);

            // Kích thước vùng vẽ - TĂNG MARGIN DƯỚI ĐỂ HIỂN THỊ TÊN
            int marginLeft = 50;
            int marginRight = 20;
            int marginTop = 40;
            int marginBottom = 100; // Tăng từ 70 lên 100

            int chartTop = bounds.Y + marginTop;
            int chartBottom = bounds.Y + bounds.Height - marginBottom;
            int plotHeight = chartBottom - chartTop;
            int plotWidth = bounds.Width - marginLeft - marginRight;

            if (plotHeight <= 0 || plotWidth <= 0) return;

            // Tìm max
            int maxValue = data.Max(d => d.Item2);
            if (maxValue == 0) maxValue = 1;

            int roundedMax = (int)Math.Ceiling(maxValue / 50.0) * 50;
            if (roundedMax < maxValue) roundedMax = maxValue;

            // Vẽ trục
            int axisX = bounds.X + marginLeft;
            Pen gridPen = new Pen(Color.LightGray, 1);
            Pen axisPen = new Pen(Color.Black, 2);

            g.DrawLine(axisPen, axisX, chartTop, axisX, chartBottom);
            g.DrawLine(axisPen, axisX, chartBottom, bounds.Right - marginRight, chartBottom);

            // Mốc trục Y
            int steps = 5;
            Font labelFont = new Font("Arial", 8);

            for (int i = 0; i <= steps; i++)
            {
                int yValue = roundedMax * i / steps;
                int yPos = chartBottom - (int)((double)yValue / roundedMax * plotHeight);

                if (i > 0 && i < steps)
                {
                    g.DrawLine(gridPen, axisX, yPos, bounds.Right - marginRight, yPos);
                }

                g.DrawLine(Pens.Black, axisX - 5, yPos, axisX, yPos);

                string label = yValue.ToString();
                SizeF labelSize = g.MeasureString(label, labelFont);
                g.DrawString(label, labelFont, Brushes.Black,
                    axisX - labelSize.Width - 8, yPos - labelSize.Height / 2);
            }

            // Vẽ cột
            int barWidth = Math.Min(50, (plotWidth - 20) / data.Count - 12);
            int spacing2 = Math.Max(8, (plotWidth - (barWidth * data.Count)) / (data.Count + 1));
            int x = axisX + spacing2;

            Font valueFont = new Font("Arial", 9, FontStyle.Bold);
            Font nameFont = new Font("Arial", 7);

            for (int i = 0; i < data.Count; i++)
            {
                string name = data[i].Item1;
                int value = data[i].Item2;

                int barHeight = (int)((double)value / roundedMax * plotHeight);
                if (barHeight < 5 && value > 0) barHeight = 5;

                Rectangle bar = new Rectangle(x, chartBottom - barHeight, barWidth, barHeight);

                // Gradient đẹp hơn
                Color lightColor = Color.FromArgb(180, barColor);
                using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        bar, barColor, lightColor, 90f))
                {
                    g.FillRectangle(brush, bar);
                }

                g.DrawRectangle(new Pen(Color.FromArgb(100, Color.Black), 2), bar);

                // Giá trị trên cột
                string valueText = value.ToString();
                SizeF valueSize = g.MeasureString(valueText, valueFont);
                g.DrawString(valueText, valueFont, Brushes.Black,
                    x + (barWidth - valueSize.Width) / 2,
                    Math.Max(chartTop, chartBottom - barHeight - valueSize.Height - 5));

                // Tên sản phẩm - xoay 45 độ
                g.TranslateTransform(x + barWidth / 2, chartBottom + 8);
                g.RotateTransform(45);
                g.DrawString(name, nameFont, Brushes.Black, 0, 0);
                g.ResetTransform();

                x += barWidth + spacing2;
            }

            // Label trục Y
            Font axisFont = new Font("Arial", 9, FontStyle.Italic);
            string yLabel = "Số lượng";
            SizeF yLabelSize = g.MeasureString(yLabel, axisFont);

            // Xoay 90 độ cho label trục Y
            g.TranslateTransform(bounds.X + 12, chartTop + plotHeight / 2 + yLabelSize.Width / 2);
            g.RotateTransform(-90);
            g.DrawString(yLabel, axisFont, Brushes.DarkGray, 0, 0);
            g.ResetTransform();
        }


        private async void TxtName_TextChanged(object sender, EventArgs e)
        {
            await LoadInventoryData();
        }

        private async void CboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadInventoryData();
        }

        private async void BtnIncrease_Click(object sender, EventArgs e)
        {
            _currentSortOrder = "ASC";
            await LoadInventoryData();
        }

        private async void BtnDecrease_Click(object sender, EventArgs e)
        {
            _currentSortOrder = "DESC";
            await LoadInventoryData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvTonKho.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = $"BaoCaoTonKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

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
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Báo cáo tồn kho");

                // Tiêu đề
                worksheet.Cell(1, 1).Value = "Mã SP";
                worksheet.Cell(1, 2).Value = "Tên sản phẩm";
                worksheet.Cell(1, 3).Value = "Loại SP";
                worksheet.Cell(1, 4).Value = "Đơn vị";
                worksheet.Cell(1, 5).Value = "Min";
                worksheet.Cell(1, 6).Value = "Tồn kho";
                worksheet.Cell(1, 7).Value = "Max";
                worksheet.Cell(1, 8).Value = "Trạng thái";

                var headerRange = worksheet.Range(1, 1, 1, 8);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Dữ liệu
                var dt = (DataTable)dtgvTonKho.DataSource;
                int row = 2;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dr["Code"].ToString();
                    worksheet.Cell(row, 2).Value = dr["Name"].ToString();
                    worksheet.Cell(row, 3).Value = dr["CategoryName"].ToString();
                    worksheet.Cell(row, 4).Value = dr["Unit"].ToString();
                    worksheet.Cell(row, 5).Value = Convert.ToInt32(dr["MinStock"]);
                    worksheet.Cell(row, 6).Value = Convert.ToInt32(dr["CurrentStock"]);
                    worksheet.Cell(row, 7).Value = Convert.ToInt32(dr["MaxStock"]);
                    worksheet.Cell(row, 8).Value = dr["Status"].ToString();

                    // Tô màu theo trạng thái
                    string status = dr["Status"].ToString();
                    if (status == "Thiếu hàng")
                    {
                        worksheet.Range(row, 1, row, 8).Style.Fill.BackgroundColor = XLColor.LightCoral;
                    }
                    else if (status == "Tồn cao")
                    {
                        worksheet.Range(row, 1, row, 8).Style.Fill.BackgroundColor = XLColor.LightYellow;
                    }

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
        // ==================== PHẦN 1: THỐNG KÊ DOANH THU THEO NGÀY ====================

        private async Task InitializeRevenueTab()
        {
            // Thiết lập DateTimePicker không cho chọn tương lai
            dtpDT.MaxDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            dtpDT.Value = DateTime.Now.Date;
            dtpDT.Format = DateTimePickerFormat.Short;
            dtpDT.ValueChanged += DtpDT_ValueChanged;

            // Thiết lập DateTimePicker cho khoảng thời gian
            dtpDT_Start.Value = DateTime.Now.AddMonths(-1).Date;
            dtpDT_End.MaxDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            dtpDT_End.Value = DateTime.Now.Date;
            dtpDT_Start.ValueChanged += DtpDT_Range_ValueChanged;
            dtpDT_End.ValueChanged += DtpDT_Range_ValueChanged;

            // Thiết lập các sự kiện
            txtDT_SP.TextChanged += TxtDT_SP_TextChanged;
            cboCategory_DT.SelectedIndexChanged += CboCategory_DT_SelectedIndexChanged;
            btnAll_Loai.Click += BtnAll_Loai_Click;
            btnAll_SP.Click += BtnAll_SP_Click;
            btnExport_DT.Click += BtnExport_DT_Click;

            // Cấu hình DataGridView
            ConfigureRevenueDataGridView();
            ConfigureRevenueDetailDataGridView();

            // Load categories cho tab doanh thu
            await LoadCategoriesForRevenue();
        }

        private void ConfigureRevenueDataGridView()
        {
            dtgvDT.AutoGenerateColumns = false;
            dtgvDT.AllowUserToAddRows = false;
            dtgvDT.RowTemplate.Height = 35;
            dtgvDT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvDT.RowHeadersVisible = false;

            dtgvDT.Columns.Clear();

            dtgvDT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductCode",
                HeaderText = "Mã SP",
                DataPropertyName = "ProductCode",
                FillWeight = 15
            });

            dtgvDT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "ProductName",
                FillWeight = 30
            });

            dtgvDT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantitySold",
                HeaderText = "Số lượng bán",
                DataPropertyName = "QuantitySold",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvDT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRevenue",
                HeaderText = "Doanh thu",
                DataPropertyName = "Revenue",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void ConfigureRevenueDetailDataGridView()
        {
            dtgvDT_CT.AutoGenerateColumns = false;
            dtgvDT_CT.AllowUserToAddRows = false;
            dtgvDT_CT.RowTemplate.Height = 35;
            dtgvDT_CT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvDT_CT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvDT_CT.RowHeadersVisible = false;

            dtgvDT_CT.Columns.Clear();

            dtgvDT_CT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colItem",
                HeaderText = "Sản phẩm/Loại",
                DataPropertyName = "ItemName",
                FillWeight = 40
            });

            dtgvDT_CT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQty",
                HeaderText = "Số lượng",
                DataPropertyName = "Quantity",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvDT_CT.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRev",
                HeaderText = "Doanh thu",
                DataPropertyName = "Revenue",
                FillWeight = 40,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font(dtgvDT_CT.Font, FontStyle.Bold)
                }
            });
        }

        private async Task LoadRevenueByDate()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime selectedDate = dtpDT.Value.Date;
                DateTime startOfDay = selectedDate;
                DateTime endOfDay = selectedDate.AddDays(1).AddSeconds(-1);

                // Query các giao dịch xuất kho trong ngày
                string query = $@"
    SELECT 
        t.transaction_code,
        t.transaction_date,
        t.items
    FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
    WHERE t.type = 'inventory_transaction' 
    AND t.transaction_type = 'ISSUE'
    AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startOfDay).ToUnixTimeMilliseconds()}
    AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endOfDay).ToUnixTimeMilliseconds()}
";


                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                // Tổng hợp doanh thu theo sản phẩm
                var revenueByProduct = new Dictionary<string, RevenueItem>();
                decimal totalRevenue = 0;

                foreach (var trans in transactions)
                {
                    var items = trans.items as IEnumerable<dynamic>;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            string productCode = item.product_code?.ToString() ?? "";
                            string productName = item.product_name?.ToString() ?? "";
                            int quantity = Convert.ToInt32(item.quantity ?? 0);
                            decimal unitPrice = Convert.ToDecimal(item.unit_price ?? 0);
                            decimal totalPrice = Convert.ToDecimal(item.total_price ?? 0);

                            if (!revenueByProduct.ContainsKey(productCode))
                            {
                                revenueByProduct[productCode] = new RevenueItem
                                {
                                    ProductCode = productCode,
                                    ProductName = productName,
                                    QuantitySold = 0,
                                    Revenue = 0
                                };
                            }

                            revenueByProduct[productCode].QuantitySold += quantity;
                            revenueByProduct[productCode].Revenue += totalPrice;
                            totalRevenue += totalPrice;
                        }
                    }
                }

                // Tạo DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductCode", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("QuantitySold", typeof(int));
                dt.Columns.Add("Revenue", typeof(decimal));

                // Thêm dòng tổng ở đầu
                var totalRow = dt.NewRow();
                totalRow["ProductCode"] = "";
                totalRow["ProductName"] = "TỔNG DOANH THU";
                totalRow["QuantitySold"] = revenueByProduct.Values.Sum(r => r.QuantitySold);
                totalRow["Revenue"] = totalRevenue;
                dt.Rows.Add(totalRow);

                // Thêm các sản phẩm
                foreach (var item in revenueByProduct.Values.OrderByDescending(r => r.Revenue))
                {
                    var row = dt.NewRow();
                    row["ProductCode"] = item.ProductCode;
                    row["ProductName"] = item.ProductName;
                    row["QuantitySold"] = item.QuantitySold;
                    row["Revenue"] = item.Revenue;
                    dt.Rows.Add(row);
                }

                dtgvDT.DataSource = dt;

                // Tô màu dòng tổng
                if (dtgvDT.Rows.Count > 0)
                {
                    dtgvDT.Rows[0].DefaultCellStyle.BackColor = Color.LightBlue;
                    dtgvDT.Rows[0].DefaultCellStyle.Font = new Font(dtgvDT.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ==================== PHẦN 2: THỐNG KÊ THEO KHOẢNG THỜI GIAN - ĐÃ SỬA ====================

        private async Task LoadCategoriesForRevenue()
        {
            // Categories đã được load từ LoadCategories() rồi
            cboCategory_DT.Items.Clear();
            foreach (var catName in _categoryCodeToName.Values.Distinct())
            {
                cboCategory_DT.Items.Add(catName);
            }

            await Task.CompletedTask;
        }

        private async void TxtDT_SP_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingRevenueFilter) return;

            _isUpdatingRevenueFilter = true;
            cboCategory_DT.SelectedIndex = -1;
            _isUpdatingRevenueFilter = false;

            if (!string.IsNullOrWhiteSpace(txtDT_SP.Text))
            {
                await LoadRevenueByProduct(txtDT_SP.Text.Trim());
            }
            else
            {
                // Clear grid khi xóa hết text
                dtgvDT_CT.DataSource = null;
            }
        }

        private async void CboCategory_DT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingRevenueFilter) return;

            _isUpdatingRevenueFilter = true;
            txtDT_SP.Clear();
            _isUpdatingRevenueFilter = false;

            if (cboCategory_DT.SelectedIndex >= 0)
            {
                await LoadRevenueByCategory(cboCategory_DT.SelectedItem.ToString());
            }
        }

        private async Task LoadRevenueByProduct(string productName)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime startDate = dtpDT_Start.Value.Date;
                DateTime endDate = dtpDT_End.Value.Date.AddDays(1).AddSeconds(-1);

                // ✅ THÊM status = 'COMPLETED'
                string query = $@"
            SELECT t.items
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            WHERE t.type = 'inventory_transaction' 
            AND t.transaction_type = 'ISSUE'
            AND t.status = 'COMPLETED'
            AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startDate).ToUnixTimeMilliseconds()}
            AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endDate).ToUnixTimeMilliseconds()}
        ";

                System.Diagnostics.Debug.WriteLine($"Query Product: {productName}");
                System.Diagnostics.Debug.WriteLine($"Từ {startDate:yyyy-MM-dd} đến {endDate:yyyy-MM-dd}");

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                System.Diagnostics.Debug.WriteLine($"Số transactions tìm thấy: {transactions.Count}");

                int totalQuantity = 0;
                decimal totalRevenue = 0;

                foreach (var trans in transactions)
                {
                    // ✅ SỬA CÁCH PARSE ITEMS
                    if (trans.items == null) continue;

                    try
                    {
                        var itemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(
                            trans.items.ToString()
                        );

                        foreach (var item in itemsList)
                        {
                            string itemName = item.product_name?.ToString() ?? "";

                            if (itemName.IndexOf(productName, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                int qty = Convert.ToInt32(item.quantity ?? 0);
                                decimal price = Convert.ToDecimal(item.total_price ?? 0);

                                totalQuantity += qty;
                                totalRevenue += price;

                                System.Diagnostics.Debug.WriteLine($"  - {itemName}: {qty} x {price:N0}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi parse items: {ex.Message}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Tổng: {totalQuantity} | {totalRevenue:N0}");

                // Tạo DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Revenue", typeof(decimal));

                if (totalQuantity > 0 || totalRevenue > 0)
                {
                    var row = dt.NewRow();
                    row["ItemName"] = productName;
                    row["Quantity"] = totalQuantity;
                    row["Revenue"] = totalRevenue;
                    dt.Rows.Add(row);
                }
                else
                {
                    // ✅ THÊM DÒNG "KHÔNG CÓ DỮ LIỆU"
                    var row = dt.NewRow();
                    row["ItemName"] = $"Không tìm thấy '{productName}'";
                    row["Quantity"] = 0;
                    row["Revenue"] = 0;
                    dt.Rows.Add(row);
                }

                dtgvDT_CT.DataSource = dt;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LỖI LoadRevenueByProduct: {ex.Message}");
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadRevenueByCategory(string categoryName)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // ✅ SỬA CÁCH LẤY CATEGORY CODES
                var categoryCodes = _categoryCodeToName
                    .Where(x => x.Value == categoryName)
                    .Select(x => x.Key)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"Category: {categoryName}");
                System.Diagnostics.Debug.WriteLine($"Category codes: {string.Join(", ", categoryCodes)}");

                // Lấy danh sách sản phẩm thuộc category
                string productQuery = @"
            SELECT p.code, p.name, p.category_id
            FROM `mini_mart`.`masterdata`.`products` AS p
            WHERE p.type = 'product'
        ";

                var productResult = await _cluster.QueryAsync<dynamic>(productQuery);
                var products = await productResult.Rows.ToListAsync();

                var categoryProducts = products
                    .Where(p =>
                    {
                        string catId = p.category_id?.ToString() ?? "";
                        return categoryCodes.Contains(catId);
                    })
                    .Select(p => p.name?.ToString() ?? "")
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"Số sản phẩm trong category: {categoryProducts.Count}");
                System.Diagnostics.Debug.WriteLine($"Danh sách: {string.Join(", ", categoryProducts)}");

                DateTime startDate = dtpDT_Start.Value.Date;
                DateTime endDate = dtpDT_End.Value.Date.AddDays(1).AddSeconds(-1);

                // ✅ THÊM status = 'COMPLETED'
                string transQuery = $@"
            SELECT t.items
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            WHERE t.type = 'inventory_transaction' 
            AND t.transaction_type = 'ISSUE'
            AND t.status = 'COMPLETED'
            AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startDate).ToUnixTimeMilliseconds()}
            AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endDate).ToUnixTimeMilliseconds()}
        ";

                var result = await _cluster.QueryAsync<dynamic>(transQuery);
                var transactions = await result.Rows.ToListAsync();

                System.Diagnostics.Debug.WriteLine($"Số transactions: {transactions.Count}");

                int totalQuantity = 0;
                decimal totalRevenue = 0;

                foreach (var trans in transactions)
                {
                    if (trans.items == null) continue;

                    try
                    {
                        var itemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(
                            trans.items.ToString()
                        );

                        foreach (var item in itemsList)
                        {
                            string itemName = item.product_name?.ToString() ?? "";

                            if (categoryProducts.Contains(itemName))
                            {
                                int qty = Convert.ToInt32(item.quantity ?? 0);
                                decimal price = Convert.ToDecimal(item.total_price ?? 0);

                                totalQuantity += qty;
                                totalRevenue += price;

                                System.Diagnostics.Debug.WriteLine($"  - {itemName}: {qty} x {price:N0}");
                            }
                        }
                    }
                    catch { }
                }

                System.Diagnostics.Debug.WriteLine($"Tổng: {totalQuantity} | {totalRevenue:N0}");

                // Tạo DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Revenue", typeof(decimal));

                if (totalQuantity > 0 || totalRevenue > 0)
                {
                    var row = dt.NewRow();
                    row["ItemName"] = categoryName;
                    row["Quantity"] = totalQuantity;
                    row["Revenue"] = totalRevenue;
                    dt.Rows.Add(row);
                }
                else
                {
                    var row = dt.NewRow();
                    row["ItemName"] = $"Không có dữ liệu cho '{categoryName}'";
                    row["Quantity"] = 0;
                    row["Revenue"] = 0;
                    dt.Rows.Add(row);
                }

                dtgvDT_CT.DataSource = dt;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LỖI LoadRevenueByCategory: {ex.Message}");
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void BtnAll_Loai_Click(object sender, EventArgs e)
        {
            await LoadAllCategoriesRevenue();
        }

        private async Task LoadAllCategoriesRevenue()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime startDate = dtpDT_Start.Value.Date;
                DateTime endDate = dtpDT_End.Value.Date.AddDays(1).AddSeconds(-1);

                // ✅ THÊM status = 'COMPLETED'
                string query = $@"
            SELECT t.items
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            WHERE t.type = 'inventory_transaction' 
            AND t.transaction_type = 'ISSUE'
            AND t.status = 'COMPLETED'
            AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startDate).ToUnixTimeMilliseconds()}
            AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endDate).ToUnixTimeMilliseconds()}
        ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                // Lấy danh sách products với category
                string productQuery = @"
            SELECT p.code, p.name, p.category_id
            FROM `mini_mart`.`masterdata`.`products` AS p
            WHERE p.type = 'product'
        ";

                var productResult = await _cluster.QueryAsync<dynamic>(productQuery);
                var products = await productResult.Rows.ToListAsync();

                var productToCategory = new Dictionary<string, string>();
                foreach (var p in products)
                {
                    string productName = p.name?.ToString() ?? "";
                    string categoryId = p.category_id?.ToString() ?? "";
                    string categoryName = _categoryCodeToName.ContainsKey(categoryId)
                        ? _categoryCodeToName[categoryId]
                        : "Khác";
                    productToCategory[productName] = categoryName;
                }

                var revenueByCategory = new Dictionary<string, RevenueSummary>();
                decimal grandTotal = 0;

                foreach (var trans in transactions)
                {
                    if (trans.items == null) continue;

                    try
                    {
                        var itemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(
                            trans.items.ToString()
                        );

                        foreach (var item in itemsList)
                        {
                            string productName = item.product_name?.ToString() ?? "";
                            int quantity = Convert.ToInt32(item.quantity ?? 0);
                            decimal totalPrice = Convert.ToDecimal(item.total_price ?? 0);

                            string categoryName = productToCategory.ContainsKey(productName)
                                ? productToCategory[productName]
                                : "Khác";

                            if (!revenueByCategory.ContainsKey(categoryName))
                            {
                                revenueByCategory[categoryName] = new RevenueSummary { ItemName = categoryName };
                            }

                            revenueByCategory[categoryName].Quantity += quantity;
                            revenueByCategory[categoryName].Revenue += totalPrice;
                            grandTotal += totalPrice;
                        }
                    }
                    catch { }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Revenue", typeof(decimal));

                if (grandTotal > 0)
                {
                    // Dòng tổng ở đầu
                    var totalRow = dt.NewRow();
                    totalRow["ItemName"] = "TỔNG DOANH THU";
                    totalRow["Quantity"] = revenueByCategory.Values.Sum(r => r.Quantity);
                    totalRow["Revenue"] = grandTotal;
                    dt.Rows.Add(totalRow);

                    foreach (var item in revenueByCategory.Values.OrderByDescending(r => r.Revenue))
                    {
                        var row = dt.NewRow();
                        row["ItemName"] = item.ItemName;
                        row["Quantity"] = item.Quantity;
                        row["Revenue"] = item.Revenue;
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    var row = dt.NewRow();
                    row["ItemName"] = "Không có dữ liệu trong khoảng thời gian này";
                    row["Quantity"] = 0;
                    row["Revenue"] = 0;
                    dt.Rows.Add(row);
                }

                dtgvDT_CT.DataSource = dt;

                if (dtgvDT_CT.Rows.Count > 0 && grandTotal > 0)
                {
                    dtgvDT_CT.Rows[0].DefaultCellStyle.BackColor = Color.LightGreen;
                    dtgvDT_CT.Rows[0].DefaultCellStyle.Font = new Font(dtgvDT_CT.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LỖI LoadAllCategoriesRevenue: {ex.Message}");
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void BtnAll_SP_Click(object sender, EventArgs e)
        {
            await LoadAllProductsRevenue();
        }

        private async Task LoadAllProductsRevenue()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime startDate = dtpDT_Start.Value.Date;
                DateTime endDate = dtpDT_End.Value.Date.AddDays(1).AddSeconds(-1);

                // ✅ THÊM status = 'COMPLETED'
                string query = $@"
            SELECT t.items
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            WHERE t.type = 'inventory_transaction' 
            AND t.transaction_type = 'ISSUE'
            AND t.status = 'COMPLETED'
            AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startDate).ToUnixTimeMilliseconds()}
            AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endDate).ToUnixTimeMilliseconds()}
        ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                var revenueByProduct = new Dictionary<string, RevenueSummary>();
                decimal grandTotal = 0;

                foreach (var trans in transactions)
                {
                    if (trans.items == null) continue;

                    try
                    {
                        var itemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(
                            trans.items.ToString()
                        );

                        foreach (var item in itemsList)
                        {
                            string productName = item.product_name?.ToString() ?? "";
                            int quantity = Convert.ToInt32(item.quantity ?? 0);
                            decimal totalPrice = Convert.ToDecimal(item.total_price ?? 0);

                            if (!revenueByProduct.ContainsKey(productName))
                            {
                                revenueByProduct[productName] = new RevenueSummary { ItemName = productName };
                            }

                            revenueByProduct[productName].Quantity += quantity;
                            revenueByProduct[productName].Revenue += totalPrice;
                            grandTotal += totalPrice;
                        }
                    }
                    catch { }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Revenue", typeof(decimal));

                if (grandTotal > 0)
                {
                    // Dòng tổng ở đầu
                    var totalRow = dt.NewRow();
                    totalRow["ItemName"] = "TỔNG DOANH THU";
                    totalRow["Quantity"] = revenueByProduct.Values.Sum(r => r.Quantity);
                    totalRow["Revenue"] = grandTotal;
                    dt.Rows.Add(totalRow);

                    foreach (var item in revenueByProduct.Values.OrderByDescending(r => r.Revenue))
                    {
                        var row = dt.NewRow();
                        row["ItemName"] = item.ItemName;
                        row["Quantity"] = item.Quantity;
                        row["Revenue"] = item.Revenue;
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    var row = dt.NewRow();
                    row["ItemName"] = "Không có dữ liệu trong khoảng thời gian này";
                    row["Quantity"] = 0;
                    row["Revenue"] = 0;
                    dt.Rows.Add(row);
                }

                dtgvDT_CT.DataSource = dt;

                if (dtgvDT_CT.Rows.Count > 0 && grandTotal > 0)
                {
                    dtgvDT_CT.Rows[0].DefaultCellStyle.BackColor = Color.LightGreen;
                    dtgvDT_CT.Rows[0].DefaultCellStyle.Font = new Font(dtgvDT_CT.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LỖI LoadAllProductsRevenue: {ex.Message}");
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ==================== PHẦN 3: BIỂU ĐỒ ====================

        private async Task LoadRevenueCharts()
        {
            await LoadTopRevenueChart();
            await LoadTodayRevenueChart();
        }

        private async Task LoadTopRevenueChart()
        {
            try
            {
                // Top 10 sản phẩm doanh thu cao nhất mọi thời đại
                string query = @"
    SELECT t.items
    FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
    WHERE t.type = 'inventory_transaction' 
    AND t.transaction_type = 'ISSUE'
";


                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                var revenueByProduct = new Dictionary<string, decimal>();

                foreach (var trans in transactions)
                {
                    var items = trans.items as IEnumerable<dynamic>;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            string productName = item.product_name?.ToString() ?? "";
                            decimal totalPrice = Convert.ToDecimal(item.total_price ?? 0);

                            if (!revenueByProduct.ContainsKey(productName))
                            {
                                revenueByProduct[productName] = 0;
                            }
                            revenueByProduct[productName] += totalPrice;
                        }
                    }
                }

                var top10 = revenueByProduct
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .Select(x => new Tuple<string, int>(
                        x.Key.Length > 12 ? x.Key.Substring(0, 10) + ".." : x.Key,
                        (int)x.Value))
                    .ToList();

                DrawRevenueChart(panelDT1, top10, "Top 10 sản phẩm doanh thu cao nhất", Color.Green);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTodayRevenueChart()
        {
            try
            {
                DateTime today = DateTime.Now.Date;
                DateTime endOfDay = today.AddDays(1).AddSeconds(-1);

                string query = $@"
    SELECT t.items
    FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
    WHERE t.type = 'inventory_transaction' 
    AND t.transaction_type = 'ISSUE'
    AND STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(today).ToUnixTimeMilliseconds()}
    AND STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endOfDay).ToUnixTimeMilliseconds()}
";


                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                // Lấy products với category
                string productQuery = @"
            SELECT p.name, p.category_id
            FROM `mini_mart`.`masterdata`.`products` AS p
            WHERE p.type = 'product'
        ";

                var productResult = await _cluster.QueryAsync<dynamic>(productQuery);
                var products = await productResult.Rows.ToListAsync();

                var productToCategory = new Dictionary<string, string>();
                foreach (var p in products)
                {
                    string productName = p.name?.ToString() ?? "";
                    string categoryId = p.category_id?.ToString() ?? "";
                    string categoryName = _categoryCodeToName.ContainsKey(categoryId) ? _categoryCodeToName[categoryId] : "Khác";
                    productToCategory[productName] = categoryName;
                }

                var revenueByCategory = new Dictionary<string, decimal>();

                foreach (var trans in transactions)
                {
                    var items = trans.items as IEnumerable<dynamic>;
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            string productName = item.product_name?.ToString() ?? "";
                            decimal totalPrice = Convert.ToDecimal(item.total_price ?? 0);

                            string categoryName = productToCategory.ContainsKey(productName) ? productToCategory[productName] : "Khác";

                            if (!revenueByCategory.ContainsKey(categoryName))
                            {
                                revenueByCategory[categoryName] = 0;
                            }
                            revenueByCategory[categoryName] += totalPrice;
                        }
                    }
                }

                var chartData = revenueByCategory
                    .OrderByDescending(x => x.Value)
                    .Select(x => new Tuple<string, int>(
                        x.Key.Length > 12 ? x.Key.Substring(0, 10) + ".." : x.Key,
                        (int)x.Value))
                    .ToList();

                DrawRevenueChart(panelDT2, chartData, $"Doanh thu theo loại hôm nay ({DateTime.Now:dd/MM/yyyy})", Color.DodgerBlue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawRevenueChart(Panel panel, List<Tuple<string, int>> data, string title, Color barColor)
        {
            panel.Controls.Clear();

            if (data.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = "Không có dữ liệu",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 11)
                };
                panel.Controls.Add(lblNoData);
                return;
            }

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            int chartWidth = Math.Max(panel.Width, 800);
            int chartHeight = Math.Max(panel.Height, 550); // TĂNG CHIỀU CAO

            Bitmap bitmap = new Bitmap(chartWidth, chartHeight);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Tiêu đề
                Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.Black,
                    (bitmap.Width - titleSize.Width) / 2, 15);

                int marginLeft = 70;
                int marginRight = 30;
                int marginTop = 55;
                int marginBottom = 300; // TĂNG LÊN 150

                int chartTop = marginTop;
                int chartBottom = bitmap.Height - marginBottom;
                int plotHeight = chartBottom - chartTop;
                int plotWidth = bitmap.Width - marginLeft - marginRight;

                if (plotHeight <= 0 || plotWidth <= 0) return;

                int maxValue = data.Max(d => d.Item2);
                if (maxValue == 0) maxValue = 1;

                int roundedMax = (int)Math.Ceiling(maxValue / 100000.0) * 100000;
                if (roundedMax < maxValue) roundedMax = maxValue;

                // Vẽ trục
                Pen gridPen = new Pen(Color.FromArgb(230, 230, 230), 1);
                Pen axisPen = new Pen(Color.FromArgb(80, 80, 80), 2);

                g.DrawLine(axisPen, marginLeft, chartTop, marginLeft, chartBottom);
                g.DrawLine(axisPen, marginLeft, chartBottom, bitmap.Width - marginRight, chartBottom);

                // Mốc Y
                Font labelFont = new Font("Arial", 9);
                for (int i = 0; i <= 5; i++)
                {
                    int yValue = roundedMax * i / 5;
                    int yPos = chartBottom - (int)((double)yValue / roundedMax * plotHeight);

                    if (i > 0)
                    {
                        g.DrawLine(gridPen, marginLeft, yPos, bitmap.Width - marginRight, yPos);
                    }

                    g.DrawLine(axisPen, marginLeft - 5, yPos, marginLeft, yPos);

                    string label = (yValue / 1000).ToString() + "K";
                    SizeF labelSize = g.MeasureString(label, labelFont);
                    g.DrawString(label, labelFont, Brushes.Black,
                        marginLeft - labelSize.Width - 10, yPos - labelSize.Height / 2);
                }

                // Vẽ cột
                int barWidth = Math.Min(60, (plotWidth - 40) / data.Count - 15);
                int spacing = Math.Max(10, (plotWidth - (barWidth * data.Count)) / (data.Count + 1));
                int x = marginLeft + spacing;

                Font valueFont = new Font("Arial", 9, FontStyle.Bold);
                Font nameFont = new Font("Arial", 8);

                for (int i = 0; i < data.Count; i++)
                {
                    string name = data[i].Item1;
                    int value = data[i].Item2;

                    int barHeight = (int)((double)value / roundedMax * plotHeight);
                    if (barHeight < 8 && value > 0) barHeight = 8;

                    Rectangle bar = new Rectangle(x, chartBottom - barHeight, barWidth, barHeight);

                    Color darkColor = Color.FromArgb(
                        Math.Max(0, barColor.R - 30),
                        Math.Max(0, barColor.G - 30),
                        Math.Max(0, barColor.B - 30)
                    );

                    using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                        new System.Drawing.Drawing2D.LinearGradientBrush(bar, barColor, darkColor, 90f))
                    {
                        g.FillRectangle(brush, bar);
                    }

                    g.DrawRectangle(new Pen(Color.FromArgb(120, Color.Black), 1), bar);

                    // Giá trị TRÊN ĐẦU CỘT
                    string valueText = (value / 1000).ToString() + "K";
                    SizeF valueSize = g.MeasureString(valueText, valueFont);

                    float textX = x + (barWidth - valueSize.Width) / 2;
                    float textY = chartBottom - barHeight - valueSize.Height - 8;

                    if (textY < chartTop)
                    {
                        textY = chartBottom - barHeight + 8;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)),
                            textX - 3, textY - 2, valueSize.Width + 6, valueSize.Height + 4);
                    }

                    g.DrawString(valueText, valueFont, Brushes.Black, textX, textY);

                    // Tên sản phẩm - XOAY 45°
                    GraphicsState state = g.Save();

                    float labelX = x + (barWidth / 2);
                    float labelY = chartBottom + 10;

                    g.TranslateTransform(labelX, labelY);
                    g.RotateTransform(45);

                    g.DrawString(name, nameFont, Brushes.Black, 0, 0);

                    g.Restore(state);

                    x += barWidth + spacing;
                }
            }

            pictureBox.Image = bitmap;
            panel.Controls.Add(pictureBox);
        }


        // ==================== CÁC SỰ KIỆN ====================

        private async void DtpDT_ValueChanged(object sender, EventArgs e)
        {
            await LoadRevenueByDate();
        }

        private async void DtpDT_Range_ValueChanged(object sender, EventArgs e)
        {
            // Reload data khi thay đổi khoảng thời gian
        }

        private void BtnExport_DT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvDT.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = $"DoanhThu_{dtpDT.Value:yyyyMMdd}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportRevenueToExcel(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportRevenueToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Doanh thu");

                worksheet.Cell(1, 1).Value = "Mã SP";
                worksheet.Cell(1, 2).Value = "Tên sản phẩm";
                worksheet.Cell(1, 3).Value = "Số lượng bán";
                worksheet.Cell(1, 4).Value = "Doanh thu";

                var headerRange = worksheet.Range(1, 1, 1, 4);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var dt = (DataTable)dtgvDT.DataSource;
                int row = 2;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dr["ProductCode"].ToString();
                    worksheet.Cell(row, 2).Value = dr["ProductName"].ToString();
                    worksheet.Cell(row, 3).Value = Convert.ToInt32(dr["QuantitySold"]);
                    worksheet.Cell(row, 4).Value = Convert.ToDecimal(dr["Revenue"]);

                    if (dr["ProductName"].ToString() == "TỔNG DOANH THU")
                    {
                        worksheet.Range(row, 1, row, 4).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        worksheet.Range(row, 1, row, 4).Style.Font.Bold = true;
                    }

                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        // ==================== CLASS HỖ TRỢ ====================

        private class RevenueItem
        {
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
            public decimal Revenue { get; set; }
        }

        private class RevenueSummary
        {
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public decimal Revenue { get; set; }
        }

        // ==================== TAB NHẬP/XUẤT ====================

        private async Task InitializeImportExportTab()
        {
            // Thiết lập ComboBox loại chứng từ
            cboLCT.Items.Clear();
            cboLCT.Items.Add("Tất cả");
            cboLCT.Items.Add("Phiếu nhập");
            cboLCT.Items.Add("Phiếu xuất");
            cboLCT.SelectedIndex = 0;

            // Thiết lập DateTimePicker
            dtpNX_Start.Format = DateTimePickerFormat.Short;
            dtpNX_End.Format = DateTimePickerFormat.Short;
            dtpNX_End.MaxDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            dtpNX_End.Value = DateTime.Now.Date;

            // Không thiết lập giá trị mặc định cho Start - để trống = toàn thời gian
            dtpNX_Start.CustomFormat = " "; // Hiển thị trống
            dtpNX_Start.Format = DateTimePickerFormat.Custom;


            // Cấu hình DataGridView
            ConfigureImportExportDataGridView();

            // Load dữ liệu ban đầu
            await LoadImportExportData();
            await LoadImportExportChart();
        }

        private void ConfigureImportExportDataGridView()
        {
            dtgvNX.AutoGenerateColumns = false;
            dtgvNX.AllowUserToAddRows = false;
            dtgvNX.RowTemplate.Height = 35;
            dtgvNX.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvNX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvNX.RowHeadersVisible = false;

            dtgvNX.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvNX.ColumnHeadersDefaultCellStyle.Font = new Font(dtgvNX.Font, FontStyle.Bold);

            dtgvNX.Columns.Clear();

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTransCode",
                HeaderText = "Mã chứng từ",
                DataPropertyName = "TransactionCode",
                FillWeight = 15,
                ReadOnly = true
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTransType",
                HeaderText = "Loại",
                DataPropertyName = "TransactionType",
                FillWeight = 10,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font(dtgvNX.Font, FontStyle.Bold)
                }
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTransDate",
                HeaderText = "Ngày giao dịch",
                DataPropertyName = "TransactionDate",
                FillWeight = 15,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStaff",
                HeaderText = "Nhân viên",
                DataPropertyName = "StaffName",
                FillWeight = 15,
                ReadOnly = true
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalAmount",
                HeaderText = "Tổng tiền",
                DataPropertyName = "TotalAmount",
                FillWeight = 15,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font(dtgvNX.Font, FontStyle.Bold)
                }
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                FillWeight = 12,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvNX.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNote",
                HeaderText = "Ghi chú",
                DataPropertyName = "Note",
                FillWeight = 18,
                ReadOnly = true
            });
        }

        private async Task LoadImportExportData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<string> whereClauses = new List<string> { "t.type = 'inventory_transaction'" };

                // Lọc theo loại chứng từ
                string selectedType = cboLCT.SelectedItem?.ToString() ?? "Tất cả";
                if (selectedType == "Phiếu nhập")
                {
                    whereClauses.Add("t.transaction_type = 'RECEIPT'");
                }
                else if (selectedType == "Phiếu xuất")
                {
                    whereClauses.Add("t.transaction_type = 'ISSUE'");
                }

                // Lọc theo thời gian (nếu đã chọn)
                if (dtpNX_Start.Format != DateTimePickerFormat.Custom)
                {
                    DateTime startDate = dtpNX_Start.Value.Date;
                    DateTime endDate = dtpNX_End.Value.Date.AddDays(1).AddSeconds(-1);

                    whereClauses.Add($"STR_TO_MILLIS(t.transaction_date) >= {new DateTimeOffset(startDate).ToUnixTimeMilliseconds()}");
                    whereClauses.Add($"STR_TO_MILLIS(t.transaction_date) <= {new DateTimeOffset(endDate).ToUnixTimeMilliseconds()}");
                }

                string whereClause = "WHERE " + string.Join(" AND ", whereClauses);

                string query = $@"
            SELECT 
                t.transaction_code,
                t.transaction_type,
                t.transaction_date,
                t.staff_id,
                t.total_amount,
                t.status,
                t.note
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            {whereClause}
            ORDER BY STR_TO_MILLIS(t.transaction_date) DESC
        ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                // ✅ SỬA: Lấy thông tin nhân viên theo username
                string staffQuery = @"
            SELECT META(u).id as _id, u.username, u.full_name
            FROM `mini_mart`.`security`.`users` AS u
            WHERE u.type = 'user'
        ";
                var staffResult = await _cluster.QueryAsync<dynamic>(staffQuery);
                var staffList = await staffResult.Rows.ToListAsync();

                var staffDict = new Dictionary<string, string>();
                foreach (var staff in staffList)
                {
                    string staffId = staff._id?.ToString() ?? "";
                    string fullName = staff.full_name?.ToString() ?? "";
                    staffDict[staffId] = fullName;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("TransactionCode", typeof(string));
                dt.Columns.Add("TransactionType", typeof(string));
                dt.Columns.Add("TransactionDate", typeof(DateTime));
                dt.Columns.Add("StaffName", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Note", typeof(string));

                foreach (var trans in transactions)
                {
                    var row = dt.NewRow();
                    row["TransactionCode"] = trans.transaction_code?.ToString() ?? "";

                    string transType = trans.transaction_type?.ToString() ?? "";
                    row["TransactionType"] = transType == "RECEIPT" ? "Nhập" : "Xuất";

                    string dateStr = trans.transaction_date?.ToString() ?? "";
                    if (DateTime.TryParse(dateStr, out DateTime transDate))
                    {
                        row["TransactionDate"] = transDate;
                    }
                    else
                    {
                        row["TransactionDate"] = DateTime.MinValue;
                    }

                    string staffId = trans.staff_id?.ToString() ?? "";
                    row["StaffName"] = staffDict.ContainsKey(staffId) ? staffDict[staffId] : staffId;

                    row["TotalAmount"] = Convert.ToDecimal(trans.total_amount ?? 0);

                    string status = trans.status?.ToString() ?? "";
                    row["Status"] = status == "COMPLETED" ? "Hoàn thành" :
                                   status == "PENDING" ? "Chờ xử lý" : status;

                    row["Note"] = trans.note?.ToString() ?? "";

                    dt.Rows.Add(row);
                }

                dtgvNX.DataSource = dt;

                // Tô màu các dòng
                foreach (DataGridViewRow dgvRow in dtgvNX.Rows)
                {
                    string transType = dgvRow.Cells["colTransType"].Value?.ToString() ?? "";
                    if (transType == "Nhập")
                    {
                        dgvRow.Cells["colTransType"].Style.BackColor = Color.LightGreen;
                    }
                    else if (transType == "Xuất")
                    {
                        dgvRow.Cells["colTransType"].Style.BackColor = Color.LightCoral;
                    }

                    string status = dgvRow.Cells["colStatus"].Value?.ToString() ?? "";
                    if (status == "Hoàn thành")
                    {
                        dgvRow.Cells["colStatus"].Style.BackColor = Color.LightBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadImportExportChart()
        {
            try
            {
                // Query tổng giá trị nhập/xuất toàn thời gian
                string query = @"
            SELECT 
                t.transaction_type,
                t.total_amount
            FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
            WHERE t.type = 'inventory_transaction'
            AND t.status = 'COMPLETED'
        ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                decimal totalReceipt = 0;
                decimal totalIssue = 0;

                foreach (var trans in transactions)
                {
                    string transType = trans.transaction_type?.ToString() ?? "";
                    decimal amount = Convert.ToDecimal(trans.total_amount ?? 0);

                    if (transType == "RECEIPT")
                    {
                        totalReceipt += amount;
                    }
                    else if (transType == "ISSUE")
                    {
                        totalIssue += amount;
                    }
                }

                DrawPieChart(totalReceipt, totalIssue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawPieChart(decimal receiptAmount, decimal issueAmount)
        {
            panelNX.Controls.Clear();

            if (receiptAmount == 0 && issueAmount == 0)
            {
                Label lblNoData = new Label
                {
                    Text = "Không có dữ liệu",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 12)
                };
                panelNX.Controls.Add(lblNoData);
                return;
            }

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // ✅ SỬA: Tăng kích thước để không bị khuất
            int width = Math.Max(panelNX.Width, 500);
            int height = Math.Max(panelNX.Height, 500);

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Tiêu đề
                Font titleFont = new Font("Arial", 14, FontStyle.Bold);
                string title = "Tỉ lệ Nhập/Xuất (Toàn thời gian)";
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.Black,
                    (bitmap.Width - titleSize.Width) / 2, 20);

                // Tính toán
                decimal total = receiptAmount + issueAmount;
                float receiptPercent = (float)(receiptAmount / total * 100);
                float issuePercent = (float)(issueAmount / total * 100);

                // ✅ SỬA: Điều chỉnh vị trí biểu đồ để không bị khuất chú thích
                int diameter = Math.Min(bitmap.Width, bitmap.Height) - 200; // Tăng margin
                int radius = diameter / 2;
                int centerX = bitmap.Width / 2;
                int centerY = 60 + radius + 20; // Đẩy xuống dưới tiêu đề

                Rectangle pieRect = new Rectangle(
                    centerX - radius,
                    centerY - radius,
                    diameter,
                    diameter
                );

                // Góc bắt đầu và góc quét
                float startAngle = -90; // Bắt đầu từ 12 giờ
                float receiptAngle = 360 * receiptPercent / 100;
                float issueAngle = 360 * issuePercent / 100;

                // Vẽ phần Nhập (xanh lá)
                using (SolidBrush brushReceipt = new SolidBrush(Color.FromArgb(144, 238, 144)))
                {
                    g.FillPie(brushReceipt, pieRect, startAngle, receiptAngle);
                }
                g.DrawPie(new Pen(Color.White, 3), pieRect, startAngle, receiptAngle);

                // Vẽ phần Xuất (đỏ nhạt)
                using (SolidBrush brushIssue = new SolidBrush(Color.FromArgb(255, 160, 160)))
                {
                    g.FillPie(brushIssue, pieRect, startAngle + receiptAngle, issueAngle);
                }
                g.DrawPie(new Pen(Color.White, 3), pieRect, startAngle + receiptAngle, issueAngle);

                // ✅ SỬA: Chú thích ở dưới biểu đồ
                Font legendFont = new Font("Arial", 11);
                int legendY = centerY + radius + 40;
                int legendX = 30;

                // Nhập
                g.FillRectangle(Brushes.LightGreen, legendX, legendY, 25, 25);
                g.DrawRectangle(Pens.Black, legendX, legendY, 25, 25);
                string receiptText = $"Nhập: {receiptPercent:F1}% ({receiptAmount:N0} VNĐ)";
                g.DrawString(receiptText, legendFont, Brushes.Black, legendX + 35, legendY + 2);

                // Xuất
                legendY += 40;
                g.FillRectangle(Brushes.LightCoral, legendX, legendY, 25, 25);
                g.DrawRectangle(Pens.Black, legendX, legendY, 25, 25);
                string issueText = $"Xuất: {issuePercent:F1}% ({issueAmount:N0} VNĐ)";
                g.DrawString(issueText, legendFont, Brushes.Black, legendX + 35, legendY + 2);

                // Tổng
                legendY += 50;
                Font totalFont = new Font("Arial", 12, FontStyle.Bold);
                string totalText = $"Tổng giá trị giao dịch: {total:N0} VNĐ";
                SizeF totalSize = g.MeasureString(totalText, totalFont);
                g.DrawString(totalText, totalFont, Brushes.DarkBlue,
                    (bitmap.Width - totalSize.Width) / 2, legendY);
            }

            pictureBox.Image = bitmap;
            panelNX.Controls.Add(pictureBox);
        }

        // ==================== CÁC SỰ KIỆN ====================

        private async void CboLCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadImportExportData();
        }

        private async void DtpNX_ValueChanged(object sender, EventArgs e)
        {
            // Khi user chọn ngày, đổi format về bình thường
            if (sender == dtpNX_Start && dtpNX_Start.Format == DateTimePickerFormat.Custom)
            {
                dtpNX_Start.Format = DateTimePickerFormat.Short;
            }

            await LoadImportExportData();
        }

        private async void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset về trạng thái xem toàn thời gian
            dtpNX_Start.CustomFormat = " ";
            dtpNX_Start.Format = DateTimePickerFormat.Custom;
            dtpNX_End.Value = DateTime.Now.Date;

            await LoadImportExportData();
        }

        private void BtnExport_NX_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvNX.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = $"BaoCaoNhapXuat_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportImportExportToExcel(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportImportExportToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Báo cáo Nhập Xuất");

                // Tiêu đề các cột
                worksheet.Cell(1, 1).Value = "Mã chứng từ";
                worksheet.Cell(1, 2).Value = "Loại";
                worksheet.Cell(1, 3).Value = "Ngày giao dịch";
                worksheet.Cell(1, 4).Value = "Nhân viên";
                worksheet.Cell(1, 5).Value = "Tổng tiền";
                worksheet.Cell(1, 6).Value = "Trạng thái";
                worksheet.Cell(1, 7).Value = "Ghi chú";

                // Format header
                var headerRange = worksheet.Range(1, 1, 1, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Lấy dữ liệu từ DataGridView
                var dt = (DataTable)dtgvNX.DataSource;
                int row = 2;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dr["TransactionCode"].ToString();
                    worksheet.Cell(row, 2).Value = dr["TransactionType"].ToString();

                    // Format ngày tháng
                    DateTime transDate = Convert.ToDateTime(dr["TransactionDate"]);
                    worksheet.Cell(row, 3).Value = transDate;
                    worksheet.Cell(row, 3).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";

                    worksheet.Cell(row, 4).Value = dr["StaffName"].ToString();

                    // Format số tiền
                    worksheet.Cell(row, 5).Value = Convert.ToDecimal(dr["TotalAmount"]);
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";

                    worksheet.Cell(row, 6).Value = dr["Status"].ToString();
                    worksheet.Cell(row, 7).Value = dr["Note"].ToString();

                    // Tô màu theo loại giao dịch
                    string transType = dr["TransactionType"].ToString();
                    if (transType == "Nhập")
                    {
                        worksheet.Cell(row, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (transType == "Xuất")
                    {
                        worksheet.Cell(row, 2).Style.Fill.BackgroundColor = XLColor.LightCoral;
                    }

                    // Tô màu theo trạng thái
                    string status = dr["Status"].ToString();
                    if (status == "Hoàn thành")
                    {
                        worksheet.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.LightBlue;
                    }

                    // Border cho từng dòng
                    worksheet.Range(row, 1, row, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    row++;
                }

                // Thêm dòng tổng kết (nếu muốn)
                row++; // Dòng trống
                worksheet.Cell(row, 4).Value = "TỔNG CỘNG:";
                worksheet.Cell(row, 4).Style.Font.Bold = true;

                decimal totalAmount = dt.AsEnumerable().Sum(r => Convert.ToDecimal(r["TotalAmount"]));
                worksheet.Cell(row, 5).Value = totalAmount;
                worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(row, 5).Style.Font.Bold = true;
                worksheet.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.Yellow;

                // Tự động điều chỉnh độ rộng cột
                worksheet.Columns().AdjustToContents();

                // Đặt độ rộng tối thiểu cho một số cột
                worksheet.Column(1).Width = 15; // Mã chứng từ
                worksheet.Column(3).Width = 18; // Ngày giao dịch
                worksheet.Column(7).Width = 30; // Ghi chú

                workbook.SaveAs(filePath);
            }
        }

    }
}