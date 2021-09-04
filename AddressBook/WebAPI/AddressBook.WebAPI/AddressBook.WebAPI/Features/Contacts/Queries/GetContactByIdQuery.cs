using AddressBook.WebAPI.Context;
using AddressBook.WebAPI.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.WebAPI.Features.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Contact>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
        {

            private readonly IContactsDbContext _context;
            public GetProductByIdQueryHandler(IContactsDbContext context)
            {
                _context = context;
            }
            public async Task<Contact> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
            {
                var product = _context.Contacts.Where(a => a.Id == query.Id).FirstOrDefault();
                if (product == null) return null;
                return product;
            }
        }
    }
}
