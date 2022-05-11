using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucKeHoachMS : UserControl
    {
        BindingSource kehoachList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;
        string makh;
        public ucKeHoachMS()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
            AddFoodBinding();
            EnabledControl(false);
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvKeHoach.DataSource = kehoachList;
            kehoachList.DataSource = KeHoachMuaSamDAO.Instance.GetListKeHoachMS(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã kế hoạch");
            cbxSearch.Items.Add("Thời gian áp dụng");
            cbxSearch.Items.Add("Thời gian hiệu lực");
            cbxSearch.Items.Add("Đơn vị");
            cbxSearch.Items.Add("Phòng ban");
            cbxSearch.Items.Add("Trạng Thái");

            cbxTT.Items.Add("Từ chối");
            cbxTT.Items.Add("Đã duyệt");
            cbxTT.Items.Add("Chưa duyệt");


            cbxDV.DataSource = DonViDAO.Instance.GetDataDonVi();
            cbxDV.DisplayMember = "TENDV";
            cbxDV.ValueMember = "MADV";

        }
        void AddFoodBinding()
        {
            lblTittle.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "MAKHMS", true, DataSourceUpdateMode.Never));
            dpkTGAD.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "TGAPDUNG", true, DataSourceUpdateMode.Never));
            dpkTGHL.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "TGHIEULUC", true, DataSourceUpdateMode.Never));
            cbxDV.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "Donvi", true, DataSourceUpdateMode.Never));
            cbxPB.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "Phongban", true, DataSourceUpdateMode.Never));
            cbxTT.DataBindings.Add(new Binding("Text", dgvKeHoach.DataSource, "Trangthai", true, DataSourceUpdateMode.Never));
        }
        void ClearControl()
        {
            dpkTGAD.Value = DateTime.Now.Date;
            dpkTGHL.Value = DateTime.Now.Date;
            cbxDV.SelectedIndex = 0;
            cbxTT.SelectedIndex = 2;

        }
        void EnabledControl(bool value)
        {
            dpkTGAD.Enabled = value;
            dpkTGHL.Enabled = value;
            cbxDV.Enabled = value;
            cbxPB.Enabled = value;
        }
        bool DieuKien()
        {
            if (!string.IsNullOrEmpty(cbxDV.Text))
                if (!string.IsNullOrEmpty(cbxPB.Text))
                    if (!string.IsNullOrEmpty(cbxTT.Text))
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Kế Hoạch Mua Sắm").Rows[0][1].ToString() == "True")
            {
                HoatDongObj.Noidung = "Thêm";
                lblTittle.Text = funtions.SDienMaTuDong("KHMS");
                EnabledControl(true);
                ClearControl();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (DieuKien() == true)
            {
                string tgapdung = dpkTGAD.Value.ToString("MM/dd/yyyy");
                string tghieuluc = dpkTGHL.Value.ToString("MM/dd/yyyy");
                string donvi = cbxDV.SelectedValue.ToString();
                string phongban = PhongBanDAO.Instance.GetDataPBByTenPB(cbxPB.Text).Rows[0][0].ToString();
                string trangthai;


                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":
                        cbxTT.SelectionStart = 2;
                        if (KeHoachMuaSamDAO.Instance.Them(lblTittle.Text, tgapdung, tghieuluc, donvi, phongban, "NULL"))
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
                        if (cbxTT.SelectedIndex == 2) trangthai = "NULL";
                        else trangthai = cbxTT.SelectedIndex.ToString();

                        if (KeHoachMuaSamDAO.Instance.Sua(lblTittle.Text, tgapdung, tghieuluc, donvi, phongban, trangthai))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(2, lblTittle.Text);
                            LoadData(Convert.ToInt32(txtPage.Text));
                            EnabledControl(false);
                            cbxTT.Enabled = false;
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
        private void dgvKeHoach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                makh = dgvKeHoach[3, e.RowIndex].Value.ToString();
                KeHoachMSObj.Makh = makh;
                KeHoachMSObj.Tgad = dgvKeHoach[4, e.RowIndex].Value.ToString();
                KeHoachMSObj.Tghl = dgvKeHoach[5, e.RowIndex].Value.ToString();
                KeHoachMSObj.Dvt = dgvKeHoach[6, e.RowIndex].Value.ToString();
                KeHoachMSObj.Pb = dgvKeHoach[7, e.RowIndex].Value.ToString();
                KeHoachMSObj.Tt = dgvKeHoach[8, e.RowIndex].Value.ToString();
            }

            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Duyệt";
                    timer1.Start();
                    frmDeXuat frmDeXuat = new frmDeXuat();
                    frmDeXuat.ShowDialog();
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Kế Hoạch Mua Sắm").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        EnabledControl(true);
                        cbxTT.Enabled = true;
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 2:

                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Kế Hoạch Mua Sắm").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + makh + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (KeHoachMuaSamDAO.Instance.Xoa(makh))
                            {
                                LichSuHoatDongDAO.Instance.ThongBao(3, makh);
                                LoadData(Convert.ToInt32(txtPage.Text));
                                ThongBao.Show("Xóa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                            else
                            {
                                ThongBao.Show("Có lỗi khi xóa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                        }
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền xóa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
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
            int count = KeHoachMuaSamDAO.Instance.CountData();
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
            int count = KeHoachMuaSamDAO.Instance.CountData() / 10;
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
                case 4:
                    index = 4;
                    break;
                case 5:
                    index = 5;
                    break;
            }
        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            List<KeHoachMSObj> dt = null;
            switch (index)
            {
                case 0:
                    dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("MAKHMS", txtSearch.Text);
                    break;
                case 1:
                    dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("TGAPDUNG", txtSearch.Text);
                    break;
                case 2:
                    dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("TGHIEULUC", txtSearch.Text);
                    break;
                case 3:
                    dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("DV.TENDV", txtSearch.Text);
                    break;
                case 4:
                    dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("PB.TENPB", txtSearch.Text);
                    break;
                case 5:
                    if (String.Compare(txtSearch.Text, "Duyệt", true) == 0)
                    {
                        dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "1");
                    }
                    if (String.Compare(txtSearch.Text, "Từ chối", true) == 0)
                    {
                        dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "0");
                    }
                    if (String.Compare(txtSearch.Text, "Chưa duyệt", true) == 0)
                    {
                        dt = KeHoachMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "NULL");
                    }
                    break;
            }

            if (dt != null && dt.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                kehoachList.DataSource = dt;
                dgvKeHoach.DataSource = kehoachList;
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


        private void cbxDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxPB.DataSource = PhongBanDAO.Instance.GetDataPhongBanByMaDV(cbxDV.SelectedValue.ToString());
            cbxPB.DisplayMember = "TENPB";
            cbxPB.ValueMember = "MAPB";
        }
    }
}
