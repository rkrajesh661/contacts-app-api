using Contacts.Domain.Models;
using MediatR;
using Shared.Response;

namespace Contacts.Application.Operations.Queries;

public record GetContactsQuery(int PageNumber, int PageSize, string? SearchTerm, string? SortBy, bool IsAscending) : IRequest<PaginatedResponse<Contact>>;
