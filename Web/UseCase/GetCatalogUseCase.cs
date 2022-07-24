using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Repository;

namespace Web.UseCase
{
    public class GetCatalogUseCase : IUseCase<GetCatalogUseCase>
    {
        private ILinkedRepo<Accessory> AccessoryRepo;
        private ILinkedRepo<Notebook> NotebookRepo;
        private ILinkedRepo<Smartphone> SmartRepo;
        private ILinkedRepo<WireHeadphone> WireRepo;
        private ILinkedRepo<WirelessHeadphone> WirelessRepo;
        public GetCatalogUseCase(ILinkedRepo<Accessory> AccessoryRepository, ILinkedRepo<Notebook> NotebookRepository, 
            ILinkedRepo<Smartphone> SmartRepository,ILinkedRepo<WireHeadphone> WireRepository, ILinkedRepo<WirelessHeadphone> WirelessRepository) 
        {
            AccessoryRepo = AccessoryRepository;
            NotebookRepo = NotebookRepository;
            SmartRepo = SmartRepository;
            WireRepo = WireRepository;
            WirelessRepo = WirelessRepository;
        }
        public async Task<List<Product>> Execute(string type, int skip, int count) 
        {
            List<Product> output = new List<Product>();
            switch (type)
            {
                case nameof(Accessory):
                    IEnumerable<Accessory> Accessories = await AccessoryRepo.GetListFull(skip, count);
                    foreach (Accessory prod in Accessories)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(Notebook):
                    IEnumerable<Notebook> Notebooks = await NotebookRepo.GetListFull(skip, count);
                    foreach (Notebook prod in Notebooks)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(Smartphone):
                    IEnumerable<Smartphone> smartphones = await SmartRepo.GetListFull(skip, count);
                    foreach (Smartphone prod in smartphones)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(WireHeadphone):
                    IEnumerable<WireHeadphone> WireHeads = await WireRepo.GetListFull(skip, count);
                    foreach (WireHeadphone prod in WireHeads)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(WirelessHeadphone):
                    IEnumerable<WirelessHeadphone> wireless = await WirelessRepo.GetListFull(skip, count);
                    foreach (WirelessHeadphone prod in wireless)
                    {
                        output.Add(prod.Product);
                    }
                    break;
            }
            return output;
        }
    }
}
