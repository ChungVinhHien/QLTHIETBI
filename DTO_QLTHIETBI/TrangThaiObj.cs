using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class TrangThaiObj
    {
        private string matt;
        private string tentt;
        private static string trangthai;

        public TrangThaiObj(string matt, string tentt)
        {
            this.Matt = matt;
            this.Tentt = tentt;
        }

        public TrangThaiObj(DataRow row)
        {
            this.Matt = row["MATT"].ToString();
            this.Tentt = row["TENTT"].ToString();
        }

        public string Matt { get => matt; set => matt = value; }
        public string Tentt { get => tentt; set => tentt = value; }
        public static string Trangthai { get => trangthai; set => trangthai = value; }

       

    }
}
