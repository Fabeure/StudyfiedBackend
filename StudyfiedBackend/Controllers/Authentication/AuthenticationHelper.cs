using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using StudyfiedBackend.Models;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Xml.Schema;
using StudyfiedBackend.DataLayer;
using System.Diagnostics.Eventing.Reader;

namespace StudyfiedBackend.Controllers.Authentication
{
    public static class AuthenticationHelper
    {
        private readonly static ConcurrentDictionary<string, UserToken> tokenCache = new ConcurrentDictionary<string, UserToken>();

        public static UserToken Parse(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var id = decodedToken.Claims.ElementAt(0).Value;
            var email = decodedToken.Claims.ElementAt(1).Value;
            return new UserToken(id, email, DateTime.Now.AddHours(-1));
        }
        public static ApplicationUser processToken(string token, IMongoRepository<ApplicationUser> userRepository)
        {
            UserToken userToken;
            if (tokenCache.TryGetValue(token, out userToken))
            {
                if (userToken.IsExpired)
                {
                    tokenCache.TryRemove(token, out _);
                    return null;
                }
                userToken.UpdateLastAccessed();
                return userRepository.GetByIdAsync(userToken.id).Result;
            }
            else{
                userToken = Parse(token);
                var user = userRepository.GetByIdAsync(userToken.id).Result;
                tokenCache[token] = userToken;
                return user; 
            }
        }
    }
}
