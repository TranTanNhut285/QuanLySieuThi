namespace Mini_Mart
{
    partial class frmReceiptTransaction
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
            txtNote = new TextBox();
            txtStaff = new TextBox();
            cboSupplier = new ComboBox();
            dtpTransactionDate = new DateTimePicker();
            txtTransactionCode = new TextBox();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            txtTotalAmount = new TextBox();
            label14 = new Label();
            dataGridView1 = new DataGridView();
            btnSave = new Button();
            btnCancel = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            txtTotalPrice = new TextBox();
            label13 = new Label();
            nudUnitPrice = new NumericUpDown();
            label12 = new Label();
            nudQuantity = new NumericUpDown();
            label11 = new Label();
            txtProductName = new TextBox();
            label10 = new Label();
            txtUnit = new TextBox();
            label9 = new Label();
            txtProductCode = new TextBox();
            label8 = new Label();
            cboProduct = new ComboBox();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 20.25F, FontStyle.Bold);
            label5.Location = new Point(407, 10);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(241, 33);
            label5.TabIndex = 10;
            label5.Text = "Phiếu nhập hàng";
            label5.UseWaitCursor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(txtStaff);
            groupBox1.Controls.Add(cboSupplier);
            groupBox1.Controls.Add(dtpTransactionDate);
            groupBox1.Controls.Add(txtTransactionCode);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(2, 52);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(1093, 164);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin phiếu nhập";
            groupBox1.UseWaitCursor = true;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // txtNote
            // 
            txtNote.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtNote.Location = new Point(542, 88);
            txtNote.Margin = new Padding(4, 3, 4, 3);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(277, 56);
            txtNote.TabIndex = 9;
            txtNote.UseWaitCursor = true;
            // 
            // txtStaff
            // 
            txtStaff.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtStaff.Location = new Point(119, 103);
            txtStaff.Margin = new Padding(4, 3, 4, 3);
            txtStaff.Name = "txtStaff";
            txtStaff.ReadOnly = true;
            txtStaff.Size = new Size(270, 22);
            txtStaff.TabIndex = 8;
            txtStaff.UseWaitCursor = true;
            // 
            // cboSupplier
            // 
            cboSupplier.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboSupplier.FormattingEnabled = true;
            cboSupplier.Location = new Point(939, 38);
            cboSupplier.Margin = new Padding(4, 3, 4, 3);
            cboSupplier.Name = "cboSupplier";
            cboSupplier.Size = new Size(140, 24);
            cboSupplier.TabIndex = 7;
            cboSupplier.UseWaitCursor = true;
            // 
            // dtpTransactionDate
            // 
            dtpTransactionDate.Font = new Font("Microsoft Sans Serif", 9.75F);
            dtpTransactionDate.Location = new Point(542, 38);
            dtpTransactionDate.Margin = new Padding(4, 3, 4, 3);
            dtpTransactionDate.Name = "dtpTransactionDate";
            dtpTransactionDate.Size = new Size(233, 22);
            dtpTransactionDate.TabIndex = 6;
            dtpTransactionDate.UseWaitCursor = true;
            // 
            // txtTransactionCode
            // 
            txtTransactionCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtTransactionCode.Location = new Point(152, 38);
            txtTransactionCode.Margin = new Padding(4, 3, 4, 3);
            txtTransactionCode.Name = "txtTransactionCode";
            txtTransactionCode.Size = new Size(237, 22);
            txtTransactionCode.TabIndex = 5;
            txtTransactionCode.UseWaitCursor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(472, 110);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(54, 16);
            label6.TabIndex = 4;
            label6.Text = "Ghi chú:";
            label6.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(30, 106);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(70, 16);
            label4.TabIndex = 3;
            label4.Text = "Nhân viên:";
            label4.UseWaitCursor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(826, 43);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 16);
            label3.TabIndex = 2;
            label3.Text = "Nhà cung cấp:";
            label3.UseWaitCursor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(447, 43);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(76, 16);
            label2.TabIndex = 1;
            label2.Text = "Ngày nhập:";
            label2.UseWaitCursor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(30, 43);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(98, 16);
            label1.TabIndex = 0;
            label1.Text = "Mã phiếu nhập:";
            label1.UseWaitCursor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtTotalAmount);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(btnCancel);
            groupBox2.Controls.Add(btnAdd);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(txtTotalPrice);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(nudUnitPrice);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(nudQuantity);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(txtProductName);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(txtUnit);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(txtProductCode);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(cboProduct);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(6, 223);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(1090, 532);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chi tiết nhập hàng";
            groupBox2.UseWaitCursor = true;
            // 
            // txtTotalAmount
            // 
            txtTotalAmount.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotalAmount.Location = new Point(162, 479);
            txtTotalAmount.Margin = new Padding(4, 3, 4, 3);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Size = new Size(268, 38);
            txtTotalAmount.TabIndex = 31;
            txtTotalAmount.UseWaitCursor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(7, 490);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(127, 31);
            label14.TabIndex = 30;
            label14.Text = "Tổng tiền";
            label14.UseWaitCursor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 188);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1090, 284);
            dataGridView1.TabIndex = 29;
            dataGridView1.UseWaitCursor = true;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(317, 137);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(113, 44);
            btnSave.TabIndex = 27;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.UseWaitCursor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(615, 137);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(113, 44);
            btnCancel.TabIndex = 25;
            btnCancel.Text = "Huỷ";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.UseWaitCursor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(183, 137);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(113, 44);
            btnAdd.TabIndex = 24;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.UseWaitCursor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(460, 137);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(113, 44);
            btnDelete.TabIndex = 26;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.UseWaitCursor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // txtTotalPrice
            // 
            txtTotalPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtTotalPrice.Location = new Point(735, 96);
            txtTotalPrice.Margin = new Padding(4, 3, 4, 3);
            txtTotalPrice.Name = "txtTotalPrice";
            txtTotalPrice.ReadOnly = true;
            txtTotalPrice.Size = new Size(208, 22);
            txtTotalPrice.TabIndex = 13;
            txtTotalPrice.UseWaitCursor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 9.75F);
            label13.Location = new Point(644, 103);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(72, 16);
            label13.TabIndex = 12;
            label13.Text = "Thành tiền:";
            label13.UseWaitCursor = true;
            // 
            // nudUnitPrice
            // 
            nudUnitPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudUnitPrice.Location = new Point(398, 96);
            nudUnitPrice.Margin = new Padding(4, 3, 4, 3);
            nudUnitPrice.Name = "nudUnitPrice";
            nudUnitPrice.Size = new Size(140, 22);
            nudUnitPrice.TabIndex = 11;
            nudUnitPrice.UseWaitCursor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F);
            label12.Location = new Point(326, 103);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(56, 16);
            label12.TabIndex = 10;
            label12.Text = "Đơn giá:";
            label12.UseWaitCursor = true;
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudQuantity.Location = new Point(119, 96);
            nudQuantity.Margin = new Padding(4, 3, 4, 3);
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(140, 22);
            nudQuantity.TabIndex = 9;
            nudQuantity.UseWaitCursor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F);
            label11.Location = new Point(29, 96);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(63, 16);
            label11.TabIndex = 8;
            label11.Text = "Số lượng:";
            label11.UseWaitCursor = true;
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtProductName.Location = new Point(623, 42);
            txtProductName.Margin = new Padding(4, 3, 4, 3);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(116, 22);
            txtProductName.TabIndex = 7;
            txtProductName.UseWaitCursor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F);
            label10.Location = new Point(562, 48);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(55, 16);
            label10.TabIndex = 6;
            label10.Text = "Tên SP:";
            label10.UseWaitCursor = true;
            // 
            // txtUnit
            // 
            txtUnit.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtUnit.Location = new Point(883, 42);
            txtUnit.Margin = new Padding(4, 3, 4, 3);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(135, 22);
            txtUnit.TabIndex = 5;
            txtUnit.UseWaitCursor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F);
            label9.Location = new Point(833, 47);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(37, 16);
            label9.TabIndex = 4;
            label9.Text = "ĐVT:";
            label9.UseWaitCursor = true;
            // 
            // txtProductCode
            // 
            txtProductCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtProductCode.Location = new Point(388, 40);
            txtProductCode.Margin = new Padding(4, 3, 4, 3);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.Size = new Size(116, 22);
            txtProductCode.TabIndex = 3;
            txtProductCode.UseWaitCursor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F);
            label8.Location = new Point(332, 45);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(50, 16);
            label8.TabIndex = 2;
            label8.Text = "Mã SP:";
            label8.UseWaitCursor = true;
            // 
            // cboProduct
            // 
            cboProduct.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboProduct.FormattingEnabled = true;
            cboProduct.Location = new Point(119, 35);
            cboProduct.Margin = new Padding(4, 3, 4, 3);
            cboProduct.Name = "cboProduct";
            cboProduct.Size = new Size(140, 24);
            cboProduct.TabIndex = 1;
            cboProduct.UseWaitCursor = true;
            cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F);
            label7.Location = new Point(29, 40);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(71, 16);
            label7.TabIndex = 0;
            label7.Text = "Sản phẩm:";
            label7.UseWaitCursor = true;
            // 
            // frmReceiptTransaction
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 759);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmReceiptTransaction";
            Text = "frmReceiptTransaction";
            Load += frmReceiptTransaction_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.TextBox txtTransactionCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudUnitPrice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}