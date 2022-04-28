using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            ExpiredDrugs();

            OutOfStock();
            Console.WriteLine("test");
            return Task.CompletedTask;
        }
        public Task OutOfStock()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<Product> Items = context.Product
                    .Where(x => x.InStock > 20)
                    .ToList();
                int count = Items.Count();
               
               

                if (Items != null)
                {
                    string title = "<P>The following drugs are in low stock </p>";

                   
                    string drugrow = "";
                    int num = 1;
                    foreach (var batch in Items)
                    {
                       
                        string drug = "  " + num + " Name: " + batch.ProductName + " Units of measure: "
                                        + batch.UnitOfMeasureId + " Quantity: " + batch.InStock ;
                        drugrow = drugrow + "\r\n " + drug;
                        num++;
                    }

                    var message = title  + drugrow;

                    Console.WriteLine(message);
                    emailSender.SendEmailAsync("tengevictor7@gmail.com", "Low Stock", message);
                }



            }

            return Task.CompletedTask;
        }
        public Task ExpiredDrugs()
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                List<GoodsRecievedNoteLine> Items = context.GoodsRecievedNoteLine
                    .Where(x => x.InStock > 0)
                    .Include(x => x.Product)
                    .ToList();
                int count = Items.Count();
                Console.WriteLine(count);
                DateTime current = DateTime.Now;
                List<GoodsRecievedNoteLine> Batches = new List<GoodsRecievedNoteLine>();
                foreach (GoodsRecievedNoteLine item in Items) 
                {
                    if ((item.ExpiryDate - current).TotalDays < 90)
                    {
                        Batches.Add(item);
                    }
                }

                if (Batches != null) 
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

                    Console.WriteLine(message);
                    emailSender.SendEmailAsync("tengevictor7@gmail.com", "expireddrugs", message);
                }
                

               
            }
          
            return Task.CompletedTask;
        }
    }
}
