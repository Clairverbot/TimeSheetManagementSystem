using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoginUserName = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_UserInfoId", x => x.UserInfoId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                columns: table => new
                {
                    CustomerAccountId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Comments = table.Column<string>(type: "NVARCHAR(4000)", nullable: true),
                    IsVisible = table.Column<bool>(type: "BIT", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_CustomerAccountId", x => x.CustomerAccountId);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionSynopses",
                columns: table => new
                {
                    SessionSynopsisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionSynopsisName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_SessionSynopsisId", x => x.SessionSynopsisId);
                    table.ForeignKey(
                        name: "FK_SessionSynopses_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionSynopses_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    TimeSheetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MonthAndYear = table.Column<DateTime>(nullable: false),
                    RatePerHour = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    InstructorId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    VerifiedAndSubmittedAt = table.Column<DateTime>(nullable: true),
                    CheckedById = table.Column<int>(nullable: false),
                    ApprovedById = table.Column<int>(nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetId", x => x.TimeSheetId);
                    table.ForeignKey(
                        name: "FK_TimeSheets_UserInfo_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheets_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheets_UserInfo_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheets_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    AccountDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeekNumber = table.Column<int>(nullable: false),
                    StartTimeInMinutes = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTimeInMinutes = table.Column<TimeSpan>(type: "time", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CustomerAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AccountDetailId", x => x.AccountDetailId);
                    table.ForeignKey(
                        name: "FK_AccountDetails_CustomerAccounts_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccounts",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRates",
                columns: table => new
                {
                    AccountRateId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAccountId = table.Column<int>(nullable: false),
                    RatePerHour = table.Column<decimal>(type: "DECIMAL(6,2)", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AccountRateId", x => x.AccountRateId);
                    table.ForeignKey(
                        name: "FK_AccountRates_CustomerAccounts_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccounts",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorAccounts",
                columns: table => new
                {
                    InstructorAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    CustomerAccountId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "NVARCHAR(4000)", nullable: true),
                    WageRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InstructorAccounttId", x => x.InstructorAccountId);
                    table.ForeignKey(
                        name: "FK_InstructorAccounts_CustomerAccounts_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccounts",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorAccounts_UserInfo_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetails",
                columns: table => new
                {
                    TimeSheetDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeSheetId = table.Column<int>(nullable: false),
                    AccountDetailId = table.Column<int>(nullable: false),
                    SessionSynopsisNames = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    TimeInInMinutes = table.Column<int>(nullable: false),
                    TimeOutInMinutes = table.Column<int>(nullable: false),
                    DateOfLesson = table.Column<DateTime>(nullable: false),
                    IsReplacementInstructor = table.Column<bool>(nullable: false, defaultValue: false),
                    WageRatePerHour = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    OfficialTimeInMinutes = table.Column<int>(nullable: false),
                    OfficialTimeOutMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetDetailId", x => x.TimeSheetDetailId);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetails_AccountDetails_AccountDetailId",
                        column: x => x.AccountDetailId,
                        principalTable: "AccountDetails",
                        principalColumn: "AccountDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetails_TimeSheets_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalTable: "TimeSheets",
                        principalColumn: "TimeSheetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetailSignature",
                columns: table => new
                {
                    TimeSheetDetailSignatureId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Signature = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    TimeSheetIDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetSignatureId", x => x.TimeSheetDetailSignatureId);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetailSignature_TimeSheetDetails_TimeSheetIDetailId",
                        column: x => x.TimeSheetIDetailId,
                        principalTable: "TimeSheetDetails",
                        principalColumn: "TimeSheetDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "UserInfoId", "Email", "FullName", "IsActive", "LoginUserName", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[,]
                {
                    { 1, "KENNY@FAIRYSCHOOL.COM", "KENNY", true, "88881", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Admin" },
                    { 2, "JULIET@FAIRYSCHOOL.COM", "JULIET", true, "88882", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Admin" },
                    { 3, "RANDY@HOTINSTRUCTOR.COM", "RANDY", true, "88883", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Instructor" },
                    { 4, "THOMAS@HOTINSTRUCTOR.COM", "THOMAS", true, "88884", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Instructor" },
                    { 5, "BEN@HOTINSTRUCTOR.COM", "BEN", true, "88885", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Instructor" },
                    { 6, "GABRIEL@HOTINSTRUCTOR.COM", "GABRIEL", true, "88886", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Instructor" },
                    { 7, "FRED@HOTINSTRUCTOR.COM", "FRED", true, "88887", new byte[] { 46, 234, 10, 145, 31, 176, 177, 66, 179, 216, 85, 46, 136, 252, 53, 100, 27, 216, 2, 243, 114, 106, 78, 161, 216, 26, 104, 144, 51, 186, 254, 51, 208, 241, 222, 102, 37, 188, 9, 25, 195, 252, 227, 76, 136, 31, 151, 46, 68, 90, 42, 34, 18, 47, 164, 102, 239, 25, 149, 133, 186, 96, 47, 4 }, new byte[] { 204, 71, 141, 0, 26, 173, 212, 191, 79, 191, 52, 19, 188, 32, 120, 90, 169, 134, 19, 64, 140, 129, 201, 24, 30, 169, 120, 111, 69, 217, 93, 229, 192, 178, 5, 30, 127, 110, 170, 123, 28, 17, 192, 170, 21, 144, 46, 174, 33, 68, 60, 75, 219, 1, 85, 245, 222, 44, 100, 175, 25, 94, 82, 167, 125, 31, 231, 54, 48, 188, 148, 80, 122, 88, 43, 235, 242, 88, 79, 66, 164, 4, 122, 35, 53, 140, 73, 55, 237, 108, 80, 1, 126, 166, 40, 209, 161, 189, 217, 163, 88, 212, 183, 208, 180, 79, 210, 54, 119, 90, 114, 26, 212, 166, 43, 41, 225, 182, 191, 53, 35, 65, 173, 72, 214, 41, 37, 21 }, "Instructor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_CustomerAccountId",
                table: "AccountDetails",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRates_CustomerAccountId",
                table: "AccountRates",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "CustomerAccount_AccountName_UniqueConstraint",
                table: "CustomerAccounts",
                column: "AccountName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_CreatedById",
                table: "CustomerAccounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_UpdatedById",
                table: "CustomerAccounts",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAccounts_CustomerAccountId",
                table: "InstructorAccounts",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAccounts_InstructorId",
                table: "InstructorAccounts",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSynopses_CreatedById",
                table: "SessionSynopses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "SessionSynopsis_SessionSynopsisName_UniqueConstraint",
                table: "SessionSynopses",
                column: "SessionSynopsisName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionSynopses_UpdatedById",
                table: "SessionSynopses",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetails_AccountDetailId",
                table: "TimeSheetDetails",
                column: "AccountDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetails_TimeSheetId",
                table: "TimeSheetDetails",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetailSignature_TimeSheetIDetailId",
                table: "TimeSheetDetailSignature",
                column: "TimeSheetIDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_ApprovedById",
                table: "TimeSheets",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_CreatedById",
                table: "TimeSheets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_InstructorId",
                table: "TimeSheets",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_UpdatedById",
                table: "TimeSheets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "UserInfo_LoginUserName_UniqueConstraint",
                table: "UserInfo",
                column: "LoginUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRates");

            migrationBuilder.DropTable(
                name: "InstructorAccounts");

            migrationBuilder.DropTable(
                name: "SessionSynopses");

            migrationBuilder.DropTable(
                name: "TimeSheetDetailSignature");

            migrationBuilder.DropTable(
                name: "TimeSheetDetails");

            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "CustomerAccounts");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
