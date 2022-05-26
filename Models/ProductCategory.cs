using System;
using System.Collections.Generic;
using System.Text;

namespace WebCoreAppBasics.Models
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool EnabledYN { get; set; }
        public decimal LowestPrice { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
}
}
