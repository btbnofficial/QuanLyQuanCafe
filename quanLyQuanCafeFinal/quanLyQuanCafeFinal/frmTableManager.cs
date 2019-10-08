using quanLyQuanCafeFinal.DAO;
using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = quanLyQuanCafeFinal.DTO.Menu;

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
                Button btn = new Button() { Width = TableDAO.tableWidth, Height = TableDAO.tableHeight};
                btn.Text = table.Name + Environment.NewLine + table.Status;
                btn.Click += btn_Click;
                btn.Tag = table;

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

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> lstMenu = MenuDAO.Instance.getListMenuByTable(id);

            float totalPrice = 0;

            foreach(Menu item in lstMenu)
            {
                ListViewItem lstvItem = new ListViewItem(item.FoodName.ToString());
                lstvItem.SubItems.Add(item.Count.ToString());
                lstvItem.SubItems.Add(item.Price.ToString());
                lstvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lstvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            txtToalPrice.Text = totalPrice.ToString("c", culture);//Chữ c là định dạng tiền bạc
        }
        #endregion



        #region events

        private void btn_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).ID;
            ShowBill(tableId);

        }

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
