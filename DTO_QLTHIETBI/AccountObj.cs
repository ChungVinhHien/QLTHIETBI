using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_QLTHIETBI
{
    public class AccountObj
    {
        private string username;
        private string password;
        private string is_admin;
        private string is_active;
        private string ngaytao;
        private string email;

        public AccountObj(string userName, string password, string is_admin, string is_active, string ngaytao,string email)
        {
            this.Username = userName;
            this.Password = password;
            this.Is_active = is_active;
            this.Is_admin = is_admin;
            this.Ngaytao = ngaytao;
            this.Email = email;
        }

        public AccountObj(DataRow row)
        {
            this.Username = row["USERNAME"].ToString();
            this.Password = " * * * * * ";
            if (row["IS_ACTIVE"].ToString() == "True")
            {
                this.Is_active = "Online";
            }
            else this.Is_active = "Offline";
            if (row["IS_ADMIN"].ToString() == "True")
            {
                this.Is_admin = "Admin";
            }
            else this.Is_admin = "User";
            this.Ngaytao = row["NGAYTAO"].ToString();
            this.Email = row["EMAIL"].ToString();
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Is_admin { get => is_admin; set => is_admin = value; }
        public string Is_active { get => is_active; set => is_active = value; }
        public string Ngaytao { get => ngaytao; set => ngaytao = value; }
        public string Email { get => email; set => email = value; }

        
    }
}
