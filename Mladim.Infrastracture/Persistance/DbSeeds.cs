using Microsoft.EntityFrameworkCore;

namespace Mladim.Infrastracture.Persistance;

public class DbSeeds
{
    public static void GeneratedSeeds(ModelBuilder modelBuilder)
    {
        var surveyQuestionQuestionnairyTable = "SurveyQuestionSurveyQuestionnairy";
        var surveyQuestionId = "QuestionsId";
        var surveyQuestionnairyId = "SurveyQuestionnairiesId";

        foreach (var index in Enumerable.Range(1, 30))
        {
            modelBuilder.Entity(surveyQuestionQuestionnairyTable).HasData(
                new Dictionary<string, object>
                {
                    [surveyQuestionId] = index,
                    [surveyQuestionnairyId] = 1, 
                });
        }

        
    }
}

