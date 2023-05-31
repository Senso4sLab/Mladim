using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class YearAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfBirth",
                table: "Member",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
