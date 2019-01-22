using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AuthDemo.Models;
using AuthDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Specialized;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthDemo.APIs
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    public class AccountRatesController : Controller
    {

        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }

        public AccountRatesController(ApplicationDbContext database)
        {
            Database = database;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var accountRates = from accountRate in Database.AccountRates
                               select new
                               {
                                   accountRateId = accountRate.AccountRateId,
                                   customerAccountId = accountRate.CustomerAccountId,
                                   ratePerHour = accountRate.RatePerHour,
                                   effectiveStartDate = accountRate.EffectiveStartDate,
                                   effectiveEndDate = accountRate.EffectiveEndDate
                               };
            return new JsonResult(accountRates);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                var accountRate = Database.AccountRates
                .Where(x => x.AccountRateId== id)
                .SingleOrDefault();
                var response = new
                {
                    accountRateId = accountRate.AccountRateId,
                    customerAccountId = accountRate.CustomerAccountId,
                    ratePerHour = accountRate.RatePerHour,
                    effectiveStartDate = accountRate.EffectiveStartDate,
                    effectiveEndDate = accountRate.EffectiveEndDate,
                };
                return new JsonResult(response);
            }
            return null;
        }

        //GET by customer account
        [HttpGet("GetDatesByCustId/{id}")]
        public JsonResult GetDatesByCustId(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                var accountRates = from accountRate in Database.AccountRates
                    .Where(x => x.CustomerAccountId == id)
                                   select new
                                   {
                                       ratePerHour=accountRate.RatePerHour,
                                       effectiveStartDate = accountRate.EffectiveStartDate.Year + "" + accountRate.EffectiveStartDate.Month.ToString("#00"),
                                       effectiveEndDate = (DateTime)accountRate.EffectiveEndDate != null ?
                                       DateTime.Parse(accountRate.EffectiveEndDate.ToString()).Year + "" + DateTime.Parse(accountRate.EffectiveEndDate.ToString()).Month.ToString("#00")
                                       : DateTime.MaxValue.Year + "" + DateTime.MaxValue.Month
                                   };
                return new JsonResult(accountRates);
            }
            return null;
        }

        [HttpGet("GetTotalEarnings")]
        public IActionResult GetTotalEarnings()
        {
            var accountRates = from accountRate in Database.AccountRates
                               select new
                               {
                                   ratePerHour = accountRate.RatePerHour.ToString(),
                                   effectiveStartDate = accountRate.EffectiveStartDate.ToString(),
                                   effectiveEndDate = accountRate.EffectiveEndDate.ToString()
                               };
            if (accountRates.Count() != 0)
            {
                DateTime d1 = accountRates.Min(a => DateTime.Parse(a.effectiveStartDate));
                DateTime d2 = accountRates.Max(a => DateTime.Parse(a.effectiveEndDate));
                var monthlist = new List<DateTime>();
                var monthRate = new ListDictionary();
                string format = d1.Year == d2.Year ? "MMMM" : "MMMM yyyy";
                for (var d = d1; d <= d2; d = d.AddMonths(1))
                {
                    monthlist.Add(d);
                }
                for (var i = 0; i < monthlist.Count(); i++)
                {
                    decimal rate = 0;
                    foreach (var r in accountRates)
                    {
                        if (monthlist[i].CompareTo(DateTime.Parse(r.effectiveStartDate)) >= 0 && monthlist[i].CompareTo(DateTime.Parse(r.effectiveEndDate)) <= 0)
                        {
                            rate += decimal.Parse(r.ratePerHour);
                        }
                    }
                    monthRate.Add(monthlist[i], rate);
                }

                return new JsonResult(monthRate);
            }
            return null;
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromForm]IFormCollection value)
        {
            AccountRate newAccountRate = new AccountRate();
            try
            {
                newAccountRate.CustomerAccountId = Convert.ToInt32(value["customerAccountId"]);
                newAccountRate.RatePerHour = decimal.Parse(value["ratePerHour"]);
                newAccountRate.EffectiveStartDate = DateTime.Parse(value["effectiveStartDate"]);
                newAccountRate.EffectiveEndDate = DateTime.Parse(value["effectiveEndDate"]);
                Database.AccountRates.Add(newAccountRate);
                Database.SaveChanges();
            }catch(Exception ex)
            {
                object httpFailRequestResultMessage = new { message = ex.Message };
                return BadRequest(httpFailRequestResultMessage);

            }
            var successRequestResultMessage = new
            {
                message = "Saved account rate record"
            };
            OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
            return httpOkResult;

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm]IFormCollection value)
        {
            string customMsg = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var oneAccRate = Database.AccountRates.Where(x => x.AccountRateId== id).SingleOrDefault();
                oneAccRate.RatePerHour= decimal.Parse(value["ratePerHour"]);
                oneAccRate.EffectiveStartDate = DateTime.Parse(value["effectiveStartDate"]);
                oneAccRate.EffectiveEndDate = DateTime.Parse(value["effectiveEndDate"]);

                try
                {
                    Database.Update(oneAccRate);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    customMsg = ex.Message;
                    object msg = new { message = customMsg };
                    return BadRequest(msg);
                }
                var successRequestResultMessage = new { message = "success yo" };
                OkObjectResult okObjectResult = new OkObjectResult(successRequestResultMessage);
                return okObjectResult;
            }
            return null;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var delAccRates = Database.AccountRates.Where(rates => rates.AccountRateId == id).SingleOrDefault();
                try
                {
                    Database.AccountRates.Remove(delAccRates);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    object msg = new { message = ex.Message };
                    return BadRequest(msg);
                }
                var successRequestResultMessage = new
                {
                    message = "delete liao"
                };
                return new OkObjectResult(successRequestResultMessage);
            }
            return null;
        }
    }
}
