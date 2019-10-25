using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AccountDAO();

                }
                return instance;
            }
            private set => instance = value;
        }

        private AccountDAO()
        {

        }

        public bool logIn(String username, String password)
        {
            String query = "usp_logIn @username , @password";
            DataTable result = DataProvider.Instance.ExcecuteQuery(query,new object[] {username, password });
            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserNamePassword(String username, String password)
        {
            DataTable data = DataProvider.Instance.ExcecuteQuery("Select * from Account where userName = N'"+username+"' and password = N'"+password+"'");

            //Nếu có username đó sẽ trả về account, nếu không có sẽ trả về null
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool updateAccount(string username, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExcecuteNonQuery("exec usp_updateAccount @username , @displayName , @password , @newPassword ", new object[] { username, displayName,pass,newPass});

            return result > 0;
        }
    }
}
