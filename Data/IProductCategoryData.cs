using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreAppBasics.Models;

namespace WebCoreAppBasics.Data
{
    public interface IProductCategoryData
    {
        Task<IEnumerable<ProductCategory>> get_ProductCategories();
        Task<ProductCategory?> get_ProductCategory(Guid id);
        Task Insert_ProductCategory(ProductCategory itm);
        Task Update_ProductCategory(ProductCategory itm);
    }
}