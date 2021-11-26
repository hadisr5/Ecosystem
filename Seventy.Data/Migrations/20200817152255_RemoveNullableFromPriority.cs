using Microsoft.EntityFrameworkCore.Migrations;

namespace Seventy.Data.Migrations
{
    public partial class RemoveNullableFromPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "Core",
                table: "Roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "Core",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
