using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Authentication;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public class UsersService : IUsersService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;
        private readonly IAuthenticationService _authenticationService;


        public UsersService(IMongoContext context, IAuthenticationService authenticationService) { 
            _userRepository = context.GetRepository<ApplicationUser>();
            _authenticationService = authenticationService;
        }

        public BaseResponse<ApplicationUser> getUserById(string id)
        {
            ApplicationUser user = _userRepository.GetByIdAsync(id).Result;

            if (user != null)
            {
                return new BaseResponse<ApplicationUser>(ResultCodeEnum.Success, user, $"Succesfully fetched user {user.Id}");
            }
            return new BaseResponse<ApplicationUser>(ResultCodeEnum.Failed, null, $"No user with id {id} found");
        }

        public PrimitiveBaseResponse<bool> deleteUserById(string id, string token)
        {
            try
            {
                ApplicationUser caller = _authenticationService.AuthenticateTokenAndGetUser(token);
            }
            catch (Exception ex)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, "USER NOT AUTHORIZED");
            }

            bool deleteResult = _userRepository.DeleteAsync(id).Result;

            if (deleteResult)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, null, $"user with id {id} deleted");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"user could not be deleted");
        }
        public PrimitiveBaseResponse<bool> updateUserById(string id, ApplicationUser user, string token)
        {
            try
            {
                //ApplicationUser caller = _authenticationService.AuthenticateTokenAndGetUser(token);
            }
            catch (Exception ex)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, "USER NOT AUTHORIZED");
            }

            bool updateResult = _userRepository.UpdateAsync(id, user).Result;

            if (updateResult)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, null, $"user with id {id} updated");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"user could not be deleted");
        }
    }
}
