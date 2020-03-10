using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "Unit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Incident",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Incident",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Unit");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Incident",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Incident",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
