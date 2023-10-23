using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Conversions;



public class SurveyQuestionConverter : ValueConverter<List<string>, string>
{
    public SurveyQuestionConverter() : base(v => Serialize(v), v => DeSerialize(v))
    {

    }

    private static string Serialize(List<string> questions)
    {
        return JsonSerializer.Serialize(questions) ?? string.Empty;
    }

    private static List<string> DeSerialize(string json)
    {
        return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }
}

