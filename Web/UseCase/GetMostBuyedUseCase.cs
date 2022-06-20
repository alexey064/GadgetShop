using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Repository;
using Web.Repository.IProductRepo;

namespace Web.UseCase
{
    public class GetMostBuyedUseCase
    {
        IProductRepo<Product> ProductRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;
        public GetMostBuyedUseCase(IProductRepo<Product> ProductRepository, ILinkedRepo<ProdMovement> ProdRepository)
        {
            ProductRepo = ProductRepository; 
            ProdRepo = ProdRepository;
        }
        public async Task<List<Product>> Execute(int skip, int count) 
        {
             List<Product> output = new List<Product>();
             var temp =ProdRepo.GetListFull(0, await ProdRepo.GetCount()).Result.Where(o=>o.MovementTypeId==2).GroupBy(o => o.ProductId)
            .Select(g => new { ProductId = g.Key, Count = g.Sum(o => o.Count) }).OrderByDescending(o => o.Count)
            .Skip(skip).Take(count).ToDictionary(o => o.ProductId, o => o.Count);
            foreach (KeyValuePair<int, int> item in temp)
            {
                output.Add(await ProductRepo.Get(item.Key));
            }
            return output;
        }
    }
}