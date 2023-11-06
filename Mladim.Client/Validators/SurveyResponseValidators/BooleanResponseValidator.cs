using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Enums;

namespace Mladim.Client.Validators.SurveyResponseValidators;

public class BooleanResponseValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is SurveyBooleanResponseType type && type is not SurveyBooleanResponseType.None)
            return null;
        else
            return new ValidationResult("Odgovor je obvezen", new[] { validationContext.MemberName!});       
    }
}
