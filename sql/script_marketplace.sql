IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] varchar(100) NOT NULL,
        [Name] varchar(100) NULL,
        [NormalizedName] varchar(100) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] varchar(100) NOT NULL,
        [UserName] varchar(100) NULL,
        [NormalizedUserName] varchar(100) NULL,
        [Email] varchar(100) NULL,
        [NormalizedEmail] varchar(100) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] varchar(100) NULL,
        [SecurityStamp] varchar(100) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        [PhoneNumber] varchar(100) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [CATEGORIAS] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(500) NOT NULL,
        CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [VENDEDORES] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Email] VARCHAR(50) NOT NULL,
        [Senha] VARCHAR(256) NOT NULL,
        CONSTRAINT [PK_VENDEDORES] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] varchar(100) NOT NULL,
        [ProviderKey] varchar(100) NOT NULL,
        [ProviderDisplayName] varchar(100) NULL,
        [UserId] varchar(100) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] varchar(100) NOT NULL,
        [RoleId] varchar(100) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] varchar(100) NOT NULL,
        [LoginProvider] varchar(100) NOT NULL,
        [Name] varchar(100) NOT NULL,
        [Value] varchar(100) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE TABLE [PRODUTOS] (
        [Id] uniqueidentifier NOT NULL,
        [Estoque] INT NOT NULL,
        [Preco] DECIMAL(18,2) NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(500) NOT NULL,
        [Imagem] VARCHAR(500) NULL,
        [CategoriaId] uniqueidentifier NOT NULL,
        [VendedorId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PRODUTOS] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PRODUTO_CATEGORIAID] FOREIGN KEY ([CategoriaId]) REFERENCES [CATEGORIAS] ([Id]),
        CONSTRAINT [FK_PRODUTO_VENDEDORID] FOREIGN KEY ([VendedorId]) REFERENCES [VENDEDORES] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao', N'Nome') AND [object_id] = OBJECT_ID(N'[CATEGORIAS]'))
        SET IDENTITY_INSERT [CATEGORIAS] ON;
    EXEC(N'INSERT INTO [CATEGORIAS] ([Id], [Descricao], [Nome])
    VALUES (''2ce8ce71-e766-41ee-839a-f0824f7fd3b8'', ''Categoria destinada a vestuário'', ''Vestuário''),
    (''63cb29c3-db97-4744-b01d-def53fc1cccb'', ''Comidas em geral'', ''Alimentação''),
    (''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''Eletrônicos e eletrodomésticos'', ''Eletrônicos'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao', N'Nome') AND [object_id] = OBJECT_ID(N'[CATEGORIAS]'))
        SET IDENTITY_INSERT [CATEGORIAS] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_NOME_CATEGORIA] ON [CATEGORIAS] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTO_NOME] ON [PRODUTOS] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_CategoriaId] ON [PRODUTOS] ([CategoriaId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_VendedorId] ON [PRODUTOS] ([VendedorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID] ON [PRODUTOS] ([Nome], [CategoriaId], [VendedorId]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_VENDEDOR_NOME] ON [VENDEDORES] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_VENDEDOR_EMAIL] ON [VENDEDORES] ([Email]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406165554_V1_first_migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406165554_V1_first_migration', N'8.0.14');
END;
GO

COMMIT;
GO

