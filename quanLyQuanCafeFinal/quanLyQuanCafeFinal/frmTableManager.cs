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
    public partial class frmTableManager : Form
    {
        public frmTableManager()
        {
            InitializeComponent();
            loadTableList();
        }



        #region methods
        void loadTableList()
        {
            List<Table> tablelst = TableDAO.Instance.loadTableList();

            foreach(Table table in tablelst)
            {
                Button btn = new Button();// { Width = TableDAO.tableWidth, Height = TableDAO.tableHeight};
                btn.Text = table.Name + Environment.NewLine + table.Status;
                if(table.Status=="Trống")
                {
                    btn.BackColor = Color.Gray;
                }
                else
                {
                    btn.BackColor = Color.Aqua;
                }

                flpTable.Controls.Add(btn);
            }
        }
        #endregion



        #region events
        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountProfile f = new frmAccountProfile();
            f.ShowDialog();
        }

        private void AdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.ShowDialog();
        }
        #endregion
    }
}
