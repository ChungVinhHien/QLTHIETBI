using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuLuanChuyenDAO
    {
        private static PhieuLuanChuyenDAO instance;

        public static PhieuLuanChuyenDAO Instance
        {
            get { if (instance == null) instance = new PhieuLuanChuyenDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuLuanChuyenDAO() { }

        public DataTable GetDataPhieuLuanChuyen()
        {
            string query = "select * from PHIEULUANCHUYENTB";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhieuLuanChuyen(int page)
        {
            string query = "GetDataPhieuLuanChuyen @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
       
        public DataTable GetDataCTPhieuLuanChuyen(string maplc)
        {
            string query = "GetDataChiTietPhieuLuanChuyen @maplc";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maplc });
        }
        public int CountDataPhieuLuanChuyen()
        {
            string query = "SELECT COUNT(*) FROM PHIEULUANCHUYENTB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckPhieuLuanChuyen(string maplc)
        {
            string query = "select * from CHITIET_PHIEULUANCHUYEN WHERE MAPLC ='" + maplc + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckPhieuLuanChuyenByMaTB(string maplc, string matb)
        {
            string query = "select * from CHITIET_PHIEULUANCHUYEN WHERE MAPLC ='" + maplc + "' and MATB='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query;
            if (atr == "NGUOILC")
            {
                query = "select MAPLC,NGAYLC,NGAYLAP,DV.TENDV,NHANVIEN1,NHANVIEN2,NHANVIEN3,NHANVIENNHAN,SOLUONG "
                + " from PHIEULUANCHUYENTB PLC, DONVI DV  where PLC.MADV=DV.MADV " +
                " and NHANVIEN1 like N'%" + value + "%' or NHANVIEN2 like N'%" + value + "%' or NHANVIEN3 like N'%" + value + "%'";
            }
            else if (atr == "NGUOINHAN")
            {
                query = "select MAPLC,NGAYLC,NGAYLAP,DV.TENDV,NHANVIEN1,NHANVIEN2,NHANVIEN3,NHANVIENNHAN,SOLUONG "
                + " from PHIEULUANCHUYENTB PLC, DONVI DV  where PLC.MADV=DV.MADV " +
                "and NHANVIENNHAN like N'%" + value + "%'";
            }
            else
            {
                query = "select MAPLC,NGAYLC,NGAYLAP,DV.TENDV,NHANVIEN1,NHANVIEN2,NHANVIEN3,NHANVIENNHAN,SOLUONG "
                 + " from PHIEULUANCHUYENTB PLC, DONVI DV  where PLC.MADV=DV.MADV and " + atr + " like N'%" + value + "%'";
            }

            return DataProvider.Instance.ExecuteQuery(query);
        }
       

        public bool Them(string maplc, string ngaylc, string ngaylap, string nhanvien1, string nhanvien2, string nhanvien3, string nguoinhan, string madv)
        {
            string query = string.Format("INSERT INTO PHIEULUANCHUYENTB (MAPLC,NGAYLC,NGAYLAP,NHANVIEN1,NHANVIEN2,NHANVIEN3,NHANVIENNHAN,MADV) " +
                "VALUES  ('{0}', '{1}' , '{2}', N'{3}', N'{4}', N'{5}',N'{6}','{7}')", maplc, ngaylc, ngaylap, nhanvien1, nhanvien2, nhanvien3,nguoinhan, madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string maplc, string matb,string tudonvi, string dendonvi, string lydo)
        {
            string query = string.Format("INSERT INTO CHITIET_PHIEULUANCHUYEN " +
                "VALUES ('{0}','{1}',N'{2}',N'{3}',N'{4}')", maplc, matb,tudonvi,dendonvi,lydo);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string maplc, string ngaylc, string nv1, string nv2, string nv3,string nguoinhan)
        {
            string query = string.Format("UPDATE PHIEULUANCHUYENTB SET NGAYLC= '{0}', NHANVIEN1= N'{1}', NHANVIEN2= N'{2}',NHANVIEN3=N'{3}, NHANVIENNHAN = N'{4}'  WHERE MAPLC= '{5}'", ngaylc, nv1, nv2, nv3, nguoinhan, maplc);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool SuaCT(string maplc, string matb, string dendonvi, string lydo)
        {
            string query = string.Format("UPDATE CHITIET_PHIEULUANCHUYEN SET DENDONVI={0},LYDO=N'{1}'" +
                " WHERE MAPLC = '{2}' AND MATB='{3}'", dendonvi, lydo, maplc, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string maplc)
        {
            string query = "DelDataPhieuLuanChuyen @maplc";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maplc });

            return result > 0;
        }
        public bool XoaCT(string maplc, string matb)
        {
            string query = string.Format("Delete CHITIET_PHIEULUANCHUYEN WHERE MAPLC='{0}' AND MATB='{1}'", maplc, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
