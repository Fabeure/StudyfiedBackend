using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Authentication
{
    public class UsersController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("allUsers")]
        public BaseResponse<List<ApplicationUser>> getAllUsers()
        {
            return userService.GetAllUsersAsync();
        }
    }
}
