namespace Mini_Mart
{
    partial class frmTransactionHistory
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
            txtSearch = new TextBox();
            label6 = new Label();
            cboStatus = new ComboBox();
            label4 = new Label();
            cboTransactionType = new ComboBox();
            label3 = new Label();
            dtpToDate = new DateTimePicker();
            label2 = new Label();
            dtpFromDate = new DateTimePicker();
            label1 = new Label();
            dgvTransactions = new DataGridView();
            MaPhieu = new DataGridViewTextBoxColumn();
            Loai = new DataGridViewTextBoxColumn();
            Ngay = new DataGridViewTextBoxColumn();
            NCC = new DataGridViewTextBoxColumn();
            Nhanvien = new DataGridViewTextBoxColumn();
            Tongtien = new DataGridViewTextBoxColumn();
            Trangthai = new DataGridViewTextBoxColumn();
            btnPrint = new Button();
            btnRefresh = new Button();
            btnView = new Button();
            btnExport = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 20.25F, FontStyle.Bold);
            label5.Location = new Point(357, 10);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(304, 33);
            label5.TabIndex = 12;
            label5.Text = "Lịch sử Giao dịch Kho";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtSearch);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cboStatus);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cboTransactionType);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(dtpToDate);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dtpFromDate);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 52);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(1086, 208);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bộ lọc tìm kiếm";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtSearch.Location = new Point(482, 162);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(243, 22);
            txtSearch.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(399, 165);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(65, 16);
            label6.TabIndex = 8;
            label6.Text = "Tìm kiếm:";
            // 
            // cboStatus
            // 
            cboStatus.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(620, 95);
            cboStatus.Margin = new Padding(4, 3, 4, 3);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(140, 24);
            cboStatus.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(531, 98);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(70, 16);
            label4.TabIndex = 6;
            label4.Text = "Trạng thái:";
            // 
            // cboTransactionType
            // 
            cboTransactionType.Font = new Font("Microsoft Sans Serif", 9.75F);
            cboTransactionType.FormattingEnabled = true;
            cboTransactionType.Location = new Point(164, 95);
            cboTransactionType.Margin = new Padding(4, 3, 4, 3);
            cboTransactionType.Name = "cboTransactionType";
            cboTransactionType.Size = new Size(140, 24);
            cboTransactionType.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(89, 99);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(59, 16);
            label3.TabIndex = 4;
            label3.Text = "Loại GD:";
            // 
            // dtpToDate
            // 
            dtpToDate.Font = new Font("Microsoft Sans Serif", 9.75F);
            dtpToDate.Location = new Point(623, 32);
            dtpToDate.Margin = new Padding(4, 3, 4, 3);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(233, 22);
            dtpToDate.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(538, 38);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(67, 16);
            label2.TabIndex = 2;
            label2.Text = "Đến ngày:";
            // 
            // dtpFromDate
            // 
            dtpFromDate.Font = new Font("Microsoft Sans Serif", 9.75F);
            dtpFromDate.Location = new Point(139, 32);
            dtpFromDate.Margin = new Padding(4, 3, 4, 3);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(233, 22);
            dtpFromDate.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(74, 37);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 16);
            label1.TabIndex = 0;
            label1.Text = "Từ ngày:";
            // 
            // dgvTransactions
            // 
            dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransactions.Columns.AddRange(new DataGridViewColumn[] { MaPhieu, Loai, Ngay, NCC, Nhanvien, Tongtien, Trangthai });
            dgvTransactions.Location = new Point(18, 286);
            dgvTransactions.Margin = new Padding(4, 3, 4, 3);
            dgvTransactions.Name = "dgvTransactions";
            dgvTransactions.Size = new Size(1069, 230);
            dgvTransactions.TabIndex = 14;
            dgvTransactions.CellDoubleClick += DgvTransactions_CellDoubleClick;
            // 
            // MaPhieu
            // 
            MaPhieu.HeaderText = "Mã phiếu";
            MaPhieu.Name = "MaPhieu";
            // 
            // Loai
            // 
            Loai.HeaderText = "Loại";
            Loai.Name = "Loai";
            // 
            // Ngay
            // 
            Ngay.HeaderText = "Ngày";
            Ngay.Name = "Ngay";
            // 
            // NCC
            // 
            NCC.HeaderText = "Nhà cung cấp";
            NCC.Name = "NCC";
            // 
            // Nhanvien
            // 
            Nhanvien.HeaderText = "Nhân viên";
            Nhanvien.Name = "Nhanvien";
            // 
            // Tongtien
            // 
            Tongtien.HeaderText = "Tổng tiền";
            Tongtien.Name = "Tongtien";
            // 
            // Trangthai
            // 
            Trangthai.HeaderText = "Trạng thái";
            Trangthai.Name = "Trangthai";
            // 
            // btnPrint
            // 
            btnPrint.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrint.Location = new Point(369, 540);
            btnPrint.Margin = new Padding(4, 3, 4, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(113, 44);
            btnPrint.TabIndex = 36;
            btnPrint.Text = "In phiếu";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(666, 540);
            btnRefresh.Margin = new Padding(4, 3, 4, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(113, 44);
            btnRefresh.TabIndex = 34;
            btnRefresh.Text = "Làm lại";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnView
            // 
            btnView.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnView.Location = new Point(235, 540);
            btnView.Margin = new Padding(4, 3, 4, 3);
            btnView.Name = "btnView";
            btnView.Size = new Size(113, 44);
            btnView.TabIndex = 33;
            btnView.Text = "Xem chi tiết";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += BtnViewDetail_Click;
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(526, 540);
            btnExport.Margin = new Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(113, 44);
            btnExport.TabIndex = 35;
            btnExport.Text = "Xuất Excel";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // frmTransactionHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 616);
            Controls.Add(btnPrint);
            Controls.Add(btnRefresh);
            Controls.Add(btnView);
            Controls.Add(btnExport);
            Controls.Add(dgvTransactions);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmTransactionHistory";
            Text = "frmTransactionHistory";
            Load += frmTransactionHistory_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTransactionType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loai;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn NCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nhanvien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tongtien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trangthai;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnExport;
    }
}