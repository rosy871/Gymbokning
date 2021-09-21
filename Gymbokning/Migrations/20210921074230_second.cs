using Microsoft.EntityFrameworkCore.Migrations;

namespace Gymbokning.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymclass_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymclass");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymclass_GymClasses_GymClassId",
                table: "ApplicationUserGymclass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymclass",
                table: "ApplicationUserGymclass");

            migrationBuilder.RenameTable(
                name: "ApplicationUserGymclass",
                newName: "ApplicationUserGymclasses");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserGymclass_GymClassId",
                table: "ApplicationUserGymclasses",
                newName: "IX_ApplicationUserGymclasses_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymclasses",
                table: "ApplicationUserGymclasses",
                columns: new[] { "ApplicationUserId", "GymClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymclasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymclasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymclasses_GymClasses_GymClassId",
                table: "ApplicationUserGymclasses",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymclasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymclasses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymclasses_GymClasses_GymClassId",
                table: "ApplicationUserGymclasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymclasses",
                table: "ApplicationUserGymclasses");

            migrationBuilder.RenameTable(
                name: "ApplicationUserGymclasses",
                newName: "ApplicationUserGymclass");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserGymclasses_GymClassId",
                table: "ApplicationUserGymclass",
                newName: "IX_ApplicationUserGymclass_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymclass",
                table: "ApplicationUserGymclass",
                columns: new[] { "ApplicationUserId", "GymClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymclass_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymclass",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymclass_GymClasses_GymClassId",
                table: "ApplicationUserGymclass",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
