using Contacts.Application.Contract;
using MediatR;

namespace Contacts.Application.Operations.Commands.Handlers
{
    public class UpdateContactCommandHandler(IContactService contactService) : IRequestHandler<UpdateContactCommand>
    {
        public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contacts = await contactService.GetAllContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == request.Id);

            if (contact is null)
                throw new KeyNotFoundException($"Contact not found with id: {request.Id}");

            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.Email = request.Email;

            await contactService.UpdateContact(contact);
        }
    }
}
