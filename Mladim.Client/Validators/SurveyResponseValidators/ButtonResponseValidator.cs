using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Enums;

namespace Mladim.Client.Validators.SurveyResponseValidators;

public class ButtonResponseValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is SurveyButtonResponseType type)
            return null;
        else
            return new ValidationResult("Odgovor je obvezen", new[] { validationContext.MemberName! });
    }
}
