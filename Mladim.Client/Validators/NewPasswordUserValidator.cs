using FluentValidation;

using Mladim.Domain.Models;

namespace Mladim.Client.Validators;

public class NewPasswordUserValidator : AbstractValidator<NewPasswordUser>
{
    public NewPasswordUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Neveljavna oblika email naslova");

        RuleFor(x => x.Password)
            .NotEmpty()            
            .WithMessage("Vnosno polje je obvezno");
       
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<NewPasswordUser>.CreateWithOptions((NewPasswordUser)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

