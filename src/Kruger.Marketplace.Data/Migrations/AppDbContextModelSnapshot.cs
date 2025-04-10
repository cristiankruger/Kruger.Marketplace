﻿// <auto-generated />
using System;
using Kruger.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kruger.Marketplace.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AI")
                .HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique()
                        .HasDatabaseName("IX_NOME_CATEGORIA")
                        .HasAnnotation("SqlServer:FillFactor", 80);

                    b.ToTable("CATEGORIAS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"),
                            Descricao = "Categoria destinada a vestuário",
                            Nome = "Vestuário"
                        },
                        new
                        {
                            Id = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "Eletrônicos e eletrodomésticos",
                            Nome = "Eletrônicos"
                        },
                        new
                        {
                            Id = new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"),
                            Descricao = "Comidas em geral",
                            Nome = "Alimentação"
                        });
                });

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)");

                    b.Property<int>("Estoque")
                        .HasColumnType("INT");

                    b.Property<string>("Imagem")
                        .HasColumnType("VARCHAR(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<Guid>("VendedorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("Nome")
                        .HasDatabaseName("IX_PRODUTO_NOME")
                        .HasAnnotation("SqlServer:FillFactor", 80);

                    b.HasIndex("VendedorId");

                    b.HasIndex("Nome", "CategoriaId", "VendedorId")
                        .IsUnique()
                        .HasDatabaseName("UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID")
                        .HasAnnotation("SqlServer:FillFactor", 80);

                    b.ToTable("PRODUTOS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("6c06634e-1872-4b5a-ac07-ef52177835d4"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "Personal Computer",
                            Estoque = 100,
                            Imagem = "",
                            Nome = "Computador",
                            Preco = 5000m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("3288f0bf-8925-4972-b030-dbaec2ca6e2f"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "mouse com fio",
                            Estoque = 20,
                            Imagem = "",
                            Nome = "Mouse",
                            Preco = 60m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("177cb2ed-74d1-466d-8be2-829681b2dc7a"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "teclado mecânico",
                            Estoque = 15,
                            Imagem = "",
                            Nome = "Teclado",
                            Preco = 100m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("4c6bd085-16e1-41f7-9a55-760f6c4f55ec"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "Monitor curso 27",
                            Estoque = 28,
                            Imagem = "",
                            Nome = "Monitor",
                            Preco = 780m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        });
                });

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Vendedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UQ_VENDEDOR_EMAIL")
                        .HasAnnotation("SqlServer:FillFactor", 80);

                    b.HasIndex("Nome")
                        .HasDatabaseName("IX_VENDEDOR_NOME")
                        .HasAnnotation("SqlServer:FillFactor", 80);

                    b.ToTable("VENDEDORES", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"),
                            Email = "cristian.kruger@live.com",
                            Nome = "Cristian Kruger",
                            Senha = "AQAAAAIAAYagAAAAECb71CLXHseUZWuW+gUqfchl7eBtMS8a/ZFRWNF/nK41w/ulA/XqEo0HQnMlNJ+9gQ=="
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Produto", b =>
                {
                    b.HasOne("Kruger.Marketplace.Business.Models.CadastroBasico.Categoria", "Categoria")
                        .WithMany("Produto")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_PRODUTO_CATEGORIAID");

                    b.HasOne("Kruger.Marketplace.Business.Models.CadastroBasico.Vendedor", "Vendedor")
                        .WithMany("Produto")
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_PRODUTO_VENDEDORID");

                    b.Navigation("Categoria");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Categoria", b =>
                {
                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Kruger.Marketplace.Business.Models.CadastroBasico.Vendedor", b =>
                {
                    b.Navigation("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
