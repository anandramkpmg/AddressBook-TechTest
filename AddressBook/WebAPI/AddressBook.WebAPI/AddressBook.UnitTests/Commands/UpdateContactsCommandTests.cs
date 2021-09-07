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
                context.Contacts.Add(new Contact
                {
                    FirstName = "Alpha",
                    SurName = "A",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 1
                });

                await context.SaveChangesAsync();

                var handler = new UpdateContactsCommandHandler(context);

                var command = new UpdateContactsCommand
                {
                    FirstName = "Alpha",
                    SurName = "B",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 1
                };

                // Act
                var id = await handler.Handle(command, new CancellationToken());

                var updatedContact = context.Contacts.FirstOrDefault(x => x.Id == command.Id);

                // Assert
                Assert.True(updatedContact.FirstName == "Alpha");
                Assert.True(updatedContact.SurName == "B");
                Assert.True(updatedContact.Email == "test@gmail.com");
                Assert.True(updatedContact.DateOfBirth == DateTime.Today);
            }
        }

        [Fact]
        public async void UpdateContactsCommand_CandidateNotFound_DefaultCandidateReturned()
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

                await context.SaveChangesAsync();

                var handler = new UpdateContactsCommandHandler(context);

                var command = new UpdateContactsCommand
                {
                    FirstName = "Alpha",
                    SurName = "B",
                    Email = "test1@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 2
                };

                var id = await handler.Handle(command, new CancellationToken());

                var updatedContact = context.Contacts.FirstOrDefault(x => x.Id == command.Id);

                Assert.Null(updatedContact);
            }
        }

        [Fact]
        public async void UpdateContactsCommand_EmailIdAlreadyExists_ThrowsContactExistsException()
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
