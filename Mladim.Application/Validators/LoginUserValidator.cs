using FluentValidation;
using Mladim.Application.Contracts.Identity;
using Mladim.Domain.Models;

namespace Mladim.Application.Validators;

public class LoginUserValidator : AbstractValidator<LoginUser>
{    
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)            
            .NotEmpty()                 
            .EmailAddress()
            .WithMessage("Neveljavna oblika email naslova");

        RuleFor(x => x.Password)
            .NotEmpty()           
            .WithMessage("Vnosno polje je obvezno"); 
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginUser>.CreateWithOptions((LoginUser)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
