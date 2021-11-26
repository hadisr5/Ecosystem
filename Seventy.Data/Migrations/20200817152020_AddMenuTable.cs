using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seventy.Data.Migrations
{
    public partial class AddMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "Core",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuAccess",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    MenuCode = table.Column<int>(nullable: false),
                    AccessCode = table.Column<int>(nullable: false),
                    Route = table.Column<string>(nullable: true),
                    eModule = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAccess", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MenuAccess_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuAccess_RegUserID",
                schema: "Core",
                table: "MenuAccess",
                column: "RegUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuAccess",
                schema: "Core");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "Core",
                table: "Roles");
        }
    }
}
