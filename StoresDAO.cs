using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROJECT2
{
    class StoresDAO
    {
        private string m_conn = "Data Source=SAGI\\MSSQLSERVER01;Initial Catalog=CityMall;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public StoresDAO(string conn)
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
        public void AddStore(string store)
        {
            ExecuteNonQuery($"INSERT INTO Stores (Name) VALUES ('{store}');");
            my_logger.Info($"New Store '{store}' Was Added Seccessfully");
        }
        public void UpdateStore(int store_id, string name)
        {
            ExecuteNonQuery($"UPDATE Stores SET Name = '{name}' WHERE ID = {store_id};");
            my_logger.Info($"Store '{name}' Was Updated Seccessfully");
        }
        public void DeleteStore(int store_id)
        {
            ExecuteNonQuery($"DELETE FROM Stores WHERE ID = {store_id};");
            my_logger.Info($"New Store '{store_id}' Was Deleted Seccessfully");
        }

        public void GetAllStores()
        {
            string query = "SELECT * FROM Stores";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<Stores> list = new List<Stores>();

                while (reader.Read() == true)
                {
                    list.Add(
                        new Stores
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Floor = Convert.ToInt32(reader["Floor"]),
                            Category_ID = Convert.ToInt32(reader["Category_ID"])
                        });
                    Console.WriteLine($"Store ID : {reader["ID"]}," +
                        $" Store Name : {reader["NAME"].ToString()}," +
                        $" Store Floor : {reader["Floor"]}," +
                        $" Store Category ID : {reader["Category_ID"]}");
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Stores from DB [SELECT * FROM Stores]. Error : {ex}");
            }
        }
        public Stores GetStoresByID(int store_id)
        {
            string query = $"SELECT * FROM Stores WHERE ID = {store_id}";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                Stores c = new Stores
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString(),
                    Floor = Convert.ToInt32(reader["Floor"]),
                    Category_ID = Convert.ToInt32(reader["Category_ID"])
                };
                Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}");
                return c;
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Stores from DB [SELECT * FROM Stores]. Error : {ex}");
                return null;
            }
        }
        public Stores GetStoreByIDAndFloor(int category_id, int floor)
        {
            string query = $"select stores.Name, stores.ID, stores.Floor FROM stores" +
                            $" JOIN categories ON stores.Category_ID = {category_id} WHERE stores.Floor = {floor};";
            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_conn));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                Stores c = new Stores
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString(),
                    Floor = Convert.ToInt32(reader["Floor"]),
                    Category_ID = Convert.ToInt32(reader["Category_ID"])
                };
                Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}, Floor : {reader["Floor"]}");
                return c;
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Stores from DB [SELECT * FROM Stores]. Error : {ex}");
                return null;
            }
        }

    }       //SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate
           //FROM Orders
          //INNER JOIN Customers ON Orders.CustomerID=Customers.CustomerID;
}
