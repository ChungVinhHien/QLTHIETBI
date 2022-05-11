using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhieuSuaChua : Form
    {
        BindingSource phieuscList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        MyFuntions funtions = new MyFuntions();
        int column;
        TextBox txtvalue;
        public frmPhieuSuaChua()
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
            //Nhân viên 
            DataTable data = NhanVienDAO.Instance.GetDataNhanVien(donvi);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbxNV1.Items.Add(data.Rows[i][0].ToString());
            }
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ac.Add(data.Rows[i][1].ToString());
            }
            txtNV1.AutoCompleteCustomSource = ac;
        }
        void LoadData()
        {
            txtMaPSC.Text = PhieuSuaChuaObj.Maphieusc;
            dpkNgaylap.Value = DateTime.ParseExact(PhieuSuaChuaObj.Ngaylap.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkTuNgay.Value = DateTime.ParseExact(PhieuSuaChuaObj.Tungay.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkDenNgay.Value = DateTime.ParseExact(PhieuSuaChuaObj.Denngay.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            txtNV1.Text = PhieuSuaChuaObj.Nhanvien;
            cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();
            txtNguoiSC.Text = PhieuSuaChuaObj.Nguoisc;
            txtDV.Text = PhieuSuaChuaObj.Donvi;
            cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();

            thietbiList.DataSource = PhieuSuaChuaDAO.Instance.GetDataCTPhieuSuaChua(PhieuSuaChuaObj.Maphieusc);
            dgvCTPhieuSC.DataSource = thietbiList;
        }
        void LoadDGVSuaChuaTS()
        {
            phieuscList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvSuaChua.DataSource = phieuscList;
        }
        void ResetForm()
        {
            dpkNgaylap.Value = DateTime.Now;
            dpkTuNgay.Value = DateTime.Now;
            dpkDenNgay.Value = DateTime.Now;
            cbxNV1.SelectedIndex = -1; txtNV1.Clear();
            txtNguoiSC.Clear();
            dgvSuaChua.DataSource = null;
            dgvCTPhieuSC.Rows.Clear();
        }
        void EnabledControls(bool value)
        {
            dpkTuNgay.Enabled = value;
            dpkDenNgay.Enabled = value;
            cbxNV1.Enabled = value; txtNV1.Enabled = value;
            txtNguoiSC.Enabled = value;
            cbxDV.Enabled = value; txtDV.Enabled = value;
            btnMove.Enabled = value;
            btnMoveAll.Enabled = value;
            dgvSuaChua.Enabled = value;
            dgvCTPhieuSC.Enabled = value;
        }
        private bool DieukienLuu()
        {
            if (String.IsNullOrEmpty(cbxDV.Text))
            {
                ThongBao.Show("Đơn vị không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxNV1.Text) || String.IsNullOrEmpty(txtNguoiSC.Text))
            {
                ThongBao.Show("Nhân viên không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (dgvCTPhieuSC == null)
            {
                ThongBao.Show("Chưa có thiết bị nào cần kiểm kê", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion

        #region Sự Kiện
        private void frmPhieuSuaChua_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaPSC.Text = funtions.SDienMaTuDong("PSC");
                    dpkNgaylap.Value = DateTime.Now;
                    dpkTuNgay.Value = DateTime.Now;
                    dpkDenNgay.Value = DateTime.Now;
                    break;
                case "Xem":
                    LoadData();
                    EnabledControls(false);
                    dgvCTPhieuSC.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    dgvCTPhieuSC.AllowUserToDeleteRows = false;
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

                        if (PhieuSuaChuaDAO.Instance.Them(txtMaPSC.Text, ngaylap, cbxNV1.Text, dpkTuNgay.Value.ToString("MM/dd/yyyy"), dpkDenNgay.Value.ToString("MM/dd/yyyy"), txtNguoiSC.Text, cbxDV.Text))
                        {
                            int i = 0;
                            for (i = 0; i < dgvCTPhieuSC.RowCount; i++)
                            {
                                string matb = dgvCTPhieuSC.Rows[i].Cells[0].Value.ToString();
                                string noidung = "";
                                if (dgvCTPhieuSC.Rows[i].Cells[2].Value != null)
                                    noidung = dgvCTPhieuSC.Rows[i].Cells[2].Value.ToString();
                                string giadudoan = funtions.RemoveChars(dgvCTPhieuSC.Rows[i].Cells[3].Value.ToString());
                                string giathucte = funtions.RemoveChars(dgvCTPhieuSC.Rows[i].Cells[4].Value.ToString());
                                string ketqua = "";
                                if (dgvCTPhieuSC.Rows[i].Cells[5].Value != null)
                                    ketqua = dgvCTPhieuSC.Rows[i].Cells[5].Value.ToString();
                                if (PhieuSuaChuaDAO.Instance.ThemCT(txtMaPSC.Text, matb, noidung, giadudoan, giathucte, ketqua))
                                {
                                    //LichSuHoatDongDAO.Instance.ThongBao(1, matb);

                                }
                            }
                            if (i == dgvCTPhieuSC.RowCount)
                            {
                                frmPhieuSuaChua_Load(new object(), new EventArgs());
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

                        if (PhieuSuaChuaDAO.Instance.Sua(txtMaPSC.Text, cbxNV1.Text, dpkTuNgay.Value.ToString("MM/dd/yyyy"), dpkDenNgay.Value.ToString("MM/dd/yyyy"), txtNguoiSC.Text))
                        {
                            if (dgvCTPhieuSC.RowCount > 0)
                            {
                                int i = 0;
                                for (i = 0; i < dgvCTPhieuSC.RowCount; i++)
                                {
                                    string matb = dgvCTPhieuSC.Rows[i].Cells[0].Value.ToString();
                                    string noidung = "";
                                    if (dgvCTPhieuSC.Rows[i].Cells[2].Value != null)
                                        noidung = dgvCTPhieuSC.Rows[i].Cells[2].Value.ToString();
                                    string giadudoan = funtions.RemoveChars(dgvCTPhieuSC.Rows[i].Cells[3].Value.ToString());
                                    string giathucte = funtions.RemoveChars(dgvCTPhieuSC.Rows[i].Cells[4].Value.ToString());
                                    string ketqua = "";
                                    if (dgvCTPhieuSC.Rows[i].Cells[5].Value != null)
                                        ketqua = dgvCTPhieuSC.Rows[i].Cells[5].Value.ToString();
                                    if (PhieuSuaChuaDAO.Instance.SuaCT(txtMaPSC.Text, matb, noidung, giadudoan, giathucte, ketqua))
                                    {
                                        //LichSuHoatDongDAO.Instance.ThongBao(2, matb);                                       
                                    }

                                }
                                if (i == dgvCTPhieuSC.RowCount)
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

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Sửa Chữa").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "PSC";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }
        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
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
                phieuscList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvSuaChua.DataSource = phieuscList;
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
                        LoadDGVSuaChuaTS();
                    else phieuscList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVSuaChuaTS();
                    else phieuscList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVSuaChuaTS();
                    else phieuscList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvSuaChua.DataSource = phieuscList;
        }

        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (dgvSuaChua.RowCount > 0)
            {
                if (dgvCTPhieuSC.RowCount == 0)
                {
                    dgvCTPhieuSC.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                }
                else
                {
                    for (i = 0; i < dgvCTPhieuSC.RowCount; i++)
                    {
                        string matb2 = dgvCTPhieuSC.Rows[i].Cells[0].Value.ToString();
                        if (ThietBiObj.Matb == matb2)
                            break;
                    }
                    if (i == dgvCTPhieuSC.RowCount)
                    {
                        dgvCTPhieuSC.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                    }
                }
            }
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            if (dgvSuaChua.RowCount > 0)
            {
                dgvCTPhieuSC.Rows.Clear();
                for (int i = 0; i < dgvSuaChua.RowCount; i++)
                {
                    string matb1 = dgvSuaChua.Rows[i].Cells[0].Value.ToString();
                    string tentb = dgvSuaChua.Rows[i].Cells[1].Value.ToString();
                    dgvCTPhieuSC.Rows.Add(matb1, tentb);
                }
            }
        }

        private void dgvSuaChua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvSuaChua.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvSuaChua.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgvCTPhieuSC_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (int.TryParse(dgvCTPhieuSC.Rows[e.RowIndex].Cells[3].Value.ToString(), out int n))
                {

                }
                else dgvCTPhieuSC.Rows[e.RowIndex].Cells[3].Value = 0;
            }
            else if (e.ColumnIndex == 4)
            {
                if (int.TryParse(dgvCTPhieuSC.Rows[e.RowIndex].Cells[4].Value.ToString(), out int n))
                {
                }
                else dgvCTPhieuSC.Rows[e.RowIndex].Cells[4].Value = 0;
            }
        }
        #endregion


    }
}
