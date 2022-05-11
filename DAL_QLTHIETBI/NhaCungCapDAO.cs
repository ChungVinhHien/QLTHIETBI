using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class NhaCungCapDAO
    {
        private static NhaCungCapDAO instance;

        public static NhaCungCapDAO Instance
        {
            get { if (instance == null) instance = new NhaCungCapDAO(); return instance; }
            private set { instance = value; }
        }

        public NhaCungCapDAO() { }

        public DataTable GetDataNhaCungCap()
        {
            string query = "select * from NHACUNGCAP";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataNhaCungCap(int page)
        {
            string query = "GetDataNhaCungCap @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataNCCByMaNCC(string mancc)
        {
            string query = "SELECT * FROM NHACUNGCAP where MANCC  = '" + mancc + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataNCCByTenNCC(string tenncc)
        {
            string query = "SELECT * FROM NHACUNGCAP where TENNCC = N'" + tenncc + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MANCC) FROM NHACUNGCAP where TENNCC = N'" + tenncc + "'");
            if (result < 1)
                return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }
               
        public int CountDataNhaCungCap()
        {
            string query = "SELECT COUNT(*) FROM NHACUNGCAP";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MANCC, TENNCC, DIACHINCC, SDTNCC "
                + "FROM NHACUNGCAP "
                + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string diachi, string sdt, string email)
        {
            string query = string.Format("INSERT INTO NHACUNGCAP VALUES  ( '{0}', N'{1}', N'{2}' , '{3}', '{4}')", ma, ten, diachi, sdt, email);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string diachi, string sdt, string email)
        {
            string query = string.Format("UPDATE NHACUNGCAP SET TENNCC = N'{0}', DIACHINCC = N'{1}', SDTNCC = '{2}', EMAILNCC = '{3}'  WHERE MANCC = '{4}'", ten, diachi, sdt, email, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("Delete NHACUNGCAP where MANCC = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
