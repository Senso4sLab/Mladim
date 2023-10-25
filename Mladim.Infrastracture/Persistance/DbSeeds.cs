using Microsoft.EntityFrameworkCore;

namespace Mladim.Infrastracture.Persistance;

public class DbSeeds
{
    public static void GeneratedSeeds(ModelBuilder modelBuilder)
    {
        var femaleSurveyQuestionQuestionnairyTable = "FemaleSurveyQuestionSurveyQuestionnairy";
        var femaleSurveyQuestionId = "QuestionsId";
        var femaleSurveyQuestionnairyId = "SurveyQuestionnairiesId";

        foreach (var index in Enumerable.Range(1, 15))
        {
            modelBuilder.Entity(femaleSurveyQuestionQuestionnairyTable).HasData(
                new Dictionary<string, object>
                {
                    [femaleSurveyQuestionId] = index,
                    [femaleSurveyQuestionnairyId] = 1,
                });
        }

        var maleSurveyQuestionQuestionnairyTable = "FemaleSurveyQuestionSurveyQuestionnairy";
        var maleSurveyQuestionId = "QuestionsId";
        var maleSurveyQuestionnairyId = "SurveyQuestionnairiesId";

        foreach (var index in Enumerable.Range(1, 15))
        {
            modelBuilder.Entity(maleSurveyQuestionQuestionnairyTable).HasData(
                new Dictionary<string, object>
                {
                    [maleSurveyQuestionId] = index,
                    [maleSurveyQuestionnairyId] = 1,
                });
        }
    }
}

