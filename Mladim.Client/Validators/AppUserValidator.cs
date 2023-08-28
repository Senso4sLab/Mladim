using FluentValidation;
using Mladim.Client.ViewModels;
using Mladim.Domain.Models;

namespace Mladim.Client.Validators;

public class AppUserValidator : AbstractValidator<AppUserVM>
{
    public AppUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Nickname)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Email)
           .EmailAddress()
           .WithMessage("Nepravilna oblika poštnega naslova");
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AppUserVM>.CreateWithOptions((AppUserVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}


