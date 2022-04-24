using Diplom.Models.Model;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class MainPageViewModel
    {
        public List<Product> NewlyAdded { get; set; }
        public List<Product> MostBuyed { get; set; }
        public List<Product> MaxDiscounted { get; set; }
    }
}