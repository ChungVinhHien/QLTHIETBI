using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhongBanDAO
    {
        private static PhongBanDAO instance;

        public static PhongBanDAO Instance
        {
            get { if (instance == null) instance = new PhongBanDAO(); return instance; }
            private set { instance = value; }
        }

        public PhongBanDAO() { }

        public DataTable GetDataPhongBan()
        {
            string query = "select * from PHONGBAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhongBan(int page)
        {
            string query = "GetDataPhongBan @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataPhongBanByMaDV(string madv)
        {
            string query = "SELECT * FROM  PHONGBAN where MADV='" + madv + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPBByMaPB(string mapb)
        {
            string query = "SELECT * FROM  PHONGBAN where MAPB='" + mapb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPBByTenPB(string tenpb)
        {
            string query = "SELECT * FROM PHONGBAN where TENPB = N'" + tenpb + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MAPB) FROM PHONGBAN where TENPB = N'" + tenpb + "'");
            if (result < 1)
                return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataPhongBan()
        {
            string query = "SELECT COUNT(*) FROM PHONGBAN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MAPB, TENPB, DIACHIPB, SDTPB, EMAILPB, DV.TENDV "
                + "FROM PHONGBAN PB, DONVI DV "
                + "WHERE DV.MADV = PB.MADV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string diachi, string sdt, string email, string mota, string madv)
        {
            string query = string.Format("INSERT INTO PHONGBAN VALUES  ('{0}', N'{1}', N'{2}' , '{3}', '{4}', N'{5}', '{6}')", ma, ten, diachi, sdt, email, mota, madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string diachi, string sdt, string email, string mota, string madv)
        {
            string query = string.Format("UPDATE PHONGBAN SET TENPB = N'{0}', DIACHIPB= N'{1}', SDTPB = '{2}', EMAILPB = '{3}', MOTAPB= N'{4}', MADV= '{5}' WHERE MAPB = '{6}'", ten, diachi, sdt, email, mota, madv, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE PHONGBAN WHERE MAPB = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
