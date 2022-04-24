using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
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