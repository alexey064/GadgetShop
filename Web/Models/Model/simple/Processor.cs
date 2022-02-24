using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Diplom.Models.Model.simple
{
    public class Processor
    {
        public Processor()
        {
            Notebooks = new HashSet<Notebook>();
            Smartphones = new HashSet<Smartphone>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AddInfo { get; set; }
        public virtual ICollection<Notebook> Notebooks { get; set; }
        public virtual ICollection<Smartphone> Smartphones { get; set; }
    }
}