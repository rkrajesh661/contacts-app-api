using MediatR;

namespace Contacts.Application.Operations.Commands;

public record CreateContactCommand(string FirstName, string LastName, string Email) : IRequest<int>;
