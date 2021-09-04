using AddressBook.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Context
{
    public interface IContactsDbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
