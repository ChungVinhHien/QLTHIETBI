using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class HoatDongDAO
    {
        private static HoatDongDAO instance;

        public static HoatDongDAO Instance
        {
            get { if (instance == null) instance = new HoatDongDAO(); return instance; }
            private set { instance = value; }
        }

        public HoatDongDAO() { }
        public DataTable GetDataHoatDong()
        {
            string query = "select * from HOATDONG";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
