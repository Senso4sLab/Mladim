using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class SurveyFemaleQuestionConfiguration : IEntityTypeConfiguration<FemaleSurveyQuestion>
{
    public void Configure(EntityTypeBuilder<FemaleSurveyQuestion> builder)
    {
        builder.HasData(SurveyQuestion.CreateFemaleQuestion(1, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Počutila sem se varno in prijetno."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(2, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Bila sem slišana in sprejeta."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(3, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Sodelovala sem pri načrtovanju ali izvedbi te aktivnosti/dogodka"));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(4, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Spodbujena sem bila k aktivni udeležbi."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(5, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Z aktivnostjo sem bila zadovoljna."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(6, SurveyQuestionType.Text, SurveyQuestionCategory.General)
            .AddText("Z eno ali nekaj besedami opiši, kaj si z udeležbo pridobila."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(7, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Ali si zaradi svojih telesnih značilnosti, socialnega položaja, narodnosti ali barve kože v slabšem položaju kot večina ostalih?"));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(8, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(9, SurveyQuestionType.Rating, SurveyQuestionCategory.Group)
            .AddText("Sodelovala sem pri oblikovanju ciljev skupine in skupinskega dela."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(10, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Moja pričakovanja, ki sem jih imela od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)"));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(11, SurveyQuestionType.Multiple, SurveyQuestionCategory.Group)
            .AddText("Zaradi sodelovanja v aktivnosti sem:")
            .AddText("bolj samozavestena")
            .AddText("bolj sposobna delati v skupini")
            .AddText("se je izboljšal moj učni uspeh")
            .AddText("lažje branim svoje mnenje")
            .AddText("verjamem, da je skupaj mogoče doseči pomembne spremembe"));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(12, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(13, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Mentor je vzpostavil varen in vključujoč prostor."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(14, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Moja skupina se je redno srečevala (vsaj dvakrat mesečno)."));

        builder.HasData(SurveyQuestion.CreateFemaleQuestion(15, SurveyQuestionType.Rating, SurveyQuestionCategory.Group)
            .AddText("V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)"));      

    }
}

