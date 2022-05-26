using System;
using System.Collections.Generic;
using System.Text;

namespace WebCoreAppBasics.Models
{
    public class ProductMeals
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductID { get; set; }
        public string Name { get; set; } = "New Meal";
        public string? Description { get; set; }
        public string? MealImageURL { get; set; }
        public string? LargeImageURL { get; set; }
        public decimal Price { get; set; } = 0;
        public string? Size { get; set; }
    }
}
