using EFCoreAttachBugTest002.HostedServices;
using EFCoreAttachBugTest002.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreAttachBugTest002
{
    class Program
    {
        private static IHost Host { get; }

        static Program()
        {
            Host = CreateHostBuilder(null).Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                                                        .UseDefaultServiceProvider((context, options) =>
                                                        {
                                                            options.ValidateOnBuild = true;
                                                            options.ValidateScopes = true;
                                                        })
                                                        .ConfigureLogging((context, loggingBuilder) => { loggingBuilder.SetMinimumLevel(LogLevel.Error); })
                                                        .ConfigureServices((hostBuilderContext, services) => ConfigureServices(services, hostBuilderContext.Configuration));
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(ServiceLifetime.Transient, ServiceLifetime.Singleton);
            services.AddHostedService<AppHostedService>();
        }

        static async Task Main(string[] args)
        {
            await Host.StartAsync().ConfigureAwait(false);

            Example one = new Example() { Id = 1, IntegerValue = 10 };
            Example two = new Example() { Id = 2, IntegerValue = 20 };

            Holder holder = new Holder();
            holder.Examples.Add(one);
            holder.Examples.Add(two);

            IEnumerable<MysteryType> mysteryTypes;

            using (IServiceScope scope = Host.Services.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>())
            {
                // Reset the examples.
                dbContext.Examples.RemoveRange(dbContext.Examples);
                dbContext.Holders.RemoveRange(dbContext.Holders);

                await dbContext.SaveChangesAsync().ConfigureAwait(false);

                // Populate mysteryTypes list.
                mysteryTypes = dbContext.MysteryTypes.ToList();
            }

            using (IServiceScope scope = Host.Services.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>())
            {
                dbContext.Add(holder);

                Console.WriteLine("Created examples:");
                Console.WriteLine(GetExampleText(one));
                Console.WriteLine(GetExampleText(two));
                Console.WriteLine();

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            await UpdateExampleMysteryType(one, 1, mysteryTypes.First(mt => mt.Id == 1)).ConfigureAwait(false);
            await UpdateExampleMysteryType(two, 1, mysteryTypes.First(mt => mt.Id == 1)).ConfigureAwait(false);
            await UpdateExampleMysteryType(one, 2, mysteryTypes.First(mt => mt.Id == 2)).ConfigureAwait(false);

            await Host.StopAsync().ConfigureAwait(false);
            Host.Dispose();
        }

        private static async Task UpdateExampleMysteryType(Example example, int? mysteryTypeId, MysteryType mysteryType)
        {
            Console.WriteLine($"Setting mystery type to {mysteryTypeId} ({mysteryType?.Name ?? "null"})");
            example.MysteryTypeId = mysteryTypeId;
            example.MysteryType = mysteryType;
            example.IntegerValue = example.IntegerValue + 10;

            using (IServiceScope scope = Host.Services.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>())
            {

                Console.WriteLine($"Example before attach: {GetExampleText(example)}");
                var entry = dbContext.Attach(example);
                Console.WriteLine($"Example after attach: {GetExampleText(example)}");

                if (example.MysteryTypeId != mysteryTypeId)
                {
                    Console.WriteLine("Value has been reset!?");
                }

                Console.WriteLine();
                Console.WriteLine();

                entry.Property(e => e.MysteryTypeId).IsModified = true;

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private static string GetExampleText(Example example)
        {
            return $"Example - Id: {example.Id}, IntegerValue: {example.IntegerValue}, MysteryType: {example.MysteryTypeId} ({example.MysteryType?.Name ?? "null"})";
        }
    }
}
