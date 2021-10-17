using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class AttributesTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public AttributesTemplate()
        {
            ProductAttributes = new List<ProductAttribute>();
        }

    }
}
