using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_QLTHIETBI
{
    public class FormDAO
    {
        private static FormDAO instance;

        public static FormDAO Instance
        {
            get { if (instance == null) instance = new FormDAO(); return instance; }
            private set { instance = value; }
        }

        public FormDAO() { }
        public DataTable GetDataFrom()
        {
            string query = "select * from FORM";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
