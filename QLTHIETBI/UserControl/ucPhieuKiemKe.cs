using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucPhieuKiemKe : UserControl
    {
        BindingSource phieukkList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;
        public ucPhieuKiemKe()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvPhieuKK.DataSource = phieukkList;
            phieukkList.DataSource = PhieuKiemKeDAO.Instance.GetDataPhieuKiemKe(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã phiếu kiểm kê");
            cbxSearch.Items.Add("Tên nhân viên");
            cbxSearch.Items.Add("Ngày kiểm kê");
            cbxSearch.Items.Add("Đơn vị");
        }

        #endregion

        #region Sự Kiện
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][1].ToString() == "True")
            {
                frmPhieuKiemKe kiemke = new frmPhieuKiemKe();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                kiemke.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void dgvPhieuKK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                PhieuKiemKeObj.Mapkk = dgvPhieuKK[3, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Ngaykk = dgvPhieuKK[4, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Ngaylap = dgvPhieuKK[5, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Donvi = dgvPhieuKK[6, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Nguoikk1 = dgvPhieuKK[7, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Nguoikk2 = dgvPhieuKK[8, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Nguoikk3 = dgvPhieuKK[9, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.Tongsl = dgvPhieuKK[10, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.TongNG = dgvPhieuKK[11, e.RowIndex].Value.ToString();
                PhieuKiemKeObj.TongGTCL = dgvPhieuKK[12, e.RowIndex].Value.ToString();

            }
            frmPhieuKiemKe kiemke = new frmPhieuKiemKe();
            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Xem";
                    kiemke.ShowDialog();
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        kiemke.ShowDialog();
                        timer1.Start();
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    break;
                case 2:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + PhieuKiemKeObj.Mapkk + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (PhieuKiemKeDAO.Instance.Xoa(PhieuKiemKeObj.Mapkk))
                            {
                                //LichSuHoatDongDAO.Instance.ThongBao(3, lblTittle.Text);
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
            int count = PhieuKiemKeDAO.Instance.CountDataPhieuKiemKe();
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
            int count = PhieuKiemKeDAO.Instance.CountDataPhieuKiemKe() / 10;
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
                    index = 3;
                    break;
                case 3:
                    index = 4;
                    break;
            }
        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            switch (index)
            {
                case 0:
                    dt = PhieuKiemKeDAO.Instance.TimKiemTheoTen("MAPKK", txtSearch.Text);
                    break;
                case 1:
                    dt = PhieuKiemKeDAO.Instance.TimKiemTheoTen("NGUOIKK ", txtSearch.Text);
                    break;
                case 2:
                    dt = PhieuKiemKeDAO.Instance.TimKiemTheoTen("NGAYKK", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"));
                    break;
                case 3:
                    dt = PhieuKiemKeDAO.Instance.TimKiemTheoTen("DV.TENDV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                phieukkList.DataSource = dt;
                dgvPhieuKK.DataSource = phieukkList;
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
            if (dgvPhieuKK.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvPhieuKK.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvPhieuKK.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvPhieuKK.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvPhieuKK.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvPhieuKK.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PhieuKiemKeObj.Mapkk))
            {
                HoatDongObj.Noidung = "PKK";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
        }


        #endregion
    }
}
