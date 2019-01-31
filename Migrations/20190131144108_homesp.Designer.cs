﻿// <auto-generated />
using System;
using AuthDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthDemo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190131144108_homesp")]
    partial class homesp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuthDemo.Models.AccountDetail", b =>
                {
                    b.Property<int>("AccountDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountDetailId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerAccountId");

                    b.Property<int>("DayOfWeekNumber");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("EndTimeInMinutes")
                        .HasColumnName("EndTimeInMinutes")
                        .HasColumnType("time");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsVisible")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<TimeSpan>("StartTimeInMinutes")
                        .HasColumnName("StartTimeInMinutes")
                        .HasColumnType("time");

                    b.HasKey("AccountDetailId")
                        .HasName("PrimaryKey_AccountDetailId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("AuthDemo.Models.AccountRate", b =>
                {
                    b.Property<int>("AccountRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountRateId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerAccountId");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate");

                    b.Property<decimal>("RatePerHour")
                        .HasColumnName("RatePerHour")
                        .HasColumnType("DECIMAL(6,2)");

                    b.HasKey("AccountRateId")
                        .HasName("PrimaryKey_AccountRateId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("AccountRates");
                });

            modelBuilder.Entity("AuthDemo.Models.CustomerAccount", b =>
                {
                    b.Property<int>("CustomerAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerAccountId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnName("AccountName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Comments")
                        .HasColumnName("Comments")
                        .HasColumnType("NVARCHAR(4000)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CreatedById");

                    b.Property<bool>("IsVisible")
                        .HasColumnName("IsVisible")
                        .HasColumnType("BIT");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UpdatedById");

                    b.HasKey("CustomerAccountId")
                        .HasName("PrimaryKey_CustomerAccountId");

                    b.HasIndex("AccountName")
                        .IsUnique()
                        .HasName("CustomerAccount_AccountName_UniqueConstraint");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("CustomerAccounts");
                });

            modelBuilder.Entity("AuthDemo.Models.InstructorAccount", b =>
                {
                    b.Property<int>("InstructorAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InstructorAccountId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnName("Comments")
                        .HasColumnType("NVARCHAR(4000)");

                    b.Property<int>("CustomerAccountId")
                        .HasColumnName("CustomerAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate");

                    b.Property<int>("InstructorId")
                        .HasColumnName("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCurrent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsCurrent")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<decimal>("WageRate")
                        .HasColumnName("WageRate")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("InstructorAccountId")
                        .HasName("PrimaryKey_InstructorAccounttId");

                    b.HasIndex("CustomerAccountId");

                    b.HasIndex("InstructorId");

                    b.ToTable("InstructorAccounts");
                });

            modelBuilder.Entity("AuthDemo.Models.SessionSynopsis", b =>
                {
                    b.Property<int>("SessionSynopsisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SessionSynopsisId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedById");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsVisible")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SessionSynopsisName")
                        .IsRequired()
                        .HasColumnName("SessionSynopsisName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("UpdatedById");

                    b.HasKey("SessionSynopsisId")
                        .HasName("PrimaryKey_SessionSynopsisId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("SessionSynopsisName")
                        .IsUnique()
                        .HasName("SessionSynopsis_SessionSynopsisName_UniqueConstraint");

                    b.HasIndex("UpdatedById");

                    b.ToTable("SessionSynopses");
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheet", b =>
                {
                    b.Property<int>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ApprovedAt");

                    b.Property<int?>("ApprovedById")
                        .IsRequired();

                    b.Property<int>("CheckedById");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CreatedById");

                    b.Property<int>("InstructorId");

                    b.Property<DateTime>("MonthAndYear")
                        .HasColumnName("MonthAndYear");

                    b.Property<decimal>("RatePerHour")
                        .HasColumnName("RatePerHour")
                        .HasColumnType("decimal(6,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UpdatedAt")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UpdatedById")
                        .HasColumnName("UpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VerifiedAndSubmittedAt")
                        .HasColumnName("VerifiedAndSubmittedAt");

                    b.HasKey("TimeSheetId")
                        .HasName("PrimaryKey_TimeSheetId");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InstructorId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheetDetail", b =>
                {
                    b.Property<int>("TimeSheetDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetDetailId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountDetailId");

                    b.Property<DateTime>("DateOfLesson");

                    b.Property<bool>("IsReplacementInstructor")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsReplacementInstructor")
                        .HasDefaultValue(false);

                    b.Property<int>("OfficialTimeInMinutes")
                        .HasColumnName("OfficialTimeInMinutes");

                    b.Property<int>("OfficialTimeOutMinutes")
                        .HasColumnName("OfficialTimeOutMinutes");

                    b.Property<string>("SessionSynopsisNames")
                        .IsRequired()
                        .HasColumnName("SessionSynopsisNames")
                        .HasColumnType("VARCHAR(300)");

                    b.Property<int>("TimeInInMinutes");

                    b.Property<int>("TimeOutInMinutes");

                    b.Property<int>("TimeSheetId");

                    b.Property<decimal>("WageRatePerHour")
                        .HasColumnName("WageRatePerHour")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("TimeSheetDetailId")
                        .HasName("PrimaryKey_TimeSheetDetailId");

                    b.HasIndex("AccountDetailId");

                    b.HasIndex("TimeSheetId");

                    b.ToTable("TimeSheetDetails");
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheetDetailSignature", b =>
                {
                    b.Property<int>("TimeSheetDetailSignatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetDetailSignatureId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Signature")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<int>("TimeSheetIDetailId");

                    b.HasKey("TimeSheetDetailSignatureId")
                        .HasName("PrimaryKey_TimeSheetSignatureId");

                    b.HasIndex("TimeSheetIDetailId")
                        .IsUnique();

                    b.ToTable("TimeSheetDetailSignature");
                });

            modelBuilder.Entity("AuthDemo.Models.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserInfoId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnName("FullName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LoginUserName")
                        .IsRequired()
                        .HasColumnName("LoginUserName")
                        .HasColumnType("VARCHAR(10)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("PasswordHash")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnName("PasswordSalt")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("Role")
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("UserInfoId")
                        .HasName("PrimaryKey_UserInfoId");

                    b.HasIndex("LoginUserName")
                        .IsUnique()
                        .HasName("UserInfo_LoginUserName_UniqueConstraint");

                    b.ToTable("UserInfo");

                    b.HasData(
                        new { UserInfoId = 1, Email = "KENNY@FAIRYSCHOOL.COM", FullName = "KENNY", IsActive = true, LoginUserName = "88881", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Admin" },
                        new { UserInfoId = 2, Email = "JULIET@FAIRYSCHOOL.COM", FullName = "JULIET", IsActive = true, LoginUserName = "88882", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Admin" },
                        new { UserInfoId = 3, Email = "RANDY@HOTINSTRUCTOR.COM", FullName = "RANDY", IsActive = true, LoginUserName = "88883", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Instructor" },
                        new { UserInfoId = 4, Email = "THOMAS@HOTINSTRUCTOR.COM", FullName = "THOMAS", IsActive = true, LoginUserName = "88884", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Instructor" },
                        new { UserInfoId = 5, Email = "BEN@HOTINSTRUCTOR.COM", FullName = "BEN", IsActive = true, LoginUserName = "88885", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Instructor" },
                        new { UserInfoId = 6, Email = "GABRIEL@HOTINSTRUCTOR.COM", FullName = "GABRIEL", IsActive = true, LoginUserName = "88886", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Instructor" },
                        new { UserInfoId = 7, Email = "FRED@HOTINSTRUCTOR.COM", FullName = "FRED", IsActive = true, LoginUserName = "88887", PasswordHash = new byte[] { 154, 12, 9, 150, 114, 52, 100, 234, 129, 185, 166, 218, 90, 88, 79, 221, 177, 53, 212, 251, 56, 212, 246, 32, 38, 191, 53, 46, 148, 201, 59, 33, 74, 35, 35, 0, 182, 190, 12, 96, 169, 73, 20, 92, 242, 39, 249, 137, 9, 159, 162, 106, 252, 42, 17, 218, 109, 69, 251, 44, 122, 46, 218, 85 }, PasswordSalt = new byte[] { 250, 172, 96, 23, 193, 118, 24, 118, 178, 79, 153, 223, 153, 9, 235, 239, 47, 128, 176, 79, 5, 167, 135, 194, 124, 119, 8, 159, 71, 92, 9, 141, 139, 94, 120, 152, 17, 180, 74, 111, 205, 181, 213, 231, 40, 73, 21, 22, 1, 141, 53, 80, 14, 230, 152, 35, 230, 219, 185, 135, 0, 58, 169, 162, 58, 146, 171, 78, 96, 234, 102, 43, 145, 79, 156, 255, 118, 52, 128, 115, 43, 1, 194, 137, 106, 218, 137, 209, 25, 174, 205, 214, 132, 101, 85, 54, 89, 49, 201, 251, 157, 175, 145, 167, 157, 72, 142, 20, 228, 7, 193, 29, 54, 180, 171, 107, 77, 71, 248, 4, 158, 119, 156, 127, 165, 214, 64, 186 }, Role = "Instructor" }
                    );
                });

            modelBuilder.Entity("AuthDemo.Models.AccountDetail", b =>
                {
                    b.HasOne("AuthDemo.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("AccountDetails")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthDemo.Models.AccountRate", b =>
                {
                    b.HasOne("AuthDemo.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("AccountRates")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthDemo.Models.CustomerAccount", b =>
                {
                    b.HasOne("AuthDemo.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AuthDemo.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AuthDemo.Models.InstructorAccount", b =>
                {
                    b.HasOne("AuthDemo.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("InstructorAccounts")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthDemo.Models.UserInfo", "Instructor")
                        .WithMany("InstructorAccounts")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthDemo.Models.SessionSynopsis", b =>
                {
                    b.HasOne("AuthDemo.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AuthDemo.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheet", b =>
                {
                    b.HasOne("AuthDemo.Models.UserInfo", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AuthDemo.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AuthDemo.Models.UserInfo", "Instructor")
                        .WithMany("TimeSheets")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthDemo.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheetDetail", b =>
                {
                    b.HasOne("AuthDemo.Models.AccountDetail", "AccountDetail")
                        .WithMany("TimeSheetDetails")
                        .HasForeignKey("AccountDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AuthDemo.Models.TimeSheet", "TimeSheet")
                        .WithMany("TimeSheetDetails")
                        .HasForeignKey("TimeSheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AuthDemo.Models.TimeSheetDetailSignature", b =>
                {
                    b.HasOne("AuthDemo.Models.TimeSheetDetail", "TimeSheetDetail")
                        .WithOne("TimeSheetDetailSignature")
                        .HasForeignKey("AuthDemo.Models.TimeSheetDetailSignature", "TimeSheetIDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}