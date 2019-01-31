using AuthDemo.Data;
using AuthDemo.Helpers;
using AuthDemo.Models;
using AuthDemo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthDemo.APIs
{
    public interface IAccountDetailsController
    {
        IActionResult Get();
        IActionResult Get(int id);
        IActionResult Post(int id, [FromForm]AccountDetail value);
        IActionResult Put(int id, [FromForm]AccountDetail value);
        IActionResult Delete(int id);
    }
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountDetailsController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }

        public AccountDetailsController(
                ApplicationDbContext database,
                IUserService userService,
                IOptions<AppSettings> appSettings)
        {
            Database = database;
            _userService = userService;
            _appSettings = appSettings.Value;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                List<object> customerAccountsList = new List<object>();
                var customerAccounts = from customerAccount in Database.CustomerAccounts.Where(x => x.IsVisible || x.CreatedBy.Role == "Admin")
                                       select new
                                       {
                                           customerAccountId = customerAccount.CustomerAccountId,
                                           accountName = customerAccount.AccountName,
                                           accountDetails = customerAccount.AccountDetails,
                                           accountRate = customerAccount.AccountRates
                                       };
                return new JsonResult(customerAccounts);
            }
            return null;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var accountDetail = Database.AccountDetails.Where(x => x.AccountDetailId == id).SingleOrDefault();
                var response = new
                {
                    dayOfWeekNumber = accountDetail.DayOfWeekNumber,
                    startTimeInMinutes = accountDetail.StartTimeInMinutes,
                    endTimeInMinutes = accountDetail.EndTimeInMinutes,
                    effectiveStartDate = accountDetail.EffectiveStartDate,
                    effectiveEndDate = accountDetail.EffectiveEndDate,
                    isVisible = accountDetail.IsVisible,
                    customerAccountId = accountDetail.CustomerAccountId,
                    
                };
                return new JsonResult(response);
            }
            return null;
        }
        [HttpGet("GetByCustAcc/{id}")]
        public IActionResult GetByCustAcc(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var accountDetails = from accountDetail in Database.AccountDetails.Where(x => x.CustomerAccountId == id)
                                     select new
                                     {
                                         dayOfWeekNumber = accountDetail.DayOfWeekNumber,
                                         startTimeInMinutes = accountDetail.StartTimeInMinutes,
                                         endTimeInMinutes = accountDetail.EndTimeInMinutes,
                                         effectiveStartDate = accountDetail.EffectiveStartDate,
                                         effectiveEndDate = accountDetail.EffectiveEndDate,
                                         isVisible = accountDetail.IsVisible,
                                     };
                                      

                return new JsonResult(accountDetails);
            }
            return null;
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromForm]AccountDetail value)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                try
                {
                    value.AccountDetailId = id;
                    Database.Update(value);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    object httpFailRequestResultMessage = new { message = ex.InnerException.Message };
                    return BadRequest(httpFailRequestResultMessage);

                }
                var successRequestResultMessage = new
                {
                    message = "Updated account details record"
                };
                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return null;
        }
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromForm]AccountDetail value)
        {
            DateTime EffectiveEndDate = DateTime.Parse(value.EffectiveEndDate.ToString());
            if (EffectiveEndDate != null) {
                if (DateTime.Compare(value.EffectiveStartDate, EffectiveEndDate) > 0)
                {
                    object httpFailRequestResultMessage = new { message = "Effective Start Date is earlier than Effective End Date" };
                    return BadRequest(httpFailRequestResultMessage);
                }
            }
            if (TimeSpan.Compare(value.StartTimeInMinutes,value.EndTimeInMinutes)>0)
            {
                object httpFailRequestResultMessage = new { message = "Start Time is earlier than End Time" };
                return BadRequest(httpFailRequestResultMessage);
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                try
                {
                    value.CustomerAccountId = id;
                    Database.AccountDetails.Add(value);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    object httpFailRequestResultMessage = new { message = ex.InnerException.Message };
                    return BadRequest(httpFailRequestResultMessage);

                }
                var successRequestResultMessage = new
                {
                    message = "Saved account details record"
                };
                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
        return null;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                
                var delAccRate = Database.AccountDetails.Where(acc => acc.AccountDetailId == id).SingleOrDefault();
                try
                {
                    Database.AccountDetails.Remove(delAccRate);
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
