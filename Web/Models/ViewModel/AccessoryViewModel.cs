using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using System.Collections.Generic;

namespace Diplom.Models.ViewModel
{
    public class AccessoryViewModel :ICreateViewModel
    {
        public Accessory EditItem;
        public Dictionary<int, string> Brands;
        public Dictionary<int, string> department;
        public Dictionary<int, string> types;
        public Dictionary<int, string> Colors;
    }
}
