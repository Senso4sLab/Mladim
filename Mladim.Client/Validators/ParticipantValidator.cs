using FluentValidation;
using Mladim.Client.ViewModels;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

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

		RuleFor(x => x.Age)
			.GreaterThan(0)
			.WithMessage("Nepravilni vnos");


		RuleFor(x => x.Gender)
			.IsInEnum()	
			.WithMessage("Vnosno polje je obvezno");


        
    }

    private bool IsGenderSelected(Gender gender) =>
        gender > 0;


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ParticipantVM>.CreateWithOptions((ParticipantVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}


  

