using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class HoatDongObj
    {
        private static string mahd;
        private static string noidung;

        public HoatDongObj(string mahd, string noidung)
        {
            Mahd = mahd;
            Noidung = noidung;
        }
        public static string Mahd { get => mahd; set => mahd = value; }
        public static string Noidung { get => noidung; set => noidung = value; }
    }
}
