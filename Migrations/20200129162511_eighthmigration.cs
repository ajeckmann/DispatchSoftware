using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class eighthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Units",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AddColumn<string>(
                name: "NumberType",
                table: "Units",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberType",
                table: "Units");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Units",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
