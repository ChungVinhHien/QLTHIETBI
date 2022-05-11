using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmSanLuong : Form
    {
        MyFuntions funtions = new MyFuntions();
        BindingSource sanluongList = new BindingSource();
        BindingSource thietbiList = new BindingSource();
        int column;
        TextBox txtvalue;
        public frmSanLuong()
        {
            InitializeComponent();
        }
        private void frmSanLuong_Load(object sender, EventArgs e)
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
        void LoadData()
        {
            thietbiList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
            dgvThietbi.DataSource = thietbiList;
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
                thietbiList.DataSource = ThietBiDAO.Instance.GetDataByMADV(cbxDV.Text);
                dgvThietbi.DataSource = thietbiList;
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
        private void dgvTimkiem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            column = e.ColumnIndex;
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
                        frmSanLuong_Load(sender, e);
                    else sanluongList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.MATB", value);
                    break;
                case 1:
                    if (String.IsNullOrEmpty(value))
                        LoadData();
                    else sanluongList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "TB.TENTB", value);
                    break;
                case 3:
                    if (String.IsNullOrEmpty(value))
                        LoadData();
                    else sanluongList.DataSource = ThietBiDAO.Instance.TimKiemTS(cbxDV.Text, "PB.TENPB", value);
                    break;
            }
            dgvThietbi.DataSource = sanluongList;
        }
        private void dgvThietbi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ThietBiObj.Matb = dgvThietbi.Rows[e.RowIndex].Cells[0].Value.ToString();
            ThietBiObj.Tentb = dgvThietbi.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void dgvSanLuong_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (int.TryParse(dgvSanLuong.Rows[e.RowIndex].Cells[2].Value.ToString(), out int n))
                {

                }
                else dgvSanLuong.Rows[e.RowIndex].Cells[2].Value = 0;
            }
            else
            if (e.ColumnIndex == 3)
            {
                if (int.TryParse(dgvSanLuong.Rows[e.RowIndex].Cells[3].Value.ToString(), out int n))
                {

                }
                else dgvSanLuong.Rows[e.RowIndex].Cells[3].Value = 0;
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (dgvThietbi.RowCount > 0)
            {
                if (dgvSanLuong.RowCount == 0)
                {
                    dgvSanLuong.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                }
                else
                {
                    for (i = 0; i < dgvSanLuong.RowCount; i++)
                    {
                        string matb2 = dgvSanLuong.Rows[i].Cells[0].Value.ToString();
                        if (ThietBiObj.Matb == matb2)
                            break;
                    }
                    if (i == dgvSanLuong.RowCount)
                    {
                        dgvSanLuong.Rows.Add(ThietBiObj.Matb, ThietBiObj.Tentb);
                    }
                }
            }
        }

        private void btnMoveAll_Click(object sender, EventArgs e)
        {
            if (dgvThietbi.RowCount > 0)
            {
                dgvSanLuong.Rows.Clear();
                for (int i = 0; i < dgvThietbi.RowCount; i++)
                {
                    string matb1 = dgvThietbi.Rows[i].Cells[0].Value.ToString();
                    string tentb = dgvThietbi.Rows[i].Cells[1].Value.ToString();
                    dgvSanLuong.Rows.Add(matb1, tentb);
                }
            }
        }

        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ngay = DateTime.Now.ToString("MM/dd/yyyy");
            int i = 0;
            for (i = 0; i < dgvSanLuong.RowCount; i++)
            {
                string matb = dgvSanLuong.Rows[i].Cells[0].Value.ToString();
                string sanluongtk = dgvSanLuong.Rows[i].Cells[2].Value.ToString();
                string sanluong = dgvSanLuong.Rows[i].Cells[3].Value.ToString();
                string dvt = dgvSanLuong.Rows[i].Cells[4].Value.ToString();
                if (ThietBiDAO.Instance.ThemSL(ngay, matb, sanluongtk, sanluong, dvt))
                {
                    LichSuHoatDongDAO.Instance.ThongBao(1, matb);

                }
            }
            if (i == dgvSanLuong.RowCount)
            {
                frmSanLuong_Load(new object(), new EventArgs());
                ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
            }
        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }


    }
}
