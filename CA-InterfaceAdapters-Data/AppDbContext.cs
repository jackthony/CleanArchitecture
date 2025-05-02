using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapters_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<BeerModel> Beers { get; set; }
        public DbSet<EMP_EmpresaModel> Empresas { get; set; }
        public DbSet<DepartmentosModel> Departmentos { get; set; }
        public DbSet<ProvinciasModel> Provincias { get; set; }
        public DbSet<DistritosModel> Distritos { get; set; }
        public DbSet<ConstanteModel> Constante { get; set; }
        public DbSet<CatMinisterioModel> Ministerio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeerModel>().ToTable("Beer");
            modelBuilder.Entity<EMP_EmpresaModel>().ToTable("EMP_Empresa");
            modelBuilder.Entity<CatMinisterioModel>().ToTable("CAT_Ministerio");

            modelBuilder.Entity<ConstanteModel>().HasNoKey();
        }
    }
}
