using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhieuLuanChuyen : Form
    {
        MyFuntions funtions = new MyFuntions();
        BindingSource phieulcList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        int column;
        TextBox txtvalue;
        public frmPhieuLuanChuyen()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
            LoadComboBox();
        }
        #region Phương thức
        void LoadComboBox()
        {
            //Đơn vị
            DataTable data1 = DonViDAO.Instance.GetDataDonVi();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                cbxDV.Items.Add(data1.Rows[i][0].ToString());
                cbxDV2.Items.Add(data1.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac1 = new AutoCompleteStringCollection();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                ac1.Add(data1.Rows[i][1].ToString());
            }
            txtDV.AutoCompleteCustomSource = ac1;
            txtDV2.AutoCompleteCustomSource = ac1;


        }

        void LoadNhanVien(string donvi)
        {
            cbxNV1.Items.Clear();
            cbxNV2.Items.Clear();
            cbxNV3.Items.Clear();
            //Nhân viên 
            DataTable data = NhanVienDAO.Instance.GetDataNhanVien(donvi);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxNV1.Items.Add(data.Rows[i][0].ToString());
                cbxNV2.Items.Add(data.Rows[i][0].ToString());
                cbxNV3.Items.Add(data.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            txtNV1.AutoCompleteCustomSource = ac;
            txtNV2.AutoCompleteCustomSource = ac;
            txtNV3.AutoCompleteCustomSource = ac;
        }
        void LoadNguoiNhan(string donvi)
        {
            cbxNguoiNhan.Items.Clear();
            //Nhân viên 
            DataTable data = NhanVienDAO.Instance.GetDataNhanVien(donvi);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxNguoiNhan.Items.Add(data.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            cbxNguoiNhan.AutoCompleteCustomSource = ac;
        }
        void LoadData()
        {
            txtMaPLC.Text = PhieuLuanChuyenObj.Maplc;
            dpkNgayLC.Value = DateTime.ParseExact(PhieuLuanChuyenObj.Ngaylc.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkNgaylap.Value = DateTime.ParseExact(PhieuLuanChuyenObj.Ngaylap.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            txtNV1.Text = PhieuLuanChuyenObj.Nguoilc1;
            cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();
            txtNV2.Text = PhieuLuanChuyenObj.Nguoilc2;
            cbxNV2.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV2.Text).Rows[0][0].ToString();
            txtNV3.Text = PhieuLuanChuyenObj.Nguoilc3;
            cbxNV3.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV3.Text).Rows[0][0].ToString();
            txtNguoiNhan.Text = PhieuLuanChuyenObj.Nguoilc3;
            cbxNguoiNhan.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV3.Text).Rows[0][0].ToString();
            txtDV.Text = PhieuLuanChuyenObj.Donvi;
            cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();

            thietbiList.DataSource = PhieuLuanChuyenDAO.Instance.GetDataCTPhieuLuanChuyen(PhieuLuanChuyenObj.Maplc);
            dgvCTPhieuLC.DataSource = thietbiList;
        }
        void LoadDGVLuanChuyenTS()
        {
            phieulcList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvLuanChuyen.DataSource = phieulcList;
        }
        void ResetForm()
        {
            dpkNgayLC.Value = DateTime.Now;
            cbxNV1.SelectedIndex = -1; txtNV1.Clear();
            cbxNV2.SelectedIndex = -1; txtNV2.Clear();
            cbxNV3.SelectedIndex = -1; txtNV3.Clear();
            cbxNguoiNhan.SelectedIndex = -1; txtNguoiNhan.Clear();
            cbxDV.SelectedIndex = -1; txtDV.Clear();
            cbxDV2.SelectedIndex = -1; txtDV2.Clear();
            dgvLuanChuyen.DataSource = null;
            dgvCTPhieuLC.Rows.Clear();
        }
        void EnabledControls(bool value)
        {
            dpkNgayLC.Enabled = value;
            cbxNV1.Enabled = value; txtNV1.Enabled = value;
            cbxNV2.Enabled = value; txtNV2.Enabled = value;
            cbxNV3.Enabled = value; txtNV3.Enabled = value;
            cbxNguoiNhan.Enabled = value; txtNguoiNhan.Enabled = value;
            cbxDV.Enabled = value; txtDV.Enabled = value;
            cbxDV2.Enabled = value; txtDV2.Enabled = value;
            btnMove.Enabled = value;
            btnMoveAll.Enabled = value;
            dgvLuanChuyen.Enabled = value;
            dgvCTPhieuLC.Enabled = value;
        }
        private bool DieukienLuu()
        {
            if (String.IsNullOrEmpty(cbxDV.Text))
            {
                ThongBao.Show("Đơn vị không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxNV1.Text) || String.IsNullOrEmpty(cbxNV2.Text) || String.IsNullOrEmpty(cbxNV3.Text))
            {
                ThongBao.Show("Nhân viên kiểm không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxNguoiNhan.Text))
            {
                ThongBao.Show("Người nhận không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (dgvCTPhieuLC == null)
            {
                ThongBao.Show("Chưa có thiết bị nào cần chuyển", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion
        #region Sự Kiện
        private void frmPhieuLuanChuyen_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaPLC.Text = funtions.SDienMaTuDong("PLC");
                    dpkNgaylap.Value = DateTime.Now;
                    dpkNgayLC.Value = DateTime.Now;
                    break;
                case "Xem":
                    LoadData();
                    EnabledControls(false);
                    dgvCTPhieuLC.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    dgvCTPhieuLC.AllowUserToDeleteRows = false;
                    cbxDV.Enabled = txtDV.Enabled = false;
                    btnMove.Enabled = false;
                    btnMoveAll.Enabled = false;
                    break;
            }
        }
        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");
            if (DieukienLuu())
            {
                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":

                        if (PhieuLuanChuyenDAO.Instance.Them(txtMaPLC.Text, dpkNgayLC.Value.ToString("MM/dd/yyyy"), ngaylap, txtNV1.Text, txtNV2.Text, txtNV3.Text, txtNguoiNhan.Text, cbxDV.Text))
                        {
                            int i = 0;
                            for (i = 0; i < dgvCTPhieuLC.RowCount; i++)
                            {
                                string matb = dgvCTPhieuLC.Rows[i].Cells[0].Value.ToString();
                                string tudonvi = dgvCTPhieuLC.Rows[i].Cells[2].Value.ToString();
                                string dendonvi = dgvCTPhieuLC.Rows[i].Cells[3].Value.ToString();
                                string lydo = "";
                                if (dgvCTPhieuLC.Rows[i].Cells[4].Value != null)
                                    lydo = dgvCTPhieuLC.Rows[i].Cells[4].Value.ToString();
                                if (PhieuLuanChuyenDAO.Instance.ThemCT(txtMaPLC.Text, matb, tudonvi, dendonvi, lydo))
                                {
                                    //LichSuHoatDongDAO.Instance.ThongBao(1, matb);

                                }
                            }
                            if (i == dgvCTPhieuLC.RowCount)
                            {
                                frmPhieuLuanChuyen_Load(new object(), new EventArgs());
                                ResetForm();
                                ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            }
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Error, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;

                    case "Sửa":

                        if (PhieuLuanChuyenDAO.Instance.Sua(txtMaPLC.Text, dpkNgayLC.Value.ToString("MM/dd/yyyy"), txtNV1.Text, txtNV2.Text, txtNV3.Text, txtNguoiNhan.Text))
                        {
                            if (dgvCTPhieuLC.RowCount > 0)
                            {
                                int i = 0;
                                for (i = 0; i < dgvCTPhieuLC.RowCount; i++)
                                {
                                    string matb = dgvCTPhieuLC.Rows[i].Cells[0].Value.ToString();
                                    string dendonvi = dgvCTPhieuLC.Rows[i].Cells[2].Value.ToString();
                                    string lydo = "";
                                    if (dgvCTPhieuLC.Rows[i].Cells[3].Value != null)
                                        lydo = dgvCTPhieuLC.Rows[i].Cells[3].Value.ToString();
                                    if (PhieuLuanChuyenDAO.Instance.SuaCT(txtMaPLC.Text, matb, dendonvi, lydo))
                                    {
                                        //LichSuHoatDongDAO.Instance.ThongBao(2, matb);                                       
                                    }

                                }
                                if (i == dgvCTPhieuLC.RowCount)
                                {
                                    ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                                    LoadData();
                                }
                            }
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
        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Luân Chuyển").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "PLC";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void cbxDV_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string madv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tendv = DonViDAO.Instance.GetDataDVByMaDV(madv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tendv))
                txtDV.Text = tendv;
        }
        private void cbxDV_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxDV.Text))
            {
                phieulcList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvLuanChuyen.DataSource = phieulcList;
                LoadNhanVien(cbxDV.Text);
            }

        }

        private void txtDV_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDV.Text))
            {
                if (DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text) != null)
                {
                    cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();
                    cbxDV_SelectedValueChanged(sender, e);
                }
            }
        }
        private void cbxDV2_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string madv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tendv = DonViDAO.Instance.GetDataDVByMaDV(madv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tendv))
                txtDV2.Text = tendv;
        }
        private void cbxDV2_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadNguoiNhan(cbxDV2.Text);
        }
        private void txtDV2_TextChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDV2.Text))
            {
                if (DonViDAO.Instance.GetDataDVByTenDV(txtDV2.Text) != null)
                {
                    cbxDV2.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV2.Text).Rows[0][0].ToString();
                }
            }
        }
        private void cbxNV1_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string manv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tennv = NhanVienDAO.Instance.GetInfoNhanVien(manv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tennv))
                txtNV1.Text = tennv;
        }
        private void cbxNV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxNV1.Text))
            {
                if (NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text) != null)
                {
                    cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();
                }
            }
        }
        private void cbxNV2_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string manv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tennv = NhanVienDAO.Instance.GetInfoNhanVien(manv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tennv))
                txtNV2.Text = tennv;
        }


        private void cbxNV2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxNV2.Text))
            {
                if (NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV2.Text) != null)
                {
                    cbxNV2.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV2.Text).Rows[0][0].ToString();
                }
            }
        }
        private void cbxNV3_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string manv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tennv = NhanVienDAO.Instance.GetInfoNhanVien(manv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tennv))
                txtNV3.Text = tennv;
        }
        private void cbxNV3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxNV3.Text))
            {
                if (NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV3.Text) != null)
                {
                    cbxNV3.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV3.Text).Rows[0][0].ToString();
                }
            }
        }

        private void cbxNguoiNhan_Hover(object sender, QLTHIETBI.HoverEventArgs e)
        {
            string manv = ((QLTHIETBI.MyComboBox)sender).Items[e.itemIndex].ToString();
            string tennv = NhanVienDAO.Instance.GetInfoNhanVien(manv).Rows[0][1].ToString();
            if (!String.IsNullOrEmpty(tennv))
                txtNguoiNhan.Text = tennv;
        }
        private void cbxNguoiNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxNguoiNhan.Text))
            {
                if (NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNguoiNhan.Text) != null)
                {
                    cbxNguoiNhan.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNguoiNhan.Text).Rows[0][0].ToString();
                }
            }
        }
        private void dgvTimkiem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvTimkiem.CurrentCell.ColumnIndex == 0)
            {
                txtvalue = (TextBox)e.Control;
                txtvalue.TextChanged += new EventHandler(txtvalue_TextChanged);
            }
        }
        void txtvalue_TextChanged(object sender, EventArgs e)
        {
            string value = txtvalue.Text;
            switch (column)
            {
                case 0:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVLuanChuyenTS();
                    else phieulcList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVLuanChuyenTS();
                    else phieulcList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVLuanChuyenTS();
                    else phieulcList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvLuanChuyen.DataSource = phieulcList;
        }

        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (dgvLuanChuyen.RowCount > 0 && !String.IsNullOrEmpty(cbxDV2.Text))
            {
                if (dgvCTPhieuLC.RowCount == 0)
                {
                    dgvCTPhieuLC.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb, txtDV.Text, txtDV2.Text);

                }
                else
                {
                    for (i = 0; i < dgvCTPhieuLC.RowCount; i++)
                    {
                        string matb2 = dgvCTPhieuLC.Rows[i].Cells[0].Value.ToString();
                        if (ThietBiObj.Matb == matb2)
                            break;
                    }
                    if (i == dgvCTPhieuLC.RowCount)
                    {
                        dgvCTPhieuLC.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb, txtDV.Text, txtDV2.Text);

                    }
                }
            }
            else
                ThongBao.Show("Đơn vị đến không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            if (dgvLuanChuyen.RowCount > 0 && !String.IsNullOrEmpty(cbxDV2.Text))
            {
                dgvCTPhieuLC.Rows.Clear();
                for (int i = 0; i < dgvLuanChuyen.RowCount; i++)
                {
                    string matb1 = dgvLuanChuyen.Rows[i].Cells[0].Value.ToString();
                    string tentb = dgvLuanChuyen.Rows[i].Cells[1].Value.ToString();
                    dgvCTPhieuLC.Rows.Add(matb1, tentb, txtDV.Text, txtDV2.Text);
                }
            }
            else
                ThongBao.Show("Đơn vị đến không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
        }

        private void dgvLuanChuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvLuanChuyen.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvLuanChuyen.Rows[e.RowIndex].Cells[1].Value.ToString();
        }




        #endregion


    }
}
