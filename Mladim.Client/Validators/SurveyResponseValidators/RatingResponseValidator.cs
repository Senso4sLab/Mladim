using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Enums;

namespace Mladim.Client.Validators.SurveyResponseValidators;

public class RatingResponseValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is SurveyRatingResponseType type && type is not SurveyRatingResponseType.None)
            return null;
        else
            return new ValidationResult("Odgovor je obvezen", new[] { validationContext.MemberName! });
    }
}
