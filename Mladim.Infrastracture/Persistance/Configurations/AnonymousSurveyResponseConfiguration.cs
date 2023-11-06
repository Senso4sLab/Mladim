using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models.Survey.Responses;
using System.Text.Json;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class AnonymousSurveyResponseConfiguration: IEntityTypeConfiguration<AnonymousSurveyResponse>
{
    public void Configure(EntityTypeBuilder<AnonymousSurveyResponse> builder)
    {
        builder.OwnsOne(sr => sr.AnonymousParticipant);
        //builder.OwnsMany(sr => sr.Responses, builder => builder.ToJson());
        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);

        builder.Property(sr => sr.Responses)
            .HasConversion(v => JsonSerializer.Serialize(v, options), 
                           s => JsonSerializer.Deserialize<List<QuestionResponse>>(s, options)!);
    }

}
