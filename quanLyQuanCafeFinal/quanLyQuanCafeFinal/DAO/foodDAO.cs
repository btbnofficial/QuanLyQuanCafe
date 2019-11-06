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

        public List<Food> getFoodList()
        {
            List<Food> lstFood = new List<Food>();

            String query = "select * from food";

            DataTable data = DataProvider.Instance.ExcecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food f = new Food(item);
                lstFood.Add(f);
            }

            return lstFood;
        }

        public bool insertFood(String name, int idCategory, float price)
        {
            String query = String.Format("insert dbo.food ( Name, idCategory , price ) values (N'{0}', {1}, {2})", name, idCategory, price);
            int result = DataProvider.Instance.ExcecuteNonQuery(query);

            return result > 0;
        }

        public bool updateFood(String name, int idCategory, float price, int id)
        {
            String query = String.Format("update Food set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, idCategory, price,id);
            int result = DataProvider.Instance.ExcecuteNonQuery(query);

            return result > 0;
        }

        public bool deleteFood(int id)
        {
            BillInfoDAO.Instance.deleteBillInfoByFoodId(id);
            int result = DataProvider.Instance.ExcecuteNonQuery("Delete Food where id = " + id);

            return result > 0;
        }

        public List<Food> searchFoodByName(String name)
        {
            List<Food> lstFood = new List<Food>();

            String query = string.Format("select * from dbo.Food where dbo.fuConvertToUnsign1(name) like N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name );

            DataTable data = DataProvider.Instance.ExcecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food f = new Food(item);
                lstFood.Add(f);
            }

            return lstFood;
        }
    }

}
