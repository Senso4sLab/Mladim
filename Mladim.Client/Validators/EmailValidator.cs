using FluentValidation;
using Mladim.Domain.Models;

namespace Mladim.Client.Validators;

public class EmailValidator : AbstractValidator<UserEmail>
{
    public EmailValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Neveljavna oblika email naslova");       
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UserEmail>.CreateWithOptions((UserEmail)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

