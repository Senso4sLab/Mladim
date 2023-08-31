using FluentValidation;

using Mladim.Domain.Models;

namespace Mladim.Client.Validators;



public class UrlRegistrationValidator : AbstractValidator<UrlRegistration>
{
    public UrlRegistrationValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Neveljavna oblika email naslova");

        RuleFor(x => x.Password)
           .NotEmpty()
           .NotNull()
           .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.ConfirmPassword)
         .NotEmpty()
         .NotNull()
         .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Gesli se ne ujemata");
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UrlRegistration>.CreateWithOptions((UrlRegistration)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

