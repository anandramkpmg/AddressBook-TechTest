using AddressBook.UnitTests.Helpers;
using AddressBook.WebAPI.Features.Contacts.Queries;
using AddressBook.WebAPI.Models;
using System;
using System.Threading;
using Xunit;
using static AddressBook.WebAPI.Features.Contacts.Queries.GetContactByIdQuery;

namespace AddressBook.UnitTests.Queries
{
    public class GetContactByIdQueryTests : TestBase
    {
        [Fact]
        public async void GetContactById_MatchFound_ReturnsCandidate()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                var contact1 = GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today);

                context.Contacts.Add(contact1);

                var contact2 = GetContact(2, "Beta", "B", "test1@gmail.com", DateTime.Today);

                context.Contacts.Add(contact2);

                await context.SaveChangesAsync();

                // Act
                var handler = new GetContactByIdQueryHandler(context);
                var contact = await handler.Handle(new GetContactByIdQuery { Id = 1 }, new CancellationToken());

                // Assert
                Assert.Equal(contact.Id, contact1.Id);
                Assert.Equal(contact.FirstName, contact1.FirstName);
                Assert.Equal(contact.SurName, contact1.SurName);
                Assert.Equal(contact.Email, contact1.Email);
                Assert.Equal(contact.DateOfBirth, contact1.DateOfBirth);
            }
        }


        [Fact]
        public async void GetContactById_MatchNotFound_ReturnsNull()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                var contact1 = GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today);

                context.Contacts.Add(contact1);

                await context.SaveChangesAsync();

                // Act
                var handler = new GetContactByIdQueryHandler(context);
                var contact = await handler.Handle(new GetContactByIdQuery { Id = 3 }, new CancellationToken());

                // Assert
                Assert.Null(contact);
            }
        }
    }
}
