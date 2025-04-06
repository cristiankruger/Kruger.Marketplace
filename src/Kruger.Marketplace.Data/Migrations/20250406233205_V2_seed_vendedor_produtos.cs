using Microsoft.EntityFrameworkCore.Migrations;

namespace Kruger.Marketplace.Data.Migrations
{
    public partial class V2_seed_vendedor_produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "VENDEDORES",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendedorId",
                table: "PRODUTOS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaId",
                table: "PRODUTOS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PRODUTOS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CATEGORIAS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "VENDEDORES",
                columns: new[] { "Id", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"), "cristian.kruger@live.com", "Cristian Kruger", "AQAAAAIAAYagAAAAECb71CLXHseUZWuW+gUqfchl7eBtMS8a/ZFRWNF/nK41w/ulA/XqEo0HQnMlNJ+9gQ==" });

            migrationBuilder.InsertData(
                table: "PRODUTOS",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Estoque", "Imagem", "Nome", "Preco", "VendedorId" },
                values: new object[,]
                {
                    { new Guid("177cb2ed-74d1-466d-8be2-829681b2dc7a"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "teclado mecânico", 15, "", "Teclado", 100m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("3288f0bf-8925-4972-b030-dbaec2ca6e2f"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "mouse com fio", 20, "", "Mouse", 60m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("4c6bd085-16e1-41f7-9a55-760f6c4f55ec"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Monitor curso 27", 28, "", "Monitor", 780m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("6c06634e-1872-4b5a-ac07-ef52177835d4"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Personal Computer", 100, "", "Computador", 5000m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") }
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("177cb2ed-74d1-466d-8be2-829681b2dc7a"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("3288f0bf-8925-4972-b030-dbaec2ca6e2f"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("4c6bd085-16e1-41f7-9a55-760f6c4f55ec"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("6c06634e-1872-4b5a-ac07-ef52177835d4"));

            migrationBuilder.DeleteData(
                table: "VENDEDORES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "VENDEDORES",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendedorId",
                table: "PRODUTOS",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaId",
                table: "PRODUTOS",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PRODUTOS",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CATEGORIAS",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }
    }
}
