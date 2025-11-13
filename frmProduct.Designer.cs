namespace Mini_Mart
{
    partial class frmProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label5 = new Label();
            groupBox1 = new GroupBox();
            panelPreview = new Panel();
            btnUpload = new Button();
            label14 = new Label();
            txtDescription = new TextBox();
            nudMaxStock = new NumericUpDown();
            nudMinStock = new NumericUpDown();
            nudCurrentStock = new NumericUpDown();
            label13 = new Label();
            nudSellingPrice = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            nudCostPrice = new NumericUpDown();
            cboSupplier = new ComboBox();
            cboUnit = new ComboBox();
            label10 = new Label();
            label9 = new Label();
            cboCategory = new ComboBox();
            txtBarcode = new TextBox();
            label7 = new Label();
            label8 = new Label();
            txtName = new TextBox();
            txtCode = new TextBox();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnExport = new Button();
            btnSave = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            dgvProducts = new DataGridView();
            cboFilterCategory = new ComboBox();
            label16 = new Label();
            txtSearch = new TextBox();
            label15 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMaxStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCurrentStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSellingPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCostPrice).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 20.25F, FontStyle.Bold);
            label5.Location = new Point(333, 10);
            label5.Name = "label5";
            label5.Size = new Size(257, 33);
            label5.TabIndex = 9;
            label5.Text = "Quản lý sản phẩm";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panelPreview);
            groupBox1.Controls.Add(btnUpload);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Controls.Add(nudMaxStock);
            groupBox1.Controls.Add(nudMinStock);
            groupBox1.Controls.Add(nudCurrentStock);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(nudSellingPrice);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(nudCostPrice);
            groupBox1.Controls.Add(cboSupplier);
            groupBox1.Controls.Add(cboUnit);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(cboCategory);
            groupBox1.Controls.Add(txtBarcode);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(txtName);
            groupBox1.Controls.Add(txtCode);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(5, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(974, 230);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin sản phẩm";
            // 
            // panelPreview
            // 
            panelPreview.Location = new Point(239, 161);
            panelPreview.Margin = new Padding(2, 1, 2, 1);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(87, 64);
            panelPreview.TabIndex = 27;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(123, 178);
            btnUpload.Margin = new Padding(2, 1, 2, 1);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(105, 22);
            btnUpload.TabIndex = 26;
            btnUpload.Text = "Upload Here";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 9.75F);
            label14.Location = new Point(7, 185);
            label14.Name = "label14";
            label14.Size = new Size(95, 16);
            label14.TabIndex = 25;
            label14.Text = "Ảnh sản phẩm:";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtDescription.Location = new Point(765, 111);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(198, 116);
            txtDescription.TabIndex = 24;
            // 
            // nudMaxStock
            // 
            nudMaxStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudMaxStock.Location = new Point(765, 72);
            nudMaxStock.Name = "nudMaxStock";
            nudMaxStock.Size = new Size(140, 22);
            nudMaxStock.TabIndex = 23;
            // 
            // nudMinStock
            // 
            nudMinStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudMinStock.Location = new Point(765, 27);
            nudMinStock.Name = "nudMinStock";
            nudMinStock.Size = new Size(140, 22);
            nudMinStock.TabIndex = 22;
            // 
            // nudCurrentStock
            // 
            nudCurrentStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudCurrentStock.Location = new Point(461, 66);
            nudCurrentStock.Name = "nudCurrentStock";
            nudCurrentStock.Size = new Size(140, 22);
            nudCurrentStock.TabIndex = 21;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 9.75F);
            label13.Location = new Point(689, 106);
            label13.Name = "label13";
            label13.Size = new Size(43, 16);
            label13.TabIndex = 11;
            label13.Text = "Mô tả:";
            // 
            // nudSellingPrice
            // 
            nudSellingPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudSellingPrice.Location = new Point(461, 147);
            nudSellingPrice.Name = "nudSellingPrice";
            nudSellingPrice.Size = new Size(140, 22);
            nudSellingPrice.TabIndex = 20;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F);
            label11.Location = new Point(689, 25);
            label11.Name = "label11";
            label11.Size = new Size(31, 16);
            label11.TabIndex = 9;
            label11.Text = "Min:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F);
            label12.Location = new Point(689, 66);
            label12.Name = "label12";
            label12.Size = new Size(35, 16);
            label12.TabIndex = 10;
            label12.Text = "Max:";
            // 
            // nudCostPrice
            // 
            nudCostPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudCostPrice.Location = new Point(461, 111);
            nudCostPrice.Name = "nudCostPrice";
            nudCostPrice.Size = new Size(140, 22);
            nudCostPrice.TabIndex = 19;
            // 
            // cboSupplier
            // 
            cboSupplier.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboSupplier.FormattingEnabled = true;
            cboSupplier.Location = new Point(461, 191);
            cboSupplier.Name = "cboSupplier";
            cboSupplier.Size = new Size(300, 24);
            cboSupplier.TabIndex = 18;
            // 
            // cboUnit
            // 
            cboUnit.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboUnit.FormattingEnabled = true;
            cboUnit.Location = new Point(461, 16);
            cboUnit.Name = "cboUnit";
            cboUnit.Size = new Size(213, 24);
            cboUnit.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F);
            label10.Location = new Point(336, 66);
            label10.Name = "label10";
            label10.Size = new Size(59, 16);
            label10.TabIndex = 8;
            label10.Text = "Tồn kho:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F);
            label9.Location = new Point(331, 149);
            label9.Name = "label9";
            label9.Size = new Size(57, 16);
            label9.TabIndex = 7;
            label9.Text = "Giá bán:";
            // 
            // cboCategory
            // 
            cboCategory.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(123, 138);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(189, 24);
            cboCategory.TabIndex = 16;
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtBarcode.Location = new Point(123, 103);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(189, 22);
            txtBarcode.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F);
            label7.Location = new Point(328, 191);
            label7.Name = "label7";
            label7.Size = new Size(93, 16);
            label7.TabIndex = 5;
            label7.Text = "Nhà cung cấp:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F);
            label8.Location = new Point(331, 106);
            label8.Name = "label8";
            label8.Size = new Size(56, 16);
            label8.TabIndex = 6;
            label8.Text = "Giá vốn:";
            // 
            // txtName
            // 
            txtName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtName.Location = new Point(123, 66);
            txtName.Name = "txtName";
            txtName.Size = new Size(189, 22);
            txtName.TabIndex = 14;
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtCode.Location = new Point(123, 27);
            txtCode.Name = "txtCode";
            txtCode.ReadOnly = true;
            txtCode.Size = new Size(157, 22);
            txtCode.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(331, 25);
            label6.Name = "label6";
            label6.Size = new Size(70, 16);
            label6.TabIndex = 4;
            label6.Text = "Đơn vị tính:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(7, 147);
            label4.Name = "label4";
            label4.Size = new Size(70, 16);
            label4.TabIndex = 3;
            label4.Text = "Danh mục:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(7, 106);
            label3.Name = "label3";
            label3.Size = new Size(62, 16);
            label3.TabIndex = 2;
            label3.Text = "Barcode:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(7, 73);
            label2.Name = "label2";
            label2.Size = new Size(96, 16);
            label2.TabIndex = 1;
            label2.Text = "Tên sản phẩm:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(7, 30);
            label1.Name = "label1";
            label1.Size = new Size(91, 16);
            label1.TabIndex = 0;
            label1.Text = "Mã sản phẩm:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnExport);
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(btnEdit);
            groupBox2.Controls.Add(btnAdd);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(dgvProducts);
            groupBox2.Controls.Add(cboFilterCategory);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(txtSearch);
            groupBox2.Controls.Add(label15);
            groupBox2.Location = new Point(5, 288);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(974, 381);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách sản phẩm";
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(601, 325);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(113, 44);
            btnExport.TabIndex = 25;
            btnExport.Text = "Xuất Excel";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(428, 325);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(113, 44);
            btnSave.TabIndex = 22;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.Location = new Point(142, 325);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(113, 44);
            btnEdit.TabIndex = 20;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(7, 325);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(113, 44);
            btnAdd.TabIndex = 19;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(283, 325);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(113, 44);
            btnDelete.TabIndex = 21;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(0, 67);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 82;
            dgvProducts.Size = new Size(967, 241);
            dgvProducts.TabIndex = 18;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboFilterCategory.FormattingEnabled = true;
            cboFilterCategory.Location = new Point(680, 38);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(213, 24);
            cboFilterCategory.TabIndex = 17;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Sans Serif", 9.75F);
            label16.Location = new Point(519, 42);
            label16.Name = "label16";
            label16.Size = new Size(122, 16);
            label16.TabIndex = 14;
            label16.Text = "Lọc theo danh mục:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtSearch.Location = new Point(100, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(277, 22);
            txtSearch.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(17, 42);
            label15.Name = "label15";
            label15.Size = new Size(65, 16);
            label15.TabIndex = 12;
            label15.Text = "Tìm kiếm:";
            // 
            // frmProduct
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 497);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Name = "frmProduct";
            Text = "frmProduct";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMaxStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCurrentStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSellingPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCostPrice).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown nudMaxStock;
        private System.Windows.Forms.NumericUpDown nudMinStock;
        private System.Windows.Forms.NumericUpDown nudCurrentStock;
        private System.Windows.Forms.NumericUpDown nudSellingPrice;
        private System.Windows.Forms.NumericUpDown nudCostPrice;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private Button btnUpload;
        private Label label14;
        private Panel panelPreview;
    }
}