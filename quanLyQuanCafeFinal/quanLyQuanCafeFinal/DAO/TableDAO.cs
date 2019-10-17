using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new TableDAO();

                }
                return instance;
            }

            private set => instance = value;
        }

        public static int tableWidth = 120;
        public static int tableHeight = 120;

        public List<Table> loadTableList()
        {
            List<Table> tablelst = new List<Table>();
            DataTable data = DataProvider.Instance.ExcecuteQuery("exec usp_GetTableList");

            foreach(DataRow row in data.Rows)
            {
                Table table = new Table(row);
                tablelst.Add(table);
            }

            return tablelst;
        }

        public void SwitchTable (int id1, int id2)
        {
            DataProvider.Instance.ExcecuteQuery("usp_SwitchTable @idTable1 , @idTable2", new object[] { id1, id2 });
        }
    }
}
