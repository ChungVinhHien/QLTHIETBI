using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class PhieuKhauHaoObj
    {
        private static string mapkh;
        private static string matb;

        public PhieuKhauHaoObj(string mapkh, string matb)
        {
            Mapkh = mapkh;
            Matb = matb;
        }

        public static string Mapkh { get => mapkh; set => mapkh = value; }
        public static string Matb { get => matb; set => matb = value; }
    }
}
