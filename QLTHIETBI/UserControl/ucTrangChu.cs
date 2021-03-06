using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucTrangChu : UserControl
    {
        private int count = 0;
        public ucTrangChu()
        {
            InitializeComponent();
        }
        private void TinhPhanTram(int tongsl, int count, Bunifu.UI.WinForms.BunifuCircleProgress progress, Bunifu.UI.WinForms.BunifuPanel col, Label label)
        {
            if (count > 0)
            {
                float sl = ((float)count / (float)tongsl) * 100;
                string kq = Math.Round(sl, 1).ToString();
                progress.ValueByTransition = Convert.ToInt32(kq.Substring(0, kq.Length - 2));
                progress.SubScriptText = kq.Substring(kq.Length - 2, 2);

                col.Size = new Size(30, (int)sl *2);
                col.Location = new Point(col.Location.X, (200 - ((int)sl *2)) + col.Location.Y);
                label.Text = kq.ToString();
            }
            else
            {
                progress.ValueByTransition = 0;
                progress.SubScriptText = ",0";
                col.Size = new Size(30,2);
                col.Location = new Point(col.Location.X, 198 + col.Location.Y);
                label.Text = "0";

            }
            
        }
        private void ucTrangChu_Load(object sender, EventArgs e)
        {
            lblSoLuongTB.Text = ThietBiDAO.Instance.CountDataThietBi().ToString();
            if (TaikhoanObj.Is_admin == "True")
            {
                lblSoLuongKH.Text = KeHoachMuaSamDAO.Instance.CountData().ToString();
            }
            else
            {
                string nguoidx = NhanVienDAO.Instance.GetInfoNhanVien(TaikhoanObj.Username).Rows[0][1].ToString();
                lblSoLuongKH.Text = DeXuatMuaSamDAO.Instance.CountData(nguoidx).ToString();
            }
            lblSoLuongNCC.Text = NhaCungCapDAO.Instance.CountDataNhaCungCap().ToString();
            lblSoLuongNV.Text = NhanVienDAO.Instance.CountDataNhanVien().ToString();

            int tongsl = ThietBiDAO.Instance.CountDataThietBi();

            TinhPhanTram(tongsl, ThietBiDAO.Instance.CountTBDangHoatDong(), crlHD, colHD,lblHD);
            TinhPhanTram(tongsl, ThietBiDAO.Instance.CountTBDaThanhLy(), crlTL,colTL,lblTL);
            TinhPhanTram(tongsl, ThietBiDAO.Instance.CountTBBiHuHong(), crlHH,colHH,lblHH);
            TinhPhanTram(tongsl, ThietBiDAO.Instance.CountTBBiMat(), crlBM,colBM,lblBM);

            

            count = NhanVienDAO.Instance.CountNhanVienDangHoatDong();
            LoadNhanVien();
            timer1.Start();
        }

        private void linkShowTB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HoatDongObj.Noidung = "ShowTB";
        }

        private void linkShowKH_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TaikhoanObj.Is_admin == "True")
                HoatDongObj.Noidung = "ShowKH";
            else HoatDongObj.Noidung = "ShowDX";
        }

        private void linkShowNCC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HoatDongObj.Noidung = "ShowNCC";
        }

        private void linkShowNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HoatDongObj.Noidung = "ShowNV";
        }

        void LoadNhanVien()
        {
            flowLayoutPanel.Controls.Clear();

            DataTable dt = NhanVienDAO.Instance.GetNhanVienDangHoatDong();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tennv = dt.Rows[i][0].ToString();
                string email = dt.Rows[i][1].ToString();


                Panel panel = new Panel()
                {
                    Width = 307,
                    Height = 60,
                };
                Bunifu.UI.WinForms.BunifuImageButton pic = new Bunifu.UI.WinForms.BunifuImageButton()
                {
                    Width = 45,
                    Height = 45,
                    ImageMargin = 5,
                    Image = new Bitmap(Properties.Resources.user),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Location = new Point(20, 5),
                };
                Label tittle = new Label()
                {
                    Text = tennv,
                    Width = 200,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                    Location = new Point(70, 10),
                };


                Label footer = new Label()
                {
                    Text = email,
                    Width = 200,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Location = new Point(70, 25),
                };
                PictureBox sep = new PictureBox()
                {
                    Width = 300,
                    Height = 1,
                    BackColor = Color.Silver,
                    Location = new Point(0, 59),
                };
                panel.Controls.Add(sep);
                panel.Controls.Add(footer);
                panel.Controls.Add(tittle);
                panel.Controls.Add(pic);



                if (dt.Rows.Count > 0)
                    flowLayoutPanel.Controls.Add(panel);
                else
                    MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int dem = NhanVienDAO.Instance.CountNhanVienDangHoatDong();
            if (dem != count)
            {
                count = dem;
                LoadNhanVien();

            }

        }

        private void linkShowTB_MouseHover(object sender, EventArgs e)
        {
            linkShowTB.LinkColor = Color.Red;
        }

        private void linkShowTB_MouseLeave(object sender, EventArgs e)
        {
            linkShowTB.LinkColor = Color.Gray;
        }

        private void linkShowKH_MouseHover(object sender, EventArgs e)
        {
            linkShowKH.LinkColor = Color.FromArgb(255, 152, 1);
        }

        private void linkShowKH_MouseLeave(object sender, EventArgs e)
        {
            linkShowKH.LinkColor = Color.Gray;
        }

        private void linkShowNCC_MouseHover(object sender, EventArgs e)
        {
            linkShowNCC.LinkColor = Color.FromArgb(40, 96, 144);
        }

        private void linkShowNCC_MouseLeave(object sender, EventArgs e)
        {
            linkShowNCC.LinkColor = Color.Gray;
        }

        private void linkShowNV_MouseHover(object sender, EventArgs e)
        {
            linkShowNV.LinkColor = Color.FromArgb(77, 177, 81);
        }

        private void linkShowNV_MouseLeave(object sender, EventArgs e)
        {
            linkShowNV.LinkColor = Color.Gray;
        }

       
    
    }
}
