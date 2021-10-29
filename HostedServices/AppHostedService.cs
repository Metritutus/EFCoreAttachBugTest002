using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAttachBugTest002.HostedServices
{
    public class AppHostedService : IHostedService
    {
        private IServiceProvider ServiceProvider { get; }

        public AppHostedService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = ServiceProvider.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>())
            {
                await dbContext.Database.MigrateAsync().ConfigureAwait(false);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
