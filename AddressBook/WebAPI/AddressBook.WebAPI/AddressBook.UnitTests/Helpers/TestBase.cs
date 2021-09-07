using AddressBook.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.UnitTests.Helpers
{
    public class TestBase
    {
        protected internal DbContextOptions<ContactsDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ContactsDbContext>();
            builder.UseInMemoryDatabase("TestDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
