using System.Collections.Generic;
namespace Diplom.Models.Model.simple
{
    public class Videocard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Notebook> Notebooks { get; set; }
    }
}