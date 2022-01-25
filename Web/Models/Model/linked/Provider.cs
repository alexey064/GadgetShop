using Diplom.Models.Model.simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Diplom.Models.Model
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<ProdMovement> ProdMovement { get; set; }
    }
}
