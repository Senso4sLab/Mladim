using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class SurveyQuestionnairyConfigurationcs : IEntityTypeConfiguration<SurveyQuestionnairy>
{
    public void Configure(EntityTypeBuilder<SurveyQuestionnairy> builder)
    {
        builder.Navigation(sq => sq.Questions).AutoInclude();
        builder.HasData(SurveyQuestionnairy.Create(1));
    }
}






