using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mladim.Domain.Enums;

namespace Mladim.Infrastracture.Persistance.Configurations;


public class SurveyMaleQuestionConfiguration : IEntityTypeConfiguration<MaleSurveyQuestion>
{
    public void Configure(EntityTypeBuilder<MaleSurveyQuestion> builder)
    {

        builder.HasData(SurveyQuestion.CreateMaleQuestion(16, 1, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Počutil sem se varno in prijetno."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(17, 2, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Bil sem slišan in sprejet."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(18, 3, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Sodeloval sem pri načrtovanju ali izvedbi te aktivnosti/dogodka"));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(19, 4, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Spodbujen sem bil k aktivni udeležbi."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(20, 5, SurveyQuestionType.Rating, SurveyQuestionCategory.General)
            .AddText("Z aktivnostjo sem bil zadovoljn."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(21, 6, SurveyQuestionType.Text, SurveyQuestionCategory.General)
            .AddText("Z eno ali nekaj besedami opiši, kaj si z udeležbo pridobil."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(22, 7, SurveyQuestionType.Boolean, SurveyQuestionCategory.General)
            .AddText("Ali si zaradi svojih telesnih značilnosti, socialnega položaja, narodnosti ali barve kože v slabšem položaju kot večina ostalih?"));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(23, 8, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(24, 9, SurveyQuestionType.Rating, SurveyQuestionCategory.Group)
            .AddText("Sodeloval sem pri oblikovanju ciljev skupine in skupinskega dela."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(25, 10, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Moja pričakovanja, ki sem jih imel od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)"));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(26, 11, SurveyQuestionType.Multiple, SurveyQuestionCategory.Group)
            .AddText("Zaradi sodelovanja v aktivnosti sem:")
            .AddText("bolj samozavesten")
            .AddText("bolj sposoben delati v skupini")
            .AddText("se je izboljšal moj učni uspeh")
            .AddText("lažje branim svoje mnenje")
            .AddText("verjamem, da je skupaj mogoče doseči pomembne spremembe"));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(27, 12, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(28, 13, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Mentor je vzpostavil varen in vključujoč prostor."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(29, 14, SurveyQuestionType.Boolean, SurveyQuestionCategory.Group)
            .AddText("Moja skupina se je redno srečevala (vsaj dvakrat mesečno)."));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(30, 15, SurveyQuestionType.Rating, SurveyQuestionCategory.Group)
            .AddText("V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)"));

        builder.HasData(SurveyQuestion.CreateMaleQuestion(32, 16, SurveyQuestionType.MultipleRepetitive, SurveyQuestionCategory.Repetitive)
           .AddText("V kolikšni meri si zaradi udeležbe okrepil naslednje sposobnosti:")
           .AddText("Sposoben sem se uspešno sporazumevati in povezovati z drugimi.")
           .AddText("Sposoben sem ustrezno uporabljati različne jezike za sporazumevanje z drugimi.")
           .AddText("Sposoben sem uporabljati matematično znanje za reševanje vsakodnevnih izzivov.")
           .AddText("Sposoben sem kompetentno uporabljati digitalna orodja pri delu, učenju in stikih z drugimi.")
           .AddText("Sposoben sem ohranjati dobro psihično in fizično počutje ter dobre stike z drugimi.")
           .AddText("Sposoben sem oceniti svoje šibke točke ter tudi pridobiti novo znanje, s katerim jih nadomestim.")
           .AddText("Sposoben sem delovati kot odgovoren državljan in se polno udeleževati v družbeno in politično življenje.")
           .AddText("Sposoben sem delovati podjetno in izkoristiti priložnosti, ki se mi ponujajo.")
           .AddText("Odprt sem do različnih kultur in njihovih običajev ter jih tudi spoštujem."));

    }
}

