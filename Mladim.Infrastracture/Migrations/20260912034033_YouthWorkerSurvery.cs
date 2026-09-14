using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class YouthWorkerSurvery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Questionnairies_SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "SurveyQuestionSurveyQuestionnairy");

            migrationBuilder.DropTable(
                name: "Questionnairies");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Texts",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UniqueQuestionId",
                table: "Questions",
                newName: "TargetGroup");

            migrationBuilder.AddColumn<string>(
                name: "Text_Female",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text_Male",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_Id",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_Gender",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_AgeGroup",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AnonymousYouthWorker_AgeGroup",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnonymousYouthWorker_Gender",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnonymousYouthWorker_Role",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnonymousYouthWorker_YearsOfExperience",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attributes_ActivityTargetGroup",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "SurveyQuestionSubQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text_Female = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text_Male = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionSubQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionSubQuestions_Questions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionSubQuestions_SurveyQuestionId",
                table: "SurveyQuestionSubQuestions",
                column: "SurveyQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyQuestionSubQuestions");

            migrationBuilder.DropColumn(
                name: "Text_Female",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Text_Male",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnonymousYouthWorker_AgeGroup",
                table: "AnonymousSurveyResponse");

            migrationBuilder.DropColumn(
                name: "AnonymousYouthWorker_Gender",
                table: "AnonymousSurveyResponse");

            migrationBuilder.DropColumn(
                name: "AnonymousYouthWorker_Role",
                table: "AnonymousSurveyResponse");

            migrationBuilder.DropColumn(
                name: "AnonymousYouthWorker_YearsOfExperience",
                table: "AnonymousSurveyResponse");

            migrationBuilder.DropColumn(
                name: "Attributes_ActivityTargetGroup",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "TargetGroup",
                table: "Questions",
                newName: "UniqueQuestionId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Texts",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_Id",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_Gender",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnonymousParticipant_AgeGroup",
                table: "AnonymousSurveyResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurveyQuestionnairyId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Questionnairies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnairies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionSurveyQuestionnairy",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionnairiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionSurveyQuestionnairy", x => new { x.QuestionsId, x.SurveyQuestionnairiesId });
                    table.ForeignKey(
                        name: "FK_SurveyQuestionSurveyQuestionnairy_Questionnairies_SurveyQuestionnairiesId",
                        column: x => x.SurveyQuestionnairiesId,
                        principalTable: "Questionnairies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionSurveyQuestionnairy_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questionnairies",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Category", "Discriminator", "Texts", "Type", "UniqueQuestionId" },
                values: new object[,]
                {
                    { 1, 1, "FemaleSurveyQuestion", "[\"Po\\u010Dutila sem se varno in prijetno.\"]", 2, 1 },
                    { 2, 1, "FemaleSurveyQuestion", "[\"Bila sem sli\\u0161ana in sprejeta.\"]", 2, 2 },
                    { 3, 1, "FemaleSurveyQuestion", "[\"Sodelovala sem pri na\\u010Drtovanju ali izvedbi te aktivnosti/dogodka\"]", 1, 3 },
                    { 4, 1, "FemaleSurveyQuestion", "[\"Spodbujena sem bila k aktivni udele\\u017Ebi.\"]", 1, 4 },
                    { 5, 1, "FemaleSurveyQuestion", "[\"Z aktivnostjo sem bila zadovoljna.\"]", 2, 5 },
                    { 6, 1, "FemaleSurveyQuestion", "[\"Z eno ali nekaj besedami opi\\u0161i, kaj si z udele\\u017Ebo pridobila.\"]", 4, 6 },
                    { 7, 1, "FemaleSurveyQuestion", "[\"Ali si zaradi svojih telesnih zna\\u010Dilnosti, socialnega polo\\u017Eaja, narodnosti ali barve ko\\u017Ee v slab\\u0161em polo\\u017Eaju kot ve\\u010Dina ostalih?\"]", 1, 7 },
                    { 8, 2, "FemaleSurveyQuestion", "[\"Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.\"]", 1, 8 },
                    { 9, 2, "FemaleSurveyQuestion", "[\"Sodelovala sem pri oblikovanju ciljev skupine in skupinskega dela.\"]", 2, 9 },
                    { 10, 2, "FemaleSurveyQuestion", "[\"Moja pri\\u010Dakovanja, ki sem jih imela od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)\"]", 1, 10 },
                    { 11, 2, "FemaleSurveyQuestion", "[\"Zaradi sodelovanja v aktivnosti sem:\",\"bolj samozavestena\",\"bolj sposobna delati v skupini\",\"se je izbolj\\u0161al moj u\\u010Dni uspeh\",\"la\\u017Eje branim svoje mnenje\",\"verjamem, da je skupaj mogo\\u010De dose\\u010Di pomembne spremembe\"]", 3, 11 },
                    { 12, 2, "FemaleSurveyQuestion", "[\"Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.\"]", 1, 12 },
                    { 13, 2, "FemaleSurveyQuestion", "[\"Mentor je vzpostavil varen in vklju\\u010Dujo\\u010D prostor.\"]", 1, 13 },
                    { 14, 2, "FemaleSurveyQuestion", "[\"Moja skupina se je redno sre\\u010Devala (vsaj dvakrat mese\\u010Dno).\"]", 1, 14 },
                    { 15, 2, "FemaleSurveyQuestion", "[\"V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)\"]", 2, 15 },
                    { 16, 1, "MaleSurveyQuestion", "[\"Po\\u010Dutil sem se varno in prijetno.\"]", 2, 1 },
                    { 17, 1, "MaleSurveyQuestion", "[\"Bil sem sli\\u0161an in sprejet.\"]", 2, 2 },
                    { 18, 1, "MaleSurveyQuestion", "[\"Sodeloval sem pri na\\u010Drtovanju ali izvedbi te aktivnosti/dogodka\"]", 1, 3 },
                    { 19, 1, "MaleSurveyQuestion", "[\"Spodbujen sem bil k aktivni udele\\u017Ebi.\"]", 1, 4 },
                    { 20, 1, "MaleSurveyQuestion", "[\"Z aktivnostjo sem bil zadovoljn.\"]", 2, 5 },
                    { 21, 1, "MaleSurveyQuestion", "[\"Z eno ali nekaj besedami opi\\u0161i, kaj si z udele\\u017Ebo pridobil.\"]", 4, 6 },
                    { 22, 1, "MaleSurveyQuestion", "[\"Ali si zaradi svojih telesnih zna\\u010Dilnosti, socialnega polo\\u017Eaja, narodnosti ali barve ko\\u017Ee v slab\\u0161em polo\\u017Eaju kot ve\\u010Dina ostalih?\"]", 1, 7 },
                    { 23, 2, "MaleSurveyQuestion", "[\"Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.\"]", 1, 8 },
                    { 24, 2, "MaleSurveyQuestion", "[\"Sodeloval sem pri oblikovanju ciljev skupine in skupinskega dela.\"]", 2, 9 },
                    { 25, 2, "MaleSurveyQuestion", "[\"Moja pri\\u010Dakovanja, ki sem jih imel od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)\"]", 1, 10 },
                    { 26, 2, "MaleSurveyQuestion", "[\"Zaradi sodelovanja v aktivnosti sem:\",\"bolj samozavesten\",\"bolj sposoben delati v skupini\",\"se je izbolj\\u0161al moj u\\u010Dni uspeh\",\"la\\u017Eje branim svoje mnenje\",\"verjamem, da je skupaj mogo\\u010De dose\\u010Di pomembne spremembe\"]", 3, 11 },
                    { 27, 2, "MaleSurveyQuestion", "[\"Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.\"]", 1, 12 },
                    { 28, 2, "MaleSurveyQuestion", "[\"Mentor je vzpostavil varen in vklju\\u010Dujo\\u010D prostor.\"]", 1, 13 },
                    { 29, 2, "MaleSurveyQuestion", "[\"Moja skupina se je redno sre\\u010Devala (vsaj dvakrat mese\\u010Dno).\"]", 1, 14 },
                    { 30, 2, "MaleSurveyQuestion", "[\"V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)\"]", 2, 15 },
                    { 31, 4, "FemaleSurveyQuestion", "[\"V kolik\\u0161ni meri si zaradi udele\\u017Ebe okrepila naslednje sposobnosti:\",\"Sposobna sem se uspe\\u0161no sporazumevati in povezovati z drugimi.\",\"Sposobna sem ustrezno uporabljati razli\\u010Dne jezike za sporazumevanje z drugimi.\",\"Sposobna sem uporabljati matemati\\u010Dno znanje za re\\u0161evanje vsakodnevnih izzivov.\",\"Sposobna sem kompetentno uporabljati digitalna orodja pri delu, u\\u010Denju in stikih z drugimi.\",\"Sposobna sem ohranjati dobro psihi\\u010Dno in fizi\\u010Dno po\\u010Dutje ter dobre stike z drugimi.\",\"Sposobna sem oceniti svoje \\u0161ibke to\\u010Dke ter tudi pridobiti novo znanje, s katerim jih nadomestim.\",\"Sposobna sem delovati kot odgovoren dr\\u017Eavljan in se polno udele\\u017Eevati v dru\\u017Ebeno in politi\\u010Dno \\u017Eivljenje.\",\"Sposobna sem delovati podjetno in izkoristiti prilo\\u017Enosti, ki se mi ponujajo.\",\"Odprta sem do razli\\u010Dnih kultur in njihovih obi\\u010Dajev ter jih tudi spo\\u0161tujem.\"]", 5, 16 },
                    { 32, 4, "MaleSurveyQuestion", "[\"V kolik\\u0161ni meri si zaradi udele\\u017Ebe okrepil naslednje sposobnosti:\",\"Sposoben sem se uspe\\u0161no sporazumevati in povezovati z drugimi.\",\"Sposoben sem ustrezno uporabljati razli\\u010Dne jezike za sporazumevanje z drugimi.\",\"Sposoben sem uporabljati matemati\\u010Dno znanje za re\\u0161evanje vsakodnevnih izzivov.\",\"Sposoben sem kompetentno uporabljati digitalna orodja pri delu, u\\u010Denju in stikih z drugimi.\",\"Sposoben sem ohranjati dobro psihi\\u010Dno in fizi\\u010Dno po\\u010Dutje ter dobre stike z drugimi.\",\"Sposoben sem oceniti svoje \\u0161ibke to\\u010Dke ter tudi pridobiti novo znanje, s katerim jih nadomestim.\",\"Sposoben sem delovati kot odgovoren dr\\u017Eavljan in se polno udele\\u017Eevati v dru\\u017Ebeno in politi\\u010Dno \\u017Eivljenje.\",\"Sposoben sem delovati podjetno in izkoristiti prilo\\u017Enosti, ki se mi ponujajo.\",\"Odprt sem do razli\\u010Dnih kultur in njihovih obi\\u010Dajev ter jih tudi spo\\u0161tujem.\"]", 5, 16 }
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
                    { 30, 1 },
                    { 31, 1 },
                    { 32, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SurveyQuestionnairyId",
                table: "Activities",
                column: "SurveyQuestionnairyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionSurveyQuestionnairy_SurveyQuestionnairiesId",
                table: "SurveyQuestionSurveyQuestionnairy",
                column: "SurveyQuestionnairiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Questionnairies_SurveyQuestionnairyId",
                table: "Activities",
                column: "SurveyQuestionnairyId",
                principalTable: "Questionnairies",
                principalColumn: "Id");
        }
    }
}
