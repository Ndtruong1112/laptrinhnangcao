using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using QuanLySinhVien.BUL;
using QuanLySinhVien.Data.Entity;

namespace Bai4_QuanLySinhVien_WinForms
{
    /// <summary>
    /// Tầng Presentation Layer (GUI): Chỉ giữ lại phần xử lý về giao diện,
    /// tiếp nhận tương tác người dùng và gọi xuống tầng Business (BUL: SinhVienBUL, LopBus).
    /// </summary>
    public partial class Form1 : Form
    {
        // Giao diện làm việc trực tiếp với tầng Business (BUL) theo mẫu của giáo viên
        private readonly SinhVienBUL svBUL = new();
        private readonly LopBus lopBus = new();
        private List<SinhVien> danhSachSinhVien = new();

        // ErrorProvider theo mẫu của cô giáo (errPMaSV, erpHoten, erpEmail, erpDienThoai)
        private readonly ErrorProvider errPMaSV = new();
        private readonly ErrorProvider erpHoten = new();
        private readonly ErrorProvider erpEmail = new();
        private readonly ErrorProvider erpDienThoai = new();
        private readonly ErrorProvider errorProvider = new();

        // Biến cờ ngăn sự kiện chọn dòng đè lên khi đang thao tác nhập
        private bool isBindingData = false;

        public Form1()
        {
            InitializeComponent();
            SetupCustomEvents();
        }

        private void SetupCustomEvents()
        {
            this.Load += Form1_Load;

            // Cấu hình ErrorProvider
            errPMaSV.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            erpHoten.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            erpEmail.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            erpDienThoai.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // Xóa lỗi ngay khi người dùng bắt đầu nhập lại vào ô tương ứng
            this.txtMaSV.TextChanged += (s, e) => errPMaSV.SetError(txtMaSV, "");
            this.txtHoTen.TextChanged += (s, e) => erpHoten.SetError(txtHoTen, "");
            this.txtEmail.TextChanged += (s, e) => erpEmail.SetError(txtEmail, "");
            this.txtDienThoai.TextChanged += (s, e) => erpDienThoai.SetError(txtDienThoai, "");
            this.nudDiem.ValueChanged += (s, e) => errorProvider.SetError(nudDiem, "");
            this.cboLop.SelectedIndexChanged += (s, e) => errorProvider.SetError(cboLop, "");

            // Xử lý sự kiện nhập Mã sinh viên
            this.txtMaSV.TextChanged += TxtMaSV_TextChanged;
            this.txtMaSV.Leave += TxtMaSV_Leave;

            // Các nút thao tác
            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnLamMoi.Click += BtnLamMoi_Click;

            // Tìm kiếm và lọc
            this.btnTimKiem.Click += BtnTimKiem_Click;
            this.btnHienThiTatCa.Click += BtnHienThiTatCa_Click;
            this.cboLocLop.SelectedIndexChanged += CboLocLop_SelectedIndexChanged;

            // Enter tại ô tìm kiếm để lọc nhanh
            this.txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BtnTimKiem_Click(s, e);
                }
            };

            // Click chọn dòng trên DataGridView
            this.dgvSinhVien.CellClick += DgvSinhVien_CellClick;

            // Vẽ badge trạng thái đẹp mắt
            this.dgvSinhVien.CellPainting += DgvSinhVien_CellPainting;
        }

        #region --- 1. SỰ KIỆN FORM LOAD ---

        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                // 1. Nạp danh sách lớp học thông qua tầng Business (LopBus -> LopHocDAO / ADO.NET)
                var dsLop = lopBus.GetAllLopHoc();

                // 2. Nạp dữ liệu vào ComboBox Lớp học
                cboLop.DataSource = dsLop.ToList();
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // 3. Nạp dữ liệu vào ComboBox Lọc lớp
                var dsLocLop = new List<LopItem> { new LopItem { MaLop = "", TenLop = "Tất cả lớp" } };
                dsLocLop.AddRange(dsLop.Select(l => new LopItem { MaLop = l.MaLop, TenLop = l.TenLop }));
                cboLocLop.DataSource = dsLocLop;
                cboLocLop.DisplayMember = "TenLop";
                cboLocLop.ValueMember = "MaLop";
                cboLocLop.SelectedIndex = 0;

                // 4. Mặc định trạng thái
                cboTrangThai.SelectedIndex = 0;

                // 5. Nạp danh sách sinh viên lên DataGridView thông qua SinhVienBUL (ADO.NET)
                TaiLaiDanhSachSinhVien();
                HienThiDanhSachLenGrid(danhSachSinhVien);
            }
            catch (Exception ex)
            {
                // Tầng View xử lý ngoại lệ theo đúng yêu cầu đề bài
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu khi khởi động:\n{ex.Message}\n\n(Lưu ý: Vui lòng chạy file Database_QLSV.sql trong SQL Server để tạo cơ sở dữ liệu QLSV)", 
                    "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 6. Thiết lập trạng thái các button theo yêu cầu đề bài:
            // Enable Thêm, Disable Sửa, Xóa, Enable Làm mới
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLamMoi.Enabled = true;

            // 7. Con trỏ thiết lập mặc định ở txtMaSV
            this.ActiveControl = txtMaSV;
            txtMaSV.Focus();
        }

        #endregion

        #region --- 2. XỬ LÝ NHẬP MÃ SINH VIÊN (KIỂM TRA TỒN TẠI) ---

        private void TxtMaSV_TextChanged(object? sender, EventArgs e)
        {
            if (isBindingData) return;
            KiemTraMaSinhVien();
        }

        private void TxtMaSV_Leave(object? sender, EventArgs e)
        {
            if (isBindingData) return;
            KiemTraMaSinhVien();
        }

        private void KiemTraMaSinhVien()
        {
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                return;
            }

            // Tìm kiếm sinh viên theo mã (từ danh sách đang hiển thị hoặc truy vấn CSDL)
            var sv = danhSachSinhVien.FirstOrDefault(s =>
                s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                try
                {
                    sv = svBUL.GetSinhVienById(maSV);
                }
                catch
                {
                    // Bỏ qua lỗi kết nối khi người dùng đang gõ dở
                }
            }

            if (sv != null)
            {
                // YÊU CẦU: Nếu mã tồn tại hiển thị thông tin của sinh viên vào các điều khiển
                HienThiThongTinLenControls(sv, giuNguyenMaSV: true);
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                // YÊU CẦU: Còn không tồn tại thì xóa trống
                XoaTrangCacDieuKhienConLai();
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        #endregion

        #region --- 3. CÁC NÚT THAO TÁC (THÊM, SỬA, XÓA, LÀM MỚI VỚI ERRORPROVIDER) ---

        // THÊM SINH VIÊN (DÙNG ERRORPROVIDER BÁO LỖI THEO MẪU CÔ GIÁO)
        private void BtnThem_Click(object? sender, EventArgs e)
        {
            XoaTatCaLoi();

            // 1. Lấy dữ liệu từ giao diện
            string maLop = cboLop.SelectedValue?.ToString() ?? "";
            var sv = new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rbNam.Checked,
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtDienThoai.Text.Trim(),
                Diem = (double)nudDiem.Value,
                MaLop = maLop,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang học"
            };

            // 2. YÊU CẦU: Kiểm tra thông tin, nếu sai trường nào dùng ErrorProvider để báo lỗi
            var errors = sv.IsInValid();
            if (errors.Count > 0)
            {
                HienThiLoiErrorProvider(errors);
                return;
            }

            // Kiểm tra trùng mã
            if (svBUL.GetSinhVienById(sv.MaSV) != null)
            {
                errPMaSV.SetError(txtMaSV, "Mã sinh viên đã tồn tại trong hệ thống!");
                txtMaSV.Focus();
                return;
            }

            // 3. Thông tin hợp lệ -> gọi tầng Business (BUL) thực hiện
            try
            {
                svBUL.AddSinhVien(sv);
                TaiLaiDanhSachSinhVien();
                LocVaHienThiSinhVien();
                MessageBox.Show("Thêm mới sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KiemTraMaSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // SỬA SINH VIÊN (DÙNG ERRORPROVIDER BÁO LỖI)
        private void BtnSua_Click(object? sender, EventArgs e)
        {
            XoaTatCaLoi();

            // 1. Lấy dữ liệu từ giao diện
            string maLopMoi = cboLop.SelectedValue?.ToString() ?? "";
            var sv = new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rbNam.Checked,
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtDienThoai.Text.Trim(),
                Diem = (double)nudDiem.Value,
                MaLop = maLopMoi,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang học"
            };

            // 2. YÊU CẦU: Kiểm tra thông tin, nếu sai trường nào dùng ErrorProvider để báo lỗi
            var errors = sv.IsInValid();
            if (errors.Count > 0)
            {
                HienThiLoiErrorProvider(errors);
                return;
            }

            // 3. Thông tin hợp lệ -> gọi tầng Business (BUL) thực hiện
            try
            {
                svBUL.UpdateSinhVien(sv);
                TaiLaiDanhSachSinhVien();
                LocVaHienThiSinhVien();
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // XÓA SINH VIÊN (CHỨC NĂNG NGUY HIỂM -> XÁC THỰC TRƯỚC KHI THỰC HIỆN)
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            XoaTatCaLoi();

            string maSV = txtMaSV.Text.Trim();
            var sv = svBUL.GetSinhVienById(maSV);
            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // YÊU CẦU ĐỀ BÀI: Khi người dùng thực hiện các chức năng nguy hiểm cần xác thực trước khi thực hiện
            var confirmResult = MessageBox.Show(
                $"CẢNH BÁO HÀNH ĐỘNG NGUY HIỂM!\n\nBạn có chắc chắn muốn xóa sinh viên sau khỏi hệ thống?\n- Mã SV: {sv.MaSV}\n- Họ và tên: {sv.HoTen}\n\nHành động này không thể hoàn tác!",
                "Xác nhận xóa sinh viên",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    svBUL.DeleteSinhVien(maSV);
                    TaiLaiDanhSachSinhVien();
                    LocVaHienThiSinhVien();
                    BtnLamMoi_Click(sender, e);
                    MessageBox.Show("Đã xóa sinh viên khỏi hệ thống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi xóa sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // LÀM MỚI FORM
        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            XoaTatCaLoi();
            isBindingData = true;

            txtMaSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = new DateTime(2006, 1, 1);
            rbNam.Checked = true;
            txtEmail.Clear();
            txtDienThoai.Clear();
            nudDiem.Value = 8.5m;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dgvSinhVien.ClearSelection();

            isBindingData = false;

            txtMaSV.Focus();
        }

        #endregion

        #region --- 4. TÌM KIẾM VÀ LỌC DỮ LIỆU (CƠ SỞ DỮ LIỆU & GIAO DIỆN) ---

        // 1. TÌM KIẾM PHÍA CƠ SỞ DỮ LIỆU (Gọi qua BUL -> DAO Search ADO.NET)
        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                string maLopLoc = cboLocLop.SelectedValue?.ToString() ?? "";
                double diemTu = (double)nudDiemTu.Value;

                // Tìm kiếm phía cơ sở dữ liệu qua tầng Business
                var ketQua = svBUL.Search(tuKhoa, maLopLoc, diemTu);
                HienThiDanhSachLenGrid(ketQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm từ CSDL: {ex.Message}", "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. KHI CHỌN LỚP NÀO THÌ HIỂN THỊ SINH VIÊN CỦA LỚP ĐẤY (Lọc theo lớp)
        private void CboLocLop_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LocVaHienThiSinhVien();
        }

        private void BtnHienThiTatCa_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocLop.SelectedIndex = 0;
            nudDiemTu.Value = 0.0m;
            TaiLaiDanhSachSinhVien();
            HienThiDanhSachLenGrid(danhSachSinhVien);
        }

        private void TaiLaiDanhSachSinhVien()
        {
            try
            {
                danhSachSinhVien = svBUL.GetAllSinhVien().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách sinh viên từ CSDL:\n{ex.Message}", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhSachSinhVien = new List<SinhVien>();
            }
        }

        // Tìm kiếm và lọc trên giao diện
        private void LocVaHienThiSinhVien()
        {
            if (cboLocLop.SelectedValue == null) return;

            string tuKhoa = txtTimKiem.Text.Trim();
            string maLop = cboLocLop.SelectedValue.ToString() ?? "";
            double diemTu = (double)nudDiemTu.Value;

            var ketQua = danhSachSinhVien.Where(sv =>
                (string.IsNullOrEmpty(tuKhoa) ||
                    sv.MaSV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    sv.Email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    sv.DienThoai.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(maLop) ||
                    sv.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase)) &&
                sv.Diem >= diemTu)
                .ToList();

            HienThiDanhSachLenGrid(ketQua);
        }

        #endregion

        #region --- 5. HÀM TIỆN ÍCH HIỂN THỊ LỖI VÀ GIAO DIỆN ---

        private void XoaTatCaLoi()
        {
            errPMaSV.Clear();
            erpHoten.Clear();
            erpEmail.Clear();
            erpDienThoai.Clear();
            errorProvider.Clear();
        }

        // Hiển thị lỗi qua ErrorProvider theo đúng mẫu cô giáo (switch-case theo tên trường)
        private void HienThiLoiErrorProvider(List<ValidationResult> errors)
        {
            XoaTatCaLoi();
            Control? firstErrorControl = null;

            // Hiển thị thông báo lỗi tương ứng với từng trường
            foreach (var error in errors)
            {
                if (error.MemberNames != null && error.MemberNames.Any())
                {
                    string fieldName = error.MemberNames.First();
                    switch (fieldName)
                    {
                        case "MaSV":
                            errPMaSV.SetError(txtMaSV, error.ErrorMessage);
                            firstErrorControl ??= txtMaSV;
                            break;
                        case "HoTen":
                            erpHoten.SetError(txtHoTen, error.ErrorMessage);
                            firstErrorControl ??= txtHoTen;
                            break;
                        case "Email":
                            erpEmail.SetError(txtEmail, error.ErrorMessage);
                            firstErrorControl ??= txtEmail;
                            break;
                        case "SoDienThoai":
                        case "DienThoai":
                            erpDienThoai.SetError(txtDienThoai, error.ErrorMessage);
                            firstErrorControl ??= txtDienThoai;
                            break;
                        case "Diem":
                            errorProvider.SetError(nudDiem, error.ErrorMessage);
                            firstErrorControl ??= nudDiem;
                            break;
                        case "MaLop":
                            errorProvider.SetError(cboLop, error.ErrorMessage);
                            firstErrorControl ??= cboLop;
                            break;
                        default:
                            MessageBox.Show(error.ErrorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(error.ErrorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            firstErrorControl?.Focus();

            // Hiển thị hộp thoại tổng hợp tất cả lỗi 1 lần duy nhất (đặt ngoài vòng lặp foreach để không hiện lặp hộp thoại)
            string errorMessage = string.Join("\n", errors.Select(err => err.ErrorMessage));
            MessageBox.Show(errorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HienThiDanhSachLenGrid(List<SinhVien> danhSach)
        {
            var dsHienThi = danhSach.Select(s => new SinhVienGridViewModel
            {
                MaSV = s.MaSV,
                HoTen = s.HoTen,
                NgaySinhFormatted = s.NgaySinh.ToString("dd/MM/yyyy"),
                GioiTinhFormatted = s.GioiTinh ? "Nam" : "Nữ",
                Email = s.Email,
                DienThoai = s.DienThoai,
                DiemFormatted = s.Diem.ToString("F1"),
                TenLop = s.LopHoc?.TenLop ?? "",
                TrangThai = s.TrangThai
            }).ToList();

            dgvSinhVien.DataSource = dsHienThi;
            lblTongSo.Text = $"Tổng số: {danhSach.Count} sinh viên";
        }

        private void DgvSinhVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSinhVien.Rows.Count) return;

            string? maSV = dgvSinhVien.Rows[e.RowIndex].Cells["colMaSV"].Value?.ToString();
            if (!string.IsNullOrEmpty(maSV))
            {
                var sv = danhSachSinhVien.FirstOrDefault(s =>
                    s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
                if (sv != null)
                {
                    errorProvider.Clear();
                    HienThiThongTinLenControls(sv, giuNguyenMaSV: false);
                    btnThem.Enabled = false;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void HienThiThongTinLenControls(SinhVien sv, bool giuNguyenMaSV)
        {
            isBindingData = true;

            if (!giuNguyenMaSV)
            {
                txtMaSV.Text = sv.MaSV;
            }
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;
            if (sv.GioiTinh) rbNam.Checked = true; else rbNu.Checked = true;
            txtEmail.Text = sv.Email;
            txtDienThoai.Text = sv.DienThoai;
            nudDiem.Value = (decimal)sv.Diem;

            if (!string.IsNullOrEmpty(sv.MaLop))
            {
                cboLop.SelectedValue = sv.MaLop;
            }
            cboTrangThai.SelectedItem = sv.TrangThai;

            isBindingData = false;
        }

        private void XoaTrangCacDieuKhienConLai()
        {
            isBindingData = true;

            txtHoTen.Clear();
            dtpNgaySinh.Value = new DateTime(2006, 1, 1);
            rbNam.Checked = true;
            txtEmail.Clear();
            txtDienThoai.Clear();
            nudDiem.Value = 8.5m;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;

            isBindingData = false;
        }

        // Vẽ pill/badge xanh đẹp mắt cho cột Trạng thái y hệt trong ảnh thiết kế
        private void DgvSinhVien_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colTrangThai.Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string trangThai = e.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(trangThai))
                {
                    Graphics? g = e.Graphics;
                    if (g == null) return;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    Color backBadge = Color.FromArgb(220, 252, 231); // #DCFCE7 xanh nhạt
                    Color textBadge = Color.FromArgb(21, 128, 61);   // #15803D xanh lá đậm

                    if (trangThai != "Đang học")
                    {
                        backBadge = Color.FromArgb(241, 245, 249);
                        textBadge = Color.FromArgb(71, 85, 105);
                    }

                    int badgeWidth = 78;
                    int badgeHeight = 22;
                    int x = e.CellBounds.X + 8;
                    int y = e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2;

                    var rect = new Rectangle(x, y, badgeWidth, badgeHeight);
                    using (GraphicsPath path = RoundedRect(rect, 10))
                    {
                        using (Brush brush = new SolidBrush(backBadge))
                        {
                            g.FillPath(brush, path);
                        }
                    }

                    using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
                    using (Brush textBrush = new SolidBrush(textBadge))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(trangThai, font, textBrush, rect, sf);
                    }
                }

                e.Handled = true;
            }
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void nudDiem_ValueChanged(object? sender, EventArgs e) { }
        private void txtHoTen_TextChanged(object? sender, EventArgs e) { }

        #endregion
    }

    // Lớp phụ trợ cho DataSource của DataGridView
    public class SinhVienGridViewModel
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string NgaySinhFormatted { get; set; } = "";
        public string GioiTinhFormatted { get; set; } = "";
        public string Email { get; set; } = "";
        public string DienThoai { get; set; } = "";
        public string DiemFormatted { get; set; } = "";
        public string TenLop { get; set; } = "";
        public string TrangThai { get; set; } = "";
    }

    // Lớp phụ trợ cho ComboBox Lọc lớp
    public class LopItem
    {
        public string MaLop { get; set; } = "";
        public string TenLop { get; set; } = "";
    }
}
