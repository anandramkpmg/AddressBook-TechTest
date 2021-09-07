using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static AddressBook.WebAPI.Features.Contacts.Queries.GetAllContactsQuery;

namespace AddressBook.UnitTests
{
    public class Class1
    {
        [Fact]
        public async void Test1()
        {
        }

        [Fact]
        public async Task GetAllBlogsAsync_orders_by_name()
        {

            var data = new List<Contact>
            {
                new Contact { FirstName = "BBB" },
                new Contact { FirstName = "AAA" },
            }.AsQueryable();

            var data1 = new List<Contact>
            {
                new Contact { FirstName = "BBB" },
                new Contact { FirstName = "AAA" },
            };


            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Contact>>();
            //mockSet.As<IDbAsyncEnumerable<Contact>>()
            //    .Setup(m => m.GetAsyncEnumerator())
            //    .Returns(new TestDbAsyncEnumerator<Contact>(data.GetEnumerator()));

            //mockSet.As<IQueryable<Contact>>()
            //    .Setup(m => m.Provider)
            //    .Returns(new TestDbAsyncQueryProvider<Contact>(data.Provider));

            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ToListAsync()).Returns(Task.FromResult(data1));


            var mockContext = new Mock<IContactsDbContext>();
            mockContext.Setup(c => c.Contacts).Returns(mockSet.Object);

            var obj = new GetAllContactsQueryHandler(mockContext.Object);
            var contactsList = await obj.Handle(new WebAPI.Features.Contacts.Queries.GetAllContactsQuery(), new System.Threading.CancellationToken());
            Assert.NotNull(contactsList);
        }


        private ICollection<Contact> GetContacts()
        {

            var contacts = new List<Contact>(2);

            var contact1 = new Contact
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            var contact2 = new Contact
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            contacts.Add(contact1);
            contacts.Add(contact2);

            return contacts;
        }

    }
}

