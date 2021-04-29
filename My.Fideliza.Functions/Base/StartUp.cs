using Demo.Fideliza.Functions.Data;
using Demo.Fideliza.Functions.Data.Repositories;
using Demo.Fideliza.Functions.Domain;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using My.Fideliza.Functions.Data.Repositories;
using My.Fideliza.Functions.Domain;
using System;

[assembly: FunctionsStartup(typeof(Demo.Fideliza.Functions.StartUp))]
namespace Demo.Fideliza.Functions
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");
            builder.Services.AddDbContext<FidelizaDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            builder.Services.AddScoped(typeof(ICustomerDomain), typeof(CustomerDomain));
            builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));

            builder.Services.AddScoped(typeof(ITransactionDomain), typeof(TransactionDomain));
            builder.Services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
        }
    }
}
