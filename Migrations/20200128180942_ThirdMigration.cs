using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Unit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatdAt",
                table: "Unit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rescuer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatdAt",
                table: "Rescuer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Incident",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Incident",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatdAt",
                table: "Incident",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Dispatch",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatdAt",
                table: "Dispatch",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Assignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatdAt",
                table: "Assignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatdAt",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rescuer");

            migrationBuilder.DropColumn(
                name: "UpdatdAt",
                table: "Rescuer");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "UpdatdAt",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Dispatch");

            migrationBuilder.DropColumn(
                name: "UpdatdAt",
                table: "Dispatch");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "UpdatdAt",
                table: "Assignment");
        }
    }
}
