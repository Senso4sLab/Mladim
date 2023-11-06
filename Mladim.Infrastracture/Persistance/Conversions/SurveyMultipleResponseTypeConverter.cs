using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Mladim.Infrastracture.Persistance.Conversions;

public class SurveyMultipleResponseTypeConverter : ValueConverter<List<SurveyButtonResponseType>, string>
{
    public SurveyMultipleResponseTypeConverter() : base(v => Serialize(v), v => DeSerialize(v))        
    {
        
    }

    private static string Serialize(List<SurveyButtonResponseType> surveyMultipleResponseTypes)
    {
        return JsonSerializer.Serialize(surveyMultipleResponseTypes) ?? string.Empty;
    }

    private static List<SurveyButtonResponseType> DeSerialize(string json)
    {
        return JsonSerializer.Deserialize<List<SurveyButtonResponseType>>(json) ?? new List<SurveyButtonResponseType>();
    }    
}
