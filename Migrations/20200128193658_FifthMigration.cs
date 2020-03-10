using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rescuer_Unit_AssignedUnitUnitId",
                table: "Rescuer");

            migrationBuilder.DropIndex(
                name: "IX_Rescuer_AssignedUnitUnitId",
                table: "Rescuer");

            migrationBuilder.DropColumn(
                name: "AssignedUnitUnitId",
                table: "Rescuer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Incident",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUnitUnitId",
                table: "Rescuer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Incident",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Rescuer_AssignedUnitUnitId",
                table: "Rescuer",
                column: "AssignedUnitUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rescuer_Unit_AssignedUnitUnitId",
                table: "Rescuer",
                column: "AssignedUnitUnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
