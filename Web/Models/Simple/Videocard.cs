using System.Collections.Generic;
using Web.Models.Linked;

namespace Web.Models.Simple
{
    public class Videocard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Notebook> Notebooks { get; set; }
    }
}