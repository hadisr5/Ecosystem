using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seventy.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "EDU");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Mobile = table.Column<string>(unicode: false, maxLength: 11, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 124, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExamAnswerSheet",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ExamID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    AnswerOption = table.Column<int>(nullable: true),
                    AchievedBarom = table.Column<double>(nullable: true),
                    FileID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAnswerSheet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEvalIndex",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    TeacherID = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEvalIndex", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEvalResult",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    TeacherEvalIndexID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEvalResult", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserLesson",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    LikeRank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLesson", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deductions_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Amount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_UserID_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoroohAccount",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoroohAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GoroohAccount_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    AccessControl = table.Column<int>(nullable: false),
                    AccessType = table.Column<int>(nullable: false),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Route = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    AllowAnonymous = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Access_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroup",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccessGroup_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Section = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    DocType = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    DocFormat = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Documents_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentType_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    FileExtension = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Files_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KMcategory",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KMcategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KMcategory_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KmExperience",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Section = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    CatID = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KmExperience", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KmExperience_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KmNeeds",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Section = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    CatID = table.Column<int>(nullable: false),
                    Response = table.Column<string>(nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KmNeeds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KmNeeds_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Section = table.Column<string>(maxLength: 35, nullable: false),
                    LogType = table.Column<string>(maxLength: 10, nullable: false),
                    IP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    MAC = table.Column<string>(unicode: false, maxLength: 17, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logs_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    SenderUserID = table.Column<int>(nullable: false),
                    ReceiverUserID = table.Column<int>(nullable: false),
                    MsgTitle = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    MsgType = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    MsgViewed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_ReceiverUserID_Users",
                        column: x => x.ReceiverUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_SenderUserID_Users",
                        column: x => x.SenderUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 35, nullable: false),
                    ENTitle = table.Column<string>(maxLength: 35, nullable: false),
                    Section = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permissions_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaceLayers",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceLayers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceLayers_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    RoleID = table.Column<int>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roles_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    TagContainer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tags_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 35, nullable: false),
                    Section = table.Column<string>(maxLength: 35, nullable: false),
                    Priority = table.Column<string>(maxLength: 10, nullable: false),
                    Status = table.Column<string>(maxLength: 12, nullable: false),
                    Actions = table.Column<string>(nullable: false),
                    ResponderUserID = table.Column<int>(nullable: false),
                    Response = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tickets_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CateringPackage",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringPackage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CateringPackage_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategory",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    PrimaryCat = table.Column<string>(maxLength: 50, nullable: false),
                    SecondaryCat = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseCategory_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LMS",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LMS_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestedCourses",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    CourseType = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestedCourses_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvalIndex",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    TargetType = table.Column<string>(maxLength: 50, nullable: false),
                    TargetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvalIndex", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingEvalIndex_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KolAccount",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    GoroohID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KolAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KolAccount_GoroohAccount",
                        column: x => x.GoroohID,
                        principalSchema: "Accounting",
                        principalTable: "GoroohAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KolAccount_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccess",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    AccessID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserAccess_Access_AccessID",
                        column: x => x.AccessID,
                        principalSchema: "Core",
                        principalTable: "Access",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccess_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccess_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionGroup",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    PermissionGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPermissionGroup_AccessGroup_PermissionGroupID",
                        column: x => x.PermissionGroupID,
                        principalSchema: "Core",
                        principalTable: "AccessGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissionGroup_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPermissionGroup_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    DocumentTypeID = table.Column<int>(nullable: false),
                    FileID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserDocuments_DocumentType",
                        column: x => x.DocumentTypeID,
                        principalSchema: "Core",
                        principalTable: "DocumentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Files",
                        column: x => x.FileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDocuments_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Father = table.Column<string>(maxLength: 25, nullable: false),
                    Sex = table.Column<string>(maxLength: 3, nullable: false),
                    Tavalod = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    CodeMelli = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Country = table.Column<string>(maxLength: 20, nullable: false),
                    Ostan = table.Column<string>(maxLength: 20, nullable: false),
                    Shahr = table.Column<string>(maxLength: 20, nullable: false),
                    OstanSokoonat = table.Column<string>(maxLength: 20, nullable: false),
                    ShahrSokoonat = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(maxLength: 12, nullable: false),
                    Cell = table.Column<string>(maxLength: 12, nullable: false),
                    Madrak = table.Column<string>(maxLength: 12, nullable: false),
                    Reshte = table.Column<string>(maxLength: 25, nullable: false),
                    Daneshgah = table.Column<string>(maxLength: 25, nullable: false),
                    PhotoFileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Files",
                        column: x => x.PhotoFileId,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SampleFileID = table.Column<int>(nullable: false),
                    CreditorOrganization = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Certificate_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificate_Files",
                        column: x => x.SampleFileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    PicFileID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lesson_Files",
                        column: x => x.PicFileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingContent",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    ExternalContentID = table.Column<int>(nullable: true),
                    FileID = table.Column<int>(nullable: true),
                    DemoState = table.Column<string>(maxLength: 50, nullable: false),
                    Achievement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingContent_Files",
                        column: x => x.FileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingContent_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LayerID = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(12, 9)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(12, 9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Places_LocationLayers",
                        column: x => x.LayerID,
                        principalSchema: "Core",
                        principalTable: "PlaceLayers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Places_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefaultRoleAccess",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    AccessID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultRoleAccess", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DefaultRoleAccess_Access_AccessID",
                        column: x => x.AccessID,
                        principalSchema: "Core",
                        principalTable: "Access",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefaultRoleAccess_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefaultRoleAccess_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Core",
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Core",
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupMembers",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    UserGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupMembers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserGroupMembers_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroupMembers_UserGroups",
                        column: x => x.UserGroupID,
                        principalSchema: "Core",
                        principalTable: "UserGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroupMembers_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    CourseType = table.Column<string>(maxLength: 50, nullable: false),
                    RequiredDocuments = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    PublishState = table.Column<string>(maxLength: 50, nullable: false),
                    Achievements = table.Column<string>(nullable: false),
                    HozoriType = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<int>(nullable: false),
                    PhotoFileID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Course_CourseCategory",
                        column: x => x.CategoryID,
                        principalSchema: "EDU",
                        principalTable: "CourseCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_PhotoFileID_Files",
                        column: x => x.PhotoFileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvalResult",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    TrainingEvalIndexID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Result = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvalResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingEvalResult_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvalResult_TrainingEvalIndex",
                        column: x => x.TrainingEvalIndexID,
                        principalSchema: "EDU",
                        principalTable: "TrainingEvalIndex",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvalResult_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoeinAccount",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    KolID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoeinAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MoeinAccount_KolAccount",
                        column: x => x.KolID,
                        principalSchema: "Accounting",
                        principalTable: "KolAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoeinAccount_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessPermissionGroup",
                schema: "Core",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    AccessID = table.Column<int>(nullable: false),
                    PermissionGroupID = table.Column<int>(nullable: false),
                    UserPermissionGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPermissionGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccessPermissionGroup_Access_AccessID",
                        column: x => x.AccessID,
                        principalSchema: "Core",
                        principalTable: "Access",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessPermissionGroup_AccessGroup_PermissionGroupID",
                        column: x => x.PermissionGroupID,
                        principalSchema: "Core",
                        principalTable: "AccessGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessPermissionGroup_Users_RegUserID",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessPermissionGroup_UserPermissionGroup_UserPermissionGroupID",
                        column: x => x.UserPermissionGroupID,
                        principalSchema: "Core",
                        principalTable: "UserPermissionGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LessonID = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    QuestionCount = table.Column<int>(nullable: false),
                    PassingGrade = table.Column<int>(nullable: false),
                    RandomQuestionsOrder = table.Column<bool>(nullable: false),
                    RandomQuestionOptionsOrder = table.Column<bool>(nullable: false),
                    FileID = table.Column<int>(nullable: true),
                    Time = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exam_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exam_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LessonID = table.Column<int>(nullable: false),
                    FileID = table.Column<int>(nullable: false),
                    Barom = table.Column<int>(nullable: false),
                    CorrectAnswer = table.Column<string>(nullable: false),
                    ContentID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exercise_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forum",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LessonID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forum", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Forum_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forum_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonObservation",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonObservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonObservation_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonObservation_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonObservation_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LessonID = table.Column<int>(nullable: false),
                    QuestionLevel = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    MultiOption = table.Column<bool>(nullable: false),
                    FileID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Files",
                        column: x => x.FileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestForContent",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestForContent_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestForContent_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestForContent_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLesson",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    TeacherID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLesson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_TeacherID_Users",
                        column: x => x.TeacherID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLike",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    TeacherID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false),
                    LikeRank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLike", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeacherLike_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherLike_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherLike_TeacherID_Users",
                        column: x => x.TeacherID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherLike_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentObservation",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    ContentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentObservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContentObservation_TrainingContent",
                        column: x => x.ContentID,
                        principalSchema: "EDU",
                        principalTable: "TrainingContent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentObservation_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentObservation_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserContent",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    TrainingContentID = table.Column<int>(nullable: false),
                    Progress = table.Column<int>(nullable: true),
                    LikeRank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserContent_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContent_TrainingContent",
                        column: x => x.TrainingContentID,
                        principalSchema: "EDU",
                        principalTable: "TrainingContent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContent_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCenter",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    PlaceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCenter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingCenter_Places",
                        column: x => x.PlaceID,
                        principalSchema: "Core",
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingCenter_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseGroups",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CourseID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseGroups_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseObservation",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseObservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseObservation_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseObservation_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseObservation_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteCourses",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    LikeRank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavoriteCourses_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteCourses_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteCourses_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedCourses",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    FirstCourseID = table.Column<int>(nullable: false),
                    SecondCourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelatedCourses_FirstCourseID_Course",
                        column: x => x.FirstCourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedCourses_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedCourses_SecondCourseID_Course",
                        column: x => x.SecondCourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TafsiliAccount",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    MoeinID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TafsiliAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TafsiliAccount_MoeinAccount",
                        column: x => x.MoeinID,
                        principalSchema: "Accounting",
                        principalTable: "MoeinAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TafsiliAccount_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamUser",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    ExamID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Result = table.Column<double>(nullable: true),
                    LikeRank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExamUser_Exam",
                        column: x => x.ExamID,
                        principalSchema: "EDU",
                        principalTable: "Exam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamUser_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamUser_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseUser",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    FileID = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    LikeRank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExerciseUser_Exercise",
                        column: x => x.ExerciseId,
                        principalSchema: "EDU",
                        principalTable: "Exercise",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseUser_Files",
                        column: x => x.FileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseUser_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseUser_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestions",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ExamID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Barom = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_Exam",
                        column: x => x.ExamID,
                        principalSchema: "EDU",
                        principalTable: "Exam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_Questions",
                        column: x => x.QuestionID,
                        principalSchema: "EDU",
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    QuestionId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    FileID = table.Column<int>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Files",
                        column: x => x.FileID,
                        principalSchema: "Core",
                        principalTable: "Files",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions",
                        column: x => x.QuestionId,
                        principalSchema: "EDU",
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificateUser",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CertificateID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: true),
                    CourseID = table.Column<int>(nullable: false),
                    CourseGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CertificateUser_Certificate",
                        column: x => x.CertificateID,
                        principalSchema: "EDU",
                        principalTable: "Certificate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificateUser_CourseGroups_CourseGroupID",
                        column: x => x.CourseGroupID,
                        principalSchema: "EDU",
                        principalTable: "CourseGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificateUser_Course_CourseID",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificateUser_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificateUser_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    CourseGroupID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Term_CourseGroups",
                        column: x => x.CourseGroupID,
                        principalSchema: "EDU",
                        principalTable: "CourseGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Term_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Term_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettlementRequest",
                schema: "Accounting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    TafsiliID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Amount = table.Column<long>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SettlementRequest_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SettlementRequest_TafsiliAccount",
                        column: x => x.TafsiliID,
                        principalSchema: "Accounting",
                        principalTable: "TafsiliAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SettlementRequest_UserID_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseRegistration",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    CourseGroupID = table.Column<int>(nullable: false),
                    TermID = table.Column<int>(nullable: false),
                    DocumentsState = table.Column<string>(maxLength: 50, nullable: true),
                    CertificateType = table.Column<string>(maxLength: 50, nullable: true),
                    Progress = table.Column<int>(nullable: true),
                    LikeRank = table.Column<int>(nullable: true),
                    AchievementsState = table.Column<string>(nullable: true),
                    HozoriState = table.Column<string>(maxLength: 50, nullable: true),
                    CateringPackId = table.Column<int>(nullable: true),
                    ResidState = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_CateringPackage",
                        column: x => x.CateringPackId,
                        principalSchema: "EDU",
                        principalTable: "CateringPackage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseGroup_Course",
                        column: x => x.CourseGroupID,
                        principalSchema: "EDU",
                        principalTable: "CourseGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_Term",
                        column: x => x.TermID,
                        principalSchema: "EDU",
                        principalTable: "Term",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TermLesson",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    CourseGroupID = table.Column<int>(nullable: false),
                    TermID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false),
                    TeacherID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermLesson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TermLesson_CourseGroups_CourseGroupID",
                        column: x => x.CourseGroupID,
                        principalSchema: "EDU",
                        principalTable: "CourseGroups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TermLesson_Course_CourseID",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TermLesson_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TermLesson_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TermLesson_Term_TermID",
                        column: x => x.TermID,
                        principalSchema: "EDU",
                        principalTable: "Term",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TrainingWeek",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    LessonID = table.Column<int>(nullable: false),
                    TermID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingWeek", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingWeek_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingWeek_Term_TermID",
                        column: x => x.TermID,
                        principalSchema: "EDU",
                        principalTable: "Term",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Poll",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    TrainingWeekID = table.Column<int>(nullable: false),
                    Barom = table.Column<int>(nullable: false),
                    CorrectAnswer = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Poll_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poll_TrainingWeek",
                        column: x => x.TrainingWeekID,
                        principalSchema: "EDU",
                        principalTable: "TrainingWeek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingWeekContent",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    ContentID = table.Column<int>(nullable: false),
                    TrainingWeekID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingWeekContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainingWeekContent_TrainingContent",
                        column: x => x.ContentID,
                        principalSchema: "EDU",
                        principalTable: "TrainingContent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingWeekContent_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingWeekContent_TrainingWeek",
                        column: x => x.TrainingWeekID,
                        principalSchema: "EDU",
                        principalTable: "TrainingWeek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainingWeekContent",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CourseID = table.Column<int>(nullable: false),
                    CourseGroupID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    TrainingWeekID = table.Column<int>(nullable: false),
                    ContentID = table.Column<int>(nullable: false),
                    Progress = table.Column<int>(nullable: true),
                    Result = table.Column<bool>(nullable: false),
                    LikeRank = table.Column<int>(nullable: true),
                    TermID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrainingWeekContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_TrainingContent",
                        column: x => x.ContentID,
                        principalSchema: "EDU",
                        principalTable: "TrainingContent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_CourseGroups",
                        column: x => x.CourseGroupID,
                        principalSchema: "EDU",
                        principalTable: "CourseGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_Course",
                        column: x => x.CourseID,
                        principalSchema: "EDU",
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_Lesson",
                        column: x => x.LessonID,
                        principalSchema: "EDU",
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_Term_TermID",
                        column: x => x.TermID,
                        principalSchema: "EDU",
                        principalTable: "Term",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_TrainingWeek",
                        column: x => x.TrainingWeekID,
                        principalSchema: "EDU",
                        principalTable: "TrainingWeek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrainingWeekContent_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollUser",
                schema: "EDU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RegUserID = table.Column<int>(nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    PollID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    LikeRank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PollUser_Poll",
                        column: x => x.PollID,
                        principalSchema: "EDU",
                        principalTable: "Poll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollUser_RegUserID_Users",
                        column: x => x.RegUserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollUser_Users",
                        column: x => x.UserID,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_RegUserID",
                schema: "Accounting",
                table: "Deductions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_RegUserID",
                schema: "Accounting",
                table: "FinancialTransactions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_UserID",
                schema: "Accounting",
                table: "FinancialTransactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GoroohAccount_RegUserID",
                schema: "Accounting",
                table: "GoroohAccount",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_KolAccount_GoroohID",
                schema: "Accounting",
                table: "KolAccount",
                column: "GoroohID");

            migrationBuilder.CreateIndex(
                name: "IX_KolAccount_RegUserID",
                schema: "Accounting",
                table: "KolAccount",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoeinAccount_KolID",
                schema: "Accounting",
                table: "MoeinAccount",
                column: "KolID");

            migrationBuilder.CreateIndex(
                name: "IX_MoeinAccount_RegUserID",
                schema: "Accounting",
                table: "MoeinAccount",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementRequest_RegUserID",
                schema: "Accounting",
                table: "SettlementRequest",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementRequest_TafsiliID",
                schema: "Accounting",
                table: "SettlementRequest",
                column: "TafsiliID");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementRequest_UserID",
                schema: "Accounting",
                table: "SettlementRequest",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TafsiliAccount_MoeinID",
                schema: "Accounting",
                table: "TafsiliAccount",
                column: "MoeinID");

            migrationBuilder.CreateIndex(
                name: "IX_TafsiliAccount_RegUserID",
                schema: "Accounting",
                table: "TafsiliAccount",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Access_RegUserID",
                schema: "Core",
                table: "Access",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroup_RegUserID",
                schema: "Core",
                table: "AccessGroup",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermissionGroup_AccessID",
                schema: "Core",
                table: "AccessPermissionGroup",
                column: "AccessID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermissionGroup_PermissionGroupID",
                schema: "Core",
                table: "AccessPermissionGroup",
                column: "PermissionGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermissionGroup_RegUserID",
                schema: "Core",
                table: "AccessPermissionGroup",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessPermissionGroup_UserPermissionGroupID",
                schema: "Core",
                table: "AccessPermissionGroup",
                column: "UserPermissionGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultRoleAccess_AccessID",
                schema: "Core",
                table: "DefaultRoleAccess",
                column: "AccessID");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultRoleAccess_RegUserID",
                schema: "Core",
                table: "DefaultRoleAccess",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultRoleAccess_RoleID",
                schema: "Core",
                table: "DefaultRoleAccess",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RegUserID",
                schema: "Core",
                table: "Documents",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_RegUserID",
                schema: "Core",
                table: "DocumentType",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "UK_DocumentType",
                schema: "Core",
                table: "DocumentType",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_RegUserID",
                schema: "Core",
                table: "Files",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserID",
                schema: "Core",
                table: "Files",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_KMcategory_RegUserID",
                schema: "Core",
                table: "KMcategory",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_KmExperience_RegUserID",
                schema: "Core",
                table: "KmExperience",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_KmNeeds_RegUserID",
                schema: "Core",
                table: "KmNeeds",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_RegUserID",
                schema: "Core",
                table: "Logs",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverUserID",
                schema: "Core",
                table: "Messages",
                column: "ReceiverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RegUserID",
                schema: "Core",
                table: "Messages",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderUserID",
                schema: "Core",
                table: "Messages",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RegUserID",
                schema: "Core",
                table: "Permissions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceLayers_RegUserID",
                schema: "Core",
                table: "PlaceLayers",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_LayerID",
                schema: "Core",
                table: "Places",
                column: "LayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_RegUserID",
                schema: "Core",
                table: "Places",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RegUserID",
                schema: "Core",
                table: "RolePermissions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "UK_RolePermissions",
                schema: "Core",
                table: "RolePermissions",
                columns: new[] { "RoleID", "PermissionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RegUserID",
                schema: "Core",
                table: "Roles",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RegUserID",
                schema: "Core",
                table: "Tags",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RegUserID",
                schema: "Core",
                table: "Tickets",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_AccessID",
                schema: "Core",
                table: "UserAccess",
                column: "AccessID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_RegUserID",
                schema: "Core",
                table: "UserAccess",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UserID",
                schema: "Core",
                table: "UserAccess",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_DocumentTypeID",
                schema: "Core",
                table: "UserDocuments",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_FileID",
                schema: "Core",
                table: "UserDocuments",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_RegUserID",
                schema: "Core",
                table: "UserDocuments",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserID",
                schema: "Core",
                table: "UserDocuments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMembers_RegUserID",
                schema: "Core",
                table: "UserGroupMembers",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMembers_UserGroupID",
                schema: "Core",
                table: "UserGroupMembers",
                column: "UserGroupID");

            migrationBuilder.CreateIndex(
                name: "UK_UserGroupMembers",
                schema: "Core",
                table: "UserGroupMembers",
                columns: new[] { "UserID", "UserGroupID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_RegUserID",
                schema: "Core",
                table: "UserGroups",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionGroup_PermissionGroupID",
                schema: "Core",
                table: "UserPermissionGroup",
                column: "PermissionGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionGroup_RegUserID",
                schema: "Core",
                table: "UserPermissionGroup",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionGroup_UserID",
                schema: "Core",
                table: "UserPermissionGroup",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_PhotoFileId",
                schema: "Core",
                table: "UserProfiles",
                column: "PhotoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_RegUserID",
                schema: "Core",
                table: "UserProfiles",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserID",
                schema: "Core",
                table: "UserProfiles",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RegUserID",
                schema: "Core",
                table: "UserRole",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                schema: "Core",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                schema: "Core",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CateringPackage_RegUserID",
                schema: "EDU",
                table: "CateringPackage",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_RegUserID",
                schema: "EDU",
                table: "Certificate",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_SampleFileID",
                schema: "EDU",
                table: "Certificate",
                column: "SampleFileID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateUser_CertificateID",
                schema: "EDU",
                table: "CertificateUser",
                column: "CertificateID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateUser_CourseGroupID",
                schema: "EDU",
                table: "CertificateUser",
                column: "CourseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateUser_CourseID",
                schema: "EDU",
                table: "CertificateUser",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateUser_RegUserID",
                schema: "EDU",
                table: "CertificateUser",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateUser_UserID",
                schema: "EDU",
                table: "CertificateUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObservation_ContentID",
                schema: "EDU",
                table: "ContentObservation",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObservation_RegUserID",
                schema: "EDU",
                table: "ContentObservation",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObservation_UserID",
                schema: "EDU",
                table: "ContentObservation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryID",
                schema: "EDU",
                table: "Course",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_PhotoFileID",
                schema: "EDU",
                table: "Course",
                column: "PhotoFileID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_RegUserID",
                schema: "EDU",
                table: "Course",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategory_RegUserID",
                schema: "EDU",
                table: "CourseCategory",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_CourseID",
                schema: "EDU",
                table: "CourseGroups",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseObservation_CourseID",
                schema: "EDU",
                table: "CourseObservation",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseObservation_RegUserID",
                schema: "EDU",
                table: "CourseObservation",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseObservation_UserID",
                schema: "EDU",
                table: "CourseObservation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_CateringPackId",
                schema: "EDU",
                table: "CourseRegistration",
                column: "CateringPackId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_CourseGroupID",
                schema: "EDU",
                table: "CourseRegistration",
                column: "CourseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_CourseID",
                schema: "EDU",
                table: "CourseRegistration",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_RegUserID",
                schema: "EDU",
                table: "CourseRegistration",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_TermID",
                schema: "EDU",
                table: "CourseRegistration",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_UserID",
                schema: "EDU",
                table: "CourseRegistration",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_LessonID",
                schema: "EDU",
                table: "Exam",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_RegUserID",
                schema: "EDU",
                table: "Exam",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamID",
                schema: "EDU",
                table: "ExamQuestions",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_QuestionID",
                schema: "EDU",
                table: "ExamQuestions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_RegUserID",
                schema: "EDU",
                table: "ExamQuestions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamUser_ExamID",
                schema: "EDU",
                table: "ExamUser",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamUser_RegUserID",
                schema: "EDU",
                table: "ExamUser",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamUser_UserID",
                schema: "EDU",
                table: "ExamUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_LessonID",
                schema: "EDU",
                table: "Exercise",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUser_ExerciseId",
                schema: "EDU",
                table: "ExerciseUser",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUser_FileID",
                schema: "EDU",
                table: "ExerciseUser",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUser_RegUserID",
                schema: "EDU",
                table: "ExerciseUser",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUser_UserID",
                schema: "EDU",
                table: "ExerciseUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCourses_CourseID",
                schema: "EDU",
                table: "FavoriteCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCourses_RegUserID",
                schema: "EDU",
                table: "FavoriteCourses",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCourses_UserID",
                schema: "EDU",
                table: "FavoriteCourses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_LessonID",
                schema: "EDU",
                table: "Forum",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_RegUserID",
                schema: "EDU",
                table: "Forum",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_PicFileID",
                schema: "EDU",
                table: "Lesson",
                column: "PicFileID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_RegUserID",
                schema: "EDU",
                table: "Lesson",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonObservation_LessonID",
                schema: "EDU",
                table: "LessonObservation",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonObservation_RegUserID",
                schema: "EDU",
                table: "LessonObservation",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonObservation_UserID",
                schema: "EDU",
                table: "LessonObservation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LMS_RegUserID",
                schema: "EDU",
                table: "LMS",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Poll_RegUserID",
                schema: "EDU",
                table: "Poll",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Poll_TrainingWeekID",
                schema: "EDU",
                table: "Poll",
                column: "TrainingWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_PollUser_PollID",
                schema: "EDU",
                table: "PollUser",
                column: "PollID");

            migrationBuilder.CreateIndex(
                name: "IX_PollUser_RegUserID",
                schema: "EDU",
                table: "PollUser",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PollUser_UserID",
                schema: "EDU",
                table: "PollUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_FileID",
                schema: "EDU",
                table: "QuestionOptions",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                schema: "EDU",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_RegUserID",
                schema: "EDU",
                table: "QuestionOptions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FileID",
                schema: "EDU",
                table: "Questions",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LessonID",
                schema: "EDU",
                table: "Questions",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_RegUserID",
                schema: "EDU",
                table: "Questions",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedCourses_RegUserID",
                schema: "EDU",
                table: "RelatedCourses",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedCourses_SecondCourseID",
                schema: "EDU",
                table: "RelatedCourses",
                column: "SecondCourseID");

            migrationBuilder.CreateIndex(
                name: "UK_RelatedCourses",
                schema: "EDU",
                table: "RelatedCourses",
                columns: new[] { "FirstCourseID", "SecondCourseID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestedCourses_RegUserID",
                schema: "EDU",
                table: "RequestedCourses",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForContent_LessonID",
                schema: "EDU",
                table: "RequestForContent",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForContent_RegUserID",
                schema: "EDU",
                table: "RequestForContent",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForContent_UserID",
                schema: "EDU",
                table: "RequestForContent",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UK_TeacherLesson",
                schema: "EDU",
                table: "TeacherLesson",
                column: "LessonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_RegUserID",
                schema: "EDU",
                table: "TeacherLesson",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_TeacherID",
                schema: "EDU",
                table: "TeacherLesson",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLike_LessonID",
                schema: "EDU",
                table: "TeacherLike",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLike_RegUserID",
                schema: "EDU",
                table: "TeacherLike",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLike_TeacherID",
                schema: "EDU",
                table: "TeacherLike",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLike_UserID",
                schema: "EDU",
                table: "TeacherLike",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CourseGroupID",
                schema: "EDU",
                table: "Term",
                column: "CourseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CourseID",
                schema: "EDU",
                table: "Term",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Term_RegUserID",
                schema: "EDU",
                table: "Term",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TermLesson_CourseGroupID",
                schema: "EDU",
                table: "TermLesson",
                column: "CourseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_TermLesson_CourseID",
                schema: "EDU",
                table: "TermLesson",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TermLesson_LessonID",
                schema: "EDU",
                table: "TermLesson",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_TermLesson_TeacherID",
                schema: "EDU",
                table: "TermLesson",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TermLesson_TermID",
                schema: "EDU",
                table: "TermLesson",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCenter_PlaceID",
                schema: "EDU",
                table: "TrainingCenter",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCenter_RegUserID",
                schema: "EDU",
                table: "TrainingCenter",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContent_FileID",
                schema: "EDU",
                table: "TrainingContent",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContent_RegUserID",
                schema: "EDU",
                table: "TrainingContent",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvalIndex_RegUserID",
                schema: "EDU",
                table: "TrainingEvalIndex",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvalResult_RegUserID",
                schema: "EDU",
                table: "TrainingEvalResult",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvalResult_TrainingEvalIndexID",
                schema: "EDU",
                table: "TrainingEvalResult",
                column: "TrainingEvalIndexID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvalResult_UserID",
                schema: "EDU",
                table: "TrainingEvalResult",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingWeek_LessonID",
                schema: "EDU",
                table: "TrainingWeek",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingWeek_TermID",
                schema: "EDU",
                table: "TrainingWeek",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingWeekContent_ContentID",
                schema: "EDU",
                table: "TrainingWeekContent",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingWeekContent_RegUserID",
                schema: "EDU",
                table: "TrainingWeekContent",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingWeekContent_TrainingWeekID",
                schema: "EDU",
                table: "TrainingWeekContent",
                column: "TrainingWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_UserContent_RegUserID",
                schema: "EDU",
                table: "UserContent",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserContent_TrainingContentID",
                schema: "EDU",
                table: "UserContent",
                column: "TrainingContentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserContent_UserID",
                schema: "EDU",
                table: "UserContent",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_ContentID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_CourseGroupID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "CourseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_CourseID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_LessonID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_RegUserID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "RegUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_TermID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_TrainingWeekID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "TrainingWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingWeekContent_UserID",
                schema: "EDU",
                table: "UserTrainingWeekContent",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deductions",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "FinancialTransactions",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "SettlementRequest",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "AccessPermissionGroup",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "DefaultRoleAccess",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "KMcategory",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "KmExperience",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "KmNeeds",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserAccess",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserDocuments",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserGroupMembers",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserProfiles",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "CertificateUser",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "ContentObservation",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "CourseObservation",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "CourseRegistration",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "ExamAnswerSheet",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "ExamQuestions",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "ExamUser",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "ExerciseUser",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "FavoriteCourses",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Forum",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "LessonObservation",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "LMS",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "PollUser",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "QuestionOptions",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "RelatedCourses",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "RequestedCourses",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "RequestForContent",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TeacherEvalIndex",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TeacherEvalResult",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TeacherLesson",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TeacherLike",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TermLesson",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TrainingCenter",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TrainingEvalResult",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TrainingWeekContent",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "UserContent",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "UserLesson",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "UserTrainingWeekContent",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TafsiliAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "UserPermissionGroup",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Access",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserGroups",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Certificate",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "CateringPackage",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Poll",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Places",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TrainingEvalIndex",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "TrainingContent",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "MoeinAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "AccessGroup",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TrainingWeek",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "PlaceLayers",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "KolAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Lesson",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Term",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "GoroohAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "CourseGroups",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "CourseCategory",
                schema: "EDU");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Core");
        }
    }
}
