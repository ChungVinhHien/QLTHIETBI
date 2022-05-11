using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class NhomTaiSanDAO
    {
        private static NhomTaiSanDAO instance;

        public static NhomTaiSanDAO Instance
        {
            get { if (instance == null) instance = new NhomTaiSanDAO(); return instance; }
            private set { instance = value; }
        }

        public NhomTaiSanDAO() { }

        public DataTable GetDataNhomTaiSan(int page)
        {
            string query = "GetDataNhomTaiSan @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataNhomTaiSan()
        {
            string query = "SELECT * FROM  NHOMTAISAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataByMaNhomTS(string manhomts)
        {
            string query = "SELECT * FROM NHOMTAISAN where MANHOMTS  = '" + manhomts + "'";            
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataByTenNhomTS(string tennhomts)
        {
            string query = "SELECT * FROM NHOMTAISAN where TENNHOMTS = N'" + tennhomts + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MANHOMTS) FROM NHOMTAISAN where TENNHOMTS = N'" + tennhomts + "'");
            if (result < 1 )
                 return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataByMaloaiTS(string maloaits)
        {
            string query = "SELECT * FROM LOAITAISAN LTS, NHOMTAISAN NTS where NTS.MANHOMTS = LTS.MANHOMTS AND LTS.MALOAITS  = N'"+ maloaits + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataNhomTaiSan()
        {
            string query = "SELECT COUNT(*) FROM NHOMTAISAN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MANHOMTS, TENNHOMTS "
                + "FROM NHOMTAISAN "
                + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten)
        {
            string query = string.Format("INSERT INTO NHOMTAISAN VALUES  ( '{0}', N'{1}')", ma, ten);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten)
        {
            string query = string.Format("UPDATE NHOMTAISAN SET TENNHOMTS = N'{0}' WHERE MANHOMTS = '{1}'", ten, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("Delete NHOMTAISAN where MANHOMTS = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
