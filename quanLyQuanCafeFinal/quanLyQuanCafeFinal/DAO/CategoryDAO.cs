using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private CategoryDAO() { }

        public List<Category> getListCategory()
        {
            List<Category> lstCategory = new List<Category>();

            DataTable data = DataProvider.Instance.ExcecuteQuery("Select * from foodCateGory");

            foreach(DataRow item in data.Rows)
            {
                Category cate = new Category(item);
                lstCategory.Add(cate);
            }

            return lstCategory;
        }
    }
}
