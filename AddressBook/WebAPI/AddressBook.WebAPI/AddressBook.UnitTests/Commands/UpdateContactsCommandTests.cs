using AddressBook.UnitTests.Helpers;
using AddressBook.WebAPI.Exceptions;
using AddressBook.WebAPI.Features.Contacts.Commands;
using AddressBook.WebAPI.Models;
using System;
using System.Linq;
using System.Threading;
using Xunit;
using static AddressBook.WebAPI.Features.Contacts.Commands.UpdateContactsCommand;

namespace AddressBook.UnitTests.Commands
{
    public class UpdateContactsCommandTests : TestBase
    {
        [Fact]
        public async void UpdateContactsCommand_CandidateMatch_CandidateUpdated()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                context.Contacts.Add(GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today));

                await context.SaveChangesAsync();

                var handler = new UpdateContactsCommandHandler(context);

                var contact2 = GetContact(1, "Alpha", "B", "test@gmail.com", DateTime.Today);

                var command = new UpdateContactsCommand
                {
                    FirstName = contact2.FirstName,
                    SurName = contact2.SurName,
                    Email = contact2.Email,
                    DateOfBirth = contact2.DateOfBirth,
                    Id = contact2.Id
                };

                // Act
                var id = await handler.Handle(command, new CancellationToken());

                var updatedContact = context.Contacts.First(x => x.Id == id);

                // Assert
                Assert.Equal(contact2.FirstName, updatedContact.FirstName);
                Assert.Equal(contact2.SurName, updatedContact.SurName);
                Assert.Equal(contact2.Email, updatedContact.Email);
                Assert.Equal(contact2.DateOfBirth, updatedContact.DateOfBirth);
            }
        }

        [Fact]
        public async void UpdateContactsCommand_CandidateNotFound_DefaultCandidateReturned()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                context.Contacts.Add(GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today));

                await context.SaveChangesAsync();

                var handler = new UpdateContactsCommandHandler(context);

                var contact2 = GetContact(2, "Alpha", "B", "test1@gmail.com", DateTime.Today);

                var command = new UpdateContactsCommand
                {
                    FirstName = contact2.FirstName,
                    SurName = contact2.SurName,
                    Email = contact2.Email,
                    DateOfBirth = contact2.DateOfBirth,
                    Id = contact2.Id
                };

                var id = await handler.Handle(command, new CancellationToken());

                var updatedContact = context.Contacts.FirstOrDefault(x => x.Id == id);

                Assert.Null(updatedContact);
            }
        }

        [Fact]
        public async void UpdateContactsCommand_EmailIdAlreadyExists_ThrowsContactExistsException()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                context.Contacts.Add(GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today));

                context.Contacts.Add(GetContact(2, "Beta", "B", "test1@gmail.com", DateTime.Today));

                await context.SaveChangesAsync();

                var handler = new UpdateContactsCommandHandler(context);

                var command = new UpdateContactsCommand
                {
                    FirstName = "Alpha",
                    SurName = "B",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 2
                };

                // Act
                _ = Assert.ThrowsAsync<ContactExistsException>(() => handler.Handle(command, new CancellationToken()));
            }
        }
    }
}
