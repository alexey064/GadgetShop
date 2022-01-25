using Diplom.Models.Model.simple;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Model
{
    public class Notebook
    {
        public int Id { get; set; }
        public int ProcessorId { get; set; }
        public int OSId { get; set; }
        public byte RAMCount { get; set; }
        public double? Weight { get; set; }
        public int VideocardID { get; set; }
        public int? HDDSize { get; set; }
        public int? SSDSize { get; set; }
        [Required]
        public double? ScreenSize { get; set; }
        public string ScreenResolution { get; set; }
        public string Camera { get; set; }
        public string WirelessCommunication { get; set; }
        public int? ScreenTypeId { get; set; }
        public short? BatteryCapacity { get; set; }
        public string Outputs { get; set; }
        public string Optional { get; set; }
        public virtual int ProductId { get; set; }
        public virtual OS OS { get; set; }
        public virtual Product product { get; set; }
        public virtual Videocard Videocard { get; set; }
        
        public virtual Processor Processor { get; set; }
        
        public virtual ScreenType ScreenType { get; set; }
    }
}
