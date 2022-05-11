using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DAL_QLTHIETBI
{
    public class Screenshot 
    {
        public static Bitmap TakeSnapshot(Control ctl)
        {
            Bitmap bmp = new Bitmap(ctl.Size.Width, ctl.Size.Height);
            using (Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(
                    ctl.PointToScreen(ctl.ClientRectangle.Location),
                    new Point(0, 0), ctl.ClientRectangle.Size
                );
            }
            return bmp;
        }
    }
}
