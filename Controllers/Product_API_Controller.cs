using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebCoreAppBasics.Models;
using WebCoreAppBasics.Data;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCoreAppBasics.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Product_API_Controller : ControllerBase
    {
        private readonly ProductData _db = new ProductData();

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _db.get_Products();
        }

        [HttpGet()]
        public IEnumerable<Product> GetPerCategory(string CategoryID, int startIndex)
        {
            try
            {
                int endIndex = startIndex + 3;
                List<Product> FullList = _db.get_ProductsPerCategory(Guid.Parse(CategoryID));
                return FullList.Skip(startIndex).Take(3);
            }
            catch
            {
                return _db.get_ProductsPerCategory(Guid.Parse("f89474c7-6b71-40fc-9ff9-0dad8ee15c42"));
            }
        }

        [HttpGet("{ProductID}")]
        public IEnumerable<ProductMeals> GetMealsPerProduct(string ProductID)
        {
            try
            {
                return _db.get_MealsPerProduct(Guid.Parse(ProductID));
            }
            catch
            {
                return _db.get_MealsPerProduct(Guid.Parse("908fe6e3-4903-4d76-9c07-a20d1fff6336"));
            }
        }

       

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        

        // POST api/<Product_API_Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Product_API_Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Product_API_Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
