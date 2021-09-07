using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Exceptions;
using AddressBook.WebAPI.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Features.Contacts.Commands
{
    public class CreateContactsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public class CreateContactsCommandHandler : IRequestHandler<CreateContactsCommand, int>
        {
            private readonly IContactsDbContext _context;
            public CreateContactsCommandHandler(IContactsDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateContactsCommand command, CancellationToken cancellationToken)
            {
                if (_context.Contacts.Any(x => x.Email.ToLower() == command.Email.ToLower()))
                {
                    throw new ContactExistsException($"Contact already exists with email id: {command.Email}");
                }

                var contact = new Contact
                {
                    FirstName = command.FirstName,
                    SurName = command.SurName,
                    DateOfBirth = command.DateOfBirth,
                    Email = command.Email
                };
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return contact.Id;
            }
        }
    }
}
