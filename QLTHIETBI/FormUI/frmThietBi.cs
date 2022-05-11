using Bunifu.UI.WinForms;
using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using QRCoder;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{

    public partial class frmThietBi : Form
    {

        MyFuntions funtions = new MyFuntions();
        string Hinh1 = "";
        string Hinh2 = "";
        string Hinh3 = "";
        string Hinh4 = "";

        public frmThietBi()
        {
            InitializeComponent();
            LoadCombobox();
            TrangThaiObj.Trangthai = "open";
        }
        #region Phương thức
        void LoadCombobox()
        {
            cbxDVT.DataSource = DonViTinhDAO.Instance.GetDataDonViTinh();
            cbxDVT.DisplayMember = "TENDVT";
            cbxDVT.ValueMember = "MADVT";

            cbxTT.DataSource = TrangThaiDAO.Instance.GetDataTrangThai();
            cbxTT.DisplayMember = "TENTT";
            cbxTT.ValueMember = "MATT";

            //Nhóm tài sản
            DataTable data = NhomTaiSanDAO.Instance.GetDataNhomTaiSan();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxNhomTS.Items.Add(data.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            txtNhomTS.AutoCompleteCustomSource = ac;


            //Đơn vị
            DataTable data1 = DonViDAO.Instance.GetDataDonVi();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                cbxDV.Items.Add(data1.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac1 = new AutoCompleteStringCollection();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                ac1.Add(data1.Rows[i][1].ToString());
            }
            txtDV.AutoCompleteCustomSource = ac1;


            //Nhà cung cấp
            DataTable data2 = NhaCungCapDAO.Instance.GetDataNhaCungCap();
            for (int i = 0; i < data2.Rows.Count; i++)
            {
                cbxNCC.Items.Add(data2.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac2 = new AutoCompleteStringCollection();
            for (int i = 0; i < data2.Rows.Count; i++)
            {
                ac2.Add(data2.Rows[i][1].ToString());
            }
            txtNCC.AutoCompleteCustomSource = ac2;

            string now = DateTime.Now.ToString();
            txtddNN.Text = now.Substring(0, 2);
            txtmmNN.Text = now.Substring(3, 2);
            txtyyNN.Text = now.Substring(6, 4);
        }

        void LoadData()
        {
            txtMaTB.Text = ThietBiObj.Matb;
            txtTenTB.Text = ThietBiObj.Tentb;
            txtNamSX.Text = ThietBiObj.Namsx;
            txtNuocSX.Text = ThietBiObj.Nuocsx;
            txtNguyenGia.Text = ThietBiObj.Nguyengia;
            dpkTGBaoHanh.Value = DateTime.ParseExact(ThietBiObj.Tgbaohanh.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkNgaySD.Text = ThietBiObj.Tgduavaosd;
            txtSoNamKH.Text = ThietBiObj.Sonamkhauhao;
            txtTSKT.Text = ThietBiObj.Thongsokythuat;
            txtNguyenGia.Text = ThietBiObj.Nguyengia;
            dpkNgayNhap.Text = ThietBiObj.Ngaynhap;
            cbxDVT.Text = DonViTinhDAO.Instance.GetDataDVTByMa(ThietBiObj.Madvt).Rows[0][1].ToString();
            cbxNhomTS.Text = NhomTaiSanDAO.Instance.GetDataByMaloaiTS(ThietBiObj.Maloaits).Rows[0][0].ToString();
            txtNhomTS.Text = NhomTaiSanDAO.Instance.GetDataByMaloaiTS(ThietBiObj.Maloaits).Rows[0][1].ToString();
            cbxLoaiTS.Text = ThietBiObj.Maloaits;
            txtLoaiTS.Text = LoaiTaiSanDAO.Instance.GetTenLoaiTSByMaLoaiTS(ThietBiObj.Maloaits).Rows[0][0].ToString();
            cbxLoaiTS_SelectedValueChanged(new object(), new EventArgs());
            cbxNCC.Text = ThietBiObj.Mancc;
            txtNCC.Text = NhaCungCapDAO.Instance.GetDataNCCByMaNCC(ThietBiObj.Mancc).Rows[0][1].ToString();
            cbxNCC_SelectedValueChanged(new object(), new EventArgs());
            cbxDV.Text = ThietBiObj.Madv;
            txtDV.Text = DonViDAO.Instance.GetDataDVByMaDV(ThietBiObj.Madv).Rows[0][0].ToString();
            cbxDV_SelectedValueChanged(new object(), new EventArgs());
            cbxPB.Text = ThietBiObj.Mapb;
            txtPB.Text = PhongBanDAO.Instance.GetDataPBByMaPB(ThietBiObj.Mapb).Rows[0][0].ToString();
            cbxPB_SelectedValueChanged(new object(), new EventArgs());
            cbxTT.Text = TrangThaiDAO.Instance.GetDataByMa(ThietBiObj.Matt).Rows[0][1].ToString();

            string Hinh1 = ThietBiDAO.Instance.GetHinh(ThietBiObj.Matb).Rows[0][0].ToString();
            string Hinh2 = ThietBiDAO.Instance.GetHinh(ThietBiObj.Matb).Rows[0][1].ToString();
            string Hinh3 = ThietBiDAO.Instance.GetHinh(ThietBiObj.Matb).Rows[0][2].ToString();
            string Hinh4 = ThietBiDAO.Instance.GetHinh(ThietBiObj.Matb).Rows[0][3].ToString();

            picHinh1.Image = new MyFuntions().byteArrayToImage(Convert.FromBase64String(Hinh1));
            if (!String.IsNullOrEmpty(Hinh2))
                picHinh2.Image = new MyFuntions().byteArrayToImage(Convert.FromBase64String(Hinh2));
            if (!String.IsNullOrEmpty(Hinh3))
                picHinh3.Image = new MyFuntions().byteArrayToImage(Convert.FromBase64String(Hinh3));
            if (!String.IsNullOrEmpty(Hinh4))
                picHinh4.Image = new MyFuntions().byteArrayToImage(Convert.FromBase64String(Hinh4));
        }
        void ResetForm()
        {
            txtTenTB.Clear();
            txtTenTB.Focus();
            txtNguyenGia.Clear();
            txtNuocSX.Clear();
            txtNamSX.Clear();
            txtSoNamKH.Clear();
            txtddNSD.Text = txtddNN.Text = txtddTGBH.Text = "dd";
            txtmmNSD.Text = txtmmNN.Text = txtmmTGBH.Text = "mm";
            txtyyNSD.Text = txtyyNN.Text = txtyyTGBH.Text = "yyyy";
            cbxNhomTS.SelectedIndex = 0; txtNhomTS.Clear();
            cbxLoaiTS.SelectedIndex = 0; txtLoaiTS.Clear();
            cbxDV.SelectedIndex = 0; txtDV.Clear();
            cbxPB.SelectedIndex = 0; txtPB.Clear();
            cbxNCC.SelectedIndex = 0; txtNCC.Clear();
            picHinh2.Image = Properties.Resources.add_image;
            picHinh3.Image = Properties.Resources.add_image;
            picHinh4.Image = Properties.Resources.add_image;
            txtTSKT.Clear();

        }
        void CreateQRCode(string matb)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCode qrCode = new QRCode(qrGenerator.CreateQrCode(matb, QRCodeGenerator.ECCLevel.H));
            Image image = qrCode.GetGraphic(10, Color.Black, Color.White, false);
            byte[] array = new MyFuntions().imgToByteArray(image);
            Hinh1 = Convert.ToBase64String(array);

            picHinh1.Image = new MyFuntions().byteArrayToImage(array);
        }

        private int checkMonth(int month)
        {
            int songay = 0;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: songay = 31; break;
                case 4:
                case 6:
                case 9:
                case 11: songay = 30; break;
                case 2: songay = 29; break;
            }
            return songay;
        }
        private int checkYear(int month, int year)
        {
            int songay = 0;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: songay = 31; break;
                case 4:
                case 6:
                case 9:
                case 11: songay = 30; break;
                case 2:
                    if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                        songay = 29;
                    else
                        songay = 28;
                    break;
            }
            return songay;
        }
        private void selectAllText(BunifuTextBox textbox, string text, KeyPressEventArgs e)
        {
            e.Handled = true;
            textbox.Text = text;
            textbox.Focus();
            textbox.Select(0, textbox.Text.Length);
        }
        private void checkKeypress(int length, int keypress, BunifuTextBox yytextBox, string day, string month, string year, BunifuDatePicker datePicker, KeyPressEventArgs e)
        {
            string fromyear = (DateTime.Now.Year - 100).ToString();
            int num1 = Convert.ToInt32(fromyear.Substring(length, 1));
            int num2 = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(length, 1));

            int first = Convert.ToInt32(year.Substring(0, 1));
            if (first == 1)
            {
                if (length != 3 && keypress < num1)
                {
                    selectAllText(yytextBox, "yyyy", e);
                }
            }
            else
            {
                if (length != 3 && keypress > num2)
                {
                    selectAllText(yytextBox, "yyyy", e);
                }
            }
        }
        private bool DieukienLuu()
        {
            if (String.IsNullOrEmpty(txtTenTB.Text))
            {
                ThongBao.Show("Tên thiết bị tài sản không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                txtTenTB.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtNguyenGia.Text))
            {
                ThongBao.Show("Nguyên giá thiết bị tài sản không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                txtNguyenGia.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(txtSoNamKH.Text))
            {
                ThongBao.Show("Thời gian trích khấu hao không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                txtSoNamKH.Focus();
                return false;
            }
            else if (txtddNSD.Text == "dd" || txtmmNSD.Text == "mm" || txtyyNSD.Text == "yyyy")
            {
                ThongBao.Show("Thời gian đưa vào sử dụng không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                if (txtddNSD.Text == "dd") txtddNSD.Focus();
                else if (txtmmNSD.Text == "mm") txtmmNSD.Focus();
                else if (txtyyNSD.Text == "yyyy") txtyyNSD.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(cbxLoaiTS.Text))
            {
                ThongBao.Show("Loại tài sản không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxDV.Text))
            {
                ThongBao.Show("Đơn vị không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxPB.Text))
            {
                ThongBao.Show("Phòng ban không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxNCC.Text))
            {
                ThongBao.Show("Nhà cung cấp không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion

        #region Sự kiện
        private void frmThietBi_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaTB.Text = funtions.SDienMaTuDong("TB");
                    CreateQRCode(txtMaTB.Text);
                    break;
                case "Xem":
                    LoadData();
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    break;
            }
        }

        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string matb = txtMaTB.Text;
            string tentb = txtTenTB.Text;
            string nuocsx = txtNuocSX.Text;
            string namsx = txtNamSX.Text;
            string tgbaohanh = dpkTGBaoHanh.Value.ToString("MM/dd/yyyy");
            string tgduavaosd = dpkNgaySD.Value.ToString("MM/dd/yyyy");
            string ngaynhap = dpkNgayNhap.Value.ToString("MM/dd/yyyy");
            string tgtrichkh = txtSoNamKH.Text;
            string thongsokt = txtTSKT.Text;
            string nguyengia = funtions.RemoveChars(txtNguyenGia.Text);
            string mancc = cbxNCC.Text;
            string madvt = cbxDVT.SelectedValue.ToString();
            string madv = cbxDV.Text;
            string mapb = cbxPB.Text;
            string maloaits = cbxLoaiTS.Text;
            string matt = cbxTT.SelectedValue.ToString();

            if (DieukienLuu())
            {
                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":
                        if (ThietBiDAO.Instance.Them(matb, tentb, nuocsx, namsx, tgbaohanh, tgduavaosd, tgtrichkh, thongsokt, nguyengia, ngaynhap,
                            mancc, madvt, mapb, maloaits, matt, madv))
                        {
                            ThietBiDAO.Instance.InsertAnhQRCode(matb, Hinh1, Hinh2, Hinh3, Hinh4);
                            //Tạo thẻ tài sản của thiết bị mới thêm vào
                            string matts = funtions.SDienMaTuDong("TTS");
                            string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");
                            string manv = TaikhoanObj.Username;
                            string gthaomon = TheTaiSanDAO.Instance.GetGiaTriHaoMon(matb).ToString();

                            if (TheTaiSanDAO.Instance.Them(matts, matb, ngaylap, manv, gthaomon, nguyengia))
                            {
                                LichSuHoatDongDAO.Instance.ThongBao(1, matts);
                            }
                            else
                            {
                                ThongBao.Show("Có lỗi khi tạo thẻ tài sản", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }

                            LichSuHoatDongDAO.Instance.ThongBao(1, matb);
                            frmThietBi_Load(new object(), new EventArgs());
                            ResetForm();
                            ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Error, ThongBao.AnimateStyle.FadeIn);
                        }

                        break;

                    case "Sửa":
                        if (ThietBiDAO.Instance.Sua(matb, tentb, nuocsx, namsx, tgbaohanh, tgduavaosd, tgtrichkh, thongsokt, nguyengia, ngaynhap, mancc, madvt, mapb, maloaits, matt, madv))
                        {
                            if (!String.IsNullOrEmpty(Hinh2))
                                ThietBiDAO.Instance.EditAnh(txtMaTB.Text, 2, Hinh2);
                            if (!String.IsNullOrEmpty(Hinh3))
                                ThietBiDAO.Instance.EditAnh(txtMaTB.Text, 3, Hinh2);
                            if (!String.IsNullOrEmpty(Hinh4))
                                ThietBiDAO.Instance.EditAnh(txtMaTB.Text, 4, Hinh2);
                            LichSuHoatDongDAO.Instance.ThongBao(2, matb);
                            ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi sửa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;
                }
            }

        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }


        #endregion

        #region Thông tin chung
        private void txtNguyenGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
            else
            {
                txtNguyenGia.Text = funtions.RemoveChars(txtNguyenGia.Text);
                int length = txtNguyenGia.Text.Length;

                if ((int)e.KeyChar == 8)
                    length = length - 2;
                switch (length)
                {
                    case 3:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(1, ".");
                        break;
                    case 4:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(2, ".");
                        break;
                    case 5:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(3, ".");
                        break;
                    case 6:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(1, ".");
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(5, ".");
                        break;
                    case 7:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(2, ".");
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(6, ".");
                        break;
                    case 8:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(3, ".");
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(7, ".");
                        break;
                    case 9:
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(1, ".");
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(5, ".");
                        txtNguyenGia.Text = txtNguyenGia.Text.Insert(9, ".");
                        break;
                    case 10:
                        break;
                }
                if ((txtNguyenGia.Text.Length) > 3) txtNguyenGia.Select(txtNguyenGia.Text.Length, 0);
            }
        }

        private void txtNamSX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtSoNamKH_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoNamKH.Text))
            {
                txtSoThangKH.Text = (Convert.ToInt32(txtSoNamKH.Text) * 12).ToString();
            }
            else txtSoThangKH.Text = "";
        }

        private void txtSoNamKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtSoThangKH_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoThangKH.Text))
            {
                txtSoNamKH.Text = (Convert.ToInt32(txtSoThangKH.Text) / 12).ToString();
            }
        }

        private void txtSoThangKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtddNSD_Click(object sender, EventArgs e)
        {
            txtddNSD.SelectAll();
        }

        private void txtmmNSD_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtddNSD.Text) || txtddNSD.Text == "dd")
            {
                txtddNSD.Focus();
                txtddNSD.SelectAll();
            }
            else txtmmNSD.SelectAll();
        }

        private void txtyyNSD_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtddNSD.Text) || txtddNSD.Text == "dd")
            {
                txtddNSD.Focus();
                txtddNSD.SelectAll();
            }
            else if (String.IsNullOrEmpty(txtmmNSD.Text) || txtmmNSD.Text == "mm")
            {
                txtmmNSD.Focus();
                txtmmNSD.SelectAll();
            }
            else txtyyNSD.SelectAll();
        }

        private void txtddNSD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtddNSD.Text.Length == 2)
                {
                    if (keypress > 3)
                    {
                        e.Handled = true;
                        txtddNSD.Text = "0" + keypress.ToString();
                        txtmmNSD.Focus();
                        txtmmNSD.Select(0, 2);
                    }
                }
                else
                {
                    int first = Convert.ToInt32(txtddNSD.Text.Substring(0, 1));
                    if (first == 3)
                    {
                        if (keypress <= 1)
                        {
                            txtmmNSD.Focus();
                            txtmmNSD.Select(0, 2);
                        }
                        else selectAllText(txtddNSD, "dd", e);
                    }
                    else
                    {
                        txtmmNSD.Focus();
                        txtmmNSD.Select(0, 2);
                    }
                }

            }
            else selectAllText(txtddNSD, "dd", e);
        }

        private void txtmmNSD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtmmNSD.Text.Length == 2)
                {
                    if (keypress > 1)
                    {
                        if (Convert.ToInt32(txtddNSD.Text) <= checkMonth(keypress))
                        {
                            e.Handled = true;
                            txtmmNSD.Text = "0" + keypress.ToString();
                            txtyyNSD.Focus();
                            txtyyNSD.Select(0, 4);
                        }
                        else selectAllText(txtmmNSD, "mm", e);
                    }

                }
                else
                {
                    int first = Convert.ToInt32(txtmmNSD.Text.Substring(0, 1));
                    if (first == 1)
                    {
                        if (keypress <= 2)
                        {
                            if (Convert.ToInt32(txtddNSD.Text) <= checkMonth(Convert.ToInt32(("1" + keypress.ToString()))))
                            {
                                txtyyNSD.Focus();
                                txtyyNSD.Select(0, 4);
                            }
                        }
                        else selectAllText(txtmmNSD, "mm", e);
                    }
                    else
                    {
                        if (Convert.ToInt32(txtddNSD.Text) <= checkMonth(keypress))
                        {
                            txtyyNSD.Focus();
                            txtyyNSD.Select(0, 4);
                        }
                        else selectAllText(txtmmNSD, "mm", e);
                    }
                }

            }
            else selectAllText(txtmmNSD, "mm", e);
        }

        private void txtyyNSD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtyyNSD.Text.Length == 4)
                {
                    if (txtyyNSD.SelectionLength == 4)
                    {
                        if (keypress > 2)
                        {
                            selectAllText(txtyyNSD, "yyyy", e);
                        }
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
                else if (txtyyNSD.Text.Length == 1)
                {
                    checkKeypress(1, keypress, txtyyNSD, txtddNSD.Text, txtmmNSD.Text, txtyyNSD.Text, dpkNgaySD, e);
                }
                else if (txtyyNSD.Text.Length == 2)
                {
                    checkKeypress(2, keypress, txtyyNSD, txtddNSD.Text, txtmmNSD.Text, txtyyNSD.Text, dpkNgaySD, e);
                }
                else if (txtyyNSD.Text.Length == 3)
                {
                    int thang = Convert.ToInt32(txtmmNSD.Text);
                    int nam = Convert.ToInt32(txtyyNSD.Text + keypress.ToString());
                    if (Convert.ToInt32(txtddNSD.Text) <= checkYear(thang, nam))
                        checkKeypress(3, keypress, txtyyNSD, txtddNSD.Text, txtmmNSD.Text, txtyyNSD.Text, dpkNgaySD, e);
                    else e.Handled = true;
                }

            }
            else
            {
                selectAllText(txtyyNSD, "yyyy", e);
            }
        }

        private void dpkNgaySD_ValueChanged(object sender, EventArgs e)
        {
            txtddNSD.Text = dpkNgaySD.Text.Substring(0, 2);
            txtmmNSD.Text = dpkNgaySD.Text.Substring(3, 2);
            txtyyNSD.Text = dpkNgaySD.Text.Substring(6, 4);
        }
        private void dpkNgaySD_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtddNSD.Text != "dd" || txtmmNSD.Text != "mm" || txtyyNSD.Text != "yyyy")
            {
                int ngay = Convert.ToInt32(txtddNSD.Text);
                int thang = Convert.ToInt32(txtmmNSD.Text);
                int nam = Convert.ToInt32(txtyyNSD.Text);
                dpkNgaySD.Value = new DateTime(nam, thang, ngay);
            }

        }

        private void txtddTGBH_Click(object sender, EventArgs e)
        {
            txtddTGBH.SelectAll();
        }

        private void txtmmTGBH_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtddTGBH.Text) || txtddTGBH.Text == "dd")
            {
                txtddTGBH.Focus();
                txtddTGBH.SelectAll();
            }
            else txtmmTGBH.SelectAll();
        }

        private void txtyyTGBH_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtddTGBH.Text) || txtddTGBH.Text == "dd")
            {
                txtddTGBH.Focus();
                txtddTGBH.SelectAll();
            }
            if (String.IsNullOrEmpty(txtmmTGBH.Text) || txtmmTGBH.Text == "mm")
            {
                txtmmTGBH.Focus();
                txtmmTGBH.SelectAll();
            }
            else txtyyTGBH.SelectAll();
        }

        private void txtddTGBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtddTGBH.Text.Length == 2)
                {
                    if (keypress > 3)
                    {
                        e.Handled = true;
                        txtddTGBH.Text = "0" + keypress.ToString();
                        txtmmTGBH.Focus();
                        txtmmTGBH.Select(0, 2);
                    }
                }
                else
                {
                    int first = Convert.ToInt32(txtddTGBH.Text.Substring(0, 1));
                    if (first == 3)
                    {
                        if (keypress <= 1)
                        {
                            txtmmTGBH.Focus();
                            txtmmTGBH.Select(0, 2);
                        }
                        else selectAllText(txtddTGBH, "dd", e);
                    }
                    else
                    {
                        txtmmTGBH.Focus();
                        txtmmTGBH.Select(0, 2);
                    }
                }

            }
            else selectAllText(txtddTGBH, "dd", e);
        }

        private void txtmmTGBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtmmTGBH.Text.Length == 2)
                {
                    if (keypress > 1)
                    {
                        if (Convert.ToInt32(txtddTGBH.Text) <= checkMonth(keypress))
                        {
                            e.Handled = true;
                            txtmmTGBH.Text = "0" + keypress.ToString();
                            txtyyTGBH.Focus();
                            txtyyTGBH.Select(0, 4);
                        }
                        else selectAllText(txtmmTGBH, "mm", e);
                    }

                }
                else
                {
                    int first = Convert.ToInt32(txtmmTGBH.Text.Substring(0, 1));
                    if (first == 1)
                    {
                        if (keypress <= 2)
                        {
                            if (Convert.ToInt32(txtddTGBH.Text) <= checkMonth(Convert.ToInt32(("1" + keypress.ToString()))))
                            {
                                txtyyTGBH.Focus();
                                txtyyTGBH.Select(0, 4);
                            }
                        }
                        else selectAllText(txtmmTGBH, "mm", e);
                    }
                    else
                    {
                        if (Convert.ToInt32(txtddTGBH.Text) <= checkMonth(keypress))
                        {
                            txtyyTGBH.Focus();
                            txtyyTGBH.Select(0, 4);
                        }
                        else selectAllText(txtmmTGBH, "mm", e);
                    }
                }

            }
            else selectAllText(txtmmTGBH, "mm", e);
        }

        private void txtyyTGBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                int keypress = Convert.ToInt32(e.KeyChar.ToString());
                if (txtyyTGBH.Text.Length == 4)
                {
                    if (txtyyTGBH.SelectionLength == 4)
                    {
                        if (keypress > 2)
                        {
                            selectAllText(txtyyTGBH, "yyyy", e);
                        }
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
                else if (txtyyTGBH.Text.Length == 1)
                {
                    checkKeypress(1, keypress, txtyyTGBH, txtddTGBH.Text, txtmmTGBH.Text, txtyyTGBH.Text, dpkTGBaoHanh, e);
                }
                else if (txtyyTGBH.Text.Length == 2)
                {
                    checkKeypress(2, keypress, txtyyTGBH, txtddTGBH.Text, txtmmTGBH.Text, txtyyTGBH.Text, dpkTGBaoHanh, e);
                }
                else if (txtyyTGBH.Text.Length == 3)
                {
                    int thang = Convert.ToInt32(txtmmTGBH.Text);
                    int nam = Convert.ToInt32(txtyyTGBH.Text + keypress.ToString());
                    if (Convert.ToInt32(txtddTGBH.Text) <= checkYear(thang, nam))
                        checkKeypress(3, keypress, txtyyTGBH, txtddTGBH.Text, txtmmTGBH.Text, txtyyTGBH.Text, dpkTGBaoHanh, e);
                    else e.Handled = true;
                }

            }
            else
            {
                selectAllText(txtyyTGBH, "yyyy", e);
            }
        }

        private void dpkTGBaoHanh_ValueChanged(object sender, EventArgs e)
        {
            txtddTGBH.Text = dpkTGBaoHanh.Text.Substring(0, 2);
            txtmmTGBH.Text = dpkTGBaoHanh.Text.Substring(3, 2);
            txtyyTGBH.Text = dpkTGBaoHanh.Text.Substring(6, 4);
        }
        private void dpkTGBaoHanh_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtddTGBH.Text != "dd" || txtmmTGBH.Text != "mm" || txtyyTGBH.Text != "yyyy")
            {
                int ngay = Convert.ToInt32(txtddTGBH.Text);
                int thang = Convert.ToInt32(txtmmTGBH.Text);
                int nam = Convert.ToInt32(txtyyTGBH.Text);
                dpkTGBaoHanh.Value = new DateTime(nam, thang, ngay);
            }
        }

        #endregion


        #region Thông tin tài sản
        private void cbxNhomTS_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string manhomts = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tennhomts = NhomTaiSanDAO.Instance.GetDataByMaNhomTS(manhomts).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tennhomts))
                txtNhomTS.Text = tennhomts;
        }

        private void cbxNhomTS_SelectedValueChanged(object sender, EventArgs e)
        {
            txtLoaiTS.AutoCompleteCustomSource = null;
            cbxLoaiTS.Items.Clear();
            DataTable data = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSanByMaNhomTS(cbxNhomTS.Text);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxLoaiTS.Items.Add(data.Rows[i][0].ToString());
            }

            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            txtLoaiTS.AutoCompleteCustomSource = ac;
        }

        private void txtNhomTS_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNhomTS.Text))
            {
                if (NhomTaiSanDAO.Instance.GetDataByTenNhomTS(txtNhomTS.Text) != null)
                {
                    cbxNhomTS.Text = NhomTaiSanDAO.Instance.GetDataByTenNhomTS(txtNhomTS.Text).Rows[0][0].ToString();
                    cbxNhomTS_SelectedValueChanged(sender, e);
                }
            }
        }
        private void cbxLoaiTS_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string maloaits = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tenloaits = LoaiTaiSanDAO.Instance.GetTenLoaiTSByMaLoaiTS(maloaits).Rows[0][0].ToString();
            if (!String.IsNullOrEmpty(tenloaits))
                txtLoaiTS.Text = tenloaits;
        }
        private void cbxLoaiTS_SelectedValueChanged(object sender, EventArgs e)
        {
            txtNamKHMin.Text = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSan(cbxLoaiTS.Text).Rows[0][2].ToString();
            txtNamKHMax.Text = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSan(cbxLoaiTS.Text).Rows[0][3].ToString();
            txtTGSD.Text = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSan(cbxLoaiTS.Text).Rows[0][4].ToString();
            txtTLHM.Text = LoaiTaiSanDAO.Instance.GetDataLoaiTaiSan(cbxLoaiTS.Text).Rows[0][5].ToString();
        }

        private void txtLoaiTS_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLoaiTS.Text))
            {
                if (LoaiTaiSanDAO.Instance.GetMaLoaiByTenLoaiTS(txtLoaiTS.Text) != null)
                {
                    cbxLoaiTS.Text = LoaiTaiSanDAO.Instance.GetMaLoaiByTenLoaiTS(txtLoaiTS.Text).Rows[0][0].ToString();
                }
            }
        }
        #endregion

        #region Thông tin đơn vị
        private void cbxDV_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string madv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tendv = DonViDAO.Instance.GetDataDVByMaDV(madv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tendv))
                txtDV.Text = tendv;
        }

        private void cbxDV_SelectedValueChanged(object sender, EventArgs e)
        {
            txtPB.AutoCompleteCustomSource = null;
            cbxPB.Items.Clear();
            DataTable data = PhongBanDAO.Instance.GetDataPhongBanByMaDV(cbxDV.Text);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxPB.Items.Add(data.Rows[i][0].ToString());
            }

            txtSdtDV.Text = DonViDAO.Instance.GetDataDVByMaDV(cbxDV.Text).Rows[0][3].ToString();
            txtEmailDV.Text = DonViDAO.Instance.GetDataDVByMaDV(cbxDV.Text).Rows[0][4].ToString();
            txtDiaChiDV.Text = DonViDAO.Instance.GetDataDVByMaDV(cbxDV.Text).Rows[0][2].ToString();
            txtMoTaDV.Text = DonViDAO.Instance.GetDataDVByMaDV(cbxDV.Text).Rows[0][5].ToString();

            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            txtPB.AutoCompleteCustomSource = ac;
        }

        private void txtDV_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDV.Text))
            {
                if (DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text) != null)
                {
                    cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();
                    txtSdtDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][3].ToString();
                    txtEmailDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][4].ToString();
                    txtDiaChiDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][2].ToString();
                    txtMoTaDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][5].ToString();
                    cbxDV_SelectedValueChanged(sender, e);
                }
            }
        }
        #endregion

        #region Thông tin phòng ban

        private void cbxPB_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string mapb = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tenpb = PhongBanDAO.Instance.GetDataPBByMaPB(mapb).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tenpb))
                txtPB.Text = tenpb;
        }
        private void cbxPB_SelectedValueChanged(object sender, EventArgs e)
        {
            txtSdtPB.Text = PhongBanDAO.Instance.GetDataPBByMaPB(cbxPB.Text).Rows[0][3].ToString();
            txtEmailPB.Text = PhongBanDAO.Instance.GetDataPBByMaPB(cbxPB.Text).Rows[0][4].ToString();
            txtDiaChiPB.Text = PhongBanDAO.Instance.GetDataPBByMaPB(cbxPB.Text).Rows[0][2].ToString();
            txtMoTaPB.Text = PhongBanDAO.Instance.GetDataPBByMaPB(cbxPB.Text).Rows[0][5].ToString();
        }

        private void txtPB_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLoaiTS.Text))
            {
                if (PhongBanDAO.Instance.GetDataPBByTenPB(txtPB.Text) != null)
                {
                    cbxPB.Text = PhongBanDAO.Instance.GetDataPBByTenPB(txtPB.Text).Rows[0][0].ToString();
                }
            }
        }
        #endregion

        #region Thông thông Nhà cung cấp
        private void cbxNCC_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string mancc = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tenncc = NhaCungCapDAO.Instance.GetDataNCCByMaNCC(mancc).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tenncc))
                txtNCC.Text = tenncc;
        }
        private void cbxNCC_SelectedValueChanged(object sender, EventArgs e)
        {
            txtSdtNCC.Text = NhaCungCapDAO.Instance.GetDataNCCByMaNCC(cbxNCC.Text).Rows[0][3].ToString();
            txtEmailNCC.Text = NhaCungCapDAO.Instance.GetDataNCCByMaNCC(cbxNCC.Text).Rows[0][4].ToString();
            txtDiaChiNCC.Text = NhaCungCapDAO.Instance.GetDataNCCByMaNCC(cbxNCC.Text).Rows[0][2].ToString();
        }

        private void txtNCC_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNCC.Text))
            {
                if (NhaCungCapDAO.Instance.GetDataNCCByTenNCC(txtNCC.Text) != null)
                {
                    cbxNCC.Text = NhaCungCapDAO.Instance.GetDataNCCByTenNCC(txtNCC.Text).Rows[0][0].ToString();
                }
            }
        }
        #endregion

        private void picHinh2_Click(object sender, EventArgs e)
        {
            string filepath = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            if (!String.IsNullOrEmpty(filepath))
            {
                picHinh2.Image = Image.FromFile(filepath);

                Image image = picHinh2.Image;
                byte[] array = new MyFuntions().imgToByteArray(image);
                Hinh2 = Convert.ToBase64String(array);
            }
        }

        private void picHinh3_Click(object sender, EventArgs e)
        {
            string filepath = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            if (!String.IsNullOrEmpty(filepath))
            {
                picHinh3.Image = Image.FromFile(filepath);

                Image image = picHinh3.Image;
                byte[] array = new MyFuntions().imgToByteArray(image);
                Hinh3 = Convert.ToBase64String(array);
            }
        }

        private void picHinh4_Click(object sender, EventArgs e)
        {
            string filepath = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            if (!String.IsNullOrEmpty(filepath))
            {
                picHinh4.Image = Image.FromFile(filepath);

                Image image = picHinh4.Image;
                byte[] array = new MyFuntions().imgToByteArray(image);
                Hinh4 = Convert.ToBase64String(array);
            }
        }

        private void frmThietBi_FormClosing(object sender, FormClosingEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
        }


    }
}
