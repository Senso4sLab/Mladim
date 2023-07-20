using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mladim.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class addGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityActivityGroup_ActivityGroups_GroupsId",
                table: "ActivityActivityGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_ActivityGroups_ActivityGroupId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_ProjectGroups_ProjectGroupId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProjectGroup_ProjectGroups_GroupsId",
                table: "ProjectProjectGroup");

            migrationBuilder.DropTable(
                name: "ActivityGroups");

            migrationBuilder.DropIndex(
                name: "IX_Member_ActivityGroupId",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectGroups",
                table: "ProjectGroups");

            migrationBuilder.DropColumn(
                name: "ActivityGroupId",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "ProjectGroups",
                newName: "Groups");

            migrationBuilder.RenameColumn(
                name: "ProjectGroupId",
                table: "Member",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_ProjectGroupId",
                table: "Member",
                newName: "IX_Member_GroupId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityActivityGroup_Groups_GroupsId",
                table: "ActivityActivityGroup",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Groups_GroupId",
                table: "Member",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProjectGroup_Groups_GroupsId",
                table: "ProjectProjectGroup",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityActivityGroup_Groups_GroupsId",
                table: "ActivityActivityGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Groups_GroupId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProjectGroup_Groups_GroupsId",
                table: "ProjectProjectGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "ProjectGroups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Member",
                newName: "ProjectGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_GroupId",
                table: "Member",
                newName: "IX_Member_ProjectGroupId");

            migrationBuilder.AddColumn<int>(
                name: "ActivityGroupId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectGroups",
                table: "ProjectGroups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_ActivityGroupId",
                table: "Member",
                column: "ActivityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityActivityGroup_ActivityGroups_GroupsId",
                table: "ActivityActivityGroup",
                column: "GroupsId",
                principalTable: "ActivityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_ActivityGroups_ActivityGroupId",
                table: "Member",
                column: "ActivityGroupId",
                principalTable: "ActivityGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_ProjectGroups_ProjectGroupId",
                table: "Member",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProjectGroup_ProjectGroups_GroupsId",
                table: "ProjectProjectGroup",
                column: "GroupsId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
