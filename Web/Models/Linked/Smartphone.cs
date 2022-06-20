using System.ComponentModel.DataAnnotations;
using Web.Models.Simple;

namespace Web.Models.Linked
{
    public class Smartphone 
    {
        public int Id { get; set; }
        [Required]
        public int ScreenTypeId { get; set; }
        public int OSId { get; set; }
        public int MemoryCount { get; set; }
        public byte RAMCount { get; set; }
        public int BatteryCapacity { get; set; }
        [Required]
        public string Camera { get; set; }
        public string PhoneSize { get; set; }
        public int Weight { get; set; }
        public bool NFC { get; set; }
        public short SimCount { get; set; }
        public int ChargingTypeId { get; set; }
        public string Communication { get; set; }
        public double ScreenSize { get; set; }
        [Required]
        public int ProcessorId { get; set; }
        public string Optional { get; set; }
        public string ScreenResolution { get; set; }
        public virtual OS OS { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Processor Processor { get; set; }
        public virtual ScreenType ScreenType { get; set; }
        public virtual ChargingType ChargingType { get; set; }
    }
}
