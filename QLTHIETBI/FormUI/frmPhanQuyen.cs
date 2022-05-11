using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhanQuyen : Form
    {
        string formname, value;
        int scrollindex = 0;
        public frmPhanQuyen()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
        }

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            dgvPhanQuyen.DataSource = PhanQuyenDAO.Instance.GetPhanQuyen(PhanQuyenObj.Taikhoan);
        }

        private void dgvPhanQuyen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.RowIndex >= 0)
            {
                formname = dgvPhanQuyen[0, e.RowIndex].Value.ToString();
                dgvPhanQuyen[0, e.RowIndex].ReadOnly = true;
            }

            switch (e.ColumnIndex)
            {
                case 1:
                    value = dgvPhanQuyen[1, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_ALL", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 2:
                    value = dgvPhanQuyen[2, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_VIEW", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 3:
                    value = dgvPhanQuyen[3, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_ADD", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 4:
                    value = dgvPhanQuyen[4, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_EDIT", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 5:
                    value = dgvPhanQuyen[5, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_DELETE", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 6:
                    value = dgvPhanQuyen[6, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_PRINT", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 7:
                    value = dgvPhanQuyen[7, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_APPROVE", value, PhanQuyenObj.Taikhoan, formname);
                    break;
            }

            frmPhanQuyen_Load(new object(), new EventArgs());
        }

        private void dgvPhanQuyen_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) ;
            //else
            //{
            //    formname = dgvPhanQuyen[0, e.RowIndex].Value.ToString();
            //    value = dgvPhanQuyen[1, e.RowIndex].Value.ToString();
            //}
            //MessageBox.Show(formname, value);

            /*
            switch (e.ColumnIndex)
            {
                case 1:
                    value = dgvPhanQuyen[1, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_VIEW", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 2:
                    value = dgvPhanQuyen[2, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_ADD", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 3:
                    value = dgvPhanQuyen[3, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_EDIT", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 4:
                    value = dgvPhanQuyen[4, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_DELETE", value, PhanQuyenObj.Taikhoan, formname);
                    break;
                case 5:
                    value = dgvPhanQuyen[5, e.RowIndex].Value.ToString();
                    PhanQuyenDAO.Instance.Sua("IS_PRINT", value, PhanQuyenObj.Taikhoan, formname);
                    break;                
            }
            */
        }
    }
}
