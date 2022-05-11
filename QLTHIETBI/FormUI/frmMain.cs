using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmMain : Form
    {
        Image closeImageAct, closeImage, closeTab;
        PictureBox pb;
        Point relativeClickedPosition;
        Point screenClickedPosition;
        Point point;
        int index;

        public frmMain()
        {
            InitializeComponent();
            if (TaikhoanObj.Is_admin == "True")
            {
                btnAdmin.Visible = true;
            }
            AddTabPage(new ucTrangChu());
            timer2.Start();
            timer3.Start();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            TrangThaiObj.Trangthai = "1";

            toolTip1.SetToolTip(btnUser, TaikhoanObj.Username);

            Size mysize = new System.Drawing.Size(20, 20);
            Bitmap bt1 = new Bitmap(Properties.Resources.close_tab);
            Bitmap btm1 = new Bitmap(bt1, mysize);
            closeImageAct = btm1;

            Bitmap bt2 = new Bitmap(Properties.Resources.tab_close);
            Bitmap btm2 = new Bitmap(bt2, mysize);
            closeImage = btm2;

            Bitmap bt3 = new Bitmap(Properties.Resources.closetab);
            Bitmap btm3 = new Bitmap(bt3, mysize);
            closeTab = btm3;

        }


        #region Method
        public void HideSubMenu()
        {
            if (pnlAssetSubMenu.Visible == true)
                pnlAssetSubMenu.Visible = false;
            if (pnlShoppingSubMenu.Visible == true)
                pnlShoppingSubMenu.Visible = false;
            if (pnlCategorySubMenu.Visible == true)
                pnlCategorySubMenu.Visible = false;
            if (pnlContractSubMenu.Visible == true)
                pnlContractSubMenu.Visible = false;
            if (pnlEmployeeSubMenu.Visible == true)
                pnlEmployeeSubMenu.Visible = false;
            if (pnlOfficeSubMenu.Visible == true)
                pnlOfficeSubMenu.Visible = false;
            if (pnlReportSubMenu.Visible == true)
                pnlReportSubMenu.Visible = false;
        }

        public void ShowSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                HideSubMenu();
                submenu.Visible = true;
            }
            else submenu.Visible = false;
        }
        private int KTFormTonTai(UserControl uc)
        {
            for (int i = 0; i < tabControl.TabCount; i++)
                if (tabControl.TabPages[i].Text == uc.Tag.ToString())
                    return i;
            return -1;
        }

        private void AddTabPage(UserControl uc)
        {

            if (picEmpty.Visible == true)
                picEmpty.Visible = false;

            int t = KTFormTonTai(uc);
            if (t >= 0)
            {
                if (tabControl.SelectedTab != tabControl.TabPages[t])
                    tabControl.SelectedTab = tabControl.TabPages[t];
            }
            else
            {
                TabPage newTab = new TabPage(uc.Tag.ToString());
                tabControl.TabPages.Add(newTab);
                newTab.ImageIndex = 0;
                uc.Parent = newTab;
                uc.Show();
                uc.Dock = DockStyle.Fill;
                tabControl.SelectedTab = tabControl.TabPages[tabControl.TabCount - 1];
            }
        }

        void Blur()
        {
            Bitmap bmp = Screenshot.TakeSnapshot(pnlSideMain);
            if (pb != null)
            {
                this.Controls.Remove(pb);
            }
            pb = new PictureBox();
            this.Controls.Add(pb);
            pb.Image = BitmapFilter.AdjustBrightness(bmp, 0.75f); ;
            pb.Dock = DockStyle.Fill;
            pb.BringToFront();
        }
        #endregion


        #region Event TabControl
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {

            Rectangle rect = tabControl.GetTabRect(e.Index);
            Rectangle imageRec = new Rectangle(rect.Right - closeImageAct.Width,
                rect.Top + (rect.Height - closeImageAct.Height) / 2,
              closeImageAct.Width, closeImageAct.Height);

            Brush brush = new SolidBrush(Color.FromArgb(40, 96, 144));
            Font font = new Font("Segoe UI", 9, FontStyle.Regular);
            Brush br1 = Brushes.White;
            Brush br2 = Brushes.Black;
            StringFormat strF = new StringFormat(StringFormat.GenericDefault);



            if (tabControl.SelectedTab == tabControl.TabPages[e.Index])
            {
                e.DrawBackground();
                Graphics g = e.Graphics;
                g.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawImage(closeImageAct, imageRec);
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, font, br1, rect, strF);
            }
            else
            {
                e.DrawBackground();
                Graphics g = e.Graphics;
                g.FillRectangle(Brushes.White, rect);
                //e.Graphics.DrawImage(closeImage, imageRec);
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, font, br2, rect, strF);
            }
        }
        private void tabControl_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnMouseClick(e);
            index = tabControl.SelectedIndex;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip CMS = new ContextMenuStrip();
                CMS.Items.Add("Close", closeTab, new EventHandler(Close_Clicked));
                CMS.Items.Add("Close All", null, new EventHandler(CloseAll_Clicked));
                CMS.BackColor = Color.White;
                CMS.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                CMS.RenderMode = ToolStripRenderMode.System;
                tabControl.ContextMenuStrip = CMS;
            }
            else
            {
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    Rectangle rect = tabControl.GetTabRect(i);
                    Rectangle imageRec = new Rectangle(rect.Right - closeImageAct.Width,
                        rect.Top + (rect.Height - closeImageAct.Height) / 2,
                        closeImageAct.Width, closeImageAct.Height);

                    if (imageRec.Contains(e.Location))
                    {
                        tabControl.TabPages.Remove(tabControl.SelectedTab);
                        if (index > 1) this.tabControl.SelectedTab = tabControl.TabPages[index - 1];
                    }


                    if (tabControl.TabPages.Count == 0)
                    {
                        picEmpty.Visible = true;
                    }
                }
            }

        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            index = tabControl.SelectedIndex;

            tabControl.TabPages.RemoveAt(index);
            if (index > 1) this.tabControl.SelectedTab = tabControl.TabPages[index - 1];
            TrangThaiObj.Trangthai = "stop";

            if (tabControl.TabPages.Count == 0)
            {
                picEmpty.Visible = true;
            }
        }
        private void CloseAll_Clicked(object sender, EventArgs e)
        {
            tabControl.TabPages.Clear();
            picEmpty.Visible = true;
        }
        #endregion


        #region Event Menu
        private void btnHome_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            AddTabPage(new ucTrangChu());
            timer2.Start();
        }
        private void btnTrangchu_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            AddTabPage(new ucTrangChu());
            timer2.Start();
        }
        private void btnAsset_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlAssetSubMenu);
        }

        private void btnThietbi_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucThietBi());
                timer1.Start();
            }
        }

        private void btnTheTS_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thẻ Tài Sản").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucTheTaiSan());
                timer1.Start();
            }
        }
        private void btnKHMS_Click(object sender, EventArgs e)
        {
            if (TaikhoanObj.Is_admin == "True")
            {
                if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Kế Hoạch Mua Sắm").Rows[0][0].ToString() == "False")
                {
                    ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                }
                else
                {
                    AddTabPage(new ucKeHoachMS());
                    timer1.Start();
                }
            }
            else ShowSubMenu(pnlShoppingSubMenu);
        }
        private void btnDXMS_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đề Xuất Mua Sắm").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucDeXuatMuaSam());
                timer1.Start();
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlCategorySubMenu);
        }

        private void btnNhomTS_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhóm Tài Sản").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucNhomTaiSan());
        }

        private void btnLoaiTS_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Loại Tài Sản").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucLoaiTaiSan());
        }

        private void btnTrangthai_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Trạng Thái").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucTrangThai());
        }

        private void btnDVT_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đơn Vị Tính").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucDonViTinh());
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            AddTabPage(new ucPhongThietBi());
            timer1.Start();
        }

        private void btnOffice_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlOfficeSubMenu);
        }

        private void btnDonvi_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Đơn Vị").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucDonVi());
        }

        private void btnPhongban_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phòng Ban").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucPhongBan());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlEmployeeSubMenu);
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhân Viên").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucNhanVien());
                timer1.Start();
            }
        }

        private void btnChucvu_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Chức Vụ").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else AddTabPage(new ucChucVu());
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlContractSubMenu);
        }


        private void btnPhieuKH_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Khấu Hao").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuKhauHao());
                timer1.Start();
            }
        }

        private void btnPhieuSC_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Sửa Chữa").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuSuaChua());
                timer1.Start();
            }
        }

        private void btnPhieuTL_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuThanhLy());
                timer1.Start();
            }
        }

        private void btnPhieuKK_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuKiemKe());
                timer1.Start();
            }
        }


        private void btnPhieuLC_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Luân Chuyển").Rows[0][0].ToString() == "False")
            {
                btnPhieuLC.Enabled = false;
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuLuanChuyen());
                timer1.Start();
            }
        }
        private void btnPhieuBT_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Bảo Trì").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                AddTabPage(new ucPhieuBaoTri());
                timer1.Start();
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhà Cung Cấp").Rows[0][0].ToString() == "False")
            {
                ThongBao.Show("Bạn không có quyền xem danh mục này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
            else
            {
                HideSubMenu();
                AddTabPage(new ucNhaCungCap());
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlReportSubMenu);

        }
        private void btnTKNCC_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Nhà Cung Cấp").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "NCC";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnTKTSActive_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "ThietBiHD";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnTKTSHuHong_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "ThietBiHH";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnTKTSMat_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "ThietBiBM";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnTKTSThanhLy_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "ThietBiTL";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }


        private void btnBCKHThang_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Khấu Hao").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "KHThang";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnBCKHNam_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Khấu Hao").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "KHNam";
                frmPrint bckhnam = new frmPrint();
                bckhnam.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }


        private void btnAdmin_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                contextMenu.Items.Add("Tài Khoản");
                contextMenu.Items.Add("Nhật Ký Hoạt Động");
                contextMenu.Items.Add("Cài Đặt");
                contextMenu.BackColor = Color.White;
                contextMenu.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                contextMenu.RenderMode = ToolStripRenderMode.System;
                contextMenu.ShowImageMargin = false;
                btnAdmin.ContextMenuStrip = contextMenu;
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                var relativeClickedPosition = e.Location;
                relativeClickedPosition.X = 5;
                relativeClickedPosition.Y = relativeClickedPosition.Y - 80;
                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenu.Show(screenClickedPosition);
            }
        }
        private void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Tài Khoản":
                    AddTabPage(new ucTaiKhoan());
                    timer1.Start();
                    break;
                case "Nhật Ký Hoạt Động":
                    AddTabPage(new ucLichSuHoatDong());
                    timer1.Start();
                    break;
            }
        }

        private void btnThongbao_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                btnThongbao.Image = Properties.Resources.thongbao;
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                if (LichSuHoatDongDAO.Instance.GetIsRead(TaikhoanObj.Username, "0") == false)
                {
                    contextMenu.Items.Add("Không có thông báo mới nào!");
                }
                else
                {
                    DataTable dt = LichSuHoatDongDAO.Instance.GetThongBaoMoi(TaikhoanObj.Username);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        contextMenu.Items.Add(dt.Rows[i][0].ToString());
                        string s = dt.Rows[i][0].ToString();
                        DateTime ngayhd = DateTime.Parse(s.Substring(0, 10));
                        string thoigian = s.Substring(11, 8);
                        LichSuHoatDongDAO.Instance.Update(TaikhoanObj.Username, ngayhd.ToString("MM/dd/yyyy"), thoigian);
                    }
                }

                contextMenu.BackColor = Color.White;
                contextMenu.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                contextMenu.RenderMode = ToolStripRenderMode.System;
                contextMenu.ShowImageMargin = true;
                contextMenu.Enabled = true;
                point = e.Location;
                relativeClickedPosition = e.Location;
                relativeClickedPosition.X = relativeClickedPosition.X - 100;
                relativeClickedPosition.Y = 30;
                screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenu.Show(screenClickedPosition);
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            TrangThaiObj.Trangthai = "0";
            TaikhoanDAO.Instance.UpdateIsActive(TaikhoanObj.Username, "0");
            TaikhoanObj.Username = null;
            Application.Restart();
        }

        #endregion

        #region Event App
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (ThongBao.Show("Bạn có chắc chắn muốn đóng phần mềm không?", "Thoát?", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
            {
                TaikhoanDAO.Instance.UpdateIsActive(TaikhoanObj.Username, "0");
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count == 0)
            {
                timer1.Stop();
            }
            else
            {
                switch (TrangThaiObj.Trangthai)
                {
                    case "open":
                        Blur();
                        TrangThaiObj.Trangthai = "";
                        break;
                    case "close":
                        this.Controls.Remove(pb);
                        if (pb != null)
                            pb.SendToBack();
                        TrangThaiObj.Trangthai = "";
                        break;
                }
            }

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count == 0)
            {
                timer2.Stop();
            }
            else
            {
                switch (HoatDongObj.Noidung)
                {
                    case "ShowTB":
                        AddTabPage(new ucThietBi());
                        timer1.Start();
                        HoatDongObj.Noidung = "";
                        break;
                    case "ShowKH":
                        AddTabPage(new ucKeHoachMS());
                        timer1.Start();
                        HoatDongObj.Noidung = "";
                        break;
                    case "ShowDX":
                        AddTabPage(new ucDeXuatMuaSam());
                        timer1.Start();
                        HoatDongObj.Noidung = "";
                        break;
                    case "ShowNCC":
                        AddTabPage(new ucNhaCungCap());
                        HoatDongObj.Noidung = "";
                        break;
                    case "ShowNV":
                        AddTabPage(new ucNhanVien());
                        timer1.Start();
                        HoatDongObj.Noidung = "";
                        break;
                    case "Duyệt":
                        AddTabPage(new ucDeXuatMuaSam());
                        timer1.Start();
                        HoatDongObj.Noidung = "";
                        break;
                }
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (TrangThaiObj.Trangthai == "Thông Báo")
            {
                btnThongbao.Image = Properties.Resources.hasNotice;
            }
            if (LichSuHoatDongDAO.Instance.GetIsRead(TaikhoanObj.Username, "0"))
            {
                btnThongbao.Image = Properties.Resources.hasNotice;
            }
            else btnThongbao.Image = Properties.Resources.thongbao;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            frmThongTin nhanVien = new frmThongTin();
            nhanVien.ShowDialog();
            timer1.Start();
        }

        private void btnThongtin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("baocao.pdf");
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

        }

        private void btnThongbao_Click(object sender, EventArgs e)
        {

        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.MaximumSize = this.Size;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        #endregion

    }
}
