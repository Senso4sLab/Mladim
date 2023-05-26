using FluentValidation;
using Mladim.Client.ViewModels;

namespace Mladim.Client.Validators;

public class ProjectValidator : AbstractValidator<ProjectVM>
{
	public ProjectValidator()
	{
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Vnosno polje je obvezno");

        RuleFor(x => x.Start)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");       

        RuleFor(x => x.End)
           .NotEmpty()
           .WithMessage("Vnosno polje je obvezno");
    }
}
