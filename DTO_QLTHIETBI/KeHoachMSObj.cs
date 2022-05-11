using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class KeHoachMSObj
    {
        private string makhms;
        private string tgapdung;
        private string tghieuluc;
        private string donvi;
        private string phongban;
        private string trangthai;

        private static string makh;
        private static string tgad;
        private static string tghl;
        private static string dvt;
        private static string pb;
        private static string tt;

        public string Makhms { get => makhms; set => makhms = value; }
        public string Tgapdung { get => tgapdung; set => tgapdung = value; }
        public string Tghieuluc { get => tghieuluc; set => tghieuluc = value; }
        public string Donvi { get => donvi; set => donvi = value; }
        public string Phongban { get => phongban; set => phongban = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
        public static string Makh { get => makh; set => makh = value; }
        public static string Tgad { get => tgad; set => tgad = value; }
        public static string Tghl { get => tghl; set => tghl = value; }
        public static string Dvt { get => dvt; set => dvt = value; }
        public static string Pb { get => pb; set => pb = value; }
        public static string Tt { get => tt; set => tt = value; }

        public KeHoachMSObj(string makhms, string tgapdung, string tghieuluc, string madv, string mapb, string trangthai)
        {
            this.Makhms = makhms;
            this.Tgapdung = tgapdung;
            this.Tghieuluc = tghieuluc;
            this.Donvi = madv;
            this.Phongban = mapb;
            this.Trangthai = trangthai;
        }
        public KeHoachMSObj(DataRow row)
        {
            this.Makhms = row["MAKHMS"].ToString();
            this.Tgapdung = row["TGAPDUNG"].ToString();
            this.Tghieuluc = row["TGHIEULUC"].ToString();
            this.Donvi = row["TENDV"].ToString();
            this.Phongban = row["TENPB"].ToString();
            if (row["TRANGTHAI"].ToString() == "True")
                this.Trangthai = "Đã duyệt";
            else if (row["TRANGTHAI"].ToString() == "False")
                this.Trangthai = "Từ chối";
            else this.Trangthai = "Chưa duyệt";

        }

       

       
    }
}
