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
                    StartTimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    EndTimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
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
                    { 1, "KENNY@FAIRYSCHOOL.COM", "KENNY", true, "88881", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Admin" },
                    { 2, "JULIET@FAIRYSCHOOL.COM", "JULIET", true, "88882", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Admin" },
                    { 3, "RANDY@HOTINSTRUCTOR.COM", "RANDY", true, "88883", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Instructor" },
                    { 4, "THOMAS@HOTINSTRUCTOR.COM", "THOMAS", true, "88884", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Instructor" },
                    { 5, "BEN@HOTINSTRUCTOR.COM", "BEN", true, "88885", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Instructor" },
                    { 6, "GABRIEL@HOTINSTRUCTOR.COM", "GABRIEL", true, "88886", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Instructor" },
                    { 7, "FRED@HOTINSTRUCTOR.COM", "FRED", true, "88887", new byte[] { 157, 66, 138, 212, 232, 79, 169, 236, 237, 155, 26, 243, 65, 196, 5, 182, 250, 42, 62, 193, 227, 118, 222, 176, 187, 179, 131, 78, 17, 75, 218, 92, 187, 108, 162, 48, 228, 25, 77, 93, 163, 179, 201, 188, 104, 68, 65, 21, 3, 4, 80, 3, 125, 240, 192, 121, 41, 63, 230, 7, 87, 119, 198, 2 }, new byte[] { 17, 231, 174, 4, 154, 193, 62, 54, 54, 1, 85, 88, 253, 120, 114, 129, 12, 171, 156, 22, 159, 183, 41, 124, 30, 31, 64, 224, 229, 252, 170, 238, 117, 49, 77, 23, 207, 110, 112, 248, 195, 237, 254, 117, 79, 73, 36, 4, 213, 245, 53, 36, 8, 23, 143, 109, 218, 29, 193, 13, 219, 196, 78, 211, 121, 34, 197, 100, 249, 140, 185, 115, 11, 69, 167, 172, 53, 232, 94, 175, 70, 233, 221, 142, 224, 4, 37, 102, 60, 55, 3, 153, 168, 40, 167, 202, 18, 53, 237, 117, 238, 152, 20, 214, 4, 118, 125, 35, 147, 141, 57, 198, 79, 9, 204, 143, 148, 185, 101, 32, 248, 156, 232, 91, 156, 6, 4, 243 }, "Instructor" }
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
