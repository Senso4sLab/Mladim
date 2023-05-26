using FluentValidation;
using Mladim.Client.ViewModels;


namespace Mladim.Client.Validators;

public class ParticipantValidator : AbstractValidator<ParticipantVM>
{
	public ParticipantValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Vnosno polje je obvezno");

		RuleFor(x => x.Surname)
			.NotEmpty()
			.WithMessage("Vnosno polje je obvezno");

		RuleFor(x => x.Year)
			.GreaterThan(0)
			.WithMessage("Nepravilni vnos");
	}
	

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ParticipantVM>.CreateWithOptions((ParticipantVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
