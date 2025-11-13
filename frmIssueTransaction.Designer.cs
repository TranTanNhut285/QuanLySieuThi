namespace Mini_Mart
{
    partial class frmIssueTransaction
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
            dtpTransactionDate = new DateTimePicker();
            txtTransactionCode = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            groupBox2 = new GroupBox();
            txtChange = new TextBox();
            label15 = new Label();
            txtCustomerPaid = new TextBox();
            label14 = new Label();
            txtTotalAmount = new TextBox();
            label13 = new Label();
            btnPrint = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            dataGridView1 = new DataGridView();
            Masp = new DataGridViewTextBoxColumn();
            Tensp = new DataGridViewTextBoxColumn();
            dvt = new DataGridViewTextBoxColumn();
            Soluong = new DataGridViewTextBoxColumn();
            dongia = new DataGridViewTextBoxColumn();
            Thanhtien = new DataGridViewTextBoxColumn();
            txtTotalPrice = new TextBox();
            label12 = new Label();
            nudUnitPrice = new NumericUpDown();
            label11 = new Label();
            nudQuantity = new NumericUpDown();
            label10 = new Label();
            txtProductName = new TextBox();
            txtUnit = new TextBox();
            txtProductCode = new TextBox();
            label9 = new Label();
            cboProduct = new ComboBox();
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
            label5.Location = new Point(233, 10);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(363, 33);
            label5.TabIndex = 11;
            label5.Text = "Phiếu Xuất Hàng / Bán Lẻ";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(txtStaff);
            groupBox1.Controls.Add(dtpTransactionDate);
            groupBox1.Controls.Add(txtTransactionCode);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(1, 52);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(903, 135);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin phiếu xuất";
            // 
            // txtNote
            // 
            txtNote.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtNote.Location = new Point(167, 83);
            txtNote.Margin = new Padding(4, 3, 4, 3);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(163, 22);
            txtNote.TabIndex = 10;
            // 
            // txtStaff
            // 
            txtStaff.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtStaff.Location = new Point(625, 80);
            txtStaff.Margin = new Padding(4, 3, 4, 3);
            txtStaff.Name = "txtStaff";
            txtStaff.ReadOnly = true;
            txtStaff.Size = new Size(233, 22);
            txtStaff.TabIndex = 9;
            // 
            // dtpTransactionDate
            // 
            dtpTransactionDate.Font = new Font("Microsoft Sans Serif", 9.75F);
            dtpTransactionDate.Location = new Point(625, 35);
            dtpTransactionDate.Margin = new Padding(4, 3, 4, 3);
            dtpTransactionDate.Name = "dtpTransactionDate";
            dtpTransactionDate.Size = new Size(233, 22);
            dtpTransactionDate.TabIndex = 8;
            // 
            // txtTransactionCode
            // 
            txtTransactionCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtTransactionCode.Location = new Point(167, 35);
            txtTransactionCode.Margin = new Padding(4, 3, 4, 3);
            txtTransactionCode.Name = "txtTransactionCode";
            txtTransactionCode.Size = new Size(163, 22);
            txtTransactionCode.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(59, 87);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(54, 16);
            label4.TabIndex = 3;
            label4.Text = "Ghi chú:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(537, 87);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(70, 16);
            label3.TabIndex = 2;
            label3.Text = "Nhân viên:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(537, 39);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 16);
            label2.TabIndex = 1;
            label2.Text = "Ngày xuất:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(58, 39);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 16);
            label1.TabIndex = 0;
            label1.Text = "Mã phiếu xuất:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F);
            label8.Location = new Point(44, 96);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(37, 16);
            label8.TabIndex = 6;
            label8.Text = "ĐVT:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F);
            label7.Location = new Point(299, 38);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(50, 16);
            label7.TabIndex = 5;
            label7.Text = "Mã SP:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(27, 38);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(71, 16);
            label6.TabIndex = 4;
            label6.Text = "Sản phẩm:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtChange);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(txtCustomerPaid);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(txtTotalAmount);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(btnPrint);
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(btnCancel);
            groupBox2.Controls.Add(btnAdd);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(txtTotalPrice);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(nudUnitPrice);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(nudQuantity);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(txtProductName);
            groupBox2.Controls.Add(txtUnit);
            groupBox2.Controls.Add(txtProductCode);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(cboProduct);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(1, 194);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(903, 597);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chi tiết xuất hàng";
            // 
            // txtChange
            // 
            txtChange.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtChange.Location = new Point(757, 479);
            txtChange.Margin = new Padding(4, 3, 4, 3);
            txtChange.Name = "txtChange";
            txtChange.ReadOnly = true;
            txtChange.Size = new Size(138, 29);
            txtChange.TabIndex = 39;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(640, 482);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(94, 24);
            label15.TabIndex = 38;
            label15.Text = "Tiền thừa:";
            // 
            // txtCustomerPaid
            // 
            txtCustomerPaid.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCustomerPaid.Location = new Point(476, 477);
            txtCustomerPaid.Margin = new Padding(4, 3, 4, 3);
            txtCustomerPaid.Name = "txtCustomerPaid";
            txtCustomerPaid.Size = new Size(131, 29);
            txtCustomerPaid.TabIndex = 37;
            txtCustomerPaid.TextChanged += TxtCustomerPaid_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(344, 480);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(107, 24);
            label14.TabIndex = 36;
            label14.Text = "Khách đưa:";
            // 
            // txtTotalAmount
            // 
            txtTotalAmount.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotalAmount.Location = new Point(158, 474);
            txtTotalAmount.Margin = new Padding(4, 3, 4, 3);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Size = new Size(148, 29);
            txtTotalAmount.TabIndex = 35;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(13, 480);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(118, 24);
            label13.TabIndex = 34;
            label13.Text = "TỔNG TIỀN:";
            // 
            // btnPrint
            // 
            btnPrint.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrint.Location = new Point(691, 546);
            btnPrint.Margin = new Padding(4, 3, 4, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(113, 44);
            btnPrint.TabIndex = 33;
            btnPrint.Text = "In";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += BtnPrint_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(259, 546);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(113, 44);
            btnSave.TabIndex = 32;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(556, 546);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(113, 44);
            btnCancel.TabIndex = 30;
            btnCancel.Text = "Huỷ";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(125, 546);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(113, 44);
            btnAdd.TabIndex = 29;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(401, 546);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(113, 44);
            btnDelete.TabIndex = 31;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Masp, Tensp, dvt, Soluong, dongia, Thanhtien });
            dataGridView1.Location = new Point(7, 200);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(863, 253);
            dataGridView1.TabIndex = 16;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Masp
            // 
            Masp.HeaderText = "Mã sản phẩm";
            Masp.Name = "Masp";
            // 
            // Tensp
            // 
            Tensp.HeaderText = "Tên sản phẩm";
            Tensp.Name = "Tensp";
            // 
            // dvt
            // 
            dvt.HeaderText = "Đơn vị";
            dvt.Name = "dvt";
            // 
            // Soluong
            // 
            Soluong.HeaderText = "Số lượng";
            Soluong.Name = "Soluong";
            // 
            // dongia
            // 
            dongia.HeaderText = "Đơn giá";
            dongia.Name = "dongia";
            // 
            // Thanhtien
            // 
            Thanhtien.HeaderText = "Thành tiền";
            Thanhtien.Name = "Thanhtien";
            // 
            // txtTotalPrice
            // 
            txtTotalPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtTotalPrice.Location = new Point(416, 144);
            txtTotalPrice.Margin = new Padding(4, 3, 4, 3);
            txtTotalPrice.Name = "txtTotalPrice";
            txtTotalPrice.ReadOnly = true;
            txtTotalPrice.Size = new Size(216, 22);
            txtTotalPrice.TabIndex = 15;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F);
            label12.Location = new Point(326, 148);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(72, 16);
            label12.TabIndex = 14;
            label12.Text = "Thành tiền:";
            // 
            // nudUnitPrice
            // 
            nudUnitPrice.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudUnitPrice.Location = new Point(638, 89);
            nudUnitPrice.Margin = new Padding(4, 3, 4, 3);
            nudUnitPrice.Name = "nudUnitPrice";
            nudUnitPrice.Size = new Size(232, 22);
            nudUnitPrice.TabIndex = 13;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F);
            label11.Location = new Point(551, 97);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(56, 16);
            label11.TabIndex = 12;
            label11.Text = "Đơn giá:";
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Microsoft Sans Serif", 9.75F);
            nudQuantity.Location = new Point(379, 89);
            nudQuantity.Margin = new Padding(4, 3, 4, 3);
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(140, 22);
            nudQuantity.TabIndex = 11;
            nudQuantity.ValueChanged += CalculateTotalPrice;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F);
            label10.Location = new Point(299, 96);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(63, 16);
            label10.TabIndex = 10;
            label10.Text = "Số lượng:";
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtProductName.Location = new Point(638, 35);
            txtProductName.Margin = new Padding(4, 3, 4, 3);
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(231, 22);
            txtProductName.TabIndex = 9;
            // 
            // txtUnit
            // 
            txtUnit.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtUnit.Location = new Point(117, 89);
            txtUnit.Margin = new Padding(4, 3, 4, 3);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(140, 22);
            txtUnit.TabIndex = 8;
            // 
            // txtProductCode
            // 
            txtProductCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtProductCode.Location = new Point(355, 35);
            txtProductCode.Margin = new Padding(4, 3, 4, 3);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.Size = new Size(116, 22);
            txtProductCode.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F);
            label9.Location = new Point(551, 38);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(55, 16);
            label9.TabIndex = 6;
            label9.Text = "Tên SP:";
            // 
            // cboProduct
            // 
            cboProduct.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboProduct.FormattingEnabled = true;
            cboProduct.Location = new Point(117, 29);
            cboProduct.Margin = new Padding(4, 3, 4, 3);
            cboProduct.Name = "cboProduct";
            cboProduct.Size = new Size(140, 24);
            cboProduct.TabIndex = 5;
            cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChange;
            // 
            // frmIssueTransaction
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 804);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmIssueTransaction";
            Text = "frmIssueTransaction";
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.TextBox txtTransactionCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudUnitPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Masp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tensp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Soluong;
        private System.Windows.Forms.DataGridViewTextBoxColumn dongia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thanhtien;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCustomerPaid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}