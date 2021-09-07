using AddressBook.UnitTests.Helpers;
using AddressBook.WebAPI.Exceptions;
using AddressBook.WebAPI.Features.Contacts.Commands;
using AddressBook.WebAPI.Models;
using System;
using System.Linq;
using System.Threading;
using Xunit;
using static AddressBook.WebAPI.Features.Contacts.Commands.CreateContactsCommand;

namespace AddressBook.UnitTests.Commands
{
    public class CreateContactsCommandTests : TestBase
    {
        [Fact]
        public async void CreateContactsCommand_NoDuplicateEmailFound_CandidateAdded()
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

                var handler = new CreateContactsCommandHandler(context);

                var command = new CreateContactsCommand
                {
                    FirstName = "Beta",
                    SurName = "B",
                    Email = "test1@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 2
                };

                Assert.True(context.Contacts.Count() == 1);

                var id = await handler.Handle(command, new CancellationToken());

                Assert.True(context.Contacts.Count() == 2);

                var addedContact = context.Contacts.FirstOrDefault(x => x.Id == id);

                Assert.True(addedContact.Id == 2);
                Assert.True(addedContact.FirstName == "Beta");
                Assert.True(addedContact.SurName == "B");
                Assert.True(addedContact.Email == "test1@gmail.com");
                Assert.True(addedContact.DateOfBirth == DateTime.Today);
            }
        }

        [Fact]
        public async void CreateContactsCommand_DuplicateEmailFound_ThrowsContactExistsException()
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

                var handler = new CreateContactsCommandHandler(context);

                var command = new CreateContactsCommand
                {
                    FirstName = "Beta",
                    SurName = "B",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 2
                };

                Assert.True(context.Contacts.Count() == 1);
                _ = Assert.ThrowsAsync<ContactExistsException>(() => handler.Handle(command, new CancellationToken()));
                Assert.True(context.Contacts.Count() == 1);
            }
        }
    }
}
