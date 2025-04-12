﻿using Kruger.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
                .HasAnnotation("ProductVersion", "8.0.15");

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
                            Id = new Guid("e41b3d2b-d59a-4734-9e55-2ef601b9070c"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "Personal Computer",
                            Estoque = 100,
                            Imagem = "00000000-0000-0000-0000-000000000000_imagem.jpg",
                            Nome = "Computador",
                            Preco = 5000m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("65a744e2-95d6-42d2-b319-8dd959a96258"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "mouse com fio",
                            Estoque = 20,
                            Imagem = "00000000-0000-0000-0000-000000000000_imagem.jpg",
                            Nome = "Mouse",
                            Preco = 60m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("bcf572f7-b754-42d2-af49-1eb0c268b686"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "teclado mecânico",
                            Estoque = 15,
                            Imagem = "00000000-0000-0000-0000-000000000000_imagem.jpg",
                            Nome = "Teclado",
                            Preco = 100m,
                            VendedorId = new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d")
                        },
                        new
                        {
                            Id = new Guid("48dea8b7-d5cd-42ca-82cd-e40177906d66"),
                            CategoriaId = new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"),
                            Descricao = "Monitor curso 27",
                            Estoque = 28,
                            Imagem = "00000000-0000-0000-0000-000000000000_imagem.jpg",
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
                        .HasColumnType("VARCHAR(100)");

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
                            Email = "mail.teste@teste.com",
                            Nome = "mail.teste@teste.com",
                            Senha = "AQAAAAIAAYagAAAAELir8o/W80jaR21p99MhJnKq89mT/KtfLmIDoexqgiNGvDSHx3r0XrQ/sKC8JXHGcA=="
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

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRADOR"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "16aacd76-5c6d-418a-884c-116871ca2fe0",
                            Name = "Vendedor",
                            NormalizedName = "VENDEDOR"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = "f96e5735-7f8a-49a7-8fe1-64304e70257d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "96fa875e-cac1-4b4f-8357-afd599a2cf92",
                            Email = "mail.teste@teste.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "MAIL.TESTE@TESTE.COM",
                            NormalizedUserName = "MAIL.TESTE@TESTE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELir8o/W80jaR21p99MhJnKq89mT/KtfLmIDoexqgiNGvDSHx3r0XrQ/sKC8JXHGcA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3a6b6f47-8dea-43ae-be65-26305d39d8e7",
                            TwoFactorEnabled = false,
                            UserName = "mail.teste@teste.com"
                        });
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
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProviderKey")
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

                    b.HasData(
                        new
                        {
                            UserId = "f96e5735-7f8a-49a7-8fe1-64304e70257d",
                            RoleId = "2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
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
