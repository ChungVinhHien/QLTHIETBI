using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucPhongBan : UserControl
    {
        BindingSource phongbanList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;
        public ucPhongBan()
        {
            InitializeComponent();
            LoadData(1);
            AddFoodBinding();
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvPhongBan.DataSource = phongbanList;
            phongbanList.DataSource = PhongBanDAO.Instance.GetDataPhongBan(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxDonVi.DataSource = DonViDAO.Instance.GetDataDonVi();
            cbxDonVi.DisplayMember = "TENDV";
            cbxDonVi.ValueMember = "MADV";

            cbxSearch.Items.Add("Mã phòng ban");
            cbxSearch.Items.Add("Tên phòng ban");
            cbxSearch.Items.Add("Địa chỉ");
            cbxSearch.Items.Add("Số điện thoại");
            cbxSearch.Items.Add("Tên đơn vị");
        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "MAPB", true, DataSourceUpdateMode.Never));
            txtTenPB.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "TENPB", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "DIACHIPB", true, DataSourceUpdateMode.Never));
            txtSdt.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "SDTPB", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "EMAILPB", true, DataSourceUpdateMode.Never));
            txtMoTa.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "MOTAPB", true, DataSourceUpdateMode.Never));
            cbxDonVi.DataBindings.Add(new Binding("Text", dgvPhongBan.DataSource, "TENDV", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtTenPB.Clear();
            txtDiaChi.Clear();
            txtSdt.Clear();
            txtEmail.Clear();
            txtMoTa.Clear();
            cbxDonVi.SelectedIndex = 1;
        }
        void EnabledControl(bool value)
        {
            txtTenPB.Enabled = value;
            txtDiaChi.Enabled = value;
            txtSdt.Enabled = value;
            txtEmail.Enabled = value;
            txtMoTa.Enabled = value;
            cbxDonVi.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtTenPB.Text))
                if (!string.IsNullOrEmpty(txtDiaChi.Text))
                    if (!string.IsNullOrEmpty(txtSdt.Text))
                        if (!string.IsNullOrEmpty(txtEmail.Text))
                            if (!string.IsNullOrEmpty(txtMoTa.Text))
                                if (!string.IsNullOrEmpty(cbxDonVi.Text))
                                    return true;
            return false;
        }
        #endregion

        #region Sự kiện
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Ban").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("PB");
                EnabledControl(true);
                ClearControl();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    if (PhongBanDAO.Instance.Them(lblTittle.Text, txtTenPB.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, txtMoTa.Text, cbxDonVi.SelectedValue.ToString()))
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
                    if (PhongBanDAO.Instance.Sua(lblTittle.Text, txtTenPB.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, txtMoTa.Text, cbxDonVi.SelectedValue.ToString()))
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

        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Ban").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Ban").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + lblTittle.Text + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (PhongBanDAO.Instance.Xoa(lblTittle.Text))
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
            int count = PhongBanDAO.Instance.CountDataPhongBan();
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
            int count = PhongBanDAO.Instance.CountDataPhongBan() / 10;
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
                    dt = PhongBanDAO.Instance.TimKiemTheoTen("MAPB", txtSearch.Text);
                    break;
                case 1:
                    dt = PhongBanDAO.Instance.TimKiemTheoTen("TENPB", txtSearch.Text);
                    break;
                case 2:
                    dt = PhongBanDAO.Instance.TimKiemTheoTen("DIACHIPB", txtSearch.Text);
                    break;
                case 3:
                    dt = PhongBanDAO.Instance.TimKiemTheoTen("SDTPB", txtSearch.Text);
                    break;
                case 4:
                    dt = PhongBanDAO.Instance.TimKiemTheoTen("DV.TENDV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                phongbanList.DataSource = dt;
                dgvPhongBan.DataSource = phongbanList;
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
                case 4:
                    index = 4;
                    break;
            }
        }


        #endregion


    }
}
