using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmPhieuKhauHao : Form
    {
        BindingSource phieukhList = new BindingSource();
        public frmPhieuKhauHao()
        {
            InitializeComponent();
            TrangThaiObj.Trangthai = "open";
            LoadData();
        }
        #region Phương thức

        void LoadData()
        {
            phieukhList.DataSource = PhieuKhauHaoDAO.Instance.GetDataCTPhieuKhauHao(PhieuKhauHaoObj.Mapkh);
            dgvCTPhieuKH.DataSource = phieukhList;
        }

        #endregion

        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }
    }
}
