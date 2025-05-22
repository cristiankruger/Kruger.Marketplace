using Microsoft.EntityFrameworkCore.Migrations;

namespace Kruger.Marketplace.Core.Data.Migrations
{
    public partial class V1_first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIAS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VENDEDORES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDEDORES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "varchar(100)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(100)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(100)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(100)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estoque = table.Column<int>(type: "INT", nullable: false),
                    Preco = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    Imagem = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUTO_CATEGORIAID",
                        column: x => x.CategoriaId,
                        principalTable: "CATEGORIAS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PRODUTO_VENDEDORID",
                        column: x => x.VendedorId,
                        principalTable: "VENDEDORES",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "2c5e174e-3b0e-446f-86af-483d56fd7210", "Administrator", "ADMINISTRADOR" },
                    { "2", "16aacd76-5c6d-418a-884c-116871ca2fe0", "Vendedor", "VENDEDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f96e5735-7f8a-49a7-8fe1-64304e70257d", 0, "ea685453-06bb-4ef1-b052-96fd402eada9", "teste@teste.com", true, true, null, "TESTE@TESTE.COM", "TESTE@TESTE.COM", "AQAAAAIAAYagAAAAEER10GhU13Zx3GH3cJWNUvq0j56gqEt9u7exj+Ead+YB4RlXHv2qy6x40W1zxUshhQ==", null, false, "6e5ea4a2-bdf2-42b4-a922-64f47e5cb131", false, "teste@teste.com" });

            migrationBuilder.InsertData(
                table: "CATEGORIAS",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Categoria destinada a vestuário", "Vestuário" },
                    { new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Comidas em geral", "Alimentação" },
                    { new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos e eletrodomésticos", "Eletrônicos" }
                });

            migrationBuilder.InsertData(
                table: "VENDEDORES",
                columns: new[] { "Id", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"), "mail.teste@teste.com", "mail.teste@teste.com", "AQAAAAIAAYagAAAAEER10GhU13Zx3GH3cJWNUvq0j56gqEt9u7exj+Ead+YB4RlXHv2qy6x40W1zxUshhQ==" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "f96e5735-7f8a-49a7-8fe1-64304e70257d" });

            migrationBuilder.InsertData(
                table: "PRODUTOS",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Estoque", "Imagem", "Nome", "Preco", "VendedorId" },
                values: new object[,]
                {
                    { new Guid("203e4ce7-d0b6-4a78-bab6-59740d70940d"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Personal Computer", 100, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Computador", 5000m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("7095ea8f-86a2-4f4a-94e7-e99affee9f8d"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "mouse com fio", 20, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Mouse", 60m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("870d9b8f-fd45-456c-b830-487319d8363e"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Monitor curso 27", 28, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Monitor", 780m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("bf50aa0a-673f-40d6-86ac-17a3bd1e65b5"), new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "teclado mecânico", 15, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Teclado", 100m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NOME_CATEGORIA",
                table: "CATEGORIAS",
                column: "Nome",
                unique: true)
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_NOME",
                table: "PRODUTOS",
                column: "Nome")
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_CategoriaId",
                table: "PRODUTOS",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_VendedorId",
                table: "PRODUTOS",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID",
                table: "PRODUTOS",
                columns: new[] { "Nome", "CategoriaId", "VendedorId" },
                unique: true)
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "IX_VENDEDOR_NOME",
                table: "VENDEDORES",
                column: "Nome")
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "UQ_VENDEDOR_EMAIL",
                table: "VENDEDORES",
                column: "Email",
                unique: true)
                .Annotation("SqlServer:FillFactor", 80);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PRODUTOS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CATEGORIAS");

            migrationBuilder.DropTable(
                name: "VENDEDORES");
        }
    }
}
