using Contacts.Application.Operations.Commands;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThanOrEqualTo(1);
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName).NotEmpty();
            RuleFor(command =>  command.Email).NotEmpty();
        }
    }
}
