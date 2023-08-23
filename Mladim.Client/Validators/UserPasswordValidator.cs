using FluentValidation;

using Mladim.Domain.Models;

namespace Mladim.Client.Validators;

public class UserPasswordValidator : AbstractValidator<UserPassword>
{
    public UserPasswordValidator()
    {
        RuleFor(x => x.Password)
           .NotEmpty()
           .NotNull()
           .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Password)
            .NotEqual(x => x.ConfirmPassword)
            .WithMessage("Gesli se ne ujemata");
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UserPassword>.CreateWithOptions((UserPassword)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

