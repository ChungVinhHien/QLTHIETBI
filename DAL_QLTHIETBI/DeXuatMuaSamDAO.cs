using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class DeXuatMuaSamDAO
    {
        private static DeXuatMuaSamDAO instance;
        private MyFuntions funtions = new MyFuntions();

        public static DeXuatMuaSamDAO Instance
        {
            get { if (instance == null) instance = new DeXuatMuaSamDAO(); return instance; }
            private set { instance = value; }
        }

        public DeXuatMuaSamDAO() { }


        public List<DeXuatMuaSamObj> GetListDeXuatMS(int page, string nguoidx)
        {

            List<DeXuatMuaSamObj> list = new List<DeXuatMuaSamObj>();

            string query = "GetDataDeXuatMS @page , @username";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { page , nguoidx });

            foreach (DataRow item in data.Rows)
            {
                DeXuatMuaSamObj dexuat = new DeXuatMuaSamObj(item);
                list.Add(dexuat);
            }

            return list;
        }
        public List<DeXuatMuaSamObj> GetListDeXuatMuaSam(int page, string value, int type)
        {

            List<DeXuatMuaSamObj> list = new List<DeXuatMuaSamObj>();

            string query;
            if(type==1)
                query = "GetDataDeXuatMuaSam @page , @makh";
            else query = "GetDataDeXuatMS @page , @username";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { page, value });

            foreach (DataRow item in data.Rows)
            {
                DeXuatMuaSamObj dexuat = new DeXuatMuaSamObj(item);
                list.Add(dexuat);
            }

            return list;
        }
        public DataTable GetDataDeXuatMS()
        {
            string query = "select * from DEXUATMUASAM";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataCTDeXuatMS(string ma)
        {
            string query = "SELECT TENTAISAN,SOLUONG,DONVI,GIADUTINH,TONGGIA,GHICHU FROM CHITIET_DEXUATMUASAM WHERE MADXMS='"+ma+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataByMaDX(string madx)
        {
            string query = "select * from DEXUATMUASAM WHERE MADXMS ='" + madx + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }


        public void UpdateTrangThai(string madx, string value)
        {
            string query = string.Format("update DEXUATMUASAM set TRANGTHAI = '{0}' where MADXMS = '{1}'", value, madx);
            DataProvider.Instance.ExecuteNonQuery(query);

        }

        public int CountData()
        {
            string query = "SELECT COUNT(*) FROM DEXUATMUASAM";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int CountData(string nguoidx)
        {
            string query = "SELECT COUNT(*) FROM DEXUATMUASAM WHERE NGUOIDX=N'" + nguoidx + "'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public List<DeXuatMuaSamObj> TimKiemTheoTen(string atr, string value, string nguoidx)
        {
            List<DeXuatMuaSamObj> list = new List<DeXuatMuaSamObj>();

            string query = "select MADXMS,convert(varchar(10),NGAYLAPDX,103) as NGAYLAPDX," +
                " NGUOIDX, NGUOIDUYET,GIAITRINH, TRANGTHAI,SOLUONG, TONGTIEN from DEXUATMUASAM DX" +
                " where " + atr + " like N'%" + value + "%' and NGUOIDX=N'"+ nguoidx + "'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                DeXuatMuaSamObj dexuat = new DeXuatMuaSamObj(item);
                list.Add(dexuat);
            }

            return list;
        }
        public bool Them(string madx, string ngaylap, string nguoidx, string nguoiduyet, string giaitrinh, string trangthai,string soluong, string tongtien)
        {
            string query = string.Format("INSERT INTO DEXUATMUASAM VALUES  ('{0}','{1}',N'{2}',N'{3}',N'{4}',{5},{6},{7})", 
                madx, ngaylap, nguoidx, nguoiduyet, giaitrinh, trangthai, soluong, tongtien);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemCT(string madx, string makh, string tentb, string soluong, string gia, string tong, string ghichu, string donvi)
        {
            string query = string.Format("INSERT INTO CHITIET_DEXUATMUASAM VALUES  ('{0}','{1}',N'{2}',{3},{4},{5},N'{6}',N'{7}')",
                madx, makh, tentb, soluong, gia, tong, ghichu, donvi);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string madx, string makh,string giaitrinh, string tentb, string soluong, string donvi, string gia, string tong, string ghichu)
        {
            string query = string.Format("UPDATE DEXUATMUASAM SET GIAITRINH=N'{2}' WHERE MADXMS='{0}' " +
                "UPDATE CHITIET_DEXUATMUASAM SET MAKHMS='{1}', SOLUONG = {4}, DONVI=N'{5}', GIADUTINH = {6}, TONGGIA = {7}, GHICHU =N'{8}' WHERE MADXMS='{0}' AND TENTAISAN = N'{3}'", 
                madx, makh, giaitrinh, tentb, soluong, donvi, gia, tong, ghichu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Duyet(string madx, int trangthai,string nguoiduyet)
        {
            string query = "";
            if (trangthai == 1)
                query = string.Format("UPDATE DEXUATMUASAM SET TRANGTHAI=1, NGUOIDUYET=N'{1}' WHERE MADXMS='{0}' ",madx, nguoiduyet);
            else query = string.Format("UPDATE DEXUATMUASAM SET TRANGTHAI=0 WHERE MADXMS='{0}' ", madx);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Xoa(string ma)
        {
            string query = "DelDataDeXuatMS @ma";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma });

            return result > 0;
        }

        public bool XoaCT(string madx,string makh, string tentb)
        {
            string query = string.Format("DELETE CHITIET_DEXUATMUASAM WHERE MADXMS = '{0}' AND MAKHMS = '{1}' AND TENTAISAN = N'{2}' ",
                madx,makh,tentb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
