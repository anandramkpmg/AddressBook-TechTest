using AddressBook.UnitTests.Helpers;
using AddressBook.WebAPI.Features.Contacts.Queries;
using AddressBook.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using static AddressBook.WebAPI.Features.Contacts.Queries.GetAllContactsQuery;

namespace AddressBook.UnitTests.Queries
{
    public class GetAllContactsQueryTests : TestBase
    {
        public GetAllContactsQueryTests()
        {
        }

        [Fact]
        public async void GetAllContactsQuery_ContactsExists_ReturnsAllContacts()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                context.Contacts.Add(new Contact
                {
                    FirstName = "Alpha",
                    SurName = "A",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 1
                });

                context.Contacts.Add(new Contact
                {
                    FirstName = "Beta",
                    SurName = "B",
                    Email = "test1@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 2
                });

                await context.SaveChangesAsync();

                // Act
                var handler = new GetAllContactsQueryHandler(context);
                var contacts = await handler.Handle(new GetAllContactsQuery(), new CancellationToken());

                // Assert
                var lists = new List<Contact>(contacts);
                Assert.True(lists.Count == 2);
                Assert.True(lists[0].FirstName == "Alpha");
                Assert.True(lists[0].SurName == "A");
                Assert.True(lists[0].Email == "test@gmail.com");
                Assert.True(lists[0].DateOfBirth == DateTime.Today);
            }
        }
    }
}