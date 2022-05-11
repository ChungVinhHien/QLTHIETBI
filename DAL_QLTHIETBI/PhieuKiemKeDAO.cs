using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuKiemKeDAO
    {
        private static PhieuKiemKeDAO instance;

        public static PhieuKiemKeDAO Instance
        {
            get { if (instance == null) instance = new PhieuKiemKeDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuKiemKeDAO() { }

        public DataTable GetDataPhieuKiemKe()
        {
            string query = "select * from PHIEUKIEMKETB";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataPhieuKiemKe(int page)
        {
            string query = "GetDataPhieuKiemKe @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataTaiSanKiemKe(string madv)
        {
            string query = "GetDataThietBiKiemKe @madv";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { madv });
        }
        
        public DataTable GetDataCTPhieuKiemKe(string mapkk)
        {
            string query = "GetDataChiTietPhieuKiemKe @mapkk";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { mapkk });
        }
        public int CountDataPhieuKiemKe()
        {
            string query = "SELECT COUNT(*) FROM PHIEUKIEMKETB";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public bool CheckPhieuKiemKe(string mapsc)
        {
            string query = "select * from CHITIET_PHIEUKIEMKE WHERE MAPKK ='" + mapsc + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public bool CheckPhieuKiemKeByMaTB(string mapkk, string matb)
        {
            string query = "select * from CHITIET_PHIEUKIEMKE WHERE MAPKK ='" + mapkk + "' and MATB='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query;
            if (atr == "NGUOIKK")
            {
                query = "select MAPKK,NGAYLAPPKK,NGAYKK,DV.TENDV,NHANVIENKK1,NHANVIENKK2,NHANVIENKK3,NGAYKK,TONGSL,TONGNGUYENGIA,TONGGTCONLAI"
                + " from PHIEUKIEMKETB PKK, DONVI DV  where PKK.MADV=DV.MADV " +
                "and NHANVIENKK1 like N'%" + value + "%' or NHANVIENKK2 like N'%" + value + "%' or NHANVIENKK3 like N'%" + value + "%'";
            }
            else
            {
                query = "select MAPKK,NGAYLAPPKK,NGAYKK,NHANVIENKK1,DV.TENDV,NHANVIENKK2,NHANVIENKK3,NGAYKK,TONGSL,TONGNGUYENGIA,TONGGTCONLAI"
                + " from PHIEUKIEMKETB PKK, DONVI DV  where PKK.MADV=DV.MADV and " + atr + " like N'%" + value + "%'";
            }           

            return DataProvider.Instance.ExecuteQuery(query);
        }
      

        public bool Them(string mapkk, string ngaykk, string ngaylap, string nhanvien1, string nhanvien2, string nhanvien3, string madv)
        {
            string query = string.Format("INSERT INTO PHIEUKIEMKETB (MAPKK,NGAYKK,NGAYLAPPKK,NHANVIENKK1,NHANVIENKK2,NHANVIENKK3,MADV) " +
                "VALUES  ('{0}', '{1}' , '{2}', N'{3}', N'{4}', N'{5}','{6}')", mapkk, ngaykk, ngaylap,nhanvien1,nhanvien2,nhanvien3,madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string mapkk, string matb, string nguyengiakt,string nguyengiakk, string nguyengiacl, string gtclkt, string gtclkk, string gtclcl,string tinhtrang,string ghichu)
        {
            string query = string.Format("INSERT INTO CHITIET_PHIEUKIEMKE VALUES ('{0}','{1}',{2},{3},{4},{5},{6},{7},N'{8}',N'{9}')", mapkk, matb,nguyengiakt,nguyengiakk,nguyengiacl,gtclkt,gtclkk,gtclcl,tinhtrang,ghichu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string mapkk, string ngaykk, string nv1, string  nv2, string nv3)
        {
            string query = string.Format("UPDATE PHIEUKIEMKETB SET NGAYKK= '{0}', NHANVIENKK1= N'{1}', NHANVIENKK2= N'{2}',NHANVIENKK3=N'{3}'  WHERE MAPKK= '{4}'", ngaykk, nv1, nv2, nv3,mapkk);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool SuaCT(string mapkk, string matb, string nguyengiaKK, string gtconlaiKK,string tinhtrang, string ghichu)
        {
            string query = string.Format("UPDATE CHITIET_PHIEUKIEMKE SET NGUYENGIA_KIEMKE={0},GTCONLAI_KIEMKE={1},TINHTRANG=N'{2}',GHICHUKK=N'{3} '" +
                " WHERE MAPKK = '{4}' AND MATB='{5}'", nguyengiaKK, gtconlaiKK, tinhtrang, ghichu, mapkk, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string mapkk)
        {
            string query = "DelDataPhieuKiemKe @mapkk";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { mapkk });

            return result > 0;
        }
        public bool XoaCT(string mapkk, string matb)
        {
            string query = string.Format("Delete CHITIET_PHIEUKIEMKE WHERE MAPKK='{0}' AND MATB='{1}'", mapkk, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
