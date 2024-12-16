using MediatR;

namespace Contacts.Application.Operations.Commands;

public record DeleteContactCommand(int Id) : IRequest;
