using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
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