using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class responsestatusupate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResponseStatus",
                table: "Units",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ResponseStatus",
                table: "Units",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
