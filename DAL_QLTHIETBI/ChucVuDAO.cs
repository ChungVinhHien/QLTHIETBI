using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class ChucVuDAO
    {
        private static ChucVuDAO instance;

        public static ChucVuDAO Instance
        {
            get { if (instance == null) instance = new ChucVuDAO(); return instance; }
            private set { instance = value; }
        }

        public ChucVuDAO() { }
        public DataTable GetDataChucVu()
        {
            string query = "select * from CHUCVU";
            return DataProvider.Instance.ExecuteQuery(query);
        }
       
        public DataTable GetDataChucVu(int page)
        {
            string query = "GetDataChucVu @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }

        public int CountDataChucVu()
        {
            string query = "SELECT COUNT(*) FROM CHUCVU";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MACV, TENCV, MOTACV "
                + "FROM CHUCVU "
                + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string mota)
        {
            string query = string.Format("INSERT INTO CHUCVU VALUES  ( '{0}', N'{1}', N'{2}')", ma, ten, mota);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string mota)
        {
            string query = string.Format("UPDATE CHUCVU SET TENCV = N'{0}', MOTACV = N'{1}'  WHERE MACV = '{2}'", ten, mota, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE CHUCVU WHERE MACV = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
