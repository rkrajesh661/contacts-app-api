using Contacts.Domain.Models;
using MediatR;

namespace Contacts.Application.Operations.Queries;

public record GetContactByIdQuery(int Id) : IRequest<Contact>;