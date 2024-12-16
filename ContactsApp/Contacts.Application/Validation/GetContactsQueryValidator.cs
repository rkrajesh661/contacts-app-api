using Contacts.Application.Operations.Queries;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class GetContactsQueryValidator : AbstractValidator<GetContactsQuery>
    {
        public GetContactsQueryValidator() { }
    }
}
