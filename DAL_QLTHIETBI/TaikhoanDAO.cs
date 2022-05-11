using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;
using DTO_QLTHIETBI;

namespace DAL_QLTHIETBI
{
    public class TaikhoanDAO
    {
        private static TaikhoanDAO instance;
        private MyFuntions funtions = new MyFuntions();

        public static TaikhoanDAO Instance {
            get { if (instance == null) instance = new TaikhoanDAO(); return instance; }
            private set { instance = value; } 
        }

        public TaikhoanDAO() { }

        public bool Login(string userName, string passWord)
        {
            string temp = funtions.Encrypt(passWord);
            string pass = funtions.ReverseString(temp);
           
            string query = "check_login @username , @password";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, pass});

            return result.Rows.Count > 0;
        }
        public bool CheckEmailTaiKhoan(string username, string email)
        {
            string query = "EXEC CheckEmail '" + username +"','" + email +"'";

            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }
        public List<AccountObj> GetListTaiKhoan(int page)
        {

            List<AccountObj> list = new List<AccountObj>();

            string query = "GetDataTaiKhoan @page";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { page });

            foreach (DataRow item in data.Rows)
            {
                AccountObj account = new AccountObj(item);
                list.Add(account);
            }

            return list;
        }
        public DataTable GetDataTaiKhoan()
        {
            string query = "select * from TAIKHOAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public DataTable GetTaiKhoan(string username)
        {
            string query = "select * from TAIKHOAN WHERE USERNAME='" + username + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool UpdatePassword(string username, string passWord)
        {
            string temp = funtions.Encrypt(passWord);
            string pass = funtions.ReverseString(temp);

            string query = string.Format("update TAIKHOAN set PASSWORDS = '{0}' where USERNAME = '{1}'", pass,username);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public void UpdateIsActive(string username, string value)
        {
            string query = string.Format("update TAIKHOAN set IS_ACTIVE = '{0}' where USERNAME = '{1}'", value, username);
            DataProvider.Instance.ExecuteNonQuery(query);
            
        }

        public int CountDataTaiKhoan()
        {
            string query = "SELECT COUNT(*) FROM TAIKHOAN";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public List<AccountObj> TimKiemTheoTen(string atr, string value)
        {            
            List<AccountObj> list = new List<AccountObj>();

            string query = "select * FROM TAIKHOAN "
                + "WHERE " + atr + " like N'%" + value + "%'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                AccountObj account = new AccountObj(item);
                list.Add(account);
            }

            return list;
        }
        public bool Them(string username, string password, string is_admin, string email)
        {
            string temp = funtions.Encrypt(password);
            string pass = funtions.ReverseString(temp);
            string ngaytao = DateTime.Now.ToString("MM/dd/yyyy");

            string query = string.Format("INSERT INTO TAIKHOAN VALUES  ('{0}','{1}',{2},0,'{3}','{4}')", username,pass,is_admin,ngaytao, email);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Sua(string username, string is_admin, string email)
        {
            string query = string.Format("UPDATE TAIKHOAN SET IS_ADMIN={0}, EMAIL='{1}' WHERE USERNAME='{2}'", is_admin, email, username);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool CapNhatAdmin(string username, string is_admin)
        {
            string query = "CapNhatAdmin @username , @quyen";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, is_admin });

            return result.Rows.Count > 0;
        }
        public bool Xoa(string ma)
        {
            string query = string.Format("delete from CHITIET_FORM where USERNAME='{0}' DELETE TAIKHOAN WHERE USERNAME = '{0}'", ma);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
