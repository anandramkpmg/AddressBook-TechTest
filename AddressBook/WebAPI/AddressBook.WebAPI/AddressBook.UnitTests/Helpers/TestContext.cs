using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AddressBook.UnitTests.Helpers
{
    public static class TestContext
    {
        public static ContactsDbContext GetContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ContactsDbContext>().UseInMemoryDatabase("AddressBookTestDB");

            return new ContactsDbContext(dbContextOptions.Options);
        }
       
    }
}
