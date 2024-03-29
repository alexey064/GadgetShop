﻿using Web.Models.EF;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.IProductRepo;
using Web.UseCase;
using Web.Models.Linked;
using Web.Models.ViewModel;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        int itemPerPage = 10;
        private IProductRepo<Product> ProductRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;
        private ILinkedRepo<Accessory> AccessoryRepo;
        private ILinkedRepo<Notebook> NotebookRepo;
        private ILinkedRepo<Smartphone> SmartRepo;
        private ILinkedRepo<WireHeadphone> WireRepo;
        private ILinkedRepo<WirelessHeadphone> WirelessRepo;
        public ShopController(IProductRepo<Product> ProductRepository, ILinkedRepo<ProdMovement> ProdRepository,
            ILinkedRepo<Accessory> AccessoryRepository, ILinkedRepo<Notebook> NotebookRepository, ILinkedRepo<Smartphone> SmartRepository,
            ILinkedRepo<WireHeadphone> WireRepository, ILinkedRepo<WirelessHeadphone> WirelessRepository) 
        {
            ProductRepo = ProductRepository;
            ProdRepo = ProdRepository;
            AccessoryRepo = AccessoryRepository;
            NotebookRepo = NotebookRepository;
            SmartRepo = SmartRepository;
            WireRepo = WireRepository;
            WirelessRepo = WirelessRepository;
        }
        public async Task<IActionResult> Main([FromServices] ShopContext DB)
        {
            MainPageViewModel model = new MainPageViewModel();
            GetNewlyAddedUseCase NewlyAdded = new GetNewlyAddedUseCase(DB);

            GetMostBuyedUseCase MostBuyed = new GetMostBuyedUseCase(ProductRepo, ProdRepo);
            MaxDiscountedUseCase MaxDiscounted = new MaxDiscountedUseCase(DB);
            model.NewlyAdded = await NewlyAdded.Execute(0, 5);
            model.MostBuyed = await MostBuyed.Execute(0, 5);
            model.MaxDiscounted = await MaxDiscounted.Execute(0, 5);
            return View("main", model);
        }
        public async Task<IActionResult> Catalog(string type, int Page = 1)
        {
            List<Product> output = new List<Product>();
            ViewBag.CurrentPage = Page;
            int count = 0;
            switch (type)
            {
                case nameof(Accessory):
                    ViewBag.Type = nameof(Accessory);
                    count = await AccessoryRepo.GetCount();
                    IEnumerable<Accessory> Accessories = await AccessoryRepo.GetListFull((Page - 1) * itemPerPage, itemPerPage);
                    foreach (Accessory prod in Accessories)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(Notebook):
                    ViewBag.Type = nameof(Notebook);
                    count = await NotebookRepo.GetCount();
                    IEnumerable<Notebook> Notebooks = await NotebookRepo.GetListFull((Page - 1) * itemPerPage, itemPerPage);
                    foreach (Notebook prod in Notebooks) 
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(Smartphone):
                    ViewBag.Type = nameof(Smartphone);
                    count = await SmartRepo.GetCount();
                    IEnumerable<Smartphone> smartphones = await SmartRepo.GetListFull((Page - 1) * itemPerPage, itemPerPage);
                    foreach (Smartphone prod in smartphones)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(WireHeadphone):
                    ViewBag.Type = nameof(WireHeadphone);
                    count = await WireRepo.GetCount();
                    IEnumerable<WireHeadphone> WireHeads = await WireRepo.GetListFull((Page - 1) * itemPerPage, itemPerPage);
                    foreach (WireHeadphone prod in WireHeads)
                    {
                        output.Add(prod.Product);
                    }
                    break;
                case nameof(WirelessHeadphone):
                    ViewBag.Type = nameof(WirelessHeadphone);
                    count = await WirelessRepo.GetCount();
                    IEnumerable<WirelessHeadphone> wireless = await WirelessRepo.GetListFull((Page - 1) * itemPerPage, itemPerPage);
                    foreach (WirelessHeadphone prod in wireless)
                    {
                        output.Add(prod.Product);
                    }
                    break;
            }
            int temp = (int)count / itemPerPage;
            if (temp * itemPerPage == count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            return View(output);
        }
        public async Task<IActionResult> Search(string Name, int Page = 1)
        {
            ViewBag.Search = Name;
            var output = await ProductRepo.Search(Name, (Page - 1) * itemPerPage, itemPerPage);
            int count = await ProductRepo.SearchCount(Name);
            int res = (int)count / itemPerPage;
            if (res * itemPerPage == count)
            {
                ViewBag.MaxPage = res;
            }
            else ViewBag.MaxPage = res + 1;
            ViewBag.Type = "Search";
            return View("Catalog", output);
        }
        public IActionResult Filter()
        {
            return View();
        }
        public async Task<IActionResult> Product(int id)
        {
            Product model = await ProductRepo.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return View("Single", model);
        }
    }
}