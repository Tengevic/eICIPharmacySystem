﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using coderush.Models;

namespace coderush.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        internal object Include()
        {
            throw new NotImplementedException();
        }

        public DbSet<coderush.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<coderush.Models.Bill> Bill { get; set; }

        public DbSet<coderush.Models.BillType> BillType { get; set; }

        public DbSet<coderush.Models.Branch> Branch { get; set; }

        public DbSet<coderush.Models.CashBank> CashBank { get; set; }
        
        public DbSet<coderush.Models.Currency> Currency { get; set; }

        public DbSet<coderush.Models.Customer> Customer { get; set; }

        public DbSet<coderush.Models.CustomerType> CustomerType { get; set; }

        public DbSet<coderush.Models.GoodsReceivedNote> GoodsReceivedNote { get; set; }
        public DbSet<coderush.Models.Invoice> Invoice { get; set; }

        public DbSet<coderush.Models.InvoiceType> InvoiceType { get; set; }

        public DbSet<coderush.Models.NumberSequence> NumberSequence { get; set; }

        public DbSet<coderush.Models.PaymentReceive> PaymentReceive { get; set; }

        public DbSet<coderush.Models.PaymentType> PaymentType { get; set; }

        public DbSet<coderush.Models.PaymentVoucher> PaymentVoucher { get; set; }

        public DbSet<coderush.Models.Product> Product { get; set; }

        public DbSet<coderush.Models.ProductType> ProductType { get; set; }

        public DbSet<coderush.Models.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<coderush.Models.PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<coderush.Models.PurchaseType> PurchaseType { get; set; }

        public DbSet<coderush.Models.SalesOrder> SalesOrder { get; set; }

        public DbSet<coderush.Models.SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<coderush.Models.SalesType> SalesType { get; set; }

        public DbSet<coderush.Models.Shipment> Shipment { get; set; }

        public DbSet<coderush.Models.ShipmentType> ShipmentType { get; set; }

        public DbSet<coderush.Models.UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<coderush.Models.Vendor> Vendor { get; set; }

        public DbSet<coderush.Models.VendorType> VendorType { get; set; }

        public DbSet<coderush.Models.Warehouse> Warehouse { get; set; }

        public DbSet<coderush.Models.UserProfile> UserProfile { get; set; }

        public DbSet<coderush.Models.GoodsRecievedNoteLine> GoodsRecievedNoteLine { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsProduct> ClinicalTrialsProducts { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsDonation> ClinicalTrialsDonation { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsDonationLine> ClinicalTrialsDonationLine { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsSales> ClinicalTrialsSales { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsSalesLine> ClinicalTrialsSalesLine { get; set; }

        public DbSet<coderush.Models.Upload> Uploads { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsReturned> ClinicalTrialsReturned { get; set; }

        public DbSet<coderush.Models.ClinicalTrialsReturnedLine> ClinicalTrialsReturnedLine { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionLines> PrescriptionLines { get; set; }
        public DbSet<coderush.Models.RFPCustomer> RFPCustomer { get; set; }
        public DbSet<coderush.Models.RFPpaymentRecieved> RFPpaymentRecieved { get; set; }
        public DbSet<coderush.Models.RFPinvoice> RFPinvoice { get; set; }
        public DbSet<coderush.Models.RFPDrugRecieve> RFPDrugRecieve { get; set; }
        public DbSet<coderush.Models.RFPSaleorder> RFPSaleorder { get; set; }

    }
}
