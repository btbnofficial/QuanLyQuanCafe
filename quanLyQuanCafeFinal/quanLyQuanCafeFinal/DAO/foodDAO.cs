using quanLyQuanCafeFinal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new FoodDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private FoodDAO() { }

        public List<Food> getFoodListByCategoryId(int id)
        {
            List<Food> lstFood = new List<Food>();

            String query = "select * from food where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExcecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Food f = new Food(item);
                lstFood.Add(f);
            }

            return lstFood;
        }
    }

}
