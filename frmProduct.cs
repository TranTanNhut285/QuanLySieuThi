using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Couchbase;
using Couchbase.KeyValue;
using Couchbase.Query;
using ZXing;
using ZXing.Common;
using ClosedXML.Excel;

namespace Mini_Mart
{
    public partial class frmProduct : Form
    {
        private ICluster _cluster;
        private IBucket _bucket;
        private ICouchbaseCollection _collection;
        private string _selectedImagePath = "";
        private string _currentProductCode = "";
        private bool _isUpdatingStatus = false;

        // Dictionaries để mapping code -> name
        private Dictionary<string, string> _categoryCodeToName = new Dictionary<string, string>();
        private Dictionary<string, string> _supplierCodeToName = new Dictionary<string, string>();

        public frmProduct()
        {
            InitializeComponent();
            this.Load += frmProduct_Load;

            // Đăng ký các sự kiện
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cboFilterCategory.SelectedIndexChanged += CboFilterCategory_SelectedIndexChanged;
            dgvProducts.CellClick += DgvProducts_CellClick;
            dgvProducts.CellValueChanged += DgvProducts_CellValueChanged;
            dgvProducts.CurrentCellDirtyStateChanged += DgvProducts_CurrentCellDirtyStateChanged;

            btnAdd.Click += BtnAdd_Click;
            btnSave.Click += BtnSave_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnUpload.Click += BtnUpload_Click;
            btnExport.Click += BtnExport_Click;
        }

        private void DgvProducts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProducts.IsCurrentCellDirty)
            {
                dgvProducts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private async void frmProduct_Load(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập txtCode không cho phép nhập
                txtCode.ReadOnly = true;
                txtCode.BackColor = SystemColors.Control;

                // Thiết lập giới hạn cho NumericUpDown
                nudCostPrice.Maximum = 999999999;
                nudCostPrice.Minimum = 0;
                nudCostPrice.DecimalPlaces = 0;

                nudSellingPrice.Maximum = 999999999;
                nudSellingPrice.Minimum = 0;
                nudSellingPrice.DecimalPlaces = 0;

                nudCurrentStock.Maximum = 999999;
                nudCurrentStock.Minimum = 0;
                nudCurrentStock.DecimalPlaces = 0;

                nudMinStock.Maximum = 999999;
                nudMinStock.Minimum = 0;
                nudMinStock.DecimalPlaces = 0;

                nudMaxStock.Maximum = 999999;
                nudMaxStock.Minimum = 0;
                nudMaxStock.DecimalPlaces = 0;

                await ConnectCouchbase();
                ConfigureDataGridView();
                await LoadCategories();
                await LoadSuppliers();
                await LoadUnits();
                await LoadProducts();

                cboFilterCategory.Items.Insert(0, "Tất cả");
                cboFilterCategory.SelectedIndex = 0;

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
                _collection = _bucket.Scope("masterdata").Collection("products");
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
                cboFilterCategory.Items.Clear();
                _categoryCodeToName.Clear();

                foreach (var cat in categories)
                {
                    string catCode = cat.code?.ToString() ?? "";
                    string catId = cat._id?.ToString() ?? "";
                    string catName = cat.name?.ToString() ?? "";

                    // Lưu tất cả các dạng có thể
                    _categoryCodeToName[catCode] = catName;
                    _categoryCodeToName[$"category::{catCode}"] = catName;
                    if (!string.IsNullOrEmpty(catId))
                    {
                        _categoryCodeToName[catId] = catName;
                    }

                    cboCategory.Items.Add(catName);
                    cboFilterCategory.Items.Add(catName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSuppliers()
        {
            try
            {
                string query = @"
                    SELECT s.code, s.name, META(s).id as _id
                    FROM `mini_mart`.`masterdata`.`suppliers` AS s
                    WHERE s.type = 'supplier'
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

                    // Lưu tất cả các dạng có thể
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

        private Task LoadUnits()
        {
            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(new string[] { "kg", "gói", "chai", "hộp", "thùng", "cái", "viên", "lon", "cây" });
            return Task.CompletedTask;
        }

        private void ConfigureDataGridView()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.RowTemplate.Height = 100;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.RowHeadersVisible = false;

            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);

            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã SP",
                DataPropertyName = "Code",
                FillWeight = 7,
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "Name",
                FillWeight = 15,
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCategory",
                HeaderText = "Loại SP",
                DataPropertyName = "CategoryName",
                FillWeight = 10,
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Mô tả",
                DataPropertyName = "Description",
                FillWeight = 18,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    WrapMode = DataGridViewTriState.True
                }
            });

            var colBarcode = new DataGridViewImageColumn
            {
                Name = "colBarcode",
                HeaderText = "Mã vạch",
                DataPropertyName = "BarcodeImage",
                FillWeight = 12,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                ReadOnly = true
            };
            dgvProducts.Columns.Add(colBarcode);

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnit",
                HeaderText = "Đơn vị",
                DataPropertyName = "Unit",
                FillWeight = 6,
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCostPrice",
                HeaderText = "Giá vốn",
                DataPropertyName = "CostPrice",
                FillWeight = 8,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Giá bán",
                DataPropertyName = "SellingPrice",
                FillWeight = 8,
                ReadOnly = true,
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
                FillWeight = 6,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSupplier",
                HeaderText = "Nhà cung cấp",
                DataPropertyName = "SupplierName",
                FillWeight = 10,
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActive",
                HeaderText = "Hoạt động",
                DataPropertyName = "IsActive",
                FillWeight = 6,
                TrueValue = true,
                FalseValue = false,
                ReadOnly = false
            });

            var colImage = new DataGridViewImageColumn
            {
                Name = "colImage",
                HeaderText = "Ảnh",
                DataPropertyName = "ProductImage",
                FillWeight = 9,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                ReadOnly = true
            };
            dgvProducts.Columns.Add(colImage);
        }

        private async Task LoadProducts(string searchText = "", string filterCategory = "")
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<string> whereClauses = new List<string> { "p.type = 'product'" };

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClauses.Add($"LOWER(p.name) LIKE '%{searchText.ToLower()}%'");
                }

                // Lọc theo category - tìm tất cả các code có cùng name
                if (!string.IsNullOrEmpty(filterCategory) && filterCategory != "Tất cả")
                {
                    var matchingCodes = _categoryCodeToName
                        .Where(x => x.Value == filterCategory)
                        .Select(x => x.Key)
                        .ToList();

                    if (matchingCodes.Any())
                    {
                        string categoryFilter = string.Join(" OR ", matchingCodes.Select(code => $"p.category_id = '{code}'"));
                        whereClauses.Add($"({categoryFilter})");
                    }
                }

                string whereClause = "WHERE " + string.Join(" AND ", whereClauses);

                string query = $@"
                    SELECT
                        p.code,
                        p.name,
                        p.category_id,
                        p.description,
                        p.barcode,
                        p.unit,
                        p.cost_price,
                        p.selling_price,
                        p.current_stock,
                        p.min_stock,
                        p.max_stock,
                        p.supplier_id,
                        p.is_active,
                        p.image_url
                    FROM `mini_mart`.`masterdata`.`products` AS p
                    {whereClause}
                    ORDER BY p.code
                ";

                var result = await _cluster.QueryAsync<dynamic>(query);
                var rows = await result.Rows.ToListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("CategoryId", typeof(string));
                dt.Columns.Add("CategoryName", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("BarcodeImage", typeof(Image));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("CostPrice", typeof(decimal));
                dt.Columns.Add("SellingPrice", typeof(decimal));
                dt.Columns.Add("CurrentStock", typeof(int));
                dt.Columns.Add("MinStock", typeof(int));
                dt.Columns.Add("MaxStock", typeof(int));
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("SupplierName", typeof(string));
                dt.Columns.Add("IsActive", typeof(bool));
                dt.Columns.Add("ImageUrl", typeof(string));
                dt.Columns.Add("ProductImage", typeof(Image));

                foreach (var item in rows)
                {
                    var row = dt.NewRow();
                    row["Code"] = item.code?.ToString() ?? "";
                    row["Name"] = item.name?.ToString() ?? "";

                    string categoryId = item.category_id?.ToString() ?? "";
                    row["CategoryId"] = categoryId;
                    row["CategoryName"] = _categoryCodeToName.ContainsKey(categoryId) ? _categoryCodeToName[categoryId] : "";

                    row["Description"] = item.description?.ToString() ?? "";
                    row["Barcode"] = item.barcode?.ToString() ?? "";
                    row["Unit"] = item.unit?.ToString() ?? "";
                    row["CostPrice"] = Convert.ToDecimal(item.cost_price ?? 0);
                    row["SellingPrice"] = Convert.ToDecimal(item.selling_price ?? 0);
                    row["CurrentStock"] = Convert.ToInt32(item.current_stock ?? 0);
                    row["MinStock"] = Convert.ToInt32(item.min_stock ?? 0);
                    row["MaxStock"] = Convert.ToInt32(item.max_stock ?? 0);

                    string supplierId = item.supplier_id?.ToString() ?? "";
                    row["SupplierId"] = supplierId;
                    row["SupplierName"] = _supplierCodeToName.ContainsKey(supplierId) ? _supplierCodeToName[supplierId] : "";

                    row["IsActive"] = Convert.ToBoolean(item.is_active ?? true);
                    row["ImageUrl"] = item.image_url?.ToString() ?? "";

                    string barcodeValue = item.barcode?.ToString();
                    if (!string.IsNullOrEmpty(barcodeValue))
                    {
                        row["BarcodeImage"] = GenerateBarcode(barcodeValue);
                    }

                    string imageFileName = item.image_url?.ToString();
                    if (!string.IsNullOrEmpty(imageFileName))
                    {
                        row["ProductImage"] = LoadImageFromFile(imageFileName);
                    }

                    dt.Rows.Add(row);
                }

                dgvProducts.DataSource = dt;
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

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterCategory = cboFilterCategory.SelectedItem?.ToString() ?? "";
            await LoadProducts(txtSearch.Text, filterCategory);
        }

        private async void CboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterCategory = cboFilterCategory.SelectedItem?.ToString() ?? "";
            await LoadProducts(txtSearch.Text, filterCategory);
        }

        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                try
                {
                    var dt = (DataTable)dgvProducts.DataSource;
                    DataRow dataRow = dt.Rows[e.RowIndex];

                    _currentProductCode = dataRow["Code"]?.ToString() ?? "";
                    txtCode.Text = _currentProductCode;
                    txtName.Text = dataRow["Name"]?.ToString() ?? "";
                    txtBarcode.Text = dataRow["Barcode"]?.ToString() ?? "";
                    txtDescription.Text = dataRow["Description"]?.ToString() ?? "";

                    // Chọn category theo tên
                    string categoryName = dataRow["CategoryName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(categoryName) && cboCategory.Items.Contains(categoryName))
                        cboCategory.SelectedItem = categoryName;
                    else
                        cboCategory.SelectedIndex = -1;

                    // Chọn supplier theo tên
                    string supplierName = dataRow["SupplierName"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(supplierName) && cboSupplier.Items.Contains(supplierName))
                        cboSupplier.SelectedItem = supplierName;
                    else
                        cboSupplier.SelectedIndex = -1;

                    string unit = dataRow["Unit"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(unit) && cboUnit.Items.Contains(unit))
                        cboUnit.SelectedItem = unit;
                    else
                        cboUnit.SelectedIndex = -1;

                    // Set giá trị cho NumericUpDown
                    nudCostPrice.Value = Convert.ToDecimal(dataRow["CostPrice"] ?? 0);
                    nudSellingPrice.Value = Convert.ToDecimal(dataRow["SellingPrice"] ?? 0);
                    nudCurrentStock.Value = Convert.ToDecimal(dataRow["CurrentStock"] ?? 0);
                    nudMinStock.Value = Convert.ToDecimal(dataRow["MinStock"] ?? 0);
                    nudMaxStock.Value = Convert.ToDecimal(dataRow["MaxStock"] ?? 0);

                    _selectedImagePath = dataRow["ImageUrl"]?.ToString() ?? "";

                    // Hiển thị preview ảnh nếu có
                    if (!string.IsNullOrEmpty(_selectedImagePath))
                    {
                        var previewImage = LoadImageFromFile(_selectedImagePath);
                        if (previewImage != null)
                        {
                            ShowImagePreview(previewImage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void DgvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && !_isUpdatingStatus)
            {
                string columnName = dgvProducts.Columns[e.ColumnIndex].Name;

                if (columnName == "colActive")
                {
                    _isUpdatingStatus = true;
                    try
                    {
                        var dt = (DataTable)dgvProducts.DataSource;
                        string code = dt.Rows[e.RowIndex]["Code"]?.ToString();
                        bool isActive = Convert.ToBoolean(dgvProducts.Rows[e.RowIndex].Cells["colActive"].Value);

                        await UpdateProductStatus(code, isActive);

                        // Cập nhật DataTable để không bị reset
                        dt.Rows[e.RowIndex]["IsActive"] = isActive;
                    }
                    finally
                    {
                        _isUpdatingStatus = false;
                    }
                }
            }
        }

        private async Task UpdateProductStatus(string code, bool isActive)
        {
            try
            {
                string documentId = $"product::{code}";
                var result = await _collection.GetAsync(documentId);
                var content = result.ContentAs<Dictionary<string, object>>();

                content["is_active"] = isActive;
                content["updated_at"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                content["updated_by"] = "admin";

                await _collection.ReplaceAsync(documentId, content);

                MessageBox.Show($"Đã cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật trạng thái: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _currentProductCode = "";
            txtCode.Clear(); // Xóa mã sản phẩm khi thêm mới
            txtName.Clear();
            txtBarcode.Clear();
            txtDescription.Clear();
            cboCategory.SelectedIndex = -1;
            cboUnit.SelectedIndex = -1;
            cboSupplier.SelectedIndex = -1;
            nudCostPrice.Value = 0;
            nudSellingPrice.Value = 0;
            nudCurrentStock.Value = 0;
            nudMinStock.Value = 0;
            nudMaxStock.Value = 0;
            _selectedImagePath = "";

            panelPreview.Controls.Clear();
        }


        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ========== VALIDATE CÁC TRƯỜNG BẮT BUỘC ==========

                // Validate tên sản phẩm
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                // Validate barcode
                if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã vạch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBarcode.Focus();
                    return;
                }

                // Validate danh mục
                if (cboCategory.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboCategory.Focus();
                    return;
                }

                // Validate đơn vị
                if (cboUnit.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn đơn vị tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboUnit.Focus();
                    return;
                }

                // Validate nhà cung cấp
                if (cboSupplier.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSupplier.Focus();
                    return;
                }

                // ========== VALIDATE CÁC TRƯỜNG SỐ PHẢI > 0 ==========

                // Validate giá vốn phải > 0
                if (nudCostPrice.Value <= 0)
                {
                    MessageBox.Show("Giá vốn phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudCostPrice.Focus();
                    return;
                }

                // Validate giá bán phải > 0
                if (nudSellingPrice.Value <= 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudSellingPrice.Focus();
                    return;
                }

                // Validate max stock phải > 0
                if (nudMaxStock.Value <= 0)
                {
                    MessageBox.Show("Tồn kho tối đa phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudMaxStock.Focus();
                    return;
                }

                // ========== VALIDATE LOGIC NGHIỆP VỤ ==========

                // Validate giá bán >= giá vốn
                if (nudSellingPrice.Value < nudCostPrice.Value)
                {
                    MessageBox.Show("Giá bán không được thấp hơn giá vốn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudSellingPrice.Focus();
                    return;
                }

                // Validate min stock <= max stock
                if (nudMinStock.Value > nudMaxStock.Value)
                {
                    MessageBox.Show("Tồn kho tối thiểu không được lớn hơn tồn kho tối đa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudMinStock.Focus();
                    return;
                }

                // Validate min stock <= current stock <= max stock
                if (nudCurrentStock.Value < nudMinStock.Value)
                {
                    MessageBox.Show("Tồn kho hiện tại không được nhỏ hơn tồn kho tối thiểu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudCurrentStock.Focus();
                    return;
                }

                if (nudCurrentStock.Value > nudMaxStock.Value)
                {
                    MessageBox.Show("Tồn kho hiện tại không được lớn hơn tồn kho tối đa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudCurrentStock.Focus();
                    return;
                }

                // ========== LƯU SAN PHẨM ==========

                // Tạo mã sản phẩm mới nếu chưa có
                string productCode = string.IsNullOrEmpty(_currentProductCode) ? await GenerateProductCode() : _currentProductCode;

                // TẠO DOCUMENT ID ĐÚNG FORMAT
                string documentId = $"product::{productCode}";

                // Lấy category code
                string categoryName = cboCategory.SelectedItem?.ToString() ?? "";
                string categoryCode = GetCategoryCodeByName(categoryName);

                // Lấy supplier code
                string supplierName = cboSupplier.SelectedItem?.ToString() ?? "";
                string supplierCode = GetSupplierCodeByName(supplierName);

                // Xử lý upload ảnh nếu có
                string imagePath = _selectedImagePath;
                if (!string.IsNullOrEmpty(_selectedImagePath) && !_selectedImagePath.StartsWith("D:\\"))
                {
                    imagePath = await SaveImageToResources(_selectedImagePath, productCode);
                }

                // Tạo document
                var product = new Dictionary<string, object>
                {
                    ["type"] = "product",
                    ["code"] = productCode,
                    ["name"] = txtName.Text.Trim(),
                    ["category_id"] = categoryCode,
                    ["description"] = txtDescription.Text.Trim(),
                    ["barcode"] = txtBarcode.Text.Trim(),
                    ["unit"] = cboUnit.SelectedItem?.ToString() ?? "",
                    ["cost_price"] = nudCostPrice.Value,
                    ["selling_price"] = nudSellingPrice.Value,
                    ["current_stock"] = (int)nudCurrentStock.Value,
                    ["min_stock"] = (int)nudMinStock.Value,
                    ["max_stock"] = (int)nudMaxStock.Value,
                    ["supplier_id"] = supplierCode,
                    ["is_active"] = true,
                    ["image_url"] = imagePath,
                    ["created_at"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    ["created_by"] = "admin",
                    ["updated_at"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    ["updated_by"] = "admin"
                };

                if (string.IsNullOrEmpty(_currentProductCode))
                {
                    await _collection.InsertAsync(documentId, product);
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _collection.ReplaceAsync(documentId, product);
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await LoadProducts();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task<string> GenerateProductCode()
        {
            try
            {
                // Lấy mã sản phẩm lớn nhất hiện có
                string query = @"
            SELECT RAW p.code
            FROM `mini_mart`.`masterdata`.`products` AS p
            WHERE p.type = 'product'
            ORDER BY p.code DESC
            LIMIT 1
        ";

                var result = await _cluster.QueryAsync<string>(query);
                var lastCode = await result.Rows.FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(lastCode))
                {
                    // Nếu chưa có sản phẩm nào, bắt đầu từ P001
                    return "P001";
                }

                // Tách phần số từ mã (ví dụ: P030 -> 030)
                string numericPart = lastCode.Substring(1); // Bỏ ký tự 'P'

                // Chuyển thành số và tăng lên 1
                int nextNumber = int.Parse(numericPart) + 1;

                // Format lại thành P###
                return $"P{nextNumber:D3}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi generate code: {ex.Message}");
                // Nếu có lỗi, bắt đầu từ P001
                return "P001";
            }
        }


        private string GetCategoryCodeByName(string categoryName)
        {
            var entry = _categoryCodeToName.FirstOrDefault(x => x.Value == categoryName);
            return entry.Key ?? "";
        }

        private string GetSupplierCodeByName(string supplierName)
        {
            if (string.IsNullOrEmpty(supplierName)) return "";
            var entry = _supplierCodeToName.FirstOrDefault(x => x.Value == supplierName);
            return entry.Key ?? "";
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentProductCode))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentProductCode))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm '{txtName.Text}'?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    string documentId = $"product::{_currentProductCode}";
                    await _collection.RemoveAsync(documentId);
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadProducts();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    ofd.Title = "Chọn ảnh sản phẩm";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Đọc và validate ảnh
                            Image testImage = Image.FromFile(ofd.FileName);

                            _selectedImagePath = ofd.FileName;

                            // Hiển thị preview vào panelPreview
                            ShowImagePreview(testImage);

                            MessageBox.Show("Chọn ảnh thành công! Ảnh sẽ được lưu khi bấm Lưu.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("File không phải là ảnh hợp lệ! Vui lòng chọn file khác.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chọn ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowImagePreview(Image image)
        {
            try
            {
                // Xóa control cũ nếu có
                panelPreview.Controls.Clear();

                // Tạo PictureBox mới
                PictureBox pictureBox = new PictureBox
                {
                    Image = new Bitmap(image),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill
                };

                panelPreview.Controls.Add(pictureBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị preview: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> SaveImageToResources(string sourcePath, string productCode)
        {
            try
            {
                string resourcesFolder = Path.Combine(@"D:\Mini_Mart", "Resources", "products");

                if (!Directory.Exists(resourcesFolder))
                {
                    Directory.CreateDirectory(resourcesFolder);
                }

                string extension = Path.GetExtension(sourcePath);
                string newFileName = $"{productCode}{extension}";
                string destPath = Path.Combine(resourcesFolder, newFileName);

                // Copy file
                File.Copy(sourcePath, destPath, true);

                return destPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return sourcePath;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProducts.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

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
                var worksheet = workbook.Worksheets.Add("Sản phẩm");

                // Tiêu đề
                worksheet.Cell(1, 1).Value = "Mã SP";
                worksheet.Cell(1, 2).Value = "Tên sản phẩm";
                worksheet.Cell(1, 3).Value = "Loại SP";
                worksheet.Cell(1, 4).Value = "Mô tả";
                worksheet.Cell(1, 5).Value = "Đơn vị";
                worksheet.Cell(1, 6).Value = "Giá vốn";
                worksheet.Cell(1, 7).Value = "Giá bán";
                worksheet.Cell(1, 8).Value = "Tồn kho";
                worksheet.Cell(1, 9).Value = "Nhà cung cấp";
                worksheet.Cell(1, 10).Value = "Trạng thái";

                // Định dạng tiêu đề
                var headerRange = worksheet.Range(1, 1, 1, 10);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Dữ liệu
                var dt = (DataTable)dgvProducts.DataSource;
                int row = 2;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dr["Code"].ToString();
                    worksheet.Cell(row, 2).Value = dr["Name"].ToString();
                    worksheet.Cell(row, 3).Value = dr["CategoryName"].ToString();
                    worksheet.Cell(row, 4).Value = dr["Description"].ToString();
                    worksheet.Cell(row, 5).Value = dr["Unit"].ToString();
                    worksheet.Cell(row, 6).Value = Convert.ToDecimal(dr["CostPrice"]);
                    worksheet.Cell(row, 7).Value = Convert.ToDecimal(dr["SellingPrice"]);
                    worksheet.Cell(row, 8).Value = Convert.ToInt32(dr["CurrentStock"]);
                    worksheet.Cell(row, 9).Value = dr["SupplierName"].ToString();
                    worksheet.Cell(row, 10).Value = Convert.ToBoolean(dr["IsActive"]) ? "Hoạt động" : "Không hoạt động";

                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private Image LoadImageFromFile(string imagePathOrFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePathOrFileName))
                {
                    return CreatePlaceholderImage();
                }

                imagePathOrFileName = imagePathOrFileName.Trim();
                string imagePath = "";

                if (imagePathOrFileName.Contains(":\\"))
                {
                    imagePath = imagePathOrFileName;
                }
                else
                {
                    string[] possiblePaths = new string[]
                    {
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..", "Resources", "products", imagePathOrFileName),
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources", "products", imagePathOrFileName),
                        Path.Combine(@"D:\Mini_Mart", "Resources", "products", imagePathOrFileName),
                        Path.Combine(Application.StartupPath, "Resources", "products", imagePathOrFileName)
                    };

                    foreach (string path in possiblePaths)
                    {
                        string normalizedPath = Path.GetFullPath(path);
                        if (File.Exists(normalizedPath))
                        {
                            imagePath = normalizedPath;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return CreatePlaceholderImage();
                    }
                }

                byte[] imageBytes = File.ReadAllBytes(imagePath);
                using (var ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    return new Bitmap(img);
                }
            }
            catch
            {
                return CreatePlaceholderImage();
            }
        }

        private Image GenerateBarcode(string barcodeValue)
        {
            try
            {
                barcodeValue = barcodeValue?.Trim().Replace(" ", "").Replace("-", "");

                if (string.IsNullOrEmpty(barcodeValue))
                {
                    return CreatePlaceholderBarcode("N/A");
                }

                BarcodeFormat format;
                string processedBarcode = barcodeValue;

                if (barcodeValue.All(char.IsDigit))
                {
                    int length = barcodeValue.Length;

                    if (length == 13)
                    {
                        format = BarcodeFormat.EAN_13;
                        processedBarcode = CalculateEAN13Checksum(barcodeValue);
                    }
                    else if (length == 12)
                    {
                        format = BarcodeFormat.EAN_13;
                        processedBarcode = CalculateEAN13Checksum(barcodeValue);
                    }
                    else if (length == 8)
                    {
                        format = BarcodeFormat.EAN_8;
                    }
                    else
                    {
                        format = BarcodeFormat.CODE_128;
                    }
                }
                else
                {
                    format = BarcodeFormat.CODE_128;
                }

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
