using Kruger.Marketplace.Business.Models.CadastroBasico;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kruger.Marketplace.Data.Seed
{
    public static class SeedDatabase
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Categoria>().HasData(
                new Categoria(Guid.Parse("2CE8CE71-E766-41EE-839A-F0824F7FD3B8"), "Vestuário", "Categoria destinada a vestuário"),
                new Categoria(Guid.Parse("7B87817F-F13C-4A68-87C5-0FC28EDA22CE"), "Eletrônicos", "Eletrônicos e eletrodomésticos"),
                new Categoria(Guid.Parse("63CB29C3-DB97-4744-B01D-DEF53FC1CCCB"), "Alimentação", "Comidas em geral")
            );

            var vendedorId = Guid.NewGuid();

            builder.Entity<Vendedor>().HasData(
                new Vendedor(vendedorId, "Cristian Kruger", "cristian.kruger@live.com", "AQAAAAIAAYagAAAAECb71CLXHseUZWuW+gUqfchl7eBtMS8a/ZFRWNF/nK41w/ulA/XqEo0HQnMlNJ+9gQ==")//password @Aa12345
            );

            builder.Entity<Produto>().HasData(
                new Produto(Guid.NewGuid(), 100, 5000, "Computador", "Personal Computer", string.Empty, Guid.Parse("7B87817F-F13C-4A68-87C5-0FC28EDA22CE"), vendedorId),
                new Produto(Guid.NewGuid(), 20, 60, "Mouse", "mouse com fio", string.Empty, Guid.Parse("7B87817F-F13C-4A68-87C5-0FC28EDA22CE"), vendedorId),
                new Produto(Guid.NewGuid(), 15, 100, "Teclado", "teclado mecânico", string.Empty, Guid.Parse("7B87817F-F13C-4A68-87C5-0FC28EDA22CE"), vendedorId),
                new Produto(Guid.NewGuid(), 28, 780, "Monitor", "Monitor curso 27", string.Empty, Guid.Parse("7B87817F-F13C-4A68-87C5-0FC28EDA22CE"), vendedorId)
            );

        }
    }
}
