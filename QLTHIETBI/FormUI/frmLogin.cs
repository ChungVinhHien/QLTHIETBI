using DAL_QLTHIETBI;
using DTO_QLTHIETBI;
using QLTHIETBI.FormUI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class frmLogin : Form
    {
        TaikhoanDAO taikhoan = new TaikhoanDAO();
        private int i = 0;
        PictureBox pb;
        public frmLogin()
        {
            if (PhuongPhapDAO.Instance.CheckPhuongPhap())
            {
                InitializeComponent();
            }
            else
            {
                new frmPhuongPhap().ShowDialog();
            }
        }

        #region Method

        void VisibleThongbao()
        {
            txtThongbao1.Visible = false;
            txtThongbao2.Visible = false;
            txtThongbao3.Visible = false;
        }
        void Blur()
        {
            Bitmap bmp = Screenshot.TakeSnapshot(this);
            //BitmapFilter.GaussianBlur(bmp, 4);
            Bitmap bitmap = BitmapFilter.AdjustBrightness(bmp, 0.75f);

            if (pb != null)
            {
                this.Controls.Remove(pb);
            }
            pb = new PictureBox();
            this.Controls.Add(pb);
            pb.Image = bitmap;
            pb.Dock = DockStyle.Fill;
            pb.BringToFront();
        }
        #endregion

        #region Event
        private void chxShow_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (chxShow.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else txtPassword.UseSystemPasswordChar = true;
        }

        private void txtPw_TextChange(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.PlaceholderText = "Password";
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                if (chxShow.Checked) txtPassword.UseSystemPasswordChar = false;
                else txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void pnlForgetPw_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaikhoanObj.Username = txtUsername.Text;
            frmResetPw resetPw = new frmResetPw();

            Blur();

            TrangThaiObj.Trangthai = "";
            timer2.Start();
            resetPw.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtThongbao1.Visible = true;
                txtUsername.BorderColorIdle = Color.Red;
                txtUsername.Focus();
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtThongbao2.Visible = true;
                txtPassword.BorderColorIdle = Color.Red;
                txtPassword.Focus();
            }
            else if (taikhoan.Login(username, password))
            {
                Bitmap bmp = Screenshot.TakeSnapshot(this);
                BitmapFilter.GaussianBlur(bmp, 4);

                if (pb != null)
                {
                    this.Controls.Remove(pb);
                }
                pb = new PictureBox();
                this.Controls.Add(pb);
                pb.Image = bmp;
                pb.Dock = DockStyle.Fill;
                pb.BringToFront();

                DataTable dt = new DataTable();
                dt = TaikhoanDAO.Instance.GetTaiKhoan(txtUsername.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TaikhoanObj.Username = dt.Rows[0][0].ToString();
                    TaikhoanObj.Password = dt.Rows[0][1].ToString();
                    TaikhoanObj.Is_admin = dt.Rows[0][2].ToString();
                    TaikhoanObj.Is_active = dt.Rows[0][3].ToString();
                    TaikhoanObj.Ngaytao = dt.Rows[0][4].ToString();
                    TaikhoanObj.Email = dt.Rows[0][5].ToString();
                }

                TaikhoanDAO.Instance.UpdateIsActive(TaikhoanObj.Username, "1");

                TrangThaiObj.Trangthai = "0";
                btnLogin.Enabled = false;
                Program.th2.Start();

                pic1.Visible = pic2.Visible = pic3.Visible = pic4.Visible = true;
                pic1.BringToFront(); pic2.BringToFront(); pic3.BringToFront(); pic4.BringToFront();
                timer1.Start();

            }
            else
            {
                txtThongbao3.Visible = true;
            }
        }


        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            VisibleThongbao();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            VisibleThongbao();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (ThongBao.Show("Bạn có chắc chắn muốn đóng phần mềm không?", "Thoát?", ThongBao.Buttons.YesNo, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void ChangeBgPic(PictureBox pic)
        {
            if (pic.BackColor == Color.FromArgb(40, 96, 144))
            {
                pic.BackColor = Color.FromArgb(37, 46, 59);
            }
            else pic.BackColor = Color.FromArgb(40, 96, 144);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            i++;
            switch (i)
            {
                case 1:
                    ChangeBgPic(pic1);
                    break;
                case 2:
                    ChangeBgPic(pic2);
                    break;
                case 3:
                    ChangeBgPic(pic3);
                    break;
                case 4:
                    ChangeBgPic(pic4);
                    break;
            }
            if (i > 4) i = 0;
            if (TrangThaiObj.Trangthai == "1")
                this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (TrangThaiObj.Trangthai == "close")
            {
                this.Controls.Remove(pb);
                pb.SendToBack();
                timer2.Stop();
            }
        }
        #endregion
    }
}
