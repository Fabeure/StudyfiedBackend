﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getUserByToken")]
        public BaseResponse<ApplicationUser> getUserByToken(string token)
        {
            return _usersService.getUserByToken(token);
        }

        [HttpDelete("deleteUserById")]
        public PrimitiveBaseResponse<bool> deleteUserById(string id, string token)
        {
            return _usersService.deleteUserById(id, token);
        }

        [HttpPatch("updateUserById")]
        public PrimitiveBaseResponse<bool> updateUserById(string id, ApplicationUser updatedUser, string token)
        {
            return _usersService.updateUserById(id, updatedUser, token);
        }

        [HttpPatch("updatePasswordById")]
        public PrimitiveBaseResponse<bool> updateUserById(string id, string oldPassword, string newPassword)
        {
            return _usersService.updateUserPassword(id, oldPassword, newPassword);
        }
    }
}
