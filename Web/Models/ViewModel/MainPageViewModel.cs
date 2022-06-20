using System.Collections.Generic;
using Web.Models.Linked;

namespace Web.Models.ViewModel
{
    public class MainPageViewModel
    {
        public List<Product> NewlyAdded { get; set; }
        public List<Product> MostBuyed { get; set; }
        public List<Product> MaxDiscounted { get; set; }
    }
}