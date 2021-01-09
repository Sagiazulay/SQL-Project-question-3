using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROJECT2
{
    class CategoriesDAO
    {
        private string m_conn = "Data Source=SAGI\\MSSQLSERVER01;Initial Catalog=CityMall;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CategoriesDAO(string conn)
        {
            m_conn = conn;
        }
        private int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (cmd.Connection = new SqlConnection(m_conn))
                    {
                        cmd.Connection.Open();
                        my_logger.Debug($"Connection sting is Open");
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = query;

                        result = cmd.ExecuteNonQuery();
                        Console.WriteLine(result);
                    }
                }
                my_logger.Info("*ExecuteNonQuery Secceeded!*");
                return result;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Could not connect to server!{Ex}");
                return 0;
            }
        }
        public void AddCategory(string category)
        {
            ExecuteNonQuery($"INSERT INTO Categories (Name) VALUES ('{category}');");
        }
        public void UpdateCategory(int category, string name)
        {
            ExecuteNonQuery($"UPDATE Categories SET Name = '{name}' WHERE ID = {category};");
        }

        public void GetAllCategories ()
        {
            string query = "SELECT * FROM CATEGORIES";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<Categories> list = new List<Categories>();

                while (reader.Read() == true)
                {
                    list.Add(
                        new Categories
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString()
                        });
                    Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}");
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Categories from DB [SELECT * FROM CATEGORIES]. Error : {ex}");
            }
            
        }
        public void DeleteCategory(int category_id)
        {
            ExecuteNonQuery($"DELETE FROM Categories WHERE ID = {category_id};");
        }
        public Categories GetCategoryByID(int category_id)
        {
            string query = $"SELECT * FROM Categories WHERE ID = {category_id}";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);                
                    reader.Read();
                        Categories c = new Categories
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString()
                        };
                    Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}");
                    return c;                              
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Categories from DB [SELECT * FROM CATEGORIES]. Error : {ex}");
                return null;
            }


        }
    }
}
