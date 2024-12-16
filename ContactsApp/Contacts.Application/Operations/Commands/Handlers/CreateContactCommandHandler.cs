using Contacts.Application.Contract;
using Contacts.Domain.Models;
using MediatR;

namespace Contacts.Application.Operations.Commands.Handlers;

public class CreateContactCommandHandler(IContactService contactService) : IRequestHandler<CreateContactCommand, int>
{
    public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var newContact = new Contact
        {
            Id = await contactService.GetNextContactId(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        await contactService.CreateContact(newContact);
        return newContact.Id;
    }
}
