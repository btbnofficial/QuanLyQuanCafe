using quanLyQuanCafeFinal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLyQuanCafeFinal
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            loadAccountList();
            loadFoodList();
        }

        void loadFoodList()
        {
            string query = "select * from food";

            dtgvFood.DataSource = DataProvider.Instance.ExcecuteQuery(query, new object[] { "btbn" });
        }

        void loadAccountList()
        {
            string query = "exec dbo.usp_getAccountByUserName @username ";

            dtgvAccount.DataSource = DataProvider.Instance.ExcecuteQuery(query, new object[] {"btbn"});
        }
    }
}
