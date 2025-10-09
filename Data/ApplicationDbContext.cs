using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Production>()
                .HasMany(e => e.ProductManufactureds)
                .WithOne(e => e.Production)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<User> User { set; get; }
        public DbSet<Rol> Rol { set; get; }
        public DbSet<Profile> Profile { set; get; }

        public DbSet<KG_Catalog> KG_Catalog { set; get; }
        public DbSet<Price_KG> Price_KG { set; get; }

        public DbSet<RawMaterial> RawMaterial { set; get; }
        public DbSet<KgMonitoring> KgMonitoring { set; get; }
        public DbSet<Accounting> Accounting { set; get; }
        public DbSet<ImageRawMaterial> ImageRawMaterial { set; get; }

        public DbSet<CatalogProduction> CatalogProduction { set; get; }
        public DbSet<ImageCatalogProduction> ImageCatalogProduction { set; get; }

        public DbSet<Production> Production { set; get; }
        public DbSet<MaterialProduction> MaterialProduction { set; get; }
    
        public DbSet<DataCatalogProduct> DataCatalogProduct { set; get; }

        public DbSet<ProductManufactured> ProductManufactured { set; get; }

        public DbSet<Sale> Sale { set; get; }
    }
}
    