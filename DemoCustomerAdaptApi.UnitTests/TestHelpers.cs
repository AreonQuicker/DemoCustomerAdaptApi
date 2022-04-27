using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCustomerAdaptApi.DataAccess;
using DemoCustomerAdaptApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCustomerAdaptApi.UnitTests
{
    public static class TestHelpers
    {
        public static DataContext CreateDbContext()
        {
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase("TestDb")
                .UseInternalServiceProvider(serviceProvider);

            var options = builder.Options;

            var dbContext = new DataContext(options);

            return dbContext;
        }

        public static async Task SeedDataAsync(DataContext dataContext)
        {
            await dataContext.Customers.AddRangeAsync(new CustomerDomainModel()
            {
                Email = "Email",
                Phone = "Phone",
                FirstName = "FirstName",
                LastName = "LastName",
                InvoiceTotal = 1
            });

            await dataContext.SaveChangesAsync();
        }
    }
}