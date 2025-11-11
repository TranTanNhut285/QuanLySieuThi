namespace Mini_Mart
{
    partial class Form2
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnExport = new Button();
            btnDecrease = new Button();
            btnIncrease = new Button();
            txtName = new TextBox();
            label2 = new Label();
            panelTonKho = new Panel();
            dtgvTonKho = new DataGridView();
            cboCategory = new ComboBox();
            label3 = new Label();
            tabPage2 = new TabPage();
            panelDT2 = new Panel();
            btnAll_SP = new Button();
            txtDT_SP = new TextBox();
            label5 = new Label();
            dtgvDT = new DataGridView();
            label4 = new Label();
            dtpDT = new DateTimePicker();
            btnExport_DT = new Button();
            btnAll_Loai = new Button();
            panelDT1 = new Panel();
            dtgvDT_CT = new DataGridView();
            cboCategory_DT = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            dtpDT_End = new DateTimePicker();
            dtpDT_Start = new DateTimePicker();
            label6 = new Label();
            tabPage3 = new TabPage();
            btnExport_NX = new Button();
            btnReset = new Button();
            panelNX = new Panel();
            dtgvNX = new DataGridView();
            dtpNX_End = new DateTimePicker();
            dtpNX_Start = new DateTimePicker();
            label14 = new Label();
            label13 = new Label();
            cboLCT = new ComboBox();
            label12 = new Label();
            label1 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvTonKho).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvDT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgvDT_CT).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvNX).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new Font("Microsoft Sans Serif", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(13, 106);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1944, 861);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnExport);
            tabPage1.Controls.Add(btnDecrease);
            tabPage1.Controls.Add(btnIncrease);
            tabPage1.Controls.Add(txtName);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(panelTonKho);
            tabPage1.Controls.Add(dtgvTonKho);
            tabPage1.Controls.Add(cboCategory);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(8, 47);
            tabPage1.Margin = new Padding(3, 4, 3, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 4, 3, 4);
            tabPage1.Size = new Size(1928, 806);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tồn kho";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(1080, 37);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(164, 80);
            btnExport.TabIndex = 12;
            btnExport.Text = "Xuất file Excel";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnDecrease
            // 
            btnDecrease.Location = new Point(668, 84);
            btnDecrease.Name = "btnDecrease";
            btnDecrease.Size = new Size(379, 51);
            btnDecrease.TabIndex = 11;
            btnDecrease.Text = "Sắp xếp giảm dần";
            btnDecrease.UseVisualStyleBackColor = true;
            // 
            // btnIncrease
            // 
            btnIncrease.Location = new Point(668, 16);
            btnIncrease.Name = "btnIncrease";
            btnIncrease.Size = new Size(379, 51);
            btnIncrease.TabIndex = 10;
            btnIncrease.Text = "Sắp xếp tăng dần";
            btnIncrease.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            txtName.Location = new Point(253, 22);
            txtName.Name = "txtName";
            txtName.Size = new Size(320, 40);
            txtName.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 17);
            label2.Name = "label2";
            label2.Size = new Size(209, 33);
            label2.TabIndex = 8;
            label2.Text = "Tên sản phẩm:";
            // 
            // panelTonKho
            // 
            panelTonKho.BackColor = Color.RosyBrown;
            panelTonKho.Location = new Point(1271, 8);
            panelTonKho.Margin = new Padding(3, 4, 3, 4);
            panelTonKho.Name = "panelTonKho";
            panelTonKho.Size = new Size(654, 793);
            panelTonKho.TabIndex = 7;
            // 
            // dtgvTonKho
            // 
            dtgvTonKho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgvTonKho.Location = new Point(16, 154);
            dtgvTonKho.Margin = new Padding(3, 4, 3, 4);
            dtgvTonKho.Name = "dtgvTonKho";
            dtgvTonKho.RowHeadersWidth = 82;
            dtgvTonKho.RowTemplate.Height = 33;
            dtgvTonKho.Size = new Size(1249, 647);
            dtgvTonKho.TabIndex = 4;
            // 
            // cboCategory
            // 
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(252, 76);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(321, 41);
            cboCategory.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 84);
            label3.Name = "label3";
            label3.Size = new Size(214, 33);
            label3.TabIndex = 2;
            label3.Text = "Loại sản phẩm:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panelDT2);
            tabPage2.Controls.Add(btnAll_SP);
            tabPage2.Controls.Add(txtDT_SP);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(dtgvDT);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(dtpDT);
            tabPage2.Controls.Add(btnExport_DT);
            tabPage2.Controls.Add(btnAll_Loai);
            tabPage2.Controls.Add(panelDT1);
            tabPage2.Controls.Add(dtgvDT_CT);
            tabPage2.Controls.Add(cboCategory_DT);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(dtpDT_End);
            tabPage2.Controls.Add(dtpDT_Start);
            tabPage2.Controls.Add(label6);
            tabPage2.Location = new Point(8, 47);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new Size(1928, 806);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Doanh thu";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelDT2
            // 
            panelDT2.BackColor = Color.RosyBrown;
            panelDT2.Location = new Point(921, 22);
            panelDT2.Margin = new Padding(3, 4, 3, 4);
            panelDT2.Name = "panelDT2";
            panelDT2.Size = new Size(491, 805);
            panelDT2.TabIndex = 8;
            // 
            // btnAll_SP
            // 
            btnAll_SP.Location = new Point(652, 480);
            btnAll_SP.Name = "btnAll_SP";
            btnAll_SP.Size = new Size(251, 50);
            btnAll_SP.TabIndex = 17;
            btnAll_SP.Text = "Tất cả sản phẩm";
            btnAll_SP.UseVisualStyleBackColor = true;
            // 
            // txtDT_SP
            // 
            txtDT_SP.Location = new Point(232, 480);
            txtDT_SP.Name = "txtDT_SP";
            txtDT_SP.Size = new Size(344, 40);
            txtDT_SP.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 478);
            label5.Name = "label5";
            label5.Size = new Size(201, 33);
            label5.TabIndex = 15;
            label5.Text = "Tên sản phẩm";
            // 
            // dtgvDT
            // 
            dtgvDT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgvDT.Location = new Point(25, 91);
            dtgvDT.Margin = new Padding(3, 4, 3, 4);
            dtgvDT.Name = "dtgvDT";
            dtgvDT.RowHeadersWidth = 82;
            dtgvDT.RowTemplate.Height = 33;
            dtgvDT.Size = new Size(878, 218);
            dtgvDT.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 32);
            label4.Name = "label4";
            label4.Size = new Size(293, 33);
            label4.TabIndex = 13;
            label4.Text = "Doanh thu trong ngày";
            // 
            // dtpDT
            // 
            dtpDT.Location = new Point(302, 32);
            dtpDT.Margin = new Padding(3, 4, 3, 4);
            dtpDT.Name = "dtpDT";
            dtpDT.Size = new Size(390, 40);
            dtpDT.TabIndex = 12;
            // 
            // btnExport_DT
            // 
            btnExport_DT.Location = new Point(734, 22);
            btnExport_DT.Name = "btnExport_DT";
            btnExport_DT.Size = new Size(169, 52);
            btnExport_DT.TabIndex = 11;
            btnExport_DT.Text = "Xuất Excel";
            btnExport_DT.UseVisualStyleBackColor = true;
            // 
            // btnAll_Loai
            // 
            btnAll_Loai.Location = new Point(729, 333);
            btnAll_Loai.Name = "btnAll_Loai";
            btnAll_Loai.Size = new Size(174, 52);
            btnAll_Loai.TabIndex = 10;
            btnAll_Loai.Text = "Tất cả loại";
            btnAll_Loai.UseVisualStyleBackColor = true;
            // 
            // panelDT1
            // 
            panelDT1.BackColor = Color.RosyBrown;
            panelDT1.Location = new Point(1418, 22);
            panelDT1.Margin = new Padding(3, 4, 3, 4);
            panelDT1.Name = "panelDT1";
            panelDT1.Size = new Size(462, 780);
            panelDT1.TabIndex = 7;
            // 
            // dtgvDT_CT
            // 
            dtgvDT_CT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgvDT_CT.Location = new Point(18, 537);
            dtgvDT_CT.Margin = new Padding(3, 4, 3, 4);
            dtgvDT_CT.Name = "dtgvDT_CT";
            dtgvDT_CT.RowHeadersWidth = 82;
            dtgvDT_CT.RowTemplate.Height = 33;
            dtgvDT_CT.Size = new Size(885, 244);
            dtgvDT_CT.TabIndex = 6;
            // 
            // cboCategory_DT
            // 
            cboCategory_DT.FormattingEnabled = true;
            cboCategory_DT.Location = new Point(324, 333);
            cboCategory_DT.Margin = new Padding(3, 4, 3, 4);
            cboCategory_DT.Name = "cboCategory_DT";
            cboCategory_DT.Size = new Size(300, 41);
            cboCategory_DT.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(18, 338);
            label9.Name = "label9";
            label9.Size = new Size(300, 33);
            label9.TabIndex = 4;
            label9.Text = "Doanh mục sản phẩm";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(528, 413);
            label8.Name = "label8";
            label8.Size = new Size(25, 33);
            label8.TabIndex = 3;
            label8.Text = "-";
            // 
            // dtpDT_End
            // 
            dtpDT_End.Location = new Point(559, 413);
            dtpDT_End.Margin = new Padding(3, 4, 3, 4);
            dtpDT_End.Name = "dtpDT_End";
            dtpDT_End.Size = new Size(344, 40);
            dtpDT_End.TabIndex = 2;
            // 
            // dtpDT_Start
            // 
            dtpDT_Start.Location = new Point(204, 413);
            dtpDT_Start.Margin = new Padding(3, 4, 3, 4);
            dtpDT_Start.Name = "dtpDT_Start";
            dtpDT_Start.Size = new Size(318, 40);
            dtpDT_Start.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 419);
            label6.Name = "label6";
            label6.Size = new Size(180, 33);
            label6.TabIndex = 0;
            label6.Text = "Doanh thu từ";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnExport_NX);
            tabPage3.Controls.Add(btnReset);
            tabPage3.Controls.Add(panelNX);
            tabPage3.Controls.Add(dtgvNX);
            tabPage3.Controls.Add(dtpNX_End);
            tabPage3.Controls.Add(dtpNX_Start);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(label13);
            tabPage3.Controls.Add(cboLCT);
            tabPage3.Controls.Add(label12);
            tabPage3.Location = new Point(8, 47);
            tabPage3.Margin = new Padding(3, 4, 3, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1928, 806);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Nhập/Xuất kho";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnExport_NX
            // 
            btnExport_NX.Location = new Point(715, 35);
            btnExport_NX.Name = "btnExport_NX";
            btnExport_NX.Size = new Size(190, 46);
            btnExport_NX.TabIndex = 16;
            btnExport_NX.Text = "Xuất Excel";
            btnExport_NX.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(1248, 134);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(156, 46);
            btnReset.TabIndex = 15;
            btnReset.Text = "Đặt lại";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // panelNX
            // 
            panelNX.BackColor = Color.RosyBrown;
            panelNX.Location = new Point(1222, 201);
            panelNX.Margin = new Padding(3, 4, 3, 4);
            panelNX.Name = "panelNX";
            panelNX.Size = new Size(680, 588);
            panelNX.TabIndex = 13;
            // 
            // dtgvNX
            // 
            dtgvNX.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgvNX.Location = new Point(3, 185);
            dtgvNX.Margin = new Padding(3, 4, 3, 4);
            dtgvNX.Name = "dtgvNX";
            dtgvNX.RowHeadersWidth = 82;
            dtgvNX.RowTemplate.Height = 33;
            dtgvNX.Size = new Size(1190, 590);
            dtgvNX.TabIndex = 12;
            // 
            // dtpNX_End
            // 
            dtpNX_End.Location = new Point(725, 137);
            dtpNX_End.Margin = new Padding(3, 4, 3, 4);
            dtpNX_End.Name = "dtpNX_End";
            dtpNX_End.Size = new Size(468, 40);
            dtpNX_End.TabIndex = 9;
            // 
            // dtpNX_Start
            // 
            dtpNX_Start.Location = new Point(207, 134);
            dtpNX_Start.Margin = new Padding(3, 4, 3, 4);
            dtpNX_Start.Name = "dtpNX_Start";
            dtpNX_Start.Size = new Size(481, 40);
            dtpNX_Start.TabIndex = 8;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(694, 143);
            label14.Name = "label14";
            label14.Size = new Size(25, 33);
            label14.TabIndex = 8;
            label14.Text = "-";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(21, 134);
            label13.Name = "label13";
            label13.Size = new Size(180, 33);
            label13.TabIndex = 8;
            label13.Text = "Doanh thu từ";
            // 
            // cboLCT
            // 
            cboLCT.FormattingEnabled = true;
            cboLCT.Location = new Point(207, 34);
            cboLCT.Margin = new Padding(3, 4, 3, 4);
            cboLCT.Name = "cboLCT";
            cboLCT.Size = new Size(415, 41);
            cboLCT.TabIndex = 1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(21, 37);
            label12.Name = "label12";
            label12.Size = new Size(197, 33);
            label12.TabIndex = 0;
            label12.Text = "Loại chứng từ:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(876, 46);
            label1.Name = "label1";
            label1.Size = new Size(325, 33);
            label1.TabIndex = 7;
            label1.Text = "BÁO CÁO - THỐNG KÊ";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1950, 993);
            Controls.Add(label1);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Form2";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvTonKho).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvDT).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgvDT_CT).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgvNX).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dtgvTonKho;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTonKho;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panelDT2;
        private System.Windows.Forms.Panel panelDT1;
        private System.Windows.Forms.DataGridView dtgvDT_CT;
        private System.Windows.Forms.ComboBox cboCategory_DT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDT_End;
        private System.Windows.Forms.DateTimePicker dtpDT_Start;
        private System.Windows.Forms.Panel panelNX;
        private System.Windows.Forms.DataGridView dtgvNX;
        private System.Windows.Forms.DateTimePicker dtpNX_End;
        private System.Windows.Forms.DateTimePicker dtpNX_Start;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboLCT;
        private System.Windows.Forms.Label label12;
        private Button btnExport;
        private Button btnDecrease;
        private Button btnIncrease;
        private TextBox txtName;
        private Label label2;
        private DataGridView dtgvDT;
        private Label label4;
        private DateTimePicker dtpDT;
        private Button btnExport_DT;
        private Button btnAll_Loai;
        private TextBox txtDT_SP;
        private Label label5;
        private Button btnAll_SP;
        private Button btnReset;
        private Button btnExport_NX;
    }
}