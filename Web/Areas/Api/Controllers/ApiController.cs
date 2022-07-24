using Web.Models.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Areas.Api.Controllers;
using Web.Repository;
using Web.Repository.IProductRepo;
using Web.UseCase;
using Web.Models.Linked;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : ControllerBase
    {
        private ShopContext DB;
        private IProductRepo<Product> ProductRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;
        private GetCatalogUseCase GetCatalogCase;
        public ApiController(ShopContext context,IProductRepo<Product> ProductRepository, ILinkedRepo<ProdMovement> ProdRepository,
            IUseCase<GetCatalogUseCase> getCatalogCase)
        {
            DB = context;
            ProductRepo = ProductRepository;
            ProdRepo = ProdRepository;
            GetCatalogCase =(GetCatalogUseCase) getCatalogCase;
        }
        [Route("NewlyAdded")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> NewlyAdded(int skip, int count)
        {
            GetNewlyAddedUseCase NewlyAdded = new GetNewlyAddedUseCase(DB);
            try
            {
                List<Product> output = await NewlyAdded.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostBuyed")]
        public async Task<string> MostBuyed(int skip, int count) 
        {
            try
            {
                GetMostBuyedUseCase MostBuyed = new GetMostBuyedUseCase(ProductRepo,ProdRepo);
                List<Product> output = await MostBuyed.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostDiscounted")]
        public async Task<string> MostDiscounted(int skip, int count) 
        {
            try
            {
                MaxDiscountedUseCase MaxDiscounted = new MaxDiscountedUseCase(DB);
                List<Product> output = await MaxDiscounted.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [Route("Catalog")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> Catalog(string type, int skip, int count) 
        {
            return JsonCommon.ConvertToJson(await GetCatalogCase.Execute(type, skip, count));
        }
        [HttpGet]
        [Route("GetProduct")]
        [AllowAnonymous]
        public async Task<string> GetProduct(int id) 
        {
            try
            {
                Product model = await ProductRepo.Get(id);
                return JsonCommon.ConvertToJson(model);
            }
            catch (Exception e) { return e.Message; }
        }
    }
}