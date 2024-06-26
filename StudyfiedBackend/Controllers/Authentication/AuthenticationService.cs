﻿using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;
       

        public AuthenticationService(IMongoContext context)
        {
            _userRepository = context.GetRepository<ApplicationUser>();
        }

        public ApplicationUser AuthenticateTokenAndGetUser(string token)
        {
            var user = AuthenticationHelper.processToken(token, _userRepository);
            if (user == null)
            {
                throw new Exception(message: "Invalid token, please try again");
            }
            return user;
        }
    }

}
