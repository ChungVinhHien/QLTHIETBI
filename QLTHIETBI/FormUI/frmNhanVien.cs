using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmNhanVien : Form
    {
        MyFuntions funtions = new MyFuntions();
        private string filepath = " ", gioitinh = "", Data = "";
        public frmNhanVien()
        {
            InitializeComponent();
            LoadCombobox();
            TrangThaiObj.Trangthai = "open";
        }
        #region Phương thức
        void LoadCombobox()
        {
            cbxPhongBan.DataSource = PhongBanDAO.Instance.GetDataPhongBan();
            cbxPhongBan.DisplayMember = "TENPB";
            cbxPhongBan.ValueMember = "MAPB";

            cbxChucVu.DataSource = ChucVuDAO.Instance.GetDataChucVu();
            cbxChucVu.DisplayMember = "TENCV";
            cbxChucVu.ValueMember = "MACV";
        }

        void LoadData()
        {
            lblTittle.Text = NhanVienObj.Manv;
            txtTenNV.Text = NhanVienObj.Tennv;
            txtDiaChi.Text = NhanVienObj.Diachinv;
            txtEmail.Text = NhanVienObj.Emailnv;
            txtSdt.Text = NhanVienObj.Sdtnv;
            cbxPhongBan.Text = NhanVienObj.Tenpb;
            cbxChucVu.Text = NhanVienObj.Tencv;
            dpkNgaySinh.Text = NhanVienObj.Ngaysinhnv;
            if (NhanVienObj.Gioitinhnv == "Nam")
            {
                rdbtnNam.Checked = true;
                rdbtnNu.Checked = false;
            }
            else
            {
                rdbtnNu.Checked = true;
                rdbtnNam.Checked = false;
            }
            if (!String.IsNullOrEmpty(NhanVienObj.Hinhanh))
                picUser.Image = new MyFuntions().byteArrayToImage(Convert.FromBase64String(NhanVienObj.Hinhanh));
        }
        void EnableControls(bool value)
        {
            lblTittle.Enabled = value;
            txtTenNV.Enabled = value;
            txtDiaChi.Enabled = value;
            txtEmail.Enabled = value;
            txtSdt.Enabled = value;
            cbxPhongBan.Enabled = value;
            cbxChucVu.Enabled = value;
            dpkNgaySinh.Enabled = value;
            rdbtnNam.Enabled = value;
            rdbtnNu.Enabled = value;

        }
        #endregion

        #region Sự kiện
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    lblTittle.Text = funtions.SDienMaTuDong("NV");
                    break;
                case "Xem":
                    LoadData();
                    linkLuu.Visible = false;
                    panel1.Visible = false;
                    EnableControls(false);
                    break;
                case "Sửa":
                    LoadData();
                    break;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string filepath = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            if (!String.IsNullOrEmpty(filepath))
            {
                picUser.Image = Image.FromFile(filepath);

                Image image = picUser.Image;
                byte[] array = new MyFuntions().imgToByteArray(image);
                Data = Convert.ToBase64String(array);
            }
        }
        private void linkLuu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (rdbtnNam.Checked == true)
                gioitinh = "Nam";
            else gioitinh = "Nữ";
            string ngaysinh = dpkNgaySinh.Value.ToString("MM/dd/yyyy");

            switch (HoatDongObj.Noidung)
            {
                case "Thêm":
                    if (NhanVienDAO.Instance.Them(lblTittle.Text, txtTenNV.Text, gioitinh, ngaysinh, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, cbxPhongBan.SelectedValue.ToString(), cbxChucVu.SelectedValue.ToString(), filepath))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(1, lblTittle.Text);
                        ThongBao.Show("Thêm dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi thêm dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;

                case "Sửa":
                    if (NhanVienDAO.Instance.Sua(lblTittle.Text, txtTenNV.Text, gioitinh, ngaysinh, txtDiaChi.Text, txtSdt.Text, txtEmail.Text, cbxPhongBan.SelectedValue.ToString(), cbxChucVu.SelectedValue.ToString(), Data))
                    {
                        LichSuHoatDongDAO.Instance.ThongBao(2, lblTittle.Text);
                        ThongBao.Show("Sửa dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi sửa dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                    break;
            }
        }
        private void linkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
            this.Close();
        }
        #endregion
    }
}
