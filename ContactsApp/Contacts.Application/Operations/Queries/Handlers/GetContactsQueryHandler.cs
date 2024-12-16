using Contacts.Application.Contract;
using Contacts.Domain.Models;
using MediatR;
using Shared.Response;

namespace Contacts.Application.Operations.Queries.Handlers
{
    public class GetContactsQueryHandler(IContactService contactService) : IRequestHandler<GetContactsQuery, PaginatedResponse<Contact>>
    {
        public async Task<PaginatedResponse<Contact>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await contactService.GetAllContacts();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                contacts = contacts.Where(c =>
                    c.FirstName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.LastName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.Email.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            contacts = request.SortBy?.ToLower() switch
            {
                "firstname" => request.IsAscending ? contacts.OrderBy(c => c.FirstName).ToList() : contacts.OrderByDescending(c => c.FirstName).ToList(),
                "lastname" => request.IsAscending ? contacts.OrderBy(c => c.LastName).ToList() : contacts.OrderByDescending(c => c.LastName).ToList(),
                "email" => request.IsAscending ? contacts.OrderBy(c => c.Email).ToList() : contacts.OrderByDescending(c => c.Email).ToList(),
                _ => contacts
            };

            var totalCount = contacts.Count;

            if (request.PageSize > 0)
                contacts = contacts.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return new PaginatedResponse<Contact>(contacts, totalCount);
        }
    }
}
