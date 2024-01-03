using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class RepetitiveActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attributes_IsRepetitive",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Attributes_NumOfRepetitions",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attributes_RepetitiveInterval",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes_IsRepetitive",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Attributes_NumOfRepetitions",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Attributes_RepetitiveInterval",
                table: "Activities");
        }
    }
}
