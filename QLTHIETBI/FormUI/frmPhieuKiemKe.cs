using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhieuKiemKe : Form
    {
        BindingSource phieukkList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        int column;
        TextBox txtvalue;


        public frmPhieuKiemKe()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
            LoadCombobox();
        }
        #region Phương thức
        void LoadCombobox()
        {
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
        void LoadData()
        {
            txtMaPKK.Text = PhieuKiemKeObj.Mapkk;
            dpkNgayKK.Value = DateTime.ParseExact(PhieuKiemKeObj.Ngaykk.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkNgaylap.Value = DateTime.ParseExact(PhieuKiemKeObj.Ngaylap.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            txtNV1.Text = PhieuKiemKeObj.Nguoikk1;
            cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();
            txtNV2.Text = PhieuKiemKeObj.Nguoikk2;
            cbxNV2.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV2.Text).Rows[0][0].ToString();
            txtNV3.Text = PhieuKiemKeObj.Nguoikk3;
            cbxNV3.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV3.Text).Rows[0][0].ToString();
            txtDV.Text = PhieuKiemKeObj.Donvi;
            cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();

            thietbiList.DataSource = PhieuKiemKeDAO.Instance.GetDataCTPhieuKiemKe(PhieuKiemKeObj.Mapkk);
            dgvCTPhieuKK.DataSource = thietbiList;
        }
        void LoadDGVKiemKeTS()
        {
            phieukkList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvKiemKe.DataSource = phieukkList;
        }
        void ResetForm()
        {
            dpkNgayKK.Value = DateTime.Now;
            cbxNV1.SelectedIndex = -1; txtNV1.Clear();
            cbxNV2.SelectedIndex = -1; txtNV2.Clear();
            cbxNV3.SelectedIndex = -1; txtNV3.Clear();
            cbxDV.SelectedIndex = -1; txtDV.Clear();
            dgvKiemKe.DataSource = null;
            dgvCTPhieuKK.Rows.Clear();
        }
        void EnabledControls(bool value)
        {
            dpkNgayKK.Enabled = value;
            cbxNV1.Enabled = value; txtNV1.Enabled = value;
            cbxNV2.Enabled = value; txtNV2.Enabled = value;
            cbxNV3.Enabled = value; txtNV3.Enabled = value;
            cbxDV.Enabled = value; txtDV.Enabled = value;
            btnMove.Enabled = value;
            btnMoveAll.Enabled = value;
            dgvKiemKe.Enabled = value;
            dgvCTPhieuKK.Enabled = value;
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
            else if (dgvCTPhieuKK == null)
            {
                ThongBao.Show("Chưa có thiết bị nào cần kiểm kê", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion

        #region Sự Kiện
        private void frmPhieuKiemKe_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaPKK.Text = funtions.SDienMaTuDong("PKK");
                    dpkNgaylap.Value = DateTime.Now;
                    dpkNgayKK.Value = DateTime.Now;
                    break;
                case "Xem":
                    LoadData();
                    EnabledControls(false);
                    dgvCTPhieuKK.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    dgvCTPhieuKK.AllowUserToDeleteRows = false;
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

                        if (PhieuKiemKeDAO.Instance.Them(txtMaPKK.Text, dpkNgayKK.Value.ToString("MM/dd/yyyy"), ngaylap, txtNV1.Text, txtNV2.Text, txtNV3.Text, cbxDV.Text))
                        {
                            int i = 0;
                            for (i = 0; i < dgvCTPhieuKK.RowCount; i++)
                            {
                                string matb = dgvCTPhieuKK.Rows[i].Cells[0].Value.ToString();
                                string nguyengiakt = dgvCTPhieuKK.Rows[i].Cells[2].Value.ToString();
                                string nguyengiakk = dgvCTPhieuKK.Rows[i].Cells[3].Value.ToString();
                                string nguyengiacl = dgvCTPhieuKK.Rows[i].Cells[4].Value.ToString();
                                string gtclkt = dgvCTPhieuKK.Rows[i].Cells[5].Value.ToString();
                                string gtclkk = dgvCTPhieuKK.Rows[i].Cells[6].Value.ToString();
                                string gtclcl = dgvCTPhieuKK.Rows[i].Cells[7].Value.ToString();
                                string tinhtrang = "";
                                if (dgvCTPhieuKK.Rows[i].Cells[8].Value != null)
                                    tinhtrang = dgvCTPhieuKK.Rows[i].Cells[8].Value.ToString();
                                string ghichu = "";
                                if (dgvCTPhieuKK.Rows[i].Cells[9].Value != null)
                                    ghichu = dgvCTPhieuKK.Rows[i].Cells[10].Value.ToString();
                                if (PhieuKiemKeDAO.Instance.ThemCT(txtMaPKK.Text, matb, nguyengiakt, nguyengiakk, nguyengiacl, gtclkt, gtclkk, gtclcl, tinhtrang, ghichu))
                                {
                                    //LichSuHoatDongDAO.Instance.ThongBao(1, matb);

                                }
                            }
                            if (i == dgvCTPhieuKK.RowCount)
                            {
                                frmPhieuKiemKe_Load(new object(), new EventArgs());
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

                        if (PhieuKiemKeDAO.Instance.Sua(txtMaPKK.Text, dpkNgayKK.Value.ToString("MM/dd/yyyy"), txtNV1.Text, txtNV2.Text, txtNV3.Text))
                        {
                            if (dgvCTPhieuKK.RowCount > 0)
                            {
                                int i = 0;
                                for (i = 0; i < dgvCTPhieuKK.RowCount; i++)
                                {
                                    string matb = dgvCTPhieuKK.Rows[i].Cells[0].Value.ToString();
                                    string nguyengiakk = funtions.RemoveChars(dgvCTPhieuKK.Rows[i].Cells[3].Value.ToString());
                                    string gtclkk = dgvCTPhieuKK.Rows[i].Cells[6].Value.ToString();
                                    string tinhtrang = "";
                                    if (dgvCTPhieuKK.Rows[i].Cells[8].Value != null)
                                        tinhtrang = dgvCTPhieuKK.Rows[i].Cells[8].Value.ToString();
                                    string ghichu = "";
                                    if (dgvCTPhieuKK.Rows[i].Cells[9].Value != null)
                                        ghichu = dgvCTPhieuKK.Rows[i].Cells[9].Value.ToString();
                                    if (PhieuKiemKeDAO.Instance.SuaCT(txtMaPKK.Text, matb, nguyengiakk, gtclkk, tinhtrang, ghichu))
                                    {
                                        //LichSuHoatDongDAO.Instance.ThongBao(2, matb);                                       
                                    }

                                }
                                if (i == dgvCTPhieuKK.RowCount)
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
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Kiểm Kê").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "PKK";
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
                phieukkList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvKiemKe.DataSource = phieukkList;
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
                        LoadDGVKiemKeTS();
                    else phieukkList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVKiemKeTS();
                    else phieukkList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVKiemKeTS();
                    else phieukkList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvKiemKe.DataSource = phieukkList;
        }

        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (dgvKiemKe.RowCount > 0)
            {
                if (dgvCTPhieuKK.RowCount == 0)
                {
                    string nguyengia = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][0].ToString();
                    string gtcl = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][1].ToString();
                    dgvCTPhieuKK.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb, nguyengia, "", "", gtcl);
                }
                else
                {
                    for (i = 0; i < dgvCTPhieuKK.RowCount; i++)
                    {
                        string matb2 = dgvCTPhieuKK.Rows[i].Cells[0].Value.ToString();
                        if (ThietBiObj.Matb == matb2)
                            break;
                    }
                    if (i == dgvCTPhieuKK.RowCount)
                    {
                        string nguyengia = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][0].ToString();
                        string gtcl = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][1].ToString();
                        dgvCTPhieuKK.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb, nguyengia, "", "", gtcl);
                    }
                }
            }
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            if (dgvKiemKe.RowCount > 0)
            {
                dgvCTPhieuKK.Rows.Clear();
                for (int i = 0; i < dgvKiemKe.RowCount; i++)
                {
                    string matb1 = dgvKiemKe.Rows[i].Cells[0].Value.ToString();
                    string tentb = dgvKiemKe.Rows[i].Cells[1].Value.ToString();
                    string nguyengia = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][0].ToString();
                    string gtcl = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][1].ToString();
                    dgvCTPhieuKK.Rows.Add(matb1, tentb, nguyengia, "", "", gtcl);
                }
            }
        }

        private void dgvKiemKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvKiemKe.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvKiemKe.Rows[e.RowIndex].Cells[1].Value.ToString();
        }


        private void dgvCTPhieuKK_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgvCTPhieuKK.Rows[e.RowIndex].Cells[2].ReadOnly = true;
            dgvCTPhieuKK.Rows[e.RowIndex].Cells[4].ReadOnly = true;
            dgvCTPhieuKK.Rows[e.RowIndex].Cells[5].ReadOnly = true;
            dgvCTPhieuKK.Rows[e.RowIndex].Cells[7].ReadOnly = true;
        }

        private void dgvCTPhieuKK_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (int.TryParse(dgvCTPhieuKK.Rows[e.RowIndex].Cells[3].Value.ToString(), out int n))
                {
                    dgvCTPhieuKK.Rows[e.RowIndex].Cells[4].Value = Convert.ToInt32(dgvCTPhieuKK.Rows[e.RowIndex].Cells[2].Value.ToString()) - Convert.ToInt32(dgvCTPhieuKK.Rows[e.RowIndex].Cells[3].Value.ToString());
                }
                else dgvCTPhieuKK.Rows[e.RowIndex].Cells[3].Value = dgvCTPhieuKK.Rows[e.RowIndex].Cells[2].Value;
            }
            else if (e.ColumnIndex == 6)
            {
                if (int.TryParse(dgvCTPhieuKK.Rows[e.RowIndex].Cells[6].Value.ToString(), out int n))
                {
                    dgvCTPhieuKK.Rows[e.RowIndex].Cells[7].Value = Convert.ToInt32(dgvCTPhieuKK.Rows[e.RowIndex].Cells[5].Value.ToString()) - Convert.ToInt32(dgvCTPhieuKK.Rows[e.RowIndex].Cells[6].Value.ToString());
                }
                else dgvCTPhieuKK.Rows[e.RowIndex].Cells[6].Value = dgvCTPhieuKK.Rows[e.RowIndex].Cells[5].Value;
            }
        }
        #endregion
    }
}
