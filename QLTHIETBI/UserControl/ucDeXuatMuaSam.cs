using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucDeXuatMuaSam : UserControl
    {
        BindingSource dexuatList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;

        public ucDeXuatMuaSam()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            string value;
            int type;
            dgvDeXuat.DataSource = dexuatList;
            if (TaikhoanObj.Is_admin == "True")
            {
                value = KeHoachMSObj.Makh;
                type = 1;
            }
            else
            {
                value = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][1].ToString();
                type = 0;
            }
            dexuatList.DataSource = DeXuatMuaSamDAO.Instance.GetListDeXuatMuaSam(page, value, type);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã đề xuất");
            cbxSearch.Items.Add("Ngày lập");
            cbxSearch.Items.Add("Người duyệt");
            cbxSearch.Items.Add("Trạng thái");

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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đề Xuất Mua Sắm").Rows[0][1].ToString() == "True")
            {
                frmDeXuat dexuat = new frmDeXuat();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                dexuat.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void dgvDeXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                DeXuatMuaSamObj.Madx = dgvDeXuat[3, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Ngaylapdx = dgvDeXuat[4, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Nguoidx = dgvDeXuat[5, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Nguoiduyetdx = dgvDeXuat[6, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Giaitrinhdx = dgvDeXuat[7, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Trangthaidx = dgvDeXuat[8, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Sluongdx = dgvDeXuat[9, e.RowIndex].Value.ToString();
                DeXuatMuaSamObj.Tongdx = dgvDeXuat[10, e.RowIndex].Value.ToString();

                frmDeXuat dexuat = new frmDeXuat();

                switch (e.ColumnIndex)
                {
                    case 0:
                        HoatDongObj.Noidung = "Xem";
                        dexuat.ShowDialog();
                        break;
                    case 1:
                        if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đề Xuất Mua Sắm").Rows[0][2].ToString() == "True")
                        {
                            HoatDongObj.Noidung = "Sửa";
                            dexuat.ShowDialog();
                            timer1.Start();
                        }
                        else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                        break;
                    case 2:
                        if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đề Xuất Mua Sắm").Rows[0][3].ToString() == "True")
                        {
                            if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + DeXuatMuaSamObj.Madx + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                            {
                                if (DeXuatMuaSamDAO.Instance.Xoa(DeXuatMuaSamObj.Madx))
                                {
                                    LichSuHoatDongDAO.Instance.ThongBao(3, DeXuatMuaSamObj.Madx);
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
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = DeXuatMuaSamDAO.Instance.CountData();
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
            int count = DeXuatMuaSamDAO.Instance.CountData() / 10;
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
            List<DeXuatMuaSamObj> dt = null;
            string nguoidx = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][1].ToString();
            switch (index)
            {
                case 0:
                    dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("MADXMS", txtSearch.Text, nguoidx);
                    break;
                case 1:
                    dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("NGAYLAPDX", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"), nguoidx);
                    break;
                case 2:
                    dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("NGUOIDUYET", txtSearch.Text, nguoidx);
                    break;
                case 3:
                    if (String.Compare(txtSearch.Text, "Duyệt", true) == 0)
                    {
                        dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "1", nguoidx);
                    }
                    if (String.Compare(txtSearch.Text, "Từ chối", true) == 0)
                    {
                        dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "0", nguoidx);
                    }
                    if (String.Compare(txtSearch.Text, "Chưa duyệt", true) == 0)
                    {
                        dt = DeXuatMuaSamDAO.Instance.TimKiemTheoTen("TRANGTHAI", "NULL", nguoidx);
                    }
                    break;
            }

            if (dt != null && dt.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                dexuatList.DataSource = dt;
                dgvDeXuat.DataSource = dexuatList;
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


        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvDeXuat.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvDeXuat.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvDeXuat.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvDeXuat.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvDeXuat.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvDeXuat.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }
        #endregion
    }
}
