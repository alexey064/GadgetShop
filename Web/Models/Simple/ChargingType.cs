using System.Collections.Generic;
using Web.Models.Linked;

namespace Web.Models.Simple
{
    public class ChargingType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<WireHeadphone> WireHeadphone { get; set; }
        public virtual ICollection<WirelessHeadphone> WirelessHeadphones { get; set; }
        public virtual ICollection<Smartphone> Smartphone { get; set; }
    }
}