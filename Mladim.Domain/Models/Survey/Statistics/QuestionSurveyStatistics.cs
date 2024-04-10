using Mladim.Domain.Models.Survey.Questions;

namespace Mladim.Domain.Models.Survey.Statistics;

public record QuestionSurveyStatistics(SurveyQuestion SurveyQuestion, SurveyStatistics QuestionResponseTypes);
