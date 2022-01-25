using Diplom.Models.Model.simple;
using System;
using System.ComponentModel.DataAnnotations;
namespace Diplom.Models.Model
{
    public class WirelessHeadphone
    {
        public int Id { get; set; }
        [Required]
        public double BluetoothVersion { get; set; }
        public int Radius { get; set; }
        public short? Battery { get; set; }
        public short? CaseBattery { get; set; }
        public int ChargingTypeId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ChargingType ChargingType { get; set; }
    }
}
