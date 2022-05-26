using System;
using System.Collections.Generic;
using System.Text;

namespace WebCoreAppBasics.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "New Product";
        public string? Description { get; set; }
        public string? SmallImageURL { get; set; }
        public string? LargeImageURL { get; set; }
        public decimal Price { get; set; } = 0;
        public int NrAvailable { get; set; } = 1000;
        public Guid ProductCategoryId { get; set; }
        public bool EnabledYN { get; set; } = false;
        public Guid? ParentID { get; set; }
        public char? Size { get; set; }
    }
}
