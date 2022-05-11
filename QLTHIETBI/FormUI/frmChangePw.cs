using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmChangePw : Form
    {
        public frmChangePw()
        {
            InitializeComponent();
        }

        private void txtNewPW_TextChanged(object sender, EventArgs e)
        {
            txtThongbao4.Visible = false;
            txtThongbao6.Visible = false;
            if (String.IsNullOrEmpty(txtNewPW.Text))
            {
                txtNewPW.PlaceholderText = "Password";
                txtNewPW.UseSystemPasswordChar = false;
                txtNewPW.PasswordChar = '\0';
            }
            else
            {
                if (chxShow.Checked) txtNewPW.UseSystemPasswordChar = false;
                else txtNewPW.UseSystemPasswordChar = true;
            }
        }

        private void txtConfirmNPW_TextChanged(object sender, EventArgs e)
        {
            txtThongbao5.Visible = false;
            txtThongbao6.Visible = false;
            if (String.IsNullOrEmpty(txtConfirmNPW.Text))
            {
                txtConfirmNPW.PlaceholderText = "Confirm password";
                txtConfirmNPW.UseSystemPasswordChar = false;
                txtConfirmNPW.PasswordChar = '\0';
            }
            else
            {
                if (chxShow.Checked) txtConfirmNPW.UseSystemPasswordChar = false;
                else txtConfirmNPW.UseSystemPasswordChar = true;
            }
        }

        private void chxShow_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (chxShow.Checked)
            {
                txtNewPW.UseSystemPasswordChar = false;
                txtNewPW.PasswordChar = '\0';

                txtConfirmNPW.UseSystemPasswordChar = false;
                txtConfirmNPW.PasswordChar = '\0';
            }
            else
            {
                txtNewPW.UseSystemPasswordChar = true;
                txtConfirmNPW.UseSystemPasswordChar = true;
            }
        }

        private void btnSetPW_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNewPW.Text))
                txtThongbao4.Visible = true;
            else if (String.IsNullOrEmpty(txtConfirmNPW.Text))
                txtThongbao5.Visible = true;
            else if (txtNewPW.Text == txtConfirmNPW.Text)
            {
                if (TaikhoanDAO.Instance.UpdatePassword(TaikhoanObj.Username, txtNewPW.Text))
                {
                    ThongBao.Show("Đổi mật khẩu thành công?", "Thông báo?", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    txtNewPW.Clear(); txtConfirmNPW.Clear();
                }
            }
            else txtThongbao6.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }
}
