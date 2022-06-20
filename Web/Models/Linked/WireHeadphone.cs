using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.Simple;

namespace Web.Models.Linked
{
    public class WireHeadphone
    {
        public int Id { get; set; }
        public int ConnectionTypeId { get; set; }
        public double WireLenght { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("ConnectionTypeId")]
        public virtual ChargingType ConnectionType { get; set; }
    }
}
