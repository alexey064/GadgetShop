using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Model.simple
{
    public class Type
    {
        public Type()
        {
            Clients = new HashSet<Client>();
            ProdMovements = new HashSet<ProdMovement>();
            Products = new HashSet<Product>();
            PurchaseHistories = new HashSet<PurchaseHistory>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<ProdMovement> ProdMovements { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
    }
}