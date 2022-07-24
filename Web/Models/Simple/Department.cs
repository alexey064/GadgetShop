using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Web.Models.Linked;

namespace Web.Models.Simple
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
        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<Provider> Providers { get; set; }
    }
}