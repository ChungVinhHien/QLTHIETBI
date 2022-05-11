using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class LoaiTaiSanObj
    {
        private string maloaits;
        private string tenloaits;
        private string namkhmax;
        private string namkhmin;
        private string manhomts;

       

        public LoaiTaiSanObj(string maloaits, string tenloaits, string namkhmin, string namkhmax, string manhomts)
        {
            this.Maloaits = maloaits;
            this.Tenloaits = tenloaits;
            this.Namkhmin = namkhmin;
            this.Namkhmax = namkhmax;
            this.Manhomts = manhomts;
        }

        public LoaiTaiSanObj(DataRow row)
        {
            this.Maloaits = row["MALOAITS"].ToString();
            this.Tenloaits = row["TENLOAITS"].ToString();
            this.Namkhmin = row["NAMKHMIN"].ToString();
            this.Namkhmax = row["NAMKHMAX"].ToString();
            this.Manhomts = row["MANHOMTS"].ToString();
        }

        public string Maloaits { get => maloaits; set => maloaits = value; }
        public string Tenloaits { get => tenloaits; set => tenloaits = value; }
        public string Namkhmax { get => namkhmax; set => namkhmax = value; }
        public string Namkhmin { get => namkhmin; set => namkhmin = value; }
        public string Manhomts { get => manhomts; set => manhomts = value; }
    }
}
