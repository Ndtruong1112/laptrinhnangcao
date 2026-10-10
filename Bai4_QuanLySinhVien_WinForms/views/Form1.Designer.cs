namespace Bai4_QuanLySinhVien_WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlMain = new Panel();
            pnlDanhSach = new Panel();
            dgvSinhVien = new DataGridView();
            colMaSV = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colNgaySinh = new DataGridViewTextBoxColumn();
            colGioiTinh = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colDienThoai = new DataGridViewTextBoxColumn();
            colDiem = new DataGridViewTextBoxColumn();
            colLop = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
            lblTongSo = new Label();
            lblTitleDanhSach = new Label();
            pnlTimKiem = new Panel();
            btnHienThiTatCa = new Button();
            btnTimKiem = new Button();
            nudDiemTu = new NumericUpDown();
            lblDiemTu = new Label();
            cboLocLop = new ComboBox();
            lblLocLop = new Label();
            txtTimKiem = new TextBox();
            lblTimKiem = new Label();
            pnlThongTin = new Panel();
            btnLamMoi = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            cboTrangThai = new ComboBox();
            lblTrangThai = new Label();
            nudDiem = new NumericUpDown();
            lblDiem = new Label();
            cboLop = new ComboBox();
            lblLop = new Label();
            txtDienThoai = new TextBox();
            lblDienThoai = new Label();
            pnlGioiTinh = new Panel();
            rbNu = new RadioButton();
            rbNam = new RadioButton();
            lblGioiTinh = new Label();
            txtHoTen = new TextBox();
            lblHoTen = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblNgaySinh = new Label();
            txtMaSV = new TextBox();
            lblMaSV = new Label();
            lblBarThongTin = new Label();
            lblTitleThongTin = new Label();
            pnlBottomBar = new Panel();
            lblStatusRight = new Label();
            lblStatusLeft = new Label();
            pnlMain.SuspendLayout();
            pnlDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            pnlTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiemTu).BeginInit();
            pnlThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            pnlGioiTinh.SuspendLayout();
            pnlBottomBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = Color.FromArgb(248, 250, 252);
            pnlMain.Controls.Add(pnlDanhSach);
            pnlMain.Controls.Add(pnlTimKiem);
            pnlMain.Controls.Add(pnlThongTin);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(18, 14, 18, 10);
            pnlMain.Size = new Size(1184, 721);
            pnlMain.TabIndex = 1;
            // 
            // pnlDanhSach
            // 
            pnlDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlDanhSach.BackColor = Color.White;
            pnlDanhSach.BorderStyle = BorderStyle.FixedSingle;
            pnlDanhSach.Controls.Add(dgvSinhVien);
            pnlDanhSach.Controls.Add(lblTongSo);
            pnlDanhSach.Controls.Add(lblTitleDanhSach);
            pnlDanhSach.Location = new Point(18, 277);
            pnlDanhSach.Name = "pnlDanhSach";
            pnlDanhSach.Size = new Size(1148, 430);
            pnlDanhSach.TabIndex = 2;
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSinhVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Padding = new Padding(6, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSinhVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSinhVien.ColumnHeadersHeight = 38;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSinhVien.Columns.AddRange(new DataGridViewColumn[] { colMaSV, colHoTen, colNgaySinh, colGioiTinh, colEmail, colDienThoai, colDiem, colLop, colTrangThai });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Padding = new Padding(6, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvSinhVien.DefaultCellStyle = dataGridViewCellStyle2;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.GridColor = Color.FromArgb(241, 245, 249);
            dgvSinhVien.Location = new Point(16, 42);
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.RowTemplate.Height = 36;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(1114, 345);
            dgvSinhVien.TabIndex = 2;
            // 
            // colMaSV
            // 
            colMaSV.DataPropertyName = "MaSV";
            colMaSV.HeaderText = "Mã SV";
            colMaSV.MinimumWidth = 6;
            colMaSV.Name = "colMaSV";
            colMaSV.ReadOnly = true;
            colMaSV.Width = 125;
            // 
            // colHoTen
            // 
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.HeaderText = "Họ và tên";
            colHoTen.MinimumWidth = 6;
            colHoTen.Name = "colHoTen";
            colHoTen.ReadOnly = true;
            colHoTen.Width = 150;
            // 
            // colNgaySinh
            // 
            colNgaySinh.DataPropertyName = "NgaySinhFormatted";
            colNgaySinh.HeaderText = "Ngày sinh";
            colNgaySinh.MinimumWidth = 6;
            colNgaySinh.Name = "colNgaySinh";
            colNgaySinh.ReadOnly = true;
            colNgaySinh.Width = 105;
            // 
            // colGioiTinh
            // 
            colGioiTinh.DataPropertyName = "GioiTinhFormatted";
            colGioiTinh.HeaderText = "Giới tính";
            colGioiTinh.MinimumWidth = 6;
            colGioiTinh.Name = "colGioiTinh";
            colGioiTinh.ReadOnly = true;
            colGioiTinh.Width = 85;
            // 
            // colEmail
            // 
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "Email";
            colEmail.MinimumWidth = 6;
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            colEmail.Width = 170;
            // 
            // colDienThoai
            // 
            colDienThoai.DataPropertyName = "DienThoai";
            colDienThoai.HeaderText = "Điện thoại";
            colDienThoai.MinimumWidth = 6;
            colDienThoai.Name = "colDienThoai";
            colDienThoai.ReadOnly = true;
            colDienThoai.Width = 115;
            // 
            // colDiem
            // 
            colDiem.DataPropertyName = "DiemFormatted";
            colDiem.HeaderText = "Điểm";
            colDiem.MinimumWidth = 6;
            colDiem.Name = "colDiem";
            colDiem.ReadOnly = true;
            colDiem.Width = 70;
            // 
            // colLop
            // 
            colLop.DataPropertyName = "TenLop";
            colLop.HeaderText = "Lớp";
            colLop.MinimumWidth = 6;
            colLop.Name = "colLop";
            colLop.ReadOnly = true;
            colLop.Width = 170;
            // 
            // colTrangThai
            // 
            colTrangThai.DataPropertyName = "TrangThai";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.MinimumWidth = 6;
            colTrangThai.Name = "colTrangThai";
            colTrangThai.ReadOnly = true;
            colTrangThai.Width = 110;
            // 
            // lblTongSo
            // 
            lblTongSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongSo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTongSo.ForeColor = Color.FromArgb(30, 41, 59);
            lblTongSo.Location = new Point(890, 12);
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Size = new Size(240, 20);
            lblTongSo.TabIndex = 1;
            lblTongSo.Text = "Tổng số: 4 sinh viên";
            lblTongSo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitleDanhSach
            // 
            lblTitleDanhSach.AutoSize = true;
            lblTitleDanhSach.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitleDanhSach.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitleDanhSach.Location = new Point(16, 12);
            lblTitleDanhSach.Name = "lblTitleDanhSach";
            lblTitleDanhSach.Size = new Size(185, 25);
            lblTitleDanhSach.TabIndex = 0;
            lblTitleDanhSach.Text = "Danh sách sinh viên";
            // 
            // pnlTimKiem
            // 
            pnlTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTimKiem.BackColor = Color.White;
            pnlTimKiem.BorderStyle = BorderStyle.FixedSingle;
            pnlTimKiem.Controls.Add(btnHienThiTatCa);
            pnlTimKiem.Controls.Add(btnTimKiem);
            pnlTimKiem.Controls.Add(nudDiemTu);
            pnlTimKiem.Controls.Add(lblDiemTu);
            pnlTimKiem.Controls.Add(cboLocLop);
            pnlTimKiem.Controls.Add(lblLocLop);
            pnlTimKiem.Controls.Add(txtTimKiem);
            pnlTimKiem.Controls.Add(lblTimKiem);
            pnlTimKiem.Location = new Point(18, 215);
            pnlTimKiem.Name = "pnlTimKiem";
            pnlTimKiem.Size = new Size(1148, 52);
            pnlTimKiem.TabIndex = 1;
            // 
            // btnHienThiTatCa
            // 
            btnHienThiTatCa.BackColor = Color.FromArgb(241, 245, 249);
            btnHienThiTatCa.Cursor = Cursors.Hand;
            btnHienThiTatCa.FlatStyle = FlatStyle.Flat;
            btnHienThiTatCa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnHienThiTatCa.ForeColor = Color.FromArgb(30, 41, 59);
            btnHienThiTatCa.Location = new Point(890, 9);
            btnHienThiTatCa.Name = "btnHienThiTatCa";
            btnHienThiTatCa.Size = new Size(130, 32);
            btnHienThiTatCa.TabIndex = 18;
            btnHienThiTatCa.Text = "Hiển thị tất cả";
            btnHienThiTatCa.UseVisualStyleBackColor = false;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(29, 78, 216);
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.Location = new Point(775, 9);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(105, 32);
            btnTimKiem.TabIndex = 17;
            btnTimKiem.Text = "⌕ Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // nudDiemTu
            // 
            nudDiemTu.DecimalPlaces = 1;
            nudDiemTu.Font = new Font("Segoe UI", 9.5F);
            nudDiemTu.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudDiemTu.Location = new Point(670, 13);
            nudDiemTu.Maximum = new decimal(new int[] { 100, 0, 0, 65536 });
            nudDiemTu.Name = "nudDiemTu";
            nudDiemTu.Size = new Size(80, 29);
            nudDiemTu.TabIndex = 16;
            // 
            // lblDiemTu
            // 
            lblDiemTu.AutoSize = true;
            lblDiemTu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDiemTu.ForeColor = Color.FromArgb(30, 41, 59);
            lblDiemTu.Location = new Point(605, 16);
            lblDiemTu.Name = "lblDiemTu";
            lblDiemTu.Size = new Size(72, 21);
            lblDiemTu.TabIndex = 4;
            lblDiemTu.Text = "Điểm từ";
            // 
            // cboLocLop
            // 
            cboLocLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLop.Font = new Font("Segoe UI", 9.5F);
            cboLocLop.FormattingEnabled = true;
            cboLocLop.Location = new Point(400, 13);
            cboLocLop.Name = "cboLocLop";
            cboLocLop.Size = new Size(180, 29);
            cboLocLop.TabIndex = 15;
            // 
            // lblLocLop
            // 
            lblLocLop.AutoSize = true;
            lblLocLop.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLocLop.ForeColor = Color.FromArgb(30, 41, 59);
            lblLocLop.Location = new Point(360, 16);
            lblLocLop.Name = "lblLocLop";
            lblLocLop.Size = new Size(38, 21);
            lblLocLop.TabIndex = 2;
            lblLocLop.Text = "Lớp";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            txtTimKiem.Location = new Point(82, 13);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
            txtTimKiem.Size = new Size(250, 29);
            txtTimKiem.TabIndex = 14;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimKiem.ForeColor = Color.FromArgb(30, 41, 59);
            lblTimKiem.Location = new Point(16, 16);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(72, 21);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Từ khóa";
            // 
            // pnlThongTin
            // 
            pnlThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;
            pnlThongTin.Controls.Add(btnLamMoi);
            pnlThongTin.Controls.Add(btnXoa);
            pnlThongTin.Controls.Add(btnSua);
            pnlThongTin.Controls.Add(btnThem);
            pnlThongTin.Controls.Add(cboTrangThai);
            pnlThongTin.Controls.Add(lblTrangThai);
            pnlThongTin.Controls.Add(nudDiem);
            pnlThongTin.Controls.Add(lblDiem);
            pnlThongTin.Controls.Add(cboLop);
            pnlThongTin.Controls.Add(lblLop);
            pnlThongTin.Controls.Add(txtDienThoai);
            pnlThongTin.Controls.Add(lblDienThoai);
            pnlThongTin.Controls.Add(pnlGioiTinh);
            pnlThongTin.Controls.Add(lblGioiTinh);
            pnlThongTin.Controls.Add(txtHoTen);
            pnlThongTin.Controls.Add(lblHoTen);
            pnlThongTin.Controls.Add(txtEmail);
            pnlThongTin.Controls.Add(lblEmail);
            pnlThongTin.Controls.Add(dtpNgaySinh);
            pnlThongTin.Controls.Add(lblNgaySinh);
            pnlThongTin.Controls.Add(txtMaSV);
            pnlThongTin.Controls.Add(lblMaSV);
            pnlThongTin.Controls.Add(lblBarThongTin);
            pnlThongTin.Controls.Add(lblTitleThongTin);
            pnlThongTin.Location = new Point(18, 10);
            pnlThongTin.Name = "pnlThongTin";
            pnlThongTin.Size = new Size(1148, 195);
            pnlThongTin.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Location = new Point(1005, 148);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 34);
            btnLamMoi.TabIndex = 13;
            btnLamMoi.Text = "↻ Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.FromArgb(220, 38, 38);
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(900, 148);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(95, 34);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.FromArgb(37, 99, 235);
            btnSua.Cursor = Cursors.Hand;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSua.ForeColor = Color.White;
            btnSua.Location = new Point(795, 148);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(95, 34);
            btnSua.TabIndex = 11;
            btnSua.Text = "✎ Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.FromArgb(22, 163, 74);
            btnThem.Cursor = Cursors.Hand;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(690, 148);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(95, 34);
            btnThem.TabIndex = 10;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9.5F);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Items.AddRange(new object[] { "Đang học", "Bảo lưu", "Đã tốt nghiệp" });
            cboTrangThai.Location = new Point(795, 108);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(210, 29);
            cboTrangThai.TabIndex = 9;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
            lblTrangThai.Location = new Point(710, 111);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(87, 21);
            lblTrangThai.TabIndex = 18;
            lblTrangThai.Text = "Trạng thái";
            // 
            // nudDiem
            // 
            nudDiem.DecimalPlaces = 1;
            nudDiem.Font = new Font("Segoe UI", 9.5F);
            nudDiem.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudDiem.Location = new Point(795, 75);
            nudDiem.Maximum = new decimal(new int[] { 100, 0, 0, 65536 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(210, 29);
            nudDiem.TabIndex = 8;
            nudDiem.Value = new decimal(new int[] { 85, 0, 0, 65536 });
            nudDiem.ValueChanged += nudDiem_ValueChanged;
            // 
            // lblDiem
            // 
            lblDiem.AutoSize = true;
            lblDiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDiem.ForeColor = Color.FromArgb(30, 41, 59);
            lblDiem.Location = new Point(710, 78);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(62, 21);
            lblDiem.TabIndex = 16;
            lblDiem.Text = "Điểm *";
            // 
            // cboLop
            // 
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Font = new Font("Segoe UI", 9.5F);
            cboLop.FormattingEnabled = true;
            cboLop.Location = new Point(795, 42);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(210, 29);
            cboLop.TabIndex = 7;
            // 
            // lblLop
            // 
            lblLop.AutoSize = true;
            lblLop.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLop.ForeColor = Color.FromArgb(30, 41, 59);
            lblLop.Location = new Point(710, 45);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(81, 21);
            lblLop.TabIndex = 14;
            lblLop.Text = "Lớp học *";
            // 
            // txtDienThoai
            // 
            txtDienThoai.Font = new Font("Segoe UI", 9.5F);
            txtDienThoai.Location = new Point(460, 108);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(210, 29);
            txtDienThoai.TabIndex = 6;
            // 
            // lblDienThoai
            // 
            lblDienThoai.AutoSize = true;
            lblDienThoai.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDienThoai.ForeColor = Color.FromArgb(30, 41, 59);
            lblDienThoai.Location = new Point(370, 111);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(101, 21);
            lblDienThoai.TabIndex = 12;
            lblDienThoai.Text = "Điện thoại *";
            // 
            // pnlGioiTinh
            // 
            pnlGioiTinh.Controls.Add(rbNu);
            pnlGioiTinh.Controls.Add(rbNam);
            pnlGioiTinh.Location = new Point(460, 73);
            pnlGioiTinh.Name = "pnlGioiTinh";
            pnlGioiTinh.Size = new Size(210, 28);
            pnlGioiTinh.TabIndex = 4;
            // 
            // rbNu
            // 
            rbNu.AutoSize = true;
            rbNu.Font = new Font("Segoe UI", 9.5F);
            rbNu.Location = new Point(90, 4);
            rbNu.Name = "rbNu";
            rbNu.Size = new Size(52, 25);
            rbNu.TabIndex = 5;
            rbNu.Text = "Nữ";
            rbNu.UseVisualStyleBackColor = true;
            // 
            // rbNam
            // 
            rbNam.AutoSize = true;
            rbNam.Checked = true;
            rbNam.Font = new Font("Segoe UI", 9.5F);
            rbNam.Location = new Point(10, 4);
            rbNam.Name = "rbNam";
            rbNam.Size = new Size(65, 25);
            rbNam.TabIndex = 4;
            rbNam.TabStop = true;
            rbNam.Text = "Nam";
            rbNam.UseVisualStyleBackColor = true;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblGioiTinh.ForeColor = Color.FromArgb(30, 41, 59);
            lblGioiTinh.Location = new Point(370, 78);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(76, 21);
            lblGioiTinh.TabIndex = 10;
            lblGioiTinh.Text = "Giới tính";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 9.5F);
            txtHoTen.Location = new Point(460, 42);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(210, 29);
            txtHoTen.TabIndex = 3;
            txtHoTen.TextChanged += txtHoTen_TextChanged;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHoTen.ForeColor = Color.FromArgb(30, 41, 59);
            lblHoTen.Location = new Point(370, 45);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(94, 21);
            lblHoTen.TabIndex = 8;
            lblHoTen.Text = "Họ và tên *";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9.5F);
            txtEmail.Location = new Point(120, 108);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(210, 29);
            txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(30, 41, 59);
            lblEmail.Location = new Point(16, 111);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(64, 21);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email *";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Font = new Font("Segoe UI", 9.5F);
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(120, 75);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(210, 29);
            dtpNgaySinh.TabIndex = 1;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNgaySinh.ForeColor = Color.FromArgb(30, 41, 59);
            lblNgaySinh.Location = new Point(16, 78);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(87, 21);
            lblNgaySinh.TabIndex = 4;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // txtMaSV
            // 
            txtMaSV.Font = new Font("Segoe UI", 9.5F);
            txtMaSV.Location = new Point(120, 42);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(210, 29);
            txtMaSV.TabIndex = 0;
            // 
            // lblMaSV
            // 
            lblMaSV.AutoSize = true;
            lblMaSV.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMaSV.ForeColor = Color.FromArgb(30, 41, 59);
            lblMaSV.Location = new Point(16, 45);
            lblMaSV.Name = "lblMaSV";
            lblMaSV.Size = new Size(118, 21);
            lblMaSV.TabIndex = 2;
            lblMaSV.Text = "Mã sinh viên *";
            // 
            // lblBarThongTin
            // 
            lblBarThongTin.BackColor = Color.FromArgb(249, 115, 22);
            lblBarThongTin.Location = new Point(16, 13);
            lblBarThongTin.Name = "lblBarThongTin";
            lblBarThongTin.Size = new Size(4, 18);
            lblBarThongTin.TabIndex = 0;
            // 
            // lblTitleThongTin
            // 
            lblTitleThongTin.AutoSize = true;
            lblTitleThongTin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitleThongTin.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitleThongTin.Location = new Point(26, 12);
            lblTitleThongTin.Name = "lblTitleThongTin";
            lblTitleThongTin.Size = new Size(182, 25);
            lblTitleThongTin.TabIndex = 1;
            lblTitleThongTin.Text = "Thông tin sinh viên";
            // 
            // pnlBottomBar
            // 
            pnlBottomBar.BackColor = Color.FromArgb(241, 245, 249);
            pnlBottomBar.Controls.Add(lblStatusRight);
            pnlBottomBar.Controls.Add(lblStatusLeft);
            pnlBottomBar.Dock = DockStyle.Bottom;
            pnlBottomBar.Location = new Point(0, 721);
            pnlBottomBar.Name = "pnlBottomBar";
            pnlBottomBar.Size = new Size(1184, 28);
            pnlBottomBar.TabIndex = 2;
            // 
            // lblStatusRight
            // 
            lblStatusRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatusRight.Font = new Font("Segoe UI", 8.5F);
            lblStatusRight.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusRight.Location = new Point(800, 6);
            lblStatusRight.Name = "lblStatusRight";
            lblStatusRight.Size = new Size(365, 15);
            lblStatusRight.TabIndex = 1;
            lblStatusRight.Text = "Thêm • Sửa • Xóa • Tìm kiếm • Lọc theo lớp";
            lblStatusRight.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStatusLeft
            // 
            lblStatusLeft.AutoSize = true;
            lblStatusLeft.Font = new Font("Segoe UI", 8.5F);
            lblStatusLeft.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusLeft.Location = new Point(18, 6);
            lblStatusLeft.Name = "lblStatusLeft";
            lblStatusLeft.Size = new Size(406, 20);
            lblStatusLeft.TabIndex = 0;
            lblStatusLeft.Text = "Bài tập: xây dựng Windows Forms quản lý sinh viên theo lớp";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1184, 749);
            Controls.Add(pnlMain);
            Controls.Add(pnlBottomBar);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1100, 680);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ứng dụng quản lý sinh viên";
            pnlMain.ResumeLayout(false);
            pnlDanhSach.ResumeLayout(false);
            pnlDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            pnlTimKiem.ResumeLayout(false);
            pnlTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiemTu).EndInit();
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
            pnlGioiTinh.ResumeLayout(false);
            pnlGioiTinh.PerformLayout();
            pnlBottomBar.ResumeLayout(false);
            pnlBottomBar.PerformLayout();
            ResumeLayout(false);
        }
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblBarThongTin;
        private System.Windows.Forms.Label lblTitleThongTin;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.Panel pnlGioiTinh;
        private System.Windows.Forms.RadioButton rbNu;
        private System.Windows.Forms.RadioButton rbNam;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.NumericUpDown nudDiem;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblLocLop;
        private System.Windows.Forms.ComboBox cboLocLop;
        private System.Windows.Forms.Label lblDiemTu;
        private System.Windows.Forms.NumericUpDown nudDiemTu;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnHienThiTatCa;
        private System.Windows.Forms.Panel pnlDanhSach;
        private System.Windows.Forms.Label lblTitleDanhSach;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.Label lblStatusLeft;
        private System.Windows.Forms.Label lblStatusRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
    }
}
