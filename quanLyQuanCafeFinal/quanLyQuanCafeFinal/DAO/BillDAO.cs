using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
           private set => instance = value;
        }

        private BillDAO() { }

        /// <summary>
        /// trả về BillId của bàn chưa checkOut
        /// thành công: Bill Id
        /// </summary> -1
        /// <param name="id">id của bàn</param>
        /// <returns></returns>
        public int getUnCheckedBillIdByTableId(int id)
        {
            //lay nhung ban chua checkout
            DataTable data = DataProvider.Instance.ExcecuteQuery("select * from Bill where idTable = " + id + " and status = 0");
            if(data.Rows.Count>0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            else
            {
                return -1;
            }
        }
    }
}
