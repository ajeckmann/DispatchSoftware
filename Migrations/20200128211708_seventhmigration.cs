using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class seventhmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Rescuer_RiderAssignedRescuerId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Unit_UnitId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Users_UserId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Rescuer_Users_UserId",
                table: "Rescuer");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Users_UserId",
                table: "Unit");

            migrationBuilder.DropTable(
                name: "Dispatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rescuer",
                table: "Rescuer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incident",
                table: "Incident");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Rescuer",
                newName: "Rescuers");

            migrationBuilder.RenameTable(
                name: "Incident",
                newName: "Incidents");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_UserId",
                table: "Units",
                newName: "IX_Units_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rescuer_UserId",
                table: "Rescuers",
                newName: "IX_Rescuers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Incident_UserId",
                table: "Incidents",
                newName: "IX_Incidents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_UnitId",
                table: "Assignments",
                newName: "IX_Assignments_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_RiderAssignedRescuerId",
                table: "Assignments",
                newName: "IX_Assignments_RiderAssignedRescuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rescuers",
                table: "Rescuers",
                column: "RescuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents",
                column: "IncidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "AssignmentId");

            migrationBuilder.CreateTable(
                name: "Dispatches",
                columns: table => new
                {
                    DispatchhId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IncidentId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatches", x => x.DispatchhId);
                    table.ForeignKey(
                        name: "FK_Dispatches_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "IncidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dispatches_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_IncidentId",
                table: "Dispatches",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_UnitId",
                table: "Dispatches",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Rescuers_RiderAssignedRescuerId",
                table: "Assignments",
                column: "RiderAssignedRescuerId",
                principalTable: "Rescuers",
                principalColumn: "RescuerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Units_UnitId",
                table: "Assignments",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Users_UserId",
                table: "Incidents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rescuers_Users_UserId",
                table: "Rescuers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Users_UserId",
                table: "Units",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Rescuers_RiderAssignedRescuerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Units_UnitId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Users_UserId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rescuers_Users_UserId",
                table: "Rescuers");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Users_UserId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Dispatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rescuers",
                table: "Rescuers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "Rescuers",
                newName: "Rescuer");

            migrationBuilder.RenameTable(
                name: "Incidents",
                newName: "Incident");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_Units_UserId",
                table: "Unit",
                newName: "IX_Unit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rescuers_UserId",
                table: "Rescuer",
                newName: "IX_Rescuer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Incidents_UserId",
                table: "Incident",
                newName: "IX_Incident_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UnitId",
                table: "Assignment",
                newName: "IX_Assignment_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_RiderAssignedRescuerId",
                table: "Assignment",
                newName: "IX_Assignment_RiderAssignedRescuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rescuer",
                table: "Rescuer",
                column: "RescuerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incident",
                table: "Incident",
                column: "IncidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "AssignmentId");

            migrationBuilder.CreateTable(
                name: "Dispatch",
                columns: table => new
                {
                    DispatchId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IncidentId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    UpdatdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatch", x => x.DispatchId);
                    table.ForeignKey(
                        name: "FK_Dispatch_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "IncidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dispatch_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dispatch_IncidentId",
                table: "Dispatch",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatch_UnitId",
                table: "Dispatch",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Rescuer_RiderAssignedRescuerId",
                table: "Assignment",
                column: "RiderAssignedRescuerId",
                principalTable: "Rescuer",
                principalColumn: "RescuerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Unit_UnitId",
                table: "Assignment",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

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
    }
}
