using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuKhauHaoDAO
    {
        private static PhieuKhauHaoDAO instance;

        public static PhieuKhauHaoDAO Instance
        {
            get { if (instance == null) instance = new PhieuKhauHaoDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuKhauHaoDAO() { }

        public DataTable GetDataPhieuKhauHao()
        {
            string query = "select * from PHIEUKHAUHAOTB";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhieuKhauHao(int page)
        {
            string query = "GetDataPhieuKhauHao @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataCTPhieuKhauHao(string mapkh)
        {
            string query = "GetDataChiTietPhieuKhauHao @mapkh";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { mapkh });
        }

        public DataTable GetTGTrichKhauHao(string matb)
        {
            string query = "select TGTRICHKHAUHAO from THIETBI where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public string GetMucKhauHaoNam(string matb)
        {
            string query = "select dbo.GetMucKhauHaoNam('"+ matb +"')";
            string result = DataProvider.Instance.ExecuteQuery(query).Rows[0][0].ToString();

            return result;
        }
        public DataTable GetMaPhieuKHyMaTB(string matb)
        {
            string query = "select MAPKH from CHITIET_PHIEUKHAUHAO where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataPhieuKhauHao()
        {
            string query = "SELECT COUNT(*) FROM PHIEUKHAUHAOTB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool TrichKhauHao(string mapkh, string matb, string ngaykh)
        {
            string query = "KhauHao @mapkh , @matb , @ngaykh";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { mapkh, matb, ngaykh });

            return result > 0;
        }
        public bool CheckPhieuKhauHao(string matb)
        {
            string query = "select * from CHITIET_PHIEUKHAUHAO WHERE MATB ='"+ matb +"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckKhauHaoTB(string matb)
        {
            string query = "SELECT dbo.CheckKhauHaoTB('"+ matb +"')";
            int result = Int32.Parse(DataProvider.Instance.ExecuteQuery(query).Rows[0][0].ToString());

            return result > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select DISTINCT PKH.MAPKH, CT.MATB,TB.TENTB,TB.NGUYENGIA,TB.NGAYNHAP, NGAYLAPPKH, NV.TENNV " +
                "FROM PHIEUKHAUHAOTB PKH, NHANVIEN NV, CHITIET_PHIEUKHAUHAO CT, THIETBI TB " +
                "WHERE PKH.MAPKH = CT.MAPKH AND TB.MATB = CT.MATB AND PKH.MANV = NV.MANV " +
                "AND " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string mapkh, string matb, string ngaylap, string manv, string sonamkh, string sothangkh, string ghichu)
        {
            string query = "";
            query += string.Format("INSERT INTO PHIEUKHAUHAOTB VALUES ('{0}','{1}','{2}') ", mapkh, ngaylap, manv);
            query += string.Format(" INSERT INTO CHITIET_PHIEUKHAUHAO " +
                "VALUES ('{0}','{1}','{2}',{3},{4},0,0,0,0,0,0,N'{5}')", mapkh, matb, ngaylap, sonamkh, sothangkh, ghichu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool ThemChiTiet(string mapkh, string matb, string ngaylap, string sonamkh, string sothangkh, string ghichu)
        {
            string query = string.Format(" INSERT INTO CHITIET_PHIEUKHAUHAO " +
                "VALUES ('{0}','{1}','{2}',{3},{4},0,0,0,0,0,0,N'{5}')", mapkh, matb, ngaylap, sonamkh, sothangkh, ghichu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string mapkh, string matb, string ngaykh, string sonamkh, string sothangkh, string muckhnam, string muckhthang,
            string khlkkytruoc, string khlkkynay, string gtkhluyke, string gtconlai, string ghichu)
        {
            string query = string.Format("UPDATE CHITIET_PHIEUKHAUHAO SET SONAMKH=N'{0}', SOTHANGKH={1}, MUCKHNAM={2}, MUCKHTHANG={3}, KHLUYKEKYTRUOC={4}, KHLUYKEKYNAY={5}, GTKHLUYKE={6}, GTCONLAI={7},GHICHUKH=N'{8}' "
                + " WHERE MAPKH ='{9}' AND MATB='{10}' AND NGAYKH='{11}'", sonamkh, sothangkh, muckhnam, muckhthang, khlkkytruoc, khlkkynay, gtkhluyke, gtconlai, ghichu, mapkh, matb, ngaykh);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = "DelDataPhieuKhauHao @ma";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma });

            return result > 0;
        }

        public bool XoaCTPhieu(string mapkh, string matb, string ngaykh)
        {
            string query = string.Format("Delete CHITIET_PHIEUKHAUHAO WHERE MAPKH='{0}' AND MATB='{1}' AND NGAYKH='{2}'", mapkh, matb, ngaykh);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
