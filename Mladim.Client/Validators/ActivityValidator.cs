using FluentValidation;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class ActivityValidator : AbstractValidator<ActivityVM>
{
    public ActivityValidator()
    {
        RuleFor(x => x.Attributes.Name)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        //RuleFor(x => x.Attributes.Description)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .NotNull()
            .NotEqual(_ => default(TimeSpan))
            .WithMessage("Nepravilni vnos");


        RuleFor(x => x.EndTime)
            .NotEmpty()
            .NotNull()
            .NotEqual(_ => default(TimeSpan))
            .GreaterThan(x => x.StartTime)
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Attributes.NumOfRepetitions)
            .Must((activity, _, _) =>
            {
                if (activity.Attributes.IsRepetitive && activity.Attributes.NumOfRepetitions <= 1)
                    return false;
                return true;
            })
            .WithMessage("Nepravilni vnos");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ActivityVM>.CreateWithOptions((ActivityVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };

}
