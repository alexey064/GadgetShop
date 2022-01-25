using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModel
{
    public class MainPageViewModel
    {
        public List<Product> NewlyAdded { get; set; }
        public List<Product> MostBuyed { get; set; }
        public List<Product> MaxDiscounted { get; set; }
    }
}
