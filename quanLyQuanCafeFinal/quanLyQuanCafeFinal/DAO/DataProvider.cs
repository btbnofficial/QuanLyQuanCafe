using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyQuanCafeFinal.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        /// <summary>
        /// Kiến trúc Singleton, để cho chỉ tồn tại duy nhất 1 đối tượng Dataprovider trong chương trinh
        /// </summary>
        public static DataProvider Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            private set => instance = value;    //Chỉ nội bộ trong lớp này mới được set dữ liệu cho instance còn bên ngoài thì k
        }


        private String connectionString = @"Data Source=(LocalDB)\LocalDBDemo;Initial Catalog=quanLyQuanCafeProject;Integrated Security=True";

        /// <summary>
        /// thực thi Query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DataTable ExcecuteQuery(String query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if(parameter!=null)
            {
                String[] lstPara = query.Split(' ');
                int i = 0;

                foreach(String item in lstPara)
                    {
                        if(item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            connection.Close();
            }

            return data;
        }


        /// <summary>
        /// Trả về số dòng thực thi thành công
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExcecuteNonQuery(String query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    String[] lstPara = query.Split(' ');
                    int i = 0;

                    foreach (String item in lstPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }



        /// <summary>
        /// thực thi Query và trả về cột đầu tiên của dòng đầu tiên trong số các ô được trả về
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object ExcecuteScalar(String query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    String[] lstPara = query.Split(' ');
                    int i = 0;

                    foreach (String item in lstPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
