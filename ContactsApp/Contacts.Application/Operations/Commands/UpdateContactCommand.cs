using MediatR;

namespace Contacts.Application.Operations.Commands;

public record UpdateContactCommand(int Id, string FirstName, string LastName, string Email) : IRequest;
