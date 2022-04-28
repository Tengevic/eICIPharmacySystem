using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coderush.Services
{
    public class DataRefreshService : HostedService
    {
        private readonly EmailNotifications _emailNotifications;

        public DataRefreshService(EmailNotifications emailNotifications)
        {
            _emailNotifications = emailNotifications;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string test = "this is a test\n";
                string test2 = "second line test";
                Console.WriteLine(test+test2);
                
                
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                Console.WriteLine("start");
                await _emailNotifications.SendEmail();

            }
        }
    }
}
