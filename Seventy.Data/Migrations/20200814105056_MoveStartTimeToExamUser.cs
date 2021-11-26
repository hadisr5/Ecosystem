using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seventy.Data.Migrations
{
    public partial class MoveStartTimeToExamUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "EDU",
                table: "Exam");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                schema: "EDU",
                table: "ExamUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "EDU",
                table: "ExamUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                schema: "EDU",
                table: "Exam",
                type: "datetime2",
                nullable: true);
        }
    }
}
