using System.Collections.Generic;
using System.Text.Json.Serialization;
using Web.Models.Linked;

namespace Web.Models.Simple
{
    public class ChargingType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<WireHeadphone> WireHeadphone { get; set; }
        [JsonIgnore]
        public virtual ICollection<WirelessHeadphone> WirelessHeadphones { get; set; }
        [JsonIgnore]
        public virtual ICollection<Smartphone> Smartphone { get; set; }
    }
}