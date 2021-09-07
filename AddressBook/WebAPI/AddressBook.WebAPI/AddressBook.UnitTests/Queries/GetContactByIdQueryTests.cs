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
                var handler = new GetProductByIdQueryHandler(context);
                var contact = await handler.Handle(new GetContactByIdQuery { Id = 1 }, new CancellationToken());

                // Assert
                Assert.True(contact.Id == 1);
                Assert.True(contact.FirstName == "Alpha");
                Assert.True(contact.SurName == "A");
                Assert.True(contact.Email == "test@gmail.com");
                Assert.True(contact.DateOfBirth == DateTime.Today);
            }
        }


        [Fact]
        public async void GetContactById_MatchNotFound_ReturnsNull()
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
                var handler = new GetProductByIdQueryHandler(context);
                var contact = await handler.Handle(new GetContactByIdQuery { Id = 3 }, new CancellationToken());

                // Assert
                Assert.True(contact == null);
            }
        }
    }
}
