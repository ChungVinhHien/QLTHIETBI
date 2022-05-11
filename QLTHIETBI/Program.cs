using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTHIETBI
{
    static class Program
    {
        public static Thread th2;
        public static Thread th1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            th1 = new Thread(new ThreadStart(openform1));
            th1.SetApartmentState(ApartmentState.STA);
            th1.Start();

            th2 = new Thread(new ThreadStart(openform2));
            th2.SetApartmentState(ApartmentState.STA);

        }
        static void openform1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
        static void openform2()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
     
    }
}
