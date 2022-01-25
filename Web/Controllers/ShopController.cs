using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom.Controllers
{
    public class ShopController : Controller
    {
        private ShopContext DB;
        int itemPerPage = 10;
        public ShopController(ShopContext context) 
        {
            DB = context;
            var connectionString = DB.Database.GetDbConnection().ConnectionString;
        }
        public IActionResult Main() 
        {
            MainPageViewModel model = new MainPageViewModel();
            model.NewlyAdded = DB.Products.Include(o => o.Accessory)
                .Include(o => o.Notebook).ThenInclude(o=>o.OS).Include(o=>o.Notebook).ThenInclude(o=>o.Videocard).Include(o=>o.Notebook).ThenInclude(o=>o.Processor)
                .Include(o => o.Smartphone).ThenInclude(o=>o.OS).Include(o=>o.Smartphone).ThenInclude(o=>o.Processor)
                .Include(o=> o.WireHeadphones)
                .Include(o=>o.WirelessHeadphones)
                .OrderByDescending(o=>o.AddDate).Take(5).ToList();
            Dictionary<int, int> temp = DB.ProdMovements.Where(o => o.MovementTypeId == 2).GroupBy(o => o.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Sum(o => o.Count)}).OrderByDescending(o=>o.Count)
                .Take(5).ToDictionary(o=>o.ProductId, o=>o.Count);
            model.MostBuyed = new List<Product>();
            foreach (KeyValuePair<int, int> item in temp)
            {
                model.MostBuyed.Add(DB.Products.Include(o => o.Accessory)
         .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor)
                .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor)
                .Include(o => o.WireHeadphones)
                .Include(o => o.WirelessHeadphones)
                .Where(o => o.ProductId == item.Key).FirstOrDefault());
            }
            model.MaxDiscounted = DB.Products.Include(o => o.Accessory)
         .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor)
                .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor)
                .Include(o => o.WireHeadphones)
                .Include(o => o.WirelessHeadphones)
                .Where(o => o.DiscountDate > System.DateTime.Now).OrderByDescending(o => o.Discount).Take(5).ToList();
            return View(model);
        }
        public IActionResult Catalog(string type, int Page) 
        {
            List<Product> output = new List<Product>();
            ViewBag.CurrentPage = Page;
            int count = 0;
            switch (type)
            {
                case nameof(Notebook):
                    ViewBag.Type = nameof(Notebook);
                    count = DB.Notebooks.Count();
                    output = DB.Products.Include(o => o.Brand).Include(o => o.Color)
                        .Include(o=>o.Notebook).ThenInclude(o=>o.OS).Include(o=>o.Notebook).ThenInclude(o=>o.Processor)
                        .Include(o=>o.Notebook).ThenInclude(o=>o.ScreenType).Include(o=>o.Notebook).ThenInclude(o=>o.Videocard)
                        .Where(o => o.Notebook != null).Skip((Page-1) * itemPerPage).Take(itemPerPage).ToList();
                    break;
                case nameof(Smartphone):
                    ViewBag.Type = nameof(Smartphone);
                    count = DB.Smartphones.Count();
                    output = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Smartphone)
                        .Include(o=>o.Smartphone).ThenInclude(o=>o.ChargingType).Include(o=>o.Smartphone).ThenInclude(o=>o.OS)
                        .Include(o=>o.Smartphone).ThenInclude(o=>o.Processor).Include(o=>o.Smartphone).ThenInclude(o=>o.ScreenType)
                        .Where(o => o.Smartphone != null).Skip((Page-1) * itemPerPage).Take(itemPerPage).ToList();
                    break;
                case nameof(Accessory):
                    ViewBag.Type = nameof(Accessory);
                    count = DB.Accessories.Count();
                    output = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory)
                    .Where(o => o.Accessory != null).Skip((Page-1) * itemPerPage).Take(itemPerPage).ToList();
                        break;
                case nameof(WireHeadphone):
                    ViewBag.Type = nameof(WireHeadphone);
                    count = DB.WireHeadphones.Count();
                    output = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
                        .Where(o => o.WireHeadphones != null).Skip((Page-1) * itemPerPage).Take(itemPerPage).ToList();
                    break;
                case nameof(WirelessHeadphone):
                    ViewBag.Type = nameof(WirelessHeadphone);
                    count = DB.WirelessHeadphones.Count();
                    output = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
                        .Where(o => o.WirelessHeadphones != null).Skip((Page-1) * itemPerPage).Take(itemPerPage).ToList();
                    break;
            }
            int temp =(int) count / itemPerPage;
            if (temp * itemPerPage == count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            return View(output);
        }
        public IActionResult Search(string Name, int Page) 
        {
            ViewBag.Search = Name;
            var temp = DB.Products
                .Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory)
                .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor)
                .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor)
                .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
                .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
                .Where(o => o.Name.ToLower().Contains(Name.ToLower()));
            int count = temp.Count();
            List<Product> output = temp.Skip((Page-1)*itemPerPage).Take(itemPerPage).ToList();
            int res = (int)count / itemPerPage;
            if (res * itemPerPage == count)
            {
                ViewBag.MaxPage = res;
            }
            else ViewBag.MaxPage = res + 1;
            ViewBag.Type = "Search";
            return View("Catalog",output);
        }
        public IActionResult Filter() 
        {
            return View();
        }
        public IActionResult Product(int id) 
        {
            Product model = DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory).Include(o=>o.Type)
                .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor).Include(o=>o.Notebook).ThenInclude(o=>o.Videocard).Include(o=>o.Notebook).ThenInclude(o=>o.ScreenType)
                .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor).Include(o=>o.Smartphone).ThenInclude(o=>o.ChargingType).Include(o=>o.Smartphone).ThenInclude(o=>o.ScreenType)
                .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
                .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
                .Where(o => o.ProductId == id).FirstOrDefault();
            if (model==null)
            {
                return NotFound();
            }
            return View("Single",model);
        }
        public IActionResult AccountSetting() 
        {
            return View();
        }
        public IActionResult ShoppingCart() 
        {
            List<Product> products = new List<Product>();
            if (User.Identity.Name!=null)
            {
                PurchaseHistory hist = DB.PurchaseHistories.Include(o=>o.ProdMovement).ThenInclude(o=>o.Product).Include(o => o.Client).Where(o => o.Client.NickName == User.Identity.Name && o.StatusId == 11).FirstOrDefault();
                if (hist==null)
                {
                    return View(products);
                }
                foreach (ProdMovement item in hist.ProdMovement)
                {
                    item.Product.Count = item.Count;
                    products.Add(item.Product);
                }
            }
            else 
            {
                for (int i = 0; i < 30; i++)
                {
                    byte[] result;
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        int ProductId = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        Product ProdAdd = DB.Products.Where(o => o.ProductId == ProductId).First();
                        HttpContext.Session.TryGetValue("Count" + i, out result);
                        ProdAdd.Count = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        products.Add(ProdAdd);
                    }
                    else break;
                }
            }
            return View(products);
        }
        public IActionResult Buy(int id, int Count, string ReturnUrl) 
        {// если пользователь зарегистрирован, то проверяем есть ли для пользователя корзина продуктов
            if (User.Identity.Name != null)
            {
                PurchaseHistory hist = DB.PurchaseHistories.Include(o => o.ProdMovement).Include(o => o.Client).Where(o => o.Client.NickName == User.Identity.Name && o.StatusId == 11).FirstOrDefault();
                if (hist == null)
                {//Если на пользователя не зарегистрирована корзина продуктов, то надо её создать
                    hist = new PurchaseHistory();
                    hist.Client = DB.Clients.Where(o => o.NickName == User.Identity.Name).FirstOrDefault();
                    hist.StatusId = 11;
                    hist.ProdMovement = new List<ProdMovement>();
                    DB.PurchaseHistories.Add(hist);
                    hist.DepartmentId = DB.Products.Where(o => o.ProductId == id).First().DepartmentId;
                    DB.SaveChanges();
                }
                ProdMovement ExistedProd = hist.ProdMovement.Where(o => o.ProductId == id).FirstOrDefault();
                if (ExistedProd != null)
                {//Если в корзину добавляем ранее добавленный товар
                    ExistedProd.Count = ExistedProd.Count + Count;
                    DB.SaveChanges();
                }
                else
                {
                    ProdMovement prod = new ProdMovement();
                    prod.Count = Count;
                    prod.ProductId = id;
                    prod.MovementTypeId = 2;
                    hist.ProdMovement.Add(prod);
                    DB.SaveChanges();
                }
                Product product = DB.Products.Where(o => o.ProductId == id).FirstOrDefault();
                product.Count = product.Count - Count;
                DB.SaveChanges();
            }
            else
            {
                int counter = 0;
                //Проверяем наличие уже существующей записи.
                for (int i = 0; i < 30; i++)
                {
                    byte[] value;
                    HttpContext.Session.TryGetValue("Prod" + i, out value);
                    if (value==null)
                    {
                        break;
                    }
                    if(int.Parse(System.Text.Encoding.UTF8.GetString(value))==id)
                    {
                        HttpContext.Session.TryGetValue("Count" + i, out value);
                        int prodcount = int.Parse(System.Text.Encoding.UTF8.GetString(value));
                        value = System.Text.Encoding.UTF8.GetBytes((prodcount + i).ToString());
                        HttpContext.Session.Set("Count" + i,value);
                    }
                }
                //Проверяем незанятый ключ.
                for (int i = 0; i < 30; i++)
                {
                    byte[] value;
                    if (!HttpContext.Session.TryGetValue("Prod" + i, out value))
                    {
                        break;
                    }
                    counter++;
                }
                int temp = id;
                byte[] intBytes = System.Text.Encoding.UTF8.GetBytes(temp.ToString());
                HttpContext.Session.Set("Prod" + counter, intBytes);
                temp = Count;
                intBytes = System.Text.Encoding.UTF8.GetBytes(temp.ToString());
                HttpContext.Session.Set("Count" + counter, intBytes);
            }
            return Redirect(ReturnUrl);
        }
        public IActionResult MakeOrder() 
        {//Если в базе данных есть запись о списке покупок, то надо его вывести
            PurchaseHistory hist;
            if (User.Identity.Name != null)
            {
                hist = DB.PurchaseHistories.Include(o => o.Client).Include(o => o.ProdMovement).Where(o => o.Client.NickName == User.Identity.Name).FirstOrDefault();
            }//если пользователь не авторизован, то надо создать новый список покупок
            else
            {
                hist = new PurchaseHistory();
                hist.ProdMovement = new List<ProdMovement>();
                byte[] result;
                HttpContext.Session.TryGetValue("Prod0", out result);
                int ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                hist.DepartmentId = DB.Products.Where(o => o.ProductId == ProductId).FirstOrDefault().DepartmentId;
                for (int i = 0; i < 30; i++)
                {
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        ProdMovement movement = new ProdMovement();
                        ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.Product = DB.Products.Where(o => o.ProductId == ProductId).First();
                        HttpContext.Session.TryGetValue("Count" + i, out result);
                        movement.Count = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        hist.ProdMovement.Add(movement);
                    }
                    else break;
                }
            }
            return View(hist);
        }
        public IActionResult CompleteOrder(int id) 
        {
            if (User.Identity.Name!=null)
            {
                PurchaseHistory purch = DB.PurchaseHistories.Where(o => o.Id == id).First();
                purch.StatusId = 13;
                purch.PurchaseDate = DateTime.Now;
                DB.SaveChanges();
            }
            else 
            {
                PurchaseHistory hist = new PurchaseHistory();
                hist.ProdMovement = new List<ProdMovement>();
                byte[] result;
                HttpContext.Session.TryGetValue("Prod0", out result);
                int ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                hist.DepartmentId = DB.Products.Where(o => o.ProductId == ProductId).FirstOrDefault().DepartmentId;
                hist.StatusId = 13;
                hist.PurchaseDate = DateTime.Now;
                for (int i = 0; i < 30; i++)
                {
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        ProdMovement movement = new ProdMovement();
                        ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.Product = DB.Products.Where(o => o.ProductId == ProductId).First();
                        HttpContext.Session.TryGetValue("Count" + i, out result);
                        movement.Count = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.MovementTypeId = 2;
                        hist.ProdMovement.Add(movement);
                    }
                    else break;
                }
                DB.PurchaseHistories.Add(hist);
                DB.SaveChanges();
                foreach (ProdMovement item in hist.ProdMovement)
                {
                    Product prod = DB.Products.Find(item.ProductId);
                    prod.Count = prod.Count - item.Count;
                }
                DB.SaveChanges();
            }
            return RedirectToAction("main");
        }
        public IActionResult RemoveCart(int id) 
        {
            List<Product> products = new List<Product>();
            if (User.Identity.Name != null)
            {
                PurchaseHistory hist = DB.PurchaseHistories.Include(o => o.ProdMovement).ThenInclude(o => o.Product).Include(o => o.Client).Where(o => o.Client.NickName == User.Identity.Name && o.StatusId == 11).FirstOrDefault();
                int count = hist.ProdMovement.Where(o => o.Product.ProductId == id).Select(o => o.Count).First();
                Product product = DB.Products.Where(o => o.ProductId == id).First();
                hist.ProdMovement.Remove(hist.ProdMovement.Where(o => o.ProductId == id).First());
                product.Count = product.Count + count;
                DB.SaveChanges();
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    byte[] result;
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        int ProductId = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        if (ProductId==id)
                        {
                            HttpContext.Session.Remove("Prod" + i);
                            HttpContext.Session.Remove("Count" + i);
                        }
                    }
                    else break;
                }
            }
            return RedirectToAction("ShoppingCart");
        }
    }
}