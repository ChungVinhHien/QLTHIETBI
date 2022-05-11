using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmDeXuat : Form
    {
        MyFuntions funtions = new MyFuntions();
        int soluong = 0;
        int tongtien = 0;
        public frmDeXuat()
        {
            InitializeComponent();
            LoadCombobox();
            TrangThaiObj.Trangthai = "open";
            HoatDongObj.Noidung = "Xem";
        }
        void LoadCombobox()
        {
            cbxMaKH.DataSource = KeHoachMuaSamDAO.Instance.GetDataKeHoachMS();
            cbxMaKH.DisplayMember = "MAKHMS";
            cbxMaKH.ValueMember = "MAKHMS";
        }
        void LoadData()
        {
            txtMaDX.Text = DeXuatMuaSamObj.Madx;
            dpkNgayLap.Text = DeXuatMuaSamObj.Ngaylapdx;
            txtNguoiDX.Text = DeXuatMuaSamObj.Nguoidx;
            txtNguoiDuyet.Text = DeXuatMuaSamObj.Nguoiduyetdx;
            txtGiaiTrinh.Text = DeXuatMuaSamObj.Giaitrinhdx;
            txtTrangThai.Text = DeXuatMuaSamObj.Trangthaidx;
            lblSoLuong.Text = DeXuatMuaSamObj.Sluongdx;
            lblTongTien.Text = DeXuatMuaSamObj.Tongdx;

            dgvChiTiet.DataSource = DeXuatMuaSamDAO.Instance.GetDataCTDeXuatMS(txtMaDX.Text);
            dgvChiTiet.Columns[0].ReadOnly = true;
            for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
            {
                soluong += Convert.ToInt32(dgvChiTiet.Rows[i].Cells[1].Value.ToString());
                tongtien += Convert.ToInt32(dgvChiTiet.Rows[i].Cells[4].Value.ToString());
            }
            lblSoLuong.Text = soluong.ToString();
            lblTongTien.Text = tongtien.ToString();
        }
        void ResetForm()
        {
            txtTrangThai.Clear();
            txtNguoiDX.Focus();
            txtNguoiDuyet.Clear();
            txtGiaiTrinh.Clear();
            cbxMaKH.SelectedIndex = 0;
            lblSoLuong.Text = "";
            lblTongTien.Text = "";
            dgvChiTiet.Rows.Clear();

        }
        bool IsBewteenTwoDates(DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }
        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");

            switch (HoatDongObj.Noidung)
            {
                case "Thêm":

                    if (DeXuatMuaSamDAO.Instance.Them(txtMaDX.Text, ngaylap, txtNguoiDX.Text, txtNguoiDuyet.Text, txtGiaiTrinh.Text, "NULL", lblSoLuong.Text, lblTongTien.Text))
                    {
                        if (dgvChiTiet.RowCount > 1)
                        {
                            for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
                            {
                                string tentb = dgvChiTiet.Rows[i].Cells[0].Value.ToString();
                                string soluong = dgvChiTiet.Rows[i].Cells[1].Value.ToString();
                                string donvi = dgvChiTiet.Rows[i].Cells[2].Value.ToString();
                                string gia = dgvChiTiet.Rows[i].Cells[3].Value.ToString();
                                string tong = dgvChiTiet.Rows[i].Cells[4].Value.ToString();
                                string ghichu = "";
                                if (dgvChiTiet.Rows[i].Cells[5].Value != null)
                                    ghichu = dgvChiTiet.Rows[i].Cells[5].Value.ToString();
                                if (DeXuatMuaSamDAO.Instance.ThemCT(txtMaDX.Text, cbxMaKH.Text, tentb, soluong, gia, tong, ghichu, donvi))
                                {
                                    //LichSuHoatDongDAO.Instance.ThongBao(1, matb);
                                    if (i == dgvChiTiet.RowCount - 2)
                                    {
                                        frmDeXuat_Load(new object(), new EventArgs());
                                        ResetForm();
                                        ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Error, ThongBao.AnimateStyle.FadeIn);
                    }

                    break;

                case "Sửa":
                    if (dgvChiTiet.RowCount > 1)
                    {
                        for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
                        {
                            string tentb = dgvChiTiet.Rows[i].Cells[0].Value.ToString();
                            string soluong = dgvChiTiet.Rows[i].Cells[1].Value.ToString();
                            string donvi = dgvChiTiet.Rows[i].Cells[2].Value.ToString();
                            string gia = dgvChiTiet.Rows[i].Cells[3].Value.ToString();
                            string tong = dgvChiTiet.Rows[i].Cells[4].Value.ToString();
                            string ghichu = "";
                            if (dgvChiTiet.Rows[i].Cells[5].Value != null)
                                ghichu = dgvChiTiet.Rows[i].Cells[5].Value.ToString();
                            if (DeXuatMuaSamDAO.Instance.Sua(txtMaDX.Text, cbxMaKH.Text, txtGiaiTrinh.Text, tentb, soluong, donvi, gia, tong, ghichu))
                            {
                                //LichSuHoatDongDAO.Instance.ThongBao(2, matb);
                                if (i == dgvChiTiet.RowCount - 2)
                                {
                                    ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                    LoadData();
                                }
                            }
                        }

                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi sửa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;
            }
        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }

        private void frmDeXuat_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtTrangThai.Text = "Chưa duyệt";
                    txtMaDX.Text = funtions.SDienMaTuDong("DXMS");
                    txtNguoiDX.Text = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][1].ToString();
                    txtCVNguoiDX.Text = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][8].ToString();
                    break;
                case "Xem":
                    if (TaikhoanObj.Is_admin == "True")
                    {
                        btnDuyet.Visible = true;
                        btnTuchoi.Visible = true;
                    }
                    LoadData();
                    dgvChiTiet.ReadOnly = true;
                    dgvChiTiet.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    cbxMaKH.Enabled = true;
                    txtGiaiTrinh.Enabled = true;
                    break;
                case "Sửa":
                    LoadData();
                    dgvChiTiet.AllowUserToAddRows = false;
                    break;
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            string nguoiduyet = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][1].ToString();
            if (DeXuatMuaSamDAO.Instance.Duyet(txtMaDX.Text, 1, nguoiduyet))
            {
                ThongBao.Show("Duyệt thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
        }

        private void btnTuchoi_Click(object sender, EventArgs e)
        {
            if (DeXuatMuaSamDAO.Instance.Duyet(txtMaDX.Text, 0, TaikhoanObj.Username))
            {
                ThongBao.Show("Đã từ chối đề xuất", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
        }

        private void dgvChiTiet_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
                {
                    if (dgvChiTiet.Rows[i].Cells[1].Value != null)
                        if (int.TryParse(dgvChiTiet.Rows[i].Cells[1].Value.ToString(), out int n))
                        {
                            soluong += Convert.ToInt32(dgvChiTiet.Rows[i].Cells[1].Value.ToString());
                        }
                        else dgvChiTiet.Rows[i].Cells[1].Value = 1;
                }
                lblSoLuong.Text = soluong.ToString();

            }
            if (e.ColumnIndex == 3)
            {
                for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
                {
                    if (dgvChiTiet.Rows[i].Cells[3].Value != null)
                        if (int.TryParse(dgvChiTiet.Rows[i].Cells[3].Value.ToString(), out int n))
                        {
                            dgvChiTiet.Rows[i].Cells[4].Value = Convert.ToInt32(dgvChiTiet.Rows[i].Cells[3].Value.ToString()) * Convert.ToInt32(dgvChiTiet.Rows[i].Cells[1].Value.ToString());
                        }
                        else dgvChiTiet.Rows[i].Cells[3].Value = 0;
                }
            }
            if (e.ColumnIndex == 4)
            {
                for (int i = 0; i < dgvChiTiet.RowCount - 1; i++)
                {

                    if (dgvChiTiet.Rows[i].Cells[4].Value != null)
                        tongtien += Convert.ToInt32(dgvChiTiet.Rows[i].Cells[4].Value.ToString());
                }
                lblTongTien.Text = tongtien.ToString();
            }
        }


        private void dgvChiTiet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvChiTiet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                dgvChiTiet.Rows[e.RowIndex].Cells[4].ReadOnly = true;
            }
            for (int i = 0; i < e.ColumnIndex; i++)
            {
                if (dgvChiTiet.Rows[e.RowIndex].Cells[i].Value == null)
                {
                    dgvChiTiet.CurrentCell = dgvChiTiet.Rows[e.RowIndex].Cells[i];
                    break;
                }
            }

        }


        private void dgvChiTiet_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + DeXuatMuaSamObj.Madx + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
            {
                string madx = txtMaDX.Text;
                string makh = cbxMaKH.Text;
                string tentb = e.Row.Cells[0].Value.ToString();
                if (DeXuatMuaSamDAO.Instance.XoaCT(madx, makh, tentb))
                {
                    LoadData();
                }
            }
            else e.Cancel = true;

        }
    }
}
