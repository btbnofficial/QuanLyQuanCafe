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
            String query = "select * from dbo.account where username = N'"+ username+"' and password = N'"+password+"'";
            DataTable result = DataProvider.Instance.ExcecuteQuery(query);
            return result.Rows.Count > 0;
        }
    }
}
