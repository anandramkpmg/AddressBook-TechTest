using AddressBook.WebAPI.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Features.Contacts.Commands
{
    public class UpdateContactsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public class UpdateContactsCommandHandler : IRequestHandler<UpdateContactsCommand, int>
        {
            private readonly IContactsDbContext _context;
            public UpdateContactsCommandHandler(IContactsDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateContactsCommand command, CancellationToken cancellationToken)
            {
                var contact = _context.Contacts.Where(a => a.Id == command.Id).FirstOrDefault();

                if (contact == null)
                {
                    return default;
                }
                else
                {
                    contact.FirstName = command.FirstName;
                    contact.SurName = command.SurName;
                    contact.DateOfBirth = command.DateOfBirth;
                    contact.Email = command.Email;
                    await _context.SaveChangesAsync();
                    return contact.Id;
                }
            }
        }
    }
}
