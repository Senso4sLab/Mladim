using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models.Survey.Questions;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
             .ValueGeneratedOnAdd();

        builder.OwnsOne(q => q.Header, text =>
        {
            text.Property(t => t.Female)
                .HasColumnName("Text_Female")
                .IsRequired();

            text.Property(t => t.Male)
                .HasColumnName("Text_Male")
                .IsRequired();
        });


        builder.OwnsMany(q => q.Questions, sub =>
        {
            sub.ToTable("SurveyQuestionSubQuestions");

            // ensure dependent table has FK to owner
            sub.WithOwner().HasForeignKey("SurveyQuestionId");

            // shadow Id so dependent rows have a PK (EF requires a key for separate table)
            sub.Property<int>("Id").ValueGeneratedOnAdd();
            sub.HasKey("Id");

            // mapped properties of the owned type
            sub.Property(s => s.Female)
               .HasColumnName("Text_Female")
               .IsRequired();

            sub.Property(s => s.Male)
               .HasColumnName("Text_Male")
               .IsRequired();
        });

    }
}




