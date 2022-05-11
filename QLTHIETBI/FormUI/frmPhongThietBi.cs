using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhongThietBi : Form
    {
        MyFuntions funtions = new MyFuntions();
        BindingSource ThietBiList = new BindingSource();
        public frmPhongThietBi()
        {
            InitializeComponent();
            LoadCombobox();
            TrangThaiObj.Trangthai = "open";
        }
        #region Phương thức
        void LoadCombobox()
        {
            cbxNhanVien.DataSource = NhanVienDAO.Instance.GetDataNhanVien();
            cbxNhanVien.DisplayMember = "TENNV";
            cbxNhanVien.ValueMember = "MANV";

            cbxTrangThai.Items.Add("Đang hoạt động");
            cbxTrangThai.Items.Add("Trống");
        }

        void LoadData()
        {
            DataTable dt = PhongThietBiDAO.Instance.GetDataPhongTB(PhongThietBiObj.Maptb);

            lblTittle.Text = PhongThietBiObj.Maptb;
            txtTenPhong.Text = dt.Rows[0][1].ToString();
            txtSoPhong.Text = dt.Rows[0][2].ToString();
            txtSoLuong.Text = dt.Rows[0][3].ToString();
            txtViTri.Text = dt.Rows[0][4].ToString();
            cbxTrangThai.Text = dt.Rows[0][5].ToString();
            cbxNhanVien.Text = dt.Rows[0][6].ToString();

            ThietBiList.DataSource = ThietBiDAO.Instance.GetDaTaThietBiPTB(lblTittle.Text);
            dgvThietBi.DataSource = ThietBiList;
        }
        void EnableControls(bool value)
        {
            lblTittle.Enabled = value;
            txtTenPhong.Enabled = value;
            txtSoPhong.Enabled = value;
            txtViTri.Enabled = value;
            txtSoLuong.Enabled = value;
            cbxTrangThai.Enabled = value;
            cbxNhanVien.Enabled = value;
            linkLuu.Visible = value;
            linkThoat.Visible = value;

        }
        #endregion

        #region Sự kiện
        private void frmPhongThietBi_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    lblTittle.Text = funtions.SDienMaTuDong("PTB");
                    break;
                case "Xem":
                    LoadData();
                    EnableControls(false);
                    break;
                case "Sửa":
                    LoadData();
                    break;
            }
        }

        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    if (PhongThietBiDAO.Instance.Them(lblTittle.Text, txtTenPhong.Text, txtSoPhong.Text, txtSoLuong.Text, txtViTri.Text, cbxTrangThai.Text, cbxNhanVien.SelectedValue.ToString()))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(1, lblTittle.Text);
                        ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;

                case "Sửa":
                    if (PhongThietBiDAO.Instance.Sua(lblTittle.Text, txtTenPhong.Text, txtSoPhong.Text, txtSoLuong.Text, txtViTri.Text, cbxTrangThai.Text, cbxNhanVien.SelectedValue.ToString()))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(2, lblTittle.Text);
                        ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
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


        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Thiết Bị").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "TBPTB";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }
        private void linkPhieuDanNhan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HoatDongObj.Noidung = "PHIEUDANNHAN";
            frmPrint print = new frmPrint();
            print.ShowDialog();
        }


        #endregion


    }
}
