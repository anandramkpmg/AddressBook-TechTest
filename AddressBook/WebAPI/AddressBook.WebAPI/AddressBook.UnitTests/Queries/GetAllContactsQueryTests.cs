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

        [Fact]
        public async void GetAllContactsQuery_ContactsExists_ReturnsAllContacts()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                var actualContact = GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today);
                context.Contacts.Add(actualContact);

                var contact2 = GetContact(2, "Beta", "B", "test1@gmail.com", DateTime.Today);

                context.Contacts.Add(new Contact
                {
                    FirstName = contact2.FirstName,
                    SurName = contact2.SurName,
                    Email = contact2.Email,
                    DateOfBirth = contact2.DateOfBirth,
                    Id = contact2.Id
                });

                await context.SaveChangesAsync();

                // Act
                var handler = new GetAllContactsQueryHandler(context);
                var contacts = await handler.Handle(new GetAllContactsQuery(), new CancellationToken());

                // Assert
                var lists = new List<Contact>(contacts);
                Assert.Equal(2, lists.Count);
                var contact = lists[0];

                Assert.Equal(actualContact.FirstName, contact.FirstName );
                Assert.Equal(actualContact.SurName, contact.SurName );
                Assert.Equal(actualContact.Email, contact.Email );
                Assert.Equal(actualContact.DateOfBirth, contact.DateOfBirth);
            }
        }
    }
}