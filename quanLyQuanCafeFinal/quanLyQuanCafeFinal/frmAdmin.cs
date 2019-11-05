using quanLyQuanCafeFinal.DAO;
using quanLyQuanCafeFinal.DTO;
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
        BindingSource foodList = new BindingSource();

        public frmAdmin()
        {
            InitializeComponent();
            load();
        }

        #region methods

        void load()
        {
            dtgvFood.DataSource = foodList;

            loadDateTimePickerBill();
            loadFoodList();
            //loadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
            AddFoodBinding();
            loadFoodCategoryIntoCombobox(cboFoodCategory);
        }

        void loadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.getBillListByDate(checkIn, checkOut);
        }

        void loadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            //Chỉnh cái fromDate về mùng 1 đầu tháng
            dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            //Chỉnh cái toDate về cuối tháng
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }


        void loadFoodList()
        {
            foodList.DataSource = FoodDAO.Instance.getFoodList();
        }

        void AddFoodBinding()
        {
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtFoodId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));

        }

        void loadFoodCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.getListCategory();
            cb.DisplayMember = "Name";
        }

        #endregion



        #region events
        private void BtnViewBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
        }

        private void BtnShowFood_Click(object sender, EventArgs e)
        {
            loadFoodList();
        }

        private void TxtFoodId_TextChanged(object sender, EventArgs e)
        {
            if(dtgvFood.SelectedCells.Count>0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;
                
                Category category = CategoryDAO.Instance.GetCategoyById(id);

                cboFoodCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach (Category item in cboFoodCategory.Items)
                {
                    if(item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cboFoodCategory.SelectedIndex = index;
            }
        }
        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            String name = txtFoodName.Text;
            int idCategory = (cboFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if(FoodDAO.Instance.insertFood(name, idCategory, price))
            {
                MessageBox.Show("Them mon thanh cong!", "thong bao");
                loadFoodList();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Co loi khi them thuc an!", "Thong bao");
            }
        }

        private void BtnEditFood_Click(object sender, EventArgs e)
        {
            String name = txtFoodName.Text;
            int idCategory = (cboFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = int.Parse(txtFoodId.Text);

            if (FoodDAO.Instance.updateFood(name, idCategory, price,id))
            {
                MessageBox.Show("Update thanh cong!", "thong bao");
                loadFoodList();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Co loi khi update thuc an!", "Thong bao");
            }
        }

        private void BtnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtFoodId.Text);

            if (FoodDAO.Instance.deleteFood(id))
            {
                MessageBox.Show("Xoa mon an thanh cong!", "thong bao");
                loadFoodList();
                if(deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Co loi khi xoa thuc an!", "Thong bao");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value;}
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }


        #endregion


    }
}
