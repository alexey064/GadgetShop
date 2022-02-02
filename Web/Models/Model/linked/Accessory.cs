using System.ComponentModel.DataAnnotations;


namespace Diplom.Models.Model
{
    public class Accessory 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual int ProductID { get; set; }
        public virtual Product product { get; set; }
    }
}
