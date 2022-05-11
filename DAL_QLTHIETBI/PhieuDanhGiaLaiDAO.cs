using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class PhieuDanhGiaLaiDAO
    {
        private static PhieuDanhGiaLaiDAO instance;

        public static PhieuDanhGiaLaiDAO Instance
        {
            get { if (instance == null) instance = new PhieuDanhGiaLaiDAO(); return instance; }
            private set { instance = value; }
        }

        public PhieuDanhGiaLaiDAO() { }

        public DataTable GetDataPhieuDanhGiaLai()
        {
            string query = "select * from PHIEUDANHGIALAI";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
