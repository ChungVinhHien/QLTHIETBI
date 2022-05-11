using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTHIETBI;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class DonViTinhDAO
    {
        private static DonViTinhDAO instance;
        
        public static DonViTinhDAO Instance
        {
            get { if (instance == null) instance = new DonViTinhDAO(); return instance; }
            private set { instance = value; }
        }

        public DonViTinhDAO() { }

        public DataTable GetDataDonViTinh()
        {
            string query = "select * from DONVITINH";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataDonViTinh(int page)
        {
            string query = "GetDataDonViTinh @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataDVTByMa(string ma)
        {
            string query = "select * from DONVITINH WHERE MADVT = N'" + ma + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataDVTByTen(string ten)
        {
            string query = "select * from DONVITINH WHERE TENDVT = N'"+ ten +"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataDonViTinh()
        {
            string query = "SELECT COUNT(*) FROM DONVITINH";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MADVT, TENDVT "
               + "FROM DONVITINH "
               + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma,string ten)
        {
            string query = string.Format("INSERT INTO DONVITINH VALUES  ( '{0}', N'{1}')", ma, ten);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten)
        {
            string query = string.Format("UPDATE DONVITINH SET TENDVT = N'{0}' WHERE MADVT = '{1}'", ten, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("Delete DONVITINH where MADVT = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

    }
}
