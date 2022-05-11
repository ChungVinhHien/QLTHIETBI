using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuThanhLyDAO
    {
        private static PhieuThanhLyDAO instance;

        public static PhieuThanhLyDAO Instance
        {
            get { if (instance == null) instance = new PhieuThanhLyDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuThanhLyDAO() { }

        public DataTable GetDataPhieuThanhLy(string maptl)
        {
            string query = "select PTL.MATB,TB.TENTB,CHIPHITL,GTTHUHOI from PHIEUTHANHLYTB PTL, THIETBI TB WHERE PTL.MATB=TB.MATB AND MAPTL='"+maptl+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhieuThanhLy(int page)
        {
            string query = "GetDataPhieuThanhLy @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        
        public int CountDataPhieuThanhLy()
        {
            string query = "SELECT COUNT(*) FROM PHIEUTHANHLYTB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckPhieuThanhLyByMaTB(string matb)
        {
            string query = "select * from PHIEUTHANHLYTB WHERE MATB='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MAPTL,PTL.MATB,TB.TENTB,NGAYLAPPTL,NV.TENNV,NGAYTL,CHIPHITL,GTTHUHOI "
                + " from PHIEUTHANHLYTB PTL, THIETBI TB, NHANVIEN NV"
                + " where TB.MATB=PTL.MATB AND NV.MANV=PTL.MANV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string maptl, string matb, string ngaytl, string ngaylap, string manv, string chiphitl,string giatri)
        {
            string query = string.Format("INSERT INTO PHIEUTHANHLYTB (MAPTL,MATB,NGAYLAPPTL,NGAYTL,MANV,CHIPHITL,GTTHUHOI) VALUES  ('{0}', '{1}' , '{2}', '{3}','{4}',{5},{6})", maptl, matb, ngaylap, ngaytl, manv,chiphitl,giatri);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool Sua(string maptl, string matb, string ngaytl,string chiphitl,string manv)
        {
            string query = string.Format("UPDATE PHIEUTHANHLYTB SET NGAYTL= '{0}',MATB='{1}',CHIPHITL={2},MANV='{3}'  WHERE MAPTL= '{4}'", ngaytl,matb,chiphitl,manv,maptl);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string maptl,string matb)
        {

            string query = string.Format("Delete PHIEUTHANHLYTB WHERE MAPTL='{0}' AND MATB='{1}'", maptl, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
