using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class incidentmodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IncidentStatus",
                table: "Incidents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncidentStatus",
                table: "Incidents");
        }
    }
}
