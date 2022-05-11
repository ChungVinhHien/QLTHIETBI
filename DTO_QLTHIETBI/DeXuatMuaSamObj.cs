using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class DeXuatMuaSamObj
    {
        private string madexuat;
        private string ngaylap;
        private string nguoidexuat;
        private string nguoiduyet;
        private string giaitrinh;
        private string trangthai;
        private string soluong;
        private string tongtien;

        private static string madx;
        private static string ngaylapdx;
        private static string nguoidx;
        private static string nguoiduyetdx;
        private static string giaitrinhdx;
        private static string trangthaidx;
        private static string sluongdx;
        private static string tongdx;
             

        public string Madexuat { get => madexuat; set => madexuat = value; }
        public string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public string Nguoidexuat { get => nguoidexuat; set => nguoidexuat = value; }
        public string Nguoiduyet { get => nguoiduyet; set => nguoiduyet = value; }
        public string Giaitrinh { get => giaitrinh; set => giaitrinh = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
        public string Soluong { get => soluong; set => soluong = value; }
        public string Tongtien { get => tongtien; set => tongtien = value; }

        public static string Madx { get => madx; set => madx = value; }
        public static string Ngaylapdx { get => ngaylapdx; set => ngaylapdx = value; }
        public static string Nguoidx { get => nguoidx; set => nguoidx = value; }
        public static string Nguoiduyetdx { get => nguoiduyetdx; set => nguoiduyetdx = value; }
        public static string Giaitrinhdx { get => giaitrinhdx; set => giaitrinhdx = value; }
        public static string Trangthaidx { get => trangthaidx; set => trangthaidx = value; }
        public static string Sluongdx { get => sluongdx; set => sluongdx = value; }
        public static string Tongdx { get => tongdx; set => tongdx = value; }

        public DeXuatMuaSamObj(string madexuat, string ngaylap, string nguoidexuat, string nguoiduyet, string giaitrinh, string trangthai, string soluong, string tongtien)
        {
            this.Madexuat = madexuat;
            this.Ngaylap = ngaylap;
            this.Nguoidexuat = nguoidexuat;
            this.Nguoiduyet = nguoiduyet;
            this.Giaitrinh = giaitrinh;
            this.Trangthai = trangthai;
            this.Soluong = soluong;
            this.Tongtien = tongtien;
        }

        public DeXuatMuaSamObj()
        {

        }

        public DeXuatMuaSamObj(DataRow row)
        {
            this.Madexuat = row["MADXMS"].ToString();
            this.Ngaylap = row["NGAYLAPDX"].ToString();
            this.Nguoidexuat = row["NGUOIDX"].ToString();
            this.Nguoiduyet = row["NGUOIDUYET"].ToString();
            this.Giaitrinh = row["GIAITRINH"].ToString();
            if (row["TRANGTHAI"].ToString() == "True")
                this.Trangthai = "Đã duyệt";
            else if (row["TRANGTHAI"].ToString() == "False")
                this.Trangthai = "Từ chối";
            else this.Trangthai = "Chưa duyệt";
            this.Soluong = row["SOLUONG"].ToString();
            this.Tongtien = row["TONGTIEN"].ToString();
        }
    }
}
