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
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            frmTableManager f = new frmTableManager();
            this.Hide();
            f.ShowDialog();     //show dialog là top mode, chỉ thực hiện được các thao tác trên form đó
            this.Show();
        }

        private void FrmLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thực sự muốn thoát chương trình?","Thông báo!",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
