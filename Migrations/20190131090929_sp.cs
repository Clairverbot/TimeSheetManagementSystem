using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthDemo.Migrations
{
    public partial class sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAccountRatesByDate]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT [accountRate].[AccountRateId],[accountRate].[CustomerAccountId], [accountRate].[RatePerHour] AS [ratePerHour], [accountRate].[EffectiveStartDate] AS [effectiveStartDate], [accountRate].[EffectiveEndDate] AS [effectiveEndDate]
                    FROM [AccountRates] AS [accountRate]
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {}
    }
}
