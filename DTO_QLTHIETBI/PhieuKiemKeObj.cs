using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuKiemKeObj
    {
        private static string mapkk;
        private static string ngaykk;
        private static string ngaylap;
        private static string donvi;
        private static string tongsl;
        private static string tongNG;
        private static string tongGTCL;
        private static string nguoikk1;
        private static string nguoikk2;
        private static string nguoikk3;

        public PhieuKiemKeObj(string mapkk, string ngaykk, string ngaylap, string donvi, string tongsl, string tongNG, string tongGTCL, string nguoikk1, string nguoikk2, string nguoikk3)
        {
            Mapkk = mapkk;
            Ngaykk = ngaykk;
            Ngaylap = ngaylap;
            Donvi = donvi;
            Tongsl = tongsl;
            TongNG = tongNG;
            TongGTCL = tongGTCL;
            Nguoikk1 = nguoikk1;
            Nguoikk2 = nguoikk2;
            Nguoikk3 = nguoikk3;
        }

        public static string Mapkk { get => mapkk; set => mapkk = value; }
        public static string Ngaykk { get => ngaykk; set => ngaykk = value; }
        public static string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public static string Tongsl { get => tongsl; set => tongsl = value; }
        public static string TongNG { get => tongNG; set => tongNG = value; }
        public static string TongGTCL { get => tongGTCL; set => tongGTCL = value; }
        public static string Nguoikk1 { get => nguoikk1; set => nguoikk1 = value; }
        public static string Nguoikk2 { get => nguoikk2; set => nguoikk2 = value; }
        public static string Nguoikk3 { get => nguoikk3; set => nguoikk3 = value; }
        public static string Donvi { get => donvi; set => donvi = value; }
    }
}
