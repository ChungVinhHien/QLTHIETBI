using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucNhaCungCap : UserControl
    {
        BindingSource nhacungcapList = new BindingSource();
        private MyFuntions funtions = new MyFuntions();
        private int index = 0;

        public ucNhaCungCap()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
            AddFoodBinding();

        }
        #region Phương thức

        void LoadData(int page)
        {
            nhacungcapList.DataSource = NhaCungCapDAO.Instance.GetDataNhaCungCap(page);
            dgvNhaCungCap.DataSource = nhacungcapList;
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã nhà cung cấp");
            cbxSearch.Items.Add("Tên nhà cung cấp");
            cbxSearch.Items.Add("Địa chỉ");
            cbxSearch.Items.Add("Số điện thoại");

        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvNhaCungCap.DataSource, "MANCC", true, DataSourceUpdateMode.Never));
            txtTenNCC.DataBindings.Add(new Binding("Text", dgvNhaCungCap.DataSource, "TENNCC", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dgvNhaCungCap.DataSource, "DIACHINCC", true, DataSourceUpdateMode.Never));
            txtSdt.DataBindings.Add(new Binding("Text", dgvNhaCungCap.DataSource, "SDTNCC", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dgvNhaCungCap.DataSource, "EMAILNCC", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSdt.Clear();
            txtEmail.Clear();
        }
        void EnabledControl(bool value)
        {
            txtTenNCC.Enabled = value;
            txtDiaChi.Enabled = value;
            txtSdt.Enabled = value;
            txtEmail.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtTenNCC.Text))
                if (!string.IsNullOrEmpty(txtDiaChi.Text))
                    if (!string.IsNullOrEmpty(txtSdt.Text))
                        if (!string.IsNullOrEmpty(txtEmail.Text))
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhà Cung Cấp").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("NCC");
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
                        if (NhaCungCapDAO.Instance.Them(lblTittle.Text, txtTenNCC.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text))
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
                        if (NhaCungCapDAO.Instance.Sua(lblTittle.Text, txtTenNCC.Text, txtDiaChi.Text, txtSdt.Text, txtEmail.Text))
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

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhà Cung Cấp").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhà Cung Cấp").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + lblTittle.Text + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (NhaCungCapDAO.Instance.Xoa(lblTittle.Text))
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
            int count = NhaCungCapDAO.Instance.CountDataNhaCungCap();
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
            int count = NhaCungCapDAO.Instance.CountDataNhaCungCap() / 10;
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
                    dt = NhaCungCapDAO.Instance.TimKiemTheoTen("MANCC", txtSearch.Text);
                    break;
                case 1:
                    dt = NhaCungCapDAO.Instance.TimKiemTheoTen("TENNCC", txtSearch.Text);
                    break;
                case 2:
                    dt = NhaCungCapDAO.Instance.TimKiemTheoTen("DIACHINCC", txtSearch.Text);
                    break;
                case 3:
                    dt = NhaCungCapDAO.Instance.TimKiemTheoTen("SDTNCC", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                nhacungcapList.DataSource = dt;
                dgvNhaCungCap.DataSource = nhacungcapList;
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
        #endregion
    }
}
