using CA_InterfaceAdapters_Models.BeerModule;
using CA_InterfaceAdapters_Models.SaleModule;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapters_Data.Contexts.EfCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<BeerModel> Beers { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<ConceptModel> Concepts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeerModel>().ToTable("Beer");
            modelBuilder.Entity<SaleModel>().ToTable("Sale");
            modelBuilder.Entity<ConceptModel>().ToTable("Concept");


            //modelBuilder.Entity<SaleModel>()
            //    .HasMany(c => c.Concepts)
            //    .WithOne()
            //    .HasForeignKey(c => c.IdSale)
            //    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ConceptModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                // <-- ESTA LÍNEA ES LA CLAVE
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .UseIdentityColumn();
            });

            modelBuilder.Entity<SaleModel>(entity =>
            {
                entity.HasMany(s => s.Concepts)
                      .WithOne()
                      .HasForeignKey(c => c.IdSale)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
