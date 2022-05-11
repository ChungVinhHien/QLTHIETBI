using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhieuBaoTri : Form
    {
        MyFuntions funtions = new MyFuntions();
        BindingSource phieubtList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        int column;
        TextBox txtvalue;
        public frmPhieuBaoTri()
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
            txtMaPBT.Text = PhieuBaoTriObj.Maphieubt;
            dpkNgaylap.Value = DateTime.ParseExact(PhieuBaoTriObj.Ngaylap.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkTuNgay.Value = DateTime.ParseExact(PhieuBaoTriObj.Tungay.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkDenNgay.Value = DateTime.ParseExact(PhieuBaoTriObj.Denngay.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            txtNV1.Text = PhieuBaoTriObj.Nhanvien;
            cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();
            txtDV.Text = PhieuBaoTriObj.Donvi;
            cbxDV.Text = DonViDAO.Instance.GetDataDVByTenDV(txtDV.Text).Rows[0][0].ToString();

            thietbiList.DataSource = PhieuBaoTriDAO.Instance.GetDataCTPhieuBaoTri(PhieuBaoTriObj.Maphieubt);
            dgvCTPhieuBT.DataSource = thietbiList;
        }
        void LoadDGVBaoTriTS()
        {
            phieubtList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvBaoTri.DataSource = phieubtList;
        }
        void ResetForm()
        {
            dpkTuNgay.Value = DateTime.Now;
            dpkDenNgay.Value = DateTime.Now;
            cbxNV1.SelectedIndex = -1; txtNV1.Clear();
            cbxDV.SelectedIndex = -1; txtDV.Clear();
            dgvBaoTri.DataSource = null;
            dgvCTPhieuBT.Rows.Clear();
        }
        void EnabledControls(bool value)
        {
            dpkTuNgay.Enabled = value;
            dpkDenNgay.Enabled = value;
            cbxNV1.Enabled = value; txtNV1.Enabled = value;
            cbxDV.Enabled = value; txtDV.Enabled = value;
            btnMove.Enabled = value;
            btnMoveAll.Enabled = value;
            dgvBaoTri.Enabled = value;
            dgvCTPhieuBT.Enabled = value;
        }
        private bool DieukienLuu()
        {
            if (String.IsNullOrEmpty(cbxDV.Text))
            {
                ThongBao.Show("Đơn vị không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (String.IsNullOrEmpty(cbxNV1.Text))
            {
                ThongBao.Show("Nhân viên kiểm không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (dgvCTPhieuBT == null)
            {
                ThongBao.Show("Chưa có thiết bị nào cần bảo trì", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion

        #region Sự Kiện
        private void frmPhieuBaoTri_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaPBT.Text = funtions.SDienMaTuDong("PBT");
                    dpkNgaylap.Value = DateTime.Now;
                    dpkTuNgay.Value = DateTime.Now;
                    dpkDenNgay.Value = DateTime.Now;
                    break;
                case "Xem":
                    LoadData();
                    EnabledControls(false);
                    dgvCTPhieuBT.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    dgvCTPhieuBT.AllowUserToDeleteRows = false;
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

                        if (PhieuBaoTriDAO.Instance.Them(txtMaPBT.Text, ngaylap, cbxNV1.Text, dpkTuNgay.Value.ToString("MM/dd/yyyy"), dpkDenNgay.Value.ToString("MM/dd/yyyy"), cbxDV.Text))
                        {
                            int i = 0;
                            for (i = 0; i < dgvCTPhieuBT.RowCount; i++)
                            {
                                string matb = dgvCTPhieuBT.Rows[i].Cells[0].Value.ToString();
                                string noidung = "";
                                if (dgvCTPhieuBT.Rows[i].Cells[2].Value != null)
                                    noidung = dgvCTPhieuBT.Rows[i].Cells[2].Value.ToString();
                                string chiphi = dgvCTPhieuBT.Rows[i].Cells[3].Value.ToString();
                                if (PhieuBaoTriDAO.Instance.ThemCT(txtMaPBT.Text, matb, noidung, chiphi))
                                {
                                    //LichSuHoatDongDAO.Instance.ThongBao(1, matb);

                                }
                            }
                            if (i == dgvCTPhieuBT.RowCount)
                            {
                                frmPhieuBaoTri_Load(new object(), new EventArgs());
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

                        if (PhieuBaoTriDAO.Instance.Sua(txtMaPBT.Text, cbxNV1.Text, dpkTuNgay.Value.ToString("MM/dd/yyyy"), dpkDenNgay.Value.ToString("MM/dd/yyyy")))
                        {
                            if (dgvCTPhieuBT.RowCount > 0)
                            {
                                int i = 0;
                                for (i = 0; i < dgvCTPhieuBT.RowCount; i++)
                                {
                                    string matb = dgvCTPhieuBT.Rows[i].Cells[0].Value.ToString();
                                    string noidung = "";
                                    if (dgvCTPhieuBT.Rows[i].Cells[2].Value != null)
                                        noidung = dgvCTPhieuBT.Rows[i].Cells[2].Value.ToString();
                                    string chiphi = funtions.RemoveChars(dgvCTPhieuBT.Rows[i].Cells[3].Value.ToString());
                                    if (PhieuBaoTriDAO.Instance.SuaCT(txtMaPBT.Text, matb, noidung, chiphi))
                                    {
                                        //LichSuHoatDongDAO.Instance.ThongBao(2, matb);                                       
                                    }

                                }
                                if (i == dgvCTPhieuBT.RowCount)
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
                phieubtList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvBaoTri.DataSource = phieubtList;
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
        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Bảo Trì").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "PBT";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
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
                        LoadDGVBaoTriTS();
                    else phieubtList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVBaoTriTS();
                    else phieubtList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVBaoTriTS();
                    else phieubtList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvBaoTri.DataSource = phieubtList;
        }

        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (dgvBaoTri.RowCount > 0)
            {
                if (dgvCTPhieuBT.RowCount == 0)
                {
                    dgvCTPhieuBT.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                }
                else
                {
                    for (i = 0; i < dgvCTPhieuBT.RowCount; i++)
                    {
                        string matb2 = dgvCTPhieuBT.Rows[i].Cells[0].Value.ToString();
                        if (ThietBiObj.Matb == matb2)
                            break;
                    }
                    if (i == dgvCTPhieuBT.RowCount)
                    {
                        dgvCTPhieuBT.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                    }
                }
            }
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            if (dgvBaoTri.RowCount > 0)
            {
                dgvCTPhieuBT.Rows.Clear();
                for (int i = 0; i < dgvBaoTri.RowCount; i++)
                {
                    string matb1 = dgvBaoTri.Rows[i].Cells[0].Value.ToString();
                    string tentb = dgvBaoTri.Rows[i].Cells[1].Value.ToString();
                    dgvCTPhieuBT.Rows.Add(matb1, tentb);
                }
            }
        }

        private void dgvBaoTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvBaoTri.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvBaoTri.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgvCTPhieuBT_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (int.TryParse(dgvCTPhieuBT.Rows[e.RowIndex].Cells[3].Value.ToString(), out int n))
                {

                }
                else dgvCTPhieuBT.Rows[e.RowIndex].Cells[3].Value = 0;
            }
        }



        #endregion
    }
}
