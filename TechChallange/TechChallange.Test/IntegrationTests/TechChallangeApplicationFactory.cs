using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;
using Microsoft.EntityFrameworkCore;
using TechChallange.Infrastructure.Context;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Test.IntegrationTests
{
    //public class TechChallangeApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    //{
    //    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder().Build();

    //    protected override IHost CreateHost(IHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            var x = services.SingleOrDefault(
    //                d => d.ServiceType == typeof(DbContextOptions<TechChallangeContext>
    //                ));

    //            services.Remove(x!);

    //            services.AddDbContext<TechChallangeContext>(Options =>
    //                Options.UseSqlServer(_sqlContainer.GetConnectionString())
    //            );

    //        });

    //        return base.CreateHost(builder);
    //    }
    //    public async Task InitializeAsync()
    //    {
    //        await _sqlContainer.StartAsync();
    //        Environment.SetEnvironmentVariable("ConnectionStrings.Database", _sqlContainer.GetConnectionString());
    //    }

    //    async Task IAsyncLifetime.DisposeAsync()
    //    {
    //        await _sqlContainer.StopAsync();
    //    }
    //}

    public class TechChallangeApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder().Build();
        private string? _connectionString;

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<TechChallangeContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TechChallangeContext>(options =>
                    options.UseSqlServer(_connectionString!)
                );
            });

            return base.CreateHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();
            _connectionString = _sqlContainer.GetConnectionString();
            Environment.SetEnvironmentVariable("ConnectionStrings.Database", _connectionString);

            await WaitForDatabaseAsync();

            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TechChallangeContext>();
                await context.Database.EnsureCreatedAsync();

                var region = new RegionEntity("SP", "11");

                context.Region.Add(region);
                await context.SaveChangesAsync();
            }
        }

        public async Task DisposeAsync()
        {
            await _sqlContainer.StopAsync();
        }

        private async Task WaitForDatabaseAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            for (int i = 0; i < 10; i++)  // Tenta por até 10 segundos
            {
                try
                {
                    await connection.OpenAsync();
                    return;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
            throw new Exception("Banco de dados não respondeu dentro do tempo esperado.");
        }
    }

}
