﻿// <auto-generated />
using coderush.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace coderush.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("coderush.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("coderush.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("BillDate");

                    b.Property<DateTimeOffset>("BillDueDate");

                    b.Property<string>("BillName");

                    b.Property<int>("BillTypeId");

                    b.Property<int>("GoodsReceivedNoteId");

                    b.Property<string>("VendorDONumber");

                    b.Property<string>("VendorInvoiceNumber");

                    b.HasKey("BillId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("coderush.Models.BillType", b =>
                {
                    b.Property<int>("BillTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillTypeName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.HasKey("BillTypeId");

                    b.ToTable("BillType");
                });

            modelBuilder.Entity("coderush.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BranchName")
                        .IsRequired();

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("BranchId");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("coderush.Models.CashBank", b =>
                {
                    b.Property<int>("CashBankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CashBankName");

                    b.Property<string>("Description");

                    b.HasKey("CashBankId");

                    b.ToTable("CashBank");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsDonation", b =>
                {
                    b.Property<int>("ClinicalTrialsDonationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CTDDate");

                    b.Property<string>("ClinicalTrialsDonationName");

                    b.Property<string>("VendorDONumber");

                    b.Property<string>("VendorInvoiceNumber");

                    b.Property<int>("WarehouseId");

                    b.HasKey("ClinicalTrialsDonationId");

                    b.ToTable("ClinicalTrialsDonation");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsDonationLine", b =>
                {
                    b.Property<int>("ClinicalTrialsDonationLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchID");

                    b.Property<int>("ClinicalTrialsDonationId");

                    b.Property<int>("ClinicalTrialsProductsId");

                    b.Property<string>("Description");

                    b.Property<double>("Expired");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<double>("InStock");

                    b.Property<DateTime>("ManufareDate");

                    b.Property<double>("Quantity");

                    b.Property<double>("Sold");

                    b.HasKey("ClinicalTrialsDonationLineId");

                    b.HasIndex("ClinicalTrialsDonationId");

                    b.HasIndex("ClinicalTrialsProductsId");

                    b.ToTable("ClinicalTrialsDonationLine");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsProduct", b =>
                {
                    b.Property<int>("ClinicalTrialsProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BranchId");

                    b.Property<int>("CurrencyId");

                    b.Property<double>("DefaultBuyingPrice");

                    b.Property<double>("DefaultSellingPrice");

                    b.Property<double>("Deficit");

                    b.Property<string>("Description");

                    b.Property<double>("Expired");

                    b.Property<double>("InStock");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductImageUrl");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<double>("TotalRecieved");

                    b.Property<double>("TotalSales");

                    b.Property<int>("UnitOfMeasureId");

                    b.HasKey("ClinicalTrialsProductId");

                    b.ToTable("ClinicalTrialsProducts");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsSales", b =>
                {
                    b.Property<int>("ClinicalTrialsSalesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClinicalTrialsSalesName");

                    b.Property<int>("CustomerId");

                    b.Property<DateTimeOffset>("DeliveryDate");

                    b.Property<DateTimeOffset>("OrderDate");

                    b.Property<string>("PatientRefNumber");

                    b.HasKey("ClinicalTrialsSalesId");

                    b.ToTable("ClinicalTrialsSales");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsSalesLine", b =>
                {
                    b.Property<int>("ClinicalTrialsSalesLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("ClinicalTrialsDonationLineId");

                    b.Property<int>("ClinicalTrialsProductsId");

                    b.Property<int>("ClinicalTrialsSalesId");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<double>("Price");

                    b.Property<double>("Quantity");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("ClinicalTrialsSalesLineId");

                    b.HasIndex("ClinicalTrialsSalesId");

                    b.ToTable("ClinicalTrialsSalesLine");
                });

            modelBuilder.Entity("coderush.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyCode")
                        .IsRequired();

                    b.Property<string>("CurrencyName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("coderush.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<int>("CustomerTypeId");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("coderush.Models.CustomerType", b =>
                {
                    b.Property<int>("CustomerTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerTypeName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.HasKey("CustomerTypeId");

                    b.ToTable("CustomerType");
                });

            modelBuilder.Entity("coderush.Models.GoodsReceivedNote", b =>
                {
                    b.Property<int>("GoodsReceivedNoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("GRNDate");

                    b.Property<string>("GoodsReceivedNoteName");

                    b.Property<bool>("IsFullReceive");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<string>("VendorDONumber");

                    b.Property<string>("VendorInvoiceNumber");

                    b.Property<int>("WarehouseId");

                    b.HasKey("GoodsReceivedNoteId");

                    b.ToTable("GoodsReceivedNote");
                });

            modelBuilder.Entity("coderush.Models.GoodsRecievedNoteLine", b =>
                {
                    b.Property<int>("GoodsRecievedNoteLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchID");

                    b.Property<string>("Description");

                    b.Property<double>("Expired");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<int>("GoodsReceivedNoteId");

                    b.Property<double>("InStock");

                    b.Property<DateTime>("ManufareDate");

                    b.Property<int>("ProductId");

                    b.Property<double>("Quantity");

                    b.Property<double>("Sold");

                    b.HasKey("GoodsRecievedNoteLineId");

                    b.HasIndex("GoodsReceivedNoteId");

                    b.HasIndex("ProductId");

                    b.ToTable("GoodsRecievedNoteLine");
                });

            modelBuilder.Entity("coderush.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("InvoiceDate");

                    b.Property<DateTimeOffset>("InvoiceDueDate");

                    b.Property<string>("InvoiceName");

                    b.Property<int>("InvoiceTypeId");

                    b.Property<int>("ShipmentId");

                    b.HasKey("InvoiceId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("coderush.Models.InvoiceType", b =>
                {
                    b.Property<int>("InvoiceTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("InvoiceTypeName")
                        .IsRequired();

                    b.HasKey("InvoiceTypeId");

                    b.ToTable("InvoiceType");
                });

            modelBuilder.Entity("coderush.Models.NumberSequence", b =>
                {
                    b.Property<int>("NumberSequenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LastNumber");

                    b.Property<string>("Module")
                        .IsRequired();

                    b.Property<string>("NumberSequenceName")
                        .IsRequired();

                    b.Property<string>("Prefix")
                        .IsRequired();

                    b.HasKey("NumberSequenceId");

                    b.ToTable("NumberSequence");
                });

            modelBuilder.Entity("coderush.Models.PaymentReceive", b =>
                {
                    b.Property<int>("PaymentReceiveId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvoiceId");

                    b.Property<bool>("IsFullPayment");

                    b.Property<double>("PaymentAmount");

                    b.Property<DateTimeOffset>("PaymentDate");

                    b.Property<string>("PaymentReceiveName");

                    b.Property<int>("PaymentTypeId");

                    b.HasKey("PaymentReceiveId");

                    b.ToTable("PaymentReceive");
                });

            modelBuilder.Entity("coderush.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("PaymentTypeName")
                        .IsRequired();

                    b.HasKey("PaymentTypeId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("coderush.Models.PaymentVoucher", b =>
                {
                    b.Property<int>("PaymentvoucherId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BillId");

                    b.Property<int>("CashBankId");

                    b.Property<bool>("IsFullPayment");

                    b.Property<double>("PaymentAmount");

                    b.Property<DateTimeOffset>("PaymentDate");

                    b.Property<int>("PaymentTypeId");

                    b.Property<string>("PaymentVoucherName");

                    b.HasKey("PaymentvoucherId");

                    b.ToTable("PaymentVoucher");
                });

            modelBuilder.Entity("coderush.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<int>("BranchId");

                    b.Property<int>("CurrencyId");

                    b.Property<double>("DefaultBuyingPrice");

                    b.Property<double>("DefaultSellingPrice");

                    b.Property<double>("Deficit");

                    b.Property<string>("Description");

                    b.Property<double>("ExpiredStock");

                    b.Property<double>("InStock");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductImageUrl");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<double>("TotalRecieved");

                    b.Property<double>("TotalSales");

                    b.Property<int>("UnitOfMeasureId");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("coderush.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ProductTypeName")
                        .IsRequired();

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("coderush.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("BranchId");

                    b.Property<int>("CurrencyId");

                    b.Property<DateTimeOffset>("DeliveryDate");

                    b.Property<double>("Discount");

                    b.Property<double>("Freight");

                    b.Property<DateTimeOffset>("OrderDate");

                    b.Property<string>("PurchaseOrderName");

                    b.Property<int>("PurchaseTypeId");

                    b.Property<string>("Remarks");

                    b.Property<double>("SubTotal");

                    b.Property<double>("Tax");

                    b.Property<double>("Total");

                    b.Property<int>("VendorId");

                    b.HasKey("PurchaseOrderId");

                    b.ToTable("PurchaseOrder");
                });

            modelBuilder.Entity("coderush.Models.PurchaseOrderLine", b =>
                {
                    b.Property<int>("PurchaseOrderLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<double>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<double>("Quantity");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("PurchaseOrderLineId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderLine");
                });

            modelBuilder.Entity("coderush.Models.PurchaseType", b =>
                {
                    b.Property<int>("PurchaseTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("PurchaseTypeName")
                        .IsRequired();

                    b.HasKey("PurchaseTypeId");

                    b.ToTable("PurchaseType");
                });

            modelBuilder.Entity("coderush.Models.SalesOrder", b =>
                {
                    b.Property<int>("SalesOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerRefNumber");

                    b.Property<DateTimeOffset>("DeliveryDate");

                    b.Property<double>("Discount");

                    b.Property<double>("Freight");

                    b.Property<DateTimeOffset>("OrderDate");

                    b.Property<string>("Remarks");

                    b.Property<string>("SalesOrderName");

                    b.Property<double>("SubTotal");

                    b.Property<double>("Tax");

                    b.Property<double>("Total");

                    b.HasKey("SalesOrderId");

                    b.ToTable("SalesOrder");
                });

            modelBuilder.Entity("coderush.Models.SalesOrderLine", b =>
                {
                    b.Property<int>("SalesOrderLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<int>("GoodsRecievedNoteLineId");

                    b.Property<double>("Price");

                    b.Property<int>("ProductId");

                    b.Property<double>("Quantity");

                    b.Property<int>("SalesOrderId");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("SalesOrderLineId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("SalesOrderLine");
                });

            modelBuilder.Entity("coderush.Models.SalesType", b =>
                {
                    b.Property<int>("SalesTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("SalesTypeName")
                        .IsRequired();

                    b.HasKey("SalesTypeId");

                    b.ToTable("SalesType");
                });

            modelBuilder.Entity("coderush.Models.Shipment", b =>
                {
                    b.Property<int>("ShipmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFullShipment");

                    b.Property<int>("SalesOrderId");

                    b.Property<DateTimeOffset>("ShipmentDate");

                    b.Property<string>("ShipmentName");

                    b.Property<int>("ShipmentTypeId");

                    b.Property<int>("WarehouseId");

                    b.HasKey("ShipmentId");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("coderush.Models.ShipmentType", b =>
                {
                    b.Property<int>("ShipmentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ShipmentTypeName")
                        .IsRequired();

                    b.HasKey("ShipmentTypeId");

                    b.ToTable("ShipmentType");
                });

            modelBuilder.Entity("coderush.Models.UnitOfMeasure", b =>
                {
                    b.Property<int>("UnitOfMeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("UnitOfMeasureName")
                        .IsRequired();

                    b.HasKey("UnitOfMeasureId");

                    b.ToTable("UnitOfMeasure");
                });

            modelBuilder.Entity("coderush.Models.Upload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<int>("PaymentReceiveId");

                    b.Property<string>("contentType");

                    b.Property<string>("filename");

                    b.HasKey("Id");

                    b.ToTable("Uploads");
                });

            modelBuilder.Entity("coderush.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("OldPassword");

                    b.Property<string>("Password");

                    b.Property<string>("ProfilePicture");

                    b.HasKey("UserProfileId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("coderush.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("VendorName")
                        .IsRequired();

                    b.Property<int>("VendorTypeId");

                    b.Property<string>("ZipCode");

                    b.HasKey("VendorId");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("coderush.Models.VendorType", b =>
                {
                    b.Property<int>("VendorTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("VendorTypeName")
                        .IsRequired();

                    b.HasKey("VendorTypeId");

                    b.ToTable("VendorType");
                });

            modelBuilder.Entity("coderush.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BranchId");

                    b.Property<string>("Description");

                    b.Property<string>("WarehouseName")
                        .IsRequired();

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsDonationLine", b =>
                {
                    b.HasOne("coderush.Models.ClinicalTrialsDonation", "clinicalTrialsDonation")
                        .WithMany("clinicalTrialsDonationLine")
                        .HasForeignKey("ClinicalTrialsDonationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coderush.Models.ClinicalTrialsProduct", "clinicalTrialsProducts")
                        .WithMany()
                        .HasForeignKey("ClinicalTrialsProductsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coderush.Models.ClinicalTrialsSalesLine", b =>
                {
                    b.HasOne("coderush.Models.ClinicalTrialsSales", "clinicalTrialsSales")
                        .WithMany("clinicalTrialsSalesLine")
                        .HasForeignKey("ClinicalTrialsSalesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coderush.Models.GoodsRecievedNoteLine", b =>
                {
                    b.HasOne("coderush.Models.GoodsReceivedNote", "GoodsReceivedNote")
                        .WithMany("goodsRecievedNoteLines")
                        .HasForeignKey("GoodsReceivedNoteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coderush.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coderush.Models.PurchaseOrderLine", b =>
                {
                    b.HasOne("coderush.Models.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderLines")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coderush.Models.SalesOrderLine", b =>
                {
                    b.HasOne("coderush.Models.SalesOrder", "SalesOrder")
                        .WithMany("SalesOrderLines")
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("coderush.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("coderush.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coderush.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("coderush.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
