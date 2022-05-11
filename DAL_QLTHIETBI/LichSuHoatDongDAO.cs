using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class LichSuHoatDongDAO
    {
        private static LichSuHoatDongDAO instance;

        public static LichSuHoatDongDAO Instance
        {
            get { if (instance == null) instance = new LichSuHoatDongDAO(); return instance; }
            private set { instance = value; }
        }

        public LichSuHoatDongDAO() { }
        public DataTable GetDataLichSuHoatDong()
        {
            string query = "select * from LICHSUHOATDONG";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataLichSuHoatDong(int page)
        {
            string query = "GetDataLichSuHoatDong @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetThongBaoMoi(string username)
        {
            string query = "ThongBaoMoi @username";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { username });
        }

        public int CountDataLichSuHoatDong()
        {
            string query = "SELECT COUNT(*) FROM LICHSUHOATDONG";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckExistsLichSuHoatDong(string username,string ngay)
        {
            string query = "select * from LICHSUHOATDONG WHERE USERNAME ='" + username + "' and NGAYHD ='"+ngay+"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool GetIsRead(string username, string read)
        {
            string query = "select * from CHITIET_LSHD CT, LICHSUHOATDONG LSHD" +
                " WHERE LSHD.ID_LSHD = CT.ID_LSHD AND IS_READ = '"+read+"' AND LSHD.USERNAME = '"+username+"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public DataTable GetId_LSHD(string username, string ngay)
        {
            string query = "select ID_LSHD from LICHSUHOATDONG WHERE USERNAME ='" + username + "' and NGAYHD='" + ngay + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select LSHD.ID_LSHD,convert(varchar(10),NGAYHD,103)as NGAYHD,USERNAME,NV.TENNV,HD.NOIDUNG_HD,CT.DOITUONG,CT.THOIGIAN_HD"
                + " FROM LICHSUHOATDONG LSHD, NHANVIEN NV, CHITIET_LSHD CT, HOATDONG HD "
                + "where NV.MANV=LSHD.USERNAME AND CT.ID_LSHD=LSHD.ID_LSHD AND HD.ID_HD=CT.ID_HD and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string username, string ngayhd)
        {
            string query = string.Format("INSERT INTO LICHSUHOATDONG (USERNAME,NGAYHD) VALUES  ('{0}', '{1}')", username,ngayhd);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string id_lshd, string id_hd, string doituong, string thoigian)
        {
            string query = string.Format("INSERT INTO CHITIET_LSHD VALUES  ({0}, {1}, '{2}' , '{3}',0)", id_lshd, id_hd, doituong, thoigian);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public void Update(string username, string ngayhd, string thoigian)
        {
            string query = "update CHITIET_LSHD "
            + "set IS_READ = 1 "
            + "from CHITIET_LSHD CT,LICHSUHOATDONG LSHD "
            + "where LSHD.ID_LSHD = CT.ID_LSHD and USERNAME = '"+username+"' and NGAYHD = '"+ngayhd+"' and CT.THOIGIAN_HD = '"+thoigian+"'";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void ThongBao(int  hd, string doituong)
        {
            string s = DateTime.Now.ToString("g");
            if (CheckExistsLichSuHoatDong(TaikhoanObj.Username, DateTime.Now.ToString("MM/dd/yyyy")) == false)
            {
                Them(TaikhoanObj.Username, DateTime.Now.ToString("MM/dd/yyyy"));
                string id = GetId_LSHD(TaikhoanObj.Username, DateTime.Now.ToString("MM/dd/yyyy")).Rows[0][0].ToString();
                ThemCT(id, hd.ToString(), doituong, s.Substring(s.Length - 8, 8));
            }
            else
            {
                string id =GetId_LSHD(TaikhoanObj.Username, DateTime.Now.ToString("MM/dd/yyyy")).Rows[0][0].ToString();
                ThemCT(id, hd.ToString(), doituong, s.Substring(s.Length - 8, 8));
            }
            TrangThaiObj.Trangthai = "Thông Báo";
        }
    }
}
