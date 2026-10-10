using System;
using System.Collections.Generic;
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

            // Click chọn dòng trên DataGridView
            this.dgvSinhVien.CellClick += DgvSinhVien_CellClick;

            // Vẽ badge trạng thái đẹp mắt
            this.dgvSinhVien.CellPainting += DgvSinhVien_CellPainting;
        }

        #region --- 1. SỰ KIỆN FORM LOAD ---

        private void Form1_Load(object? sender, EventArgs e)
        {
            // 1. Nạp danh sách lớp học thông qua tầng Business (LopBus)
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

            // 5. Nạp danh sách sinh viên lên DataGridView thông qua SinhVienBUL
            HienThiDanhSachLenGrid(svBUL.GetAllSinhVien());

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

            // Gọi qua SinhVienBUL để kiểm tra xem sinh viên có tồn tại hay không
            var sv = svBUL.GetSinhVienById(maSV);
            if (sv != null)
            {
                // Nếu mã sinh viên tồn tại:
                // - Hiển thị tương ứng lên các điều khiển còn lại
                // - Disable chức năng nhập, enable chức năng sửa, xóa
                HienThiThongTinLenControls(sv, giuNguyenMaSV: true);
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                // Chưa tồn tại:
                // - Xóa giá trị các điều khiển textbox còn lại
                // - Enable chức năng nhập, disable chức năng sửa, xóa
                XoaTrangCacDieuKhienConLai();
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        #endregion

        #region --- 3. CÁC NÚT THAO TÁC (THÊM, SỬA, XÓA, LÀM MỚI) ---

        // THÊM SINH VIÊN
        private void BtnThem_Click(object? sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện
            string maLop = cboLop.SelectedValue?.ToString() ?? "";
            var sv = new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rbNam.Checked,
                Email = txtEmail.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                Diem = (double)nudDiem.Value,
                MaLop = maLop,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang học"
            };

            // 2. Chuyển xuống tầng Business (SinhVienBUL) xử lý kiểm tra nghiệp vụ và lưu
            try
            {
                svBUL.AddSinhVien(sv);
                HienThiDanhSachLenGrid(svBUL.GetAllSinhVien());
                MessageBox.Show("Thêm mới sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KiemTraMaSinhVien();
            }
            catch (Exception ex)
            {
                var errors = sv.IsInValid();
                if (errors.Count > 0)
                {
                    string thongBaoLoi = string.Join("\n• ", errors.Select(err => err.ErrorMessage));
                    MessageBox.Show("Thông tin sinh viên chưa hợp lệ:\n• " + thongBaoLoi, "Kiểm tra dữ liệu (Validation)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // SỬA SINH VIÊN
        private void BtnSua_Click(object? sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện
            string maLopMoi = cboLop.SelectedValue?.ToString() ?? "";
            var sv = new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rbNam.Checked,
                Email = txtEmail.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                Diem = (double)nudDiem.Value,
                MaLop = maLopMoi,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Đang học"
            };

            // 2. Gọi tầng Business (SinhVienBUL) xử lý nghiệp vụ sửa
            try
            {
                svBUL.UpdateSinhVien(sv);
                HienThiDanhSachLenGrid(svBUL.GetAllSinhVien());
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var errors = sv.IsInValid();
                if (errors.Count > 0)
                {
                    string thongBaoLoi = string.Join("\n• ", errors.Select(err => err.ErrorMessage));
                    MessageBox.Show("Thông tin sinh viên chưa hợp lệ:\n• " + thongBaoLoi, "Kiểm tra dữ liệu (Validation)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // XÓA SINH VIÊN (CHỨC NĂNG NGUY HIỂM -> XÁC THỰC TRƯỚC KHI THỰC HIỆN)
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
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
                    HienThiDanhSachLenGrid(svBUL.GetAllSinhVien());
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

        #region --- 4. TÌM KIẾM VÀ LỌC DỮ LIỆU ---

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            string maLopLoc = cboLocLop.SelectedValue?.ToString() ?? "";
            double diemTu = (double)nudDiemTu.Value;

            // Gọi qua SinhVienBUL để tìm kiếm và lọc dữ liệu
            var ketQua = svBUL.Search(tuKhoa, maLopLoc, diemTu);
            HienThiDanhSachLenGrid(ketQua);
        }

        private void BtnHienThiTatCa_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocLop.SelectedIndex = 0;
            nudDiemTu.Value = 0.0m;
            HienThiDanhSachLenGrid(svBUL.GetAllSinhVien());
        }

        #endregion

        #region --- 5. HÀM TIỆN ÍCH HIỂN THỊ GIAO DIỆN ---

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
                var sv = svBUL.GetSinhVienById(maSV);
                if (sv != null)
                {
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
