IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Email] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Bill] (
        [BillId] int NOT NULL IDENTITY,
        [BillDate] datetimeoffset NOT NULL,
        [BillDueDate] datetimeoffset NOT NULL,
        [BillName] nvarchar(max) NULL,
        [BillTypeId] int NOT NULL,
        [GoodsReceivedNoteId] int NOT NULL,
        [VendorDONumber] nvarchar(max) NULL,
        [VendorInvoiceNumber] nvarchar(max) NULL,
        CONSTRAINT [PK_Bill] PRIMARY KEY ([BillId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [BillType] (
        [BillTypeId] int NOT NULL IDENTITY,
        [BillTypeName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_BillType] PRIMARY KEY ([BillTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Branch] (
        [BranchId] int NOT NULL IDENTITY,
        [Address] nvarchar(max) NULL,
        [BranchName] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NULL,
        [ContactPerson] nvarchar(max) NULL,
        [CurrencyId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Branch] PRIMARY KEY ([BranchId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [CashBank] (
        [CashBankId] int NOT NULL IDENTITY,
        [CashBankName] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_CashBank] PRIMARY KEY ([CashBankId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Currency] (
        [CurrencyId] int NOT NULL IDENTITY,
        [CurrencyCode] nvarchar(max) NOT NULL,
        [CurrencyName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Currency] PRIMARY KEY ([CurrencyId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Customer] (
        [CustomerId] int NOT NULL IDENTITY,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [ContactPerson] nvarchar(max) NULL,
        [CustomerName] nvarchar(max) NOT NULL,
        [CustomerTypeId] int NOT NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [CustomerType] (
        [CustomerTypeId] int NOT NULL IDENTITY,
        [CustomerTypeName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_CustomerType] PRIMARY KEY ([CustomerTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [GoodsReceivedNote] (
        [GoodsReceivedNoteId] int NOT NULL IDENTITY,
        [GRNDate] datetimeoffset NOT NULL,
        [GoodsReceivedNoteName] nvarchar(max) NULL,
        [IsFullReceive] bit NOT NULL,
        [PurchaseOrderId] int NOT NULL,
        [VendorDONumber] nvarchar(max) NULL,
        [VendorInvoiceNumber] nvarchar(max) NULL,
        [WarehouseId] int NOT NULL,
        CONSTRAINT [PK_GoodsReceivedNote] PRIMARY KEY ([GoodsReceivedNoteId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Invoice] (
        [InvoiceId] int NOT NULL IDENTITY,
        [InvoiceDate] datetimeoffset NOT NULL,
        [InvoiceDueDate] datetimeoffset NOT NULL,
        [InvoiceName] nvarchar(max) NULL,
        [InvoiceTypeId] int NOT NULL,
        [ShipmentId] int NOT NULL,
        CONSTRAINT [PK_Invoice] PRIMARY KEY ([InvoiceId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [InvoiceType] (
        [InvoiceTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [InvoiceTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_InvoiceType] PRIMARY KEY ([InvoiceTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [NumberSequence] (
        [NumberSequenceId] int NOT NULL IDENTITY,
        [LastNumber] int NOT NULL,
        [Module] nvarchar(max) NOT NULL,
        [NumberSequenceName] nvarchar(max) NOT NULL,
        [Prefix] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_NumberSequence] PRIMARY KEY ([NumberSequenceId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PaymentReceive] (
        [PaymentReceiveId] int NOT NULL IDENTITY,
        [InvoiceId] int NOT NULL,
        [IsFullPayment] bit NOT NULL,
        [PaymentAmount] float NOT NULL,
        [PaymentDate] datetimeoffset NOT NULL,
        [PaymentReceiveName] nvarchar(max) NULL,
        [PaymentTypeId] int NOT NULL,
        CONSTRAINT [PK_PaymentReceive] PRIMARY KEY ([PaymentReceiveId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PaymentType] (
        [PaymentTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [PaymentTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PaymentType] PRIMARY KEY ([PaymentTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PaymentVoucher] (
        [PaymentvoucherId] int NOT NULL IDENTITY,
        [BillId] int NOT NULL,
        [CashBankId] int NOT NULL,
        [IsFullPayment] bit NOT NULL,
        [PaymentAmount] float NOT NULL,
        [PaymentDate] datetimeoffset NOT NULL,
        [PaymentTypeId] int NOT NULL,
        [PaymentVoucherName] nvarchar(max) NULL,
        CONSTRAINT [PK_PaymentVoucher] PRIMARY KEY ([PaymentvoucherId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Product] (
        [ProductId] int NOT NULL IDENTITY,
        [Barcode] nvarchar(max) NULL,
        [BranchId] int NOT NULL,
        [CurrencyId] int NOT NULL,
        [DefaultBuyingPrice] float NOT NULL,
        [DefaultSellingPrice] float NOT NULL,
        [Description] nvarchar(max) NULL,
        [ProductCode] nvarchar(max) NULL,
        [ProductImageUrl] nvarchar(max) NULL,
        [ProductName] nvarchar(max) NOT NULL,
        [UnitOfMeasureId] int NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [ProductType] (
        [ProductTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [ProductTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductType] PRIMARY KEY ([ProductTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PurchaseOrder] (
        [PurchaseOrderId] int NOT NULL IDENTITY,
        [Amount] float NOT NULL,
        [BranchId] int NOT NULL,
        [CurrencyId] int NOT NULL,
        [DeliveryDate] datetimeoffset NOT NULL,
        [Discount] float NOT NULL,
        [Freight] float NOT NULL,
        [OrderDate] datetimeoffset NOT NULL,
        [PurchaseOrderName] nvarchar(max) NULL,
        [PurchaseTypeId] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [SubTotal] float NOT NULL,
        [Tax] float NOT NULL,
        [Total] float NOT NULL,
        [VendorId] int NOT NULL,
        CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY ([PurchaseOrderId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PurchaseType] (
        [PurchaseTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [PurchaseTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PurchaseType] PRIMARY KEY ([PurchaseTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [SalesOrder] (
        [SalesOrderId] int NOT NULL IDENTITY,
        [Amount] float NOT NULL,
        [BranchId] int NOT NULL,
        [CurrencyId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [CustomerRefNumber] nvarchar(max) NULL,
        [DeliveryDate] datetimeoffset NOT NULL,
        [Discount] float NOT NULL,
        [Freight] float NOT NULL,
        [OrderDate] datetimeoffset NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [SalesOrderName] nvarchar(max) NULL,
        [SalesTypeId] int NOT NULL,
        [SubTotal] float NOT NULL,
        [Tax] float NOT NULL,
        [Total] float NOT NULL,
        CONSTRAINT [PK_SalesOrder] PRIMARY KEY ([SalesOrderId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [SalesType] (
        [SalesTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [SalesTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SalesType] PRIMARY KEY ([SalesTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Shipment] (
        [ShipmentId] int NOT NULL IDENTITY,
        [IsFullShipment] bit NOT NULL,
        [SalesOrderId] int NOT NULL,
        [ShipmentDate] datetimeoffset NOT NULL,
        [ShipmentName] nvarchar(max) NULL,
        [ShipmentTypeId] int NOT NULL,
        [WarehouseId] int NOT NULL,
        CONSTRAINT [PK_Shipment] PRIMARY KEY ([ShipmentId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [ShipmentType] (
        [ShipmentTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [ShipmentTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ShipmentType] PRIMARY KEY ([ShipmentTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [UnitOfMeasure] (
        [UnitOfMeasureId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [UnitOfMeasureName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UnitOfMeasure] PRIMARY KEY ([UnitOfMeasureId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [UserProfile] (
        [UserProfileId] int NOT NULL IDENTITY,
        [ApplicationUserId] nvarchar(max) NULL,
        [ConfirmPassword] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [OldPassword] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        CONSTRAINT [PK_UserProfile] PRIMARY KEY ([UserProfileId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Vendor] (
        [VendorId] int NOT NULL IDENTITY,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [ContactPerson] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [VendorName] nvarchar(max) NOT NULL,
        [VendorTypeId] int NOT NULL,
        [ZipCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Vendor] PRIMARY KEY ([VendorId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [VendorType] (
        [VendorTypeId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [VendorTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_VendorType] PRIMARY KEY ([VendorTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [Warehouse] (
        [WarehouseId] int NOT NULL IDENTITY,
        [BranchId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [WarehouseName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Warehouse] PRIMARY KEY ([WarehouseId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [PurchaseOrderLine] (
        [PurchaseOrderLineId] int NOT NULL IDENTITY,
        [Amount] float NOT NULL,
        [Description] nvarchar(max) NULL,
        [DiscountAmount] float NOT NULL,
        [DiscountPercentage] float NOT NULL,
        [Price] float NOT NULL,
        [ProductId] int NOT NULL,
        [PurchaseOrderId] int NOT NULL,
        [Quantity] float NOT NULL,
        [SubTotal] float NOT NULL,
        [TaxAmount] float NOT NULL,
        [TaxPercentage] float NOT NULL,
        [Total] float NOT NULL,
        CONSTRAINT [PK_PurchaseOrderLine] PRIMARY KEY ([PurchaseOrderLineId]),
        CONSTRAINT [FK_PurchaseOrderLine_PurchaseOrder_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrder] ([PurchaseOrderId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE TABLE [SalesOrderLine] (
        [SalesOrderLineId] int NOT NULL IDENTITY,
        [Amount] float NOT NULL,
        [Description] nvarchar(max) NULL,
        [DiscountAmount] float NOT NULL,
        [DiscountPercentage] float NOT NULL,
        [Price] float NOT NULL,
        [ProductId] int NOT NULL,
        [Quantity] float NOT NULL,
        [SalesOrderId] int NOT NULL,
        [SubTotal] float NOT NULL,
        [TaxAmount] float NOT NULL,
        [TaxPercentage] float NOT NULL,
        [Total] float NOT NULL,
        CONSTRAINT [PK_SalesOrderLine] PRIMARY KEY ([SalesOrderLineId]),
        CONSTRAINT [FK_SalesOrderLine_SalesOrder_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrder] ([SalesOrderId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_PurchaseOrderLine_PurchaseOrderId] ON [PurchaseOrderLine] ([PurchaseOrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    CREATE INDEX [IX_SalesOrderLine_SalesOrderId] ON [SalesOrderLine] ([SalesOrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180809031729_initialdb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180809031729_initialdb', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180810002948_profilepicture')
BEGIN
    ALTER TABLE [UserProfile] ADD [ProfilePicture] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180810002948_profilepicture')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180810002948_profilepicture', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'SalesOrder') AND [c].[name] = N'BranchId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrder] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [SalesOrder] DROP COLUMN [BranchId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'SalesOrder') AND [c].[name] = N'CurrencyId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrder] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [SalesOrder] DROP COLUMN [CurrencyId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'SalesOrder') AND [c].[name] = N'SalesTypeId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrder] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [SalesOrder] DROP COLUMN [SalesTypeId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    ALTER TABLE [GoodsReceivedNote] ADD [GoodsReceivedNoteLineId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    CREATE TABLE [GoodsRecievedNoteLine] (
        [GoodsRecievedNoteLineId] int NOT NULL IDENTITY,
        [BatchID] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [GoodsReceivedNoteId] int NOT NULL,
        [ManufareDate] datetime2 NOT NULL,
        [ProductId] int NOT NULL,
        [Quantity] float NOT NULL,
        CONSTRAINT [PK_GoodsRecievedNoteLine] PRIMARY KEY ([GoodsRecievedNoteLineId]),
        CONSTRAINT [FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId] FOREIGN KEY ([GoodsReceivedNoteId]) REFERENCES [GoodsReceivedNote] ([GoodsReceivedNoteId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    CREATE INDEX [IX_GoodsRecievedNoteLine_GoodsReceivedNoteId] ON [GoodsRecievedNoteLine] ([GoodsReceivedNoteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315092433_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315092433_initial', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315100317_initial2')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'GoodsReceivedNote') AND [c].[name] = N'GoodsReceivedNoteLineId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [GoodsReceivedNote] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [GoodsReceivedNote] DROP COLUMN [GoodsReceivedNoteLineId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315100317_initial2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315100317_initial2', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316111326_Stock')
BEGIN
    CREATE TABLE [Stock] (
        [StockId] int NOT NULL IDENTITY,
        [InStock] int NOT NULL,
        [ProductId] int NOT NULL,
        [TotalRecieved] int NOT NULL,
        [TotalSales] int NOT NULL,
        CONSTRAINT [PK_Stock] PRIMARY KEY ([StockId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316111326_Stock')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220316111326_Stock', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316125948_Stock2')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Stock') AND [c].[name] = N'TotalSales');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Stock] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Stock] ALTER COLUMN [TotalSales] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316125948_Stock2')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Stock') AND [c].[name] = N'TotalRecieved');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Stock] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Stock] ALTER COLUMN [TotalRecieved] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316125948_Stock2')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Stock') AND [c].[name] = N'InStock');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Stock] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Stock] ALTER COLUMN [InStock] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220316125948_Stock2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220316125948_Stock2', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    ALTER TABLE [Stock] ADD [Deficit] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE TABLE [ClinicalTrialsDonation] (
        [ClinicalTrialsDonationId] int NOT NULL IDENTITY,
        [CTDDate] datetimeoffset NOT NULL,
        [ClinicalTrialsDonationName] nvarchar(max) NULL,
        [VendorDONumber] nvarchar(max) NULL,
        [VendorInvoiceNumber] nvarchar(max) NULL,
        [WarehouseId] int NOT NULL,
        CONSTRAINT [PK_ClinicalTrialsDonation] PRIMARY KEY ([ClinicalTrialsDonationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE TABLE [ClinicalTrialsProducts] (
        [ClinicalTrialsProductId] int NOT NULL IDENTITY,
        [Barcode] nvarchar(max) NULL,
        [BranchId] int NOT NULL,
        [CurrencyId] int NOT NULL,
        [DefaultBuyingPrice] float NOT NULL,
        [DefaultSellingPrice] float NOT NULL,
        [Description] nvarchar(max) NULL,
        [ProductCode] nvarchar(max) NULL,
        [ProductImageUrl] nvarchar(max) NULL,
        [ProductName] nvarchar(max) NOT NULL,
        [UnitOfMeasureId] int NOT NULL,
        CONSTRAINT [PK_ClinicalTrialsProducts] PRIMARY KEY ([ClinicalTrialsProductId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE TABLE [ClinicalTrialsDonationLine] (
        [ClinicalTrialsDonationLineId] int NOT NULL IDENTITY,
        [BatchID] nvarchar(max) NULL,
        [ClinicalTrialsDonationId] int NOT NULL,
        [ClinicalTrialsProductsId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [ManufareDate] datetime2 NOT NULL,
        [Quantity] float NOT NULL,
        CONSTRAINT [PK_ClinicalTrialsDonationLine] PRIMARY KEY ([ClinicalTrialsDonationLineId]),
        CONSTRAINT [FK_ClinicalTrialsDonationLine_ClinicalTrialsDonation_ClinicalTrialsDonationId] FOREIGN KEY ([ClinicalTrialsDonationId]) REFERENCES [ClinicalTrialsDonation] ([ClinicalTrialsDonationId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ClinicalTrialsDonationLine_ClinicalTrialsProducts_ClinicalTrialsProductsId] FOREIGN KEY ([ClinicalTrialsProductsId]) REFERENCES [ClinicalTrialsProducts] ([ClinicalTrialsProductId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE TABLE [ClinicalTrialsStock] (
        [ClinicalTrialsStockId] int NOT NULL IDENTITY,
        [ClinicalTrialsProductsId] int NOT NULL,
        [Deficit] float NOT NULL,
        [InStock] float NOT NULL,
        [TotalRecieved] float NOT NULL,
        [TotalSales] float NOT NULL,
        CONSTRAINT [PK_ClinicalTrialsStock] PRIMARY KEY ([ClinicalTrialsStockId]),
        CONSTRAINT [FK_ClinicalTrialsStock_ClinicalTrialsProducts_ClinicalTrialsProductsId] FOREIGN KEY ([ClinicalTrialsProductsId]) REFERENCES [ClinicalTrialsProducts] ([ClinicalTrialsProductId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE UNIQUE INDEX [IX_Stock_ProductId] ON [Stock] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE INDEX [IX_GoodsRecievedNoteLine_ProductId] ON [GoodsRecievedNoteLine] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE INDEX [IX_ClinicalTrialsDonationLine_ClinicalTrialsDonationId] ON [ClinicalTrialsDonationLine] ([ClinicalTrialsDonationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE INDEX [IX_ClinicalTrialsDonationLine_ClinicalTrialsProductsId] ON [ClinicalTrialsDonationLine] ([ClinicalTrialsProductsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    CREATE UNIQUE INDEX [IX_ClinicalTrialsStock_ClinicalTrialsProductsId] ON [ClinicalTrialsStock] ([ClinicalTrialsProductsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    ALTER TABLE [GoodsRecievedNoteLine] ADD CONSTRAINT [FK_GoodsRecievedNoteLine_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    ALTER TABLE [Stock] ADD CONSTRAINT [FK_Stock_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329060748_clinical trial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220329060748_clinical trial', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329084613_clinicalTrials2')
BEGIN
    CREATE TABLE [ClinicalTrialsSales] (
        [ClinicalTrialsSalesId] int NOT NULL IDENTITY,
        [ClinicalTrialsSalesName] nvarchar(max) NULL,
        [CustomerId] int NOT NULL,
        [DeliveryDate] datetimeoffset NOT NULL,
        [OrderDate] datetimeoffset NOT NULL,
        [PatientRefNumber] nvarchar(max) NULL,
        CONSTRAINT [PK_ClinicalTrialsSales] PRIMARY KEY ([ClinicalTrialsSalesId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329084613_clinicalTrials2')
BEGIN
    CREATE TABLE [ClinicalTrialsSalesLine] (
        [ClinicalTrialsSalesLineId] int NOT NULL IDENTITY,
        [Amount] float NOT NULL,
        [ClinicalTrialsProductsId] int NOT NULL,
        [ClinicalTrialsSalesId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [DiscountAmount] float NOT NULL,
        [DiscountPercentage] float NOT NULL,
        [Price] float NOT NULL,
        [Quantity] float NOT NULL,
        [SubTotal] float NOT NULL,
        [TaxAmount] float NOT NULL,
        [TaxPercentage] float NOT NULL,
        [Total] float NOT NULL,
        CONSTRAINT [PK_ClinicalTrialsSalesLine] PRIMARY KEY ([ClinicalTrialsSalesLineId]),
        CONSTRAINT [FK_ClinicalTrialsSalesLine_ClinicalTrialsSales_ClinicalTrialsSalesId] FOREIGN KEY ([ClinicalTrialsSalesId]) REFERENCES [ClinicalTrialsSales] ([ClinicalTrialsSalesId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329084613_clinicalTrials2')
BEGIN
    CREATE INDEX [IX_ClinicalTrialsSalesLine_ClinicalTrialsSalesId] ON [ClinicalTrialsSalesLine] ([ClinicalTrialsSalesId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220329084613_clinicalTrials2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220329084613_clinicalTrials2', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    DROP TABLE [Stock];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [SalesOrderLine] ADD [GoodsRecievedNoteLineId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [Product] ADD [Deficit] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [Product] ADD [ExpiredStock] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [Product] ADD [InStock] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [Product] ADD [TotalRecieved] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [Product] ADD [TotalSales] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [GoodsRecievedNoteLine] ADD [Expired] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [GoodsRecievedNoteLine] ADD [InStock] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    ALTER TABLE [GoodsRecievedNoteLine] ADD [Sold] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331053625_newStock')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220331053625_newStock', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    DROP TABLE [ClinicalTrialsStock];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsSalesLine] ADD [ClinicalTrialsDonationLineId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [Deficit] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [Expired] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [InStock] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [TotalRecieved] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [TotalSales] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsDonationLine] ADD [Expired] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsDonationLine] ADD [InStock] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    ALTER TABLE [ClinicalTrialsDonationLine] ADD [Sold] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331130551_ClinicalTrialsNewStock')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220331130551_ClinicalTrialsNewStock', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405075615_paymentpath')
BEGIN
    ALTER TABLE [PaymentReceive] ADD [UploadPath] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405075615_paymentpath')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405075615_paymentpath', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405114435_uploads')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'PaymentReceive') AND [c].[name] = N'UploadPath');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PaymentReceive] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [PaymentReceive] DROP COLUMN [UploadPath];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405114435_uploads')
BEGIN
    CREATE TABLE [Uploads] (
        [Id] int NOT NULL IDENTITY,
        [Content] varbinary(max) NULL,
        [PaymentReceiveId] int NOT NULL,
        CONSTRAINT [PK_Uploads] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405114435_uploads')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405114435_uploads', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405134845_upload')
BEGIN
    ALTER TABLE [Uploads] ADD [filename] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405134845_upload')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405134845_upload', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220406072847_uploadscontentType')
BEGIN
    ALTER TABLE [Uploads] ADD [contentType] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220406072847_uploadscontentType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220406072847_uploadscontentType', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407062319_expiredBool')
BEGIN
    ALTER TABLE [GoodsRecievedNoteLine] ADD [Dispose] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407062319_expiredBool')
BEGIN
    ALTER TABLE [ClinicalTrialsDonationLine] ADD [Dispose] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407062319_expiredBool')
BEGIN
    CREATE INDEX [IX_PaymentReceive_PaymentTypeId] ON [PaymentReceive] ([PaymentTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407062319_expiredBool')
BEGIN
    ALTER TABLE [PaymentReceive] ADD CONSTRAINT [FK_PaymentReceive_PaymentType_PaymentTypeId] FOREIGN KEY ([PaymentTypeId]) REFERENCES [PaymentType] ([PaymentTypeId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407062319_expiredBool')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220407062319_expiredBool', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407070229_donationschange')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'ClinicalTrialsDonation') AND [c].[name] = N'VendorInvoiceNumber');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ClinicalTrialsDonation] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [ClinicalTrialsDonation] DROP COLUMN [VendorInvoiceNumber];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407070229_donationschange')
BEGIN
    ALTER TABLE [ClinicalTrialsDonation] ADD [VendorId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407070229_donationschange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220407070229_donationschange', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [SalesOrder] ADD [BranchId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [SalesOrder] ADD [CurrencyId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [SalesOrder] ADD [SalesTypeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [Product] ADD [ProductTypeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [ClinicalTrialsProducts] ADD [ProductTypeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    CREATE INDEX [IX_ClinicalTrialsDonation_VendorId] ON [ClinicalTrialsDonation] ([VendorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    ALTER TABLE [ClinicalTrialsDonation] ADD CONSTRAINT [FK_ClinicalTrialsDonation_Vendor_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendor] ([VendorId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220408064246_generictype')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220408064246_generictype', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220412052646_shipmentRemove')
BEGIN
    EXEC sp_rename N'Invoice.ShipmentId', N'SalesOrderId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220412052646_shipmentRemove')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220412052646_shipmentRemove', N'2.0.3-rtm-10026');
END;

GO

