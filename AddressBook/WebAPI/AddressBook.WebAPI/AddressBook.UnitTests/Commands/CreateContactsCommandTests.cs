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
                context.Contacts.Add(GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today));

                await context.SaveChangesAsync();

                var handler = new CreateContactsCommandHandler(context);

                var contact2 = GetContact(2, "Beta", "B", "test1@gmail.com", DateTime.Today);

                var command = new CreateContactsCommand
                {
                    FirstName = contact2.FirstName,
                    SurName = contact2.SurName,
                    Email = contact2.Email,
                    DateOfBirth = contact2.DateOfBirth,
                    Id = contact2.Id
                };

                Assert.True(context.Contacts.Count() == 1);

                var id = await handler.Handle(command, new CancellationToken());

                Assert.True(context.Contacts.Count() == 2);

                var addedContact = context.Contacts.First(x => x.Id == id);

                // TO DO : Override equals operator
                //Assert.True(addedContact.Equals(contact2));

                Assert.True(addedContact.Id == contact2.Id);
                Assert.True(addedContact.FirstName == contact2.FirstName);
                Assert.True(addedContact.SurName == contact2.SurName);
                Assert.True(addedContact.Email == contact2.Email);
                Assert.True(addedContact.DateOfBirth == contact2.DateOfBirth);
            }
        }

        [Fact]
        public async void CreateContactsCommand_DuplicateEmailFound_ThrowsContactExistsException()
        {
            using (var context = new ContactsDbContext(CreateNewContextOptions()))
            {
                context.Contacts.Add(GetContact(1, "Alpha", "A", "test@gmail.com", DateTime.Today));

                await context.SaveChangesAsync();

                var handler = new CreateContactsCommandHandler(context);

                var contact2 = GetContact(2, "Beta", "B", "test@gmail.com", DateTime.Today);

                var command = new CreateContactsCommand
                {
                    FirstName = contact2.FirstName,
                    SurName = contact2.SurName,
                    Email = contact2.Email,
                    DateOfBirth = contact2.DateOfBirth,
                    Id = contact2.Id
                };

                Assert.Single(context.Contacts);
                _ = Assert.ThrowsAsync<ContactExistsException>(() => handler.Handle(command, new CancellationToken()));
                Assert.Single(context.Contacts);
            }
        }
    }
}
