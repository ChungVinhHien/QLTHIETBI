using DAL_QLTHIETBI;
using System;
using System.Windows.Forms;

namespace QLTHIETBI.FormUI
{
    public partial class frmPhuongPhap : Form
    {
        public frmPhuongPhap()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string mẹthod = "";
            if (chxSelect1.Checked == true) mẹthod = "1";
            else if (chxSelect2.Checked == true) mẹthod = "2";
            else if (chxSelect3.Checked == true) mẹthod = "3";

            if (!string.IsNullOrEmpty(mẹthod))
            {
                if (ThongBao.Show("Bạn có chắc chắn muốn chọn phương pháp này không?", "Thoát?", ThongBao.Buttons.YesNo, ThongBao.Icon.Question, ThongBao.AnimateStyle.FadeIn) == DialogResult.Yes)
                {
                    if (PhuongPhapDAO.Instance.Update(mẹthod))
                    {
                        ThongBao.Show("Cập nhật dữ liệu thành công", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                        Application.Restart();

                    }
                    else
                    {
                        ThongBao.Show("Có lỗi khi cập nhật dữ liệu", "Thông báo", ThongBao.Buttons.OK, ThongBao.Icon.Info, ThongBao.AnimateStyle.FadeIn);
                    }
                }
                else Application.Restart();
            }
            else Application.Restart();
        }

        private void chxSelect1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (chxSelect1.Checked == true)
            {
                btnConfirm.Enabled = true;
                chxSelect2.Checked = false;
                chxSelect3.Checked = false;
            }
        }

        private void chxSelect2_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {

            if (chxSelect2.Checked == true)
            {
                btnConfirm.Enabled = true;
                chxSelect1.Checked = false;
                chxSelect3.Checked = false;
            }
        }

        private void chxSelect3_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {

            if (chxSelect3.Checked == true)
            {
                btnConfirm.Enabled = true;
                chxSelect2.Checked = false;
                chxSelect1.Checked = false;
            }
        }

        private void frmPhuongPhap_Load(object sender, EventArgs e)
        {

        }
    }
}
