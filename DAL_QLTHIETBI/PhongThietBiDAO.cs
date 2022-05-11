using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class PhongThietBiDAO
    {
        private static PhongThietBiDAO instance;

        public static PhongThietBiDAO Instance
        {
            get { if (instance == null) instance = new PhongThietBiDAO(); return instance; }
            private set { instance = value; }
        }

        public PhongThietBiDAO() { }
        public DataTable GetDataPhongThietBi()
        {
            string query = "select * from PHONGTHIETBI";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhongTB(string ma)
        {
            string query = "select MAPTB,TENPTB, SOPHONG, SLUONGTB,VITRI,TRANGTHAIPTB,NV.TENNV FROM PHONGTHIETBI PTB, NHANVIEN NV where MAPTB='"+ma+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhongThietBi(int page)
        {
            string query = "GetDataPhongThietBi @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public bool CapNhatSLThietBi(string maptb)
        {
            string query = string.Format("UPDATE PHONGTHETBI SET SLUONGTB=SLUONGTB-1 WHERE MAPTB='"+maptb+"'" );
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public int CountDataPhongThietBi()
        {
            string query = "SELECT COUNT(*) FROM PHONGTHIETBI";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MAPTB, TENPTB, SOPHONG, SLUONGTB, VITRI, TRANGTHAIPTB, NV.TENNV "
                + "FROM NHANVIEN NV, PHONGTHIETBI PTB "
                + "WHERE NV.MANV=PTB.MANV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool Them(string ma, string ten, string sophong, string soluong, string vitri, string trangthai, string manv)
        {
            string query = string.Format("INSERT INTO PHONGTHIETBI VALUES  ('{0}', N'{1}', '{2}' , {3}, N'{4}', N'{5}', '{6}')", ma, ten, sophong, soluong, vitri, trangthai, manv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string sophong, string soluong, string vitri, string trangthai, string manv)
        {
            string query = string.Format("UPDATE PHONGTHIETBI SET TENPTB = N'{0}', SOPHONG= N'{1}', SLUONGTB= {2}, VITRI= N'{3}', TRANGTHAIPTB = N'{4}', MANV = '{5}' WHERE MAPTB= '{6}'", ten, sophong, soluong, vitri, trangthai, manv, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE PHONGTHIETBI WHERE MAPTB = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

    }
}
