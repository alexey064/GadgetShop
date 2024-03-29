﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Models.Linked;

namespace Web.Models.Simple
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
