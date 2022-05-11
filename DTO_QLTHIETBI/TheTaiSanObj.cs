using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class TheTaiSanObj
    {
        private static string matts;
        private static string matb;
        private static string tentb;
        private static string nguyengia;

        public TheTaiSanObj(string matts, string matb, string tentb, string nguyengia)
        {
            Matts = matts;
            Matb = matb;
            Tentb = tentb;
            Nguyengia = nguyengia;
        }

        public static string Matts { get => matts; set => matts = value; }
        public static string Matb { get => matb; set => matb = value; }
        public static string Tentb { get => tentb; set => tentb = value; }
        public static string Nguyengia { get => nguyengia; set => nguyengia = value; }
    }
}
