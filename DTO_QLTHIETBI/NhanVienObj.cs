using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class NhanVienObj
    {
        private static string manv;
        private static string tennv;
        private static string gioitinhnv;
        private static string ngaysinhnv;
        private static string diachinv;
        private static string sdtnv;
        private static string emailnv;
        private static string tenpb;
        private static string tencv;
        private static string hinhanh;

        public NhanVienObj(string manv, string tenv, string gioitinhnv, string ngaysinhnv, string diachinv, string sdtnv, string emailnv, string tenpb,string tencv,string hinhanh)
        {
            Manv = manv;
            Tennv = tennv;
            Gioitinhnv = gioitinhnv;
            Ngaysinhnv = ngaysinhnv;
            Diachinv = diachinv;
            Sdtnv = sdtnv;
            Emailnv = emailnv;
            Tenpb = tenpb;
            Tencv = tencv;
            Hinhanh = hinhanh;
        }

        public static string Manv { get => manv; set => manv = value; }
        public static string Tennv { get => tennv; set => tennv = value; }
        public static string Gioitinhnv { get => gioitinhnv; set => gioitinhnv = value; }
        public static string Ngaysinhnv { get => ngaysinhnv; set => ngaysinhnv = value; }
        public static string Diachinv { get => diachinv; set => diachinv = value; }
        public static string Sdtnv { get => sdtnv; set => sdtnv = value; }
        public static string Emailnv { get => emailnv; set => emailnv = value; }
        public static string Tenpb { get => tenpb; set => tenpb = value; }
        public static string Tencv { get => tencv; set => tencv = value; }
        public static string Hinhanh { get => hinhanh; set => hinhanh = value; }
        
    }
}
