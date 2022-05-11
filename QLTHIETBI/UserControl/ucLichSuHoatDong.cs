using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucLichSuHoatDong : UserControl
    {
        BindingSource LSHDList = new BindingSource();
        private int index = 0;
        public ucLichSuHoatDong()
        {
            InitializeComponent();
            LoadData(1);
            LoadCombobox();
        }

        #region Phương thức
        void LoadData(int page)
        {
            dgvLSHD.DataSource = LSHDList;
            LSHDList.DataSource = LichSuHoatDongDAO.Instance.GetDataLichSuHoatDong(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Username");
            cbxSearch.Items.Add("Tên nhân viên");
            cbxSearch.Items.Add("Ngày");
            cbxSearch.Items.Add("Hoạt Động");
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

        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = LichSuHoatDongDAO.Instance.CountDataLichSuHoatDong();
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
            int count = LichSuHoatDongDAO.Instance.CountDataLichSuHoatDong() / 10;
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
                    dt = LichSuHoatDongDAO.Instance.TimKiemTheoTen("USERNAME", txtSearch.Text);
                    break;
                case 1:
                    dt = LichSuHoatDongDAO.Instance.TimKiemTheoTen("NV.TENNV", txtSearch.Text);
                    break;
                case 2:
                    dt = LichSuHoatDongDAO.Instance.TimKiemTheoTen("NGAYHD", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"));
                    break;
                case 3:
                    dt = LichSuHoatDongDAO.Instance.TimKiemTheoTen("HD.NOIDUNG_HD", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                LSHDList.DataSource = dt;
                dgvLSHD.DataSource = LSHDList;
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


        #endregion


    }
}
