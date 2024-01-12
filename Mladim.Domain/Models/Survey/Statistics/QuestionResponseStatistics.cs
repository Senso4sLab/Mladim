using Mladim.Domain.Models.Survey.Questions;

namespace Mladim.Domain.Models.Survey.Statistics;

public record QuestionResponseStatistics(SurveyQuestion SurveyQuestion, QuestionResponseTypes QuestionResponseTypes);
