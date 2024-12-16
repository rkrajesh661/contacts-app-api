using Contacts.Application.Operations.Queries;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class GetContactByIdQueryValidator : AbstractValidator<GetContactByIdQuery>
    {
        public GetContactByIdQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThanOrEqualTo(1);
        }
    }
}
