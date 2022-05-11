using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QLTHIETBI
{
    public partial class ThongBao : Form
    {
        private const int CS_DROPSHADOW = 0x00020000;
        private static ThongBao _msgBox;
        private Panel _plHeader = new Panel();
        private Panel _plFooter = new Panel();
        private Panel _plIcon = new Panel();
        private PictureBox _picIcon = new PictureBox();
        private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        private Label _lblTitle;
        private Label _lblMessage;
        private List<Button> _buttonCollection = new List<Button>();
        private static DialogResult _buttonResult = new DialogResult();
        private static Timer _timer;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        public ThongBao()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Width = 400;

            _lblTitle = new Label();
            _lblTitle.ForeColor = Color.White;
            _lblTitle.Font = new System.Drawing.Font("Segoe UI", 18);
            _lblTitle.Dock = DockStyle.Top;
            _lblTitle.Height = 50;

            _lblMessage = new Label();
            _lblMessage.ForeColor = Color.White;
            _lblMessage.Font = new System.Drawing.Font("Segoe UI", 10);
            _lblMessage.Dock = DockStyle.Fill;

            _flpButtons.FlowDirection = FlowDirection.RightToLeft;
            _flpButtons.Dock = DockStyle.Fill;

            _plHeader.Dock = DockStyle.Fill;
            _plHeader.Padding = new Padding(20);
            _plHeader.Controls.Add(_lblMessage);
            _plHeader.Controls.Add(_lblTitle);

            _plFooter.Dock = DockStyle.Bottom;
            _plFooter.Padding = new Padding(0, 10, 10, 0);
            _plFooter.BackColor = Color.FromArgb(37, 37, 38);
            _plFooter.Height = 50;
            _plFooter.Controls.Add(_flpButtons);

            _picIcon.Width = 32;
            _picIcon.Height = 32;
            _picIcon.Location = new Point(25, 20);

            _plIcon.Dock = DockStyle.Left;
            _plIcon.Padding = new Padding(20);
            _plIcon.Width = 60;
            _plIcon.Controls.Add(_picIcon);

            this.Controls.Add(_plHeader);
            this.Controls.Add(_plIcon);
            this.Controls.Add(_plFooter);
        }

        public static void Loading()
        {
            MessageBox.Show("Loading");
        }
        public static void Show(string message)
        {
            _msgBox = new ThongBao();
            _msgBox._lblMessage.Text = message;
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static void Show(string message, string title)
        {
            _msgBox = new ThongBao();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Size = ThongBao.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static DialogResult Show(string message, string title, Buttons buttons)
        {
            _msgBox = new ThongBao();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox._plIcon.Hide();

            ThongBao.InitButtons(buttons);

            _msgBox.Size = ThongBao.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons, Icon icon)
        {
            _msgBox = new ThongBao();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;

            ThongBao.InitButtons(buttons);
            ThongBao.InitIcon(icon);

            _msgBox.Size = ThongBao.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons, Icon icon, AnimateStyle style)
        {
            _msgBox = new ThongBao();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Height = 0;

            ThongBao.InitButtons(buttons);
            ThongBao.InitIcon(icon);

            _timer = new Timer();
            Size formSize = ThongBao.MessageSize(message);

            switch (style)
            {
                case ThongBao.AnimateStyle.SlideDown:
                    _msgBox.Size = new Size(formSize.Width, 0);
                    _timer.Interval = 1;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case ThongBao.AnimateStyle.FadeIn:
                    _msgBox.Size = formSize;
                    _msgBox.Opacity = 0;
                    _timer.Interval = 20;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case ThongBao.AnimateStyle.ZoomIn:
                    _msgBox.Size = new Size(formSize.Width + 100, formSize.Height + 100);
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    _timer.Interval = 1;
                    break;
            }

            _timer.Tick += timer_Tick;
            _timer.Start();

            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            AnimateMsgBox animate = (AnimateMsgBox)timer.Tag;

            switch (animate.Style)
            {
                case ThongBao.AnimateStyle.SlideDown:
                    if (_msgBox.Height < animate.FormSize.Height)
                    {
                        _msgBox.Height += 17;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case ThongBao.AnimateStyle.FadeIn:
                    if (_msgBox.Opacity < 1)
                    {
                        _msgBox.Opacity += 0.1;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case ThongBao.AnimateStyle.ZoomIn:
                    if (_msgBox.Width > animate.FormSize.Width)
                    {
                        _msgBox.Width -= 17;
                        _msgBox.Invalidate();
                    }
                    if (_msgBox.Height > animate.FormSize.Height)
                    {
                        _msgBox.Height -= 17;
                        _msgBox.Invalidate();
                    }
                    break;
            }
        }

        private static void InitButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case ThongBao.Buttons.AbortRetryIgnore:
                    _msgBox.InitAbortRetryIgnoreButtons();
                    break;

                case ThongBao.Buttons.OK:
                    _msgBox.InitOKButton();
                    break;

                case ThongBao.Buttons.OKCancel:
                    _msgBox.InitOKCancelButtons();
                    break;

                case ThongBao.Buttons.RetryCancel:
                    _msgBox.InitRetryCancelButtons();
                    break;

                case ThongBao.Buttons.YesNo:
                    _msgBox.InitYesNoButtons();
                    break;

                case ThongBao.Buttons.YesNoCancel:
                    _msgBox.InitYesNoCancelButtons();
                    break;
            }

            foreach (Button btn in _msgBox._buttonCollection)
            {
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(40, 96, 144);
                btn.Font = new System.Drawing.Font("Segoe UI", 8);
                btn.Padding = new Padding(3);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Height = 30;
                btn.FlatAppearance.BorderColor = Color.FromArgb(40, 96, 144);

                _msgBox._flpButtons.Controls.Add(btn);
            }
        }

        private static void InitIcon(Icon icon)
        {
            switch (icon)
            {
                case ThongBao.Icon.Application:
                    _msgBox._picIcon.Image = SystemIcons.Application.ToBitmap();
                    break;

                case ThongBao.Icon.Exclamation:
                    _msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;

                case ThongBao.Icon.Error:
                    _msgBox._picIcon.Image = SystemIcons.Error.ToBitmap();
                    break;

                case ThongBao.Icon.Info:
                    _msgBox._picIcon.Image = SystemIcons.Information.ToBitmap();
                    break;

                case ThongBao.Icon.Question:
                    _msgBox._picIcon.Image = SystemIcons.Question.ToBitmap();
                    break;

                case ThongBao.Icon.Shield:
                    _msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap();
                    break;

                case ThongBao.Icon.Warning:
                    _msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }
        }

        private void InitAbortRetryIgnoreButtons()
        {
            Button btnAbort = new Button();
            btnAbort.Text = "Abort";
            btnAbort.Click += ButtonClick;

            Button btnRetry = new Button();
            btnRetry.Text = "Retry";
            btnRetry.Click += ButtonClick;

            Button btnIgnore = new Button();
            btnIgnore.Text = "Ignore";
            btnIgnore.Click += ButtonClick;

            this._buttonCollection.Add(btnAbort);
            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnIgnore);
        }

        private void InitOKButton()
        {
            Button btnOK = new Button();
            btnOK.Text = "Đồng Ý";
            btnOK.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
        }

        private void InitOKCancelButtons()
        {
            Button btnOK = new Button();
            btnOK.Text = "Đồng Ý";
            btnOK.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Hủy Bỏ";
            btnCancel.Click += ButtonClick;


            this._buttonCollection.Add(btnOK);
            this._buttonCollection.Add(btnCancel);
        }

        private void InitRetryCancelButtons()
        {
            Button btnRetry = new Button();
            btnRetry.Text = "Đồng Ý";
            btnRetry.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Hủy Bỏ";
            btnCancel.Click += ButtonClick;


            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnCancel);
        }

        private void InitYesNoButtons()
        {
            Button btnYes = new Button();
            btnYes.Text = "Có";
            btnYes.Click += ButtonClick;
            btnYes.MouseHover += MouseHover;
            btnYes.MouseLeave += MouseLeave;

            Button btnNo = new Button();
            btnNo.Text = "Không";
            btnNo.Click += ButtonClick;
            btnNo.MouseHover += MouseHover;
            btnNo.MouseLeave += MouseLeave;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
        }


        private void MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = btn.FlatAppearance.BorderColor = Color.FromArgb(40, 96, 144);
        }

        private void MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = btn.FlatAppearance.BorderColor = Color.DodgerBlue;
        }

        private void InitYesNoCancelButtons()
        {
            Button btnYes = new Button();
            btnYes.Text = "Abort";
            btnYes.Click += ButtonClick;

            Button btnNo = new Button();
            btnNo.Text = "Retry";
            btnNo.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Hủy Bỏ";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
            this._buttonCollection.Add(btnCancel);
        }

        private static void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Abort":
                    _buttonResult = DialogResult.Abort;
                    break;

                case "Retry":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "Ignore":
                    _buttonResult = DialogResult.Ignore;
                    break;

                case "Đồng Ý":
                    _buttonResult = DialogResult.OK;
                    break;

                case "Hủy Bỏ":
                    _buttonResult = DialogResult.Cancel;
                    break;

                case "Có":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "Không":
                    _buttonResult = DialogResult.No;
                    break;
            }

            _msgBox.Dispose();
        }

        private static Size MessageSize(string message)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 350;
            int height = 200;

            SizeF size = g.MeasureString(message, new System.Drawing.Font("Segoe UI", 10));

            if (message.Length < 150)
            {
                if ((int)size.Width > 350)
                {
                    width = (int)size.Width;
                }
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length + 1;
                width = 700;
                height += (int)(size.Height + 10) * lines;
            }
            return new Size(width, height);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1));
            Pen pen = new Pen(Color.Black);

            g.DrawRectangle(pen, rect);
        }

        public enum Buttons
        {
            AbortRetryIgnore = 1,
            OK = 2,
            OKCancel = 3,
            RetryCancel = 4,
            YesNo = 5,
            YesNoCancel = 6
        }

        public enum Icon
        {
            Application = 1,
            Exclamation = 2,
            Error = 3,
            Warning = 4,
            Info = 5,
            Question = 6,
            Shield = 7,
            Search = 8
        }

        public enum AnimateStyle
        {
            SlideDown = 1,
            FadeIn = 2,
            ZoomIn = 3
        }

    }

    class AnimateMsgBox
    {
        public Size FormSize;
        public ThongBao.AnimateStyle Style;

        public AnimateMsgBox(Size formSize, ThongBao.AnimateStyle style)
        {
            this.FormSize = formSize;
            this.Style = style;
        }
    }
}
