using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
