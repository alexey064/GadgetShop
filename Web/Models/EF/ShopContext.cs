using Microsoft.EntityFrameworkCore;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.EF
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            
        }
        public ShopContext() { Database.EnsureCreated(); }
        public virtual DbSet<ChargingType> ChargingTypes { get; set; }
        public virtual DbSet<OS> OS { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<MovementType> MovementTypes { get; set; }
        public virtual DbSet<Notebook> Notebooks { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
        public virtual DbSet<ProdMovement> ProdMovements { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public virtual DbSet<ScreenType> ScreenTypes { get; set; }
        public virtual DbSet<Smartphone> Smartphones { get; set; }
        public virtual DbSet<Simple.Type> Types { get; set; }
        public virtual DbSet<WireHeadphone> WireHeadphones { get; set; }
        public virtual DbSet<WirelessHeadphone> WirelessHeadphones { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Videocard> Videocards { get; set; }
    }
}
