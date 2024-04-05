using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StudyfiedBackend.Controllers.Authentication
{
    public class UsersController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("test")]
        public BaseResponse<ApplicationUser> test(string token)
        {
            return userService.AuthenticateTokenAndGetUser(token);
        }
    }
}
