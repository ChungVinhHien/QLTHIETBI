using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucChucVu : UserControl
    {
        BindingSource chucvuList = new BindingSource();
        private MyFuntions funtions = new MyFuntions();
        private int index = 0;

        public ucChucVu()
        {
            InitializeComponent();
            LoadData(1);
            AddFoodBinding();
        }

        #region Phương thức
        void LoadData(int page)
        {

            chucvuList.DataSource = ChucVuDAO.Instance.GetDataChucVu(page);
            dgvChucVu.DataSource = chucvuList;
            txtPage.Text = page.ToString();

            cbxSearch.Items.Add("Mã chức vụ");
            cbxSearch.Items.Add("Tên chức vụ");
        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvChucVu.DataSource, "MACV", true, DataSourceUpdateMode.Never));
            txtTenCV.DataBindings.Add(new Binding("Text", dgvChucVu.DataSource, "TENCV", true, DataSourceUpdateMode.Never));
            txtMoTa.DataBindings.Add(new Binding("Text", dgvChucVu.DataSource, "MOTACV", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            txtTenCV.Clear();
            txtMoTa.Clear();
        }
        void EnabledControl(bool value)
        {
            txtTenCV.Enabled = value;
            txtMoTa.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(txtTenCV.Text))
                if (!string.IsNullOrEmpty(txtMoTa.Text))
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Chức Vụ").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("CV");
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
                        if (ChucVuDAO.Instance.Them(lblTittle.Text, txtTenCV.Text, txtMoTa.Text))
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
                        if (ChucVuDAO.Instance.Sua(lblTittle.Text, txtTenCV.Text, txtMoTa.Text))
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

        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Chức Vụ").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Chức Vụ").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + lblTittle.Text + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (ChucVuDAO.Instance.Xoa(lblTittle.Text))
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
            int count = ChucVuDAO.Instance.CountDataChucVu();
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
            int count = ChucVuDAO.Instance.CountDataChucVu() / 10;
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
                    dt = ChucVuDAO.Instance.TimKiemTheoTen("MACV", txtSearch.Text);
                    break;
                case 1:
                    dt = ChucVuDAO.Instance.TimKiemTheoTen("TENCV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                chucvuList.DataSource = dt;
                dgvChucVu.DataSource = chucvuList;
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
            }
        }
        #endregion


    }
}
