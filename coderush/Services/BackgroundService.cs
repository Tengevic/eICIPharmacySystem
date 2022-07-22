using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coderush.Services
{
    public class BackgroundService : HostedService
    {
        private readonly EmailNotifications _emailNotifications;

        public BackgroundService(EmailNotifications emailNotifications)
        {
            _emailNotifications = emailNotifications;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(1), cancellationToken);
                //await _emailNotifications.SendEmail();
            }
        }
    }
}
