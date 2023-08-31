using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class addIgnoreAgegroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeGroup",
                table: "Member",
                type: "int",
                nullable: true);
        }
    }
}
