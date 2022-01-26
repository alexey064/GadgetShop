using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SimpleController : Controller
    {
        private ShopContext DB;
        private int itemPerPage = 15;
        private string ErrorMessage;
        public SimpleController(ShopContext cont) 
        {
            DB = cont;
        }
        public async Task<ActionResult> ItemList(string Table, int Page=1) 
        {
            int Count = 0;
            object data = new object(); 
            switch (Table)
            {
              case nameof(Brand):ViewBag.Type = nameof(Brand); Count= await DB.Brands.CountAsync(); data=DB.Brands.Skip((Page-1)*itemPerPage).Take(itemPerPage); break;
              case nameof(Color): ViewBag.Type = nameof(Color); Count = await DB.Colors.CountAsync(); data=DB.Colors.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(ChargingType): ViewBag.Type = nameof(ChargingType); Count = await DB.ChargingTypes.CountAsync(); data= DB.ChargingTypes.Skip((Page - 1)*itemPerPage); break;
              case nameof(Department): ViewBag.Type = nameof(Department); Count = await DB.Departments.CountAsync(); data= DB.Departments.Skip((Page - 1)*itemPerPage).Take(itemPerPage); break;
        case nameof(MovementType): ViewBag.Type=nameof(MovementType); Count = await DB.MovementTypes.CountAsync(); data=DB.MovementTypes.Skip((Page - 1)*itemPerPage).Take(itemPerPage); break;
              case nameof(OS): ViewBag.Type = nameof(OS); Count =await DB.OS.CountAsync();data= DB.OS.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(Processor): ViewBag.Type = nameof(Processor); Count = await DB.Processors.CountAsync(); data = DB.Processors.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(Provider): ViewBag.Type = nameof(Provider); Count = await DB.Providers.CountAsync(); data=DB.Providers.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(ScreenType): ViewBag.Type=nameof(ScreenType); Count= await DB.ScreenTypes.CountAsync(); data =DB.ScreenTypes.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(Type): ViewBag.Type = nameof(Type); Count = await DB.Types.CountAsync(); data= DB.Types.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              case nameof(Videocard): ViewBag.Type = nameof(Videocard); Count = await DB.Videocards.CountAsync(); data = DB.Videocards.Skip((Page - 1) * itemPerPage).Take(itemPerPage); break;
              default: return View("Empty"); break;   
            }
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            return View("CRUD",data);
        }
        public async Task<IActionResult> AddOrUpdate(string ObType, string[] param, int id) 
        {
            switch (ObType)
            {
                case nameof(Brand):
                    if (id == 0)
                    {
                        Brand brand = new Brand();
                        brand.Name = param[0];
                        List<Brand> BRresult =await DB.Brands.Where(o => o.Name == param[0]).ToListAsync();
                        if (BRresult.Count == 0)
                        {
                            DB.Brands.Add(brand);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else {
                        Brand brand = await DB.Brands.FirstOrDefaultAsync(o => o.Id == id);
                        brand.Name = param[0];
                    }
                    break;
                case nameof(Color):
                    if (id == 0)
                    {
                        Color color = new Color();
                        color.Name = param[0];
                        List<Color> CRresult = await DB.Colors.Where(o => o.Name == param[0]).ToListAsync();
                        if (CRresult.Count == 0)
                        {
                            DB.Colors.Add(color);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        Color color = await DB.Colors.FirstOrDefaultAsync(o => o.Id == id);
                        color.Name = param[0];
                    }
                    break;
                case nameof(ChargingType):
                    if (id == 0)
                    {
                        ChargingType CType = new ChargingType();
                        CType.Name = param[0];
                        List<ChargingType> CRresult = await DB.ChargingTypes.Where(o => o.Name == param[0]).ToListAsync();
                        if (CRresult.Count == 0)
                        {
                            DB.ChargingTypes.Add(CType);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else
                    {
                        ChargingType CType = await DB.ChargingTypes.FirstOrDefaultAsync(o => o.Id == id);
                        CType.Name = param[0];
                    }
                    break;
                case nameof(Department):
                    if (id == 0)
                    {
                        Department depart = new Department();
                        depart.Adress = param[0];
                        List<Department> DepResult = await DB.Departments.Where(o => o.Adress == param[0]).ToListAsync();
                        if (DepResult.Count == 0)
                        {
                            DB.Departments.Add(depart);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        Department depart = await DB.Departments.FirstOrDefaultAsync(o => o.DepartmentId == id);
                        depart.Adress = param[0];
                    }
                    break;
                case nameof(MovementType):
                    if (id==0)
                    {
                        MovementType Movetype = new MovementType();
                        Movetype.Name = param[0];
                        List<MovementType> MovResult = await DB.MovementTypes.Where(o => o.Name == param[0]).ToListAsync();
                        if (MovResult.Count == 0)
                        {
                            DB.MovementTypes.Add(Movetype);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        MovementType Movetype = await DB.MovementTypes.FirstOrDefaultAsync(o => o.Id == id);
                        Movetype.Name = param[0];
                    }
                    break;
                case nameof(OS):
                    if (id == 0)
                    {
                        OS os = new OS();
                        os.Name = param[0];
                        List<OS> OSRresult = DB.OS.Where(o => o.Name == param[0]).ToList<OS>();
                        if (OSRresult.Count == 0)
                        {
                            DB.OS.Add(os);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        OS os = await DB.OS.FirstOrDefaultAsync(o => o.id == id);
                        os.Name = param[0];
                    }
                    break;
                case nameof(Processor):
                    if (id == 0)
                    {
                        Processor processor = new Processor();
                        processor.Name = param[0];
                        List<Processor> ProcResult = await DB.Processors.Where(o => o.Name == param[0]).ToListAsync();
                        if (ProcResult.Count == 0)
                        {
                            DB.Processors.Add(processor);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        Processor processor = await DB.Processors.FirstOrDefaultAsync(o => o.Id == id);
                        processor.Name = param[0];
                    }
                    break;
                case nameof(ScreenType):
                    if (id == 0)
                    {
                        ScreenType SCtype = new ScreenType();
                        SCtype.Name = param[0];
                        List<ScreenType> SCTResult = await DB.ScreenTypes.Where(o => o.Name == param[0]).ToListAsync();
                        if (SCTResult.Count == 0)
                        {
                            DB.ScreenTypes.Add(SCtype);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        ScreenType SCtype = await DB.ScreenTypes.FirstOrDefaultAsync(o => o.Id == id);
                        SCtype.Name = param[0];
                    }
                    break;
                case nameof(Type):
                    if (id == 0)
                    {
                        Type type = new Type();
                        type.Name = param[0];
                        type.Category = param[1];
                        List<Type> TypesResult = await DB.Types.Where(o => (o.Category == param[1])).Where(o => o.Name == param[0]).ToListAsync();
                        if (TypesResult.Count == 0)
                        {
                            DB.Types.Add(type);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        Type type = await DB.Types.FirstOrDefaultAsync(o => o.Id == id);
                        type.Name = param[0];
                        type.Category = param[1];
                    }
                    break;
                case nameof(Videocard):
                    if (id == 0)
                    {
                        Videocard card = new Videocard();
                        card.Name = param[0];
                        List<Videocard> CardRresult = await DB.Videocards.Where(o => o.Name == param[0]).ToListAsync<Videocard>();
                        if (CardRresult.Count == 0)
                        {
                            DB.Videocards.Add(card);
                        }
                        else ErrorMessage = "Данный объект уже создан";
                    }
                    else 
                    {
                        Videocard card = await DB.Videocards.FirstOrDefaultAsync(o => o.Id == id);
                        card.Name = param[0];
                    }
                    break;
            }
            await DB.SaveChangesAsync();
            return RedirectToAction("ItemList", new { Table = ObType });
        }
        public async Task<IActionResult> Delete(int Id, string ObType) 
        {
            switch (ObType)
            {
                case nameof(Brand):
                    Brand brand = await DB.Brands.FirstOrDefaultAsync(o => o.Id == Id);
                    if (brand!=null)
                    {
                        DB.Brands.Remove(brand);
                    }
                    break;
                case nameof(Color):
                    Color color = await DB.Colors.FirstOrDefaultAsync(o => o.Id == Id);
                    if (color!=null)
                    {
                        DB.Colors.Remove(color);
                    }
                    break;
                case nameof(ChargingType):
                    ChargingType CType = await DB.ChargingTypes.FirstOrDefaultAsync(o => o.Id == Id);
                    if (CType != null)
                    {
                        DB.ChargingTypes.Remove(CType);
                    }
                    break;
                case nameof(Department):
                    Department depart =await DB.Departments.FirstOrDefaultAsync(o=>o.DepartmentId==Id);
                    if (depart!=null)
                    {
                        DB.Departments.Remove(depart);
                    }
                    break;
                case nameof(MovementType):
                    MovementType Movetype = await DB.MovementTypes.FirstOrDefaultAsync(o => o.Id == Id);
                    if (Movetype!= null)
                    {
                        DB.MovementTypes.Remove(Movetype);
                    }
                    break;
                case nameof(OS):
                    OS os = await DB.OS.FirstOrDefaultAsync(o => o.id == Id);
                    if (os!=null)
                    {
                        DB.OS.Remove(os);
                    }
                    break;
                case nameof(Processor):
                    Processor processor = await DB.Processors.FirstOrDefaultAsync(o => o.Id == Id);
                    if (processor!=null)
                    {
                        DB.Processors.Remove(processor);
                    }
                    break;
                case nameof(ScreenType):
                    ScreenType SCtype = await DB.ScreenTypes.FirstOrDefaultAsync(o => o.Id == Id);
                    if (SCtype!= null)
                    {
                        DB.ScreenTypes.Remove(SCtype);
                    }
                    break;
                case nameof(Type):
                    Type type = await DB.Types.FirstOrDefaultAsync(o => o.Id == Id);
                    if (type!=null)
                    {
                        DB.Types.Remove(type);
                    }
                    break;
                case nameof(Videocard):
                    Videocard card = await DB.Videocards.FirstOrDefaultAsync(o => o.Id == Id);
                    if (card!= null)
                    {
                        DB.Videocards.Remove(card);
                    }
                    break;
            }
            await DB.SaveChangesAsync();
            return RedirectToAction("ItemList", new { Table = ObType });
        }
    }
}