using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Features.Contacts.Queries
{
    public class GetAllContactsQuery : IRequest<IEnumerable<Contact>>
    {
        public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<Contact>>
        {
            private readonly IContactsDbContext _context;
            public GetAllContactsQueryHandler(IContactsDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Contact>> Handle(GetAllContactsQuery query, CancellationToken cancellationToken)
            {
                var contactsList = await _context.Contacts.ToListAsync(cancellationToken: cancellationToken);
                return contactsList.AsReadOnly();
            }
        }
    }
}
