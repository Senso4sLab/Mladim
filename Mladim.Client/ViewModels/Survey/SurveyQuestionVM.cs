using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionVM
{    
    public int UniqueQuestionId { get; set; }
    public string? Header { get; set; } = default!;
    public List<string> Texts { get; set; } = new();    
    public SurveyQuestionType Type { get; set; }

    public SurveyQuestionVM Clone() => 
        new SurveyQuestionVM()
        {
            UniqueQuestionId = UniqueQuestionId,
            Header = Header,
            Texts = new List<string>(Texts),
            Type = Type,
        };
    
        
  

}







