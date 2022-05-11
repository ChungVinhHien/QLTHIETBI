using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuLuanChuyenObj
    {
        private static string maplc;
        private static string ngaylc;
        private static string ngaylap;
        private static string donvi;
        private static string tongsl;
        private static string nguoilc1;
        private static string nguoilc2;
        private static string nguoilc3;
        private static string nguoinhan;

        public PhieuLuanChuyenObj(string maplc, string ngaylc, string ngaylap,string donvi, string tongsl,string nguoilc1, string nguoilc2, string nguoilc3, string nguoinhan)
        {
            Maplc = maplc;
            Ngaylc = ngaylc;
            Ngaylap = ngaylap;
            Donvi = donvi;
            Tongsl = tongsl;
            Nguoilc1 = nguoilc1;
            Nguoilc2 = nguoilc2;
            Nguoilc3 = nguoilc3;
            Nguoinhan = nguoinhan;
         }

        public static string Maplc { get => maplc; set => maplc = value; }
        public static string Ngaylc { get => ngaylc; set => ngaylc = value; }
        public static string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public static string Donvi { get => donvi; set => donvi = value; }
        public static string Tongsl { get => tongsl; set => tongsl = value; }
        public static string Nguoilc1 { get => nguoilc1; set => nguoilc1 = value; }
        public static string Nguoilc2 { get => nguoilc2; set => nguoilc2 = value; }
        public static string Nguoilc3 { get => nguoilc3; set => nguoilc3 = value; }
        public static string Nguoinhan { get => nguoinhan; set => nguoinhan = value; }
    }
}
