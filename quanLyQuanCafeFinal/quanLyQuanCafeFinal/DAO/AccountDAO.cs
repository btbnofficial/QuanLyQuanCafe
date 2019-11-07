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

        public DataTable getListAccount()
        {
            return DataProvider.Instance.ExcecuteQuery("Select username, displayname, type from account");
        }

        public bool insertAccount(String username, String displayName, int type)
        {
            int result = DataProvider.Instance.ExcecuteNonQuery(String.Format("insert dbo.account(username, displayName, type) values (N'{0}', N'{1}', {2})",username, displayName,type));
            return result > 0;
        }

        public bool EditAccount(String username, String displayName, int type)
        {
            int result = DataProvider.Instance.ExcecuteNonQuery(String.Format("update dbo.account set displayName = N'{0}', type = {1} where username = N'{2}'", displayName, type, username));
            return result > 0;
        }

        public bool deleteAccount(String username)
        {
            int resutl = DataProvider.Instance.ExcecuteNonQuery(String.Format("delete dbo.account where username = N'{0}'", username));
            return resutl > 0;
        }

        public bool resetPassword(String username)
        {
            int result = DataProvider.Instance.ExcecuteNonQuery(String.Format("update dbo.Account set password = N'0' where username = N'{0}'",username));

            return result > 0;
        }
    }
}
