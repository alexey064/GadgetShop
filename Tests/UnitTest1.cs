using Diplom.Controllers;
using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DipTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;
        public UnitTest1(ITestOutputHelper output) 
        {
            this.output = output;
        }
        [Fact]
        public void AddBrand()
        {
            var option = new DbContextOptionsBuilder<ShopContext>().UseInMemoryDatabase("tempDB").Options;

            ShopContext context = new ShopContext(option);
            Brand brand = new Brand();
            brand.Name = "samsung";
            brand.Id = 1;
            string[] param = new string[] { brand.Name };
            SimpleController controller = new SimpleController(context);
            
            controller.AddOrUpdate(nameof(Brand), param, 0);
            Brand dbBrand = context.Brands.Find(1);
            Assert.Equal(brand.Name, dbBrand.Name);
            Assert.Equal(brand.Id, dbBrand.Id);
        }
        [Fact]
        public void EditBrand()
        {
            var option = new DbContextOptionsBuilder<ShopContext>().UseInMemoryDatabase("tempDB").Options;

            ShopContext context = new ShopContext(option);
            Brand brand = new Brand();
            brand.Name = "samsung";
            brand.Id = 1;
            SimpleController controller = new SimpleController(context);

            string[] param = new string[] { brand.Name };
            controller.AddOrUpdate(nameof(Brand), param, 0); // добавление
            Brand before = context.Brands.Find(1);
            brand.Name = "vega";
            param = new string[] { brand.Name };
            controller.AddOrUpdate(nameof(Brand), param, 1);
            Brand after = context.Brands.Find(1);
            Assert.Equal(brand.Name, after.Name);

        }
        /*
        [Fact]
        public async Task LoadTest() 
        {
            var option = new DbContextOptionsBuilder<ShopContext>();
            option.UseSqlServer("server=LAPTOP-09UR5JLB;Database=Shop;integrated security=true;");
            ShopContext context = new ShopContext(option.Options);
            ShopController controller = new ShopController(context);
            Stopwatch timerAsync = new Stopwatch();
            Stopwatch timerNoAsync = new Stopwatch();

            timerNoAsync.Start();
            for (int i = 0; i < 30; i++)
            {
                controller.Main();
            }
            timerNoAsync.Stop();

            timerAsync.Start();
            for (int i = 0; i < 30; i++)
            {
                await controller.MainAsync();
            }
            timerAsync.Stop();

            output.WriteLine("async:" + timerAsync.Elapsed + " noAsync:" + timerNoAsync.Elapsed);
            Assert.True(timerNoAsync.Elapsed > timerAsync.Elapsed);
        }*/
    }
}
