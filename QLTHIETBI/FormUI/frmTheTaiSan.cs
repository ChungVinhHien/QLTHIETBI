using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmTheTaiSan : Form
    {
        BindingSource thetaisanList = new BindingSource();
        public frmTheTaiSan()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
            LoadData();
        }
        #region Phương thức

        void LoadData()
        {
            thetaisanList.DataSource = TheTaiSanDAO.Instance.GetDataCTTheTaiSan(TheTaiSanObj.Matts);
            dgvCTTheTaiSan.DataSource = thetaisanList;
        }

        #endregion

        private void dgvCTTheTaiSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.ColumnIndex==0)
            //{
            //    string matts = dgvCTTheTaiSan[1, e.RowIndex].Value.ToString();
            //    string matb = dgvCTTheTaiSan[2, e.RowIndex].Value.ToString();
            //    string ngay = dgvCTTheTaiSan[3, e.RowIndex].Value.ToString();

            //    if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu "+matts+" không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
            //    {
            //        if (TheTaiSanDAO.Instance.XoaCTThe(matts,matb, ngay))
            //        {
            //            LoadData();
            //            ThongBao.Show("Xóa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            //        }
            //        else
            //        {
            //            ThongBao.Show("Có lỗi khi xóa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            //        }
            //    }
            //}
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thẻ Tài Sản").Rows[0][4].ToString() == "True")
            {
                frmPrint print = new frmPrint();
                HoatDongObj.Noidung = "TTS";
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }
    }
}
