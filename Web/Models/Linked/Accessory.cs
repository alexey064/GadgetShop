using System.ComponentModel.DataAnnotations;

namespace Web.Models.Linked
{
    public class Accessory 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
