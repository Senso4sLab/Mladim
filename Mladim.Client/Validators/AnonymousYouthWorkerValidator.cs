using FluentValidation;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class AnonymousYouthWorkerValidator : AbstractValidator<AnonymousYouthWorkerVM>
{
    public AnonymousYouthWorkerValidator()
    {
        RuleFor(a => a.AgeGroup)
         .IsInEnum()
         .WithMessage("Vnosno polje je obvezno");

        RuleFor(a => a.Gender)
           .IsInEnum()
           .WithMessage("Vnosno polje je obvezno");

        RuleFor(a => a.Role)
          .IsInEnum()
          .WithMessage("Vnosno polje je obvezno");


        RuleFor(a => a.YearsOfExperience)
          .IsInEnum()
          .WithMessage("Vnosno polje je obvezno");


    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AnonymousYouthWorkerVM>.CreateWithOptions((AnonymousYouthWorkerVM)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

