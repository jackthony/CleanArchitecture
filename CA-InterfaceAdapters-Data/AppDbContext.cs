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
        public DbSet<Dir_DirectorModel> Director { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ExeptionLogModel> ExeptionsLogs { get; set; }
        public DbSet<EMP_DietaModel> Dietas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeerModel>().ToTable("Beer");
            modelBuilder.Entity<EMP_EmpresaModel>().ToTable("EMP_Empresa");
            modelBuilder.Entity<CatMinisterioModel>().ToTable("CAT_Ministerio");
            modelBuilder.Entity<Dir_DirectorModel>().ToTable("DIR_Director");
            modelBuilder.Entity<UsuarioModel>().ToTable("SEG_Usuario");
            modelBuilder.Entity<ExeptionLogModel>().ToTable("Exception_log");

            modelBuilder.Entity<ConstanteModel>().HasNoKey();

            modelBuilder.Entity<EMP_DietaModel>(entity =>
            {
                entity.ToTable("EMP_DIETA");

                // Clave primaria compuesta
                entity.HasKey(e => new { e.sRUC, e.nTipoCargo });

                entity.Property(e => e.sRUC)
                      .HasMaxLength(11)
                      .IsRequired();

                entity.Property(e => e.nTipoCargo)
                      .IsRequired();

                entity.Property(e => e.mDieta)
                      .HasColumnType("decimal(12,2)")
                      .IsRequired();
            });
        }
    }
}
