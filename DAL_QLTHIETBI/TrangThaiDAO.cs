using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class TrangThaiDAO
    {
        private static TrangThaiDAO instance;

        public static TrangThaiDAO Instance
        {
            get { if (instance == null) instance = new TrangThaiDAO(); return instance; }
            private set { instance = value; }
        }

        public TrangThaiDAO() { }

        public DataTable GetDataTrangThai()
        {
            string query = "select * from TRANGTHAI";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataByMa(string matt)
        {
            string query = "select * from TRANGTHAI WHERE MATT = N'" + matt + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataByTen(string tentt)
        {
            string query = "select * from TRANGTHAI WHERE TENTT = N'"+ tentt +"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataTrangThai(int page)
        {
            string query = "GetDataTrangThai @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }

        public int CountDataTrangThai()
        {
            string query = "SELECT COUNT(*) FROM TRANGTHAI";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MATT, TENTT "
                + "FROM TRANGTHAI "
                + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool CheckTrangThai(string matt)
        {
            string query = "select * from TRANGTHAI WHERE MATT ='" + matt + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public bool Them(string ma, string ten)
        {
            string query = string.Format("INSERT INTO TRANGTHAI VALUES  ( '{0}', N'{1}')", ma, ten);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten)
        {
            string query = string.Format("UPDATE TRANGTHAI SET TENTT = N'{0}' WHERE MATT = '{1}'", ten, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("Delete TRANGTHAI where MATT = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
