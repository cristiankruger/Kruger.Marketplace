using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kruger.Marketplace.Data.Migrations
{
    public partial class V2_Seed_Categorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CATEGORIAS",
                columns: ["Id", "Descricao", "Nome"],
                values: new object[,]
                {
                    { new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Categoria destinada a vestuário", "Vestuário" },
                    { new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Comidas em geral", "Alimentação" },
                    { new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos e eletrodomésticos", "Eletrônicos" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"));

            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"));

            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"));
        }
    }
}
