using FluentValidation;
using Mladim.Client.ViewModels;
using Mladim.Domain.Enums;

namespace Mladim.Client.Validators;

public class StaffMemberValidator : AbstractValidator<StaffMemberVM>
{
	public StaffMemberValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Vnosno polje je obvezno");

		RuleFor(x => x.Surname)
			.NotEmpty()
			.WithMessage("Vnosno polje je obvezno");

		RuleFor(x => x.Email)
			.NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Email)
			.EmailAddress()
			.WithMessage("Nepravilni vnos email naslova");

        RuleFor(x => x.Gender)
              .IsInEnum()
              .WithMessage("Vnosno polje je obvezno");

		RuleFor(x => x.YearOfBirth)
			.Must(ValidBirthYear)
			.WithMessage("Nepravilni vnos letnice rojstva");
    }


	

	private bool ValidBirthYear(int? userBirthYear)
	{
		if (userBirthYear == null)
			return true;

		var year = DateTime.Now.Year;

		if (year - userBirthYear < 0 || year - userBirthYear > 100)
			return false;		

		return true;		
	}

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<StaffMemberVM>.CreateWithOptions((StaffMemberVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
