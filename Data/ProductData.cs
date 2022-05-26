using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using WebCoreAppBasics.Models;

namespace WebCoreAppBasics.Data
{
    public class ProductData
    {
        private readonly Helper _Helper = new Helper();

        public List<Product> get_Products()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<Product>("dbo.Product_getAll", new { }).ToList();
                return output;
            }
        }

        public List<Product> get_ProductsPerCategory(Guid CategoryID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<Product>("dbo.Product_getPerCategory @CategoryID", new { CategoryID }).ToList();
                return output;
            }
        }

        public void Insert_ProductCategory(ProductCategory itm)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.ProductCategory_Insert @Id, @Name, @EnabledYN", itm);
            }
        }

        public List<ProductMeals> get_MealsPerProduct(Guid ProductID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<ProductMeals>("dbo.get_MealsPerProduct @ProductID", new { ProductID }).ToList();
                return output;
            }
        }

        public ProductMeals get_Meal(Guid ProductID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<ProductMeals>("dbo.get_MealProduct @ProductID", new { ProductID }).FirstOrDefault();
                return output;
            }
        }

        
    }
}
