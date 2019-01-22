using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AuthDemo.Models;
using AuthDemo.Data;
using System.Security.Claims;
using AuthDemo.Services;
using AuthDemo.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthDemo.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class SessionSynopsesController : Controller
    {
        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;


        public SessionSynopsesController(IUserService userService, IOptions<AppSettings> appSettings,IMapper mapper, ApplicationDbContext database)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
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
                var sessionSynopses = from sessionSynopsis in Database.SessionSynopses.Where(x=>x.IsVisible||x.CreatedById==userId)
                                      select new
                                      {
                                          id = sessionSynopsis.SessionSynopsisId,
                                          sessionSynopsisName = sessionSynopsis.SessionSynopsisName,
                                          isVisible = sessionSynopsis.IsVisible,
                                          createdBy = sessionSynopsis.CreatedBy.FullName,
                                          updatedBy = sessionSynopsis.UpdatedBy.FullName
                                      };
            return new JsonResult(sessionSynopses);
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
                var oneSessionSynopsis = Database.SessionSynopses
                .Where(session => session.SessionSynopsisId == id)
                .SingleOrDefault();
                var response = new
                {
                    sessionSynopsisId = oneSessionSynopsis.SessionSynopsisId,
                    sessionSynopsisName = oneSessionSynopsis.SessionSynopsisName,
                    isVisible = oneSessionSynopsis.IsVisible,
                    createdById = oneSessionSynopsis.CreatedById,
                    updatedById = oneSessionSynopsis.UpdatedById,
                };
                return new JsonResult(response);
            }
            return null;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromForm]IFormCollection inFormData )
        {
            string returnMessage = "";
            SessionSynopsis a = new SessionSynopsis();
            a.SessionSynopsisName = inFormData["sessionSynopsisName"];

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                SessionSynopsis newSessionSynopsis = new SessionSynopsis();
                try
                {
                    newSessionSynopsis.SessionSynopsisName = inFormData["sessionSynopsisName"];
                    newSessionSynopsis.IsVisible = Convert.ToBoolean(inFormData["isVisible"]);
                    newSessionSynopsis.CreatedById = userId;
                    newSessionSynopsis.UpdatedById = userId;
                    Database.SessionSynopses.Add(newSessionSynopsis);
                    Database.SaveChanges();
                }
                catch (Exception exceptionObject)
                {
                    if (exceptionObject.InnerException.Message.Contains("SessionSynopsis_SessionSynopsisName_UniqueConstraint") == true)
                    {
                        returnMessage = "yo we screwed up yo";
                    }

                }
                var successRequestResultMessage = new
                {
                    message = returnMessage == "" ? "Saved session synopsis record" : returnMessage,
                    ok = returnMessage == ""
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
                var oneSessionSynopsis = Database.SessionSynopses
                    .Where(session => session.SessionSynopsisId == id).SingleOrDefault();
                oneSessionSynopsis.SessionSynopsisName = value["sessionSynopsisName"];
                oneSessionSynopsis.IsVisible = Convert.ToBoolean(value["isVisible"]);
                oneSessionSynopsis.UpdatedById = userId;
                try
                {
                    Database.Update(oneSessionSynopsis);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                        customMsg = "Unable to save session synopsis due to anotehr record having the same session synopsis name: " + value["sessionSynopsisName"];
                        object msg = new { message = customMsg };
                        return BadRequest(msg);
                    
                }
                var successRequestResultMessage = new
                {
                    message ="Updated Session Synopsis \nName: " + value["sessionSynopsisName"]
                    + "\nVisibility: " + value["isVisible"]
                };
                OkObjectResult okObjectResult = new OkObjectResult(successRequestResultMessage);
                return okObjectResult;
            }
            return null;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string customMsg = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsPrincipal user = HttpContext.User;
            if (identity != null)
            {
                var delSessionSynopsis = Database.SessionSynopses.Where(session => session.SessionSynopsisId == id).SingleOrDefault();
                try
                {
                    Database.SessionSynopses.Remove(delSessionSynopsis);
                    Database.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    customMsg = "Unable to delete session synopsis due to : " + ex.Message;
                    object msg = new { message = customMsg };
                    return BadRequest(msg);
                }
                var successRequestResultMessage = new
                {
                    message = "DELETE Web API method has executed. "
                };
                OkObjectResult okObjectResult = new OkObjectResult(successRequestResultMessage);
                return okObjectResult;
            }
            return null;
        }


    }
}
