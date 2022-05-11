using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTHIETBI
{
    public class TaikhoanObj
    {
        private static string username;
        private static string password;
        private static string is_admin;
        private static string is_active;
        private static string ngaytao;
        private static string email;

        public static string Username { get => username; set => username = value; }
        public static string Password { get => password; set => password = value; }
        public static string Is_admin { get => is_admin; set => is_admin = value; }
        public static string Is_active { get => is_active; set => is_active = value; }
        public static string Ngaytao { get => ngaytao; set => ngaytao = value; }
        public static string Email { get => email; set => email = value; }

        public TaikhoanObj(string username, string password, string is_admin, string is_active, string ngaytao, string email)
        {
            Username = username;
            Password = password;
            Is_active = is_active;
            Is_admin = is_admin;
            Ngaytao = ngaytao;
            Email = email;
        }
        public TaikhoanObj()
        {

        }
    

                       
    }
}
