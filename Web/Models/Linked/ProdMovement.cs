using System.ComponentModel.DataAnnotations;
using Web.Models.Simple;

namespace Web.Models.Linked
{
    public class ProdMovement
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        [Required]
        public int MovementTypeId { get; set; }
        public virtual PurchaseHistory PurchaseHistory { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual MovementType MovementType { get; set; }
        public virtual Product Product { get; set; }
    }
}
