using quanLyQuanCafeFinal.DAO;
using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLyQuanCafeFinal
{
    public partial class frmAccountProfile : Form
    {
        private Account logInAccount;

        public frmAccountProfile(Account acc)
        {
            InitializeComponent();

            LogInAccount = acc;

            changeAccount(acc);
        }

        void changeAccount(Account Loz)
        {
            txtUserName.Text = LogInAccount.Username;
            txtDisplayName.Text = LogInAccount.DisplayName;
        }

        public Account LogInAccount { get => logInAccount; set => logInAccount = value; }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void UpdateAccountInfo()
        {
            string displayName = txtDisplayName.Text;
            string password = txtPassword.Text;
            string newpass = txtNewPassword.Text;
            string username = txtUserName.Text;
            string reenterPass = txtReEnterPass.Text;
            if(!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else
            {
                if (AccountDAO.Instance.updateAccount(username, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    if(updateAccount!=null)
                    {
                        updateAccount(this, new AccountEvent( AccountDAO.Instance.GetAccountByUserNamePassword(username, password)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu!");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add
            {
                updateAccount += value;
            }
            remove
            {
                updateAccount -= value;
            }
        }

        public class AccountEvent:EventArgs
        {
            private Account acc;

            public Account Acc { get => acc; set => acc = value; }

            public AccountEvent (Account acc)
            {
                this.Acc = acc;
            }
        }
    }
}
