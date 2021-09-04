using AddressBook.WebAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Models
{
    public class ContactsDbContext : DbContext, IContactsDbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
