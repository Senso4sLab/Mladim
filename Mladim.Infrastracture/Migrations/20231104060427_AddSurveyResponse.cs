using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousSurveyResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Responses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnonymousParticipant_Id = table.Column<int>(type: "int", nullable: false),
                    AnonymousParticipant_Gender = table.Column<int>(type: "int", nullable: false),
                    AnonymousParticipant_AgeGroup = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousSurveyResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousSurveyResponse_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousSurveyResponse_ActivityId",
                table: "AnonymousSurveyResponse",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousSurveyResponse");
        }
    }
}
