using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Web.Models.Linked;

namespace Web.Models.Simple
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
        [JsonIgnore]
        public virtual ICollection<Notebook> Notebooks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Smartphone> Smartphones { get; set; }
    }
}