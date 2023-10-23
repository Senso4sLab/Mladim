using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Mladim.Infrastracture.Persistance.Conversions;

public class SurveyMultipleResponseTypeConverter : ValueConverter<List<SurveyMultipleResponseType>, string>
{
    public SurveyMultipleResponseTypeConverter() : base(v => Serialize(v), v => DeSerialize(v))        
    {
        
    }

    private static string Serialize(List<SurveyMultipleResponseType> surveyMultipleResponseTypes)
    {
        return JsonSerializer.Serialize(surveyMultipleResponseTypes) ?? string.Empty;
    }

    private static List<SurveyMultipleResponseType> DeSerialize(string json)
    {
        return JsonSerializer.Deserialize<List<SurveyMultipleResponseType>>(json) ?? new List<SurveyMultipleResponseType>();
    }    
}
