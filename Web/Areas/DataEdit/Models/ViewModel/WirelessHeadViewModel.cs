using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class WirelessHeadViewModel :ICreateViewModel
    {
        public WirelessHeadphone EditItem;
        public List<Brand> Brands;
        public List<Department> Departments;
        public List<Type> Types;
        public List<Color> Colors;
        public List<ChargingType> ConnectionType;
    }
}