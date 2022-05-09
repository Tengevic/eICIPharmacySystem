using coderush.Data;
using coderush.Models;
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
using System.Configuration;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Hosting;


namespace coderush.Services
{
    public class EmailNotifications 
    {
        private IHostingEnvironment _environment;
        private readonly IServiceProvider _provider;

        public EmailNotifications
        (
            IServiceProvider provider,
            IHostingEnvironment environment
        )
        {
            _provider = provider;
            _environment = environment;
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
                  
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Batches)
                    {
                        string drug = "<tr>" +
                                           "<td>"+ num + " </td>" +
                                           "<td>" + batch.clinicalTrialsProducts.ProductName + "</td>" +
                                           "<td>" + batch.BatchID + "</td>" +
                                           "<td>" + batch.ExpiryDate + "</td>" +
                                           "<td>" + batch.InStock + "</td>" +
                                       "</tr>";
                        drugrow = drugrow + drug;
                        num++;
                    }

                    var message = emailbody("Expire", drugrow);

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
                    
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Items)
                    {

                        string drug = "<tr>" +
                                             "<td>" + num + "</td>" +
                                             "<td>" + batch.ProductName + "</td>" +
                                             "<td>" + batch.UnitOfMeasureId + "</td>" +
                                             "<td>" + batch.InStock + "</td>" +
                                        "</tr>";
                        drugrow = drugrow + drug;
                       num++;
                    }

                    string message = emailbody("Stock", drugrow);
                   

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
                    string InvoiceRow = "";
                    int num = 1;
                    foreach (var batch in Due)
                    {
                        string invoice = "<tr>" +
                                              "<td>" + num + " </td> " +
                                              "<td>" + batch.BillName + "</td>" +
                                              "<td>" + batch.BillDate + " </td>" +
                                              "<td>" + batch.BillDueDate + "</td>" +
                                        "</tr>";
                        InvoiceRow = InvoiceRow + invoice;
                        num++;
                    }
                    var message = emailbody("DueInvoice", InvoiceRow);

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
                    string InvoiceRow = "";
                    int num = 1;
                    foreach (var batch in Due)
                    {
                        string invoice = "<tr>" +
                                              "<td>" + num + " </td> " +
                                              "<td>" + batch.InvoiceName + "</td>" +
                                              "<td>" + batch.InvoiceDate.Date + " </td>" +
                                              "<td>" + batch.InvoiceDueDate.Date + "</td>" +
                                        "</tr>";
                        InvoiceRow = InvoiceRow + invoice;
                        num++;
                    }

                    var message = emailbody("DueInvoice",InvoiceRow);
                    
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
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Batches)
                    {
                        string drug = "<tr>" +
                                           "<td>" + num + " </td>" +
                                           "<td>" + batch.Product.ProductName + "</td>" +
                                           "<td>" + batch.BatchID + "</td>" +
                                           "<td>" + batch.ExpiryDate.Date + "</td>" +
                                           "<td>" + batch.InStock + "</td>" +
                                       "</tr>";
                        drugrow = drugrow + drug;
                        num++;
                    }

                    var message = emailbody("Expire", drugrow);


                    await roles.GenerateRolesFromPagesAsync();

                    List<UserProfile> users = context.UserProfile.ToList();

                    foreach (UserProfile userProfile in users)
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
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Items)
                    {
                        string drug = "<tr>" +
                                            "<td>"+ num +"</td>" +
                                            "<td>" + batch.ProductName + "</td>"+
                                            "<td>" + batch.UnitOfMeasureId + "</td>" + 
                                            "<td>" + batch.InStock + "</td>" + 
                                       "</tr>";
  
                        drugrow = drugrow + drug;
                        num++;
                    }
                    string message = emailbody("Stock", drugrow);
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
        public string emailbody(string type, string message)
        {
            string path = "";
            string body = "";
          
            if (type == "Stock")
            {
                path = this._environment.ContentRootPath + @"\Views\EmailViewModels\OutOfstock.html";      
            }
            else if (type == "Expire")
            {
                path = this._environment.ContentRootPath + @"\Views\EmailViewModels\Expired.html";
            }
            else if (type == "DueInvoice")
            {
                path = this._environment.ContentRootPath + @"\Views\EmailViewModels\DueInvoice.html";
            }
            using (StreamReader reader = File.OpenText(path))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{List}", message);
          

            return body; 
        }
    }
}
