using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        ShopContext DB;
        public MainController(ShopContext context) 
        {
            DB = context;
        }
        [Route("NewlyAdded")]
        [HttpGet]
        public async Task<string> NewlyAdded()
        {
            MainPageViewModel model = new MainPageViewModel();
            model.NewlyAdded = await DB.Products.OrderByDescending(o => o.AddDate).Take(5).ToListAsync();
            Dictionary<int, int> temp = await DB.ProdMovements.Where(o => o.MovementTypeId == 2).GroupBy(o => o.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Sum(o => o.Count) }).OrderByDescending(o => o.Count)
                .Take(5).ToDictionaryAsync(o => o.ProductId, o => o.Count);
            model.MostBuyed = new List<Product>();
            foreach (KeyValuePair<int, int> item in temp)
            {
                model.MostBuyed.Add(DB.Products.Where(o => o.ProductId == item.Key).FirstOrDefault());
            }
            model.MaxDiscounted = await DB.Products.Where(o => o.DiscountDate > System.DateTime.Now).OrderByDescending(o => o.Discount).Take(5).ToListAsync();
            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        [Route("Catalog")]
        [HttpGet]
        public async Task<string> Catalog(string type) 
        {
            List<Product> catalog = new List<Product>();
            int count = 0;
            switch (type)
            {
                case nameof(Notebook):
                    count = DB.Notebooks.Count();
                    catalog = await DB.Products.Include(o => o.Brand).Include(o => o.Color)
                        .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Processor)
                        .Include(o => o.Notebook).ThenInclude(o => o.ScreenType).Include(o => o.Notebook).ThenInclude(o => o.Videocard)
                        .Where(o => o.Notebook != null).ToListAsync();
                    break;
                case nameof(Smartphone):
                    count = await DB.Smartphones.CountAsync();
                    catalog = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Smartphone)
                        .Include(o => o.Smartphone).ThenInclude(o => o.ChargingType).Include(o => o.Smartphone).ThenInclude(o => o.OS)
                        .Include(o => o.Smartphone).ThenInclude(o => o.Processor).Include(o => o.Smartphone).ThenInclude(o => o.ScreenType)
                        .Where(o => o.Smartphone != null).ToListAsync();
                    break;
                case nameof(Accessory):
                    count = await DB.Accessories.CountAsync();
                    catalog = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory)
                    .Where(o => o.Accessory != null).ToListAsync();
                    break;
                case nameof(WireHeadphone):
                    count = await DB.WireHeadphones.CountAsync();
                    catalog = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
                        .Where(o => o.WireHeadphones != null).ToListAsync();
                    break;
                case nameof(WirelessHeadphone):
                    count = await DB.WirelessHeadphones.CountAsync();
                    catalog = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
                        .Where(o => o.WirelessHeadphones != null).ToListAsync();
                    break;
            }
            string json = JsonConvert.SerializeObject(catalog, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        [HttpGet]
        [Route("GetProduct")]
        public async Task<string> GetProduct(int id) 
        {
            Product model = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory).Include(o => o.Type)
            .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.ScreenType)
            .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor).Include(o => o.Smartphone).ThenInclude(o => o.ChargingType).Include(o => o.Smartphone).ThenInclude(o => o.ScreenType)
            .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
            .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
            .Where(o => o.ProductId == id).FirstOrDefaultAsync();
            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
    }
}