using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppCoreData.Migrations
{
    public partial class WebAppCore_1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskType",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "Tasks",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Tasks");
        }
    }
}
