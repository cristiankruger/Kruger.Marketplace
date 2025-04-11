/*
Para gerar o script idempotente do SQL Server, executar o comando no package manager console:

Script-Migration -o ./sql/script_db_tn.sql -context appdbcontext -i
*/

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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE TABLE [VENDEDORES] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Email] VARCHAR(100) NOT NULL,
        [Senha] VARCHAR(256) NOT NULL,
        CONSTRAINT [PK_VENDEDORES] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (''1'', ''2c5e174e-3b0e-446f-86af-483d56fd7210'', ''Administrator'', ''ADMINISTRADOR''),
    (''2'', ''16aacd76-5c6d-418a-884c-116871ca2fe0'', ''Vendedor'', ''VENDEDOR'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (''f96e5735-7f8a-49a7-8fe1-64304e70257d'', 0, ''954c4da6-f297-45ee-8cb0-5fb9d6a838b0'', ''mail.teste@teste.com'', CAST(1 AS bit), CAST(0 AS bit), NULL, NULL, NULL, ''AQAAAAIAAYagAAAAEK4Tk7sgyL54Vhqu9UYuIic3J5nqg2Y5oSsY1RzxMAmCCKZmAepyAnrFYFVXzukUkg=='', NULL, CAST(0 AS bit), ''4b1f117e-7ab2-44d7-b49b-9fff5c9e36ac'', CAST(0 AS bit), ''mail.teste@teste.com'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
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
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[VENDEDORES]'))
        SET IDENTITY_INSERT [VENDEDORES] ON;
    EXEC(N'INSERT INTO [VENDEDORES] ([Id], [Email], [Nome], [Senha])
    VALUES (''f96e5735-7f8a-49a7-8fe1-64304e70257d'', ''mail.teste@teste.com'', ''mail.teste@teste.com'', ''AQAAAAIAAYagAAAAEK4Tk7sgyL54Vhqu9UYuIic3J5nqg2Y5oSsY1RzxMAmCCKZmAepyAnrFYFVXzukUkg=='')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[VENDEDORES]'))
        SET IDENTITY_INSERT [VENDEDORES] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (''2'', ''f96e5735-7f8a-49a7-8fe1-64304e70257d'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoriaId', N'Descricao', N'Estoque', N'Imagem', N'Nome', N'Preco', N'VendedorId') AND [object_id] = OBJECT_ID(N'[PRODUTOS]'))
        SET IDENTITY_INSERT [PRODUTOS] ON;
    EXEC(N'INSERT INTO [PRODUTOS] ([Id], [CategoriaId], [Descricao], [Estoque], [Imagem], [Nome], [Preco], [VendedorId])
    VALUES (''510cc6dc-e759-453e-8916-e5f83ff9d167'', ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''mouse com fio'', 20, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Mouse'', 60.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''b582814a-a873-4523-a18e-b12bfc154ff7'', ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''Monitor curso 27'', 28, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Monitor'', 780.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''cbeeddbf-b127-4ed9-a949-efa1207d9bbb'', ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''Personal Computer'', 100, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Computador'', 5000.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''eab3ebd5-2692-439d-8707-a77bbc675be1'', ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''teclado mecânico'', 15, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Teclado'', 100.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoriaId', N'Descricao', N'Estoque', N'Imagem', N'Nome', N'Preco', N'VendedorId') AND [object_id] = OBJECT_ID(N'[PRODUTOS]'))
        SET IDENTITY_INSERT [PRODUTOS] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_NOME_CATEGORIA] ON [CATEGORIAS] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTO_NOME] ON [PRODUTOS] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_CategoriaId] ON [PRODUTOS] ([CategoriaId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_VendedorId] ON [PRODUTOS] ([VendedorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID] ON [PRODUTOS] ([Nome], [CategoriaId], [VendedorId]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE INDEX [IX_VENDEDOR_NOME] ON [VENDEDORES] ([Nome]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_VENDEDOR_EMAIL] ON [VENDEDORES] ([Email]) WITH (FILLFACTOR = 80);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411145627_V1_first_migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250411145627_V1_first_migration', N'8.0.15');
END;
GO

COMMIT;
GO

