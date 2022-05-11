using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucNhanVien : UserControl
    {
        BindingSource nhanvienList = new BindingSource();
        private int index = 0;
        public ucNhanVien()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            nhanvienList.DataSource = NhanVienDAO.Instance.GetDataNhanVien(page);
            dgvNhanVien.DataSource = nhanvienList;
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã nhân viên");
            cbxSearch.Items.Add("Tên nhân viên");
            cbxSearch.Items.Add("Giới tính");
            cbxSearch.Items.Add("Ngày sinh");
            cbxSearch.Items.Add("Địa chỉ");
            cbxSearch.Items.Add("Số điện thoại");
            cbxSearch.Items.Add("Tên phòng ban");
            cbxSearch.Items.Add("Tên chức vụ");

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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhân Viên").Rows[0][1].ToString() == "True")
            {
                frmNhanVien nhanVien = new frmNhanVien();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                nhanVien.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                NhanVienObj.Manv = dgvNhanVien[3, e.RowIndex].Value.ToString();
                NhanVienObj.Tennv = dgvNhanVien[4, e.RowIndex].Value.ToString();
                NhanVienObj.Gioitinhnv = dgvNhanVien[5, e.RowIndex].Value.ToString();
                NhanVienObj.Ngaysinhnv = dgvNhanVien[6, e.RowIndex].Value.ToString();
                NhanVienObj.Diachinv = dgvNhanVien[7, e.RowIndex].Value.ToString();
                NhanVienObj.Sdtnv = dgvNhanVien[8, e.RowIndex].Value.ToString();
                NhanVienObj.Emailnv = dgvNhanVien[9, e.RowIndex].Value.ToString();
                NhanVienObj.Tenpb = dgvNhanVien[10, e.RowIndex].Value.ToString();
                NhanVienObj.Tencv = dgvNhanVien[11, e.RowIndex].Value.ToString();
                NhanVienObj.Hinhanh = NhanVienDAO.Instance.GetHinhAnh(NhanVienObj.Manv).Rows[0][0].ToString();
            }
            frmNhanVien nhanVien = new frmNhanVien();


            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Xem";
                    nhanVien.ShowDialog();
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhân Viên").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        timer1.Start();
                        nhanVien.ShowDialog();
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case 2:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhân Viên").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + NhanVienObj.Manv + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (NhanVienDAO.Instance.Xoa(NhanVienObj.Manv))
                            {
                                LichSuHoatDongDAO.Instance.ThongBao(3, NhanVienObj.Manv);
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
            int count = NhanVienDAO.Instance.CountDataNhanVien();
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
            int count = NhanVienDAO.Instance.CountDataNhanVien() / 10;
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
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("MANV", txtSearch.Text);
                    break;
                case 1:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("TENNV", txtSearch.Text);
                    break;
                case 2:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("GIOITINHNV", txtSearch.Text);
                    break;
                case 3:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("NGAYSINHNV", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"));
                    break;
                case 4:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("DIACHINV", txtSearch.Text);
                    break;
                case 5:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("SDTNV", txtSearch.Text);
                    break;
                case 6:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("PB.TENPB", txtSearch.Text);
                    break;
                case 7:
                    dt = NhanVienDAO.Instance.TimKiemTheoTen("CV.TENCV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                nhanvienList.DataSource = dt;
                dgvNhanVien.DataSource = nhanvienList;
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
                case 5:
                    index = 5;
                    break;
                case 6:
                    index = 6;
                    break;
                case 7:
                    index = 7;
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
            if (dgvNhanVien.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvNhanVien.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvNhanVien.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvNhanVien.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvNhanVien.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }
    }
}
