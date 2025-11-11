using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Couchbase;
using Couchbase.KeyValue;
using Couchbase.Query;
using ZXing;
using ZXing.Common;

namespace Mini_Mart
{
    public partial class frmProduct : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private ICouchbaseCollection _collection;
        private static readonly HttpClient httpClient = new HttpClient();

        public frmProduct()
        {
            InitializeComponent();
            this.Load += frmProduct_Load;
        }

        private async void frmProduct_Load(object sender, EventArgs e)
        {
            try
            {
                await ConnectCouchbase();
                ConfigureDataGridView();
                await LoadProducts();
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
                _collection = _bucket.Scope("masterdata").Collection("products");

                MessageBox.Show("Kết nối Couchbase thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Couchbase:\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ConfigureDataGridView()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.RowTemplate.Height = 100;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã SP",
                DataPropertyName = "Code",
                Width = 80
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "Name",
                Width = 200
            });

            var colBarcode = new DataGridViewImageColumn
            {
                Name = "colBarcode",
                HeaderText = "Mã vạch",
                DataPropertyName = "BarcodeImage",
                Width = 150,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dgvProducts.Columns.Add(colBarcode);

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnit",
                HeaderText = "Đơn vị",
                DataPropertyName = "Unit",
                Width = 80
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Giá bán",
                DataPropertyName = "SellingPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStock",
                HeaderText = "Tồn kho",
                DataPropertyName = "CurrentStock",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            var colImage = new DataGridViewImageColumn
            {
                Name = "colImage",
                HeaderText = "Ảnh",
                DataPropertyName = "ProductImage",
                Width = 100,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dgvProducts.Columns.Add(colImage);
        }

        private async Task LoadProducts()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
                    SELECT
                        p.code,
                        p.name,
                        p.barcode,
                        p.unit,
                        p.selling_price,
                        p.current_stock,
                        p.image_url
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    WHERE p.type = 'product'
                    ORDER BY p.code
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("BarcodeImage", typeof(Image));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("SellingPrice", typeof(decimal));
                dt.Columns.Add("CurrentStock", typeof(int));
                dt.Columns.Add("ImageUrl", typeof(string));
                dt.Columns.Add("ProductImage", typeof(Image));

                foreach (var item in rows)
                {
                    var row = dt.NewRow();
                    row["Code"] = item.code?.ToString() ?? "";
                    row["Name"] = item.name?.ToString() ?? "";
                    row["Barcode"] = item.barcode?.ToString() ?? "";
                    row["Unit"] = item.unit?.ToString() ?? "";
                    row["SellingPrice"] = Convert.ToDecimal(item.selling_price ?? 0);
                    row["CurrentStock"] = Convert.ToInt32(item.current_stock ?? 0);
                    row["ImageUrl"] = item.image_url?.ToString() ?? "";

                    string barcodeValue = item.barcode?.ToString();
                    if (!string.IsNullOrEmpty(barcodeValue))
                    {
                        row["BarcodeImage"] = GenerateBarcode(barcodeValue);
                    }

                    string imageUrl = item.image_url?.ToString();
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        row["ProductImage"] = await DownloadImageAsync(imageUrl);
                    }

                    dt.Rows.Add(row);
                }

                dgvProducts.DataSource = dt;

                MessageBox.Show($"Đã tải {rows.Count} sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Tạo ảnh mã vạch - SỬ DỤNG ZXING PIXELDATA
        private Image GenerateBarcode(string barcodeValue)
        {
            try
            {
                barcodeValue = barcodeValue?.Trim().Replace(" ", "").Replace("-", "");

                if (string.IsNullOrEmpty(barcodeValue))
                {
                    return CreatePlaceholderBarcode("N/A");
                }

                Console.WriteLine($"Tạo barcode: {barcodeValue} (Độ dài: {barcodeValue.Length})");

                BarcodeFormat format;
                string processedBarcode = barcodeValue;

                if (barcodeValue.All(char.IsDigit))
                {
                    int length = barcodeValue.Length;

                    if (length == 13)
                    {
                        format = BarcodeFormat.EAN_13;
                        processedBarcode = CalculateEAN13Checksum(barcodeValue);
                        Console.WriteLine($"→ EAN-13: {processedBarcode}");
                    }
                    else if (length == 12)
                    {
                        format = BarcodeFormat.EAN_13;
                        processedBarcode = CalculateEAN13Checksum(barcodeValue);
                        Console.WriteLine($"→ EAN-13 (từ 12 số): {processedBarcode}");
                    }
                    else if (length == 8)
                    {
                        format = BarcodeFormat.EAN_8;
                        Console.WriteLine($"→ EAN-8");
                    }
                    else
                    {
                        format = BarcodeFormat.CODE_128;
                        Console.WriteLine($"→ CODE-128");
                    }
                }
                else
                {
                    format = BarcodeFormat.CODE_128;
                    Console.WriteLine($"→ CODE-128 (có chữ)");
                }

                // Sử dụng BarcodeWriterPixelData để tạo barcode
                var writer = new ZXing.BarcodeWriterPixelData
                {
                    Format = format,
                    Options = new EncodingOptions
                    {
                        Width = 300,
                        Height = 80,
                        Margin = 5,
                        PureBarcode = false
                    }
                };

                var pixelData = writer.Write(processedBarcode);

                // Chuyển PixelData thành Bitmap
                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    var bitmapData = bitmap.LockBits(
                        new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    Console.WriteLine($"✓ Thành công!");
                    return new Bitmap(bitmap);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Lỗi tạo barcode '{barcodeValue}': {ex.Message}");

                // Thử CODE-128 nếu lỗi
                try
                {
                    var writer = new ZXing.BarcodeWriterPixelData
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Width = 300,
                            Height = 80,
                            Margin = 5,
                            PureBarcode = false
                        }
                    };

                    var pixelData = writer.Write(barcodeValue);

                    using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                    {
                        var bitmapData = bitmap.LockBits(
                            new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                        try
                        {
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }

                        return new Bitmap(bitmap);
                    }
                }
                catch
                {
                    return CreatePlaceholderBarcode(barcodeValue);
                }
            }
        }

        private string CalculateEAN13Checksum(string barcode)
        {
            try
            {
                string code = barcode.Length >= 12 ? barcode.Substring(0, 12) : barcode.PadLeft(12, '0');

                int sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    int digit = int.Parse(code[i].ToString());
                    sum += (i % 2 == 0) ? digit : digit * 3;
                }

                int checksum = (10 - (sum % 10)) % 10;
                return code + checksum;
            }
            catch
            {
                return barcode;
            }
        }

        private Image CreatePlaceholderBarcode(string barcodeValue)
        {
            Bitmap bmp = new Bitmap(300, 80);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 299, 79);

                using (Font font = new Font("Courier New", 10, FontStyle.Bold))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(barcodeValue ?? "ERROR", font, Brushes.Black, new RectangleF(0, 0, 300, 80), sf);
                }
            }
            return bmp;
        }

        private async Task<Image> DownloadImageAsync(string imageUrl)
        {
            try
            {
                var response = await httpClient.GetAsync(imageUrl);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var ms = new MemoryStream())
                    {
                        await stream.CopyToAsync(ms);
                        ms.Position = 0;
                        return Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tải ảnh từ {imageUrl}: {ex.Message}");
            }

            return CreatePlaceholderImage();
        }

        private Image CreatePlaceholderImage()
        {
            Bitmap bmp = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                g.DrawRectangle(Pens.Gray, 0, 0, 99, 99);

                using (Font font = new Font("Arial", 9))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("No Image", font, Brushes.DarkGray, new RectangleF(0, 0, 100, 100), sf);
                }
            }
            return bmp;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cluster?.Dispose();
        }
    }
}