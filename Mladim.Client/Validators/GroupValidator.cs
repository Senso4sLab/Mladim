using FluentValidation;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class GroupValidator : AbstractValidator<GroupVM>
{
	public GroupValidator()
	{
        RuleFor(x => x.FullName)
       .NotEmpty()
       .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");        
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<GroupVM>.CreateWithOptions((GroupVM)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
