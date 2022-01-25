using System.Collections.Generic;
namespace Diplom.Models.Model.simple
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