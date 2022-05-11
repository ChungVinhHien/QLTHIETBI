using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuSuaChuaObj
    {
        private static string maphieusc;
        private static string ngaylap;
        private static string tungay;
        private static string denngay;
        private static string donvi;
        private static string nhanvien;
        private static string nguoisc;
        private static string soluong;
        private static string tongtien;

        public PhieuSuaChuaObj(string maphieusc, string ngaylap, string tungay, string denngay, string donvi,string nhanvien, string nguoisc, string soluong, string tongtien)
        {
            Maphieusc = maphieusc;
            Ngaylap = ngaylap;
            Tungay = tungay;
            Denngay = denngay;
            Donvi = donvi;
            Nhanvien = nhanvien;
            Nguoisc = nguoisc;
            Soluong = soluong;
            Tongtien = tongtien;
        }
        public static string Maphieusc { get => maphieusc; set => maphieusc = value; }
        public static string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public static string Tungay { get => tungay; set => tungay = value; }
        public static string Denngay { get => denngay; set => denngay = value; }
        public static string Nhanvien { get => nhanvien; set => nhanvien = value; }
        public static string Nguoisc { get => nguoisc; set => nguoisc = value; }
        public static string Soluong { get => soluong; set => soluong = value; }
        public static string Tongtien { get => tongtien; set => tongtien = value; }
        public static string Donvi { get => donvi; set => donvi = value; }
    }
}
