using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using System.Collections.Generic;

namespace Diplom.Models.ViewModel
{
    public class WirelessHeadViewModel :ICreateViewModel
    {
        public WirelessHeadphone EditItem;
        public Dictionary<int, string> Brands;
        public Dictionary<int, string> Departments;
        public Dictionary<int, string> Types;
        public Dictionary<int, string> Colors;
        public Dictionary<int, string> ConnectionType;
    }
}
