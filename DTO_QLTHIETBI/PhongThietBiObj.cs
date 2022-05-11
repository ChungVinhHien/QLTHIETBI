using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class PhongThietBiObj
    {
        private static string maptb;
        private static string tenptb;
        private static string sophong;
        private static string soluong;
        private static string vitri;
        private static string trangthaiptb;
        private static string tennv;
        public PhongThietBiObj(string maptb, string tenptb, string sophong, string soluong, string vitri, string trangthaiptb, string tennv)
        {
            Maptb = maptb;
            Tenptb = tenptb;
            Sophong = sophong;
            Soluong = soluong;
            Vitri = vitri;
            Trangthaiptb = trangthaiptb;
            Tennv = tennv;
        }


        public static string Maptb { get => maptb; set => maptb = value; }
        public static string Tenptb { get => tenptb; set => tenptb = value; }
        public static string Sophong { get => sophong; set => sophong = value; }
        public static string Soluong { get => soluong; set => soluong = value; }
        public static string Vitri { get => vitri; set => vitri = value; }
        public static string Trangthaiptb { get => trangthaiptb; set => trangthaiptb = value; }
        public static string Tennv { get => tennv; set => tennv = value; }
        
    }
}
