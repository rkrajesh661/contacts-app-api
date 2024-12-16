using Contacts.Application.Operations.Commands;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThanOrEqualTo(1);
        }
    }
}
