using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using QRCoder;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ucThietBi : UserControl
    {
        BindingSource thietbiList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        private int index = 0;
        string Hinh1 = "";
        public ucThietBi()
        {
            InitializeComponent();
            if (ThietBiDAO.Instance.CheckPhuongPhap() == 3)
            {
                btnSanLuong.Visible = true;
            }
            LoadData(1);
            LoadCombobox();
        }
        #region Phương thức
        void LoadData(int page)
        {
            dgvThietBi.DataSource = thietbiList;
            thietbiList.DataSource = ThietBiDAO.Instance.GetDataThietBi(page);
            txtPage.Text = page.ToString();
        }
        void LoadCombobox()
        {
            cbxSearch.Items.Add("Mã thiết bị");
            cbxSearch.Items.Add("Tên thiết bị");
            cbxSearch.Items.Add("Ngày sử dụng");
            cbxSearch.Items.Add("Đơn Vị");
            cbxSearch.Items.Add("Phòng Ban");
            cbxSearch.Items.Add("Nhà cung cấp");
            cbxSearch.Items.Add("Loại tài sản");
            cbxSearch.Items.Add("Trạng thái");

        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        void CreateQRCode(string matb)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCode qrCode = new QRCode(qrGenerator.CreateQrCode(matb, QRCodeGenerator.ECCLevel.H));
            Image image = qrCode.GetGraphic(10, Color.Black, Color.White, false);
            byte[] array = new MyFuntions().imgToByteArray(image);
            Hinh1 = Convert.ToBase64String(array);

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
        private void btnSanLuong_Click(object sender, EventArgs e)
        {
            frmSanLuong sanluong = new frmSanLuong();
            TrangThaiObj.Trangthai = "open";
            timer1.Start();
            sanluong.ShowDialog();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][1].ToString() == "True")
            {
                frmThietBi thietBi = new frmThietBi();
                TrangThaiObj.Trangthai = "open";
                HoatDongObj.Noidung = "Thêm";
                timer1.Start();
                thietBi.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void btnNhapDL_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension 
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt); //read excel file  

                        for (int i = 1; i < dtExcel.Rows.Count; i++)
                        {
                            if (!ThietBiDAO.Instance.CheckExistThietBi(dtExcel.Rows[i][0].ToString()))
                            {
                                string matb = dtExcel.Rows[i][0].ToString();
                                string tentb = dtExcel.Rows[i][1].ToString();
                                string nguyengia = funtions.RemoveChars(dtExcel.Rows[i][2].ToString());
                                string nuocsx = dtExcel.Rows[i][3].ToString();
                                string namsx = dtExcel.Rows[i][4].ToString();
                                string tgbaohanh = (DateTime.ParseExact(dtExcel.Rows[i][5].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("MM/dd/yyyy");
                                string tgduavaosd = DateTime.ParseExact(dtExcel.Rows[i][6].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                                string ngaynhap = DateTime.ParseExact(dtExcel.Rows[i][7].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                                string tgtrichkh = dtExcel.Rows[i][8].ToString();
                                string thongsokt = dtExcel.Rows[i][9].ToString();
                                string mancc = NhaCungCapDAO.Instance.GetDataNCCByTenNCC(dtExcel.Rows[i][10].ToString()).Rows[0][0].ToString();
                                string madvt = DonViTinhDAO.Instance.GetDataDVTByTen(dtExcel.Rows[i][11].ToString()).Rows[0][0].ToString();
                                string madv = DonViDAO.Instance.GetDataDVByTenDV(dtExcel.Rows[i][12].ToString()).Rows[0][0].ToString();
                                string mapb = PhongBanDAO.Instance.GetDataPBByTenPB(dtExcel.Rows[i][13].ToString()).Rows[0][0].ToString();
                                string maloaits = LoaiTaiSanDAO.Instance.GetMaLoaiByTenLoaiTS(dtExcel.Rows[i][14].ToString()).Rows[0][0].ToString();
                                string matt = TrangThaiDAO.Instance.GetDataByTen(dtExcel.Rows[i][15].ToString()).Rows[0][0].ToString();

                                if (ThietBiDAO.Instance.Them(matb, tentb, nuocsx, namsx, tgbaohanh, tgduavaosd, tgtrichkh, thongsokt, nguyengia, ngaynhap,
                                    mancc, madvt, mapb, maloaits, matt, madv))
                                {
                                    CreateQRCode(matb);
                                    ThietBiDAO.Instance.InsertAnhQRCode(matb, Hinh1, "", "", "");
                                    LoadData(Convert.ToInt32(txtPage.Text));
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
        }

        private void btnKhauHao_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < dgvThietBi.RowCount; i++)
            {
                string matb = dgvThietBi[3, i].Value.ToString();

                string today = DateTime.Now.ToString("MM/dd/yyyy");
                string ghichu = "Khấu hao tháng " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                string sonamkh = PhieuKhauHaoDAO.Instance.GetTGTrichKhauHao(matb).Rows[0][0].ToString();
                string sothangkh = (Int32.Parse(sonamkh) * 12).ToString();

                if (PhieuKhauHaoDAO.Instance.CheckPhieuKhauHao(matb) == true)
                {
                    string mapkh = PhieuKhauHaoDAO.Instance.GetMaPhieuKHyMaTB(matb).Rows[0][0].ToString();
                    if (PhieuKhauHaoDAO.Instance.CheckKhauHaoTB(matb) == true)
                    {
                        if (PhieuKhauHaoDAO.Instance.ThemChiTiet(mapkh, matb, today, sonamkh, sothangkh, ghichu)
                            && PhieuKhauHaoDAO.Instance.TrichKhauHao(mapkh, matb, today))
                        {
                            LichSuHoatDongDAO.Instance.ThongBao(1, mapkh);
                            //ThongBao.Show("Có lỗi khi khấu hao thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                    }

                }
                else
                {
                    string mapkh = funtions.SDienMaTuDong("PKH");
                    string manv = TaikhoanObj.Username;


                    if (PhieuKhauHaoDAO.Instance.Them(mapkh, matb, today, manv, sonamkh, sothangkh, ghichu)
                        && PhieuKhauHaoDAO.Instance.TrichKhauHao(mapkh, matb, today))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(1, mapkh);
                    }
                }
            }
            if (i == dgvThietBi.RowCount)
                ThongBao.Show("Khấu hao của thiết bị thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            else ThongBao.Show("Có lỗi khi khấu hao thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }

        private void btnHaoMon_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < dgvThietBi.RowCount; i++)
            {
                if (TheTaiSanDAO.Instance.CheckTheTaiSan(ThietBiObj.Matb) == true)
                {
                    string matb = dgvThietBi[3, i].Value.ToString();
                    string matts = TheTaiSanDAO.Instance.GetMaTheTSByMaTB(matb).Rows[0][0].ToString();
                    string ngay = DateTime.Now.ToString("MM/dd/yyyy");
                    string diengiai = "Tính hao mòn năm " + DateTime.Now.Year.ToString();
                    if (TheTaiSanDAO.Instance.CheckHaoMonTB(matb) == true)
                    {
                        if (TheTaiSanDAO.Instance.ThemChiTiet(matts, matb, ngay, diengiai) != true && TheTaiSanDAO.Instance.TinhHaoMon(matts, matb, ngay) != true)
                        {
                            ThongBao.Show("Có lỗi khi tính hao mòn thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                    }
                }
            }
            if (i == dgvThietBi.RowCount)
                ThongBao.Show("Tính hao mòn thiết bị thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            else ThongBao.Show("Có lỗi khi tính hao mòn thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DonViObj.Madv))
            {
                HoatDongObj.Noidung = "PHIEUDANNHAN";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
        }

        private void dgvThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) ;
            else
            {
                DataTable dt = ThietBiDAO.Instance.GetDataByMaTB(dgvThietBi[3, e.RowIndex].Value.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    ThietBiObj.Matb = dgvThietBi[3, e.RowIndex].Value.ToString();
                    ThietBiObj.Tentb = dt.Rows[0][1].ToString();
                    ThietBiObj.Nuocsx = dt.Rows[0][2].ToString();
                    ThietBiObj.Namsx = dt.Rows[0][3].ToString();
                    ThietBiObj.Tgbaohanh = dt.Rows[0][4].ToString();
                    ThietBiObj.Tgduavaosd = dt.Rows[0][5].ToString();
                    ThietBiObj.Sonamkhauhao = dt.Rows[0][6].ToString();
                    ThietBiObj.Thongsokythuat = dt.Rows[0][7].ToString();
                    ThietBiObj.Nguyengia = dt.Rows[0][8].ToString();
                    ThietBiObj.Ngaynhap = dt.Rows[0][9].ToString();
                    ThietBiObj.Mancc = dt.Rows[0][10].ToString();
                    ThietBiObj.Madvt = dt.Rows[0][11].ToString();
                    ThietBiObj.Mapb = dt.Rows[0][12].ToString();
                    ThietBiObj.Maloaits = dt.Rows[0][13].ToString();
                    ThietBiObj.Matt = dt.Rows[0][14].ToString();
                    ThietBiObj.Madv = dt.Rows[0][15].ToString();
                    DonViObj.Madv = ThietBiObj.Madv;
                }

                frmThietBi thietBi = new frmThietBi();

                switch (e.ColumnIndex)
                {
                    case 0:
                        HoatDongObj.Noidung = "Xem";
                        thietBi.ShowDialog();
                        break;
                    case 1:
                        if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][2].ToString() == "True")
                        {
                            HoatDongObj.Noidung = "Sửa";
                            thietBi.ShowDialog();
                            timer1.Start();
                        }
                        else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                        break;
                    case 2:
                        if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][3].ToString() == "True")
                        {
                            if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + ThietBiObj.Matb + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                            {
                                if (ThietBiDAO.Instance.Xoa(ThietBiObj.Matb))
                                {
                                    LichSuHoatDongDAO.Instance.ThongBao(3, ThietBiObj.Matb);
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
        }
        private void dgvThietBi_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*DataTable dt = ThietBiDAO.Instance.GetDataThietBiByMaTB(dgvThietBi[3, e.RowIndex].Value.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                ThietBiObj.Matb = dgvThietBi[3, e.RowIndex].Value.ToString();
                ThietBiObj.Tentb = dt.Rows[0][1].ToString();
                ThietBiObj.Nuocsx = dt.Rows[0][2].ToString();
                ThietBiObj.Namsx = dt.Rows[0][3].ToString();
                ThietBiObj.Tgbaohanh = dt.Rows[0][4].ToString();
                ThietBiObj.Tgduavaosd = dt.Rows[0][5].ToString();
                ThietBiObj.Tgtrichkhauhao = dt.Rows[0][6].ToString();
                ThietBiObj.Thongsokythuat = dt.Rows[0][7].ToString();
                ThietBiObj.Nguyengia = dt.Rows[0][8].ToString();
                ThietBiObj.Ngaynhap = dt.Rows[0][9].ToString();
                ThietBiObj.Tenncc = dt.Rows[0][10].ToString();
                ThietBiObj.Tendvt = dt.Rows[0][11].ToString();
                ThietBiObj.Tenptb = dt.Rows[0][12].ToString();
                ThietBiObj.Tenloaits = dt.Rows[0][10].ToString();
                ThietBiObj.Tentt = dt.Rows[0][14].ToString();
                ThietBiObj.Hinhanh = dt.Rows[0][15].ToString();
            }


            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                contextMenu.Items.Add("Xem");
                contextMenu.Items.Add("Sửa");
                contextMenu.Items.Add("Xóa");
                contextMenu.BackColor = Color.White;
                contextMenu.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                contextMenu.RenderMode = ToolStripRenderMode.System;
                contextMenu.ShowImageMargin = false;
                dgvThietBi.ContextMenuStrip = contextMenu;
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                var relativeClickedPosition = e.Location;
                relativeClickedPosition.Y = relativeClickedPosition.Y + 10;

                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenu.Show(screenClickedPosition);
            }*/
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int count = ThietBiDAO.Instance.CountDataThietBi();
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
            int count = ThietBiDAO.Instance.CountDataThietBi() / 10;
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
                case 4:
                    index = 4;
                    break;
                case 5:
                    index = 5;
                    break;
                case 6:
                    index = 6;
                    break;
            }
        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            switch (index)
            {
                case 0:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("TB.MATB", txtSearch.Text);
                    break;
                case 1:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("TENTB", txtSearch.Text);
                    break;
                case 2:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("TB.TGDUAVAOSD", DateTime.Parse(txtSearch.Text).ToString("yyyy-MM-dd"));
                    break;
                case 3:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("DV.TENDV", txtSearch.Text);
                    break;
                case 4:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("PB.TENPB", txtSearch.Text);
                    break;
                case 5:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("NCC.TENNCC", txtSearch.Text);
                    break;
                case 6:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("LTS.TENLOAITS", txtSearch.Text);
                    break;
                case 7:
                    dt = ThietBiDAO.Instance.TimKiemTheoTen("TT.TENTT", txtSearch.Text);
                    break;
            }

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(txtSearch.Text))
            {
                thietbiList.DataSource = dt;
                dgvThietBi.DataSource = thietbiList;
                DonViObj.Madv = DonViDAO.Instance.GetDataDVByTenDV(dgvThietBi.Rows[0].Cells[8].Value.ToString()).Rows[0][0].ToString();
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

        private void btnChucNang_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                contextMenu.Items.Add("Tính Hao Mòn");
                contextMenu.Items.Add("Khấu Hao");
                contextMenu.Items.Add("Sửa Chữa");
                contextMenu.Items.Add("Kiểm Kê");
                contextMenu.Items.Add("Thanh Lý");
                contextMenu.Items.Add("Luân Chuyển");
                contextMenu.Items.Add("Bảo Trì");
                contextMenu.BackColor = Color.White;
                contextMenu.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                contextMenu.RenderMode = ToolStripRenderMode.System;
                contextMenu.ShowImageMargin = false;
                btnChucNang.ContextMenuStrip = contextMenu;
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                var relativeClickedPosition = e.Location;
                relativeClickedPosition.X = relativeClickedPosition.X - 100;
                relativeClickedPosition.Y = relativeClickedPosition.Y + 20;
                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenu.Show(screenClickedPosition);
            }
        }
        private void btnTao_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                contextMenu.Items.Add("Tạo Thẻ Tài Sản");
                contextMenu.Items.Add("Tạo Phiếu Khấu Hao");
                contextMenu.BackColor = Color.White;
                contextMenu.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                contextMenu.RenderMode = ToolStripRenderMode.System;
                contextMenu.ShowImageMargin = false;
                btnChucNang.ContextMenuStrip = contextMenu;
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);

                var relativeClickedPosition = e.Location;
                relativeClickedPosition.X = relativeClickedPosition.X - 100;
                relativeClickedPosition.Y = relativeClickedPosition.Y + 20;
                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenu.Show(screenClickedPosition);
            }
        }
        private void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            frmThietBi thietBi = new frmThietBi();
            switch (e.ClickedItem.Text)
            {
                #region Tạo Phiếu
                case "Tạo Thẻ Tài Sản":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thẻ Tài Sản").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            if (TheTaiSanDAO.Instance.CheckTheTaiSan(ThietBiObj.Matb) == false)
                            {
                                string matts = funtions.SDienMaTuDong("TTS");
                                string matb = ThietBiObj.Matb;
                                string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");
                                string manv = TaikhoanObj.Username;
                                string nguyengia = funtions.RemoveChars(ThietBiObj.Nguyengia);
                                string gthaomon = TheTaiSanDAO.Instance.GetGiaTriHaoMon(matb).ToString();

                                if (TheTaiSanDAO.Instance.Them(matts, matb, ngaylap, manv, gthaomon, nguyengia))
                                {
                                    LichSuHoatDongDAO.Instance.ThongBao(1, matts);
                                    ThongBao.Show("Tạo thẻ tài sản " + matts + " của thiết bị " + matb + " thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                                else
                                {
                                    ThongBao.Show("Có lỗi khi tạo thẻ tài sản", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                            }
                            else ThongBao.Show("Thẻ tài sản của thiết bị " + ThietBiObj.Matb + " đã tạo rồi", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;

                case "Tạo Phiếu Khấu Hao":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Khấu Hao").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            if (PhieuKhauHaoDAO.Instance.CheckPhieuKhauHao(ThietBiObj.Matb) == false)
                            {
                                string mapkh = funtions.SDienMaTuDong("PKH");
                                string matb = ThietBiObj.Matb;
                                string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");
                                string manv = TaikhoanObj.Username;
                                string sonamkh = PhieuKhauHaoDAO.Instance.GetTGTrichKhauHao(matb).Rows[0][0].ToString();
                                string sothangkh = (Int32.Parse(sonamkh) * 12).ToString();
                                string muckhnam = PhieuKhauHaoDAO.Instance.GetMucKhauHaoNam(matb).ToString();
                                string muckhthang = (Int32.Parse(muckhnam) / 12).ToString();

                                if (PhieuKhauHaoDAO.Instance.Them(mapkh, matb, ngaylap, manv, sonamkh, sothangkh, manv))
                                {
                                    LichSuHoatDongDAO.Instance.ThongBao(1, mapkh);
                                    ThongBao.Show("Tạo phiếu khấu hao " + mapkh + " của thiết bị " + matb + " thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                                else
                                {
                                    ThongBao.Show("Có lỗi khi tạo phiếu khấu hao", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                            }
                            else ThongBao.Show("Phiếu khấu hao của thiết bị " + ThietBiObj.Matb + " đã tạo rồi", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;

                #endregion

                #region Chức Năng
                case "Tính Hao Mòn":
                    if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                    {
                        if (TheTaiSanDAO.Instance.CheckTheTaiSan(ThietBiObj.Matb) == true)
                        {
                            string matb = ThietBiObj.Matb;
                            string matts = TheTaiSanDAO.Instance.GetMaTheTSByMaTB(matb).Rows[0][0].ToString();
                            string ngay = DateTime.Now.ToString("MM/dd/yyyy");
                            string diengiai = "Tính hao mòn năm " + DateTime.Now.Year.ToString();
                            if (TheTaiSanDAO.Instance.CheckHaoMonTB(matb) == true)
                            {
                                if (TheTaiSanDAO.Instance.ThemChiTiet(matts, matb, ngay, diengiai) == true && TheTaiSanDAO.Instance.TinhHaoMon(matts, matb, ngay) == true)
                                {
                                    ThongBao.Show("Tính hao mòn của thiết bị " + matb + " thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                                else ThongBao.Show("Có lỗi khi tính hao mòn thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                            else ThongBao.Show("Không đủ điều kiện để tính hao mòn", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Chưa có thẻ tài sản để tính hao mòn", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    TrangThaiObj.Trangthai = "close";
                    break;
                case "Khấu Hao":
                    /*if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                    {
                        if (PhieuKhauHaoDAO.Instance.CheckPhieuKhauHao(ThietBiObj.Matb) == true)
                        {
                            string matb = ThietBiObj.Matb;
                            string mapkh = PhieuKhauHaoDAO.Instance.GetMaPhieuKHyMaTB(matb).Rows[0][0].ToString();
                            string ngaykh = DateTime.Now.ToString("MM/dd/yyyy");
                            string ghichu = "Khấu hao tháng " + DateTime.Now.Month.ToString();
                            if (PhieuKhauHaoDAO.Instance.CheckKhauHaoTB(matb) == true)
                            {
                                if (PhieuKhauHaoDAO.Instance.ThemChiTiet(mapkh, matb, ngaykh, ghichu) == true && PhieuKhauHaoDAO.Instance.TrichKhauHao(mapkh, matb, ngaykh) == true)
                                {
                                    ThongBao.Show("Khấu hao của thiết bị " + matb + " thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                }
                                else ThongBao.Show("Có lỗi khi khấu hao thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                            else ThongBao.Show("Không đủ điều kiện để khấu hao", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Chưa có tạo phiếu khấu hao để khấu hao", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);*/
                    TrangThaiObj.Trangthai = "close";
                    break;
                case "Sửa Chữa":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Sửa Chữa").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            frmPhieuSuaChua phieuSuaChua = new frmPhieuSuaChua();
                            HoatDongObj.Noidung = "Thêm";
                            phieuSuaChua.Width = 310;
                            phieuSuaChua.ShowDialog();
                        }
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Kiểm Kê":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            frmPhieuKiemKe phieuKiemKe = new frmPhieuKiemKe();
                            HoatDongObj.Noidung = "Thêm";
                            phieuKiemKe.Width = 310;
                            phieuKiemKe.ShowDialog();
                        }
                        else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Thanh Lý":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            if (PhieuThanhLyDAO.Instance.CheckPhieuThanhLyByMaTB(ThietBiObj.Matb) == false)
                            {
                                frmPhieuThanhLy phieuThanhLy = new frmPhieuThanhLy();
                                HoatDongObj.Noidung = "Thêm";
                                phieuThanhLy.ShowDialog();
                            }
                            else ThongBao.Show("Thiết bị đã được thanh lý", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Luân Chuyển":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Luân Chuyển").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            //if (PhieuLuanChuyenDAO.Instance.CheckPhieuLuanChuyenByMaTB(ThietBiObj.Matb) == false)
                            {
                                frmPhieuLuanChuyen phieuLuanChuyen = new frmPhieuLuanChuyen();
                                HoatDongObj.Noidung = "Thêm";
                                phieuLuanChuyen.ShowDialog();
                            }
                            //else ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else ThongBao.Show("Không xác định được thiết bị", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Bảo Trì":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu bảo Trì").Rows[0][1].ToString() == "True")
                    {
                        if (!String.IsNullOrEmpty(ThietBiObj.Matb))
                        {
                            frmPhieuBaoTri phieuBaoTri = new frmPhieuBaoTri();
                            HoatDongObj.Noidung = "Thêm";
                            phieuBaoTri.Width = 310;
                            phieuBaoTri.ShowDialog();
                        }
                        TrangThaiObj.Trangthai = "close";
                    }
                    else ThongBao.Show("Bạn không có quyền thêm dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Xem":
                    HoatDongObj.Noidung = "Xem";
                    thietBi.ShowDialog();
                    break;
                case "Sửa":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][2].ToString() == "True")
                    {
                        HoatDongObj.Noidung = "Sửa";
                        thietBi.ShowDialog();
                        timer1.Start();
                    }
                    else ThongBao.Show("Bạn không có quyền sửa dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

                    break;
                case "Xóa":
                    if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Thiết Bị").Rows[0][3].ToString() == "True")
                    {
                        if (ThongBao.Show("Bạn có chắc chắn muốn xóa dữ liệu " + ThietBiObj.Matb + " không?", "Thông báo", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                        {
                            if (ThietBiDAO.Instance.Xoa(ThietBiObj.Matb))
                            {
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
                    #endregion
            }
        }


        #endregion

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (dgvThietBi.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvThietBi.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvThietBi.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvThietBi.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvThietBi.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvThietBi.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }


        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension 
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt); //read excel file  

                        for (int i = 1; i < dtExcel.Rows.Count; i++)
                        {
                            if (!ThietBiDAO.Instance.CheckExistThietBi(dtExcel.Rows[i][0].ToString()))
                            {
                                string matb = dtExcel.Rows[i][0].ToString();
                                string tentb = dtExcel.Rows[i][1].ToString();
                                string nguyengia = funtions.RemoveChars(dtExcel.Rows[i][2].ToString());
                                string nuocsx = dtExcel.Rows[i][3].ToString();
                                string namsx = dtExcel.Rows[i][4].ToString();
                                string tgbaohanh = (DateTime.ParseExact(dtExcel.Rows[i][5].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("MM/dd/yyyy");
                                string tgduavaosd = DateTime.ParseExact(dtExcel.Rows[i][6].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                                string ngaynhap = DateTime.ParseExact(dtExcel.Rows[i][7].ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                                string tgtrichkh = dtExcel.Rows[i][8].ToString();
                                string thongsokt = dtExcel.Rows[i][9].ToString();
                                string mancc = NhaCungCapDAO.Instance.GetDataNCCByTenNCC(dtExcel.Rows[i][10].ToString()).Rows[0][0].ToString();
                                string madvt = DonViTinhDAO.Instance.GetDataDVTByTen(dtExcel.Rows[i][11].ToString()).Rows[0][0].ToString();
                                string madv = DonViDAO.Instance.GetDataDVByTenDV(dtExcel.Rows[i][12].ToString()).Rows[0][0].ToString();
                                string mapb = PhongBanDAO.Instance.GetDataPBByTenPB(dtExcel.Rows[i][13].ToString()).Rows[0][0].ToString();
                                string maloaits = LoaiTaiSanDAO.Instance.GetMaLoaiByTenLoaiTS(dtExcel.Rows[i][14].ToString()).Rows[0][0].ToString();
                                string matt = TrangThaiDAO.Instance.GetDataByTen(dtExcel.Rows[i][15].ToString()).Rows[0][0].ToString();

                                if (ThietBiDAO.Instance.Them(matb, tentb, nuocsx, namsx, tgbaohanh, tgduavaosd, tgtrichkh, thongsokt, nguyengia, ngaynhap,
                                    mancc, madvt, mapb, maloaits, matt, madv))
                                {
                                    CreateQRCode(matb);
                                    ThietBiDAO.Instance.InsertAnhQRCode(matb, Hinh1, "", "", "");
                                    LoadData(Convert.ToInt32(txtPage.Text));
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvThietBi.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xelApp = new Microsoft.Office.Interop.Excel.Application();
                xelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvThietBi.Columns.Count - 2; i++)
                {
                    xelApp.Cells[1, i] = dgvThietBi.Columns[i + 2].HeaderText;

                }

                for (int i = 0; i < dgvThietBi.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvThietBi.Columns.Count - 3; j++)
                    {
                        xelApp.Cells[i + 2, j + 1] = dgvThietBi.Rows[i].Cells[j + 3].Value.ToString();

                    }

                }
                xelApp.Columns.AutoFit();
                xelApp.Visible = true;
            }
        }


    }
}
