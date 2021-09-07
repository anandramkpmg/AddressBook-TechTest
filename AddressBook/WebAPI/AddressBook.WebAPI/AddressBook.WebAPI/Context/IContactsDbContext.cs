using AddressBook.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Context
{
    public interface IContactsDbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public Task<int> SaveChangesAsync();

        public DatabaseFacade Database { get; }
    }
}
