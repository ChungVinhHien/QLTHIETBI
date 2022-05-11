using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class KeHoachMuaSamDAO
    {
        private static KeHoachMuaSamDAO instance;
        private MyFuntions funtions = new MyFuntions();

        public static KeHoachMuaSamDAO Instance
        {
            get { if (instance == null) instance = new KeHoachMuaSamDAO(); return instance; }
            private set { instance = value; }
        }

        public KeHoachMuaSamDAO() { }

       
        public List<KeHoachMSObj> GetListKeHoachMS(int page)
        {

            List<KeHoachMSObj> list = new List<KeHoachMSObj>();

            string query = "GetDataKeHoachMS @page";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { page });

            foreach (DataRow item in data.Rows)
            {
                KeHoachMSObj kehoach = new KeHoachMSObj(item);
                list.Add(kehoach);                
            }

            return list;
        }
        public DataTable GetDataKeHoachMS()
        {
            string query = "select * from KEHOACHMUASAM WHERE getdate() between TGAPDUNG and TGHIEULUC";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataByMaKH(string makh)
        {
            string query = "select * from KEHOACHMUASAM WHERE MAKHMS ='" + makh + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

       
        public void UpdateTrangThai(string makh, string value)
        {
            string query = string.Format("update KEHOACHMUASAM set TRANGTHAI = '{0}' where MAKHMS = '{1}'", value, makh);
            DataProvider.Instance.ExecuteNonQuery(query);

        }

        public int CountData()
        {
            string query = "SELECT COUNT(*) FROM KEHOACHMUASAM";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public List<KeHoachMSObj> TimKiemTheoTen(string atr, string value)
        {
            List<KeHoachMSObj> list = new List<KeHoachMSObj>();

            string query = "select MAKHMS,convert(varchar(10),TGAPDUNG,103) as TGAPDUNG,convert(varchar(10),TGHIEULUC,103) as TGHIEULUC, DV.TENDV, PB.TENPB, TRANGTHAI" +
                " from KEHOACHMUASAM KH, DONVI DV, PHONGBAN PB" +
                " where KH.MADV = DV.MADV AND KH.MAPB = PB.MAPB AND " + atr + " like N'%" + value + "%'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KeHoachMSObj kehoach = new KeHoachMSObj(item);
                list.Add(kehoach);
            }

            return list;
        }
        public bool Them(string makh, string tgapdung, string tghieuluc, string madv, string mapb, string trangthai)
        {
            string query = string.Format("INSERT INTO KEHOACHMUASAM VALUES  ('{0}','{1}','{2}','{3}','{4}',{5})", makh, tgapdung, tghieuluc, madv, mapb, trangthai);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string makh, string tgapdung, string tghieuluc, string madv, string mapb, string trangthai)
        {
            string query = string.Format("UPDATE KEHOACHMUASAM SET TGAPDUNG='{0}', TGHIEULUC='{1}', MADV='{2}', MAPB='{3}', TRANGTHAI={4}  WHERE MAKHMS='{5}'", tgapdung, tghieuluc, madv, mapb, trangthai, makh);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE KEHOACHMUASAM WHERE MAKHMS = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
