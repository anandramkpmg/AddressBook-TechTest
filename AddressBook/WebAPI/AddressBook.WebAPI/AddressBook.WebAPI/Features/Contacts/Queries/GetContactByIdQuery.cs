using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.WebAPI.Features.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Contact>
    {
        public int Id { get; set; }

        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
        {

            private readonly IContactsDbContext _context;
            public GetContactByIdQueryHandler(IContactsDbContext context)
            {
                _context = context;
            }
            public async Task<Contact> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.Contacts.FirstOrDefaultAsync(a => a.Id == query.Id, cancellationToken: cancellationToken);
            }
        }
    }
}
