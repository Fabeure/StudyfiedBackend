using AspNetCore.Identity.MongoDbCore.Infrastructure;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StudyfiedBackend.Controllers.Authentication
{
    public interface IAuthenticationService
    {
        public BaseResponse<ApplicationUser> AuthenticateTokenAndGetUser(string token);
    }
}
