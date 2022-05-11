using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }

        public NhanVienDAO() { }

        public DataTable GetDataNhanVien(string donvi)
        {
            string query = "select MANV, TENNV,GIOITINHNV, NGAYSINHNV,DIACHINV, SDTNV, EMAILNV,NV.MAPB, MACV " +
                " from NHANVIEN NV, PHONGBAN PB, DONVI DV " +
                " WHERE NV.MAPB = PB.MAPB AND PB.MADV = DV.MADV AND DV.MADV = '"+donvi+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataNhanVien()
        {
            string query = "select MANV, TENNV,GIOITINHNV, NGAYSINHNV,DIACHINV, SDTNV, EMAILNV,MAPB, MACV from NHANVIEN ";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataNhanVien(int page)
        {
            string query = "GetDataNhanVien @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetInfoNhanVien(string manv)
        {
            string query = "select MANV, TENNV,GIOITINHNV, NGAYSINHNV,DIACHINV, SDTNV, EMAILNV,PB.TENPB, CV.TENCV, HINHANHNV " +
                "from NHANVIEN NV, PHONGBAN PB, CHUCVU CV " +
                "Where PB.MAPB=NV.MAPB and CV.MACV=NV.MACV and NV.MANV='"+manv+"'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataNhanVienbyTen(string tennv)
        {
            string query = "SELECT * FROM NHANVIEN where TENNV = N'" + tennv + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(MANV) FROM NHANVIEN where TENNV = N'" + tennv + "'");
            if (result < 1)
                return null;
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetHinhAnh(string manv)
        {
            string query = "select HINHANHNV from NHANVIEN Where MANV='" + manv + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetNhanVienDangHoatDong()
        {
            string query = "select NV.TENNV,EMAIL FROM TAIKHOAN TK, NHANVIEN NV WHERE NV.MANV=TK.USERNAME AND IS_ACTIVE = 1 ";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountDataNhanVien()
        {
            string query = "SELECT COUNT(*) FROM NHANVIEN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int CountNhanVienDangHoatDong()
        {
            string query = "select count(*) FROM TAIKHOAN TK, NHANVIEN NV WHERE NV.MANV=TK.USERNAME AND IS_ACTIVE = 1 ";
            return (int) DataProvider.Instance.ExecuteScalar(query);
        }
        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select MANV, TENNV, GIOITINHNV, NGAYSINHNV, DIACHINV, SDTNV, EMAILNV, PB.TENPB, CV.TENCV "
                + "FROM NHANVIEN NV, PHONGBAN PB, CHUCVU CV "
                + "WHERE PB.MAPB = NV.MAPB AND CV.MACV=NV.MACV and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool Them(string ma, string ten, string gioitinh, string ngaysinh, string diachi, string sdt, string email, string mapb, string macv, string hinhanh)
        {
            string query = string.Format("INSERT INTO NHANVIEN VALUES  ('{0}', N'{1}', N'{2}' , '{3}', N'{4}', '{5}', '{6}', '{7}', '{8}','{9}')", ma, ten, gioitinh, ngaysinh, diachi, sdt, email, mapb, macv,hinhanh);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string ma, string ten, string gioitinh, string ngaysinh, string diachi, string sdt, string email, string mapb, string macv, string hinhanh)
        {
            string query = string.Format("UPDATE NHANVIEN SET TENNV = N'{0}', GIOITINHNV= N'{1}', NGAYSINHNV= '{2}', DIACHINV= N'{3}', SDTNV = '{4}', EMAILNV = '{5}', MAPB= N'{6}', MACV= '{7}', HINHANHNV='{8}' WHERE MANV = '{9}'", ten, gioitinh, ngaysinh, diachi, sdt, email, mapb, macv, hinhanh, ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public void SuaHinh(string ma, string hinhanh)
        {
            string query = string.Format("UPDATE NHANVIEN SET HINHANHNV='{0}' WHERE MANV = '{1}'", hinhanh, ma);
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public bool Xoa(string ma)
        {
            string query = string.Format("DELETE FROM NHANVIEN WHERE MANV = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
