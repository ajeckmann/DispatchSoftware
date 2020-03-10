using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class thirteenmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Rescuers_RiderAssignedRescuerId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_RiderAssignedRescuerId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "RiderAssignedRescuerId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Rescuer",
                table: "Assignments",
                newName: "RescuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_RescuerId",
                table: "Assignments",
                column: "RescuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Rescuers_RescuerId",
                table: "Assignments",
                column: "RescuerId",
                principalTable: "Rescuers",
                principalColumn: "RescuerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Rescuers_RescuerId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_RescuerId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "RescuerId",
                table: "Assignments",
                newName: "Rescuer");

            migrationBuilder.AddColumn<int>(
                name: "RiderAssignedRescuerId",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_RiderAssignedRescuerId",
                table: "Assignments",
                column: "RiderAssignedRescuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Rescuers_RiderAssignedRescuerId",
                table: "Assignments",
                column: "RiderAssignedRescuerId",
                principalTable: "Rescuers",
                principalColumn: "RescuerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
