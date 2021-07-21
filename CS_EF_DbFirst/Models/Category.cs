using System;
using System.Collections.Generic;

#nullable disable

namespace CS_EF_DbFirst.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryRowId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BasePrice { get; set; }
        public string SubCategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
