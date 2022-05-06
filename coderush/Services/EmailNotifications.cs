using coderush.Data;
using coderush.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coderush.Services
{
    public class EmailNotifications 
    {

        private readonly IServiceProvider _provider;

        public EmailNotifications
        (
            IServiceProvider provider
        )
        {
            _provider = provider;
        }

        public Task SendEmail()
        {

            _ = ExpiredDrugs();

            _ = OutOfStock();

            _ = InvoiceDueDate();

            _ = BillInvoiceDueDate();

            _ = ClinicalTrilasOutOfStock();

            _ = ClinicalTrilasExpiredDrugs();

            return Task.CompletedTask;
        }
        public async Task<Task> ClinicalTrilasExpiredDrugs()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<ClinicalTrialsDonationLine> Items = context.ClinicalTrialsDonationLine
                    .Where(x => x.InStock > 0)
                    .Include(x => x.clinicalTrialsProducts)
                    .ToList();
                int count = Items.Count();

                DateTime current = DateTime.Now;
                List<ClinicalTrialsDonationLine> Batches = new List<ClinicalTrialsDonationLine>();
                foreach (ClinicalTrialsDonationLine item in Items)
                {
                    if ((item.ExpiryDate - current).TotalDays < 90)
                    {
                        Batches.Add(item);
                    }
                }

                if (Batches.Count != 0)
                {
                    string title = "The following drug batches will expire in less than 90 days\n";

                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Batches)
                    {
                        string drug = "  " + num + " Name: " + batch.clinicalTrialsProducts.ProductName + " Batch Id: "
                                        + batch.BatchID + " Quantity: " + batch.InStock + " Expiring date: " + batch.ExpiryDate + "\r\n";
                        drugrow = drugrow + "\r\n " + drug;
                        num++;
                    }

                    var message = title + " \n " + drugrow;

                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "Clinical Trial Drug")) ? true : false;

                        if (isInRole)
                        {
                            await emailSender.SendEmailAsync(userProfile.Email, "Clinical Trials Expired drugs", message);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
        public async Task<Task> ClinicalTrilasOutOfStock()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<ClinicalTrialsProduct> Items = context.ClinicalTrialsProducts
                    .Where(x => x.InStock < 20)
                   // .Include(x => x.UnitOfMeasure)
                    .ToList();

                if (Items.Count != 0)
                {
                    string title = "<P>The following  clinical trial drugs are in low stock </p>";
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Items)
                    {

                        //string drug = "  " + num + " Name: " + batch.ProductName + " Units of measure: "
                        //                + batch.UnitOfMeasure + " Quantity: " + batch.InStock;
                        //drugrow = drugrow + "\r\n " + drug;
                        //num++;
                    }

                    var message = title + drugrow;

                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "Clinical Trial Drug")) ? true : false;

                        if (isInRole)
                        {
                            await emailSender.SendEmailAsync(userProfile.Email, "Clinical Trials low stock", message);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        public async Task<Task> BillInvoiceDueDate()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<Bill> Items = context.Bill
                                        .ToList();
                int count = Items.Count();

                DateTime current = DateTime.Now;
                List<Bill> Due = new List<Bill>();
                foreach (Bill bill in Items)
                {
                    double totaldays = (bill.BillDueDate - current).TotalDays;

                    if (totaldays < 30)
                    {
                        Due.Add(bill);
                    }
                }

                if (Due.Count != 0)
                {
                    string title = "<P>The following Invoice are due in one month </p>";
                    string InvoiceRow = "";
                    int num = 1;
                    foreach (var batch in Due)
                    {
                        string invoice = "  " + num + " Name: " + batch.BillName + " Invoice date: "
                                        + batch.BillDate + " Invoice Due date: " + batch.BillDate;
                        InvoiceRow = InvoiceRow + "\r\n " + invoice;
                        num++;
                    }
                    var message = title + InvoiceRow;

                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "Bill")) ? true : false;

                        if (isInRole)
                        {
                            await emailSender.SendEmailAsync(userProfile.Email, "Due Bill", message);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
        public async Task<Task> InvoiceDueDate()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<Invoice> Items = context.Invoice
                                        .ToList();
                int count = Items.Count();

                DateTime current = DateTime.Now;
                List<Invoice> Due = new List<Invoice>();
                foreach(Invoice invoice in Items)
                {
                    double totaldays = (invoice.InvoiceDueDate - current).TotalDays;

                    if(totaldays < 30)
                    {
                        Due.Add(invoice);
                    }
                }

                if (Due.Count != 0)
                {
                    string title = "<P>The following Invoice are due in one month </p>";
                    string InvoiceRow = "";
                    int num = 1;
                    foreach (var batch in Due)
                    {
                        string invoice = "  " + num + " Name: " + batch.InvoiceName + " Invoice date: "
                                        + batch.InvoiceDate + " Invoice Due date: " + batch.InvoiceDueDate;
                        InvoiceRow = InvoiceRow + "\r\n " + invoice;
                        num++;
                    }

                    var message = title + InvoiceRow;
                    
                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "ICI Invoice")) ? true : false;

                        if (isInRole)
                        {
                            await emailSender.SendEmailAsync(userProfile.Email, "Due ICI Invoices", message);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
        public async Task<Task> OutOfStock()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<Product> Items = context.Product
                    .Where(x => x.InStock < 20)
                   // .Include(x => x.UnitOfMeasure)
                    .ToList();

                if (Items.Count != 0)
                {
                    string title = "<P>The following drugs are in low stock </p>";
  
                    string drugrow = "";
                    int num = 1;
                    //foreach (var batch in Items)
                    //{
                   
                    //    string drug = "  " + num + " Name: " + batch.ProductName + " Units of measure: "
                    //                    + batch.UnitOfMeasureId + " Quantity: " + batch.InStock ;
                    //    drugrow = drugrow + "\r\n " + drug;
                    //    num++;
                    //}

                    var message = title  + drugrow;
                   
                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "Drugs")) ? true : false;

                        if (isInRole)
                        {
                            await emailSender.SendEmailAsync(userProfile.Email, "Low stock", message);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
        public async Task<Task> ExpiredDrugs()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                var roles = scope.ServiceProvider.GetRequiredService<IRoles>();

                List<GoodsRecievedNoteLine> Items = context.GoodsRecievedNoteLine
                    .Where(x => x.InStock > 0)
                    .Include(x => x.Product)
                    .ToList();
                int count = Items.Count();
             
                DateTime current = DateTime.Now;
                List<GoodsRecievedNoteLine> Batches = new List<GoodsRecievedNoteLine>();
                foreach (GoodsRecievedNoteLine item in Items) 
                {
                    if ((item.ExpiryDate - current).TotalDays < 90)
                    {
                        Batches.Add(item);
                    }
                }

                if (Batches.Count != 0) 
                {
                    string title = "The following drug batches will expire in less than 90 days\n";

                    string drugrow = "";
                    int num = 1;
                    foreach(var batch in Batches)
                    {
                        string drug = "  " + num + " Name: " + batch.Product.ProductName + " Batch Id: "
                                        + batch.BatchID +" Quantity: "+ batch.InStock +" Expiring date: " + batch.ExpiryDate + "\r\n";
                        drugrow = drugrow + "\r\n "+ drug;
                        num++;
                    }

                    var message = title + " \n "+ drugrow;

                    await roles.GenerateRolesFromPagesAsync();

                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach(UserProfile userProfile in users)
                    {
                        var id = userProfile.ApplicationUserId;
                        var user = await userManager.FindByIdAsync(id);

                        bool isInRole = (await userManager.IsInRoleAsync(user, "Drugs")) ? true : false;

                        if (isInRole)
                        {
                              await emailSender.SendEmailAsync(userProfile.Email, "Expired drugs", message);
                        }
                    }
                }                          
            }          
            return Task.CompletedTask;
        }
      
    }
}
