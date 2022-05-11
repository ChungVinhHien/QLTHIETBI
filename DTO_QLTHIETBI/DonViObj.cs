using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class DonViObj
    {
        private static string madv;

        public DonViObj(string madv)
        {
            Madv = madv;
        }

        public static string Madv { get => madv; set => madv = value; }
    }
}
