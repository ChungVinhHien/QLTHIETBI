using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;


namespace QLTHIETBI
{
    public partial class frmPhieuThanhLy : Form
    {
        MyFuntions funtions = new MyFuntions();
        BindingSource phieutlList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        int column;
        TextBox txtvalue;
        public frmPhieuThanhLy()
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
            txtMaPTL.Text = PhieuThanhLyObj.Maptl;
            dpkNgaylap.Value = DateTime.ParseExact(PhieuThanhLyObj.Ngaylap.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dpkNgayTL.Value = DateTime.ParseExact(PhieuThanhLyObj.Ngaytl.Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            txtNV1.Text = PhieuThanhLyObj.Tennv;
            cbxNV1.Text = NhanVienDAO.Instance.GetDataNhanVienbyTen(txtNV1.Text).Rows[0][0].ToString();

            thietbiList.DataSource = PhieuThanhLyDAO.Instance.GetDataPhieuThanhLy(PhieuThanhLyObj.Maptl);
            dgvCTPhieuTL.DataSource = thietbiList;
        }
        void LoadDGVThanhLyTS()
        {
            phieutlList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvThanhLy.DataSource = phieutlList;
        }
        void ResetForm()
        {
            dpkNgayTL.Value = DateTime.Now;
            cbxNV1.SelectedIndex = -1; txtNV1.Clear();
            cbxDV.SelectedIndex = -1; txtDV.Clear();
            dgvThanhLy.DataSource = null;
            dgvCTPhieuTL.Rows.Clear();
        }
        void EnabledControls(bool value)
        {
            dpkNgayTL.Enabled = value;
            cbxNV1.Enabled = value; txtNV1.Enabled = value;
            cbxDV.Enabled = value; txtDV.Enabled = value;
            btnMove.Enabled = value;
            dgvThanhLy.Enabled = value;
            dgvCTPhieuTL.Enabled = value;
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
                ThongBao.Show("Nhân viên không được trống", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }
            else if (dgvCTPhieuTL == null)
            {
                ThongBao.Show("Chưa có thiết bị nào cần bảo trì", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                return false;
            }

            return true;
        }
        #endregion

        #region Sự kiện
        private void frmPhieuThanhLy_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    txtMaPTL.Text = funtions.SDienMaTuDong("PTL");
                    dpkNgaylap.Value = DateTime.Now;
                    dpkNgayTL.Value = DateTime.Now;
                    break;
                case "Xem":
                    LoadData();
                    EnabledControls(false);
                    dgvCTPhieuTL.AllowUserToDeleteRows = false;
                    linkLuu.Visible = false;
                    break;
                case "Sửa":
                    LoadData();
                    dgvCTPhieuTL.AllowUserToDeleteRows = false;
                    btnMove.Enabled = false;
                    break;
            }
        }

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PhanQuyenDAO.Instance.GetChiTietQuyen(TaikhoanObj.Username, "Phiếu Thanh Lý").Rows[0][4].ToString() == "True")
            {
                HoatDongObj.Noidung = "PTL";
                frmPrint print = new frmPrint();
                print.ShowDialog();
            }
            else ThongBao.Show("Bạn không có quyền in dữ liệu này!", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);

        }

        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ngaylap = DateTime.Now.ToString("MM/dd/yyyy");
            string matb = dgvCTPhieuTL.Rows[0].Cells[0].Value.ToString();
            string chiphi = dgvCTPhieuTL.Rows[0].Cells[2].Value.ToString();
            string giatri = dgvCTPhieuTL.Rows[0].Cells[3].Value.ToString();
            if (DieukienLuu())
            {
                switch (HoatDongObj.Noidung)
                {
                    case "Thêm":

                        if (PhieuThanhLyDAO.Instance.Them(txtMaPTL.Text, matb, dpkNgayTL.Value.ToString("MM/dd/yyyy"), ngaylap, cbxNV1.Text, chiphi, giatri))
                        {
                            frmPhieuThanhLy_Load(new object(), new EventArgs());
                            ResetForm();
                            ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Error, ThongBao.AnimateStyle.FadeIn);
                        }
                        break;

                    case "Sửa":
                        if (PhieuThanhLyDAO.Instance.Sua(txtMaPTL.Text, matb, dpkNgayTL.Value.ToString("MM/dd/yyyy"), chiphi, cbxNV1.Text))
                        {
                            ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                            LoadData();
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
                phieutlList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvThanhLy.DataSource = phieutlList;
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
                        LoadDGVThanhLyTS();
                    else phieutlList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVThanhLyTS();
                    else phieutlList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadDGVThanhLyTS();
                    else phieutlList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvThanhLy.DataSource = phieutlList;
        }

        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
        }
        private void dgvThanhLy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvThanhLy.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvThanhLy.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void btnMove_Click(object sender, EventArgs e)
        {
            if (dgvThanhLy.RowCount > 0)
            {
                if (dgvCTPhieuTL.RowCount == 0)
                {
                    dgvCTPhieuTL.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                }
            }
        }

        private void dgvCTPhieuTL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvCTPhieuTL.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvCTPhieuTL.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgvCTPhieuTL_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (int.TryParse(dgvCTPhieuTL.Rows[e.RowIndex].Cells[2].Value.ToString(), out int n))
                {
                    string gtcl = ThietBiDAO.Instance.GetDataNguyenGia_GTHaoMon(ThietBiObj.Matb).Rows[0][1].ToString();
                    dgvCTPhieuTL.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(dgvCTPhieuTL.Rows[e.RowIndex].Cells[2].Value.ToString()) - Convert.ToInt32(gtcl);
                }
                else dgvCTPhieuTL.Rows[e.RowIndex].Cells[2].Value = 0;
            }
        }

        private void dgvCTPhieuTL_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgvCTPhieuTL.Rows[e.RowIndex].Cells[3].ReadOnly = true;
        }


        #endregion


    }
}
