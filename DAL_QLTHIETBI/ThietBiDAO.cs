
using System.Data;
using System;

namespace DAL_QLTHIETBI
{
    public class ThietBiDAO
    {
        private static ThietBiDAO instance;

        public static ThietBiDAO Instance
        {
            get { if (instance == null) instance = new ThietBiDAO(); return instance; }
            private set { instance = value; }
        }

        public ThietBiDAO() { }

        public DataTable GetDaTaThietBiPTB(string value)
        {
            string query = "GetDataThietBiPTB @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { value });
        }
        public DataTable GetDataThietBi()
        {
            string query = "SELECT * FROM THIETBI";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataThietBi(int page)
        {
            string query = "GetDataThietBi @page";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { page });
        }
        public DataTable GetDataByMADV(string madv)
        {
            string query = "GetDataThietBiKiemKe @madv";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { madv });
        }
        public DataTable GetDataThietBiPTB(string maptb)
        {
            string query = "GetDataThietBiPTB @maptb";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maptb });
        }
        public DataTable GetDataByMaTB(string matb)
        {
            string query = "SELECT * FROM THIETBI WHERE MATB ='"+ matb +"'";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { matb });
        }
        public DataTable GetHinhAnh(string matb)
        {
            string query = "select HINHANH from THIETBI Where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetHinh(string matb)
        {
            string query = "select HINH1,HINH2,HINH3,HINH4 from HINHANH Where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetPhongThietBi(string matb)
        {
            string query = "select MAPTB from THIETBI Where MATB='" + matb + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetDataNguyenGia_GTHaoMon(string matb)
        {
            string query = "GetNguyenGia_GTHaoMon @matb";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { matb });
        }

        public int CountDataThietBi()
        {
            string query = "SELECT COUNT(*) FROM THIETBI";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int CountTBDangHoatDong()
        {
            string query = "SELECT COUNT(*) FROM THIETBI TB, TRANGTHAI TT WHERE TT.MATT=TB.MATT AND TT.TENTT= N'Đang hoạt động'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public int CountTBDaThanhLy()
        {
            string query = "SELECT COUNT(*) FROM THIETBI TB, TRANGTHAI TT WHERE TT.MATT=TB.MATT AND TT.TENTT= N'Đã thanh lý'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int CountTBBiHuHong()
        {
            string query = "SELECT COUNT(*) FROM THIETBI TB, TRANGTHAI TT WHERE TT.MATT=TB.MATT AND TT.TENTT= N'Bị hư hỏng'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int CountTBBiMat()
        {
            string query = "SELECT COUNT(*) FROM THIETBI TB, TRANGTHAI TT WHERE TT.MATT=TB.MATT AND TT.TENTT= N'Bị mất'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public DataTable TimKiemTheoTen(string atr, string value)
        {
            string query = "select TB.MATB, TENTB, TB.NGAYNHAP,TB.TGDUAVAOSD, TB.NGUYENGIA, DV.TENDV,PB.TENPB,NCC.TENNCC,LTS.TENLOAITS, TT.TENTT, DVT.TENDVT "
                + "from THIETBI TB, PHONGBAN PB, DONVI DV, TRANGTHAI TT, NHACUNGCAP NCC, LOAITAISAN LTS, DONVITINH DVT "
                + "WHERE TT.MATT=TB.MATT AND PB.MAPB=TB.MAPB  AND DV.MADV=TB.MADV AND NCC.MANCC=TB.MANCC AND LTS.MALOAITS=TB.MALOAITS AND DVT.MADVT=TB.MADVT "
                + "and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable TimKiemTS(string donvi, string atr, string value)
        {
            string query = "select TB.MATB, TENTB, replace(convert(varchar,cast(floor(TB.NGUYENGIA) as money),1), '.00', '') AS NGUYENGIA, PB.TENPB,DVT.TENDVT " +
                "from THIETBI TB, DONVI DV, PHONGBAN PB, DONVITINH DVT " +
                "WHERE DV.MADV = TB.MADV AND PB.MAPB = TB.MAPB AND DVT.MADVT = TB.MADVT AND TB.MATT = 'TT0001' AND DV.MADV = N'" + donvi + "' and " + atr + " like N'%" + value + "%'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool CheckExistThietBi(string matb)
        {
            string query = "select * from THIETBI WHERE MATB ='" + matb + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public int CheckPhuongPhap()
        {
            string query = "select ID_PP from PHUONGPHAP WHERE IS_SELECT = 1";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }


        #region CRUD
        public bool Them(string matb, string tentb, string nuocsx, string namsx, string tgbaohanh, string tgduavaosd, string tgtrichkh, string thongsokt, 
            string nguyengia, string ngaynhap, string mancc, string madvt, string mapb, string maloaits, string matt, string madv )
        {
            string query = string.Format("INSERT INTO THIETBI VALUES ('{0}',N'{1}',N'{2}','{3}','{4}', '{5}','{6}',N'{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}')", 
                matb, tentb, nuocsx, namsx, tgbaohanh, tgduavaosd, tgtrichkh, thongsokt,nguyengia, ngaynhap, mancc, madvt, mapb, maloaits, matt, madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ThemSL(string ngay, string matb, string sanluongtk, string sanluong, string dvt )
        {
            string query = string.Format("INSERT INTO CHITIET_KHOILUONG(THOIGIAN,MATB,SANLUONGTK,DONVISP,SANLUONG) VALUES ('{0}','{1}',{2},N'{3}',{4})",ngay, matb, sanluongtk, dvt, sanluong);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public void InsertAnhQRCode(string matb,string hinh1, string hinh2, string hinh3, string hinh4)
        {
            string query = string.Format("INSERT INTO HINHANH VALUES ('{0}','{1}','{2}','{3}','{4}')",matb, hinh1,hinh2,hinh3,hinh4);
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public bool Sua(string matb, string tentb, string nuocsx, string namsx, string tgbaohanh, string tgduavaosd, string tgtrichkh, string thongsokt, string nguyengia, string ngaynhap,  string mancc, string madvt, string mapb, string maloaits, string matt, string madv)
        {
            string query = string.Format("UPDATE THIETBI SET TENTB=N'{0}',NUOCSX=N'{1}', NAMSX='{2}', TGBAOHANH='{3}', TGDUAVAOSD='{4}', TGTRICHKHAUHAO='{5}', THONGSOKYTHUAT=N'{6}', NGUYENGIA= {7}, NGAYNHAP='{8}', MANCC='{9}', MADVT='{10}', MAPB='{11}', MALOAITS='{12}', MATT='{13}', MADV='{14}' " +
                " WHERE MATB= '{15}'", tentb, nuocsx, namsx, tgbaohanh, tgduavaosd,tgtrichkh, thongsokt, nguyengia, ngaynhap, mancc, madvt, mapb, maloaits, matt, madv, matb);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public void EditAnh(string matb, int vitri, string data)
        {
            string query = "";
            switch (vitri)
            {
                case 2:
                    query += string.Format("UPDATE HINHANH SET HINH2='{1}' WHERE MATB='{0}'", matb, data);
                    break;
                case 3:
                    query += string.Format("UPDATE HINHANH SET HINH3='{1}' WHERE MATB='{0}'", matb, data);
                    break;
                case 4:
                    query += string.Format("UPDATE HINHANH SET HINH4='{1}' WHERE MATB='{0}'", matb, data);
                    break;
            }
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public bool Xoa(string ma)
        {
            string query = "DelDataThietBi @ma";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma });

            return result > 0;
        }
        public bool XoaHinh(string matb)
        {
            string query = "Update THIETBI Set HINHANH='' Where MATB='"+matb+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        #endregion
    }
}
