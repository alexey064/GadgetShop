using System.Collections.Generic;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.ViewModel
{
    public class SmartphoneViewModel
    {
        public Smartphone EditItem;
        public List<Processor> Processors;
        public List<ScreenType> ScreenTypes;
        public List<Brand> Brands;
        public List<Type> Types;
        public List<Department> Department;
        public List<OS> OS;
        public List<Color> Colors;
        public List<ChargingType> ChargingType;
    }
}