#nullable disable

using Kruger.Marketplace.Business.Models.CadastroBasico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kruger.Marketplace.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnType("VARCHAR(500)");

            builder.HasIndex(c => c.Nome)
                   .HasDatabaseName("IX_NOME_CATEGORIA")
                   .HasFillFactor(80)
                   .IsUnique();

            builder.ToTable("CATEGORIAS");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.ProdutoId);
        }
    }
}
