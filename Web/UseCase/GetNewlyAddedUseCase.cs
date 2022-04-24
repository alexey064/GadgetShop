using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.IProductRepo;

namespace Web.UseCase
{
    public class GetNewlyAddedUseCase
    {
        private ShopContext DB;
        IProductRepo<Product> ProductRepo;
        public GetNewlyAddedUseCase(IProductRepo<Product> ProdRepository) 
        {
        ProductRepo = ProdRepository;
        }
        public async Task<List<Product>> Execute(int skip, int count) 
        {
            List<Product> products = await ProductRepo.GetAll() as List<Product>;
            return products.OrderByDescending(o => o.AddDate).Skip(skip).Take(count).ToList();
        }
    }
}
