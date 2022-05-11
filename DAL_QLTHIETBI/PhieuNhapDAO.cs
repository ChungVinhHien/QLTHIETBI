using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuNhapDAO
    {
        private static PhieuNhapDAO instance;

        public static PhieuNhapDAO Instance
        {
            get { if (instance == null) instance = new PhieuNhapDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuNhapDAO() { }

        public DataTable GetDataPhieuNhap()
        {
            string query = "select * from PHIEUNHAPTB";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
