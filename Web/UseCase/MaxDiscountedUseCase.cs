using Diplom.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.IProductRepo;

namespace Web.UseCase
{
    public class MaxDiscountedUseCase
    {
        private IProductRepo<Product> ProductRepo;
        public MaxDiscountedUseCase(IProductRepo<Product> ProductRepository) 
        {
            ProductRepo = ProductRepository;
        }
        public async Task<List<Product>> Execute(int skip, int count) 
        {
            return ProductRepo.GetAll().Result
        .Where(o => o.DiscountDate > System.DateTime.Now).OrderByDescending(o => o.Discount).Skip(skip).Take(count).ToList();
        }
    }
}
