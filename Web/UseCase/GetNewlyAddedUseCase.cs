using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.UseCase
{
    public class GetNewlyAddedUseCase
    {
        private readonly ShopContext DB;
        public GetNewlyAddedUseCase(ShopContext context) 
        {
            DB = context;
        }
        public async Task<List<Product>> Execute(int skip, int count) 
        {
            List<Product> products = await DB.Products.Include(o => o.Accessory)
            .Include(o => o.Notebook)
            .Include(o => o.Smartphone)
            .Include(o => o.WireHeadphones)
            .Include(o => o.WirelessHeadphones)
            .OrderByDescending(o => o.AddDate).Skip(skip).Take(count).ToListAsync();
            for (int i = 0; i < products.Count(); i++)
            {
                if (products[i].Accessory != null)
                {
                    products[i] = DB.Products.Include(o => o.Accessory).Include(o => o.Brand).Include(o => o.Color).Include(o => o.Department).Include(o => o.Type).Where(o => o.ProductId == products[i].ProductId).First();
                }
                else if (products[i].Notebook != null)
                {
                    products[i] = DB.Products.Include(o => o.Notebook.OS).Include(o => o.Notebook.Videocard).Include(o => o.Notebook.Processor).Include(o => o.Notebook.ScreenType)
                        .Include(o => o.Brand).Include(o => o.Color).Include(o => o.Department).Include(o => o.Type)
                        .Where(o => o.ProductId == products[i].ProductId).First();
                }
                else if (products[i].Smartphone != null)
                {
                    products[i] = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Department).Include(o => o.Type)
                        .Include(o => o.Smartphone.ChargingType).Include(o => o.Smartphone.OS).Include(o => o.Smartphone.Processor).Include(o => o.Smartphone.ScreenType)
                        .Where(o => o.ProductId == products[i].ProductId).First();
                }
                else if (products[i].WireHeadphones != null)
                {
                    products[i] = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Department).Include(o => o.Type)
                        .Include(o => o.WireHeadphones.ConnectionType)
                        .Where(o => o.ProductId == products[i].ProductId).First();
                }
                else if (products[i].WirelessHeadphones != null) 
                {
                    products[i] = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Department).Include(o => o.Type)
                        .Include(o=>o.WirelessHeadphones.ChargingType)
                        .Where(o => o.ProductId == products[i].ProductId).First();

                }
            }
            return products;
        }
    }
}