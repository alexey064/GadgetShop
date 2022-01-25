using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.Model.simple
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
