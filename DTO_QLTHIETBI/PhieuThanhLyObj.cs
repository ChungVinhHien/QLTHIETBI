using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuThanhLyObj
    {
        private static string maptl;
        private static string matb;
        private static string tentb;
        private static string ngaylap;
        private static string tennv;
        private static string ngaytl;
        private static string chiphitl;
        private static string gtthuhoi;

        public PhieuThanhLyObj(string maptl, string matb,string tentb,string ngaylap, string tennv,string ngaytl, string chiphitl, string gtthuhoi)
        {
            Maptl = maptl;
            Matb = matb;
            Tentb = tentb;
            Tennv = tennv;
            Ngaylap = ngaylap;
            Ngaytl = ngaytl;
            Chiphitl = chiphitl;
            Gtthuhoi = gtthuhoi;
        }

        public static string Maptl { get => maptl; set => maptl = value; }
        public static string Tentb { get => tentb; set => tentb = value; }
        public static string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public static string Tennv { get => tennv; set => tennv = value; }
        public static string Ngaytl { get => ngaytl; set => ngaytl = value; }
        public static string Chiphitl { get => chiphitl; set => chiphitl = value; }
        public static string Gtthuhoi { get => gtthuhoi; set => gtthuhoi = value; }
        public static string Matb { get => matb; set => matb = value; }
    }
}
