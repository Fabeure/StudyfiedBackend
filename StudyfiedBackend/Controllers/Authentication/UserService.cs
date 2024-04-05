﻿using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StudyfiedBackend.Controllers.Authentication
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;
       

        public UserService(IMongoContext context)
        {
            _userRepository = context.GetRepository<ApplicationUser>();
        }

        public BaseResponse<ApplicationUser> AuthenticateTokenAndGetUser(string token)
        {
            var user = AuthenticationHelper.processToken(token, _userRepository);
            if (user == null)
            {
                return new BaseResponse<ApplicationUser>(
                    ResultCodeEnum.Unauthorized,
                    null);
            }
            return new BaseResponse<ApplicationUser> (
                ResultCodeEnum.Success,
                user);
        }
      

    }

}
