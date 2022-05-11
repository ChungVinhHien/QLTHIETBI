using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucPhongThietBi : UserControl
    {
        private int index = 0;

        public ucPhongThietBi()
        {
            InitializeComponent();
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Thiết Bị").Rows[0][0].ToString() == "True")
            {
                LoadData(1);
                LoadCombobox();
            }
            else ThongBao.Show("Bạn không có quyền xem dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        #region Phương thức
        void LoadData(int page)
        {
            flowLayoutPanel.Controls.Clear();
            txtPage.Text = page.ToString();

            DataTable dt = PhongThietBiDAO.Instance.GetDataPhongThietBi(page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string maphong = dt.Rows[i][0].ToString();
                string trangthai = dt.Rows[i][5].ToString();


                Panel panel = new Panel()
                {
                    Width = 100,
                    Height = 100,
                };


                //if (ThietBiDAO.Instance.CheckThietBiPTB(maphong))
                //{
                //    panel.BackgroundImage = new Bitmap(Properties.Resources.phongthietbi);
                //    panel.BackgroundImageLayout = ImageLayout.Stretch;
                //}
                //else {
                //    panel.BackgroundImage = new Bitmap(Properties.Resources.phongtrong);
                //    panel.BackgroundImageLayout = ImageLayout.Stretch;
                //      }

                Label tittle = new Label()
                {
                    Text = maphong,
                    Width = 52,
                    Height = 13,
                    BackColor = Color.FromArgb(40, 96, 144),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Location = new Point(3, 4),
                };

                Panel footer = new Panel()
                {
                    Width = 100,
                    Height = 100,
                    Location = new Point(0, 0),
                    BackColor = Color.Transparent,
                    BackgroundImage = new Bitmap(Properties.Resources.fphong),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Visible = false,
                };



                Bunifu.UI.WinForms.BunifuImageButton imageButton1 = new Bunifu.UI.WinForms.BunifuImageButton()
                {
                    Width = 20,
                    Height = 20,
                    Location = new Point(13, 80),
                    Image = new Bitmap(Properties.Resources.xem_),
                    ImageActive = new Bitmap(Properties.Resources.xem),
                    ImageMargin = 1,
                    ToolTipText = "Xem",
                };
                Bunifu.UI.WinForms.BunifuImageButton imageButton2 = new Bunifu.UI.WinForms.BunifuImageButton()
                {
                    Width = 20,
                    Height = 20,
                    Location = new Point(40, 80),
                    Image = new Bitmap(Properties.Resources.sua_),
                    ImageActive = new Bitmap(Properties.Resources.sua),
                    ImageMargin = 1,
                    ToolTipText = "Sửa",
                };
                Bunifu.UI.WinForms.BunifuImageButton imageButton3 = new Bunifu.UI.WinForms.BunifuImageButton()
                {
                    Width = 20,
                    Height = 20,
                    Location = new Point(65, 80),
                    Image = new Bitmap(Properties.Resources.xoa_),
                    ImageActive = new Bitmap(Properties.Resources.xoa),
                    ImageMargin = 1,
                    ToolTipText = "Xóa",
                };

                imageButton1.Click += delegate (object sender, EventArgs e) { imageButton1_Click(sender, e, maphong); };
                imageButton2.Click += delegate (object sender, EventArgs e) { imageButton2_Click(sender, e, maphong); };
                imageButton3.Click += delegate (object sender, EventArgs e) { imageButton3_Click(sender, e, maphong); };

                panel.MouseHover += delegate (object sender, EventArgs e) { Panel_MouseHover(sender, e, footer); };
                footer.MouseLeave += delegate (object sender, EventArgs e) { Footer_MouseLeave(sender, e, footer); };


                footer.Controls.Add(imageButton1);
                footer.Controls.Add(imageButton2);
                footer.Controls.Add(imageButton3);

                panel.Controls.Add(tittle);
                panel.Controls.Add(footer);



                if (dt.Rows.Count > 0)
                    flowLayoutPanel.Controls.Add(panel);
                else
                    MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã phòng");
            cbxSearch.Items.Add("Tên phòng");
            cbxSearch.Items.Add("Vị trí");
            cbxSearch.Items.Add("Trạng thái");
            cbxSearch.Items.Add("Tên nhân viên");

        }
        #endregion

        #region Sự kiện CRUD
        private void imageButton1_Click(object sender, EventArgs e, string maphong)
        {
            PhongThietBiObj.Maptb = maphong;
            HoatDongObj.Noidung = "Xem";
            frmPhongThietBi frmPhongThietBi = new frmPhongThietBi();
            frmPhongThietBi.ShowDialog();
        }
        private void imageButton2_Click(object sender, EventArgs e, string maphong)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Thiết bị").Rows[0][2].ToString() == "True")
            {
                PhongThietBiObj.Maptb = maphong;
                HoatDongObj.Noidung = "Sửa";
                frmPhongThietBi phongThietBi = new frmPhongThietBi();
                phongThietBi.Width = 310;
                phongThietBi.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void imageButton3_Click(object sender, EventArgs e, string maphong)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Thiết Bị").Rows[0][3].ToString() == "True")
            {
                if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + maphong + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                {
                    if (PhongThietBiDAO.Instance.Xoa(maphong))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(3, maphong);
                        LoadData(Convert.ToInt32(txtPage.Text));
                        ThongBao.Show("Xóa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi xóa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                }
            }
            else ThongBao.Show("Bạn không có quyền xóa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void Panel_MouseHover(object sender, EventArgs e, Panel footer)
        {
            footer.Visible = true;
        }

        private void Footer_MouseLeave(object sender, EventArgs e, Panel footer)
        {
            footer.Visible = false;
        }

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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Thiết Bị").Rows[0][1].ToString() == "True")
            {
                frmPhongThietBi phongThietBi = new frmPhongThietBi();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                phongThietBi.Width = 310;
                phongThietBi.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        #endregion

        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = PhongThietBiDAO.Instance.CountDataPhongThietBi();
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
            int count = PhongThietBiDAO.Instance.CountDataPhongThietBi() / 10;
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
                    dt = PhongThietBiDAO.Instance.TimKiemTheoTen("MAPTB", txtSearch.Text);
                    break;
                case 1:
                    dt = PhongThietBiDAO.Instance.TimKiemTheoTen("TENPTB", txtSearch.Text);
                    break;
                case 2:
                    dt = PhongThietBiDAO.Instance.TimKiemTheoTen("VITRI", txtSearch.Text);
                    break;
                case 3:
                    dt = PhongThietBiDAO.Instance.TimKiemTheoTen("TRANGTHAIPTB", txtSearch.Text);
                    break;
                case 4:
                    dt = PhongThietBiDAO.Instance.TimKiemTheoTen("NV.TENNV", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                flowLayoutPanel.Controls.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string maphong = dt.Rows[i][0].ToString();
                    string trangthai = dt.Rows[i][5].ToString();

                    Panel panel = new Panel()
                    {
                        Width = 100,
                        Height = 100,
                    };


                    if (trangthai == "Đang hoạt động")
                    {
                        panel.BackgroundImage = new Bitmap(Properties.Resources.phongthietbi);
                        panel.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        panel.BackgroundImage = new Bitmap(Properties.Resources.phongtrong);
                        panel.BackgroundImageLayout = ImageLayout.Stretch;
                    }

                    Label tittle = new Label()
                    {
                        Text = maphong,
                        Width = 52,
                        Height = 13,
                        BackColor = Color.FromArgb(40, 96, 144),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 8, FontStyle.Regular),
                        Location = new Point(3, 4),
                    };

                    Panel footer = new Panel()
                    {
                        Width = 100,
                        Height = 100,
                        Location = new Point(0, 0),
                        BackColor = Color.Transparent,
                        BackgroundImage = new Bitmap(Properties.Resources.fphong),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Visible = false,
                    };



                    Bunifu.UI.WinForms.BunifuImageButton imageButton1 = new Bunifu.UI.WinForms.BunifuImageButton()
                    {
                        Width = 20,
                        Height = 20,
                        Location = new Point(13, 80),
                        Image = new Bitmap(Properties.Resources.xem_),
                        ImageActive = new Bitmap(Properties.Resources.xem),
                        ImageMargin = 1,
                        ToolTipText = "Xem",
                    };
                    Bunifu.UI.WinForms.BunifuImageButton imageButton2 = new Bunifu.UI.WinForms.BunifuImageButton()
                    {
                        Width = 20,
                        Height = 20,
                        Location = new Point(40, 80),
                        Image = new Bitmap(Properties.Resources.sua_),
                        ImageActive = new Bitmap(Properties.Resources.sua),
                        ImageMargin = 1,
                        ToolTipText = "Sửa",
                    };
                    Bunifu.UI.WinForms.BunifuImageButton imageButton3 = new Bunifu.UI.WinForms.BunifuImageButton()
                    {
                        Width = 20,
                        Height = 20,
                        Location = new Point(65, 80),
                        Image = new Bitmap(Properties.Resources.xoa_),
                        ImageActive = new Bitmap(Properties.Resources.xoa),
                        ImageMargin = 1,
                        ToolTipText = "Xóa",
                    };

                    imageButton1.Click += delegate (object s, EventArgs ev) { imageButton1_Click(sender, e, maphong); };
                    imageButton2.Click += delegate (object s, EventArgs ev) { imageButton2_Click(sender, e, maphong); };
                    imageButton3.Click += delegate (object s, EventArgs ev) { imageButton3_Click(sender, e, maphong); };

                    panel.MouseHover += delegate (object s, EventArgs ev) { Panel_MouseHover(sender, e, footer); };
                    footer.MouseLeave += delegate (object s, EventArgs ev) { Footer_MouseLeave(sender, e, footer); };


                    footer.Controls.Add(imageButton1);
                    footer.Controls.Add(imageButton2);
                    footer.Controls.Add(imageButton3);

                    panel.Controls.Add(tittle);
                    panel.Controls.Add(footer);

                    flowLayoutPanel.Controls.Add(panel);
                }

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
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TrangThaiObj.Trangthai == "close")
            {
                if (HoatDongObj.Noidung == "Sửa")
                    LoadData(Convert.ToInt32(txtPage.Text));
                timer1.Stop();
            }
        }





    }
}
