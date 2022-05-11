using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmTaiKhoan : Form
    {
        public frmTaiKhoan()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
        }

        private void ckbxAdmin_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (ckbxAdmin.Checked == true)
                ckbxUser.Checked = false;
        }

        private void ckbxUser_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (ckbxUser.Checked == true)
                ckbxAdmin.Checked = false;
        }
        void LoadData()
        {
            txtUsername.Text = PhanQuyenObj.Taikhoan;
            txtPassword.Text = " * * * * *";
            txtEmail.Text = TaikhoanObj.Email;
            dpkNgayTao.Text = TaikhoanObj.Ngaytao;
            if (TaikhoanObj.Is_admin == "Admin")
            {
                ckbxAdmin.Checked = true;
            }
            else ckbxUser.Checked = true;
        }
        void ClearConTrol()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            dpkNgayTao.Value = DateTime.Now;
        }
        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string admin;
            if (ckbxAdmin.Checked == true)
                admin = "1";
            else admin = "0";

            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    if (TaikhoanDAO.Instance.Them(txtUsername.Text, txtPassword.Text, admin, txtEmail.Text))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(1, txtUsername.Text);
                        ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        ClearConTrol();
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;

                case "Sửa":
                    if (TaikhoanDAO.Instance.Sua(txtUsername.Text, admin, txtEmail.Text) && TaikhoanDAO.Instance.CapNhatAdmin(txtUsername.Text, admin))
                    {
                        if (admin == "1")
                        {

                        }
                        LichSuHoatDongDAO.Instance.ThongBao(1, txtUsername.Text);
                        ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi sửa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;
            }
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    dpkNgayTao.Enabled = false;
                    break;
                case "Sửa":
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                    dpkNgayTao.Enabled = false;
                    LoadData();
                    break;
            }
        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();

        }

    }
}
