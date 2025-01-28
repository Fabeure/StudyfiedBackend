using StudyfiedBackend.BaseResponse;
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

        public bool ValidatePassword(string plainPassword, string hashedPassword)
        {
            // Compare the plain-text password with the hashed password
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }
    }

}
