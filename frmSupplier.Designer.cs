namespace Mini_Mart
{
    partial class frmSupplier
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
            label12 = new Label();
            chkIsActive = new CheckBox();
            txtNote = new TextBox();
            txtBankName = new TextBox();
            txtBankAccount = new TextBox();
            txtTaxCode = new TextBox();
            txtAddress = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            txtContactPerson = new TextBox();
            txtName = new TextBox();
            txtCode = new TextBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnSave = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            dtgv_supplier = new DataGridView();
            txtSearch = new TextBox();
            label13 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgv_supplier).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 20.25F, FontStyle.Bold);
            label5.Location = new Point(331, 10);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(304, 33);
            label5.TabIndex = 10;
            label5.Text = "Quản lý nhà cung cấp";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(chkIsActive);
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(txtBankName);
            groupBox1.Controls.Add(txtBankAccount);
            groupBox1.Controls.Add(txtTaxCode);
            groupBox1.Controls.Add(txtAddress);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(txtPhone);
            groupBox1.Controls.Add(txtContactPerson);
            groupBox1.Controls.Add(txtName);
            groupBox1.Controls.Add(txtCode);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(1, 52);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(1050, 235);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin nhà cung cấp";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F);
            label12.Location = new Point(768, 36);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(70, 16);
            label12.TabIndex = 21;
            label12.Text = "Trạng thái:";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Enabled = false;
            chkIsActive.Font = new Font("Microsoft Sans Serif", 9.75F);
            chkIsActive.Location = new Point(856, 35);
            chkIsActive.Margin = new Padding(4, 3, 4, 3);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(105, 20);
            chkIsActive.TabIndex = 20;
            chkIsActive.Text = "Đang hợp tác";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtNote
            // 
            txtNote.Enabled = false;
            txtNote.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtNote.Location = new Point(464, 190);
            txtNote.Margin = new Padding(4, 3, 4, 3);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(187, 22);
            txtNote.TabIndex = 19;
            // 
            // txtBankName
            // 
            txtBankName.Enabled = false;
            txtBankName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtBankName.Location = new Point(464, 153);
            txtBankName.Margin = new Padding(4, 3, 4, 3);
            txtBankName.Name = "txtBankName";
            txtBankName.Size = new Size(187, 22);
            txtBankName.TabIndex = 18;
            // 
            // txtBankAccount
            // 
            txtBankAccount.Enabled = false;
            txtBankAccount.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtBankAccount.Location = new Point(464, 110);
            txtBankAccount.Margin = new Padding(4, 3, 4, 3);
            txtBankAccount.Name = "txtBankAccount";
            txtBankAccount.Size = new Size(187, 22);
            txtBankAccount.TabIndex = 17;
            // 
            // txtTaxCode
            // 
            txtTaxCode.Enabled = false;
            txtTaxCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtTaxCode.Location = new Point(464, 68);
            txtTaxCode.Margin = new Padding(4, 3, 4, 3);
            txtTaxCode.Name = "txtTaxCode";
            txtTaxCode.Size = new Size(187, 22);
            txtTaxCode.TabIndex = 16;
            // 
            // txtAddress
            // 
            txtAddress.Enabled = false;
            txtAddress.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtAddress.Location = new Point(464, 29);
            txtAddress.Margin = new Padding(4, 3, 4, 3);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(187, 22);
            txtAddress.TabIndex = 15;
            // 
            // txtEmail
            // 
            txtEmail.Enabled = false;
            txtEmail.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtEmail.Location = new Point(117, 190);
            txtEmail.Margin = new Padding(4, 3, 4, 3);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(162, 22);
            txtEmail.TabIndex = 14;
            // 
            // txtPhone
            // 
            txtPhone.Enabled = false;
            txtPhone.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtPhone.Location = new Point(117, 153);
            txtPhone.Margin = new Padding(4, 3, 4, 3);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(162, 22);
            txtPhone.TabIndex = 13;
            // 
            // txtContactPerson
            // 
            txtContactPerson.Enabled = false;
            txtContactPerson.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtContactPerson.Location = new Point(117, 110);
            txtContactPerson.Margin = new Padding(4, 3, 4, 3);
            txtContactPerson.Name = "txtContactPerson";
            txtContactPerson.Size = new Size(162, 22);
            txtContactPerson.TabIndex = 12;
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtName.Location = new Point(117, 72);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(162, 22);
            txtName.TabIndex = 11;
            // 
            // txtCode
            // 
            txtCode.Enabled = false;
            txtCode.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtCode.Location = new Point(117, 29);
            txtCode.Margin = new Padding(4, 3, 4, 3);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(162, 22);
            txtCode.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F);
            label11.Location = new Point(349, 197);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(54, 16);
            label11.TabIndex = 9;
            label11.Text = "Ghi chú:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F);
            label10.Location = new Point(349, 160);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(76, 16);
            label10.TabIndex = 8;
            label10.Text = "Ngân hàng:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F);
            label9.Location = new Point(349, 117);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(93, 16);
            label9.TabIndex = 7;
            label9.Text = "TK ngân hàng:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F);
            label8.Location = new Point(349, 75);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(75, 16);
            label8.TabIndex = 6;
            label8.Text = "Mã số thuế:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F);
            label7.Location = new Point(349, 36);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(50, 16);
            label7.TabIndex = 5;
            label7.Text = "Địa chỉ:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F);
            label6.Location = new Point(7, 197);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(44, 16);
            label6.TabIndex = 4;
            label6.Text = "Email:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9.75F);
            label4.Location = new Point(7, 160);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(88, 16);
            label4.TabIndex = 3;
            label4.Text = "Số điện thoại:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F);
            label3.Location = new Point(7, 117);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(88, 16);
            label3.TabIndex = 2;
            label3.Text = "Người liên hệ:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F);
            label2.Location = new Point(7, 75);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 16);
            label2.TabIndex = 1;
            label2.Text = "Tên NCC:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F);
            label1.Location = new Point(7, 36);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(60, 16);
            label1.TabIndex = 0;
            label1.Text = "Mã NCC:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(btnEdit);
            groupBox2.Controls.Add(btnAdd);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(dtgv_supplier);
            groupBox2.Controls.Add(txtSearch);
            groupBox2.Controls.Add(label13);
            groupBox2.Location = new Point(1, 294);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(1050, 407);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách nhà cung cấp";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(668, 344);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(113, 44);
            btnSave.TabIndex = 19;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.Location = new Point(361, 344);
            btnEdit.Margin = new Padding(4, 3, 4, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(113, 44);
            btnEdit.TabIndex = 17;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(215, 344);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(113, 44);
            btnAdd.TabIndex = 16;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(515, 344);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(113, 44);
            btnDelete.TabIndex = 18;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // dtgv_supplier
            // 
            dtgv_supplier.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgv_supplier.Location = new Point(0, 54);
            dtgv_supplier.Margin = new Padding(4, 3, 4, 3);
            dtgv_supplier.Name = "dtgv_supplier";
            dtgv_supplier.Size = new Size(1043, 272);
            dtgv_supplier.TabIndex = 14;
            dtgv_supplier.CellClick += dtgv_supplier_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9.75F);
            txtSearch.Location = new Point(407, 22);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(277, 22);
            txtSearch.TabIndex = 13;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(324, 29);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(65, 16);
            label13.TabIndex = 12;
            label13.Text = "Tìm kiếm:";
            // 
            // frmSupplier
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1049, 704);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label5);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmSupplier";
            Text = "frmSupplier";
            Load += frmSupplier_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgv_supplier).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtBankAccount;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dtgv_supplier;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}