using System.Collections.Generic;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.ViewModel
{
    public class WirelessHeadViewModel
    {
        public WirelessHeadphone EditItem;
        public List<Brand> Brands;
        public List<Department> Departments;
        public List<Type> Types;
        public List<Color> Colors;
        public List<ChargingType> ConnectionType;
    }
}