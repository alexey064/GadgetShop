using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Diplom.Models.Model.simple
{
    public class MovementType
    {
        public MovementType()
        {
            ProdMovements = new HashSet<ProdMovement>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProdMovement> ProdMovements { get; set; }
    }
}
