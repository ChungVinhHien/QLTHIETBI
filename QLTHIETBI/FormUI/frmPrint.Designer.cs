
namespace QLTHIETBI
{
    partial class frmPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrint));
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel = new System.Windows.Forms.Panel();
            this.BunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.cbxNam = new Bunifu.UI.WinForms.BunifuDropdown();
            this.txtThoiGian = new Bunifu.UI.WinForms.BunifuLabel();
            this.cbxThang = new Bunifu.UI.WinForms.BunifuDropdown();
            this.panel2.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.reportViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(836, 697);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer
            // 
            this.reportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(836, 697);
            this.reportViewer.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.BunifuLabel2);
            this.panel.Controls.Add(this.cbxNam);
            this.panel.Controls.Add(this.txtThoiGian);
            this.panel.Controls.Add(this.cbxThang);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(836, 52);
            this.panel.TabIndex = 1;
            this.panel.Visible = false;
            // 
            // BunifuLabel2
            // 
            this.BunifuLabel2.AllowParentOverrides = false;
            this.BunifuLabel2.AutoEllipsis = false;
            this.BunifuLabel2.CursorType = null;
            this.BunifuLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BunifuLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.BunifuLabel2.Location = new System.Drawing.Point(138, 12);
            this.BunifuLabel2.Name = "BunifuLabel2";
            this.BunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BunifuLabel2.Size = new System.Drawing.Size(31, 16);
            this.BunifuLabel2.TabIndex = 122;
            this.BunifuLabel2.Text = "Năm:";
            this.BunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cbxNam
            // 
            this.cbxNam.BackColor = System.Drawing.Color.Transparent;
            this.cbxNam.BackgroundColor = System.Drawing.Color.White;
            this.cbxNam.BorderColor = System.Drawing.Color.Silver;
            this.cbxNam.BorderRadius = 1;
            this.cbxNam.Color = System.Drawing.Color.Silver;
            this.cbxNam.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.cbxNam.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cbxNam.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cbxNam.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cbxNam.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cbxNam.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.cbxNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxNam.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.cbxNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNam.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.cbxNam.FillDropDown = true;
            this.cbxNam.FillIndicator = false;
            this.cbxNam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxNam.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxNam.ForeColor = System.Drawing.Color.Black;
            this.cbxNam.FormattingEnabled = true;
            this.cbxNam.Icon = null;
            this.cbxNam.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.cbxNam.IndicatorColor = System.Drawing.Color.Gray;
            this.cbxNam.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.cbxNam.ItemBackColor = System.Drawing.Color.White;
            this.cbxNam.ItemBorderColor = System.Drawing.Color.White;
            this.cbxNam.ItemForeColor = System.Drawing.Color.Black;
            this.cbxNam.ItemHeight = 26;
            this.cbxNam.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.cbxNam.ItemHighLightForeColor = System.Drawing.Color.White;
            this.cbxNam.ItemTopMargin = 3;
            this.cbxNam.Location = new System.Drawing.Point(183, 7);
            this.cbxNam.Name = "cbxNam";
            this.cbxNam.Size = new System.Drawing.Size(99, 32);
            this.cbxNam.TabIndex = 121;
            this.cbxNam.Text = null;
            this.cbxNam.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.cbxNam.TextLeftMargin = 5;
            this.cbxNam.SelectedIndexChanged += new System.EventHandler(this.cbxNam_SelectedIndexChanged);
            // 
            // txtThoiGian
            // 
            this.txtThoiGian.AllowParentOverrides = false;
            this.txtThoiGian.AutoEllipsis = false;
            this.txtThoiGian.CursorType = null;
            this.txtThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtThoiGian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.txtThoiGian.Location = new System.Drawing.Point(13, 12);
            this.txtThoiGian.Name = "txtThoiGian";
            this.txtThoiGian.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtThoiGian.Size = new System.Drawing.Size(39, 16);
            this.txtThoiGian.TabIndex = 120;
            this.txtThoiGian.Text = "Tháng:";
            this.txtThoiGian.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtThoiGian.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cbxThang
            // 
            this.cbxThang.BackColor = System.Drawing.Color.Transparent;
            this.cbxThang.BackgroundColor = System.Drawing.Color.White;
            this.cbxThang.BorderColor = System.Drawing.Color.Silver;
            this.cbxThang.BorderRadius = 1;
            this.cbxThang.Color = System.Drawing.Color.Silver;
            this.cbxThang.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.cbxThang.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cbxThang.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cbxThang.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cbxThang.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cbxThang.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.cbxThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxThang.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.cbxThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxThang.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.cbxThang.FillDropDown = true;
            this.cbxThang.FillIndicator = false;
            this.cbxThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxThang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbxThang.ForeColor = System.Drawing.Color.Black;
            this.cbxThang.FormattingEnabled = true;
            this.cbxThang.Icon = null;
            this.cbxThang.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.cbxThang.IndicatorColor = System.Drawing.Color.Gray;
            this.cbxThang.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.cbxThang.ItemBackColor = System.Drawing.Color.White;
            this.cbxThang.ItemBorderColor = System.Drawing.Color.White;
            this.cbxThang.ItemForeColor = System.Drawing.Color.Black;
            this.cbxThang.ItemHeight = 26;
            this.cbxThang.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.cbxThang.ItemHighLightForeColor = System.Drawing.Color.White;
            this.cbxThang.ItemTopMargin = 3;
            this.cbxThang.Location = new System.Drawing.Point(58, 7);
            this.cbxThang.Name = "cbxThang";
            this.cbxThang.Size = new System.Drawing.Size(59, 32);
            this.cbxThang.TabIndex = 2;
            this.cbxThang.Text = null;
            this.cbxThang.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.cbxThang.TextLeftMargin = 5;
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê - báo cáo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrint_Load);
            this.panel2.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel;
        private Bunifu.UI.WinForms.BunifuDropdown cbxThang;
        private Bunifu.UI.WinForms.BunifuLabel txtThoiGian;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Bunifu.UI.WinForms.BunifuLabel BunifuLabel2;
        private Bunifu.UI.WinForms.BunifuDropdown cbxNam;
    }
}