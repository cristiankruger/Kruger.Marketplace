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
                new Categoria(Guid.Parse("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Vestuário", "Categoria destinada a vestuário"),
                new Categoria(Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos", "Eletrônicos e eletrodomésticos"),
                new Categoria(Guid.Parse("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Alimentação", "Comidas em geral")
            );

            var vendedorId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257d");

            builder.Entity<Vendedor>().HasData(
                new Vendedor(vendedorId, "Cristian Kruger", "cristian.kruger@live.com", "AQAAAAIAAYagAAAAECb71CLXHseUZWuW+gUqfchl7eBtMS8a/ZFRWNF/nK41w/ulA/XqEo0HQnMlNJ+9gQ==")//password @Aa12345
            );

            builder.Entity<Produto>().HasData(
                new Produto(Guid.NewGuid(), 100, 5000, "Computador", "Personal Computer", string.Empty, Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 20, 60, "Mouse", "mouse com fio", string.Empty, Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 15, 100, "Teclado", "teclado mecânico", string.Empty, Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 28, 780, "Monitor", "Monitor curso 27", string.Empty, Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId)
            );

        }
    }
}
