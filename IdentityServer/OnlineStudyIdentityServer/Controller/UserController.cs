using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyIdentityServer.Dtos;
using OnlineStudyIdentityServer.Models;

namespace OnlineStudyIdentityServer.Controller
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp( SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City
            };
            var result = await _userManager.CreateAsync(user, signUpDto.Password);
               if (!result.Succeeded)
                {
                    return BadRequest(ResponseDto<NoContent> .Fail(result.Errors.Select(x => x.Description).ToList(), 400));
                }

               return NoContent();

        }
        
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
           var user = User.Claims.FirstOrDefault(x=>x.Type== JwtRegisteredClaimNames.Sub);
           if (user==null)
           {
               return BadRequest();
           }
           var userToReturn = await _userManager.FindByIdAsync(user.Value);
           
           if (userToReturn==null)
           {
               return BadRequest();
           }
           var userDto = new ApplicationUser()
           {
               UserName = userToReturn.UserName,
               Email = userToReturn.Email,
               City = userToReturn.City
           };

           return Ok(userDto);


        }
        
        
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users;
            var usersDto = new List<ApplicationUser>();
            foreach (var user in users)
            {
                var userDto = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    City = user.City,
                    Id = user.Id
                };
                usersDto.Add(userDto);
            }
            return Ok(usersDto);
        }   
    }
}