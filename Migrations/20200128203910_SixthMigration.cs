using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Users_DispatcherUserId",
                table: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Incident_DispatcherUserId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "DispatcherUserId",
                table: "Incident");

            migrationBuilder.RenameColumn(
                name: "UpdatdAt",
                table: "Unit",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Unit",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Unit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Rescuer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Rescuer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rescuer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Incident",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UserId",
                table: "Unit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rescuer_UserId",
                table: "Rescuer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_UserId",
                table: "Incident",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Users_UserId",
                table: "Incident",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rescuer_Users_UserId",
                table: "Rescuer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Users_UserId",
                table: "Unit",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Users_UserId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Rescuer_Users_UserId",
                table: "Rescuer");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Users_UserId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_UserId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Rescuer_UserId",
                table: "Rescuer");

            migrationBuilder.DropIndex(
                name: "IX_Incident_UserId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rescuer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Incident");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Unit",
                newName: "UpdatdAt");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Rescuer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Rescuer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "DispatcherUserId",
                table: "Incident",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incident_DispatcherUserId",
                table: "Incident",
                column: "DispatcherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Users_DispatcherUserId",
                table: "Incident",
                column: "DispatcherUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
