using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuBaoTriDAO
    {
        private static PhieuBaoTriDAO instance;

        public static PhieuBaoTriDAO Instance
        {
            get { if (instance == null) instance = new PhieuBaoTriDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuBaoTriDAO() { }

        public DataTable GetDataPhieuBaoTri()
        {
            string query = "select * from PHIEUBAOTRITB";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataPhieuBaoTri(int page)
        {
            string query = "GetDataPhieuBaoTri @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataCTPhieuBaoTri(string mapbt)
        {
            string query = "GetDataChiTietPhieuBaoTri @mapbt";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { mapbt });
        }
        public int CountDataPhieuBaoTri()
        {
            string query = "SELECT COUNT(*) FROM PHIEUBAOTRITB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckPhieuBaoTri(string mapbt)
        {
            string query = "select * from CHITIET_PHIEUBAOTRI WHERE MAPBT ='" + mapbt + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckPhieuBaoTriByMaTB(string mapbt, string matb)
        {
            string query = "select * from CHITIET_PHIEUBAOTRI WHERE MAPBT ='" + mapbt + "' and MATB='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MAPBT,NGAYLAPPBT,DV.TENDV,NV.TENNV,TUNGAY,DENNGAY,SLBAOTRI,TONGTIENBT"
                + " FROM PHIEUBAOTRITB PBT, NHANVIEN NV, DONVI DV "
                + "WHERE PBT.MADV=DV.MADV AND NV.MANV=PBT.MANV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
       

        public bool Them(string mapsc, string ngaylap, string manv, string tungay, string denngay,string madv)
        {
            string query = string.Format("INSERT INTO PHIEUBAOTRITB (MAPBT,NGAYLAPPBT,MANV,TUNGAY,DENNGAY,MADV) " +
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", mapsc, ngaylap, manv, tungay, denngay,madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string mapbt, string matb, string noidung, string chiphi)
        {
            string query = string.Format("INSERT INTO CHITIET_PHIEUBAOTRI VALUES  ('{0}', '{1}', N'{2}' , {3})", mapbt, matb, noidung, chiphi);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string mapbt, string manv, string tungay, string denngay)
        {
            string query = string.Format("UPDATE PHIEUBAOTRITB SET MANV= '{0}', TUNGAY= '{1}', DENNGAY = '{2}'  WHERE MAPBT = '{3}'", manv, tungay, denngay, mapbt);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool SuaCT(string mapbt, string matb, string noidung, string chiphi)
        {
            string query = string.Format("UPDATE CHITIET_PHIEUBAOTRI SET NOIDUNGBT = N'{0}', CHIPHI={1} WHERE MAPBT = '{2}' AND MATB='{3}'", noidung, chiphi, mapbt, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string mapbt)
        {
            string query = "DelDataPhieuBaoTri @mapbt";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { mapbt });

            return result > 0;
        }
        public bool XoaCTPhieuBaoTri(string mapbt, string matb)
        {
            string query = string.Format("Delete CHITIET_PHIEUBAOTRI WHERE MAPBT='{0}' AND MATB='{1}'", mapbt, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
