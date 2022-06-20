using System.Collections.Generic;
using Web.Models.Linked;

namespace Web.Models.Simple
{
    public class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}