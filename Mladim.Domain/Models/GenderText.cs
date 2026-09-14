using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public record GenderText(string Female, string Male)
{
    public static GenderText SameText(string text) => new GenderText(text, text);
    public string GetByGender(Gender gender) =>
        gender == Gender.Male ? Male : Female;

}


