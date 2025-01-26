﻿using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public interface IUsersService
    {
        public BaseResponse<ApplicationUser> getUserById(string id);
        public PrimitiveBaseResponse<bool> deleteUserById(string id, string token);
        public PrimitiveBaseResponse<bool> updateUserById(string id, ApplicationUser user, string token);
    }
}
