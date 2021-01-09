using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROJECT2
{
    class Program
    {
        static void Main(string[] args)
        {
            string conn = "Data Source=SAGI\\MSSQLSERVER01;Initial Catalog=CityMall;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //CategoriesDAO categoriesDAO = new CategoriesDAO(conn);
            //categoriesDAO.GetAllCategories();
            CategoriesDAO categoriesDAO = new CategoriesDAO(conn);
            // categoriesDAO.AddCategory("Babies");
            //categoriesDAO.GetAllCategories();
            //categoriesDAO.DeleteCategory(6);
            //categoriesDAO.GetCategoryByID(2);
            //categoriesDAO.UpdateCategory(2, "BIBI");
            StoresDAO storesDAO = new StoresDAO(conn);
            //storesDAO.GetAllStores();
            //storesDAO.GetStoresByID(3);
            storesDAO.GetStoreByIDAndFloor(1, 1);
        }
    }
}
