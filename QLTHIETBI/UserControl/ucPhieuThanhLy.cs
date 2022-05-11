using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucPhieuThanhLy : UserControl
    {
        BindingSource phieuthanhlyList = new BindingSource();
        private int index = 0;
        public ucPhieuThanhLy()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvPhieuThanhLy.DataSource = phieuthanhlyList;
            phieuthanhlyList.DataSource = PhieuThanhLyDAO.Instance.GetDataPhieuThanhLy(page);
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã phiếu thanh lý");
            cbxSearch.Items.Add("Tên thiết bị");
            cbxSearch.Items.Add("Tên nhân viên");
            cbxSearch.Items.Add("Ngày thanh lý");

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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][1].ToString() == "True")
            {
                frmPhieuThanhLy thanhly = new frmPhieuThanhLy();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                thanhly.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }
        private void dgvPhieuThanhLy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                PhieuThanhLyObj.Maptl = dgvPhieuThanhLy[3, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Matb = dgvPhieuThanhLy[4, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Tentb = dgvPhieuThanhLy[5, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Ngaylap = dgvPhieuThanhLy[6, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Tennv = dgvPhieuThanhLy[7, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Ngaytl = dgvPhieuThanhLy[8, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Chiphitl = dgvPhieuThanhLy[9, e.RowIndex].Value.ToString();
                PhieuThanhLyObj.Gtthuhoi = dgvPhieuThanhLy[10, e.RowIndex].Value.ToString();
            }
            frmPhieuThanhLy thanhLy = new frmPhieuThanhLy();


            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Xem";
                    thanhLy.ShowDialog();
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        timer1.Start();
                        thanhLy.ShowDialog();
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 2:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + PhieuThanhLyObj.Matb + " của " + PhieuThanhLyObj.Maptl + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (PhieuThanhLyDAO.Instance.Xoa(PhieuThanhLyObj.Maptl, PhieuThanhLyObj.Matb))
                            {
                                //LichSuHoatDongDAO.Instance.ThongBao(3, PhieuThanhLyObj.Maptl + PhieuThanhLyObj.Matb);
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
            int count = PhieuThanhLyDAO.Instance.CountDataPhieuThanhLy();
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
            int count = PhieuThanhLyDAO.Instance.CountDataPhieuThanhLy() / 10;
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
                    dt = PhieuThanhLyDAO.Instance.TimKiemTheoTen("MAPTL", txtSearch.Text);
                    break;
                case 1:
                    dt = PhieuThanhLyDAO.Instance.TimKiemTheoTen("TB.TENTB", txtSearch.Text);
                    break;
                case 2:
                    dt = PhieuThanhLyDAO.Instance.TimKiemTheoTen("NV.TENNV", txtSearch.Text);
                    break;
                case 3:
                    dt = PhieuThanhLyDAO.Instance.TimKiemTheoTen("NGAYTL", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"));
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                phieuthanhlyList.DataSource = dt;
                dgvPhieuThanhLy.DataSource = phieuthanhlyList;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TrangThaiObj.Trangthai == "close")
            {
                LoadData(Convert.ToInt32(txtPage.Text));
                timer1.Stop();
            }
        }

        #endregion

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvPhieuThanhLy.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvPhieuThanhLy.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvPhieuThanhLy.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvPhieuThanhLy.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvPhieuThanhLy.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvPhieuThanhLy.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PhieuThanhLyObj.Maptl))
            {
                HoatDongObj.Noidung = "PTL";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
        }
    }
}
