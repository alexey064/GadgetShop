using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Model.simple
{
    public class Department
    {
        public Department()
        {
            Clients = new HashSet<Client>();
            Products = new HashSet<Product>();
            Providers = new HashSet<Provider>();
        }
        public int DepartmentId { get; set; }
        [Required]
        public string Adress { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Provider> Providers { get; set; }
    }
}