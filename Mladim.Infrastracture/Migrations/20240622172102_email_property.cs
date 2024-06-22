using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class email_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EmailSent",
                table: "Member",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSent",
                table: "Member");
        }
    }
}
