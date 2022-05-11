using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucPhieuKhauHao : UserControl
    {
        BindingSource phieuKHList = new BindingSource();
        private int index = 0;
        public ucPhieuKhauHao()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvPhieuKhauHao.DataSource = phieuKHList;
            phieuKHList.DataSource = PhieuKhauHaoDAO.Instance.GetDataPhieuKhauHao(1);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã phiếu");
            cbxSearch.Items.Add("Mã thiết bị");
            cbxSearch.Items.Add("Tên thiết bị");
            cbxSearch.Items.Add("Tên nhân viên");

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

        private void dgvPhieuKhauHao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                PhieuKhauHaoObj.Mapkh = dgvPhieuKhauHao[2, e.RowIndex].Value.ToString();
                PhieuKhauHaoObj.Matb = dgvPhieuKhauHao[3, e.RowIndex].Value.ToString();
            }
            frmPhieuKhauHao phieuKhauHao = new frmPhieuKhauHao();

            switch (e.ColumnIndex)
            {
                case 0:
                    HoatDongObj.Noidung = "Xem";
                    phieuKhauHao.ShowDialog();
                    break;
                case 1:
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Khấu Hao").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + TheTaiSanObj.Matts + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (PhieuKhauHaoDAO.Instance.Xoa(PhieuKhauHaoObj.Mapkh))
                            {
                                LichSuHoatDongDAO.Instance.ThongBao(3, PhieuKhauHaoObj.Mapkh);
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
            int count = PhieuKhauHaoDAO.Instance.CountDataPhieuKhauHao();
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
            int count = PhieuKhauHaoDAO.Instance.CountDataPhieuKhauHao() / 10;
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
            DataTable dt = null;
            switch (index)
            {
                case 0:
                    dt = PhieuKhauHaoDAO.Instance.TimKiemTheoTen("PKH.MAPKH", txtSearch.Text);
                    break;
                case 1:
                    dt = PhieuKhauHaoDAO.Instance.TimKiemTheoTen("CT.MATB", txtSearch.Text);
                    break;
                case 2:
                    dt = PhieuKhauHaoDAO.Instance.TimKiemTheoTen("TB.TENTB", txtSearch.Text);
                    break;
                case 3:
                    dt = PhieuKhauHaoDAO.Instance.TimKiemTheoTen("NV.TENNV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                phieuKHList.DataSource = dt;
                dgvPhieuKhauHao.DataSource = phieuKHList;
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
            if (dgvPhieuKhauHao.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvPhieuKhauHao.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvPhieuKhauHao.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvPhieuKhauHao.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvPhieuKhauHao.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvPhieuKhauHao.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }
        #endregion

    }
}
