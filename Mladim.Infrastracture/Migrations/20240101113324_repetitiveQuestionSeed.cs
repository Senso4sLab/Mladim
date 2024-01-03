using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class repetitiveQuestionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Category", "Discriminator", "Texts", "Type", "UniqueQuestionId" },
                values: new object[,]
                {
                    { 31, 4, "FemaleSurveyQuestion", "[\"V kolik\\u0161ni meri si zaradi udele\\u017Ebe okrepila naslednje sposobnosti:\",\"Sposobna sem se uspe\\u0161no sporazumevati in povezovati z drugimi.\",\"Sposobna sem ustrezno uporabljati razli\\u010Dne jezike za sporazumevanje z drugimi.\",\"Sposobna sem uporabljati matemati\\u010Dno znanje za re\\u0161evanje vsakodnevnih izzivov.\",\"Sposobna sem kompetentno uporabljati digitalna orodja pri delu, u\\u010Denju in stikih z drugimi.\",\"Sposobna sem ohranjati dobro psihi\\u010Dno in fizi\\u010Dno po\\u010Dutje ter dobre stike z drugimi.\",\"Sposobna sem oceniti svoje \\u0161ibke to\\u010Dke ter tudi pridobiti novo znanje, s katerim jih nadomestim.\",\"Sposobna sem delovati kot odgovoren dr\\u017Eavljan in se polno udele\\u017Eevati v dru\\u017Ebeno in politi\\u010Dno \\u017Eivljenje.\",\"Sposobna sem delovati podjetno in izkoristiti prilo\\u017Enosti, ki se mi ponujajo.\",\"Odprta sem do razli\\u010Dnih kultur in njihovih obi\\u010Dajev ter jih tudi spo\\u0161tujem.\"]", 5, 16 },
                    { 32, 4, "MaleSurveyQuestion", "[\"V kolik\\u0161ni meri si zaradi udele\\u017Ebe okrepil naslednje sposobnosti:\",\"Sposoben sem se uspe\\u0161no sporazumevati in povezovati z drugimi.\",\"Sposoben sem ustrezno uporabljati razli\\u010Dne jezike za sporazumevanje z drugimi.\",\"Sposoben sem uporabljati matemati\\u010Dno znanje za re\\u0161evanje vsakodnevnih izzivov.\",\"Sposoben sem kompetentno uporabljati digitalna orodja pri delu, u\\u010Denju in stikih z drugimi.\",\"Sposoben sem ohranjati dobro psihi\\u010Dno in fizi\\u010Dno po\\u010Dutje ter dobre stike z drugimi.\",\"Sposoben sem oceniti svoje \\u0161ibke to\\u010Dke ter tudi pridobiti novo znanje, s katerim jih nadomestim.\",\"Sposoben sem delovati kot odgovoren dr\\u017Eavljan in se polno udele\\u017Eevati v dru\\u017Ebeno in politi\\u010Dno \\u017Eivljenje.\",\"Sposoben sem delovati podjetno in izkoristiti prilo\\u017Enosti, ki se mi ponujajo.\",\"Odprt sem do razli\\u010Dnih kultur in njihovih obi\\u010Dajev ter jih tudi spo\\u0161tujem.\"]", 5, 16 }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionSurveyQuestionnairy",
                columns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                values: new object[,]
                {
                    { 31, 1 },
                    { 32, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 31, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 32, 1 });

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 32);
        }
    }
}
