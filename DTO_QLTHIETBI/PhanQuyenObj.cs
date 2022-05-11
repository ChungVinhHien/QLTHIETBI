using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhanQuyenObj
    {
        private static string taikhoan;

        public PhanQuyenObj(string taikhoan)
        {
            Taikhoan = taikhoan;
        }

        public static string Taikhoan { get => taikhoan; set => taikhoan = value; }
    }
}
