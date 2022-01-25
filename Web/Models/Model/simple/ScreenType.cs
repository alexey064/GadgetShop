using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Diplom.Models.Model.simple
{
    public class ScreenType
    {
        public ScreenType()
        {
            Notebooks = new HashSet<Notebook>();
            Smartphones = new HashSet<Smartphone>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Notebook> Notebooks { get; set; }
        public virtual ICollection<Smartphone> Smartphones { get; set; }
    }
}
