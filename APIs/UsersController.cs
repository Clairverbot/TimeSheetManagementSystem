using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using AuthDemo.Helpers;
using AuthDemo.Models;
using AuthDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthDemo.APIs
{
        [ApiController]
        [Route("[controller]")]
        public class UsersController : ControllerBase
        {
            private IUserService _userService;
            private IMapper _mapper;
            private readonly AppSettings _appSettings;

            public UsersController(
                IUserService userService,
                IMapper mapper,
                IOptions<AppSettings> appSettings)
            {
                _userService = userService;
                _mapper = mapper;
                _appSettings = appSettings.Value;
            }

            [AllowAnonymous]
            [HttpPost("signin")]
            public IActionResult SignIn(IFormCollection inFormData)
            {
                var user = _userService.Authenticate(inFormData["username"], inFormData["password"]);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserInfoId.ToString() ),
                        new Claim(ClaimTypes.Role,user.Role),
                        new Claim("username", user.LoginUserName.ToString()),
                        new Claim("userid", user.UserInfoId.ToString()),
                        new Claim("fullname",user.FullName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    user = new
                    {
                        userInfoId = user.UserInfoId,
                        loginUserName = user.LoginUserName,
                        fullName = user.FullName,
                        email = user.Email,
                        role=user.Role
                    },
                    token = tokenString
                });
            }

            [AllowAnonymous]
            [HttpPost("signup")]
            public IActionResult SignUp(IFormCollection inFormData)
            {
            UserInfo newUser = new UserInfo()
            {
                LoginUserName = inFormData["loginUserName"],
                Email = inFormData["email"],
                FullName=inFormData["fullName"],
                IsActive = true
                };
                try
                {
                    // save 
                    _userService.Create(newUser, inFormData["password"]);
                    return Ok(new
                    {
                        signUpStatus = true,
                        message = "Completed user registration"
                    });
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                List<object> userList = new List<object>();
                foreach (UserInfo user in users)
                {
                    userList.Add(new
                    {
                        userInfoId = user.UserInfoId,
                        loginUserName = user.LoginUserName,
                        fullName = user.FullName,
                        email = user.Email
                    });
                }
                return Ok(userList);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var user = _userService.GetById(id);

                return Ok(new
                {
                    userInfoId = id,
                    loginUserName = user.LoginUserName,
                    fullName = user.FullName,
                    email = user.Email
                });
            }

            ////maybe change role
            //[HttpPut("editUser/{id}")]
            //[Consumes("application/x-www-form-urlencoded")]
            //public IActionResult Update(int id, IFormCollection inFormData)
            //{
            //    UserInfo user = new UserInfo()
            //    {
            //        Id = id,
            //        FirstName = inFormData["firstName"],
            //        LastName = inFormData["lastName"],
            //        UserName = inFormData["userName"]
            //    };

            //    try
            //    {
            //        // save 
            //        _userService.Update(user, inFormData["password"]);
            //        return Ok(new { message = "Completed user profile update." });
            //    }
            //    catch (AppException ex)
            //    {
            //        return BadRequest(new { message = ex.Message });
            //    }
            //}

            //[HttpDelete("editUser/{id}")]
            //public IActionResult Delete(int id)
            //{
            //    _userService.Delete(id);
            //    return Ok(new { message = "Completed user deletion" });
            //}
        }
    }
