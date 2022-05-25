using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebCoreAppBasics.Models;


namespace WebCoreAppBasics.Controllers
{
    public class ProductCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> CategoryProducts = new List<Product>();
            Product p1 = new Product();
            p1.Name = "test1";
            p1.Price = 123.3m;
            p1.NrAvailable = 1;
            p1.ProductCategoryId = System.Guid.NewGuid();
            p1.Description = "dsfsfsdfSDfs ds fds fsdf fsdfsdfsdf sdf df sdf dsf sdf sdf dsf ";
            p1.EnabledYN = true;
            p1.Id = System.Guid.NewGuid();
            p1.SmallImageURL = "McDonalds-Image-Resize.psdBig-mac.png";
            CategoryProducts.Add(p1);
            CategoryProducts.Add(p1);

            return CategoryProducts;
        }
    }
}
