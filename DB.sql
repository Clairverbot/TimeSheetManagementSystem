USE [master]
GO
/****** Object:  Database [AuthDemoDB_V4]    Script Date: 2/1/2019 3:05:17 AM ******/
CREATE DATABASE [AuthDemoDB_V4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AuthDemoDB_V4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AuthDemoDB_V4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AuthDemoDB_V4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AuthDemoDB_V4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AuthDemoDB_V4] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AuthDemoDB_V4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AuthDemoDB_V4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET ARITHABORT OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AuthDemoDB_V4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AuthDemoDB_V4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AuthDemoDB_V4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AuthDemoDB_V4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AuthDemoDB_V4] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AuthDemoDB_V4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AuthDemoDB_V4] SET  MULTI_USER 
GO
ALTER DATABASE [AuthDemoDB_V4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AuthDemoDB_V4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AuthDemoDB_V4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AuthDemoDB_V4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AuthDemoDB_V4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AuthDemoDB_V4] SET QUERY_STORE = OFF
GO
USE [AuthDemoDB_V4]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [AuthDemoDB_V4]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountDetails]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetails](
	[AccountDetailId] [int] IDENTITY(1,1) NOT NULL,
	[DayOfWeekNumber] [int] NOT NULL,
	[StartTimeInMinutes] [time](7) NOT NULL,
	[EndTimeInMinutes] [time](7) NOT NULL,
	[EffectiveStartDate] [date] NOT NULL,
	[EffectiveEndDate] [date] NULL,
	[IsVisible] [bit] NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_AccountDetailId] PRIMARY KEY CLUSTERED 
(
	[AccountDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountRates]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRates](
	[AccountRateId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
	[RatePerHour] [decimal](6, 2) NOT NULL,
	[EffectiveStartDate] [datetime2](7) NOT NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
 CONSTRAINT [PrimaryKey_AccountRateId] PRIMARY KEY CLUSTERED 
(
	[AccountRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAccounts]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAccounts](
	[CustomerAccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[Comments] [nvarchar](4000) NULL,
	[IsVisible] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UpdatedById] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_CustomerAccountId] PRIMARY KEY CLUSTERED 
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstructorAccounts]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstructorAccounts](
	[InstructorAccountId] [int] IDENTITY(1,1) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
	[Comments] [nvarchar](4000) NULL,
	[WageRate] [decimal](5, 2) NOT NULL,
	[EffectiveStartDate] [datetime2](7) NOT NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
	[IsCurrent] [bit] NOT NULL,
 CONSTRAINT [PrimaryKey_InstructorAccounttId] PRIMARY KEY CLUSTERED 
(
	[InstructorAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionSynopses]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionSynopses](
	[SessionSynopsisId] [int] IDENTITY(1,1) NOT NULL,
	[SessionSynopsisName] [varchar](100) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PrimaryKey_SessionSynopsisId] PRIMARY KEY CLUSTERED 
(
	[SessionSynopsisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheetDetails]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheetDetails](
	[TimeSheetDetailId] [int] IDENTITY(1,1) NOT NULL,
	[TimeSheetId] [int] NOT NULL,
	[AccountDetailId] [int] NOT NULL,
	[SessionSynopsisNames] [varchar](300) NOT NULL,
	[TimeInInMinutes] [int] NOT NULL,
	[TimeOutInMinutes] [int] NOT NULL,
	[DateOfLesson] [datetime2](7) NOT NULL,
	[IsReplacementInstructor] [bit] NOT NULL,
	[WageRatePerHour] [decimal](6, 2) NOT NULL,
	[OfficialTimeInMinutes] [int] NOT NULL,
	[OfficialTimeOutMinutes] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_TimeSheetDetailId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheetDetailSignature]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheetDetailSignature](
	[TimeSheetDetailSignatureId] [int] IDENTITY(1,1) NOT NULL,
	[Signature] [varbinary](max) NULL,
	[TimeSheetIDetailId] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_TimeSheetSignatureId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetDetailSignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheets]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheets](
	[TimeSheetId] [int] IDENTITY(1,1) NOT NULL,
	[MonthAndYear] [datetime2](7) NOT NULL,
	[RatePerHour] [decimal](6, 2) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[VerifiedAndSubmittedAt] [datetime2](7) NULL,
	[CheckedById] [int] NOT NULL,
	[ApprovedById] [int] NOT NULL,
	[ApprovedAt] [datetime2](7) NULL,
 CONSTRAINT [PrimaryKey_TimeSheetId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2/1/2019 3:05:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserInfoId] [int] IDENTITY(1,1) NOT NULL,
	[LoginUserName] [varchar](10) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Role] [varchar](10) NOT NULL,
 CONSTRAINT [PrimaryKey_UserInfoId] PRIMARY KEY CLUSTERED 
(
	[UserInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190125030729_InitialCreate', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190131075728_sp-GetTotalEarnings', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190131082701_storedProcedure', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190131090929_sp', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190131144108_homesp', N'2.1.4-rtm-31024')
SET IDENTITY_INSERT [dbo].[AccountDetails] ON 

INSERT [dbo].[AccountDetails] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (10, 1, CAST(N'13:00:00' AS Time), CAST(N'14:00:00' AS Time), CAST(N'2019-01-01' AS Date), CAST(N'2019-06-30' AS Date), 1, 1)
INSERT [dbo].[AccountDetails] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (11, 2, CAST(N'13:00:00' AS Time), CAST(N'14:00:00' AS Time), CAST(N'2019-01-01' AS Date), CAST(N'2019-08-30' AS Date), 1, 1)
INSERT [dbo].[AccountDetails] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (21, 1, CAST(N'15:15:00' AS Time), CAST(N'16:15:00' AS Time), CAST(N'2019-01-17' AS Date), CAST(N'2019-01-31' AS Date), 0, 1)
INSERT [dbo].[AccountDetails] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (22, 1, CAST(N'13:15:00' AS Time), CAST(N'14:15:00' AS Time), CAST(N'2019-01-17' AS Date), CAST(N'2019-01-31' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[AccountDetails] OFF
SET IDENTITY_INSERT [dbo].[AccountRates] ON 

INSERT [dbo].[AccountRates] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (5, 1, CAST(60.00 AS Decimal(6, 2)), CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-03-31T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRates] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (6, 1, CAST(70.00 AS Decimal(6, 2)), CAST(N'2019-04-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-30T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRates] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (7, 2, CAST(30.00 AS Decimal(6, 2)), CAST(N'2019-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-30T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRates] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (8, 3, CAST(80.00 AS Decimal(6, 2)), CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-05-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[AccountRates] OFF
SET IDENTITY_INSERT [dbo].[CustomerAccounts] ON 

INSERT [dbo].[CustomerAccounts] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (1, N'Sec School A', N'dunno', 1, CAST(N'2019-01-25T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-01-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[CustomerAccounts] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (2, N'Sec School B', N'BBBBBBBBBBBBBb', 0, CAST(N'2019-01-25T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-01-28T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[CustomerAccounts] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (3, N'Sec School C', N'Classes are cancelled during chinese new year', 1, CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[CustomerAccounts] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (1, N'88881', N'KENNY', N'KENNY@FAIRYSCHOOL.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Admin')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (2, N'88882', N'JULIET', N'JULIET@FAIRYSCHOOL.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Admin')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (3, N'88883', N'RANDY', N'RANDY@HOTINSTRUCTOR.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Instructor')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (4, N'88884', N'THOMAS', N'THOMAS@HOTINSTRUCTOR.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Instructor')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (5, N'88885', N'BEN', N'BEN@HOTINSTRUCTOR.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Instructor')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (6, N'88886', N'GABRIEL', N'GABRIEL@HOTINSTRUCTOR.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Instructor')
INSERT [dbo].[UserInfo] ([UserInfoId], [LoginUserName], [FullName], [Email], [IsActive], [PasswordHash], [PasswordSalt], [Role]) VALUES (7, N'88887', N'FRED', N'FRED@HOTINSTRUCTOR.COM', 1, 0x2EEA0A911FB0B142B3D8552E88FC35641BD802F3726A4EA1D81A689033BAFE33D0F1DE6625BC0919C3FCE34C881F972E445A2A22122FA466EF199585BA602F04, 0xCC478D001AADD4BF4FBF3413BC20785AA98613408C81C9181EA9786F45D95DE5C0B2051E7F6EAA7B1C11C0AA15902EAE21443C4BDB0155F5DE2C64AF195E52A77D1FE73630BC94507A582BEBF2584F42A4047A23358C4937ED6C50017EA628D1A1BDD9A358D4B7D0B44FD236775A721AD4A62B29E1B6BF352341AD48D6292515, N'Instructor')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Index [IX_AccountDetails_CustomerAccountId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AccountDetails_CustomerAccountId] ON [dbo].[AccountDetails]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountRates_CustomerAccountId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AccountRates_CustomerAccountId] ON [dbo].[AccountRates]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CustomerAccount_AccountName_UniqueConstraint]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [CustomerAccount_AccountName_UniqueConstraint] ON [dbo].[CustomerAccounts]
(
	[AccountName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAccounts_CreatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAccounts_CreatedById] ON [dbo].[CustomerAccounts]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAccounts_UpdatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAccounts_UpdatedById] ON [dbo].[CustomerAccounts]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstructorAccounts_CustomerAccountId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_InstructorAccounts_CustomerAccountId] ON [dbo].[InstructorAccounts]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstructorAccounts_InstructorId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_InstructorAccounts_InstructorId] ON [dbo].[InstructorAccounts]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionSynopses_CreatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_SessionSynopses_CreatedById] ON [dbo].[SessionSynopses]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionSynopses_UpdatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_SessionSynopses_UpdatedById] ON [dbo].[SessionSynopses]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [SessionSynopsis_SessionSynopsisName_UniqueConstraint]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [SessionSynopsis_SessionSynopsisName_UniqueConstraint] ON [dbo].[SessionSynopses]
(
	[SessionSynopsisName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetails_AccountDetailId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheetDetails_AccountDetailId] ON [dbo].[TimeSheetDetails]
(
	[AccountDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetails_TimeSheetId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheetDetails_TimeSheetId] ON [dbo].[TimeSheetDetails]
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetailSignature_TimeSheetIDetailId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TimeSheetDetailSignature_TimeSheetIDetailId] ON [dbo].[TimeSheetDetailSignature]
(
	[TimeSheetIDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheets_ApprovedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheets_ApprovedById] ON [dbo].[TimeSheets]
(
	[ApprovedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheets_CreatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheets_CreatedById] ON [dbo].[TimeSheets]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheets_InstructorId]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheets_InstructorId] ON [dbo].[TimeSheets]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheets_UpdatedById]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheets_UpdatedById] ON [dbo].[TimeSheets]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserInfo_LoginUserName_UniqueConstraint]    Script Date: 2/1/2019 3:05:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserInfo_LoginUserName_UniqueConstraint] ON [dbo].[UserInfo]
(
	[LoginUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT ((1)) FOR [IsVisible]
GO
ALTER TABLE [dbo].[CustomerAccounts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CustomerAccounts] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[InstructorAccounts] ADD  DEFAULT ((1)) FOR [IsCurrent]
GO
ALTER TABLE [dbo].[SessionSynopses] ADD  DEFAULT ((0)) FOR [IsVisible]
GO
ALTER TABLE [dbo].[TimeSheetDetails] ADD  DEFAULT ((0)) FOR [IsReplacementInstructor]
GO
ALTER TABLE [dbo].[TimeSheets] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TimeSheets] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_CustomerAccounts_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccounts] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_CustomerAccounts_CustomerAccountId]
GO
ALTER TABLE [dbo].[AccountRates]  WITH CHECK ADD  CONSTRAINT [FK_AccountRates_CustomerAccounts_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccounts] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountRates] CHECK CONSTRAINT [FK_AccountRates_CustomerAccounts_CustomerAccountId]
GO
ALTER TABLE [dbo].[CustomerAccounts]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAccounts_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[CustomerAccounts] CHECK CONSTRAINT [FK_CustomerAccounts_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[CustomerAccounts]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAccounts_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[CustomerAccounts] CHECK CONSTRAINT [FK_CustomerAccounts_UserInfo_UpdatedById]
GO
ALTER TABLE [dbo].[InstructorAccounts]  WITH CHECK ADD  CONSTRAINT [FK_InstructorAccounts_CustomerAccounts_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccounts] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstructorAccounts] CHECK CONSTRAINT [FK_InstructorAccounts_CustomerAccounts_CustomerAccountId]
GO
ALTER TABLE [dbo].[InstructorAccounts]  WITH CHECK ADD  CONSTRAINT [FK_InstructorAccounts_UserInfo_InstructorId] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstructorAccounts] CHECK CONSTRAINT [FK_InstructorAccounts_UserInfo_InstructorId]
GO
ALTER TABLE [dbo].[SessionSynopses]  WITH CHECK ADD  CONSTRAINT [FK_SessionSynopses_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[SessionSynopses] CHECK CONSTRAINT [FK_SessionSynopses_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[SessionSynopses]  WITH CHECK ADD  CONSTRAINT [FK_SessionSynopses_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[SessionSynopses] CHECK CONSTRAINT [FK_SessionSynopses_UserInfo_UpdatedById]
GO
ALTER TABLE [dbo].[TimeSheetDetails]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetails_AccountDetails_AccountDetailId] FOREIGN KEY([AccountDetailId])
REFERENCES [dbo].[AccountDetails] ([AccountDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetails] CHECK CONSTRAINT [FK_TimeSheetDetails_AccountDetails_AccountDetailId]
GO
ALTER TABLE [dbo].[TimeSheetDetails]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetails_TimeSheets_TimeSheetId] FOREIGN KEY([TimeSheetId])
REFERENCES [dbo].[TimeSheets] ([TimeSheetId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetails] CHECK CONSTRAINT [FK_TimeSheetDetails_TimeSheets_TimeSheetId]
GO
ALTER TABLE [dbo].[TimeSheetDetailSignature]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetailSignature_TimeSheetDetails_TimeSheetIDetailId] FOREIGN KEY([TimeSheetIDetailId])
REFERENCES [dbo].[TimeSheetDetails] ([TimeSheetDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetailSignature] CHECK CONSTRAINT [FK_TimeSheetDetailSignature_TimeSheetDetails_TimeSheetIDetailId]
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_UserInfo_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_UserInfo_ApprovedById]
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_UserInfo_InstructorId] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_UserInfo_InstructorId]
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([UserInfoId])
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_UserInfo_UpdatedById]
GO
/****** Object:  StoredProcedure [dbo].[CountActiveCust]    Script Date: 2/1/2019 3:05:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountActiveCust]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    return Select Count(c.CustomerAccountId)
                    from AccountRates as a, CustomerAccounts as c
                    Where a.CustomerAccountId=c.CustomerAccountId
                    And EffectiveStartDate<=GETDATE() 
                    And EffectiveEndDate>=GETDATE()
                END
GO
/****** Object:  StoredProcedure [dbo].[CountExpiringDetails]    Script Date: 2/1/2019 3:05:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountExpiringDetails]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    return Select Count(AccountDetailId)
                    from AccountDetails
                    WHERE DATEDIFF(day,cast(GETDATE() as date),EffectiveEndDate)<=14
                END
GO
/****** Object:  StoredProcedure [dbo].[CountExpiringRates]    Script Date: 2/1/2019 3:05:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountExpiringRates]
                AS
                BEGIN
                    return SET NOCOUNT ON;
                    Select Count(AccountRateId)
                    from AccountRates
                    WHERE DATEDIFF(day,cast(GETDATE() as datetime2),EffectiveEndDate)<=14
                END
GO
/****** Object:  StoredProcedure [dbo].[GetAccountRatesByDate]    Script Date: 2/1/2019 3:05:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAccountRatesByDate]
                AS
                BEGIN
                    SELECT [accountRate].[CustomerAccountId],[AccountRateId], [accountRate].[RatePerHour] AS [ratePerHour], [accountRate].[EffectiveStartDate] AS [effectiveStartDate], [accountRate].[EffectiveEndDate] AS [effectiveEndDate]
                    FROM [AccountRates] AS [accountRate]
                END
GO
USE [master]
GO
ALTER DATABASE [AuthDemoDB_V4] SET  READ_WRITE 
GO
