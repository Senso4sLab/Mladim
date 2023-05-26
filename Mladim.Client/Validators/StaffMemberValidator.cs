using FluentValidation;
using Mladim.Client.ViewModels;


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

		RuleFor(x => x.Year)
			.Must(ValidBirthYear)
			.WithMessage("Nepravilna letnica rojstva");
	}

	private bool ValidBirthYear(int userBirthYear)
	{
		var year = DateTime.Now.Year;

		if (year - userBirthYear < 0)
			return false;

		if (year - userBirthYear > 100)
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
