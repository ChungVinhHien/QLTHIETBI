using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class ThietBiObj
    {
        private static string matb;
        private static string tentb;
        private static string nuocsx;
        private static string namsx;
        private static string tgbaohanh;
        private static string tgduavaosd;
        private static string sonamkhauhao;
        private static string thongsokythuat;
        private static string nguyengia;
        private static string ngaynhap;
        private static string mancc;
        private static string madvt;
        private static string mapb;
        private static string maloaits;
        private static string matt;
        private static string madv;

        public ThietBiObj(string matb, string tentb, string nuocsx, string namsx, string tgbaohanh, string tgduavaosd,string nguyengia,string ngaynhap,string thongsokythuat, 
            string mancc, string madvt, string mapb, string maloaits, string matt,string sonamkhauhao, string madv)
        {
            Matb = matb;
            Tentb = tentb;
            Nuocsx = nuocsx;
            Namsx = namsx;
            Tgbaohanh = tgbaohanh;
            Tgduavaosd = tgduavaosd;
            Nguyengia = nguyengia;
            Ngaynhap = ngaynhap;
            Thongsokythuat = thongsokythuat;
            Mancc = mancc;
            Madvt = madvt;
            Mapb = mapb;
            Maloaits = maloaits;
            Matt = matt;
            Sonamkhauhao = sonamkhauhao;
            Madv = madv;
        }

        public static string Matb { get => matb; set => matb = value; }
        public static string Tentb { get => tentb; set => tentb = value; }
        public static string Nuocsx { get => nuocsx; set => nuocsx = value; }
        public static string Namsx { get => namsx; set => namsx = value; }
        public static string Tgbaohanh { get => tgbaohanh; set => tgbaohanh = value; }
        public static string Sonamkhauhao { get => sonamkhauhao; set => sonamkhauhao = value; }
        public static string Tgduavaosd { get => tgduavaosd; set => tgduavaosd = value; }
        public static string Nguyengia { get => nguyengia; set => nguyengia = value; }
        public static string Ngaynhap { get => ngaynhap; set => ngaynhap = value; }
        public static string Thongsokythuat { get => thongsokythuat; set => thongsokythuat = value; }
        public static string Mancc { get => mancc; set => mancc = value; }
        public static string Madvt { get => madvt; set => madvt = value; }
        public static string Mapb { get => mapb; set => mapb = value; }
        public static string Maloaits { get => maloaits; set => maloaits = value; }
        public static string Matt { get => matt; set => matt = value; }
        public static string Madv { get => madv; set => madv = value; }
        
    }
}
