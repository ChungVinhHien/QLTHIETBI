using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuSuaChuaDAO
    {
        private static PhieuSuaChuaDAO instance;

        public static PhieuSuaChuaDAO Instance
        {
            get { if (instance == null) instance = new PhieuSuaChuaDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuSuaChuaDAO() { }

        public DataTable GetDataPhieuSuaChua()
        {
            string query = "select * from PHIEUSUACHUATB";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        
        public DataTable GetDataPhieuSuaChua(int page)
        {
            string query = "GetDataPhieuSuaChua @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataCTPhieuSuaChua(string mapsc)
        {
            string query = "GetDataChiTietPhieuSuaChua @mapsc";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { mapsc });
        }
        public int CountDataPhieuSuaChua()
        {
            string query = "SELECT COUNT(*) FROM PHIEUSUACHUATB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckPhieuSuaChua(string mapsc)
        {
            string query = "select * from CHITIET_PHIEUSUACHUA WHERE MAPSC ='" + mapsc + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckPhieuSuaChuaByMaTB(string mapsc, string matb)
        {
            string query = "select * from CHITIET_PHIEUSUACHUA WHERE MAPSC ='" + mapsc + "' and MATB='"+matb+"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MAPSC,NGAYLAPPSC,TUNGAY,DENNGAY,DV.TENDV,NV.TENNV,NGUOISC,SLSUACHUA,TONGTIENSC"
                +" FROM PHIEUSUACHUATB PSC, NHANVIEN NV, DONVI DV "
                + "WHERE NV.MANV=PSC.MANV AND PSC.MADV=DV.MADV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string mapsc, string ngaylap, string manv, string tungay, string denngay, string nguoisc, string donvi)
        {
            string query = string.Format("INSERT INTO PHIEUSUACHUATB (MAPSC,NGAYLAPPSC,MANV,TUNGAY,DENNGAY,NGUOISC, MADV) VALUES  ('{0}', '{1}' , '{2}', '{3}', '{4}', N'{5}','{6}')", mapsc, ngaylap, manv, tungay, denngay, nguoisc, donvi);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string mapsc, string matb, string noidung, string giadd, string giatt, string kqkt)
        {
            string query = string.Format("INSERT INTO CHITIET_PHIEUSUACHUA VALUES  ('{0}', '{1}', N'{2}' , {3}, {4}, N'{5}')", mapsc, matb, noidung, giadd, giatt, kqkt);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string mapsc, string manv, string tungay, string denngay, string nguoisc)
        {
            string query = string.Format("UPDATE PHIEUSUACHUATB SET MANV= '{0}', TUNGAY= '{1}', DENNGAY = '{2}', NGUOISC = N'{3}'  WHERE MAPSC = '{4}'", manv, tungay, denngay, nguoisc, mapsc);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool SuaCT(string mapsc, string matb, string noidung, string giadd, string giatt, string kqkt)
        {
            string query = string.Format("UPDATE CHITIET_PHIEUSUACHUA SET NOIDUNGSC = N'{0}', GIADUDOAN={1}, GIATHUCTE={2}, KQKIEMTRA= N'{3}' WHERE MAPSC = '{4}' AND MATB='{5}'", noidung, giadd, giatt, kqkt,mapsc,matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string mapsc)
        {
            string query = "DelDataPhieuSuaChua @mapsc";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { mapsc });

            return result > 0;
        }
        public bool XoaCT(string mapsc, string matb)
        {
            string query = string.Format("Delete CHITIET_PHIEUSUACHUA WHERE MAPSC='{0}' AND MATB='{1}'", mapsc, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
