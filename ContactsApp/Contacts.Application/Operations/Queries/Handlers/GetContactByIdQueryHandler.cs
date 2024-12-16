using Contacts.Application.Contract;
using Contacts.Domain.Models;
using MediatR;

namespace Contacts.Application.Operations.Queries.Handlers;

public class GetContactByIdQueryHandler(IContactService contactService) : IRequestHandler<GetContactByIdQuery, Contact>
{
    public async Task<Contact> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contacts = await contactService.GetAllContacts();
        var contact = contacts.FirstOrDefault(c => c.Id == request.Id);

        if (contact is null)
            throw new KeyNotFoundException($"Contact not found with id: {request.Id}");

        return contact;
    }
}
