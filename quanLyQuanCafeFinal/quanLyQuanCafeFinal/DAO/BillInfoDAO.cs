using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BillInfoDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private BillInfoDAO() { }

        /// <summary>
        /// Trả về danh sách BillInfo
        /// </summary>
        /// <param name="id">Id của thằng Bill</param>
        /// <returns></returns>
        public List<BillInfo> getListBillInfo(int id)
        {
            List<BillInfo> lstBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExcecuteQuery("select * from billInfo where idBill = "+id);

            foreach(DataRow row in data.Rows)
            {
                BillInfo billInfo = new BillInfo(row);
                lstBillInfo.Add(billInfo);
            }

            return lstBillInfo;
        }

        public void insertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExcecuteQuery("exec usp_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });

        }

        public void deleteBillInfoByFoodId(int id)
        {
            DataProvider.Instance.ExcecuteQuery(" delete BillInfo where idFood = " + id);
        }
    }
}
