using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModel
{
    public class SmartphoneViewModel
    {
        public Smartphone EditItem;
        public Dictionary<int, string> Processors;
        public Dictionary<int, string> ScreenTypes;
        public Dictionary<int, string> Brands;
        public Dictionary<int, string> Types;
        public Dictionary<int, string> Department;
        public Dictionary<int, string> OS;
        public Dictionary<int, string> Colors;
        public Dictionary<int, string> ChargingType;
    }
}
