using Contacts.Application.Operations.Commands;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class CreateContactCommandValidator: AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName).NotEmpty();
            RuleFor(command =>  command.Email).NotEmpty().EmailAddress();
        }
    }
}
