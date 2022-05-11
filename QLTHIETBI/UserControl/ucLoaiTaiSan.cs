using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucLoaiTaiSan : UserControl
    {
        BindingSource loaitsList = new BindingSource();
        private MyFuntions funtions = new MyFuntions();
        private int index = 0;
        public ucLoaiTaiSan()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
            AddFoodBinding();
        }
        #region Phương thức
        void LoadData(int page)
        {
            loaitsList.DataSource = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSan(page);
            dgvLoaiTS.DataSource = loaitsList;
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxNhomTS.DataSource = NhomTaiSanDAO.Instance.GetDataNhomTaiSan();
            cbxNhomTS.DisplayMember = "TENNHOMTS";
            cbxNhomTS.ValueMember = "MANHOMTS";

            cbxSearch.Items.Add("Mã loại tài sản");
            cbxSearch.Items.Add("Tên loại tài sản");
            cbxSearch.Items.Add("Tên nhóm tài sản");

        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "MALOAITS", true, DataSourceUpdateMode.Never));
            txtTenLTS.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "TENLOAITS", true, DataSourceUpdateMode.Never));
            txtKHMin.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "NAMKHMIN", true, DataSourceUpdateMode.Never));
            txtKHMax.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "NAMKHMAX", true, DataSourceUpdateMode.Never));
            txtTGSD.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "TGSUDUNG", true, DataSourceUpdateMode.Never));
            txtTyLeHM.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "TYLEHAOMON", true, DataSourceUpdateMode.Never));
            cbxNhomTS.DataBindings.Add(new Binding("Text", dgvLoaiTS.DataSource, "TENNHOMTS", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtTenLTS.Clear();
            txtKHMin.Clear();
            txtKHMax.Clear();
            txtTGSD.Clear();
            txtTyLeHM.Clear();
            cbxNhomTS.SelectedIndex = -1;
        }
        void EnabledControl(bool value)
        {
            txtTenLTS.Enabled = value;
            txtKHMin.Enabled = value;
            txtKHMax.Enabled = value;
            txtTGSD.Enabled = value;
            txtTyLeHM.Enabled = value;
            cbxNhomTS.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtTenLTS.Text))
                if (!string.IsNullOrEmpty(txtKHMin.Text))
                    if (!string.IsNullOrEmpty(txtKHMin.Text) && Convert.ToInt32(txtKHMax.Text) > Convert.ToInt32(txtKHMin.Text))
                        if (!string.IsNullOrEmpty(txtTGSD.Text))
                            if (!string.IsNullOrEmpty(txtTyLeHM.Text))
                                if (!string.IsNullOrEmpty(cbxNhomTS.Text))
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Loại Tài Sản").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("LTS");
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
                        if (LoaiTaiSanDAO.Instance.Them(lblTittle.Text, txtTenLTS.Text, txtKHMin.Text, txtKHMax.Text, txtTGSD.Text, txtTyLeHM.Text, cbxNhomTS.SelectedValue.ToString()))
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
                        if (LoaiTaiSanDAO.Instance.Sua(lblTittle.Text, txtTenLTS.Text, txtKHMin.Text, txtKHMax.Text, txtTGSD.Text, funtions.ReplaceChars(txtTyLeHM.Text), cbxNhomTS.SelectedValue.ToString()))
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

        private void dgvLoaiTS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Loại Tài Sản").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Loại Tài Sản").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + lblTittle.Text + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (LoaiTaiSanDAO.Instance.Xoa(lblTittle.Text))
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
            int count = LoaiTaiSanDAO.Instance.CountDataLoaiTaiSan();
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
            int count = LoaiTaiSanDAO.Instance.CountDataLoaiTaiSan() / 10;
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
                    dt = LoaiTaiSanDAO.Instance.TimKiemTheoTen("MALOAITS", txtSearch.Text);
                    break;
                case 1:
                    dt = LoaiTaiSanDAO.Instance.TimKiemTheoTen("TENLOAITS", txtSearch.Text);
                    break;
                case 2:
                    dt = LoaiTaiSanDAO.Instance.TimKiemTheoTen("NTS.TENNHOMTS", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                loaitsList.DataSource = dt;
                dgvLoaiTS.DataSource = loaitsList;
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
            }
        }
        #endregion


    }
}
