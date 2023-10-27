using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class addSurveyQuestionnairy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnonymousParticipant_Id",
                table: "AnonymousParticipantGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UniqueQuestionId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Questionnairies_SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "SurveyQuestionSurveyQuestionnairy");

            migrationBuilder.DropTable(
                name: "Questionnairies");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AnonymousParticipant_Id",
                table: "AnonymousParticipantGroup");

            migrationBuilder.DropColumn(
                name: "SurveyQuestionnairyId",
                table: "Activities");
        }
    }
}
