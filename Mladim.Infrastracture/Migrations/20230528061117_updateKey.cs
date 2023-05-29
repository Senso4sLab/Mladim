using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectStaff",
                table: "ProjectStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityStaff",
                table: "ActivityStaff");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Member",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectStaff",
                table: "ProjectStaff",
                columns: new[] { "StaffMemberId", "ProjectId", "IsLead" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityStaff",
                table: "ActivityStaff",
                columns: new[] { "StaffMemberId", "ActivityId", "IsLead" });

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 3,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 4,
                column: "Gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 5,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 6,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 7,
                column: "Gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 8,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 9,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 10,
                column: "Gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 11,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 12,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 13,
                column: "Gender",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 14,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 15,
                column: "Gender",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectStaff",
                table: "ProjectStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityStaff",
                table: "ActivityStaff");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Member",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectStaff",
                table: "ProjectStaff",
                columns: new[] { "StaffMemberId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityStaff",
                table: "ActivityStaff",
                columns: new[] { "StaffMemberId", "ActivityId" });

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 3,
                column: "Gender",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 4,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 5,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 6,
                column: "Gender",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 7,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 8,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 9,
                column: "Gender",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 10,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 11,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 12,
                column: "Gender",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 13,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 14,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnonymousParticipants",
                keyColumn: "Id",
                keyValue: 15,
                column: "Gender",
                value: 3);
        }
    }
}
