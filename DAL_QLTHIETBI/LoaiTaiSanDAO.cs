using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class LoaiTaiSanDAO
    {
        private static LoaiTaiSanDAO instance;

        public static LoaiTaiSanDAO Instance
        {
            get { if (instance == null) instance = new LoaiTaiSanDAO(); return instance; }
            private set { instance = value; }
        }

        public LoaiTaiSanDAO() { }  
       
        public DataTable GetDataLoaiTaiSan(int page)
        {
            string query = "GetDataLoaiTaiSan @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataLoaiTaiSan()
        {
            string query = "SELECT * FROM  LOAITAISAN ";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataLoaiTaiSan(string maloaits)
        {
            string query = "SELECT * FROM  LOAITAISAN WHERE MALOAITS = '"+ maloaits + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataLoaiTaiSanByMaNhomTS(string mants)
        {
            string query = "SELECT * FROM  LOAITAISAN where MANHOMTS='"+mants+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetTenLoaiTSByMaLoaiTS(string maloaits)
        {
            string query = "SELECT TENLOAITS FROM  LOAITAISAN where MALOAITS='" + maloaits + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetMaLoaiByTenLoaiTS(string tenloaits)
        {
            string query = "SELECT MALOAITS FROM LOAITAISAN where TENLOAITS = N'" + tenloaits + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MALOAITS) FROM LOAITAISAN where TENLOAITS = N'" + tenloaits + "'");
            if (result < 1)
                return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataLoaiTaiSan()
        {
            string query = "SELECT COUNT(*) FROM LOAITAISAN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr,string value)
        {
            string query = "select MALOAITS, TENLOAITS, NAMKHMIN, NAMKHMAX,TGSUDUNG,TYLEHAOMON, NTS.TENNHOMTS "
                +"FROM LOAITAISAN LTS, NHOMTAISAN NTS "
                + "WHERE NTS.MANHOMTS = LTS.MANHOMTS and "+atr+" like N'%"+value+"%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string min, string max, string tgsudung, string tylehaomon, string manhom)
        {
            string query = string.Format("INSERT INTO LOAITAISAN VALUES  ( '{0}', N'{1}', {2}, {3}, {4}, {5} , '{6}')", ma, ten, min, max,tgsudung,tylehaomon, manhom);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string min, string max, string tgsudung, string tylehaomon, string manhom)
        {
            string query = string.Format("UPDATE LOAITAISAN SET TENLOAITS = N'{0}', NAMKHMIN = {1}, NAMKHMAX = {2}, TGSUDUNG={3}, TYLEHAOMON={4}  , MANHOMTS = '{5}'  WHERE MALOAITS = '{6}'", ten, min, max, tgsudung, tylehaomon, manhom, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("Delete LOAITAISAN where MALOAITS = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
