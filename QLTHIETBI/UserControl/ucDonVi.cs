using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucDonVi : UserControl
    {
        BindingSource donviList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;

        public ucDonVi()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
            AddFoodBinding();
        }
        #region Phương thức
        void LoadData(int page)
        {
            donviList.DataSource = DonViDAO.Instance.GetDataDonVi(page);
            dgvDonVi.DataSource = donviList;
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã đơn vị");
            cbxSearch.Items.Add("Tên đơn vị");
            cbxSearch.Items.Add("Địa chỉ");
            cbxSearch.Items.Add("Số điện thoại");

        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "MADV", true, DataSourceUpdateMode.Never));
            txtTenDV.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "TENDV", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "DIACHIDV", true, DataSourceUpdateMode.Never));
            txtSdt.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "SDTDV", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "EMAILDV", true, DataSourceUpdateMode.Never));
            txtMoTa.DataBindings.Add(new Binding("Text", dgvDonVi.DataSource, "MOTADV", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtTenDV.Clear();
            txtDiaChi.Clear();
            txtSdt.Clear();
            txtEmail.Clear();
            txtMoTa.Clear();
        }
        void EnabledControl(bool value)
        {
            txtTenDV.Enabled = value;
            txtDiaChi.Enabled = value;
            txtSdt.Enabled = value;
            txtEmail.Enabled = value;
            txtMoTa.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtTenDV.Text))
                if (!string.IsNullOrEmpty(txtDiaChi.Text))
                    if (!string.IsNullOrEmpty(txtSdt.Text))
                        if (!string.IsNullOrEmpty(txtEmail.Text))
                            if (!string.IsNullOrEmpty(txtMoTa.Text))
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đơn Vị").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("DV");
                EnabledControl(true);
                ClearControl();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (DieuKien() == true)
            {
                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":
                        if (DonViDAO.Instance.Them(lblTittle.Text, txtTenDV.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, txtMoTa.Text))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(1, lblTittle.Text);
                            LoadData(Convert.ToInt32(txtPage.Text));
                            EnabledControl(false);
                            ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;

                    case "Sửa":
                        if (DonViDAO.Instance.Sua(lblTittle.Text, txtTenDV.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, txtMoTa.Text))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(2, lblTittle.Text);
                            LoadData(Convert.ToInt32(txtPage.Text));
                            EnabledControl(false);
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

        private void dgvDonVi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đơn Vị").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đơn Vị").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + lblTittle.Text + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (DonViDAO.Instance.Xoa(lblTittle.Text))
                            {
                                LichSuHoatDongDAO.Instance.ThongBao(3, lblTittle.Text);
                                LoadData(Convert.ToInt32(txtPage.Text));
                                ThongBao.Show("Xóa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                            else
                            {
                                ThongBao.Show("Có lỗi khi xóa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                        }
                    }
                    else ThongBao.Show("Bạn không có quyền xóa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = DonViDAO.Instance.CountDataDonVi();
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
            int count = DonViDAO.Instance.CountDataDonVi() / 10;
            if (count % 10 != 0)
                count++;
            else count = 1;

            if (page < count)
                page++;

            LoadData(page);
        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            switch (index)
            {
                case 0:
                    dt = DonViDAO.Instance.TimKiemTheoTen("MADV", txtSearch.Text);
                    break;
                case 1:
                    dt = DonViDAO.Instance.TimKiemTheoTen("TENDV", txtSearch.Text);
                    break;
                case 2:
                    dt = DonViDAO.Instance.TimKiemTheoTen("DIACHIDV", txtSearch.Text);
                    break;
                case 3:
                    dt = DonViDAO.Instance.TimKiemTheoTen("SDTDV", txtSearch.Text);
                    break;
            }

            if (dt != null && !string.IsNullOrEmpty(txtSearch.Text))
            {
                donviList.DataSource = dt;
                dgvDonVi.DataSource = donviList;
            }
            else ThongBao.Show("Không có dữ liệu cần tìm", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

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

    }
}
