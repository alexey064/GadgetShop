using Diplom.Models.Model.simple;
using System.ComponentModel.DataAnnotations;
namespace Diplom.Models.Model
{
    public class ProdMovement
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int MovementTypeId { get; set; }
        public virtual PurchaseHistory PurchaseHistory { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual MovementType MovementType { get; set; }
        public virtual Product Product { get; set; }
    }
}
