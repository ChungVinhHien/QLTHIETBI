using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTHIETBI
{
    public class PhanQuyenDAO
    {
        private static PhanQuyenDAO instance;
        private MyFuntions funtions = new MyFuntions();

        public static PhanQuyenDAO Instance
        {
            get { if (instance == null) instance = new PhanQuyenDAO(); return instance; }
            private set { instance = value; }
        }

        public PhanQuyenDAO() { }

        public DataTable GetPhanQuyen(string username)
        {
            string query = "select F.FORM_NAME, IS_ALL, IS_VIEW, IS_ADD, IS_EDIT, IS_DELETE, IS_PRINT, IS_APPROVE "
            + " from CHITIET_FORM CT, FORM F "
            + " where F.ID_FORM = CT.ID_FORM AND USERNAME = '"+username+"'";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetChiTietQuyen(string username , string form)
        {
            string query = "GetChiTietQuyen @username , @form";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { username, form });
        }

        public void Sua(string form, string value, string username, string formname)
        {
            if (value == "True") value = "0";
            else value = "1"; 

            string query = string.Format("update CHITIET_FORM SET {0}={1} from CHITIET_FORM CT, FORM F " +
                "where F.ID_FORM = CT.ID_FORM AND USERNAME = '{2}' and FORM_NAME = N'{3}'", form, value, username, formname);

            string query2 = query;

            DataProvider.Instance.ExecuteNonQuery(query);
            DataProvider.Instance.ExecuteNonQuery(query2);

        }
        
    }
}
