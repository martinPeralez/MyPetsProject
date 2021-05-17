using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPets.Migrations
{
    public partial class HistroyDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Appointment",
                table: "Histories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstMeal",
                table: "Histories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Medication",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondMeal",
                table: "Histories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appointment",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "FirstMeal",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "Medication",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "SecondMeal",
                table: "Histories");
        }
    }
}
