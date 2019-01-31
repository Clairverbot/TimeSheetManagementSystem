using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthDemo.Migrations
{
    public partial class homesp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp1= @"CREATE PROCEDURE [dbo].[CountActiveCust]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    Select Count(c.CustomerAccountId)
                    from AccountRates as a, CustomerAccounts as c
                    Where a.CustomerAccountId=c.CustomerAccountId
                    And EffectiveStartDate<=GETDATE() 
                    And EffectiveEndDate>=GETDATE()
                END";
            var sp2 = @"CREATE PROCEDURE [dbo].[CountExpiringRates]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    Select Count(AccountRateId)
                    from AccountRates
                    WHERE DATEDIFF(day,cast(GETDATE() as datetime2),EffectiveEndDate)<=14
                END";
            var sp3 = @"CREATE PROCEDURE [dbo].[CountExpiringDetails]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    Select Count(AccountDetailId)
                    from AccountDetails
                    WHERE DATEDIFF(day,cast(GETDATE() as date),EffectiveEndDate)<=14
                END";
            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(sp3);

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
