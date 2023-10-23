using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class seedSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questionnairies",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "SurveyQuestion",
                columns: new[] { "Id", "Category", "Discriminator", "Texts", "Type" },
                values: new object[,]
                {
                    { 1, 1, "FemaleSurveyQuestion", "[\"Po\\u010Dutila sem se varno in prijetno.\"]", 2 },
                    { 2, 1, "FemaleSurveyQuestion", "[\"Bila sem sli\\u0161ana in sprejeta.\"]", 2 },
                    { 3, 1, "FemaleSurveyQuestion", "[\"Sodelovala sem pri na\\u010Drtovanju ali izvedbi te aktivnosti/dogodka\"]", 1 },
                    { 4, 1, "FemaleSurveyQuestion", "[\"Spodbujena sem bila k aktivni udele\\u017Ebi.\"]", 1 },
                    { 5, 1, "FemaleSurveyQuestion", "[\"Z aktivnostjo sem bila zadovoljna.\"]", 2 },
                    { 6, 1, "FemaleSurveyQuestion", "[\"Z eno ali nekaj besedami opi\\u0161i, kaj si z udele\\u017Ebo pridobila.\"]", 4 },
                    { 7, 1, "FemaleSurveyQuestion", "[\"Ali si zaradi svojih telesnih zna\\u010Dilnosti, socialnega polo\\u017Eaja, narodnosti ali barve ko\\u017Ee v slab\\u0161em polo\\u017Eaju kot ve\\u010Dina ostalih?\"]", 1 },
                    { 8, 2, "FemaleSurveyQuestion", "[\"Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.\"]", 1 },
                    { 9, 2, "FemaleSurveyQuestion", "[\"Sodelovala sem pri oblikovanju ciljev skupine in skupinskega dela.\"]", 2 },
                    { 10, 2, "FemaleSurveyQuestion", "[\"Moja pri\\u010Dakovanja, ki sem jih imela od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)\"]", 1 },
                    { 11, 2, "FemaleSurveyQuestion", "[\"Zaradi sodelovanja v aktivnosti sem:\",\"bolj samozavestena\",\"bolj sposobna delati v skupini\",\"se je izbolj\\u0161al moj u\\u010Dni uspeh\",\"la\\u017Eje branim svoje mnenje\",\"verjamem, da je skupaj mogo\\u010De dose\\u010Di pomembne spremembe\"]", 3 },
                    { 12, 2, "FemaleSurveyQuestion", "[\"Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.\"]", 1 },
                    { 13, 2, "FemaleSurveyQuestion", "[\"Mentor je vzpostavil varen in vklju\\u010Dujo\\u010D prostor.\"]", 1 },
                    { 14, 2, "FemaleSurveyQuestion", "[\"Moja skupina se je redno sre\\u010Devala (vsaj dvakrat mese\\u010Dno).\"]", 1 },
                    { 15, 2, "FemaleSurveyQuestion", "[\"V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)\"]", 2 },
                    { 16, 1, "MaleSurveyQuestion", "[\"Po\\u010Dutil sem se varno in prijetno.\"]", 2 },
                    { 17, 1, "MaleSurveyQuestion", "[\"Bil sem sli\\u0161an in sprejet.\"]", 2 },
                    { 18, 1, "MaleSurveyQuestion", "[\"Sodeloval sem pri na\\u010Drtovanju ali izvedbi te aktivnosti/dogodka\"]", 1 },
                    { 19, 1, "MaleSurveyQuestion", "[\"Spodbujen sem bil k aktivni udele\\u017Ebi.\"]", 1 },
                    { 20, 1, "MaleSurveyQuestion", "[\"Z aktivnostjo sem bil zadovoljn.\"]", 2 },
                    { 21, 1, "MaleSurveyQuestion", "[\"Z eno ali nekaj besedami opi\\u0161i, kaj si z udele\\u017Ebo pridobil.\"]", 4 },
                    { 22, 1, "MaleSurveyQuestion", "[\"Ali si zaradi svojih telesnih zna\\u010Dilnosti, socialnega polo\\u017Eaja, narodnosti ali barve ko\\u017Ee v slab\\u0161em polo\\u017Eaju kot ve\\u010Dina ostalih?\"]", 1 },
                    { 23, 2, "MaleSurveyQuestion", "[\"Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.\"]", 1 },
                    { 24, 2, "MaleSurveyQuestion", "[\"Sodeloval sem pri oblikovanju ciljev skupine in skupinskega dela.\"]", 2 },
                    { 25, 2, "MaleSurveyQuestion", "[\"Moja pri\\u010Dakovanja, ki sem jih imel od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)\"]", 1 },
                    { 26, 2, "MaleSurveyQuestion", "[\"Zaradi sodelovanja v aktivnosti sem:\",\"bolj samozavesten\",\"bolj sposoben delati v skupini\",\"se je izbolj\\u0161al moj u\\u010Dni uspeh\",\"la\\u017Eje branim svoje mnenje\",\"verjamem, da je skupaj mogo\\u010De dose\\u010Di pomembne spremembe\"]", 3 },
                    { 27, 2, "MaleSurveyQuestion", "[\"Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.\"]", 1 },
                    { 28, 2, "MaleSurveyQuestion", "[\"Mentor je vzpostavil varen in vklju\\u010Dujo\\u010D prostor.\"]", 1 },
                    { 29, 2, "MaleSurveyQuestion", "[\"Moja skupina se je redno sre\\u010Devala (vsaj dvakrat mese\\u010Dno).\"]", 1 },
                    { 30, 2, "MaleSurveyQuestion", "[\"V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)\"]", 2 }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionSurveyQuestionnairy",
                columns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 30, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 20, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 23, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 24, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 25, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 26, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 27, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 28, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 29, 1 });

            migrationBuilder.DeleteData(
                table: "SurveyQuestionSurveyQuestionnairy",
                keyColumns: new[] { "QuestionsId", "SurveyQuestionnairiesId" },
                keyValues: new object[] { 30, 1 });

            migrationBuilder.DeleteData(
                table: "Questionnairies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SurveyQuestion",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
