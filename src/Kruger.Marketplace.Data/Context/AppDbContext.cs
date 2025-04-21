using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Data.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kruger.Marketplace.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {

        #region CADASTRO BASICO
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(e => e.GetProperties()
                                                       .Where(p => p.ClrType == typeof(string) && p.GetColumnType() is null)))
                property.SetColumnType("varchar(100)");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(t => t.GetProperties())
                                                       .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                property.SetColumnType("decimal(18,2)");

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "DataHoraCadastro")))
                property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.NoAction;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            SeedDatabase.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
