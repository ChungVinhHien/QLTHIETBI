using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTHIETBI
{
    public class PhuongPhapDAO
    {
        private static PhuongPhapDAO instance;
        private MyFuntions funtions = new MyFuntions();

        public static PhuongPhapDAO Instance
        {
            get { if (instance == null) instance = new PhuongPhapDAO(); return instance; }
            private set { instance = value; }
        }

        public PhuongPhapDAO() { }

        public bool Update(string method)
        {
            string query = string.Format("update PHUONGPHAP set IS_SELECT = '1' where ID_PP = {0}", method);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool CheckPhuongPhap()
        {
            string query = "SELECT * FROM PHUONGPHAP WHERE IS_SELECT = 1";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
    }
}
