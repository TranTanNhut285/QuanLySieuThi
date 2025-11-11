using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Couchbase;
using Couchbase.Query;

namespace Mini_Mart
{
    public partial class Form1 : Form
    {
        private ICluster _cluster;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            // Đăng ký sự kiện cho các nút điều hướng
            btnQLND.Click += BtnQLND_Click;
            btnQLLSP.Click += BtnQLLSP_Click;
            btnQLSP.Click += BtnQLSP_Click;
            btnQLNCC.Click += BtnQLNCC_Click;
            btnPNH.Click += BtnPNH_Click;
            btnPXH.Click += BtnPXH_Click;
            btnLSGDK.Click += BtnLSGDK_Click;
            btnTK.Click += BtnTK_Click;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                await LoadDashboardData();
                lblTenTaiKhoan.Text = UserSession.CurrentUser?.FullName ?? "Chưa đăng nhập";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== KẾT NỐI COUCHBASE ====================

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
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // ==================== LOAD DỮ LIỆU TỔNG QUAN ====================

        private async Task LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Load song song các dữ liệu
                var taskTotalProducts = LoadTotalProducts();
                var taskTotalRevenue = LoadTotalRevenue();
                var taskTotalStock = LoadTotalStock();
                var taskLowStockChart = LoadLowStockChart();

                await Task.WhenAll(taskTotalProducts, taskTotalRevenue, taskTotalStock, taskLowStockChart);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu tổng quan: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ==================== TỔNG SỐ SẢN PHẨM ====================

        private async Task LoadTotalProducts()
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) as total
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product'
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                if (rows.Count > 0)
                {
                    int total = Convert.ToInt32(rows[0].total ?? 0);
                    txtTongSP.Text = total.ToString("N0");
                }
                else
                {
                    txtTongSP.Text = "0";
                }
            }
            catch (Exception ex)
            {
                txtTongSP.Text = "Lỗi";
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadTotalProducts: {ex.Message}");
            }
        }

        // ==================== TỔNG DOANH THU ====================

        private async Task LoadTotalRevenue()
        {
            try
            {
                string query = @"
                    SELECT t.total_amount
                    FROM `mini_mart`.`inventory`.`inventory_transactions` AS t
                    WHERE t.type = 'inventory_transaction' 
                    AND t.transaction_type = 'ISSUE'
                    AND t.status = 'COMPLETED'
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var transactions = await result.Rows.ToListAsync();

                decimal totalRevenue = 0;

                foreach (var trans in transactions)
                {
                    decimal amount = Convert.ToDecimal(trans.total_amount ?? 0);
                    totalRevenue += amount;
                }

                txtTongDT.Text = totalRevenue.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                txtTongDT.Text = "Lỗi";
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadTotalRevenue: {ex.Message}");
            }
        }

        // ==================== TỔNG TỒN KHO ====================

        private async Task LoadTotalStock()
        {
            try
            {
                string query = @"
                    SELECT SUM(p.current_stock) as total_stock
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product'
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                if (rows.Count > 0)
                {
                    int totalStock = Convert.ToInt32(rows[0].total_stock ?? 0);
                    txtTongTK.Text = totalStock.ToString("N0");
                }
                else
                {
                    txtTongTK.Text = "0";
                }
            }
            catch (Exception ex)
            {
                txtTongTK.Text = "Lỗi";
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadTotalStock: {ex.Message}");
            }
        }

        // ==================== BIỂU ĐỒ TOP 10 SẢN PHẨM ÍT NHẤT ====================

        private async Task LoadLowStockChart()
        {
            try
            {
                string query = @"
                    SELECT p.name, p.current_stock
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product' AND p.current_stock >= 0
                    ORDER BY p.current_stock ASC
                    LIMIT 10
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var products = await result.Rows.ToListAsync();

                var chartData = new List<Tuple<string, int>>();

                foreach (var item in products)
                {
                    string name = item.name?.ToString() ?? "";
                    int stock = Convert.ToInt32(item.current_stock ?? 0);

                    // Rút gọn tên nếu quá dài
                    if (name.Length > 15)
                        name = name.Substring(0, 13) + "..";

                    chartData.Add(new Tuple<string, int>(name, stock));
                }

                DrawLowStockChart(chartData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadLowStockChart: {ex.Message}");
                ShowNoDataMessage(panelMain, "Lỗi tải biểu đồ");
            }
        }

        private void DrawLowStockChart(List<Tuple<string, int>> data)
        {
            panelMain.Controls.Clear();

            if (data.Count == 0)
            {
                ShowNoDataMessage(panelMain, "Không có dữ liệu");
                return;
            }

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // ✅ KÍCH THƯỚC LỚN HƠN ĐỂ TRÁNH CHỒNG CHÉO
            int chartWidth = Math.Max(1600, data.Count * 140);
            int chartHeight = 800;

            Bitmap bitmap = new Bitmap(chartWidth, chartHeight);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // ✅ MARGIN ĐỦ LỚN ĐỂ TRÁNH ĐÈ CHỮ
                int marginLeft = 160; // Tăng để biểu đồ dịch sang phải, tránh đè chữ trục Y
                int marginRight = 80;
                int marginTop = 120; // Tăng để số trên cột không đè tiêu đề
                int marginBottom = 280; // Tăng để đủ chỗ tên sản phẩm xoay và dòng cảnh báo

                // ✅ TIÊU ĐỀ Ở VỊ TRÍ AN TOÀN
                Font titleFont = new Font("Arial", 22, FontStyle.Bold);
                string title = "Top 10 sản phẩm tồn kho ít nhất";
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.DarkBlue,
                    (bitmap.Width - titleSize.Width) / 2, 30);

                int chartTop = marginTop;
                int chartBottom = bitmap.Height - marginBottom;
                int plotHeight = chartBottom - chartTop;
                int plotWidth = bitmap.Width - marginLeft - marginRight;

                if (plotHeight <= 0 || plotWidth <= 0) return;

                // Tìm max value và làm tròn
                int maxValue = data.Max(d => d.Item2);
                if (maxValue == 0) maxValue = 10;

                int roundedMax = (int)Math.Ceiling(maxValue / 10.0) * 10;
                if (roundedMax <= maxValue) roundedMax = maxValue + 10;

                // ✅ VẼ TRỤC
                Pen gridPen = new Pen(Color.FromArgb(220, 220, 220), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
                Pen axisPen = new Pen(Color.Black, 3);

                g.DrawLine(axisPen, marginLeft, chartTop, marginLeft, chartBottom);
                g.DrawLine(axisPen, marginLeft, chartBottom, bitmap.Width - marginRight, chartBottom);

                // ✅ VẼ MỐC TRỤC Y VỚI KHOẢNG CÁCH HỢP LÝ
                Font labelFont = new Font("Arial", 12, FontStyle.Regular);
                int steps = 6;

                for (int i = 0; i <= steps; i++)
                {
                    int yValue = roundedMax * i / steps;
                    int yPos = chartBottom - (int)((double)yValue / roundedMax * plotHeight);

                    // Grid ngang
                    if (i < steps)
                    {
                        g.DrawLine(gridPen, marginLeft + 1, yPos, bitmap.Width - marginRight, yPos);
                    }

                    // Tick mark
                    g.DrawLine(new Pen(Color.Black, 2), marginLeft - 12, yPos, marginLeft, yPos);

                    // Label số
                    string label = yValue.ToString();
                    SizeF labelSize = g.MeasureString(label, labelFont);
                    g.DrawString(label, labelFont, Brushes.Black,
                        marginLeft - labelSize.Width - 20, yPos - labelSize.Height / 2);
                }

                // ✅ TÍNH KÍCH THƯỚC CỘT ĐỂ CÓ ĐỦ KHOẢNG TRỐNG
                int totalWidth = plotWidth - 80;
                int barWidth = Math.Min(100, (totalWidth / data.Count) - 40);
                int spacing = Math.Max(30, (totalWidth - (barWidth * data.Count)) / (data.Count + 1));

                int x = marginLeft + spacing;

                Font valueFont = new Font("Arial", 12, FontStyle.Bold);
                Font nameFont = new Font("Arial", 10, FontStyle.Regular);

                for (int i = 0; i < data.Count; i++)
                {
                    string name = data[i].Item1;
                    int value = data[i].Item2;

                    // ✅ TÍNH CHIỀU CAO CỘT CHÍNH XÁC
                    int barHeight = (int)((double)value / roundedMax * plotHeight);
                    if (barHeight < 15 && value > 0) barHeight = 15;

                    Rectangle bar = new Rectangle(x, chartBottom - barHeight, barWidth, barHeight);

                    // Màu gradient
                    Color barColor = Color.FromArgb(220, 20, 60);
                    Color lightColor = Color.FromArgb(255, 190, 190);

                    using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                        new System.Drawing.Drawing2D.LinearGradientBrush(
                            bar, lightColor, barColor, 90f))
                    {
                        g.FillRectangle(brush, bar);
                    }

                    g.DrawRectangle(new Pen(Color.FromArgb(180, 10, 40), 2), bar);

                    // ✅ SỐ TRÊN CỘT - ĐẶT CAO HƠN VÀ CÓ BACKGROUND
                    string valueText = value.ToString();
                    SizeF valueSize = g.MeasureString(valueText, valueFont);

                    // Đặt số cao hơn đỉnh cột ít nhất 20px
                    float valueY = Math.Max(chartTop + 10, chartBottom - barHeight - valueSize.Height - 20);

                    // Background trắng để nổi bật
                    RectangleF valueBg = new RectangleF(
                        x + (barWidth - valueSize.Width) / 2 - 5,
                        valueY - 3,
                        valueSize.Width + 10,
                        valueSize.Height + 6
                    );
                    g.FillRectangle(Brushes.White, valueBg);
                    g.DrawRectangle(new Pen(Color.FromArgb(50, Color.Black), 1), valueBg.X, valueBg.Y, valueBg.Width, valueBg.Height);

                    g.DrawString(valueText, valueFont, Brushes.DarkRed,
                        x + (barWidth - valueSize.Width) / 2, valueY);

                    // ✅ TÊN SẢN PHẨM - XOAY 45 ĐỘ VỚI VỊ TRÍ AN TOÀN
                    g.TranslateTransform(x + barWidth / 2, chartBottom + 30);
                    g.RotateTransform(45);
                    g.DrawString(name, nameFont, Brushes.Black, 0, 0);
                    g.ResetTransform();

                    x += barWidth + spacing;
                }

                // ✅ LABEL TRỤC Y
                Font axisFont = new Font("Arial", 13, FontStyle.Bold);
                string yLabel = "Số lượng tồn kho";
                SizeF yLabelSize = g.MeasureString(yLabel, axisFont);

                g.TranslateTransform(30, chartTop + plotHeight / 2 + yLabelSize.Width / 2);
                g.RotateTransform(-90);
                g.DrawString(yLabel, axisFont, Brushes.DarkSlateGray, 0, 0);
                g.ResetTransform();

                // ✅ GHI CHÚ CẢNH BÁO Ở VỊ TRÍ AN TOÀN PHÍA DƯỚI
                Font noteFont = new Font("Arial", 13, FontStyle.Bold);
                string note = "⚠ Cần nhập thêm hàng cho các sản phẩm này";
                SizeF noteSize = g.MeasureString(note, noteFont);

                Rectangle noteBg = new Rectangle(
                    (bitmap.Width - (int)noteSize.Width - 40) / 2,
                    bitmap.Height - 70,
                    (int)noteSize.Width + 40,
                    (int)noteSize.Height + 20
                );
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 220)), noteBg);
                g.DrawRectangle(new Pen(Color.Red, 3), noteBg);

                g.DrawString(note, noteFont, Brushes.Red,
                    (bitmap.Width - noteSize.Width) / 2, bitmap.Height - 60);
            }

            pictureBox.Image = bitmap;
            panelMain.Controls.Add(pictureBox);
        }

        private void ShowNoDataMessage(Panel panel, string message)
        {
            panel.Controls.Clear();
            Label lblNoData = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(lblNoData);
        }

        // ==================== ĐIỀU HƯỚNG CÁC FORM ====================

        private void BtnQLND_Click(object sender, EventArgs e)
        {
            try
            {
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Quản lý người dùng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQLLSP_Click(object sender, EventArgs e)
        {
            try
            {
                frmCategory frmCategory = new frmCategory();
                frmCategory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Quản lý loại sản phẩm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQLSP_Click(object sender, EventArgs e)
        {
            try
            {
                frmProduct frmProduct = new frmProduct();
                frmProduct.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Quản lý sản phẩm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQLNCC_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplier frmSupplier = new frmSupplier();
                frmSupplier.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Quản lý nhà cung cấp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPNH_Click(object sender, EventArgs e)
        {
            try
            {
                frmReceiptTransaction frmReceipt = new frmReceiptTransaction();
                frmReceipt.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Phiếu nhập hàng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPXH_Click(object sender, EventArgs e)
        {
            try
            {
                frmIssueTransaction frmIssue = new frmIssueTransaction();
                frmIssue.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Phiếu xuất hàng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLSGDK_Click(object sender, EventArgs e)
        {
            try
            {
                frmTransactionHistory frmHistory = new frmTransactionHistory();
                frmHistory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Lịch sử giao dịch kho: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTK_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form Thống kê: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ĐÓNG FORM ====================

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cluster?.Dispose();
        }
    }
}