using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new MenuDAO();
                }

                return instance;
            }
            private set => instance = value;
        }

        private MenuDAO() { }

        public List<Menu> getListMenuByTable(int id)
        {
            List<Menu> lstMenu = new List<Menu>();

            String query = "select f.Name, bi.count, f.price, f.price * bi.count as totalPrice from billinfo as bi, Bill as b, Food as f where bi.idBill = b.id and bi.idFood = f.id and b.idTable = "+id+ "and b.status = 0";
            DataTable data = DataProvider.Instance.ExcecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                lstMenu.Add(menu);
            }

            return lstMenu;
        }
    }
}
