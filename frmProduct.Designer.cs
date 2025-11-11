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
            label14 = new Label();
            txtDescription = new TextBox();
            nudMaxStock = new NumericUpDown();
            checkBox1 = new CheckBox();
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
            btnRefresh = new Button();
            btnCancel = new Button();
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
            label5.Location = new Point(618, 22);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(515, 65);
            label5.TabIndex = 9;
            label5.Text = "Quản lý sản phẩm";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Controls.Add(nudMaxStock);
            groupBox1.Controls.Add(checkBox1);
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
            groupBox1.Location = new Point(9, 111);
            groupBox1.Margin = new Padding(6, 7, 6, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 7, 6, 7);
            groupBox1.Size = new Size(1809, 490);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin sản phẩm";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 9.75F);
            label14.Location = new Point(659, 409);
            label14.Margin = new Padding(6, 0, 6, 0);
            label14.Name = "label14";
            label14.Size = new Size(128, 30);
            label14.TabIndex = 25;
            label14.Text = "Trạng thái";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtDescription.Location = new Point(1421, 325);
            txtDescription.Margin = new Padding(6, 7, 6, 7);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(255, 37);
            txtDescription.TabIndex = 24;
            // 
            // nudMaxStock
            // 
            nudMaxStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudMaxStock.Location = new Point(1421, 236);
            nudMaxStock.Margin = new Padding(6, 7, 6, 7);
            nudMaxStock.Name = "nudMaxStock";
            nudMaxStock.Size = new Size(260, 37);
            nudMaxStock.TabIndex = 23;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Microsoft Sans Serif", 9.75F);
            checkBox1.Location = new Point(817, 406);
            checkBox1.Margin = new Padding(6, 7, 6, 7);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(237, 34);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Đang kinh doanh";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // nudMinStock
            // 
            nudMinStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudMinStock.Location = new Point(1421, 148);
            nudMinStock.Margin = new Padding(6, 7, 6, 7);
            nudMinStock.Name = "nudMinStock";
            nudMinStock.Size = new Size(260, 37);
            nudMinStock.TabIndex = 22;
            // 
            // nudCurrentStock
            // 
            nudCurrentStock.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudCurrentStock.Location = new Point(1421, 39);
            nudCurrentStock.Margin = new Padding(6, 7, 6, 7);
            nudCurrentStock.Name = "nudCurrentStock";
            nudCurrentStock.Size = new Size(260, 37);
            nudCurrentStock.TabIndex = 21;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 9.75F);
            label13.Location = new Point(1280, 332);
            label13.Margin = new Padding(6, 0, 6, 0);
            label13.Name = "label13";
            label13.Size = new Size(84, 30);
            label13.TabIndex = 11;
            label13.Text = "Mô tả:";
            // 
            // nudSellingPrice
            // 
            nudSellingPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudSellingPrice.Location = new Point(856, 313);
            nudSellingPrice.Margin = new Padding(6, 7, 6, 7);
            nudSellingPrice.Name = "nudSellingPrice";
            nudSellingPrice.Size = new Size(260, 37);
            nudSellingPrice.TabIndex = 20;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F);
            label11.Location = new Point(1280, 145);
            label11.Margin = new Padding(6, 0, 6, 0);
            label11.Name = "label11";
            label11.Size = new Size(62, 30);
            label11.TabIndex = 9;
            label11.Text = "Min:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F);
            label12.Location = new Point(1280, 241);
            label12.Margin = new Padding(6, 0, 6, 0);
            label12.Name = "label12";
            label12.Size = new Size(68, 30);
            label12.TabIndex = 10;
            label12.Text = "Max:";
            // 
            // nudCostPrice
            // 
            nudCostPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudCostPrice.Location = new Point(856, 236);
            nudCostPrice.Margin = new Padding(6, 7, 6, 7);
            nudCostPrice.Name = "nudCostPrice";
            nudCostPrice.Size = new Size(260, 37);
            nudCostPrice.TabIndex = 19;
            // 
            // cboSupplier
            // 
            cboSupplier.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboSupplier.FormattingEnabled = true;
            cboSupplier.Location = new Point(856, 135);
            cboSupplier.Margin = new Padding(6, 7, 6, 7);
            cboSupplier.Name = "cboSupplier";
            cboSupplier.Size = new Size(258, 38);
            cboSupplier.TabIndex = 18;
            // 
            // cboUnit
            // 
            cboUnit.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboUnit.FormattingEnabled = true;
            cboUnit.Location = new Point(856, 34);
            cboUnit.Margin = new Padding(6, 7, 6, 7);
            cboUnit.Name = "cboUnit";
            cboUnit.Size = new Size(258, 38);
            cboUnit.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F);
            label10.Location = new Point(1280, 54);
            label10.Margin = new Padding(6, 0, 6, 0);
            label10.Name = "label10";
            label10.Size = new Size(112, 30);
            label10.TabIndex = 8;
            label10.Text = "Tồn kho:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F);
            label9.Location = new Point(615, 318);
            label9.Margin = new Padding(6, 0, 6, 0);
            label9.Name = "label9";
            label9.Size = new Size(109, 30);
            label9.TabIndex = 7;
            label9.Text = "Giá bán:";
            // 
            // cboCategory
            // 
            cboCategory.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(228, 295);
            cboCategory.Margin = new Padding(6, 7, 6, 7);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(288, 38);
            cboCategory.TabIndex = 16;
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtBarcode.Location = new Point(228, 219);
            txtBarcode.Margin = new Padding(6, 7, 6, 7);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(288, 37);
            txtBarcode.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F);
            label7.Location = new Point(615, 143);
            label7.Margin = new Padding(6, 0, 6, 0);
            label7.Name = "label7";
            label7.Size = new Size(177, 30);
            label7.TabIndex = 5;
            label7.Text = "Nhà cung cấp:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F);
            label8.Location = new Point(615, 226);
            label8.Margin = new Padding(6, 0, 6, 0);
            label8.Name = "label8";
            label8.Size = new Size(108, 30);
            label8.TabIndex = 6;
            label8.Text = "Giá vốn:";
            // 
            // txtName
            // 
            txtName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtName.Location = new Point(228, 140);
            txtName.Margin = new Padding(6, 7, 6, 7);
            txtName.Name = "txtName";
            txtName.Size = new Size(288, 37);
            txtName.TabIndex = 14;
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtCode.Location = new Point(228, 57);
            txtCode.Margin = new Padding(6, 7, 6, 7);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(288, 37);
            txtCode.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(615, 54);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(141, 30);
            label6.TabIndex = 4;
            label6.Text = "Đơn vị tính:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(13, 313);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(137, 30);
            label4.TabIndex = 3;
            label4.Text = "Danh mục:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(13, 226);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(115, 30);
            label3.TabIndex = 2;
            label3.Text = "Barcode:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(13, 155);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(183, 30);
            label2.TabIndex = 1;
            label2.Text = "Tên sản phẩm:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(13, 64);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(175, 30);
            label1.TabIndex = 0;
            label1.Text = "Mã sản phẩm:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnExport);
            groupBox2.Controls.Add(btnRefresh);
            groupBox2.Controls.Add(btnCancel);
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(btnEdit);
            groupBox2.Controls.Add(btnAdd);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(dgvProducts);
            groupBox2.Controls.Add(cboFilterCategory);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(txtSearch);
            groupBox2.Controls.Add(label15);
            groupBox2.Location = new Point(9, 615);
            groupBox2.Margin = new Padding(6, 7, 6, 7);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(6, 7, 6, 7);
            groupBox2.Size = new Size(1809, 812);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách sản phẩm";
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(1586, 694);
            btnExport.Margin = new Padding(6, 7, 6, 7);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(210, 94);
            btnExport.TabIndex = 25;
            btnExport.Text = "Xuất Excel";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(1324, 694);
            btnRefresh.Margin = new Padding(6, 7, 6, 7);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(210, 94);
            btnRefresh.TabIndex = 24;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(1064, 694);
            btnCancel.Margin = new Padding(6, 7, 6, 7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(210, 94);
            btnCancel.TabIndex = 23;
            btnCancel.Text = "Huỷ";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(795, 694);
            btnSave.Margin = new Padding(6, 7, 6, 7);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(210, 94);
            btnSave.TabIndex = 22;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.Location = new Point(264, 694);
            btnEdit.Margin = new Padding(6, 7, 6, 7);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(210, 94);
            btnEdit.TabIndex = 20;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(13, 694);
            btnAdd.Margin = new Padding(6, 7, 6, 7);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(210, 94);
            btnAdd.TabIndex = 19;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(526, 694);
            btnDelete.Margin = new Padding(6, 7, 6, 7);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(210, 94);
            btnDelete.TabIndex = 21;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(0, 143);
            dgvProducts.Margin = new Padding(6, 7, 6, 7);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 82;
            dgvProducts.Size = new Size(1796, 514);
            dgvProducts.TabIndex = 18;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboFilterCategory.FormattingEnabled = true;
            cboFilterCategory.Location = new Point(1242, 69);
            cboFilterCategory.Margin = new Padding(6, 7, 6, 7);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(288, 38);
            cboFilterCategory.TabIndex = 17;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Sans Serif", 9.75F);
            label16.Location = new Point(964, 89);
            label16.Margin = new Padding(6, 0, 6, 0);
            label16.Name = "label16";
            label16.Size = new Size(236, 30);
            label16.TabIndex = 14;
            label16.Text = "Lọc theo danh mục:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtSearch.Location = new Point(186, 74);
            txtSearch.Margin = new Padding(6, 7, 6, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(511, 37);
            txtSearch.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(32, 89);
            label15.Margin = new Padding(6, 0, 6, 0);
            label15.Name = "label15";
            label15.Size = new Size(126, 30);
            label15.TabIndex = 12;
            label15.Text = "Tìm kiếm:";
            // 
            // frmProduct
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1835, 1433);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Margin = new Padding(6, 7, 6, 7);
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown nudMaxStock;
        private System.Windows.Forms.NumericUpDown nudMinStock;
        private System.Windows.Forms.NumericUpDown nudCurrentStock;
        private System.Windows.Forms.NumericUpDown nudSellingPrice;
        private System.Windows.Forms.NumericUpDown nudCostPrice;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}