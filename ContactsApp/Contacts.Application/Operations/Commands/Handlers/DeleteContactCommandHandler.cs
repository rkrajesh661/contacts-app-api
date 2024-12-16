using Contacts.Application.Contract;
using MediatR;

namespace Contacts.Application.Operations.Commands.Handlers
{
    public class DeleteContactCommandHandler(IContactService contactService) : IRequestHandler<DeleteContactCommand>
    {
        public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            await contactService.DeleteContact(request.Id);
        }
    }
}
