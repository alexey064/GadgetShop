using System.Collections.Generic;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.ViewModel
{
    public class AccessoryViewModel
    {
        public Accessory EditItem;
        public List<Brand> Brands;
        public List<Department> department;
        public List<Type> types;
        public List<Color> Colors;
    }
}