using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class NhomTaiSanObj
    {
        private string manhomts;
        private string tennhomts;
        public NhomTaiSanObj(string manhomts, string tennhomts)
        {
            this.Manhomts = manhomts;
            this.Tennhomts = tennhomts;
        }
        public NhomTaiSanObj(DataRow row)
        {
            this.Manhomts = row["MANHOMTS"].ToString();
            this.Tennhomts = row["TENNHOMTS"].ToString();
        }
        public string Manhomts { get => manhomts; set => manhomts = value; }
        public string Tennhomts { get => tennhomts; set => tennhomts = value; }
    }
}
