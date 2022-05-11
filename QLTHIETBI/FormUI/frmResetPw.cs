using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmResetPw : Form
    {
        private int i = 45;
        Random rand = new Random();
        string sval;
        public frmResetPw()
        {
            InitializeComponent();
        }
        public string Songaunhien()
        {
            int val = rand.Next(1000, 9999);
            sval = val.ToString();
            return sval;
        }
        private bool guithu(string addressmail)
        {

            try
            {
                MailMessage msg = new MailMessage("c.v.hien99@gmail.com", addressmail, "", Songaunhien() + " là mã xác minh tài khoản của bạn");
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("c.v.hien99@gmail.com", "Chungvinhhien0108"),
                    EnableSsl = true
                };
                smtp.Send(msg);
                return true;
            }
            catch (Exception)
            {
                txtThongbao1.Visible = true;
                txtEmail.Focus();
            }
            return false;

        }


        //----------------------------------------------------- PANEL SEND EMAIL ------------------------------------------------------------------------//

        private void btnSendEMail_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text) || TaikhoanDAO.Instance.CheckEmailTaiKhoan(TaikhoanObj.Username, txtEmail.Text))
            {
                if (guithu(txtEmail.Text))
                {
                    PnlCode.Visible = true;
                    lblEmail.Text = txtEmail.Text; // Lấy địa chỉ email gán vào label email slide 2
                                                   // Đồng hồ bắt đầu đếm 
                    timer.Interval = 1000;
                    timer.Start();
                }
            }
            else
            {
                txtEmail.Focus();
                txtThongbao2.Visible = true;
            }
        }
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtThongbao1.Visible = false;
            txtThongbao2.Visible = false;
        }

        //----------------------------------------------------- PANEL VIRIFICATION CODE ------------------------------------------------------------------------//

        private void txtnum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                txtnum2.Focus();
                txtThongbao3.Visible = false;
            }
        }

        private void txtnum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                txtnum3.Focus();
                txtThongbao3.Visible = false;
            }
        }

        private void txtnum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                txtnum4.Focus();
                txtThongbao3.Visible = false;
            }
        }

        private void btnConfirmCode_Click(object sender, EventArgs e)
        {
            string random = txtnum1.Text + txtnum2.Text + txtnum3.Text + txtnum4.Text;

            if (!String.IsNullOrEmpty(txtnum1.Text) && !String.IsNullOrEmpty(txtnum2.Text) && !String.IsNullOrEmpty(txtnum3.Text) && !String.IsNullOrEmpty(txtnum4.Text))
            {
                if (sval == random)
                {
                    pnlNewPW.Visible = true;
                    timer.Stop();
                    i = 45;
                    CircleProgress.Value = 45;
                    txtnum1.Clear(); txtnum2.Clear(); txtnum3.Clear(); txtnum4.Clear();
                }
                else
                {
                    txtnum1.Clear(); txtnum2.Clear(); txtnum3.Clear(); txtnum4.Clear();
                    txtnum1.Focus();
                    txtThongbao3.Visible = true;
                }

            }
        }

        private void txtGuima_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer.Stop();
            i = 45;
            CircleProgress.Value = 45;

            timer.Interval = 1000;
            timer.Start();

            txtnum1.Clear(); txtnum2.Clear(); txtnum3.Clear(); txtnum4.Clear();
            txtnum1.Focus();

            guithu(lblEmail.Text);
        }

        //----------------------------------------------------- PANEL SET NEW PASSWORD ------------------------------------------------------------------------//

        private void txtNewPW_TextChange(object sender, EventArgs e)
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

        private void txtConfirmNPW_TextChange(object sender, EventArgs e)
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
            else if (!String.IsNullOrEmpty(txtNewPW.Text) && !String.IsNullOrEmpty(txtConfirmNPW.Text))
            {
                if (txtNewPW.Text == txtConfirmNPW.Text)
                {
                    if (TaikhoanDAO.Instance.UpdatePassword(TaikhoanObj.Username, txtNewPW.Text))
                    {
                        pnlSuccess.Visible = true;
                    }
                }
                else txtThongbao6.Visible = true;
            }
        }

        //----------------------------------------------------- PANEL LOGIN AGAIN ------------------------------------------------------------------------//

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TrangThaiObj.Trangthai = "close";
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
        private void timer_Tick(object sender, EventArgs e)
        {
            i--;
            CircleProgress.Value = i;
            if (i == 0)
            {
                timer.Stop();
                i = 45;
                Songaunhien();
                txtnum1.Focus();
                txtnum1.Clear(); txtnum2.Clear(); txtnum3.Clear(); txtnum4.Clear();
            }
        }

    }
}
