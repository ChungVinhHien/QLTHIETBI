using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_QLTHIETBI;
using System.Data.SqlClient;

namespace DAL_QLTHIETBI
{
    public class TheTaiSanDAO
    {
        private static TheTaiSanDAO instance;

        public static TheTaiSanDAO Instance
        {
            get { if (instance == null) instance = new TheTaiSanDAO(); return instance; }
            private set { instance = value; }
        }

        public TheTaiSanDAO() { }
        public DataTable GetDataTheTaiSan()
        {
            string query = "select * from THETAISAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataTheTaiSan(int page)
        {
            string query = "GetDataTheTaiSan @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataCTTheTaiSan(string matts)
        {
            string query = "GetDataChiTietTheTaiSan @matts";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { matts });
        }
        public DataTable GetMaTheTSByMaTB(string matb)
        {
            string query = "select MATHETS from CHITIET_THETAISAN where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public string GetGiaTriHaoMon(string matb)
        {
            string query = "SELECT dbo.GetGiaTriHaoMon('" + matb + "')";
            string result = DataProvider.Instance.ExecuteQuery(query).Rows[0][0].ToString();

            return result;
        }
        public int CountDataTheTaiSan()
        {
            string query = "SELECT COUNT(*) FROM THETAISAN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool TinhHaoMon(string matts, string matb, string ngay)
        {
            string query = "TinhHaoMon @matts , @matb , @ngay";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { matts, matb, ngay });

            return result > 0;
        }
        public bool CheckTheTaiSan(string matb)
        {
            string query = "select * from CHITIET_THETAISAN WHERE MATB ='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckHaoMonTB(string matb)
        {
            string query = "SELECT dbo.CheckHaoMonTB('" + matb + "')";
            int result = Int32.Parse(DataProvider.Instance.ExecuteQuery(query).Rows[0][0].ToString());

            return result > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select TTS.MATHETS,CT.MATB,TB.TENTB,TB.NGUYENGIA, TB.NGAYNHAP,NGAYLAPTHETS,NV.TENNV "
                + "FROM THETAISAN TTS, NHANVIEN NV, CHITIET_THETAISAN CT, THIETBI TB "
                + "where NV.MANV=TTS.MANV AND TTS.MATHETS=CT.MATHETS AND TB.MATB=CT.MATB "
                + "and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        
        public bool Them(string matts, string matb, string ngaylap, string manv,string gthaomon, string nguyengia)
        {
            string query = "";
            query += string.Format("INSERT INTO THETAISAN VALUES ('{0}','{1}','{2}') ", matts, ngaylap, manv);
            query += string.Format(" INSERT INTO CHITIET_THETAISAN (MATHETS,MATB,NGAY,GTHAOMON,GTHAOMONLUYKE,GTCONLAI) VALUES ('{0}','{1}','{2}',{3},0,{4})", matts, matb, ngaylap, gthaomon, nguyengia);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemChiTiet(string matts, string matb, string ngay, string diengiai)
        {
            string query = string.Format(" INSERT INTO CHITIET_THETAISAN (MATHETS,MATB,NGAY,DIENGIAI) VALUES ('{0}','{1}','{2}',N'{3}')", matts, matb, ngay, diengiai);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string matts, string matb, string ngay, string diengiai, string gthaomon, string gthaomonluyke, string gtconlai)
        {
            string query = string.Format("UPDATE CHITIET_THETAISAN SET DIENGIAI=N'{0}', GTHAOMON={1}, GTHAOMONLUYKE={2}, GTCONLAI={3} " 
                +   " WHERE MATHETS ='{6}' AND MATB='{7}' AND NGAY='{8}'",diengiai,gthaomon,gthaomonluyke,gtconlai,matts,matb,ngay);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = "DelDataTheTaiSan @ma";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma });

            return result > 0;
        }

        public bool XoaCTThe(string matts,string matb,string ngay)
        {
            string query = string.Format("Delete CHITIET_THETAISAN WHERE MATHETS='{0}' AND MATB='{1}' AND NGAY='{2}'",matts,matb,ngay);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public DataTable GetPrint(string query,string matts)
        { 
            return DataProvider.Instance.ExecuteQuery(query, new object[] { matts });
        }
        public DataTable GetPrint(string query)
        {
            return DataProvider.Instance.ExecuteQuery(query);
        }

       

    }
}
