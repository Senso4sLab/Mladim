using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnonymousParticipants",
                columns: new[] { "Id", "AgeGroup", "Gender" },
                values: new object[,]
                {
                    { 33, 1, 32 },
                    { 34, 2, 32 },
                    { 36, 4, 32 },
                    { 40, 8, 32 },
                    { 48, 16, 32 },
                    { 65, 1, 64 },
                    { 66, 2, 64 },
                    { 68, 4, 64 },
                    { 72, 8, 64 },
                    { 80, 16, 64 },
                    { 129, 1, 128 },
                    { 130, 2, 128 },
                    { 132, 4, 128 },
                    { 136, 8, 128 },
                    { 144, 16, 128 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 144);
        }
    }
}
