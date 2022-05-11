using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmThongTin : Form
    {
        public frmThongTin()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
            DataTable dt = new DataTable();
            dt = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username);
            if (dt != null && dt.Rows.Count > 0)
            {
                manv.Text = dt.Rows[0][0].ToString();
                tennv.Text = dt.Rows[0][1].ToString();
                gioitinh.Text = dt.Rows[0][2].ToString();
                ngaysinh.Text = dt.Rows[0][3].ToString();
                diachi.Text = dt.Rows[0][4].ToString();
                sdt.Text = dt.Rows[0][5].ToString();
                email.Text = dt.Rows[0][6].ToString();
                phongban.Text = dt.Rows[0][7].ToString();
                chucvu.Text = dt.Rows[0][8].ToString();
                if (File.Exists(dt.Rows[0][9].ToString()))
                    picUser.Image = Image.FromFile(dt.Rows[0][9].ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }

        private void btnChangePW_Click(object sender, EventArgs e)
        {
            frmChangePw changePw = new frmChangePw();
            changePw.ShowDialog();
        }
    }
}
