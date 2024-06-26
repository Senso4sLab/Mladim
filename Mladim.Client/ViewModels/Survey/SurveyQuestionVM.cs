﻿using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionVM
{    
    public int UniqueQuestionId { get; set; }
    public List<string> Texts { get; set; } = new();    
    public SurveyQuestionType Type { get; set; }    

    public QuestionResponseVM CreateSurveyResponse() => Type switch
    {
        SurveyQuestionType.Boolean => new QuestionBooleanResponseVM(UniqueQuestionId),
        SurveyQuestionType.Text => new QuestionTextResponseVM(UniqueQuestionId),
        SurveyQuestionType.Rating => new QuestionRatingResponseVM(UniqueQuestionId),
        SurveyQuestionType.Multiple => new QuestionMultiButtonResponseVM(UniqueQuestionId),
        SurveyQuestionType.MultipleRepetitive => new QuestionMultiRepetitiveButtonResponseVM(UniqueQuestionId),
        _ => throw new NotImplementedException(),
    };


    public IEnumerable<string> QuestionTexts =>
        Texts.Count > 1 ? Texts.Skip(1)
                       .Select(text => $"{Texts.First()} {text}")
                       .ToList() : Texts;   
    
}




