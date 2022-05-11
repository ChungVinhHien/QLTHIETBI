using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class DonViDAO
    {
        private static DonViDAO instance;

        public static DonViDAO Instance
        {
            get { if (instance == null) instance = new DonViDAO(); return instance; }
            private set { instance = value; }
        }

        public DonViDAO() { }

        public DataTable GetDataDonVi()
        {
            string query = "select * from DONVI";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataDonVi(int page)
        {
            string query = "GetDataDonVi @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataDVByMaDV(string madv)
        {
            string query = "select * from DONVI WHERE MADV='"+madv+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataDVByTenDV(string tendv)
        {
            string query = "SELECT * FROM DONVI where TENDV = N'" + tendv + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MADV) FROM DONVI where TENDV = N'" + tendv + "'");
            if (result < 1)
                return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataDonVi()
        {
            string query = "SELECT COUNT(*) FROM DONVI";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr,string value)
        {
            string query = "select MADV, TENDV, DIACHIDV, SDTDV, EMAILDV, MOTADV "
                + "FROM DONVI "
                + "WHERE " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string diachi, string sdt, string email, string mota)
        {
            string query = string.Format("INSERT INTO DONVI VALUES  ('{0}', N'{1}', N'{2}' , '{3}', '{4}', N'{5}')", ma, ten, diachi, sdt, email, mota);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string diachi, string sdt, string email, string mota)
        {
            string query = string.Format("UPDATE DONVI SET TENDV = N'{0}', DIACHIDV= N'{1}', SDTDV = '{2}', EMAILDV = '{3}', MOTADV= N'{4}'  WHERE MADV = '{5}'", ten, diachi, sdt, email, mota, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE DONVI WHERE MADV = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
