using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucTaiKhoan : UserControl
    {
        BindingSource taikhoanList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;
        string username;
        public ucTaiKhoan()
        {
            InitializeComponent();
            LoadData(1);
            AddFoodBinding();
            LoadCombobox();
            EnabledControl(false);
        }

        #region Phương thức
        void LoadData(int page)
        {
            dgvTaiKhoan.DataSource = taikhoanList;
            taikhoanList.DataSource = TaikhoanDAO.Instance.GetListTaiKhoan(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Tên tài khoản");
            cbxSearch.Items.Add("Quyền");
            cbxSearch.Items.Add("Hoạt động");
            cbxSearch.Items.Add("Email");

        }
        void AddFoodBinding()
        {
            txtUsername.DataBindings.Add(new Binding("Text", dgvTaiKhoan.DataSource, "USERNAME", true, DataSourceUpdateMode.Never));
            txtPassword.DataBindings.Add(new Binding("Text", dgvTaiKhoan.DataSource, "Password", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dgvTaiKhoan.DataSource, "EMAIL", true, DataSourceUpdateMode.Never));
            cbxQuyen.DataBindings.Add(new Binding("Text", dgvTaiKhoan.DataSource, "Is_admin", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            cbxQuyen.SelectedIndex = 0;
        }
        void EnabledControl(bool value)
        {
            txtUsername.Enabled = value;
            txtPassword.Enabled = value;
            txtEmail.Enabled = value;
            cbxQuyen.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
                if (!string.IsNullOrEmpty(txtPassword.Text))
                    if (!string.IsNullOrEmpty(txtEmail.Text))
                        if (!string.IsNullOrEmpty(cbxQuyen.Text))
                            return true;
            return false;
        }
        #endregion
        private void btnRefesh_Click(object sender, EventArgs e)
        {
            bunifuTransition1.HideSync(btnRefesh);
            bunifuTransition1.ShowSync(btnRefesh);
            cbxSearch.Text = "(Tất cả)";
            txtSearch.Clear();
            LoadData(Convert.ToInt32(txtPage.Text));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HoatDongObj.Noidung = "Thêm";
            EnabledControl(true);
            ClearControl();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (DieuKien() == true)
            {
                string admin;
                if (cbxQuyen.SelectedIndex == 1)
                    admin = "1";
                else admin = "0";

                string temp = funtions.Encrypt(txtPassword.Text);
                string pass = funtions.ReverseString(temp);

                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":
                        if (TaikhoanDAO.Instance.Them(txtUsername.Text, pass, admin, txtEmail.Text))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(1, txtUsername.Text);
                            ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            ClearControl();
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;

                    case "Sửa":
                        if (TaikhoanDAO.Instance.Sua(txtUsername.Text, admin, txtEmail.Text))
                        {
                            //LichSuHoatDongDAO.Instance.ThongBao(1, txtUsername.Text);
                            ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi sửa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;
                }
            }
            else ThongBao.Show("Thông tin chưa điền đầy đủ", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                username = dgvTaiKhoan[3, e.RowIndex].Value.ToString();
                TaikhoanObj.Is_admin = dgvTaiKhoan[5, e.RowIndex].Value.ToString();
                TaikhoanObj.Is_active = dgvTaiKhoan[6, e.RowIndex].Value.ToString();
                TaikhoanObj.Ngaytao = dgvTaiKhoan[7, e.RowIndex].Value.ToString();
                TaikhoanObj.Email = dgvTaiKhoan[8, e.RowIndex].Value.ToString();
                PhanQuyenObj.Taikhoan = dgvTaiKhoan[3, e.RowIndex].Value.ToString();
            }


            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Xem";
                    timer1.Start();
                    frmPhanQuyen frmPhanQuyen = new frmPhanQuyen();
                    frmPhanQuyen.ShowDialog();
                    break;
                case 1:
                    HoatDongObj.Noidung = "Sửa";
                    EnabledControl(true);
                    txtPassword.Enabled = false;
                    txtUsername.Enabled = false;
                    break;
                case 2:
                    if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + username + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                    {
                        if (TaikhoanDAO.Instance.Xoa(username))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(3, username);
                            LoadData(Convert.ToInt32(txtPage.Text));
                            ThongBao.Show("Xóa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi xóa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                    }
                    TrangThaiObj.Trangthai = "close";
                    break;
                default:
                    TrangThaiObj.Trangthai = "close";
                    break;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = TaikhoanDAO.Instance.CountDataTaiKhoan();
            int lastPage = count / 10;

            if (lastPage % 10 != 0)
                lastPage++;
            else lastPage = 1;

            LoadData(lastPage);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPage.Text);

            if (page > 1)
                page--;

            LoadData(page);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPage.Text);
            int count = TaikhoanDAO.Instance.CountDataTaiKhoan() / 10;
            if (count % 10 != 0)
                count++;
            else count = 1;

            if (page < count)
                page++;

            LoadData(page);
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxSearch.SelectedIndex)
            {
                case 0:
                    index = 0;
                    break;
                case 1:
                    index = 1;
                    break;
                case 2:
                    index = 2;
                    break;
                case 3:
                    index = 3;
                    break;
            }

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            List<AccountObj> dt = null;
            switch (index)
            {
                case 0:
                    dt = TaikhoanDAO.Instance.TimKiemTheoTen("USERNAME", txtSearch.Text);
                    break;
                case 1:
                    if (String.Compare(txtSearch.Text, "Admin", true) == 0)
                    {
                        dt = TaikhoanDAO.Instance.TimKiemTheoTen("IS_ADMIN", "1");
                    }
                    if (String.Compare(txtSearch.Text, "User", true) == 0)
                    {
                        dt = TaikhoanDAO.Instance.TimKiemTheoTen("IS_ADMIN", "0");
                    }
                    break;
                case 2:
                    if (String.Compare(txtSearch.Text, "Online", true) == 0)
                    {
                        dt = TaikhoanDAO.Instance.TimKiemTheoTen("IS_ACTIVE", "1");
                    }
                    if (String.Compare(txtSearch.Text, "Offline", true) == 0)
                    {
                        dt = TaikhoanDAO.Instance.TimKiemTheoTen("IS_ACTIVE", "0");
                    }
                    break;
                case 3:
                    dt = TaikhoanDAO.Instance.TimKiemTheoTen("EMAIL", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                taikhoanList.DataSource = dt;
                dgvTaiKhoan.DataSource = taikhoanList;
            }
            else ThongBao.Show("Không có dữ liệu cần tìm", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TrangThaiObj.Trangthai == "close")
            {
                LoadData(Convert.ToInt32(txtPage.Text));
                timer1.Stop();
            }
        }


    }
}
