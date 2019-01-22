using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Services;
using AuthDemo.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthDemo.APIs
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    public class CustomerAccountsController : Controller
    {

        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }
        private IUserService _userService;
        private readonly AppSettings _appSettings;


        public CustomerAccountsController(IUserService userService, IOptions<AppSettings> appSettings, ApplicationDbContext database)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            Database = database;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                userId = Int32.Parse(identity.FindFirst("userid").Value);
                List<object> customerAccountsList = new List<object>();
                var customerAccounts = from customerAccount in Database.CustomerAccounts.Where(x => x.IsVisible || x.CreatedById == userId)
                                       select new
                                       {
                                           customerAccountId = customerAccount.CustomerAccountId,
                                           accountName = customerAccount.AccountName,
                                           comments = customerAccount.Comments,
                                           createdAt = customerAccount.CreatedAt,
                                           createdBy = customerAccount.CreatedBy.FullName,
                                           updatedAt = customerAccount.UpdatedAt,
                                           updatedBy = customerAccount.UpdatedBy.FullName,
                                           isVisible = customerAccount.IsVisible,
                                           accountRates = customerAccount.AccountRates
                                           //.Where(accountRate => accountRate.EffectiveStartDate <= DateTime.Today),
                                       };

                return new JsonResult(customerAccounts);
            }
            return null;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                var oneCustomerAccount = Database.CustomerAccounts
                .Where(customerAccount => customerAccount.CustomerAccountId == id)
                .SingleOrDefault();
                var response = new
                {
                    customerAccountId = oneCustomerAccount.CustomerAccountId,
                    customerAccountName = oneCustomerAccount.AccountName,
                    isVisible = oneCustomerAccount.IsVisible,
                    comments = oneCustomerAccount.Comments,
                };
                return new JsonResult(response);
            }
            return null;
        }

        [HttpGet("GetCustAccByDate")]
        public JsonResult GetCustAccByDate()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var custAcc = from c in Database.CustomerAccounts
                .Where(customerAccount => DateTime.Compare(DateTime.Parse(customerAccount.CreatedAt.ToString()), DateTime.Today.AddDays(3.0)) <= 0 && customerAccount.IsVisible)
                              select new
                              {
                                  customerAccountId = c.CustomerAccountId,
                                  accountName = c.AccountName,
                                  createdBy = c.CreatedBy.UserInfoId==userId?"You":c.CreatedBy.FullName,
                                  date = c.CreatedAt
                              };
                ;
                
                return new JsonResult(custAcc);
            }
            return null;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromForm]IFormCollection value)
        {
            string exMsg = "";
            CustomerAccount newCustomerAccount = new CustomerAccount();
            AccountRate newAccountRate = new AccountRate();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                try
                {
                    newCustomerAccount.AccountName = value["accountName"];
                    newCustomerAccount.IsVisible = Convert.ToBoolean(value["isVisible"]);
                    newCustomerAccount.Comments = value["comments"];
                    newCustomerAccount.CreatedAt = DateTime.Today;
                    newCustomerAccount.UpdatedAt = DateTime.Today;
                    newCustomerAccount.CreatedById = userId;
                    newCustomerAccount.UpdatedById = userId;
                    Database.CustomerAccounts.Add(newCustomerAccount);

                    Database.SaveChanges();

                    if (value["ratePerHour"] != "")
                    {
                    newAccountRate.CustomerAccountId = newCustomerAccount.CustomerAccountId;
                    newAccountRate.RatePerHour = int.Parse(value["ratePerHour"]);
                    newAccountRate.EffectiveStartDate = DateTime.Parse(value["effectiveStartDate"]);
                    newAccountRate.EffectiveEndDate = DateTime.Parse(value["effectiveEndDate"]);
                    Database.AccountRates.Add(newAccountRate);
                    Database.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("CustomerAccount_CustomerAccountName_UniqueConstraint") == true)
                    {
                        exMsg = "Unable to save customer account record due to :" + ex.Message;
                    object httpFailRequestResultMessage = new { message = exMsg };
                    return BadRequest(httpFailRequestResultMessage);
                    }
                }
                var successRequestResultMessage = new
                {
                    message = "Saved customer account record" + newCustomerAccount.AccountName
                };
                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return null;
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
                var oneCustAcc = Database.CustomerAccounts.Where(acc => acc.CustomerAccountId == id).SingleOrDefault();
                oneCustAcc.AccountName = value["accountName"];
                oneCustAcc.IsVisible = Convert.ToBoolean(value["isVisible"]);
                oneCustAcc.Comments = value["comment"];
                oneCustAcc.UpdatedAt = DateTime.Today;
                oneCustAcc.UpdatedById = userId;

                try
                {
                    Database.Update(oneCustAcc);
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
        //delete associated account rate oso
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
                var delCustAcc = Database.CustomerAccounts.Where(acc => acc.CustomerAccountId == id).SingleOrDefault();
                var delAccRates = Database.AccountRates.Where(rates => rates.CustomerAccountId == id);
                try
                {
                    Database.CustomerAccounts.Remove(delCustAcc);
                    foreach (var accRate in delAccRates)
                    {
                        Database.AccountRates.Remove(accRate);
                    }
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
