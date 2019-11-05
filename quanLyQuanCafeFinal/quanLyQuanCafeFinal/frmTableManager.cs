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

        private Account logInAccount;

        public Account LogInAccount
        {
            get
            {
                return logInAccount;
            }
            set
            {
                logInAccount = value;
                //ChangeAccount(accLoz.Type);
            }
        }

        public frmTableManager(Account acc)
        {
            InitializeComponent();

            this.LogInAccount = acc;
            ChangeAccount(acc.Type);

            loadTableList();
            loadCategory();
            loadComboboxTable(cboSwitchTable);
            Account fuck = new Account(acc.Username,acc.DisplayName,acc.Type,acc.Password);
        }



        #region methods

        /// <summary>
        /// Phân quyền acc, loại 1 thì là admin còn loại 0 thì là staff
        /// </summary>
        /// <param name="type"></param>
        void ChangeAccount(int type)
        {
            adminTSMI.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "  (" + LogInAccount.DisplayName + " )";
        }

        void loadCategory()
        {
            List<Category> lstCategory = CategoryDAO.Instance.getListCategory();
            cboCategory.DataSource = lstCategory;
            cboCategory.DisplayMember = "name";
        }

        void loadFoodListByCategoryId(int id)
        {
            List<Food> lstFood = FoodDAO.Instance.getFoodListByCategoryId(id);
            cboFood.DataSource = lstFood;
            cboFood.DisplayMember = "Name";
        }

        void loadTableList()
        {
            flpTable.Controls.Clear();

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
            //Đổi trạng thái về Việt Nam
            //CultureInfo culture = new CultureInfo("vi-VN");

            //txtToalPrice.Text = totalPrice.ToString("c", culture);//Chữ c là định dạng tiền bạc
            txtToalPrice.Text = totalPrice.ToString();
        }
        
        void loadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.loadTableList();
            cb.DisplayMember = "Name";
        }
        
        
        
        #endregion



        #region events

        private void btn_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);

        }

        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountProfile f = new frmAccountProfile(LogInAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        private void f_UpdateAccount(object sender, frmAccountProfile.AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + " )";
        }

        private void AdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.InsertFood += F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.ShowDialog();
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryId((cboCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryId((cboCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            loadTableList();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryId((cboCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag!=null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void CboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox combo = sender as ComboBox;

            //Kiểm tra nếu combobox của Category mà trống thì sẽ k thực hiện clg
            if (combo.SelectedItem == null)
            {
                return;
            }

            Category selected = combo.SelectedItem as Category;
            id = selected.ID;
            loadFoodListByCategoryId(id);

        }

        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            //Lấy ra cái đối tượng table hiện tại
            Table table = lsvBill.Tag as Table;

            if(table == null)
            {
                MessageBox.Show("Hay chon ban!");
                return;
            }

            int idBill = BillDAO.Instance.getUnCheckedBillIdByTableId(table.ID);        //Lấy IdBill của cái bàn hiện tại
            int idFood = (cboFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            //Nếu IDBill = -1: không có Bill nào đang tồn tại thì phải thêm Bill mới
            if(idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.insertBillInfo(BillDAO.Instance.getMaxIDBill(), idFood, count); 
            }
            else //khi Bill đã tồn tại
            {
                BillInfoDAO.Instance.insertBillInfo(idBill, idFood, count);
            }
            ShowBill(table.ID);

            loadTableList();
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.getUnCheckedBillIdByTableId(table.ID);
            int discount = (int)nmDiscount.Value;
            double totalPrice = Convert.ToDouble(txtToalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if(idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá \n=> {1} - {1}/100 x {2} = {3}" ,table.Name , totalPrice , discount , finalTotalPrice  ), "thông báo!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill,discount,(float)finalTotalPrice);
                    ShowBill(table.ID);

                    loadTableList();
                }
            }
        }

        private void BtnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cboSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(String.Format("Ban co that su muon chuyen ban {0} qua ban {1} ", (lsvBill.Tag as Table).Name, (cboSwitchTable.SelectedItem as Table).Name),"Thong bao!", MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                loadTableList();
            }
        }


        #endregion

        
    }
}
