using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class activityGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attributes_IsGroup",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes_IsGroup",
                table: "Activities");
        }
    }
}
