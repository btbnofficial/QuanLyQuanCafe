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
            loadDateTimePickerBill();
        }

        #region methods
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

        #endregion



        #region events
        private void BtnViewBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
        }
        #endregion

    }
}
