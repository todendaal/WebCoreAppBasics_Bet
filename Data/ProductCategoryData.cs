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
    public class ProductCategoryData
    {
        private readonly Helper _Helper = new Helper();

        public List<ProductCategory> get_ProductCategories()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<ProductCategory>("dbo.ProductCategory_GetAll", new { }).ToList();
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


    }
}
