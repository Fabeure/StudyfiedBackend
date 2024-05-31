using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public class UsersController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("getUserById")]
        public BaseResponse<ApplicationUser> getUserById(string id)
        {
            return _usersService.getUserById(id);
        }
    }
}
