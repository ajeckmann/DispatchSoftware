using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    IncidentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DispatcherUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incident_Users_DispatcherUserId",
                        column: x => x.DispatcherUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Dispatch",
                columns: table => new
                {
                    DispatchId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IncidentId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Rescuer",
                columns: table => new
                {
                    RescuerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Rank = table.Column<string>(nullable: true),
                    AssignedUnitUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rescuer", x => x.RescuerId);
                    table.ForeignKey(
                        name: "FK_Rescuer_Unit_AssignedUnitUnitId",
                        column: x => x.AssignedUnitUnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rescuer = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    RiderAssignedRescuerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignment_Rescuer_RiderAssignedRescuerId",
                        column: x => x.RiderAssignedRescuerId,
                        principalTable: "Rescuer",
                        principalColumn: "RescuerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_RiderAssignedRescuerId",
                table: "Assignment",
                column: "RiderAssignedRescuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_UnitId",
                table: "Assignment",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatch_IncidentId",
                table: "Dispatch",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatch_UnitId",
                table: "Dispatch",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_DispatcherUserId",
                table: "Incident",
                column: "DispatcherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rescuer_AssignedUnitUnitId",
                table: "Rescuer",
                column: "AssignedUnitUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Dispatch");

            migrationBuilder.DropTable(
                name: "Rescuer");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
