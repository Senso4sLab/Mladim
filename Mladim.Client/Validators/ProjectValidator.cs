using FluentValidation;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class ProjectValidator : AbstractValidator<ProjectVM>
{
	public ProjectValidator()
	{
        RuleFor(x => x.Attributes.Name)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Attributes.Description)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.DateRange.Start)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");       

        RuleFor(x => x.DateRange.End)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");
    }
}
