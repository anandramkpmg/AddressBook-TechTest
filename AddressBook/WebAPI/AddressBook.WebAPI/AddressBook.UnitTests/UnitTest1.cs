using AddressBook.WebAPI.Features.Contacts.Commands;
using AutoFixture;
using System;
using Xunit;
using Moq;
using static AddressBook.WebAPI.Features.Contacts.Commands.CreateContactsCommand;
using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Features.Contacts.Queries;
using static AddressBook.WebAPI.Features.Contacts.Queries.GetAllContactsQuery;
using System.Collections;
using AddressBook.WebAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AddressBook.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //var fixture = new Fixture();
            //var command = fixture.Create<GetAllContactsQueryHandler>();
            //command.Handle()

            var getAllQuery = new GetAllContactsQuery();

            var mockContext = new Mock<IContactsDbContext>();

            var contacts = GetContacts();

            var mockDbSet = GetMockDbSet<Contact>(contacts);

            mockContext.Setup(c => c.Contacts).Returns(mockDbSet.Object);

            var obj = new GetAllContactsQueryHandler(mockContext.Object);
            var contactsList = await obj.Handle(getAllQuery, new System.Threading.CancellationToken());
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

        internal static Mock<DbSet<T>> GetMockDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            return mockSet;
        }

    }
}


//private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
//    {
//        var queryable = sourceList.AsQueryable();

//        var dbSet = new Mock<DbSet<T>>();
//        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
//        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
//        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
//        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
//        dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

//        return dbSet.Object;
//    }
//}

//        private static Mock<DbSet> T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
// {
//  var elementsAsQueryable = elements.AsQueryable();
//        var dbSetMock = new Mock<DbSet<T>>();

//        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
//        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
//        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
//        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

//  return dbSetMock;
//}