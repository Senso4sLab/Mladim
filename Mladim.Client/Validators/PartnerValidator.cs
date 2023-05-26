using FluentValidation;
using FluentValidation.Validators;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class PartnerValidator : AbstractValidator<PartnerVM>
{
    public PartnerValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Email)
             .EmailAddress()
            .WithMessage("Nepravilna oblika poštnega naslova");
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<PartnerVM>.CreateWithOptions((PartnerVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
