using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuBaoTriObj
    {
        private static string maphieubt;
        private static string ngaylap;
        private static string donvi;
        private static string nhanvien;
        private static string tungay;
        private static string denngay;
        private static string soluong;
        private static string tongtien;

        public PhieuBaoTriObj(string maphieubt, string ngaylap, string donvi, string nhanvien, string tungay,string denngay, string soluong, string tongtien)
        {
            Maphieubt = maphieubt;
            Ngaylap = ngaylap;
            Donvi = donvi;
            Nhanvien = nhanvien;
            Tungay = tungay;
            Denngay = denngay;
            Soluong = soluong;
            Tongtien = tongtien;
        }

        public static string Maphieubt { get => maphieubt; set => maphieubt = value; }
        public static string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public static string Tungay { get => tungay; set => tungay = value; }
        public static string Denngay { get => denngay; set => denngay = value; }
        public static string Soluong { get => soluong; set => soluong = value; }
        public static string Tongtien { get => tongtien; set => tongtien = value; }
        public static string Donvi { get => donvi; set => donvi = value; }
        public static string Nhanvien { get => nhanvien; set => nhanvien = value; }
    }
}
