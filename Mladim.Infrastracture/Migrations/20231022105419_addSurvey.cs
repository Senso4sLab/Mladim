using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class addSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SurveyQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveryResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveryBooleanResponse_Response = table.Column<int>(type: "int", nullable: true),
                    SurveryMultipleResponse_Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurveryRatingResponse_Response = table.Column<int>(type: "int", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveryResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveryResponses_SurveyQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SurveyQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_SurveyQuestionSurveyQuestionnairy_SurveyQuestion_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "SurveyQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SurveyQuestionnairyId",
                table: "Activities",
                column: "SurveyQuestionnairyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveryResponses_QuestionId",
                table: "SurveryResponses",
                column: "QuestionId");

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
                name: "SurveryResponses");

            migrationBuilder.DropTable(
                name: "SurveyQuestionSurveyQuestionnairy");

            migrationBuilder.DropTable(
                name: "Questionnairies");

            migrationBuilder.DropTable(
                name: "SurveyQuestion");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SurveyQuestionnairyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SurveyQuestionnairyId",
                table: "Activities");
        }
    }
}
