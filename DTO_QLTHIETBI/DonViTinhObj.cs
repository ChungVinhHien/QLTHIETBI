using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class DonViTinhObj
    {
        private string madvt;
        private string tendvt;

        public DonViTinhObj(string madvt, string tendvt)
        {
            this.Madvt = madvt;
            this.Tendvt = tendvt;
        }

        public DonViTinhObj(DataRow row)
        {
            this.Madvt = row["MADVT"].ToString();
            this.Tendvt = row["TENDVT"].ToString();
        }
        public string Madvt { get => madvt; set => madvt = value; }
        public string Tendvt { get => tendvt; set => tendvt = value; }
    }
}
